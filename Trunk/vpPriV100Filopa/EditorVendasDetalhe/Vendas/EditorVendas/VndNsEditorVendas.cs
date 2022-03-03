using Primavera.Extensibility.Sales.Editors;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Vimaponto.PrimaveraV100.Clientes.Filopa.EditorVendasDetalhe.DataSets;
using Microsoft.VisualBasic;
using VndBE100;
using System.Windows.Forms;
using TesBE100;
using CblBE100;
using System.Data;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using Vimaponto.PrimaveraV100;

namespace EditorVendasDetalhe
{
    public class VndNsEditorVendas : EditorVendas
    {
        private DsEditorVendasDetalhe DsEditorVendasDetalhe = new DsEditorVendasDetalhe();

        public override void AntesDeAnular(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeAnular(ref Cancel, e);

            // Define o Contexto
            DsEditorVendasDetalhe.SetContexto(DocumentoVenda, DocumentoTesouraria, DocumentoCBL, LinhaActual, Nome);

            // Instancia a classe Intercambio_Documentos_Primavera (para poder usar as funções que estão dentro da mesma)
            Intercambio_Documentos_Primavera IDP = new Intercambio_Documentos_Primavera();


            // Verifica se o documento de venda atual é do tipo "CNT" ou "EMB" e se já gerou documento de compra nalguma base de dados de destino.
            if ((Strings.Trim(DocumentoVenda.Tipodoc) == "CNT" | Strings.Trim(DocumentoVenda.Tipodoc) == "EMB") & !Convert.IsDBNull(DocumentoVenda.CamposUtil["CDU_DocumentoCompraDestino"].Valor))
            {
                switch (IDP.Verifica_Se_Documento_Compra_Criado_na_BD_Destino_NAO_Sofreu_Transformacao_ou_Copia_Linhas(DocumentoVenda))
                {
                    case -1: // Se deu erro, tratado internamente na função.
                        {
                            Cancel = true; // Cancela a anulação (já não vai correr o evento EditorVendas_DepoisDeAnular) , sai e cancela a Anulação.
                            break;
                        }

                    case 0: // Se encontrou linhas já transformadas (nos documentos de compra noutras bases de dados gerados a partir deste documento de venda), sai e cancela a anulação.
                        {
                            MessageBox.Show("Não é possível ANULAR o documento atual!" + Strings.Chr(13) + Strings.Chr(13) + "O primavera detectou documentos de compra noutras bases de dados, gerados de forma automática a partir deste documento de venda, que já sofreram alterações" + Strings.Chr(13) + "por 'Transformação de Documentos' ou por 'Cópia de Linhas'." + Strings.Chr(13) + Strings.Chr(13) + "Assim, não é possível ANULAR o presente documento de venda " + this.DocumentoVenda.Tipodoc + "/" + this.DocumentoVenda.Serie + "/" + this.DocumentoVenda.NumDoc, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            Cancel = true; // Cancela a anulação (já não vai correr o evento EditorVendas_DepoisDeAnular)
                            break;
                        }

                    case 1:  // Se NÃO encontrou linhas já transformadas (nos documentos de compra noutras bases de dados gerados a partir deste documento de venda)
                        {
                            // Avança para a anulação
                            // Se falhou a anulação do documento de compra relacionado na base de dados de destino, aborta a anulação
                            if (IDP.Anula_Documento_Compra_Relacionado_na_BD_Destino(this.DocumentoVenda) == false)
                                Cancel = true;
                            else
                            {
                            }

                            break;
                        }
                }
            }
        }

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);
            // Define o contexto
            DsEditorVendasDetalhe.SetContexto(this.DocumentoVenda, DocumentoTesouraria, DocumentoCBL, LinhaActual, Nome);
            // Instancia a classe Intercambio_Documentos_Primavera (para poder usar as funções que estão dentro da mesma)
            Intercambio_Documentos_Primavera IDP = new Intercambio_Documentos_Primavera();
            try
            {
                DsEditorVendasDetalhe.VerificaCamposObrigatorios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Editor Vendas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Cancel = true;
            }
            try
            {
                DsEditorVendasDetalhe.VerificaFluxoDocumental();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Editor Vendas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Cancel = true;
            }

            DsEditorVendasDetalhe.CondPagLinhas();

            // Se documento atual em modo de edição
            if (this.DocumentoVenda.EmModoEdicao == true)
            {

                if ((Strings.Trim(DocumentoVenda.Tipodoc) == "CNT" | Strings.Trim(DocumentoVenda.Tipodoc) == "EMB") && DocumentoVenda.CamposUtil["CDU_DocumentoCompraDestino"].Valor != null & DocumentoVenda.CamposUtil["CDU_DocumentoCompraDestino"].Valor.ToString() != "")
                {
                    switch (IDP.Verifica_Se_Documento_Compra_Criado_na_BD_Destino_NAO_Sofreu_Transformacao_ou_Copia_Linhas(DocumentoVenda))
                    {
                        case -1: // Se deu erro, tratado internamente na função.
                            {
                                Cancel = true; // Cancela a gravação (já não vai correr o evento EditorVendas_DepoisDeGravar) , sai e cancela a gravação.
                                return;
                            }

                        case 0: // Se encontrou linhas já transformadas (nos documentos de compra noutras bases de dados gerados a partir deste documento de venda), sai e cancela a gravação.
                            {
                                MessageBox.Show("Não é possível gravar o documento atual!" + Environment.NewLine + Environment.NewLine + "O primavera detectou documentos de compra noutras bases de dados, gerados de forma automática a partir deste documento de venda, que já sofreram alterações" + Strings.Chr(13) + "por 'Transformação de Documentos' ou por 'Cópia de Linhas'." + Strings.Chr(13) + Strings.Chr(13) + "Assim, não é possível gravar as alterações ao presente documento de venda " + this.DocumentoVenda.Tipodoc + "/" + this.DocumentoVenda.Serie + "/" + this.DocumentoVenda.NumDoc, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                Cancel = true; // Cancela a gravação (já não vai correr o evento EditorVendas_DepoisDeGravar)
                                return;
                            }
                        case 1: // Se NÂO encontrou linhas já transformadas (nos documentos de compra noutras bases de dados gerados a partir deste documento de venda)
                            {
                                break;
                            }
                    }
                }
            }
        }


        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(KeyCode, Shift, e);

            if (KeyCode == 113)
            {

                // Se o TipoDoc não está preenchido ou é inválido, ou cliente não está preenchido ou é inválido, não mostra formulário de detalhes.
                if (this.DocumentoVenda.Tipodoc.Length == 0 ||
                    !BSO.Vendas.TabVendas.Existe(this.DocumentoVenda.Tipodoc) ||
                    this.DocumentoVenda.Entidade.Length == 0 ||
                    !BSO.Base.Clientes.Existe(this.DocumentoVenda.Entidade))
                {
                    return;
                }



                DsEditorVendasDetalhe.SetContexto(this.DocumentoVenda, this.DocumentoTesouraria, this.DocumentoCBL, this.LinhaActual, this.Nome);

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmEditorVendasDetalheView));
                FrmEditorVendasDetalheView frm = result.Result;
                frm.IniciaBs(DsEditorVendasDetalhe);
                if (frm.DialogResult == DialogResult.OK)
                {
                    frm.ShowDialog();
                    DsEditorVendasDetalhe.GetContexto(this.DocumentoVenda, this.DocumentoTesouraria, this.DocumentoCBL, this.LinhaActual, this.Nome);
                }

                frm.Dispose();
                frm = default;
            }
        }

        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            // Carrega o contexto do documento de venda que está em edição no Primavera
            DsEditorVendasDetalhe.SetContexto(this.DocumentoVenda, this.DocumentoTesouraria, this.DocumentoCBL, LinhaActual, this.Nome);

            DsEditorVendasDetalhe.GetContexto(this.DocumentoVenda, this.DocumentoTesouraria, this.DocumentoCBL, this.LinhaActual, this.Nome);

            // -------------------------------------------------------
            // --- 2019.04.12 - VIMAPONTO - Gualter Costa - #1558  ------------------------------------------------------------------------------------------------------------------------
            // -------------------------------------------------------


            // Instancia a classe Intercambio_Documentos_Primavera   (para poder usar as funções que estão dentro da mesma)
            Intercambio_Documentos_Primavera IDP = new Intercambio_Documentos_Primavera();

            string Empresa_Origem;

            Intercambio_Documentos_Primavera.Modo_Edicao Modo_Edicao; // Enumeracao com os modos de edição

            // Verifica se o fornecedor indicado é um fornecedor do grupo Mundifios. Se for avança para a cópia de documentos para a base de dados de destino
            if (IDP.Verifica_Se_Fornecedor_Empresa_Grupo_Mundifios(this.DocumentoVenda) == true)
            {
                // Obtem a empresa de origem a partir do BSO do Primavera
                Empresa_Origem = BSO.Contexto.CodEmp;

                // Verifica se todos os códigos especificados nas listas do formulário F2, existem na base de dados de destino.
                // Se algum não existir não grava o documento na base de dados de destino e dá aviso.

                if (IDP.Verifica_Se_TODOS_Codigos_Listas_Existem_na_BD_Destino(this.DocumentoVenda) == false)
                    return;

                // Obtem o modo de edição do documento de origem (a partir  do objeto BSO do primavera)
                if (this.DocumentoVenda.EmModoEdicao == true)
                    Modo_Edicao = Intercambio_Documentos_Primavera.Modo_Edicao.EDITADO;
                else
                    Modo_Edicao = Intercambio_Documentos_Primavera.Modo_Edicao.NOVO;

                DataTable FornecedorGrupo = new DataTable();
                FornecedorGrupo = PriV100Api.BSO.DSO.ConsultaDataTable("SELECT CDU_NomeEmpresaGrupo FROM Fornecedores WHERE Fornecedor = '" + this.DocumentoVenda.CamposUtil["CDU_Fornecedor"].Valor + "' ");
                string NomeEmpresaGrupo = FornecedorGrupo.Rows[0]["CDU_NomeEmpresaGrupo"].ToString();

                if ((Strings.Trim(this.DocumentoVenda.Tipodoc) == "CNT" | Strings.Trim(this.DocumentoVenda.Tipodoc) == "EMB") & Strings.Trim(NomeEmpresaGrupo) != "")
                {
                    if (Strings.Trim(this.DocumentoVenda.Tipodoc) == "EMB" && IDP.Verifica_se_CNT_que_deu_origem_EMB_ja_foi_exportado_para_outra_BD(this.DocumentoVenda) == false)
                        return;

                    if (Strings.Trim(this.DocumentoVenda.Tipodoc) == "CNT" && Convert.IsDBNull(this.DocumentoVenda.CamposUtil["CDU_DocumentoCompraDestino"].Valor) && IDP.VerficaDocumentoTransformado(this.DocumentoVenda) == true)
                        return;

                    MsgBoxResult resposta = default(MsgBoxResult);

                    resposta = Interaction.MsgBox("Deseja Criar/Atualizar o documento na " + NomeEmpresaGrupo + " ?", Constants.vbYesNo, "Cópia de documentos");
                    if (resposta == MsgBoxResult.No)
                        return;
                }

                // Chama a função para cópia do documento de Venda na FILOPA para um doc de compra (noutra empresa do grupo mundifios)
                if (IDP.Copiar_BD_Origem_DocVenda_PARA_BD_Destino_DocCompra(Empresa_Origem, this.DocumentoVenda.Filial, this.DocumentoVenda.Tipodoc, this.DocumentoVenda.Serie, this.DocumentoVenda.NumDoc, Modo_Edicao) == false)
                {
                }
            }
        }

        public override void ClienteIdentificado(string Cliente, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ClienteIdentificado(Cliente, ref Cancel, e);

            // Carregar comissão do agente a partir da ficha da entidade (cliente.CDU_Agente).
            double ComissaoAgente = 0;
            ComissaoAgente = BSO.Base.Clientes.DaValorAtributo(Cliente, "CDU_Comissao");
            // Me.DocumentoVenda.IdDocOrigem = ""
            // sugere comissão do cliente para o cabeçalho

            if (Information.IsDBNull(ComissaoAgente))
                this.DocumentoVenda.CamposUtil["CDU_Comissao"].Valor = 0;
            else
                this.DocumentoVenda.CamposUtil["CDU_Comissao"].Valor = ComissaoAgente;
        }

        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);

            // Define o contexto
            DsEditorVendasDetalhe.SetContexto(this.DocumentoVenda, this.DocumentoTesouraria, this.DocumentoCBL, LinhaActual, this.Nome);
            // preenche só em modo de criação sem rasteabilidade
            DsEditorVendasDetalhe.PreencheCDUs(NumLinha);
            DsEditorVendasDetalhe.GetContexto(this.DocumentoVenda, this.DocumentoTesouraria, this.DocumentoCBL, this.LinhaActual, this.Nome);
        }

        public override void DepoisDeTransformar(ExtensibilityEventArgs e)
        {
            base.DepoisDeTransformar(e);
            // Define o contexto
            DsEditorVendasDetalhe.SetContexto(this.DocumentoVenda, this.DocumentoTesouraria, this.DocumentoCBL, LinhaActual, this.Nome);
            DsEditorVendasDetalhe.VerificaFluxosDocumentos();
            // So na Proforma
            DsEditorVendasDetalhe.InsereComissoes();
            DsEditorVendasDetalhe.GetContexto(this.DocumentoVenda, this.DocumentoTesouraria, this.DocumentoCBL, this.LinhaActual, this.Nome);
        }
        public override void DepoisDeDuplicar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeDuplicar(Filial, Tipo, Serie, NumDoc, e);

            DsEditorVendasDetalhe.SetContexto(this.DocumentoVenda, this.DocumentoTesouraria, this.DocumentoCBL, LinhaActual, this.Nome);

            // -------------------------------------------------------
            // --- 2019.06.14 - VIMAPONTO - Gualter Costa - #1558  ---
            // -------------------------------------------------------


            // Verifica se no novo documento que acabou ser criado por duplicação, tem preenchido os campos CDU_DocumentoCompraDestino e CDU_DocumentoVendaDestino.
            // Se sim limpa-os no novo documento.

            if (DocumentoVenda.CamposUtil["CDU_DocumentoCompraDestino"].Valor is null || Strings.Trim(this.DocumentoVenda.CamposUtil["CDU_DocumentoCompraDestino"].Valor.ToString()) != "")
                this.DocumentoVenda.CamposUtil["CDU_DocumentoCompraDestino"].Valor = "";


            if (DocumentoVenda.CamposUtil["CDU_DocumentoVendaDestino"].Valor is null || Strings.Trim(this.DocumentoVenda.CamposUtil["CDU_DocumentoVendaDestino"].Valor.ToString()) != "")
                this.DocumentoVenda.CamposUtil["CDU_DocumentoVendaDestino"].Valor = "";
        }

    }
}






