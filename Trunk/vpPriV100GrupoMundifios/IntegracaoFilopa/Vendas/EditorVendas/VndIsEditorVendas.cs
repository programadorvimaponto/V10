using Generico;
using InvBE100;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using System;
using System.Windows.Forms;

namespace IntegracaoFilopa
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            if (Module1.VerificaToken("IntegracaoFilopa") == 1)
            {
                // #############################################################################################################
                // ############# JFC - 21/10/2019 - Copia da Lotes para a Mundifios.                                      ######
                // #############                   (Primeiro grava o lote na Filopa depois copia o lote para a Mundifios  ######
                // #############################################################################################################
                if ((this.DocumentoVenda.Tipodoc == "CNT" | this.DocumentoVenda.Tipodoc == "EMB") & this.DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor + "" != "" & BSO.Base.Fornecedores.Edita(this.DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor.ToString()).CamposUtil["CDU_NomeEmpresaGrupo"].Valor + "" != "" & BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).CamposUtil["CDU_EntidadeInterna"].Valor + "" != "")
                {
                    StdBELista listForn;
                    listForn = BSO.Consulta("select top 1 Fornecedor  from PRIMUNDIFIOS.dbo.Fornecedores where CDU_EntidadeInterna='" + BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).CamposUtil["CDU_EntidadeInterna"].Valor + "' and FornecedorAnulado='0'");
                    listForn.Inicio();
                    for (int i = 1, loopTo = this.DocumentoVenda.Linhas.NumItens; i <= loopTo; i++)
                    {
                        if (this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "" & this.DocumentoVenda.Linhas.GetEdita(i).Lote + "" != "" & this.DocumentoVenda.Linhas.GetEdita(i).Lote + "" != "<L01>" & this.DocumentoVenda.Linhas.GetEdita(i).Estado == "P" & this.DocumentoVenda.Linhas.GetEdita(i).Fechado == false)
                        {
                            // Cria lotes na Filopa
                            var ArtigoLote = new InvBEArtigoLote();
                            if (BSO.Inventario.ArtigosLotes.Existe(this.DocumentoVenda.Linhas.GetEdita(i).Artigo, this.DocumentoVenda.Linhas.GetEdita(i).Lote) == false)
                            {
                                ArtigoLote.Artigo = this.DocumentoVenda.Linhas.GetEdita(i).Artigo;
                                ArtigoLote.Lote = this.DocumentoVenda.Linhas.GetEdita(i).Lote;
                                ArtigoLote.Descricao = BSO.Contexto.CodEmp + ", " + BSO.Contexto.UtilizadorActual;
                                ArtigoLote.DataFabrico = DateAndTime.Now;
                                ArtigoLote.Validade = DateAndTime.Now;
                                ArtigoLote.Activo = true;
                                ArtigoLote.Observacoes = this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_ObsLote"].Valor.ToString();
                                ArtigoLote.CamposUtil["CDU_TipoQualidade"].Valor = this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_TipoQualidade"].Valor;
                                ArtigoLote.CamposUtil["CDU_Parafinado"].Valor = this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Parafinado"].Valor;
                                ArtigoLote.CamposUtil["CDU_LoteForn"].Valor = this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_LoteFornecedor"].Valor;
                                if (listForn.Vazia() == false)
                                {
                                    ArtigoLote.CamposUtil["CDU_Fornecedor"].Valor = listForn.Valor("Fornecedor");
                                }

                                BSO.Inventario.ArtigosLotes.Actualiza(ArtigoLote);
                            }
                            else
                            {
                                // Atualiza Lotes
                                var Campos = new StdBECampos();
                                Campos = BSO.Inventario.ArtigosLotes.DaValorAtributos(this.DocumentoVenda.Linhas.GetEdita(i).Artigo, this.DocumentoVenda.Linhas.GetEdita(i).Lote, "Observacoes", "CDU_TipoQualidade", "CDU_Parafinado", "CDU_LoteForn", "CDU_Fornecedor");
                                Campos["Observacoes"].Valor = this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_ObsLote"].Valor;
                                Campos["CDU_TipoQualidade"].Valor = this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_TipoQualidade"].Valor;
                                Campos["CDU_Parafinado"].Valor = this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Parafinado"].Valor;
                                Campos["CDU_LoteForn"].Valor = this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_LoteFornecedor"].Valor;
                                if (listForn.Vazia() == false)
                                {
                                    Campos["CDU_Fornecedor"].Valor = listForn.Valor("Fornecedor");
                                }

                                BSO.Inventario.ArtigosLotes.ActualizaValorAtributos(this.DocumentoVenda.Linhas.GetEdita(i).Artigo, this.DocumentoVenda.Linhas.GetEdita(i).Lote, Campos);
                            }
                        }
                    }

                    // Cria lotes na Mundifios
                    if (Module1.AbreEmpresa("MUNDIFIOS"))
                    {
                        for (int i = 1, loopTo1 = this.DocumentoVenda.Linhas.NumItens; i <= loopTo1; i++)
                        {
                            if (this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "" & this.DocumentoVenda.Linhas.GetEdita(i).Lote + "" != "" & this.DocumentoVenda.Linhas.GetEdita(i).Lote + "" != "<L01>")
                            {
                                if (BSO.Base.Artigos.Existe(this.DocumentoVenda.Linhas.GetEdita(i).Artigo) == true & (BSO.Base.Artigos.Edita(this.DocumentoVenda.Linhas.GetEdita(i).Artigo).Descricao.Contains("Fio") | (BSO.Base.Artigos.Edita(this.DocumentoVenda.Linhas.GetEdita(i).Artigo).Descricao.Contains("Rama"))))
                                {
                                    CopiaLote(this.DocumentoVenda.Linhas.GetEdita(i).Artigo, this.DocumentoVenda.Linhas.GetEdita(i).Lote);
                                }
                            }
                        }

                        Module1.FechaEmpresa();
                    }
                    // JFC 14/07/2021
                    // Cria lotes na Empresa Destino (Ignora a Mundifios porque já copiou em cima)
                    if (BSO.Base.Fornecedores.Edita(this.DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor.ToString()).CamposUtil["CDU_NomeEmpresaGrupo"].Valor + "" != "Mundifios" & Module1.AbreEmpresa("" + BSO.Base.Fornecedores.Edita(this.DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor.ToString()).CamposUtil["CDU_NomeEmpresaGrupo"].Valor + ""))
                    {
                        for (int i = 1, loopTo2 = this.DocumentoVenda.Linhas.NumItens; i <= loopTo2; i++)
                        {
                            if (this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "" & this.DocumentoVenda.Linhas.GetEdita(i).Lote + "" != "" & this.DocumentoVenda.Linhas.GetEdita(i).Lote + "" != "<L01>")
                            {
                                if (BSO.Base.Artigos.Existe(this.DocumentoVenda.Linhas.GetEdita(i).Artigo) == true & (BSO.Base.Artigos.Edita(this.DocumentoVenda.Linhas.GetEdita(i).Artigo).Descricao.Contains("Fio") | BSO.Base.Artigos.Edita(this.DocumentoVenda.Linhas.GetEdita(i).Artigo).Descricao.Contains("Rama")))
                                {
                                    CopiaLote(this.DocumentoVenda.Linhas.GetEdita(i).Artigo, this.DocumentoVenda.Linhas.GetEdita(i).Lote);
                                }
                            }
                        }

                        Module1.FechaEmpresa();
                    }
                }

                // #############################################################################################################
                // ############# JFC - 21/10/2019 - Copia da Lotes para a Mundifios.                                      ######
                // #############                   (Primeiro grava o lote na Filopa depois copia o lote para a Mundifios  ######
                // #############################################################################################################

                // Sempre que se grava um CNT/EMB ele altera a serie a copiar para a serie atual. JFC - 04/01/2021
                // Ao passar de ano, há sempre contratos e embarques a registar do ano transato, mas o desenvolvimento dava erro na data px: data 2020 e serie 2021 não compativeis.
                if (this.DocumentoVenda.Tipodoc == "CNT" | this.DocumentoVenda.Tipodoc == "EMB")
                {
                    // BSO.Comercial.TabVendas.Edita(Me.DocumentoVenda.TipoDoc).CamposUtil("CDU_SerieComprasDestino") = Me.DocumentoVenda.Serie
                    BSO.DSO.ExecuteSQL("update DocumentosVenda set CDU_SerieComprasDestino='" + this.DocumentoVenda.Serie + "' where Documento='" + this.DocumentoVenda.Tipodoc + "'");
                }

                // #############################################################################################################################################
                // ############# JFC - 25/10/2019 - Copiar campo CDU_DataPrevistaChegada para LinhasCompras.DataEntrega                                   ######
                // #############################################################################################################################################
                if ((this.DocumentoVenda.Tipodoc == "CNT" | this.DocumentoVenda.Tipodoc == "EMB") & this.DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor + "" != "" & BSO.Base.Fornecedores.Edita(this.DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor.ToString()).CamposUtil["CDU_NomeEmpresaGrupo"].Valor + "" != "")
                {
                    if (Module1.AbreEmpresa(BSO.Base.Fornecedores.Edita(this.DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor.ToString()).CamposUtil["CDU_NomeEmpresaGrupo"].Valor.ToString()))
                    {
                        DateTime DataChegada;
                        StdBELista listDocDestino;
                        listDocDestino = BSO.Consulta("select cd.CDU_DocumentoCompraDestino from  CabecDoc cd where cd.Id='" + this.DocumentoVenda.ID + "'");
                        if (listDocDestino.Vazia() == false)
                        {
                            listDocDestino.Inicio();
                            if (listDocDestino.Valor("CDU_DocumentoCompraDestino") + "" != "")
                            {
                                for (int i = 1, loopTo3 = this.DocumentoVenda.Linhas.NumItens; i <= loopTo3; i++)
                                {
                                    if (this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "")
                                    {
                                        // DataChegada = ObjEmpresa.Comercial.Compras.Edita("000", "CNT", "2019X", 6).Linhas.Edita(i).DataEntrega
                                        DataChegada = DateTime.Parse(this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_DataPrevistaChegada"].Valor.ToString());
                                        // JFC 20/10/2020 - Copiar CDU_NVolumes para LinhasCompras.CDU_Volumes
                                        BSO.DSO.ExecuteSQL("update LinhasCompras set DataEntrega='" + Strings.Format(DataChegada, "yyyy-MM-dd") + "', CDU_Volumes='" + this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_NVolumes"].Valor + "'  where CDU_IDLinhaOriginalGrupo = '" + this.DocumentoVenda.Linhas.GetEdita(i).IdLinha + "'");
                                    }
                                }

                                // JFC 28/11/2019 Copiar as faturas para a ECF NumDocExterno.
                                if (this.DocumentoVenda.Tipodoc == "EMB")
                                {
                                    StdBELista listNFatura;
                                    string NFatura;
                                    NFatura = "";
                                    listNFatura = BSO.Consulta("select distinct ln.CDU_NFatura from LinhasDoc ln inner join Artigo a on a.Artigo=ln.Artigo where ln.IdCabecDoc='" + this.DocumentoVenda.ID + "'");
                                    listNFatura.Inicio();
                                    for (int i = 1, loopTo4 = listNFatura.NumLinhas(); i <= loopTo4; i++)
                                    {
                                        NFatura = NFatura + listNFatura.Valor("CDU_NFatura") + "-";
                                        listNFatura.Seguinte();
                                    }

                                    NFatura = Strings.Mid(NFatura, 1, Strings.Len(NFatura) - 1);
                                    if (Strings.Len(NFatura) > 18)
                                    {
                                        MessageBox.Show("Atenção! A concatenação das Faturas de fornecedor ultrapassa os 18 caracteres permitidos: " + Strings.Len(NFatura) + " - " + NFatura + '\r' + "O resultado a integrar será: " + Strings.Mid(NFatura, 1, 18), "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        NFatura = Strings.Mid(NFatura, 1, 18);
                                    }

                                    // JFC 04/09/2019 - Identificar a posição da barra e do espaço (TipoDoc Serie/NumDoc)
                                    int PosBarra;
                                    int PosEsp;
                                    string TipoDocFinal;
                                    string SerieFinal;
                                    string NumDocFinal;
                                    PosBarra = Strings.InStr(1, listDocDestino.Valor("CDU_DocumentoCompraDestino"), "/", Constants.vbTextCompare);
                                    PosEsp = Strings.InStr(1, listDocDestino.Valor("CDU_DocumentoCompraDestino"), " ", Constants.vbTextCompare);
                                    TipoDocFinal = Strings.Left(listDocDestino.Valor("CDU_DocumentoCompraDestino"), PosEsp - 1);
                                    SerieFinal = Strings.Mid(listDocDestino.Valor("CDU_DocumentoCompraDestino"), PosEsp + 1, PosBarra - PosEsp - 1);
                                    NumDocFinal = Strings.Mid(listDocDestino.Valor("CDU_DocumentoCompraDestino"), PosBarra + 1);
                                    BSO.DSO.ExecuteSQL("update cc set cc.NumDocExterno='" + NFatura + "' from pri" + BSO.Base.Fornecedores.Edita(this.DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor.ToString()).CamposUtil["CDU_NomeEmpresaGrupo"].Valor + ".dbo.CabecCompras cc where cc.TipoDoc='" + TipoDocFinal + "' and cc.NumDoc='" + NumDocFinal + "' and cc.Serie='" + SerieFinal + "'");
                                }
                            }
                        }

                        Module1.FechaEmpresa();
                    }
                }
            }
            // #############################################################################################################################################
            // ############# JFC - 25/10/2019 - Copiar campo CDU_DataPrevistaChegada para LinhasCompras.DataEntrega                                   ######
            // #############################################################################################################################################
        }

        public object CopiaLote(string str_Artigo, string str_Lote)
        {
            if (string.IsNullOrEmpty(str_Lote))
                return default;
            if (BSO.Inventario.ArtigosLotes.Existe(str_Artigo, str_Lote) == false)
            {
                var ArtigoLote = new InvBEArtigoLote();
                StdBELista stdBE_ListaLote;
                stdBE_ListaLote = BSO.Consulta(" SELECT * FROM ArtigoLote " + " WHERE Artigo = '" + str_Artigo + "' " + " AND Lote = '" + str_Lote + "'");
                if (!stdBE_ListaLote.Vazia())
                {
                    stdBE_ListaLote.Inicio();
                    ArtigoLote.Artigo = stdBE_ListaLote.Valor("Artigo");
                    ArtigoLote.Lote = stdBE_ListaLote.Valor("Lote");
                    ArtigoLote.Descricao = BSO.Contexto.CodEmp + ", " + BSO.Contexto.UtilizadorActual;
                    if (Strings.Len(stdBE_ListaLote.Valor("DataFabrico")) > 0)
                        ArtigoLote.DataFabrico = stdBE_ListaLote.Valor("DataFabrico");
                    if (Strings.Len(stdBE_ListaLote.Valor("Validade")) > 0)
                        ArtigoLote.Validade = stdBE_ListaLote.Valor("Validade");
                    ArtigoLote.Controlador = stdBE_ListaLote.Valor("Controlador");
                    ArtigoLote.Activo = stdBE_ListaLote.Valor("Activo");
                    ArtigoLote.Observacoes = stdBE_ListaLote.Valor("Observacoes");
                    ArtigoLote.CamposUtil["CDU_TipoQualidade"].Valor = stdBE_ListaLote.Valor("CDU_TipoQualidade");
                    ArtigoLote.CamposUtil["CDU_Parafinado"].Valor = stdBE_ListaLote.Valor("CDU_Parafinado");
                    ArtigoLote.CamposUtil["CDU_LoteForn"].Valor = stdBE_ListaLote.Valor("CDU_LoteForn");
                    ArtigoLote.CamposUtil["CDU_Fornecedor"].Valor = stdBE_ListaLote.Valor("CDU_Fornecedor");
                    BSO.Inventario.ArtigosLotes.Actualiza(ArtigoLote);
                }
            }
            else
            {
                // Atualiza Lotes
                var Campos = new StdBECampos();
                Campos = BSO.Inventario.ArtigosLotes.DaValorAtributos(str_Artigo, str_Lote, "Observacoes", "CDU_TipoQualidade", "CDU_Parafinado", "CDU_LoteForn", "CDU_Fornecedor");
                BSO.Inventario.ArtigosLotes.ActualizaValorAtributos(str_Artigo, str_Lote, Campos);
            }

            return default;
        }

        // ####################################################################################################
        // ######## JFC 21/10/2019 Funções adicionadas para copia de lotes para a Mundifios           #########
        // ####################################################################################################

        public override void DepoisDeTransformar(ExtensibilityEventArgs e)
        {
            base.DepoisDeTransformar(e);

            if (Module1.VerificaToken("IntegracaoFilopa") == 1)
            {
                // ###################################################################
                // ###### Coloca a descrição original do produto Bruno - 21/10/2019 ##
                // ###################################################################
                int i;
                if (this.DocumentoVenda.Tipodoc == "CNT" & BSO.Base.Fornecedores.Edita(this.DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor.ToString()).CamposUtil["CDU_NomeEmpresaGrupo"].Valor + "" != "")
                {
                    var loopTo = this.DocumentoVenda.Linhas.NumItens;
                    for (i = 1; i <= loopTo; i++)
                    {
                        if (this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "")
                        {
                            // JFC 23/10/2019 Apos reunião com a Filopa foi pedido para manter guardada a descrição original da NEC
                            this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_LinhaDescricaoNEC"].Valor = this.DocumentoVenda.Linhas.GetEdita(i).Descricao;
                            this.DocumentoVenda.Linhas.GetEdita(i).Descricao = BSO.Base.Artigos.Edita(this.DocumentoVenda.Linhas.GetEdita(i).Artigo).Descricao + " " + BSO.Base.Artigos.Edita(this.DocumentoVenda.Linhas.GetEdita(i).Artigo).CamposUtil["CDU_Descricaoextra"].Valor;
                        }
                    }
                }

                // ###################################################################
                // ###### Coloca a descrição original do produto Bruno - 21/10/2019 ##
                // ###################################################################

                // ###############################################################################################
                // ###### Peso Liquido = Quantidade e Situação = Embarque (Pedido Ana Castro JFC - 23/10/2019   ##
                // ###############################################################################################

                if ((this.DocumentoVenda.Tipodoc == "EMB" | this.DocumentoVenda.Tipodoc == "CNT") & BSO.Base.Fornecedores.Edita(this.DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor.ToString()).CamposUtil["CDU_NomeEmpresaGrupo"].Valor + "" != "")
                {
                    var loopTo1 = this.DocumentoVenda.Linhas.NumItens;
                    for (i = 1; i <= loopTo1; i++)
                    {
                        if (this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "")
                        {
                            if (this.DocumentoVenda.Tipodoc == "EMB")
                            {
                                this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_PesoLiquido"].Valor = this.DocumentoVenda.Linhas.GetEdita(i).Quantidade;
                                this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Situacao"].Valor = "004";
                                this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_LoteFornecedor"].Valor = "0";
                                this.DocumentoVenda.CamposUtil["CDU_NBL"].Valor = "0";
                            }
                            else
                            {
                                this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Situacao"].Valor = "001";
                            }
                        }
                    }
                }

                // ###############################################################################################
                // ###### Peso Liquido = Quantidade e Situação = Embarque (Pedido Ana Castro JFC - 23/10/2019   ##
                // ###############################################################################################
            }
        }

        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(KeyCode, Shift, e);
            string ent = string.Empty;

            if (Module1.VerificaToken("IntegracaoFilopa") == 1)
            {
                // ############################################################################################
                // ####              JFC 21/10/2019 Sugestão de Lotes                            ##############
                // ############################################################################################

                // Alt+F - Sugere Lotes
                if (KeyCode == 70 & (this.DocumentoVenda.Tipodoc == "CNT" | this.DocumentoVenda.Tipodoc == "EMB"))
                {
                    if (this.DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor + "" == "")
                    {
                        MessageBox.Show("Atenção: Cliente não está preenchido, não é possivel sugerir lotes", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (BSO.Base.Fornecedores.Edita(this.DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor.ToString()).CamposUtil["CDU_NomeEmpresaGrupo"].Valor + "" == "")
                    {
                        MessageBox.Show("Atenção: Cliente não faz parte do Grupo Mundifios (CDU_NomeEmpresaGrupo), não é possivel sugerir lotes", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Primeira validação - Campo EntidadeInterna na ficha do Fornecedor
                    if (BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).CamposUtil["CDU_EntidadeInterna"].Valor + "" == "")
                    {
                        MessageBox.Show("Atenção: Fornecedor sem EntidadeInterna", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        StdBELista listForn;
                        listForn = BSO.Consulta("select top 1 Fornecedor  from PRIMUNDIFIOS.dbo.Fornecedores where CDU_EntidadeInterna='" + BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).CamposUtil["CDU_EntidadeInterna"].Valor + "' and FornecedorAnulado='0'");
                        listForn.Inicio();

                        // Segunda validação - Se o Fornecedor existe na Mundifios

                        if (listForn.Vazia())
                        {
                            // Me.DocumentoCompra.Linhas(NumLinha).lote = "0000"
                            MessageBox.Show("Atenção: Fornecedor inexistente na Mundifios", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            // Se as duas validações acima estiverem ok, então guarda a Entidade do Fornecedor na Mundifios
                            ent = listForn.Valor("Fornecedor");
                        }
                    }

                    // Segunda parte, atibuição do lote
                    int fj;
                    int lote;
                    int loteAux;
                    StdBELista listLote;
                    var loopTo = this.DocumentoVenda.Linhas.NumItens;
                    for (fj = 1; fj <= loopTo; fj++)
                    {
                        if (this.DocumentoVenda.Linhas.GetEdita(fj).Lote + "" == "" | this.DocumentoVenda.Linhas.GetEdita(fj).Lote + "" == "<L01>" & this.DocumentoVenda.Linhas.GetEdita(fj).Artigo + "" != "")
                        {
                            // Consulta qual o proximo lote a ser utilizado. Função dbo.fnProximoLote
                            listLote = BSO.Consulta("select PRIMUNDIFIOS.dbo.fnProximoLote('" + BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).CamposUtil["CDU_EntidadeInterna"].Valor + "','" + this.DocumentoVenda.Linhas.GetEdita(fj).Artigo + "') as 'Lote'");
                            loteAux = 0;
                            listLote.Inicio();

                            // Primeira validação, ver se já existe nas outras linhas algum lote inserido e guarda o valor do maior lote.
                            for (int i = 1, loopTo1 = this.DocumentoVenda.Linhas.NumItens; i <= loopTo1; i++)
                            {
                                if (DocumentoVenda.Linhas.GetEdita(i).Artigo == DocumentoVenda.Linhas.GetEdita(fj).Artigo && i != fj && Strings.Len(DocumentoVenda.Linhas.GetEdita(i).Lote) == 8)
                                {
                                    lote = Convert.ToInt32(Strings.Right(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4));
                                    if (lote > loteAux)
                                    {
                                        loteAux = lote;
                                    }
                                }
                            }

                            // Se já existir o lote noutra linha, então o novo lote deverá ser = lote + 1
                            if (loteAux != 0)
                            {
                                loteAux = loteAux + 1;

                                // Conjunto de validações para garantir que o lote+1 é superior ou igual ao lote sugerido pela função dbo.fnProximoLote
                                if (listLote.Vazia() | listLote.Valor("Lote") == "")
                                {
                                    int i = 4 - Strings.Len(loteAux.ToString());
                                    this.DocumentoVenda.Linhas.GetEdita(fj).Lote = "" + ent + Strings.Left("0000", i) + loteAux;
                                }
                                else if (loteAux <= Convert.ToInt32(Strings.Right(listLote.Valor("Lote"), 4)))
                                {
                                    this.DocumentoVenda.Linhas.GetEdita(fj).Lote = listLote.Valor("Lote");
                                }
                                else
                                {
                                    int i = 4 - Strings.Len(loteAux.ToString());
                                    this.DocumentoVenda.Linhas.GetEdita(fj).Lote = "" + ent + Strings.Left("0000", i) + loteAux;
                                }
                            }

                            // Caso não exista nenhum lote inserido nas outras linhas, então sugere o lote devolvido pela função dbo.fnProximoLote

                            // Se não existir nenhum lote, então é o primeiro lote
                            else if (listLote.Vazia() | listLote.Valor("Lote") == "")
                            {
                                this.DocumentoVenda.Linhas.GetEdita(fj).Lote = "" + ent + "0001";
                            }
                            else
                            {
                                this.DocumentoVenda.Linhas.GetEdita(fj).Lote = listLote.Valor("Lote");
                            }
                        }
                    }
                }

                // ############################################################################################
                // ####              JFC 21/10/2019 Sugestão de Lotes                            ##############
                // ############################################################################################

                // Ctrl+F1 - Preenche dados dos campos de utilizador
                if (KeyCode == 112)
                {
                    // Verifica se é uma linha que não existe na tabela linhascompras
                    if (this.LinhaActual == -1)
                    {
                        return;
                    }

                    // Verifica se é uma linha de texto, sem artigo
                    if (this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Artigo + "" == "")
                    {
                        return;
                    }

                    Module1.ArtigoEnc = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Artigo;
                    Module1.DescArtEnc = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Descricao;
                    Module1.LoteEnc = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Lote;
                    Module1.LinhaEnc = this.LinhaActual;

                    ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmOutrosDadosView));
                    FrmOutrosDadosView frm = result.Result;
                    frm.DocumentoVenda = DocumentoVenda;
                    frm.ShowDialog();
                }
            }
        }
    }
}