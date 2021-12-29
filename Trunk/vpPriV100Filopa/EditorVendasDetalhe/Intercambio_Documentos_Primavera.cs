using BasBE100;
using ErpBS100;
using Microsoft.VisualBasic;
using StdBE100;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vimaponto.PrimaveraV100;
using Vimaponto.PrimaveraV100.Clientes.Filopa.EditorVendasDetalhe.DataSets;
using VndBE100;
using CmpBE100;

namespace EditorVendasDetalhe
{

    class Intercambio_Documentos_Primavera
    {
        #region Enumeracoes
        public enum Modo_Edicao
        {
            NOVO,
            EDITADO
        }
        #endregion
        #region
        public bool Copiar_BD_Origem_DocVenda_PARA_BD_Destino_DocCompra(string Empresa_Origem, string Filial_Documento_Origem, string Tipo_Documento_Origem, string Serie_Documento_Origem, int Numero_Documento_Origem, Modo_Edicao Modo)
        {
            // ----------------------------------------------------------------
            // --- 2019.04.12 - VIMAPONTO - Gualter Costa  - Redmine # 1558 ---
            // ----------------------------------------------------------------
            ErpBS Motor_Primavera_BD_Origem = new ErpBS();
            ErpBS Motor_Primavera_BD_Destino = new ErpBS();

            VndBEDocumentoVenda Documento_ORIGEM_VENDA = new VndBEDocumentoVenda();
            CmpBEDocumentoCompra Documento_DESTINO_COMPRA = new CmpBEDocumentoCompra();

            string Empresa_Destino = "";
            string Tipo_Documento_Destino = "";
            string Serie_Documento_Destino = "";
            List<string> listaIdsQtn = new List<string>();

            StdBELista Lista_Primavera = new StdBELista();
            int Nr_Linhas;
            string Sql_Query;

            //Copiar_BD_Origem_DocVenda_PARA_BD_Destino_DocCompra = false; // Inicializa a o retorno a false

            // --------------------------------------------------------------------------
            // ---  1. ABERTURA DOS MOTORES PRIMAVERA PARA A BASE DE DADOS DE ORIGEM  ---
            // --------------------------------------------------------------------------
            try
            {
                // Motor_Primavera_BD_Origem.AbreEmpresaTrabalho(0, Empresa_Origem, "VIMAPONTO", "PRI1774")
                Motor_Primavera_BD_Origem = PriV100Api.BSO;        // OBTEM AS CREDENCIAIS PRIMAVERA (PLATAFORMA/UTILIZADOR/PASSWORD) DO OBJECTO DO PRIMAVERA
            }
            catch (Exception ex)
            {
                MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "Erro ao abrir o Motor Primavera Empresa Origem : " + Empresa_Origem, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // --------------------------------------------------------------
            // --- 2. CARREGAMENTO DO DOCUMENTO DE VENDA DE NA BD ORIGEM ----
            // --------------------------------------------------------------
            // Verifica se o documento de origem especificado pelo utilizador existe na base de dados de origem
            if (!Motor_Primavera_BD_Origem.Vendas.Documentos.Existe(Filial_Documento_Origem, Tipo_Documento_Origem, Serie_Documento_Origem, Numero_Documento_Origem))
            {
                MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "Não existe o documento origem especificado na base de dados de origem!" + Strings.Chr(13) + Strings.Chr(13) + "NÃO FOI EFECTUADA A CRIAÇÃO DO DOCUMENTO NA BASE DE DADOS DE DESTINO!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // CARREGA  o objecto com os dados do documento venda de origem
            Documento_ORIGEM_VENDA = Motor_Primavera_BD_Origem.Vendas.Documentos.Edita(Filial_Documento_Origem, Tipo_Documento_Origem, Serie_Documento_Origem, Numero_Documento_Origem);

            // Verifica na bd de origem se o fornecedor (indicado no campo CDU_Fornecedor do documento de origem) é uma empresa do grupo (se tem o campo CDU_NomeEmpresaGrupo preenchido). Se sim avança, se não sai e não faz a cópia.
            if (Strings.Trim(Motor_Primavera_BD_Origem.Base.Fornecedores.DaValorAtributo(Documento_ORIGEM_VENDA.CamposUtil["CDU_Fornecedor"].Valor.ToString(), "CDU_NomeEmpresaGrupo")) == "")
                return false;
            else if (Strings.Trim(Motor_Primavera_BD_Origem.Base.Fornecedores.DaValorAtributo(Documento_ORIGEM_VENDA.CamposUtil["CDU_Fornecedor"].Valor.ToString(), "CDU_EntidadeInterna")) == "")
            {
                MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "O fornecedor indicado não tem o campo CDU_EntidadeInterna preenchido. " + Strings.Chr(13) + "Para efectuar a cópia do documento para a base de dados de outra empresa, é necessário que o campo CDU_EntidadeInterna esteja preenchido na ficha de fornecedor." + Strings.Chr(13) + Strings.Chr(13) + "NÃO FOI EFECTUADA A CRIAÇÃO DO DOCUMENTO NA BASE DE DADOS DE DESTINO!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            // Verifica se retornou um documento de venda válido
            if (Strings.Trim(Documento_ORIGEM_VENDA.Filial) == "" | Strings.Trim(Documento_ORIGEM_VENDA.Tipodoc) == "" | Strings.Trim(Documento_ORIGEM_VENDA.Serie) == "" | Strings.Trim(Documento_ORIGEM_VENDA.NumDoc.ToString()) == "")
            {
                MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível abrir o documento de origem !!!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            // -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // ---- Obtem a base de dados de destino e qual o código de fornecedor deste cliente na base de dados de destino (segundo a regra descrita nos 3 passos abaixo)                                                              --- 
            // --- (Mais informações ver ESQUEMA COMO SE OBTEM O FORNECEDOR NA BD DESTINO.PDF no RedMine #1558)                                                                                                                          ---
            // -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            string Documento_ORIGEM_VENDA_CDU_Fornecedor;
            string CDU_EntidadeInterna_Cliente_BD_Origem;
            string Nome_Cliente_BD_Origem;
            // Dim CDU_EntidadeInterna_Fornecedor_BD_Origem As String = ""
            string CDU_NomeEmpresaGrupo_Fornecedor_BD_Origem = "";
            string CDU_EntidadeInterna_Fornecedor_BD_Destino = "";
            string Codigo_Entidade_BD_Destino = "";


            // 1-Acha o fornecedor "especial" que está especificado no campo CDU_Fornecedor do documento de venda
            Documento_ORIGEM_VENDA_CDU_Fornecedor = (Convert.IsDBNull(Documento_ORIGEM_VENDA.CamposUtil["CDU_Fornecedor"].Valor.ToString())) ? "" : Documento_ORIGEM_VENDA.CamposUtil["CDU_Fornecedor"].Valor.ToString();

            if (Strings.Trim(Documento_ORIGEM_VENDA_CDU_Fornecedor) == "")
            {
                MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "O documento de Origem não tem o campo CDU_Fornecedor preenchido." + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível efectuar a cópia do documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            // 2-Obtem o valor do CDU_EntidadeInterna do cliente/fornecedor na tabela Fornecedores na base de dados de Origem (para o fornecedor especial) -
            CDU_NomeEmpresaGrupo_Fornecedor_BD_Origem = Motor_Primavera_BD_Origem.Base.Fornecedores.DaValorAtributo(Documento_ORIGEM_VENDA_CDU_Fornecedor, "CDU_NomeEmpresaGrupo");



            // 'ANTIGO : O Fornecedor da EMPRESA DA BASE DE DADOS DE DESTINO, é o Fornecedor no documento de compra na base de dados de destino (encontrado pelo CDU_EntidadeInterna)
            // 'CDU_EntidadeInterna_Fornecedor_BD_Origem = Motor_Primavera_BD_Origem.Comercial.Fornecedores.DaValorAtributo(Documento_ORIGEM_VENDA_CDU_Fornecedor, "CDU_EntidadeInterna")

            // If Strings.Trim(CDU_NomeEmpresaGrupo_Fornecedor_BD_Origem) = "" Then
            // MsgBox("AVISO : " & Chr(13) & Chr(13) & "Erro fornecedor indicado não tem o campo CDU_NomeEmpresaGrupo preenchido. " & Chr(13) & Chr(13) & "Não foi possível efectuar a cópia do documento.", vbCritical + vbOK)
            // Exit Function
            // End If

            // If Strings.Trim(CDU_EntidadeInterna_Fornecedor_BD_Origem) = "" Then
            // MsgBox("AVISO : " & Chr(13) & Chr(13) & "Erro fornecedor indicado não tem o campo CDU_EntidadeInterna preenchido. " & Chr(13) & Chr(13) & "Não foi possível efectuar a cópia do documento.", vbCritical + vbOK)
            // Exit Function
            // End If



            // NOVO : O CLIENTE do documento de Origem é o Fornecedor do documento de compra na base de dados de destino (encontrado pelo CDU_EntidadeInterna) - 2019.07.09 - Alterado Por indicação do Rui
            CDU_EntidadeInterna_Cliente_BD_Origem = Motor_Primavera_BD_Origem.Base.Clientes.DaValorAtributo(Documento_ORIGEM_VENDA.Entidade, "CDU_EntidadeInterna");
            Nome_Cliente_BD_Origem = Motor_Primavera_BD_Origem.Base.Clientes.DaValorAtributo(Documento_ORIGEM_VENDA.Entidade, "Nome");

            if (Strings.Trim(CDU_EntidadeInterna_Cliente_BD_Origem) == "")
            {
                MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "O cliente " + Strings.Trim(Nome_Cliente_BD_Origem) + " não tem o campo CDU_EntidadeInterna preenchido. " + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível efectuar a cópia do documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // 3 - Define a base de dados da empresa de destino
            Empresa_Destino = CDU_NomeEmpresaGrupo_Fornecedor_BD_Origem;
            // -----------------------------------------------------------------------------------------------------------------------

            // -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // ---- Obtem o tipo de documento e a série do documento de destino (a partir das configurações que estão definidas nos campos CDU_TipoDocComprasDestino e CDU_SerieComprasDestino da  tabela DocumentosVenda da bd Origem) ----
            // -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            Lista_Primavera = Motor_Primavera_BD_Origem.Consulta("select CDU_TipoDocComprasDestino,CDU_SerieComprasDestino   from DocumentosVenda where documento = '" + Tipo_Documento_Origem + "'");

            if (Lista_Primavera.NumLinhas() == 1)
            {
                Lista_Primavera.Inicio();

                Tipo_Documento_Destino = (Convert.IsDBNull(Lista_Primavera.Valor("CDU_TipoDocComprasDestino"))) ? "" : Lista_Primavera.Valor("CDU_TipoDocComprasDestino");
                Serie_Documento_Destino = (Convert.IsDBNull(Lista_Primavera.Valor("CDU_SerieComprasDestino"))) ? "" : Lista_Primavera.Valor("CDU_SerieComprasDestino");
            }
            else
            {
                MessageBox.Show("Não foi possível obter corretamente as configurações do tipo de documento e série" + Strings.Chr(13) + " para o documento de destino." + Strings.Chr(13) + Strings.Chr(13) + "P.f. verifique as configurações na tabela DocumentosVenda (Campos : CDU_TipoDocComprasDestino e CDU_SerieComprasDestino) da base de dados de origem." + Strings.Chr(13) + Strings.Chr(13) + "NÃO FOI EFECTUADA A CRIAÇÃO DO DOCUMENTO NA BASE DE DADOS DE DESTINO " + Empresa_Destino + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            Lista_Primavera.Vazia();


            if (Tipo_Documento_Destino == "" & Serie_Documento_Destino == "")
            {
                return false;
            }

            // Verifica o tipo de documento de destino
            if (Strings.Trim(Tipo_Documento_Destino) == "")
            {
                MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível obter corretamente as configurações do tipo de documento" + Strings.Chr(13) + " para o documento de destino." + Strings.Chr(13) + Strings.Chr(13) + "P.f. verifique as configurações na tabela DocumentosVenda (Campos : CDU_TipoDocComprasDestino) da base de dados de origem." + Strings.Chr(13) + Strings.Chr(13) + "NÃO FOI EFECTUADA A CRIAÇÃO DO DOCUMENTO NA BASE DE DADOS DE DESTINO " + Empresa_Destino + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }



            // Verifica se conseguiu definir a série do documento de destino 
            if (Strings.Trim(Serie_Documento_Destino) == "")
            {
                MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível obter corretamente as configurações da série " + Strings.Chr(13) + " para o documento de destino." + Strings.Chr(13) + Strings.Chr(13) + "P.f. verifique as configurações na tabela DocumentosVenda (Campos : CDU_SerieComprasDestino) da base de dados de origem." + Strings.Chr(13) + Strings.Chr(13) + "NÃO FOI EFECTUADA A CRIAÇÃO DO DOCUMENTO NA BASE DE DADOS DE DESTINO " + Empresa_Destino + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else

                // Verifica se a série proposta existe na base de dados de destino
                if (Verifica_Se_Serie_Existe_na_BD_Destino(Empresa_Destino, Tipo_Documento_Destino, Serie_Documento_Destino) == false)
            {
                return false;
            }
            // ------------------------------------------------------------------------------------------------------------------------------------------------------
            // ---------------------------------------------------------------------------
            // ---  3. ABERTURA DOS MOTORES PRIMAVERA PARA A BASE DE DADOS DE DESTINO  ---
            // ---------------------------------------------------------------------------



            // Abre os Motores Primavera (para a empresa de Destino)
            try
            {

                // Motor_Primavera_BD_Destino.AbreEmpresaTrabalho(0, Empresa_Destino, "VIMAPONTO", "PRI1774")
                Motor_Primavera_BD_Destino.AbreEmpresaTrabalho(PriV100Api.BSO.Contexto.TipoPlataforma, Empresa_Destino, PriV100Api.BSO.Contexto.UtilizadorActual, PriV100Api.BSO.Contexto.PasswordUtilizadorActual);
            }
            catch (Exception ex)
            {
                MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "Erro ao abrir o Motor Primavera Empresa Destino : " + Empresa_Destino, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            // ------------------------------------------------------------------------------------------------------------------------
            // ---  4. PASSAGEM DOS DADOS DO DOCUMENTO DE ORIGEM (NA BD DE ORIGEM) PARA O DOCUMENTO DE DESTINO (NA BD DE DESTINO)  ----
            // ------------------------------------------------------------------------------------------------------------------------

            // NOTA MUITO IMPORTANTE : (GMC 2019.05.10)
            // Esta passagem de dados e o mapeamento dos campos na cópia são os especificados na pág 3 e 4 do documento do Rui Fernandes e SÃO 100% 
            // específicos para o mecanismo de cópia de documento entre a FILOPA e as restantes bases de dados do grupo MUNDIFIOS


            switch (Modo)
            {
                case Modo_Edicao.NOVO:
                    {
                        // --------------------------------------------------------------------------------------------------------------------------------------
                        // --- SE MODO EDICAO = NOVO - CRIA NOVO documento de compra vazio na de compra na base de dados de destino                           ---
                        // - Carrega todo o cabeçalho com base no documento de venda de origem                                      ---
                        // - Carrega as linhas no documento de compra na base de dados de destino                                   ---
                        // - Carrega os anexos no documento de compra na base de dados de destino                                   ---
                        // --------------------------------------------------------------------------------------------------------------------------------------
                        // Por segurança antes de criar novo documento de destno, confirma se o documento de venda de origem já tem preenchido o campo CDU_DocumentoCompraDestino (ou seja se já gerou um documento na bd de destino), se sim sai
                        if (Strings.Len(Documento_ORIGEM_VENDA.CamposUtil["CDU_DocumentoCompraDestino"].Valor.ToString()) > 1)
                        {
                            MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "Este documento de venda já originou o documento compra " + Strings.Trim(Documento_ORIGEM_VENDA.CamposUtil["CDU_DocumentoCompraDestino"].Valor.ToString()) + " na  base de dados de destino " + Empresa_Destino + "." + Strings.Chr(13) + Strings.Chr(13) + "NÃO FOI EFECTUADA A CRIAÇÃO DO DOCUMENTO NA BASE DE DADOS DE DESTINO " + Empresa_Destino + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                        }


                        // Incializa novo documento de compra de destino
                        Documento_DESTINO_COMPRA = new CmpBEDocumentoCompra();
                        break;
                    }

                case Modo_Edicao.EDITADO:
                    {


                        // ------------------------------------------------------------------------------------------------------------------------------------------------------------
                        // --- SE MODO EDICAO = EDITADO - EDITA o documento compra (relacionado com este documento de venda) na base de dados de destino (se já exitir)             ---
                        // - CRIA NOVO documento de compra (relacionado com este documento de venda) na base de dados de destino (se ainda não exitir) ---
                        // - Recarrega todo o cabeçalho com base no documento de venda de origem                                                       ---
                        // - Apaga todas as linhas do documento de compra na base de dados de destino                                                    ---              
                        // - Recarrega as linhas no documento de compra na base de dados de destino                                                    ---
                        // - Recarrega os anexos no documento de compra na base de dados de destino                                                    ---
                        // ------------------------------------------------------------------------------------------------------------------------------------------------------------



                        // -------------------------------------------------------------------------------------------------------
                        // --- Verifica o documento de compra na base de dados de destino que o documento de venda atual criou ---
                        // -------------------------------------------------------------------------------------------------------

                        // Acha qual a base de dados de destino e qual o documento de compra (noutra base de dados da do Grupo Mundifios), que teve origem a partir do presente documento de venda deu origem

                        string Empresa_Documento_Compra_Destino;

                        string Filial_Documento_Compra_Destino;
                        string TipoDoc_Documento_Compra_Destino;
                        string Serie_Documento_Compra_Destino;
                        int NumDoc_Documento_Compra_Destino;

                        string Campo_Documento_Compra_Destino;
                        int Aux_Posicao_Espaco;
                        int Aux_Posicao_Barra;


                        try
                        {


                            // Acha qual a empresa de destino (com base no campo CDU_Fornecedor da ficha de forncedor da base de dados de origem)
                            Empresa_Documento_Compra_Destino = Strings.Trim(PriV100Api.BSO.Base.Fornecedores.DaValorAtributo(Documento_ORIGEM_VENDA.CamposUtil["CDU_Fornecedor"].Valor.ToString(), "CDU_NomeEmpresaGrupo"));


                            // Acha os dados do documento de destino, com base no campo CDU_DocumentoCompraDestino do documento de origem
                            Campo_Documento_Compra_Destino = (Strings.Trim(Documento_ORIGEM_VENDA.CamposUtil["CDU_DocumentoCompraDestino"].Valor.ToString()) == "0" ? "" : Strings.Trim(Documento_ORIGEM_VENDA.CamposUtil["CDU_DocumentoCompraDestino"].Valor.ToString()));



                            // Verifica se conseguiu obter a base de dados de destino e identificador do documento de compra gerado automáticamente por este documento de venda na base de dados de destino.
                            // Se sim,  edita o documento de compra já existente na base de dados de destino (e que foi gerado no passado por este documento de venda) e atualiza-o 
                            if (Strings.Trim(Empresa_Documento_Compra_Destino) != "" & Strings.Trim(Campo_Documento_Compra_Destino) != "")
                            {
                                Aux_Posicao_Espaco = Campo_Documento_Compra_Destino.IndexOf(" ") + 1;
                                Aux_Posicao_Barra = Campo_Documento_Compra_Destino.IndexOf("/") + 1;


                                Filial_Documento_Compra_Destino = "000";
                                TipoDoc_Documento_Compra_Destino = Strings.Trim(Strings.Mid(Campo_Documento_Compra_Destino, 1, Aux_Posicao_Espaco));
                                Serie_Documento_Compra_Destino = Strings.Trim(Strings.Mid(Campo_Documento_Compra_Destino, Aux_Posicao_Espaco, Aux_Posicao_Barra - Aux_Posicao_Espaco));
                                NumDoc_Documento_Compra_Destino = (int)Conversion.Val(Strings.Trim(Strings.Mid(Campo_Documento_Compra_Destino, Aux_Posicao_Barra + 1, Strings.Len(Campo_Documento_Compra_Destino))));

                                if (Serie_Documento_Compra_Destino != Serie_Documento_Destino & Serie_Documento_Compra_Destino != "" & Serie_Documento_Destino != "")
                                    Serie_Documento_Destino = Serie_Documento_Compra_Destino;


                                // Abre o documento de compra já existente na bd destino
                                Documento_DESTINO_COMPRA = Motor_Primavera_BD_Destino.Compras.Documentos.Edita(Filial_Documento_Compra_Destino, TipoDoc_Documento_Compra_Destino, Serie_Documento_Compra_Destino, NumDoc_Documento_Compra_Destino);


                                // Limpa todas as linhas do documento de compra já existente na bd destino (mais à frente recopia as linhas atualizadas)
                                Documento_DESTINO_COMPRA.Linhas.RemoveTodos();
                            }
                            else

                                // Incializa novo documento de compra de destino
                                Documento_DESTINO_COMPRA = new CmpBEDocumentoCompra();
                        }


                        catch (Exception ex)
                        {
                            MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível detectar qual a base de dados ou qual o número do documento de compra gerado  na base de dados de destino automáticamente por este documento de venda." + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível efectuar a cópia do documento para a base de dados de destino " + Empresa_Destino + Strings.Chr(13) + Strings.Chr(13), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }

                        break;
                    }

                default:
                    {
                        MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "Modo de edição desconhecido!" + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível efectuar a cópia do documento para a base de dados de destino " + Empresa_Destino, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
            }

            // --------------------------------------------------------------
            // --- PREENCHE OS DADOS DO CABEÇALHO DO DOCUMENTO DE DESTINO ---
            // --------------------------------------------------------------

            Documento_DESTINO_COMPRA.Filial = Documento_ORIGEM_VENDA.Filial;
            Documento_DESTINO_COMPRA.Tipodoc = Strings.Trim(Tipo_Documento_Destino);
            Documento_DESTINO_COMPRA.Serie = Strings.Trim(Serie_Documento_Destino);
            // Pelo CDU_EntidadeInterna do fornecedor na bd de origem, procura na tabela Fornecedor da base de dados de Destino o respetivo código de fornecedor, para ser usado como entidade no documento de compra de destino
            // (Mais informações ver ESQUEMA COMO SE OBTEM O FORNECEDOR NA BD DESTINO.PDF no RedMine #1558)

            try
            {
                StdBELista RsListaQuery = new StdBELista();


                // 'ANTIGO : O Fornecedor da EMPRESA DA BASE DE DADOS DE DESTINO, é o Fornecedor no documento de compra na base de dados de destino (encontrado pelo CDU_EntidadeInterna)
                // RsListaQuery = Motor_Primavera_BD_Destino.Consulta("SELECT Fornecedor from fornecedores where CDU_EntidadeInterna = '" & Trim(CDU_EntidadeInterna_Fornecedor_BD_Origem) & "'")


                // NOVO : O CLIENTE do documento de Origem é o Fornecedor do documento de compra na base de dados de destino (encontrado pelo CDU_EntidadeInterna) - 2019.07.09 - Alterado Por indicação do Rui
                RsListaQuery = Motor_Primavera_BD_Destino.Consulta("SELECT Fornecedor from fornecedores where CDU_EntidadeInterna = '" + Strings.Trim(CDU_EntidadeInterna_Cliente_BD_Origem) + "'");


                while (!RsListaQuery.NoFim())
                {
                    Codigo_Entidade_BD_Destino = RsListaQuery.Valor("Fornecedor"); // Codigo do fornecedor (na BD_Destino)
                    RsListaQuery.Seguinte();
                }
                RsListaQuery = null/* TODO Change to default(_) if this is not a reference type */;


                // Verifica se conseguiu obter o código do fornecedor na base de dados da empresa de destino.
                if (Strings.Trim(Codigo_Entidade_BD_Destino) == "")
                {
                    MessageBox.Show("AVISO:" + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível identificar o código do fornecedor " + Strings.Trim(Nome_Cliente_BD_Origem) + ", com o CDU_EntidadeInterna = " + CDU_EntidadeInterna_Cliente_BD_Origem + " na base de dados de destino " + Strings.UCase(Empresa_Destino) + "." + Strings.Chr(13) + Strings.Chr(13) + "P.f. verifique se o fornecedor existe na base de dados de destino e se tem o campo CDU_EntidadeInterna devidamente preenchido." + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível efectuar a cópia do documento para a base de dados de destino " + Empresa_Destino, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível identificar o código do fornecedor na base de dados de destino." + Strings.Chr(13) + Strings.Chr(13) + "P.f. verifique se o fornecedor existe na base de dados de destino e se tem o campo CDU_EntidadeInterna devidamente preenchido." + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível efectuar a cópia do documento para a base de dados de destino " + Empresa_Destino, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }


            Documento_DESTINO_COMPRA.TipoEntidade = "F";
            Documento_DESTINO_COMPRA.Entidade = Codigo_Entidade_BD_Destino;

            if (Documento_DESTINO_COMPRA.EmModoEdicao == false)

                // Preenche os dados relacionados no documento destino 
                Motor_Primavera_BD_Destino.Compras.Documentos.PreencheDadosRelacionados(Documento_DESTINO_COMPRA);
            else
            {
            }
            // --- CABEÇALHO ---> Copia de campos de documento de Origem para o Documento de Destino (conforme o espefificado na pág 3 documento do Rui) ----

            Documento_DESTINO_COMPRA.DataDoc = Documento_ORIGEM_VENDA.DataDoc; // Data
            Documento_DESTINO_COMPRA.DescFornecedor = Documento_ORIGEM_VENDA.DescEntidade;
            Documento_DESTINO_COMPRA.DescFinanceiro = Documento_ORIGEM_VENDA.DescFinanceiro;  // DescPag
            Documento_DESTINO_COMPRA.NumDocExterno = Documento_ORIGEM_VENDA.Referencia;
            Documento_DESTINO_COMPRA.ModoPag = Documento_ORIGEM_VENDA.ModoPag;
            Documento_DESTINO_COMPRA.DataVenc = Documento_ORIGEM_VENDA.DataVenc;
            Documento_DESTINO_COMPRA.CondPag = Documento_ORIGEM_VENDA.CondPag;
            Documento_DESTINO_COMPRA.ModoExp = Documento_ORIGEM_VENDA.ModoExp;
            Documento_DESTINO_COMPRA.Moeda = Documento_ORIGEM_VENDA.Moeda;
            Documento_DESTINO_COMPRA.Cambio = Documento_ORIGEM_VENDA.Cambio;
            Documento_DESTINO_COMPRA.Observacoes = Documento_ORIGEM_VENDA.Observacoes;
            Documento_DESTINO_COMPRA.CamposUtil["CDU_CondEntrega"].Valor = Documento_ORIGEM_VENDA.CamposUtil["CDU_Incoterms"].Valor;
            Documento_DESTINO_COMPRA.CamposUtil["CDU_NumeroLC"].Valor = Documento_ORIGEM_VENDA.CamposUtil["CDU_NCartaCredito"].Valor;
            Documento_DESTINO_COMPRA.CamposUtil["CDU_Banco"].Valor = Obtem_CDU_BANCO_DESTINO(Documento_ORIGEM_VENDA.CamposUtil["CDU_Banco"].Valor.ToString());

            Documento_DESTINO_COMPRA.DataIntroducao = Documento_ORIGEM_VENDA.DataDoc;

            if (Convert.IsDBNull(Documento_DESTINO_COMPRA.CamposUtil["CDU_CustoCalculado"].Valor) == true | Documento_DESTINO_COMPRA.CamposUtil["CDU_CustoCalculado"].Valor == null)
                Documento_DESTINO_COMPRA.CamposUtil["CDU_CustoCalculado"].Valor = 0;
            // --- Guarda a rastreabilidade para o documento de origem no documento de destino (no campo CDU_DocumentoOrigem) --- 

            Documento_DESTINO_COMPRA.CamposUtil["CDU_DocumentoOrigem"].Valor = Strings.Trim(Documento_ORIGEM_VENDA.Tipodoc) + " " + Strings.Trim(Documento_ORIGEM_VENDA.Serie) + "/" + Strings.Trim(Documento_ORIGEM_VENDA.NumDoc.ToString());
            Documento_DESTINO_COMPRA.CamposUtil["CDU_BaseDadosOrigem"].Valor = Empresa_Origem;
            // -------------------------------------------------------------
            // --- PREENCHE OS DADOS DAS LINHAS DO DOCUMENTO DE DESTINO  ---
            // -------------------------------------------------------------


            // Obtem o nº de linhas do documento de origem
            Nr_Linhas = Documento_ORIGEM_VENDA.Linhas.NumItens;



            // Insere todas as linhas do documento de origem no documento de destino
            for (var i = 1; i <= Nr_Linhas; i++)
            {

                // Verifica se a linha tem artigo indicado ou se é só linha com comentário

                // Se LINHA NORMAL (com artigo)
                if ((Strings.Trim(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).TipoLinha) == "10" | Strings.Trim(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).TipoLinha) == "20") & Strings.Trim(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Artigo) != "")
                {

                    // Verifica se o artigo  na base de dados de destino
                    if (Motor_Primavera_BD_Destino.Base.Artigos.Existe(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Artigo))
                    {

                        // Adiciona nova linha no documento de destino
                        Motor_Primavera_BD_Destino.Compras.Documentos.AdicionaLinha(Documento_DESTINO_COMPRA, Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Artigo);

                        // --- LINHAS ---> Copia de campos de documento de Origem para o Documento de Destino (conforme o espefificado na pág 3 e 4 documento do Rui) ----

                        if (Strings.Trim(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Lote) != "<L01>")
                        {

                            // Verifica se o artigo/lote já existe na bd destino, se ainda não existir cria-o na BD Destino
                            if (Verifica_Se_Artigo_Lote_Existe_na_Bd_Destino(Empresa_Destino, Strings.Trim(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Artigo), Strings.Trim(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Lote)) == false)
                            {

                                // Cria na base de dados de destino o registo do artigo/lote, herdando as propriedades do artigo/lote da base de dados de origem

                                Sql_Query = "insert into ArtigoLote (Artigo,Lote,Descricao,DataFabrico,Validade,Controlador,Activo,Observacoes,CDU_TipoQualidade,CDU_LoteForn,CDU_DespDAU,CDU_Regime,CDU_CodMerc,CDU_Contramarca,CDU_ContramarcaData,CDU_Parafinado,CDU_Humidade,CDU_Fornecedor) " + "select Artigo,Lote,Descricao,DataFabrico,Validade,Controlador,Activo,Observacoes,CDU_TipoQualidade,CDU_LoteForn,CDU_DespDAU,CDU_Regime,CDU_CodMerc,CDU_Contramarca,CDU_ContramarcaData,CDU_Parafinado,CDU_Humidade,'" + Strings.Trim(Codigo_Entidade_BD_Destino) + "' from PRI" + Strings.Trim(Empresa_Origem) + "..ArtigoLote where Artigo = '" + Strings.Trim(Strings.Trim(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Artigo)) + "' and Lote = '" + Strings.Trim(Strings.Trim(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Lote)) + "'";

                                Motor_Primavera_BD_Destino.DSO.ExecuteSQL(Sql_Query);
                            }

                            Sql_Query = "update ArtigoLote set CDU_Parafinado = '" + Documento_ORIGEM_VENDA.Linhas.GetEdita(i).CamposUtil["CDU_Parafinado"].Valor + "', CDU_TipoQualidade = '" + Documento_ORIGEM_VENDA.Linhas.GetEdita(i).CamposUtil["CDU_TipoQualidade"].Valor + "', CDU_LoteForn = '" + Documento_ORIGEM_VENDA.Linhas.GetEdita(i).CamposUtil["CDU_LoteFornecedor"].Valor + "' where artigo = '" + Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Artigo + "' and lote = '" + Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Lote + "' ";

                            Motor_Primavera_BD_Destino.DSO.ExecuteSQL(Sql_Query);


                            Sql_Query = "select CDU_Fornecedor from artigolote where artigo = '" + Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Artigo + "' and lote = '" + Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Lote + "' ";
                            Lista_Primavera.Inicio();
                            Lista_Primavera = PriV100Api.BSO.Consulta(Sql_Query);

                            if (Convert.IsDBNull(Lista_Primavera.Valor("CDU_Fornecedor")))
                            {
                                Sql_Query = "update ArtigoLote set CDU_Fornecedor = '" + Lista_Primavera.Valor("CDU_Fornecedor").ToString() + "' where artigo = '" + Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Artigo + "' and lote = '" + Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Lote + "' ";

                                Motor_Primavera_BD_Destino.DSO.ExecuteSQL(Sql_Query);
                            }
                            Lista_Primavera.Fim();
                        }



                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).Lote = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Lote;


                        // Não passa a descrição da linha do documento de origem! 
                        // Passa sim a descrição do artigo + descrição extra do artigo na base de dados de origem.
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).Descricao = Strings.Left(Motor_Primavera_BD_Origem.Base.Artigos.DaValorAtributo(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Artigo, "Descricao") + " - " + Motor_Primavera_BD_Origem.Base.Artigos.DaValorAtributo(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Artigo, "CDU_DescricaoExtra"), 200);


                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).CamposUtil["CDU_LimiteEmbarque"].Valor = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).DataEntrega;
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).PrecUnit = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).PrecUnit;
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).Desconto1 = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Desconto1;
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).Desconto2 = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Desconto2;
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).Desconto3 = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Desconto3;
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).Unidade = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Unidade;
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).FactorConv = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).FactorConv;
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).Quantidade = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Quantidade;
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).IntrastatCodigoPautal = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).IntrastatCodigoPautal;

                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).Armazem = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Armazem;
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).Localizacao = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Localizacao;




                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).CamposUtil["CDU_NumContentor"].Valor = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).CamposUtil["CDU_NContentor"].Valor.ToString();
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).CamposUtil["CDU_LoteForn"].Valor = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).CamposUtil["CDU_LoteFornecedor"].Valor.ToString();
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).CamposUtil["CDU_Situacao"].Valor = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).CamposUtil["CDU_Situacao"].Valor.ToString();
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).CamposUtil["CDU_Parafinado"].Valor = (Convert.IsDBNull(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).CamposUtil["CDU_Parafinado"].Valor)) ? 0 : (Documento_ORIGEM_VENDA.Linhas.GetEdita(i).CamposUtil["CDU_Parafinado"].Valor);
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).CamposUtil["CDU_TipoQualidade"].Valor = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).CamposUtil["CDU_TipoQualidade"].Valor.ToString();
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).CamposUtil["CDU_PaisOrigem"].Valor = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).CamposUtil["CDU_PaisOrigem"].Valor.ToString();
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).CamposUtil["CDU_Seguradora"].Valor = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).CamposUtil["CDU_Seguradora"].Valor.ToString();
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).CamposUtil["CDU_NumCertificado"].Valor = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).CamposUtil["CDU_NumCertificado"].Valor.ToString();
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).CamposUtil["CDU_Parque"].Valor = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).CamposUtil["CDU_Parque"].Valor.ToString();
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).CamposUtil["CDU_Observacoes"].Valor = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).CamposUtil["CDU_ObsMdf"].Valor.ToString();
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).CamposUtil["CDU_ComissaoCustosFilopa"].Valor = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).CamposUtil["CDU_Comissao"].Valor.ToString();



                        // --- CABECALHO para LINHAS ---> Copia de campos do cabeçalho do documento de Origem para as Linhas do Documento de Destino (conforme o espefificado na pág 3 e 4 do documento do Rui) ----
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).CamposUtil["CDU_Porto"].Valor = Documento_ORIGEM_VENDA.CamposUtil["CDU_Porto"].Valor.ToString();
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).CamposUtil["CDU_Destino"].Valor = Documento_ORIGEM_VENDA.CamposUtil["CDU_Destino"].Valor.ToString();
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).CamposUtil["CDU_CompMaritima"].Valor = Documento_ORIGEM_VENDA.CamposUtil["CDU_CompanhiaMaritima"].Valor.ToString();
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).DataEntrega = DateTime.Parse(Documento_ORIGEM_VENDA.CamposUtil["CDU_DataPrevistaChegada"].Valor.ToString());    // Converte só para Data pois o primavera não consegue consegue visualizar nos campos data, valores datahora
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).CamposUtil["CDU_NumBL"].Valor = Documento_ORIGEM_VENDA.CamposUtil["CDU_NBL"].Valor.ToString();

                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).CamposUtil["CDU_IDLinhaOriginalGrupo"].Valor = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).IdLinha;
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).Fechado = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Fechado;


                        if (Strings.Trim(Documento_ORIGEM_VENDA.Tipodoc) == "EMB" & Strings.Trim(Documento_DESTINO_COMPRA.Tipodoc) == "ECF")
                        {
                            Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).ModuloOrigemCopia = "C";
                            Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).IdLinhaOrigemCopia = Obtem_Id_Linha_Origem_no_Documento_CNT_da_BD_Destino(Motor_Primavera_BD_Destino, Documento_ORIGEM_VENDA.Linhas.GetEdita(i).IDLinhaOriginal);

                            if (Strings.Trim(Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).IdLinhaOrigemCopia) == "")
                            {
                                MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível estabelecer a rastreabilidade por linha entre os documentos na base de dados de destino " + Strings.Chr(13) + "entre a linha nº " + Documento_DESTINO_COMPRA.Linhas.NumItens.ToString() + " deste documento 'ECF' e a linha respectiva no documento 'CNT' a ele ligado." + Strings.Chr(13) + Strings.Chr(13) + "NÃO FOI EFECTUADA A CRIAÇÃO/ATUALIZAÇÃO DO DOCUMENTO NA BASE DE DADOS DE DESTINO", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                // Se deu erro, faz o rollback da transaccao
                                Motor_Primavera_BD_Origem.DesfazTransaccao();
                                Motor_Primavera_BD_Destino.DesfazTransaccao();

                                return false;
                            }
                            else
                            {
                                listaIdsQtn.Add(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Quantidade.ToString());
                                listaIdsQtn.Add(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).IdLinha);
                                listaIdsQtn.Add(Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).IdLinhaOrigemCopia.ToString());
                                int k = 0;
                                double QuantidadeAcc = 0;

                                if (listaIdsQtn.IndexOf(Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).IdLinhaOrigemCopia.ToString()) != listaIdsQtn.LastIndexOf(Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).IdLinhaOrigemCopia.ToString()))
                                {
                                    for (k = 0; k <= listaIdsQtn.Count() - 1; k++)
                                    {
                                        if (listaIdsQtn[k] == Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).IdLinhaOrigemCopia.ToString())
                                            QuantidadeAcc = Convert.ToDouble(QuantidadeAcc + listaIdsQtn[k - 2]);
                                    }
                                }

                                // listaIdsQtn.IndexOf(Documento_DESTINO_COMPRA.Linhas(Documento_DESTINO_COMPRA.Linhas.NumItens).IdLinhaOrigemCopia.ToString())
                                if (QuantidadeAcc > 0)
                                    Sql_Query = "UPDATE LinhasComprasStatus SET QuantTrans = " + Conversion.Str(QuantidadeAcc) + " WHERE IdLinhasCompras = '" + Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).IdLinhaOrigemCopia.ToString() + "'";
                                else
                                    Sql_Query = "UPDATE LinhasComprasStatus SET QuantTrans = " + Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Quantidade.ToString() + " WHERE IdLinhasCompras = '" + Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).IdLinhaOrigemCopia.ToString() + "'";

                                // Sql_Query = "UPDATE LinhasComprasStatus SET QuantTrans = " & Str(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Quantidade) & " WHERE IdLinhasCompras = '" & Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).IdLinhaOrigemCopia.ToString() & "'"

                                Motor_Primavera_BD_Destino.DSO.ExecuteSQL(Sql_Query);
                                // Motor_Primavera_BD_Destino.DSO.BDAPL.Execute("UPDATE LinhasComprasStatus SET QuantTrans = " & Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Quantidade & " WHERE IdLinhasCompras = '" & Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).IdLinhaOrigemCopia.ToString() & "'")
                                Motor_Primavera_BD_Destino.DSO.ExecuteSQL("UPDATE LinhasComprasStatus SET Fechado = '" + DaEstadoLinha(Motor_Primavera_BD_Destino, Documento_ORIGEM_VENDA.Linhas.GetEdita(i).IdLinha) + "' WHERE IdLinhasCompras = '" + Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).IdLinhaOrigemCopia + "'");
                            }
                        }


                        // ---- Tratamentos de linha ESPECÍFICOS POR TIPO DE DOCUMENTO  ----


                        // Só se o documento de venda de origem for "EMB"
                        if (Documento_ORIGEM_VENDA.Tipodoc == "EMB")
                            Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).CamposUtil["CDU_Embarque"].Valor = Documento_ORIGEM_VENDA.DataDoc;
                    }
                    else
                    {
                        MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível copiar as linhas para o documento de destino." + Strings.Chr(13) + Strings.Chr(13) + "O artigo " + Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Artigo + " não existe na base de dados da empresa de destino!" + Strings.Chr(13) + Strings.Chr(13) + "NÃO FOI EFECTUADA A CRIAÇÃO DO DOCUMENTO NA BASE DE DADOS DE DESTINO " + Empresa_Destino + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else if ((Strings.Trim(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).TipoLinha) == "10" | Strings.Trim(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).TipoLinha) == "60" | Strings.Trim(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).TipoLinha) == "65") & Strings.Trim(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Artigo) == "")
                {

                    // Adiciona nova linha especial no documento de destino
                    BasBETiposGcp.compTipoLinhaEspecial tipo_linha_especial = new BasBETiposGcp.compTipoLinhaEspecial();
                    tipo_linha_especial = BasBETiposGcp.compTipoLinhaEspecial.compLinha_Comentario;

                    Motor_Primavera_BD_Destino.Compras.Documentos.AdicionaLinhaEspecial(Documento_DESTINO_COMPRA, tipo_linha_especial);

                    if (Strings.Trim(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).TipoLinha) == "60" & Strings.Trim(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Artigo) == "" & Strings.Trim(Documento_ORIGEM_VENDA.Tipodoc) == "EMB" & Strings.Trim(Documento_DESTINO_COMPRA.Tipodoc) == "ECF" & i != Documento_ORIGEM_VENDA.Linhas.NumItens & Strings.Left(Strings.Trim(Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Descricao), 3) == "CNT")
                    {
                        for (var j = 1; j <= Nr_Linhas; j++)
                        {
                            Sql_Query = "select CDU_DocumentocompraDestino, Data from cabecdoc where id in (select IdCabecDoc  from LinhasDoc where id in (select IdLinhasDocOrigem from LinhasDocTrans where IdLinhasDoc in (select id from LinhasDoc where Id = '" + Strings.Trim(Documento_ORIGEM_VENDA.Linhas.GetEdita(i + j).IdLinha) + "' )))";
                            Lista_Primavera.Inicio();
                            Lista_Primavera = PriV100Api.BSO.Consulta(Sql_Query);

                            if (Lista_Primavera.NumLinhas() > 0)
                            {
                                Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).Descricao = Lista_Primavera.Valor("CDU_DocumentoCompraDestino").ToString() + " de " + Lista_Primavera.Valor("Data");
                                break;
                            }
                            else
                                Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).Descricao = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Descricao;
                            Lista_Primavera.Fim();
                        }
                    }
                    else
                        Documento_DESTINO_COMPRA.Linhas.GetEdita(Documento_DESTINO_COMPRA.Linhas.NumItens).Descricao = Documento_ORIGEM_VENDA.Linhas.GetEdita(i).Descricao;
                }
                else
                {
                    MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível apurar o tipo da linha nº " + Conversion.Str(i) + Strings.Chr(13) + Strings.Chr(13) + " do documento de origem." + Strings.Chr(13) + Strings.Chr(13) + "NÃO FOI EFECTUADA A CRIAÇÃO DO DOCUMENTO NA BASE DE DADOS DE DESTINO " + Empresa_Destino + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // ----------------------------------------------------------------------------
            // --- 5. VALIDA A GRAVAÇÃO E GRAVA O DOCUMENTO NA BASE DE DADOS DE DESTINO ---
            // ----------------------------------------------------------------------------


            // Validacao da gravacao
            string Desc_Erro = "";
            CmpBETabCompra AuxTabCompra;
            AuxTabCompra = Motor_Primavera_BD_Destino.Compras.TabCompras.Edita(Documento_DESTINO_COMPRA.Tipodoc);


            // Verififica se está tudo OK para gravar o documento na BD Destino
            if (Motor_Primavera_BD_Destino.Compras.Documentos.ValidaActualizacao(Documento_DESTINO_COMPRA, AuxTabCompra, ref Desc_Erro) == true)
            {
                // Abre transacao para a gravação em ambas as bases de dados
                Motor_Primavera_BD_Origem.IniciaTransaccao();
                Motor_Primavera_BD_Destino.IniciaTransaccao();
                try
                {
                    // GRAVA o documento na BD de destino
                    Motor_Primavera_BD_Destino.Compras.Documentos.Actualiza(Documento_DESTINO_COMPRA);
                    // Guarda no documento de origem, a RASTREABILIDADE PARA O DOCUMENTO DE DESTINO (no documento de origem  -  campo CDU_DocumentoCompraDestino)  ---
                    Documento_ORIGEM_VENDA.CamposUtil["CDU_DocumentoCompraDestino"].Valor = Strings.Trim(Documento_DESTINO_COMPRA.Tipodoc) + " " + Strings.Trim(Documento_DESTINO_COMPRA.Serie) + "/" + Strings.Trim(Documento_DESTINO_COMPRA.NumDoc.ToString());
                    Motor_Primavera_BD_Origem.Vendas.Documentos.Actualiza(Documento_ORIGEM_VENDA); // P/ Gravar o campo da rastreabilidade doc ORIGEM
                }

                // '------------------------------------------------------------------------------------------------------------------------
                // '--- Se o documento de origem for do tipo "EMB" e o documento de destino (criado na bd destino) for do tipo "ECF"     ---
                // '--- cria a rastreabilidade POR CÓPIA DE LINHAS (entre o documento "ECF" e o documento "CNT" respectivo na bd destino ---                                   ----
                // '------------------------------------------------------------------------------------------------------------------------ 


                // 'Só faz se o documento de venda na bd origem for "EMB" e o documento de compra na criado na bd destino for do tipo "ECF" 
                // If Trim(Documento_ORIGEM_VENDA.Tipodoc) = "EMB" And Trim(Documento_DESTINO_COMPRA.Tipodoc) = "ECF" Then



                // 'E se gerar documento de compra na base de dados de destino.
                // If Len(Trim(Documento_ORIGEM_VENDA.CamposUtil("CDU_DocumentoCompraDestino"].Valor.ToString())) > 0 Then


                // 'Edita documento de compra já existente na bd destino (só para atualizar as linhas com a rasteabilidade) - 
                // '(pq so consegue o obter o id correcto de cada linha após a 1ª gravacao na bd destino)
                // Documento_DESTINO_COMPRA = Motor_Primavera_BD_Destino.Comercial.Compras.Edita(Documento_DESTINO_COMPRA.Filial, Documento_DESTINO_COMPRA.Tipodoc, Documento_DESTINO_COMPRA.Serie, Documento_DESTINO_COMPRA.NumDoc)


                // 'Percorre todas as linhas do documento destino "ECF" atual que está em memória
                // For Nr_Linha = 1 To Documento_DESTINO_COMPRA.Linhas.NumItens

                // 'Só faz registo da reastreabilidade por linha para todaas linhas não comentário (TipoLinha <> 60)
                // If (Documento_DESTINO_COMPRA.Linhas.GetEdita(Nr_Linha).TipoLinha <> 60) Then

                // Documento_DESTINO_COMPRA.Linhas.GetEdita(Nr_Linha).ModuloOrigemCopia = "C"  'Rastreabilidade Por Cópia de Linhas
                // 'Documento_DESTINO_COMPRA.Linhas.GetEdita(Nr_Linha).IdLinhaOrigemCopia = Obtem_Id_Linha_Origem_no_Documento_CNT_da_BD_Destino(Motor_Primavera_BD_Destino, Documento_DESTINO_COMPRA.Linhas.GetEdita(Nr_Linha).IdLinha)
                // Documento_DESTINO_COMPRA.Linhas.GetEdita(Nr_Linha).IdLinhaOrigemCopia = Obtem_Id_Linha_Origem_no_Documento_CNT_da_BD_Destino(Motor_Primavera_BD_Destino, Documento_DESTINO_COMPRA.Linhas.GetEdita(Nr_Linha).CamposUtil("CDU_IDLinhaOriginalGrupo"].Valor)

                // 'Confirma se conseguiu obter a rastreabilidade da linha do documento "ECF" com a linha do documento "CNT" respectivo na bd destino, se não conseguiu, aborta a criação do documento "ECF" na bd destino
                // If Trim(Documento_DESTINO_COMPRA.Linhas(Nr_Linha).IdLinhaOrigemCopia) = "" Then

                // MsgBox("AVISO : " & Chr(13) & Chr(13) & "Não foi possível estabelecer a rastreabilidade por linha entre os documentos na base de dados de destino " & Chr(13) & "entre a linha nº " & Str(Nr_Linha) & " deste documento 'ECF' e a linha respectiva no documento 'CNT' a ele ligado." & Chr(13) & Chr(13) & "NÃO FOI EFECTUADA A CRIAÇÃO/ATUALIZAÇÃO DO DOCUMENTO NA BASE DE DADOS DE DESTINO", vbCritical)

                // 'Se deu erro, faz o rollback da transaccao
                // Motor_Primavera_BD_Origem.DesfazTransaccao()
                // Motor_Primavera_BD_Destino.DesfazTransaccao()

                // Exit Function

                // End If

                // End If

                // Next Nr_Linha

                // 'Volta a GRAVAR o documento na BD de destino (só para atualizar as linhas com a rasteabilidade)
                // Motor_Primavera_BD_Destino.Comercial.Compras.Actualiza(Documento_DESTINO_COMPRA)

                // End If

                // End If

                // '-------------------------------------------------------------------------------------------------
                catch (Exception ex)
                {

                    // Se deu erro, faz o rollback da transaccao
                    Motor_Primavera_BD_Origem.DesfazTransaccao();
                    Motor_Primavera_BD_Destino.DesfazTransaccao();

                    MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "Ocorreu um erro do motor Primavera no exato momento de gravar o documento na base de dados de destino " + Empresa_Destino + "!!!!." + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível ao mecanismo de cópia detectar a causa exata do erro retornado pelo motor primavera!" + Strings.Chr(13) + Strings.Chr(13) + "Provavelmente o erro deve-se a um campo CDU obrigatório não preenchido ou incorretamente preenchido no documento de origem!" + Strings.Chr(13) + Strings.Chr(13) + "P.f. verifique se o registo de origem está corretamente preenchido " + Strings.Chr(13) + "e se os valores dos campos de utilizador tipo 'lista' indicados " + Strings.Chr(13) + "existem nas respetivas tabelas na base de dados de destino. " + Strings.Chr(13) + Strings.Chr(13) + "NÃO FOI EFECTUADA A CRIAÇÃO/ATUALIZAÇÃO DO DOCUMENTO NA BASE DE DADOS DE DESTINO " + Empresa_Destino + "!" + Strings.Chr(13) + Strings.Chr(13) + "Erro : " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // -------------------------------------------------------------------------------------- 
                // ---- 6. FAZ A CÓPIA DOS ANEXOS DO DOCUMENTO DE ORIGEM PARA O DOCUMENTO DE DESTINO ----
                // --------------------------------------------------------------------------------------
                // (Só faz aqui a cópia dos anexos, após garantir que o documento de destino conseguiu ser gravado com sucesso na base de dados de destino)
                try
                {
                    // Acha o campo chave dos anexos do documento de origem na tabela Anexos da base de dados de origem
                    string Chave_Anexos_Documento_ORIGEM_VENDA;
                    string Chave_Anexos_Documento_DESTINO_COMPRA;


                    string Sql_Copia;
                    string Sql_Destino;
                    string Diretorio_Anexos;
                    StdBELista ListaQuery = new StdBELista();



                    string Nome_Original_Ficheiro_Documento_Compra_Origem;
                    string Data_Hora_Ficheiro_Documento_Compra_Origem;
                    string Guid_Ficheiro_Documento_Compra_Origem;
                    string Nome_Fisico_Ficheiro_Documento_Compra_Origem;

                    string Extensao_Ficheiro;
                    Guid Novo_Guid;

                    string Nome_Original_Ficheiro_Documento_Venda_Destino;
                    string Guid_Ficheiro_Documento_Venda_Destino;
                    string Nome_Fisico_Ficheiro_Documento_Venda_Destino;


                    Chave_Anexos_Documento_ORIGEM_VENDA = Strings.Trim(Documento_ORIGEM_VENDA.Tipodoc) + Strings.Trim(Documento_ORIGEM_VENDA.NumDoc.ToString()) + Strings.Trim(Documento_ORIGEM_VENDA.Serie) + Strings.Trim(Documento_ORIGEM_VENDA.Filial);
                    Chave_Anexos_Documento_DESTINO_COMPRA = Strings.Trim(Documento_DESTINO_COMPRA.Tipodoc) + Strings.Trim(Documento_DESTINO_COMPRA.NumDoc.ToString()) + Strings.Trim(Documento_DESTINO_COMPRA.Serie) + Strings.Trim(Documento_DESTINO_COMPRA.Filial);


                    // Identifica todos os ficheiros anexos do documento de origem
                    if (Strings.Trim(Chave_Anexos_Documento_ORIGEM_VENDA) != "" & Strings.Trim(Chave_Anexos_Documento_DESTINO_COMPRA) != "")
                    {

                        // Apagar anexos na base de dados de destino para serem criados novamente
                        // Sql_Destino = "select * from PRI" & Trim(Empresa_Destino) & "..Anexos where Chave = '" & Trim(Chave_Anexos_Documento_DESTINO_COMPRA) & "' order by FicheiroOrig".ToString()
                        // Lista_Primavera = PriV100Api.BSO.Consulta(Sql_Destino)
                        // If Lista_Primavera.NumLinhas > 0 Then

                        // Sql_Copia = "delete PRI" & Trim(Empresa_Destino) & "..Anexos where  chave = '" & Trim(Chave_Anexos_Documento_DESTINO_COMPRA) & "' "
                        // Motor_Primavera_BD_Destino.DSO.BDAPL.Execute(Sql_Copia)

                        // End If



                        Sql_Query = "select * from Anexos where Chave = '" + Strings.Trim(Chave_Anexos_Documento_ORIGEM_VENDA) + "' order by FicheiroOrig";

                        Lista_Primavera = PriV100Api.BSO.Consulta(Sql_Query);

                        // Verifica se o query retornou linhas (se o documento de origem tem documentos anexos)
                        if (Lista_Primavera.NumLinhas() > 0)
                        {
                            Lista_Primavera.Inicio();

                            // Percorre todos os documentos anexos
                            for (var Nr_Ficheiro_Anexo = 1; Nr_Ficheiro_Anexo <= Lista_Primavera.NumLinhas(); Nr_Ficheiro_Anexo++)
                            {

                                // Limpa as variaveis a cada iteracao do ciclo
                                Nome_Original_Ficheiro_Documento_Compra_Origem = "";
                                Guid_Ficheiro_Documento_Compra_Origem = "";
                                Nome_Fisico_Ficheiro_Documento_Compra_Origem = "";

                                Extensao_Ficheiro = "";

                                Nome_Original_Ficheiro_Documento_Venda_Destino = "";
                                Guid_Ficheiro_Documento_Venda_Destino = "";
                                Nome_Fisico_Ficheiro_Documento_Venda_Destino = "";



                                // Vê qual o nome original do ficheiro anexo e qual o seu guid no Primavera
                                Guid_Ficheiro_Documento_Compra_Origem = Strings.Trim(Lista_Primavera.Valor("id").ToString());
                                Nome_Original_Ficheiro_Documento_Compra_Origem = Strings.Trim(Lista_Primavera.Valor("FicheiroOrig").ToString());
                                Data_Hora_Ficheiro_Documento_Compra_Origem = Strings.Trim(Lista_Primavera.Valor("Data").ToString());


                                // Acha qual a extensão do ficheiro
                                Extensao_Ficheiro = System.IO.Path.GetExtension(Nome_Original_Ficheiro_Documento_Compra_Origem);
                                Nome_Fisico_Ficheiro_Documento_Compra_Origem = Strings.Trim(Guid_Ficheiro_Documento_Compra_Origem) + Strings.Trim(Extensao_Ficheiro);



                                // Cria um novo Guid (para o novo ficheiro na base de dados de destino)
                                // Novo_Guid = Guid.NewGuid()
                                // Guid_Ficheiro_Documento_Venda_Destino = Novo_Guid.ToString()


                                Guid_Ficheiro_Documento_Venda_Destino = Guid_Ficheiro_Documento_Compra_Origem;
                                Nome_Fisico_Ficheiro_Documento_Venda_Destino = "{" + Strings.Trim(Guid_Ficheiro_Documento_Venda_Destino) + "}" + Strings.Trim(Extensao_Ficheiro);
                                Nome_Original_Ficheiro_Documento_Venda_Destino = Strings.Trim(Nome_Original_Ficheiro_Documento_Compra_Origem);


                                // Copia fisicamente o ficheiro original para um com o guid (usado na base de dados de destino)


                                // Copia para o diretorio de anexos do Primavera 
                                // Diretorio_Anexos = PriV100Api.BSO.DSO.Plat.RegistryPrimavera.DaPercursoDados("GCP", "DEFAULT") & "\ANEXOS"
                                // System.IO.File.Copy(Trim(Diretorio_Anexos) & "\" & Trim(Nome_Fisico_Ficheiro_Documento_Compra_Origem), Trim(Diretorio_Anexos) & "\" & Trim(Nome_Fisico_Ficheiro_Documento_Venda_Destino))


                                // Apaga no documento de compra da base de dados de destino os anexos que já existem (para garantir que se forem actualizados na origem são também actualizados na bd destino)
                                // Sql_Copia = "delete PRI" & Trim(Empresa_Destino) & "..Anexos where  chave = '" & Trim(Chave_Anexos_Documento_DESTINO_COMPRA) & "' and FicheiroOrig = '" & Trim(Nome_Original_Ficheiro_Documento_Compra_Origem) & "' and data = convert(datetime,'" & Trim(Data_Hora_Ficheiro_Documento_Compra_Origem) & "',103)"
                                // Motor_Primavera_BD_Destino.DSO.BDAPL.Execute(Sql_Copia)


                                // Cria na base de dados de destino o registo do novo ficheiro copiado
                                Sql_Destino = "select * from PRI" + Strings.Trim(Empresa_Destino) + "..Anexos where id = '" + Guid_Ficheiro_Documento_Venda_Destino + "'";
                                ListaQuery = PriV100Api.BSO.Consulta(Sql_Destino);

                                if (ListaQuery.NumLinhas() == 0)
                                {
                                    Sql_Copia = "insert into PRI" + Strings.Trim(Empresa_Destino) + "..anexos select '" + Guid_Ficheiro_Documento_Venda_Destino + "', 40,'" + Strings.Trim(Chave_Anexos_Documento_DESTINO_COMPRA) + "',FicheiroOrig, Descricao, Data, Utilizador, Tipo, Idioma, Web, ArvoreAnexosID, FicheiroWeb, ExportarTTE, Encriptado from PRI" + Strings.Trim(Empresa_Origem) + "..Anexos where chave = '" + Strings.Trim(Chave_Anexos_Documento_ORIGEM_VENDA) + "' and FicheiroOrig = '" + Strings.Trim(Nome_Original_Ficheiro_Documento_Compra_Origem) + "' and data = convert(datetime,'" + Strings.Trim(Data_Hora_Ficheiro_Documento_Compra_Origem) + "',103)";
                                    Motor_Primavera_BD_Destino.DSO.ExecuteSQL(Sql_Copia);
                                }
                                else
                                {
                                    Sql_Copia = "update ad set ad.Descricao = a.Descricao , ad.tipo = a.Tipo, ad.Idioma = a.Idioma, ad.Web=a.Web, ad.ArvoreAnexosID=a.ArvoreAnexosID , ad.FicheiroWeb = a.FicheiroWeb, ad.ExportarTTE = a.ExportarTTE, ad.Encriptado = a.Encriptado from PRI" + Strings.Trim(Empresa_Destino) + "..anexos as ad inner join PRI" + Strings.Trim(Empresa_Origem) + "..Anexos as a on a.id = ad.id where ad.id = '" + Guid_Ficheiro_Documento_Venda_Destino + "' ";
                                    Motor_Primavera_BD_Destino.DSO.ExecuteSQL(Sql_Copia);
                                }


                                Lista_Primavera.Seguinte();
                            }
                        }

                        Lista_Primavera.Vazia();
                    }
                }
                catch (Exception ex)
                {

                    // Se deu erro, faz o rollback da transaccao
                    Motor_Primavera_BD_Origem.DesfazTransaccao();
                    Motor_Primavera_BD_Destino.DesfazTransaccao();

                    MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "Ocorreu um erro ao gravar os anexos do documento na base de dados de destino " + Empresa_Destino + "." + Strings.Chr(13) + Strings.Chr(13) + "NÃO FOI EFECTUADA A CRIAÇÃO/ATUALIZAÇÃO DO DOCUMENTO NA BASE DE DADOS DE DESTINO " + Empresa_Destino + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }



                // Faz o COMMIT DA TRANSACCAO em ambas as bases de dados
                Motor_Primavera_BD_Destino.TerminaTransaccao();
                Motor_Primavera_BD_Origem.TerminaTransaccao();
            }
            else
            {
                MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "O mecanismo de validação de documentos detectou erros na validação do documento de destino na base de dados de destino " + Empresa_Destino + "." + Strings.Chr(13) + "Não foi possivel validar a gravação do documento na base de dados de destino." + Strings.Chr(13) + Strings.Chr(13) + "Mensagem do Motor Primavera : " + Strings.Chr(13) + Desc_Erro + Strings.Chr(13) + Strings.Chr(13) + "NÃO FOI EFECTUADA A CRIAÇÃO/ATUALIZAÇÃO DO DOCUMENTO NA BASE DE DADOS DE DESTINO " + Empresa_Destino + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            MessageBox.Show("CÓPIA DE DOCUMENTO EFECTUADA COM SUCESSO ENTRE BASES DE DADOS." + Strings.Chr(13) + Strings.Chr(13) + "Documento de origem (" + Strings.UCase(Empresa_Origem) + "/DOC VENDA/" + Documento_ORIGEM_VENDA.Tipodoc + "/" + Documento_ORIGEM_VENDA.Serie.ToString() + "/" + Documento_ORIGEM_VENDA.NumDoc.ToString() + ") " + Strings.Chr(13) + Strings.Chr(13) + "Documento de destino (" + Empresa_Destino + "/DOC COMPRA/" + Documento_DESTINO_COMPRA.Tipodoc + "/" + Documento_DESTINO_COMPRA.Serie.ToString() + "/" + Documento_DESTINO_COMPRA.NumDoc.ToString() + ")", MsgBoxStyle.Information.ToString(), MessageBoxButtons.OK);
            // Se tudo correu tudo bem bem, a função retorna TRUE
            //Copiar_BD_Origem_DocVenda_PARA_BD_Destino_DocCompra = true;
            return true;
        }

        #endregion
        public bool Verifica_Se_Fornecedor_Empresa_Grupo_Mundifios(VndBEDocumentoVenda Documento_Venda_Atual)
        {

            // Verifica se o fornecedor indicado no documento de venda actual é um fornecedor do Grupo Mundifios

            string CDU_EntidadeInterna_Fornecedor_BD_Origem;
            string CDU_NomeEmpresaGrupo_Fornecedor_BD_Origem;



            // Acha a na tabela fornecedor o nome da base de dados e a Entidade_Interna
            CDU_EntidadeInterna_Fornecedor_BD_Origem = Strings.Trim(PriV100Api.BSO.Base.Fornecedores.DaValorAtributo(Documento_Venda_Atual.CamposUtil["CDU_Fornecedor"].Valor.ToString(), "CDU_EntidadeInterna"));
            CDU_NomeEmpresaGrupo_Fornecedor_BD_Origem = Strings.Trim(PriV100Api.BSO.Base.Fornecedores.DaValorAtributo(Documento_Venda_Atual.CamposUtil["CDU_Fornecedor"].Valor.ToString(), "CDU_NomeEmpresaGrupo"));

            if (Strings.Len(Strings.Trim(CDU_NomeEmpresaGrupo_Fornecedor_BD_Origem)) > 0)
            {
                // Se conseguiu obter o CDU_EntidadeInterna_Fornecedor_BD_Origem  e o CDU_NomeEmpresaGrupo_Fornecedor_BD_Origem, é fornecedor do Grupo Mundifios
                if (Strings.Len(Strings.Trim(CDU_EntidadeInterna_Fornecedor_BD_Origem)) > 0)
                    return true;
                else
                    MessageBox.Show("Não foi possível apurar os valores CDU_EntidadeInterna do fornecedor com cdu_fornecedor = "
                        + Documento_Venda_Atual.CamposUtil["CDU_Fornecedor"].Valor.ToString()
                        + Strings.Chr(13) + Strings.Chr(13) + "O DOCUMENTO NÃO SERÁ GRAVADO PARA A BASE DE DADOS DE DESTINO!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
        public bool VerficaDocumentoTransformado(VndBEDocumentoVenda Documento_Venda_EMB_Atual)
        {
            string SQL_Query;
            StdBELista Lista_Primavera = new StdBELista();


            SQL_Query = "select QuantTrans from linhasdoctrans where idlinhasdocorigem in (select id from linhasdoc where IdCabecDoc in (select id from cabecdoc where tipodoc = '"
                + Strings.Trim(Documento_Venda_EMB_Atual.Tipodoc)
                + "' and numdoc = '"
                + Strings.Trim(Documento_Venda_EMB_Atual.NumDoc.ToString())
                + "' and serie = '" + Strings.Trim(Documento_Venda_EMB_Atual.Serie) + "'))";

            Lista_Primavera = PriV100Api.BSO.Consulta(SQL_Query);

            if (!(Lista_Primavera.NumLinhas() == 0))
                return true;

            Lista_Primavera.Vazia();
            return false;
        }

        public bool Verifica_se_CNT_que_deu_origem_EMB_ja_foi_exportado_para_outra_BD(VndBEDocumentoVenda Documento_Venda_EMB_Atual)
        {
            // -------------------------------------------------------
            // --- 2019.05.30 - VIMAPONTO - Gualter Costa - #1558  ---
            // -------------------------------------------------------
            // Para o documento de venda EMB atual, verifica se o CNT que lhe deu origem (por transformação) já foi exportado para a base de dados de destino. 

            string SQL_Query;
            StdBELista Lista_Primavera = new StdBELista();

            // Verifica quais os documento de venda CNT que deram origem ao documento de venda EMB (por transformação de documentos)
            // e se todos esses documentos de compra CNT já foram exportados para outras bases de dados do Grupo Mundifios (se todos esses documentos CNT já têm preenchido o campo CDU_DocumentoCompraDestino)

            SQL_Query = "select CDU_DocumentoCompraDestino from CabecDoc where id in ("
                + "select IdCabecDoc  from LinhasDoc where id in ("
                + "select IdLinhasDocOrigem   from LinhasDocTrans  where IdLinhasDoc in (select id from LinhasDoc where IdCabecDoc in (select id from CabecDoc where tipodoc = '"
                + Strings.Trim(Documento_Venda_EMB_Atual.Tipodoc) + "' and serie = '"
                + Strings.Trim(Documento_Venda_EMB_Atual.Serie) + "' and numdoc = '"
                + Strings.Trim(Documento_Venda_EMB_Atual.NumDoc.ToString()) + "')) "
                + ")" + ") and CDU_DocumentoCompraDestino is NULL";

            Lista_Primavera = PriV100Api.BSO.Consulta(SQL_Query);

            // Se já não existirem documentos de venda CNT com o campo CDU_DocumentoCompraDestino a NULL (dos que deram origem a ao presente documento de venda "EMB")
            if (Lista_Primavera.NumLinhas() == 0)
                return true;// Retorna TRUE para deixar gravar

            Lista_Primavera.Vazia();

            return false;
        }

        private string Obtem_Id_Linha_Origem_no_Documento_CNT_da_BD_Destino(ErpBS Motor_Primavera_BD_Destino, string Id_Linha_Documento_ECF)
        {


            // ---------------------------------------------------------------------------
            // ---  'VIMAPONTO - GMC 2019.05.31 (ver especificação no redmine #1558)   ---
            // ---------------------------------------------------------------------------

            // Função para obter o Id_Linha_Origem_no_Documento_CNT_BD_Destino a partir de um Id_Linha_do_Documento_ECF_BD_Destino
            // Para mais informações ver no redmine # 1558 o ficheiro "Esquema_de_Obtencao_do_Id_Linha_Origem_no_Documento_CNT_a_partir_de_uma_Linha_do_Documento_ECF.JPG"


            string Sql_Query;
            StdBELista Lista_Primavera;


            string Obtem_Id_Linha_Origem_no_Documento_CNT_da_BD_Destino = ""; // Inicializa a vazio

            // Retira as chavetas do ID
            Id_Linha_Documento_ECF = Strings.Replace(Id_Linha_Documento_ECF, "{", "");
            Id_Linha_Documento_ECF = Strings.Replace(Id_Linha_Documento_ECF, "}", "");

            // Query para obter o Id_Linha_Origem_no_Documento_CNT de uma linha do documento ECF
            // Mais informações sobre como funciona este query, ver no redmine # 1558 o ficheiro "Esquema_de_Obtencao_do_Id_Linha_Origem_no_Documento_CNT_a_partir_de_uma_Linha_do_Documento_ECF.JPG"

            // Sql_Query = "select Rastreabilidade_Doc_CNT_Origem.Id as Id_Linha_No_Documento_CNT_Origem, LinhasCompras.Id as Id_Linha_No_Documento_ECF_Destino, LinhasCompras.Quantidade " & _
            // "from LinhasCompras INNER JOIN ( " & _
            // "select Id, Artigo , Quantidade from linhascompras where IdCabecCompras in ( " & _
            // "select id from CabecCompras where (TipoDoc + ' ' + serie +'/'+ ltrim(rtrim(str(NumDoc))))  in ( " & _
            // "select CDU_DocumentoCompraDestino from PRIFILOPA..CabecDoc where Id in ( " & _
            // "select IdCabecDoc  from PRIFILOPA..LinhasDoc where Id in ( " & _
            // "select IdLinhasDocOrigem  from PRIFILOPA..LinhasDocTrans where IdLinhasDoc in ( " & _
            // "select id from PRIFILOPA..LinhasDoc where IdCabecDoc   in ( " & _
            // "select id  from PRIFILOPA..CabecDoc where (TipoDoc + ' ' + serie +'/'+ ltrim(rtrim(str(NumDoc))))  in ( " & _
            // "select CDU_DocumentoOrigem  from CabecCompras where id in ( " & _
            // "select IdCabecCompras  from LinhasCompras where id = '" & Id_Linha_Documento_ECF & "' " & _
            // ") " & _
            // ") " & _
            // ") and PRIFILOPA..LinhasDoc.Artigo is not null " & _
            // ") " & _
            // ") " & _
            // ") " & _
            // ") " & _
            // ") and LinhasCompras.Artigo is not null " & _
            // ") as Rastreabilidade_Doc_CNT_Origem " & _
            // "ON Rastreabilidade_Doc_CNT_Origem.Artigo = LinhasCompras.Artigo " & _
            // "where LinhasCompras.id = '" & Id_Linha_Documento_ECF & "' "

            Sql_Query = "SELECT "
                + "LC_CNT.Id AS Id_Linha_No_Documento_CNT_Origem "
                + "FROM " + "PRIFILOPA.dbo.LinhasDoc LD "
                + "INNER JOIN PRIFILOPA.dbo.LinhasDocTrans LDT ON LDT.IdLinhasDoc = LD.Id "
                + "INNER JOIN PRIFILOPA.dbo.LinhasDoc LD_CNT ON LD_CNT.Id = LDT.IdLinhasDocOrigem "
                + "INNER JOIN LinhasCompras LC_CNT ON LC_CNT.CDU_IDLinhaOriginalGrupo = '" + Id_Linha_Documento_ECF + "' "
                + "WHERE "
                + "LD.Id = '" + Id_Linha_Documento_ECF + "'";


            // Carrega a lista com o resultado do query
            Lista_Primavera = Motor_Primavera_BD_Destino.Consulta(Sql_Query);

            if (Lista_Primavera.Vazia() == false)
            {
                Lista_Primavera.Inicio();

                // So deve retornar 1 registo
                for (var i = 1; i <= Lista_Primavera.NumLinhas(); i++)
                {
                    // Retorna o Id_Linha_No_Documento_CNT_Origem na base de dados de destino
                    Obtem_Id_Linha_Origem_no_Documento_CNT_da_BD_Destino = Convert.ToString(Lista_Primavera.Valor("Id_Linha_No_Documento_CNT_Origem"));

                    // Retira as chavetas do ID
                    Obtem_Id_Linha_Origem_no_Documento_CNT_da_BD_Destino = Strings.Replace(Obtem_Id_Linha_Origem_no_Documento_CNT_da_BD_Destino, "{", "");
                    Obtem_Id_Linha_Origem_no_Documento_CNT_da_BD_Destino = Strings.Replace(Obtem_Id_Linha_Origem_no_Documento_CNT_da_BD_Destino, "}", "");

                    Lista_Primavera.Seguinte();
                }
            }
            return Obtem_Id_Linha_Origem_no_Documento_CNT_da_BD_Destino;
        }

        private bool DaEstadoLinha(ErpBS Motor_Primavera_BD_Destino, string Id_Linha_Documento_ECF)
        {


            // ---------------------------------------------------------------------------
            // ---  'VIMAPONTO - GMC 2019.05.31 (ver especificação no redmine #1558)   ---
            // ---------------------------------------------------------------------------

            // Função para obter o Id_Linha_Origem_no_Documento_CNT_BD_Destino a partir de um Id_Linha_do_Documento_ECF_BD_Destino
            // Para mais informações ver no redmine # 1558 o ficheiro "Esquema_de_Obtencao_do_Id_Linha_Origem_no_Documento_CNT_a_partir_de_uma_Linha_do_Documento_ECF.JPG"


            string Sql_Query;
            StdBELista Lista_Primavera;
            bool DaEstadoLinha;

            DaEstadoLinha = false; // Inicializa a false

            // Query para determinar se a linha se encontra fechada
            Sql_Query = "SELECT LDS_CNT.Fechado "
                + "FROM "
                + "PRIFILOPA.dbo.LinhasDoc LD "
                + "INNER JOIN PRIFILOPA.dbo.LinhasDocTrans LDT ON LDT.IdLinhasDoc = LD.Id "
                + "INNER JOIN PRIFILOPA.dbo.LinhasDoc LD_CNT ON LD_CNT.Id = LDT.IdLinhasDocOrigem "
                + "INNER JOIN PRIFILOPA.dbo.LinhasDocStatus LDS_CNT ON LDS_CNT.IdLinhasDoc = LD_CNT.Id "
                + "INNER JOIN PRIFILOPA.dbo.CabecDoc CD_CNT ON CD_CNT.Id = LD_CNT.IdCabecDoc "
                + "WHERE " + "LD.Id = '" + Id_Linha_Documento_ECF + "'";

            // Carrega a lista com o resultado do query
            Lista_Primavera = Motor_Primavera_BD_Destino.Consulta(Sql_Query);

            if (Lista_Primavera.Vazia() == false)
            {
                Lista_Primavera.Inicio();

                // So deve retornar 1 registo
                for (var i = 1; i <= Lista_Primavera.NumLinhas(); i++)
                {
                    // Retorna o estado da linha
                    DaEstadoLinha = Lista_Primavera.Valor("Fechado");

                    Lista_Primavera.Seguinte();
                }
            }
            return DaEstadoLinha;
        }

        public int Verifica_Se_Documento_Compra_Criado_na_BD_Destino_NAO_Sofreu_Transformacao_ou_Copia_Linhas(VndBEDocumentoVenda Documento_Venda_Atual)
        {

            // -------------------------------------------------------
            // --- 2019.06.04 - VIMAPONTO - Gualter Costa - #1558  ---
            // -------------------------------------------------------
            // Verifica se os documentos de compra (CNT ou ECF)  gerados na bases  de dados de destino por cópia automática a partir deste documento de venda FILOPA
            // sofreram qualquer alteração (por transformação de documentos ou por cópia de linhas). 
            // Se os documentos de compra nas bases de dados de destino sofreram alterações (por transformação de documentos ou por cópia de linhas) retorna false para não deixar gravar em modo de edição o documento de venda atual

            // Retorno (-1 erro, 0 = False, 1 = True)

            string SQL_Query;
            StdBELista Lista_Primavera = new StdBELista();

            BasBEFornecedor Ficha_Fornecedor_BD_Origem = new BasBEFornecedor();

            ErpBS Motor_Primavera_BD_Destino = new ErpBS();
            CmpBEDocumentoCompra Documento_DESTINO_COMPRA = new CmpBEDocumentoCompra();

            // -------------------------------------------------------------------------------------------------------
            // --- Verifica o documento de compra na base de dados de destino que o documento de venda atual criou ---
            // -------------------------------------------------------------------------------------------------------

            // Acha qual a base de dados de destino e qual o documento de compra (noutra base de dados da do Grupo Mundifios), que teve origem a partir do presente documento de venda deu origem

            string Empresa_Documento_Compra_Destino;

            string Filial_Documento_Compra_Destino;
            string TipoDoc_Documento_Compra_Destino;
            string Serie_Documento_Compra_Destino;
            int NumDoc_Documento_Compra_Destino;

            string Campo_Documento_Compra_Destino;
            int Aux_Posicao_Espaco;
            int Aux_Posicao_Barra;


            try
            {
                // Acha qual a empresa de destino (com base no campo CDU_Fornecedor da ficha de forncedor da base de dados de origem)
                Empresa_Documento_Compra_Destino = (PriV100Api.BSO.Base.Fornecedores.DaValorAtributo(Strings.Trim(Documento_Venda_Atual.CamposUtil["CDU_Fornecedor"].Valor.ToString()), "CDU_NomeEmpresaGrupo"));

                // Acha os dados do documento de destino, com base no campo CDU_DocumentoCompraDestino do documento de origem
                Campo_Documento_Compra_Destino = Strings.Trim(Documento_Venda_Atual.CamposUtil["CDU_DocumentoCompraDestino"].Valor.ToString());
                Aux_Posicao_Espaco = Campo_Documento_Compra_Destino.IndexOf(" ") + 1;
                Aux_Posicao_Barra = Campo_Documento_Compra_Destino.IndexOf("/") + 1;

                Filial_Documento_Compra_Destino = "000";
                TipoDoc_Documento_Compra_Destino = Strings.Trim(Strings.Mid(Campo_Documento_Compra_Destino, 1, Aux_Posicao_Espaco));
                Serie_Documento_Compra_Destino = Strings.Trim(Strings.Mid(Campo_Documento_Compra_Destino,Aux_Posicao_Espaco, Aux_Posicao_Barra - Aux_Posicao_Espaco));
                NumDoc_Documento_Compra_Destino = (int)Conversion.Val(Strings.Trim(Strings.Mid(Campo_Documento_Compra_Destino, Aux_Posicao_Barra + 1, Strings.Len(Campo_Documento_Compra_Destino))));

                // Verifica se conseguiu obter a base de dados de destino e o tipodoc/serie/número do documento de compra gerado automáticamente por este documento de venda na base de dados de destino
                if (Strings.Trim(Empresa_Documento_Compra_Destino) == "" | Strings.Trim(Filial_Documento_Compra_Destino) == "" | Strings.Trim(TipoDoc_Documento_Compra_Destino) == "" | Strings.Trim(Serie_Documento_Compra_Destino) == "" | Strings.Trim(NumDoc_Documento_Compra_Destino.ToString()) == "")
                {
                    MessageBox.Show("Verifica_Se_Documento_Compra_Criado_na_BD_Destino_NAO_Sofreu_Transformacao_ou_Copia_Linhas()" + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível detectar qual a base de dados ou tipodoc/serie/numero do documento de compra gerado automáticamente por este documento de venda.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Verifica_Se_Documento_Compra_Criado_na_BD_Destino_NAO_Sofreu_Transformacao_ou_Copia_Linhas()" + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível detectar qual a base de dados ou qual o número do documento de compra gerado automáticamente por este documento de venda." + Strings.Chr(13) + Strings.Chr(13), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            // Carrega para memória o documento de compra (da base de dados de destino)

            // Abre os Motores Primavera (para a empresa de Destino)
            try
            {
                Motor_Primavera_BD_Destino.AbreEmpresaTrabalho(PriV100Api.BSO.Contexto.TipoPlataforma, Empresa_Documento_Compra_Destino, PriV100Api.BSO.Contexto.UtilizadorActual, PriV100Api.BSO.Contexto.PasswordUtilizadorActual);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifica_Se_Documento_Compra_Criado_na_BD_Destino_NAO_Sofreu_Transformacao_ou_Copia_Linhas()" + Strings.Chr(13) + Strings.Chr(13) + "Erro ao abrir o Motor Primavera Empresa Destino : " + Empresa_Documento_Compra_Destino, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;

            }

            // Incializa o documento de compra 
            Documento_DESTINO_COMPRA = new CmpBEDocumentoCompra();

            // Carrega o objecto do documento de compra (com o documento de compra na base de dados de destino)
            try
            {
                Documento_DESTINO_COMPRA = Motor_Primavera_BD_Destino.Compras.Documentos.Edita(Filial_Documento_Compra_Destino, TipoDoc_Documento_Compra_Destino, Serie_Documento_Compra_Destino, NumDoc_Documento_Compra_Destino);

                // Verifica se conseguiu carregar o documento de compra (na bd de destino)
                if (Strings.Trim(Documento_DESTINO_COMPRA.NumDoc.ToString()) == "")
                {
                    MessageBox.Show("Verifica_Se_Documento_Compra_Criado_na_BD_Destino_NAO_Sofreu_Transformacao_ou_Copia_Linhas()" + Strings.Chr(13) + Strings.Chr(13) + "Não foi possivel aceder aos dados do documento de compra " + TipoDoc_Documento_Compra_Destino + "/" + Serie_Documento_Compra_Destino + "/" + NumDoc_Documento_Compra_Destino + " na base de dados da Empresa Destino " + Empresa_Documento_Compra_Destino + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verifica_Se_Documento_Compra_Criado_na_BD_Destino_NAO_Sofreu_Transformacao_ou_Copia_Linhas()" + Strings.Chr(13) + Strings.Chr(13) + "Erro ao abrir o documento de compra " + TipoDoc_Documento_Compra_Destino + "/" + Serie_Documento_Compra_Destino + "/" + NumDoc_Documento_Compra_Destino + " na base de dados da Empresa Destino " + Empresa_Documento_Compra_Destino + "!" + Strings.Chr(13) + Strings.Chr(13), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;

            }

            // Verifica se alguma linha do documento de compra (na bd destino) já sofreu "Transformação de documentos" ou "cópia de linhas"

            try
            {

                // -------------------------------------------
                // --- Verifica se sofreu CÓPIA DE LINHAS  ---
                // -------------------------------------------

                // Verifica se alguma das linhas do documento de compra (na bd destino) já foi alvo de "Cópia de Linhas", ou seja se o id da linha existe no campo IdLinhaOrigemCopia de algum documento (se sim, indica que essa linha do documento já foi alvo de cópia de linhas para outro documento de compra)

                SQL_Query = "select IdLinhaOrigemCopia Nr from LinhasCompras where IdLinhaOrigemCopia in (select Id from LinhasCompras where IdCabecCompras = '" + Strings.Trim(Strings.Replace(Strings.Replace(Documento_DESTINO_COMPRA.ID, "{", ""), "}", "")) + "')";

                Lista_Primavera = Motor_Primavera_BD_Destino.Consulta(SQL_Query);

                // Verifica se o query retornou linhas
                if (Lista_Primavera.NumLinhas() > 0)
                {

                    // Se ja houve linhas do documento compra copiadas para outros documentos, retorna 0 para não deixar gravar
                    return 0;

                }

                Lista_Primavera.Vazia();
            }

            // '------------------------------------------------------
            // '--- Verifica se sofreu TRANSFORMACAO DE DOCUMENTOS ---
            // '------------------------------------------------------

            // 'Verifica se alguma das linhas do documento de compra (na bd destino) já foi alvo de "Transformação de Documentos"
            // SQL_Query = "select QuantTrans from linhascomprastrans where idlinhascomprasorigem in (select id from linhascompras where IdCabeccompras in (select id from cabeccompras where tipodoc = '" & Trim(Documento_DESTINO_COMPRA.Tipodoc) & "' and numdoc = '" & Trim(Documento_DESTINO_COMPRA.NumDoc) & "' and serie = '" & Trim(Documento_DESTINO_COMPRA.Serie) & "'))"

            // 'SQL_Query = "select IdLinhasCompras from LinhasComprasStatus where IdLinhasCompras in (select Id from LinhasCompras where IdCabecCompras = '" & Trim(Replace(Replace(Documento_DESTINO_COMPRA.ID, "{", ""), "}", "")) & "')  and QuantTrans > 0"

            // Lista_Primavera = Motor_Primavera_BD_Destino.Consulta(SQL_Query)

            // If Lista_Primavera.NumLinhas > 0 Then

            // 'Se ja houve linhas do documento compra transformadas para outros documentos, retorna 0 para não deixar gravar
            // Return 0
            // Exit Function

            // End If

            // Lista_Primavera.Vazia()



            catch (Exception ex)
            {
                MessageBox.Show("Verifica_Se_Documento_Compra_Criado_na_BD_Destino_NAO_Sofreu_Transformacao_ou_Copia_Linhas()" + Strings.Chr(13) + Strings.Chr(13) + "Não conseguiu aceder às linhas do documento de destino!" + Strings.Chr(13) + Strings.Chr(13), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1; // Retorna erro
            }

            // Se nenhuma linha foi copiada por cópia de linhas ou transformada,  retorna 1 
            return 1;
        }

        public bool Verifica_Se_TODOS_Codigos_Listas_Existem_na_BD_Destino(VndBEDocumentoVenda Documento_Venda_Atual)
        {
            // -------------------------------------------------------
            // --- 2019.06.11 - VIMAPONTO - Gualter Costa - #1558  ---
            // -------------------------------------------------------
            // VALIDA SE TODOS OS VALORES SELECCIONADOS NOS CAMPO TIPO LOOKUP/LISTA (EDITOR VENDAS PRIMAVERA + FORMULÁRIO F2) EXISTEM NA BASE DE DADOS DE DESTINO
            // (valida só os campos do tipo lista em que foi seleccionados valores

            int Nr_Linha;
            // --- VALIDA VALORES CAMPOS DO TIPO LOOKUP/LISTA DO CABEÇALHO DO DOCUMENTO ----
            // Fornecedor (Mais informações ver ESQUEMA COMO SE OBTEM O FORNECEDOR NA BD DESTINO.PDF no RedMine #1558)
            string CDU_EntidadeInterna_Fornecedor_BD_Origem;

            // Acha o código da entidade interna do fornecedor (comum nas várias bd's)
            CDU_EntidadeInterna_Fornecedor_BD_Origem = Strings.Trim(PriV100Api.BSO.Base.Fornecedores.DaValorAtributo(Documento_Venda_Atual.CamposUtil["CDU_Fornecedor"].Valor.ToString(), "CDU_EntidadeInterna"));

            // Entidade Interna 
            if (Verifica_Se_Codigo_Lista_Existe_na_BD_Destino(Documento_Venda_Atual, "Fornecedores", "CDU_EntidadeInterna", CDU_EntidadeInterna_Fornecedor_BD_Origem, "Cliente (Fornecedor)") == false)
            {
                return false;
            }
            // Modo Pagamento
            if (Strings.Trim(Documento_Venda_Atual.ModoPag.ToString()) != "")
            {
                if (Verifica_Se_Codigo_Lista_Existe_na_BD_Destino(Documento_Venda_Atual, "DocumentosBancos", "Movim", Documento_Venda_Atual.ModoPag.ToString(), "Modo de Pagamento") == false)
                {
                    return false;
                }
            }
            // Condições Pagamento
            if (Strings.Trim(Documento_Venda_Atual.CondPag.ToString()) != "")
            {
                if (Verifica_Se_Codigo_Lista_Existe_na_BD_Destino(Documento_Venda_Atual, "CondPag", "CondPag", Documento_Venda_Atual.CondPag.ToString(), "Codições de Pagamento") == false)
                {
                    return false;
                }
            }
            // Modo Expedição
            if (Strings.Trim(Documento_Venda_Atual.ModoExp.ToString()) != "")
            {
                if (Verifica_Se_Codigo_Lista_Existe_na_BD_Destino(Documento_Venda_Atual, "ModosExp", "ModoExp", Documento_Venda_Atual.ModoExp.ToString(), "Modo de Expedição") == false)
                {
                    return false;
                }
            }
            // Moeda
            if (Strings.Trim(Documento_Venda_Atual.Moeda.ToString()) != "")
            {
                if (Verifica_Se_Codigo_Lista_Existe_na_BD_Destino(Documento_Venda_Atual, "Moedas", "Moeda", Documento_Venda_Atual.Moeda.ToString(), "Moeda") == false)
                {
                    return false;
                }
            }

            // Incoterms
            if (Documento_Venda_Atual.CamposUtil["CDU_Incoterms"].Valor!= null && Strings.Trim(Documento_Venda_Atual.CamposUtil["CDU_Incoterms"].Valor.ToString()) != "")
            {
                if (Verifica_Se_Codigo_Lista_Existe_na_BD_Destino(Documento_Venda_Atual, "IntrastatCondEntrega", "CondEntrega", Documento_Venda_Atual.CamposUtil["CDU_Incoterms"].Valor.ToString(), "Incoterms") == false)
                {
                    return false;
                }
            }
            // Localidade
            if (Documento_Venda_Atual.CamposUtil["CDU_Localidade"].Valor != null && Strings.Trim(Documento_Venda_Atual.CamposUtil["CDU_Localidade"].Valor.ToString()) != "")
            {
                if (Verifica_Se_Codigo_Lista_Existe_na_BD_Destino(Documento_Venda_Atual, "TDU_LOCAIS", "CDU_Local", Documento_Venda_Atual.CamposUtil["CDU_Localidade"].Valor.ToString(), "Localidade") == false)
                {
                    return false;
                }
            }
            // Banco (No lado da Filopa esta informação está na tabela "TDU_Bancos". Mas nas restantes bases de dados do Grupo Mundifios esta informação está na tabela "BANCOS")
            // Assim para o banco seleccionado na caixa de texto, há necessidade de se obter qual o valor do campo CDU_BANCO_DESTINO e é esse valor que é depois pesquisado no campo Banco da tabela BANCOS da base de dados de destino.

            if (Documento_Venda_Atual.CamposUtil["CDU_Banco"].Valor != null && Strings.Trim(Documento_Venda_Atual.CamposUtil["CDU_Banco"].Valor.ToString()) != "")
            {
                if (Obtem_CDU_BANCO_DESTINO(Documento_Venda_Atual.CamposUtil["CDU_Banco"].Valor.ToString()) == "")
                {
                    return false;
                }
                else if (Verifica_Se_Codigo_Lista_Existe_na_BD_Destino(Documento_Venda_Atual, "BANCOS", "Banco", Obtem_CDU_BANCO_DESTINO(Documento_Venda_Atual.CamposUtil["CDU_Banco"].Valor.ToString()), "Banco") == false)
                {
                    return false;
                }
            }
            // Porto
            if (Documento_Venda_Atual.CamposUtil["CDU_Porto"].Valor != null && Strings.Trim(Documento_Venda_Atual.CamposUtil["CDU_Porto"].Valor.ToString()) != "")
            {
                if (Verifica_Se_Codigo_Lista_Existe_na_BD_Destino(Documento_Venda_Atual, "TDU_LOCAIS", "CDU_Local", Documento_Venda_Atual.CamposUtil["CDU_Porto"].Valor.ToString(), "Porto") == false)
                {
                    return false;
                }
            }

            // Destino
            if (Documento_Venda_Atual.CamposUtil["CDU_Destino"].Valor != null && Strings.Trim(Documento_Venda_Atual.CamposUtil["CDU_Destino"].Valor.ToString()) != "")
            {
                if (Verifica_Se_Codigo_Lista_Existe_na_BD_Destino(Documento_Venda_Atual, "TDU_LOCAIS", "CDU_Local", Documento_Venda_Atual.CamposUtil["CDU_Destino"].Valor.ToString(), "Destino") == false)
                {
                    return false;
                }
            }
            // Companhia Maritima (NOTA : na Filopa estes valores estão tabela TDU_CompanhiasMaritimas mas nas restantes bases de dados do Grupo Mundifios estes dados estão na tabela TDU_CompMaritimas)
            if (Documento_Venda_Atual.CamposUtil["CDU_CompanhiaMaritima"].Valor != null && Strings.Trim(Documento_Venda_Atual.CamposUtil["CDU_CompanhiaMaritima"].Valor.ToString()) != "")
            {
                if (Verifica_Se_Codigo_Lista_Existe_na_BD_Destino(Documento_Venda_Atual, "TDU_CompMaritimas", "CDU_Companhia", Documento_Venda_Atual.CamposUtil["CDU_CompanhiaMaritima"].Valor.ToString(), "Companhia Maritima") == false)
                {
                    return false;
                }
            }
            // --- VALIDA VALORES CAMPOS DO TIPO LOOKUP/LISTA EM TODAS AS LINHAS (NORMAIS) DO DOCUMENTO ----

            for (Nr_Linha = 1; Nr_Linha <= Documento_Venda_Atual.Linhas.NumItens; Nr_Linha++)
            {
                // Se LINHA NORMAL (com artigo)
                if ((Strings.Trim(Documento_Venda_Atual.Linhas.GetEdita(Nr_Linha).TipoLinha) == "10" | Strings.Trim(Documento_Venda_Atual.Linhas.GetEdita(Nr_Linha).TipoLinha) == "20") & Strings.Trim(Documento_Venda_Atual.Linhas.GetEdita(Nr_Linha).Artigo) != "")
                {
                    // Situação
                    if (Documento_Venda_Atual.Linhas.GetEdita(Nr_Linha).CamposUtil["CDU_Situacao"].Valor != null && Strings.Trim(Documento_Venda_Atual.Linhas.GetEdita(Nr_Linha).CamposUtil["CDU_Situacao"].Valor.ToString()) != "")
                    {
                        if (Verifica_Se_Codigo_Lista_Existe_na_BD_Destino(Documento_Venda_Atual, "TDU_SituacoesLote", "CDU_Situacao", Documento_Venda_Atual.Linhas.GetEdita(Nr_Linha).CamposUtil["CDU_Situacao"].Valor.ToString(), "Situação", " Linha Nº " + Conversion.Str(Nr_Linha)) == false)
                        {
                            return false;
                        }
                    }
                    // Tipo Qualidade
                    if (Documento_Venda_Atual.Linhas.GetEdita(Nr_Linha).CamposUtil["CDU_TipoQualidade"].Valor != null && Strings.Trim(Documento_Venda_Atual.Linhas.GetEdita(Nr_Linha).CamposUtil["CDU_TipoQualidade"].Valor.ToString()) != "")
                    {
                        if (Verifica_Se_Codigo_Lista_Existe_na_BD_Destino(Documento_Venda_Atual, "TDU_TipoQualidade", "CDU_TipoQualidade", Documento_Venda_Atual.Linhas.GetEdita(Nr_Linha).CamposUtil["CDU_TipoQualidade"].Valor.ToString(), "Tipo Qualidade", " Linha Nº " + Conversion.Str(Nr_Linha)) == false)
                        {
                            return false;
                        }
                    }
                    // Pais Origem
                    if (Documento_Venda_Atual.Linhas.GetEdita(Nr_Linha).CamposUtil["CDU_PaisOrigem"].Valor != null && Strings.Trim(Documento_Venda_Atual.Linhas.GetEdita(Nr_Linha).CamposUtil["CDU_PaisOrigem"].Valor.ToString()) != "")
                    {
                        if (Verifica_Se_Codigo_Lista_Existe_na_BD_Destino(Documento_Venda_Atual, "Paises", "Pais", Documento_Venda_Atual.Linhas.GetEdita(Nr_Linha).CamposUtil["CDU_PaisOrigem"].Valor.ToString(), "País Origem", " Linha Nº " + Conversion.Str(Nr_Linha)) == false)
                        {
                            return false;
                        }
                    }
                    // Parque
                    if (Documento_Venda_Atual.Linhas.GetEdita(Nr_Linha).CamposUtil["CDU_Parque"].Valor != null && Strings.Trim(Documento_Venda_Atual.Linhas.GetEdita(Nr_Linha).CamposUtil["CDU_Parque"].Valor.ToString()) != "")
                    {
                        if (Verifica_Se_Codigo_Lista_Existe_na_BD_Destino(Documento_Venda_Atual, "TDU_Parques", "CDU_Codigo", Documento_Venda_Atual.Linhas.GetEdita(Nr_Linha).CamposUtil["CDU_Parque"].Valor.ToString(), "Parque", " Linha Nº " + Conversion.Str(Nr_Linha)) == false)
                        {
                            return false;

                        }
                    }
                }
            }
            return true;
        }

        public bool Verifica_Se_Codigo_Lista_Existe_na_BD_Destino(VndBEDocumentoVenda Documento_Venda_Atual, string Tabela_Pesquisa, string Campo_Pesquisa, string Codigo, string Texto_Label_Campo, string Parte_Documento = "Cabeçalho")
        {
            // -------------------------------------------------------
            // --- 2019.06.07 - VIMAPONTO - Gualter Costa - #1558  ---
            // -------------------------------------------------------

            // Verifica se um valor seleccionado numa lookup/lista existe na tabela respectiva na base de dados de destino.
            string BD_Empresa_Destino;
            StdBELista Lista_Primavera = new StdBELista();
            string Sql_Query;
            // Só faz a validação na base de dados de destino se o campo a validar já estiver preenchido. Se estiver a vazio não necessita de validar.
            if (Strings.Trim(Codigo) != "")
            {
                // Acha qual a empresa de destino (com base no campo CDU_Fornecedor da ficha de forncedor da base de dados de origem)
                BD_Empresa_Destino = Strings.Trim(PriV100Api.BSO.Base.Fornecedores.DaValorAtributo(Documento_Venda_Atual.CamposUtil["CDU_Fornecedor"].Valor.ToString(), "CDU_NomeEmpresaGrupo"));


                if (BD_Empresa_Destino == "")
                {
                    MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "NÃO FOI POSSIVEL ACTUALIZAR O DOCUMENTO DE COMPRA NA BASE DE DADOS DE DESTINO " + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível apurar qual a base de dados de destino no campo CDU_NomeEmpresaGrupo da tabela fornecedores, para o fornecedor " + Documento_Venda_Atual.CamposUtil["CDU_Fornecedor"].Valor.ToString() + Strings.Chr(13) + Strings.Chr(13) + "A criação/atualizacao do documento de compra na base de dados de destino não será efectuado!");
                    return false;
                }
                // Verifica se o código seleccionado na lista, existe no campo da tabela indicados na base de dados de destino

                Sql_Query = "select * from PRI" + Strings.Trim(BD_Empresa_Destino) + ".." + Strings.Trim(Tabela_Pesquisa) + " where " + Strings.Trim(Campo_Pesquisa) + " = '" + Strings.Trim(Codigo) + "'";

                Lista_Primavera = PriV100Api.BSO.Consulta(Sql_Query);


                // Verifica se o query retornou linhas
                if (Lista_Primavera.NumLinhas() <= 0)
                {
                    // Se na tabela da base de dados de destino não houver na respectiva tabela o código seleccionado na lista
                    MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "NÃO FOI POSSIVEL ACTUALIZAR O DOCUMENTO DE COMPRA NA BASE DE DADOS DE DESTINO " + Strings.Trim(Strings.UCase(BD_Empresa_Destino)) + Strings.Chr(13) + Strings.Chr(13) + "No " + Strings.Trim(Parte_Documento) + " do documento atual, o código  (" + Strings.Trim(Codigo) + ") seleccionado no campo " + Strings.UCase(Texto_Label_Campo) + " não foi detectado na tabela " + Strings.UCase(Strings.Trim(Tabela_Pesquisa)) + " da base de dados de destino " + Strings.UCase(BD_Empresa_Destino) + "." + Strings.Chr(13) + Strings.Chr(13) + "P.f. crie na tabela " + Strings.UCase(Strings.Trim(Tabela_Pesquisa)) + " da base de dados de destino " + Strings.UCase(BD_Empresa_Destino) + " um novo registo com o código chave (" + Strings.Trim(Codigo) + ")." + Strings.Chr(13) + Strings.Chr(13) + "Posteriormente, tente gravar novamente este documento de venda." + Strings.Chr(13) + Strings.Chr(13) + "AS ALTERAÇÕES AO DOCUMENTO DE VENDA ACTUAL NÃO SERÃO POR ENQUANTO GRAVADAS NA BASE DE DADOS DE DESTINO " + Strings.Trim(Strings.UCase(BD_Empresa_Destino)) + " !!!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                Lista_Primavera.Vazia();
            }
            // Se encontrou o código seleccionado na lista, na tabela respectiva da base de dados de destino retorna TRUE
            return true;
        }

        public bool Verifica_Se_Serie_Existe_na_BD_Destino(string BD_Empresa_Destino, string TipoDoc_Documento_Compra_Destino, string Serie_Documento_Compra_Destino)
        {
            // -------------------------------------------------------
            // --- 2019.06.07 - VIMAPONTO - Gualter Costa - #1558  ---
            // -------------------------------------------------------

            // Verifica se o tipo de documento/série (proposta) existe na base de dados de destino.
            StdBELista Lista_Primavera = new StdBELista();
            string Sql_Query;

            // Só faz a validação na base de dados de destino se o campo a validar já estiver preenchido. Se estiver a vazio não necessita de validar.
            if (Strings.Trim(TipoDoc_Documento_Compra_Destino) != "" & Strings.Trim(Serie_Documento_Compra_Destino) != "")
            {
                // Verifica se a série proposta existe na base de dados de destino 
                Sql_Query = "select * from PRI" + Strings.Trim(BD_Empresa_Destino) + "..SeriesCompras where TipoDoc = '" + Strings.Trim(TipoDoc_Documento_Compra_Destino) + "' and Serie = '" + Strings.Trim(Serie_Documento_Compra_Destino) + "'";
                Lista_Primavera = PriV100Api.BSO.Consulta(Sql_Query);
                // Verifica se o query retornou linhas
                if (Lista_Primavera.NumLinhas() <= 0)
                {
                    // Se na tabela da base de dados de destino não houver na respectiva tabela o código seleccionado na lista
                    MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "NÃO FOI POSSIVEL ACTUALIZAR O DOCUMENTO DE COMPRA NA BASE DE DADOS DE DESTINO " + Strings.Trim(Strings.UCase(BD_Empresa_Destino)) + Strings.Chr(13) + Strings.Chr(13) + "A série " + Strings.Trim(Serie_Documento_Compra_Destino) + " que o documento atual deve assumir na base de dados de destino não está ainda criada na base de dados de destino " + Strings.Trim(Strings.UCase(BD_Empresa_Destino)) + " para o tipo de documento" + Strings.UCase(Strings.Trim(TipoDoc_Documento_Compra_Destino)) + Strings.Chr(13) + Strings.Chr(13) + "P.f. Inicialize a série " + Strings.Trim(Strings.UCase(Serie_Documento_Compra_Destino)) + " para o tipo de documento " + Strings.Trim(Strings.UCase(TipoDoc_Documento_Compra_Destino)) + " na base de dados de destino " + Strings.Trim(Strings.UCase(BD_Empresa_Destino)) + "." + Strings.Chr(13) + Strings.Chr(13) + "Posteriormente, tente gravar novamente este documento de venda." + Strings.Chr(13) + Strings.Chr(13) + "AS ALTERAÇÕES AO DOCUMENTO DE VENDA ACTUAL NÃO SERÃO POR ENQUANTO GRAVADAS NA BASE DE DADOS DE DESTINO " + Strings.Trim(Strings.UCase(BD_Empresa_Destino)) + " !!!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                Lista_Primavera.Vazia();
            }
            // Se encontrou a série, na tabela respectiva da base de dados de destino retorna TRUE
            return true;
        }
        public bool Anula_Documento_Compra_Relacionado_na_BD_Destino(VndBEDocumentoVenda Documento_Venda_Atual)
        {
            // -------------------------------------------------------
            // --- 2019.06.14 - VIMAPONTO - Gualter Costa - #1558  ---
            // -------------------------------------------------------
            // Anula na base de dados de destino, o documento de compra relacionado com o documento de venda actual da Filopa
            // -------------------------------------------------------------------------------------------------------
            // --- Verifica o documento de compra na base de dados de destino que o documento de venda atual criou ---
            // -------------------------------------------------------------------------------------------------------
            // Acha qual a base de dados de destino e qual o documento de compra (noutra base de dados da do Grupo Mundifios), que teve origem a partir do presente documento de venda deu origem
            string Empresa_Documento_Compra_Destino;
            string Campo_Documento_Compra_Destino;
            int Aux_Posicao_Espaco;
            int Aux_Posicao_Barra;
            string Filial_Documento_Compra_Destino;
            string TipoDoc_Documento_Compra_Destino;
            string Serie_Documento_Compra_Destino;
            int NumDoc_Documento_Compra_Destino;

            ErpBS Motor_Primavera_BD_Destino = new ErpBS();
            CmpBEDocumentoCompra Documento_DESTINO_COMPRA = new CmpBEDocumentoCompra();
            // Verifica se o documento de venda atual é do tipo "CNT" ou "EMB" e se já gerou documento de compra nalguma base de dados de destino.
            if ((Strings.Trim(Documento_Venda_Atual.Tipodoc) == "CNT" | Strings.Trim(Documento_Venda_Atual.Tipodoc) == "EMB") & Strings.Len(Documento_Venda_Atual.CamposUtil["CDU_DocumentoCompraDestino"].Valor.ToString()) > 1)
            {
                try
                {
                    // Acha qual a empresa de destino (com base no campo CDU_Fornecedor da ficha de forncedor da base de dados de origem)
                    Empresa_Documento_Compra_Destino = Strings.Trim(PriV100Api.BSO.Base.Fornecedores.DaValorAtributo(Documento_Venda_Atual.CamposUtil["CDU_Fornecedor"].Valor.ToString(), "CDU_NomeEmpresaGrupo"));
                    // Acha os dados do documento de destino, com base no campo CDU_DocumentoCompraDestino do documento de origem
                    Campo_Documento_Compra_Destino = Strings.Trim(Documento_Venda_Atual.CamposUtil["CDU_DocumentoCompraDestino"].Valor.ToString());

                    Aux_Posicao_Espaco = Campo_Documento_Compra_Destino.IndexOf(" ") + 1;
                    Aux_Posicao_Barra = Campo_Documento_Compra_Destino.IndexOf("/") + 1;

                    Filial_Documento_Compra_Destino = "000";
                    TipoDoc_Documento_Compra_Destino = Strings.Trim(Strings.Mid(Campo_Documento_Compra_Destino, 1, Aux_Posicao_Espaco));
                    Serie_Documento_Compra_Destino = Strings.Trim(Strings.Mid(Campo_Documento_Compra_Destino, Aux_Posicao_Espaco, Aux_Posicao_Barra - Aux_Posicao_Espaco));
                    NumDoc_Documento_Compra_Destino = Convert.ToInt32(Conversion.Val(Strings.Trim(Strings.Mid(Campo_Documento_Compra_Destino, Aux_Posicao_Barra + 1, Strings.Len(Campo_Documento_Compra_Destino)))));


                    // Verifica se conseguiu obter a base de dados de destino e o tipodoc/serie/número do documento de compra gerado automáticamente por este documento de venda na base de dados de destino
                    if (Strings.Trim(Empresa_Documento_Compra_Destino) == "" | Strings.Trim(Filial_Documento_Compra_Destino) == "" | Strings.Trim(TipoDoc_Documento_Compra_Destino) == "" | Strings.Trim(Serie_Documento_Compra_Destino) == "" | Strings.Trim(NumDoc_Documento_Compra_Destino.ToString()) == "")
                    {
                        MessageBox.Show("Anula_Documento_Compra_Relacionado_na_BD_Destino()" + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível detectar qual a base de dados ou tipodoc/serie/numero do documento de compra de destino gerado automáticamente por este documento de venda." + Strings.Chr(13) + Strings.Chr(13) + "Assim, não será possível a anulação do documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Anula_Documento_Compra_Relacionado_na_BD_Destino()" + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível detectar qual a base de dados ou qual o número do documento de compra gerado automáticamente por este documento de venda." + Strings.Chr(13) + Strings.Chr(13), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                // Se foi possível identificar o documento de compra (relacionado a este documento de venda) na base de dados de destino
                // Anula o documento de compra relacionado na base de dados de destino
                // Abre os Motores Primavera (para a empresa de Destino)
                try
                {
                    Motor_Primavera_BD_Destino.AbreEmpresaTrabalho(PriV100Api.BSO.Contexto.TipoPlataforma, Empresa_Documento_Compra_Destino, PriV100Api.BSO.Contexto.UtilizadorActual, PriV100Api.BSO.Contexto.PasswordUtilizadorActual);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Anula_Documento_Compra_Relacionado_na_BD_Destino()" + Strings.Chr(13) + Strings.Chr(13) + "Erro ao abrir o Motor Primavera Empresa Destino : " + Empresa_Documento_Compra_Destino, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                // Anula o documento de compra na base de dados de destino
                try
                {
                    Motor_Primavera_BD_Destino.Compras.Documentos.AnulaDocumento(Filial_Documento_Compra_Destino, TipoDoc_Documento_Compra_Destino, Serie_Documento_Compra_Destino, NumDoc_Documento_Compra_Destino);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Anula_Documento_Compra_Relacionado_na_BD_Destino()" + Strings.Chr(13) + Strings.Chr(13) + "Erro ao Anular o documento de compra na base de dados da Empresa Destino : " + Empresa_Documento_Compra_Destino, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            // Se correu bem a anulação na base de dados de destino, retorna True
            return true;
        }

        private string Obtem_CDU_BANCO_DESTINO(string CDU_Banco)
        {
            // -------------------------------------------------------
            // --- 2019.06.18 - VIMAPONTO - Gualter Costa - #1558  ---
            // -------------------------------------------------------

            // Acha na tabela TDU_BANCOS da Filopa o valor do campo CDU_BANCO_DESTINO.
            StdBELista Lista_Primavera = new StdBELista();
            string Sql_Query;
            string Obtem_CDU_BANCO_DESTINO = ""; // Inicializa a vazio

            if (Strings.Trim(CDU_Banco) != "")
            {
                Sql_Query = "select CDU_BANCO_DESTINO from TDU_BANCOS where CDU_BANCO = '" + Strings.Trim(CDU_Banco) + "'";

                Lista_Primavera = PriV100Api.BSO.Consulta(Sql_Query);

                // Verifica se o query retornou linhas
                if (Lista_Primavera.NumLinhas() <= 0)
                {

                    // Se não conseguiu obter o CDU_BANCO_DESTINO
                    MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "Não foi possível encontrar na tabela TDU_BANCOS da base de dados FILOPA o banco com o código " + Strings.Trim(CDU_Banco) + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "";

                }
                else
                {

                    // Verifica se retornou um valor válido
                    if (Convert.IsDBNull(Lista_Primavera.Valor("CDU_BANCO_DESTINO")))
                    {
                        MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "O banco seleccionado com o código (" + Strings.Trim(CDU_Banco) + ") não tem o campo CDU_BANCO_DESTINO preenchido na tabela TDU_BANCOS da base de dados FILOPA." + Strings.Chr(13) + Strings.Chr(13) + "Deve preencher o campo CDU_BANCO_DESTINO.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return "";
                        if (Strings.Trim(Lista_Primavera.Valor("CDU_BANCO_DESTINO")) == "")
                        {
                            MessageBox.Show("AVISO : " + Strings.Chr(13) + Strings.Chr(13) + "O banco seleccionado com o código (" + Strings.Trim(CDU_Banco) + ") não tem o campo CDU_BANCO_DESTINO preenchido na tabela TDU_BANCOS da base de dados FILOPA." + Strings.Chr(13) + Strings.Chr(13) + "Deve preencher o campo CDU_BANCO_DESTINO.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return "";
                        }
                    }

                    Obtem_CDU_BANCO_DESTINO = Lista_Primavera.Valor("CDU_BANCO_DESTINO"); // Retorna o CDU_BANCO_DESTINO
                }

                Lista_Primavera.Vazia();
            }
            return Obtem_CDU_BANCO_DESTINO;
        }

        public bool Verifica_Se_Artigo_Lote_Existe_na_Bd_Destino(string BD_Empresa_Destino, string Artigo, string Lote)
        {
            // -------------------------------------------------------
            // --- 2019.07.10 - VIMAPONTO - Gualter Costa - #1558  ---
            // -------------------------------------------------------
            // Verifica se um dado Artigo/Lote já existe na base de dados de destino
            StdBELista Lista_Primavera = new StdBELista();
            string Sql_Query;
            // Verifica se o artigo/lote existe na base de dados de destino 

            Sql_Query = "select * from PRI" + Strings.Trim(BD_Empresa_Destino) + "..ArtigoLote where Artigo = '" + Strings.Trim(Artigo) + "' and Lote = '" + Strings.Trim(Lote) + "'";

            Lista_Primavera = PriV100Api.BSO.Consulta(Sql_Query);
            // Verifica se o query retornou linhas
            if (Lista_Primavera.NumLinhas() > 0)
            {
                if (string.Equals(Strings.Trim(PriV100Api.BSO.Inventario.ArtigosLotes.DaValorAtributo(Artigo, Lote, "CDU_TipoQualidade")), Strings.Trim(Lista_Primavera.Valor("CDU_TipoQualidade"))) == false)
                    MessageBox.Show("INFORMAÇÃO : " + Strings.Chr(13) + Strings.Chr(13) + "O tipo de qualidade (CDU_TipoQualidade) do artigo/lote " + Artigo + "/" + Lote + " na empresa de destino " + BD_Empresa_Destino + "é diferente do empresa de origem FILOPA." + Strings.Chr(13) + Strings.Chr(13) + "P.F. Analise esta situação.", Constants.vbInformation.ToString());


                if (string.Equals(Strings.Trim(Convert.ToString(PriV100Api.BSO.Inventario.ArtigosLotes.DaValorAtributo(Artigo, Lote, "CDU_Parafinado"))), Strings.Trim(Convert.ToString(Lista_Primavera.Valor("CDU_Parafinado")))) == false)
                    MessageBox.Show("INFORMAÇÃO : " + Strings.Chr(13) + Strings.Chr(13) + "O parafinado (CDU_Parafinado) do artigo/lote " + Artigo + "/" + Lote + " na empresa de destino " + BD_Empresa_Destino + "é diferente do empresa de origem FILOPA." + Strings.Chr(13) + Strings.Chr(13) + "P.F. Analise esta situação.", Constants.vbInformation.ToString());

                // Se encontrou/criou o lote na bd destino
                return true;
            }
            Lista_Primavera.Vazia();
            return false;
        }
    }
}
