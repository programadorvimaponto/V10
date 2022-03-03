using Generico;
using InvBE100;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using System;
using System.Windows.Forms;

namespace FAC
{
    public class VndIsEditorVendas : EditorVendas
    {
        private bool FACAcabadaDeCriar;

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
            {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("FAC") == 1)
            {
                // JFC Valida de o documento FAC foi criado pela primeira vez.
                FACAcabadaDeCriar = false;
                if (this.DocumentoVenda.Tipodoc == "FAC")
                {
                    if (!BSO.Vendas.Documentos.Existe(DocumentoVenda.Filial, DocumentoVenda.Tipodoc, DocumentoVenda.Serie, DocumentoVenda.NumDoc))
                        FACAcabadaDeCriar = true;
                    else
                        FACAcabadaDeCriar = false;
                }
            }
        }

        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            if (Module1.VerificaToken("FAC") == 1)
            {
                // *******************************************************************************************************************************************
                // #### Criar Transferencia de Armazém Cliente (FAC) - JFC 08/07/2019 ####
                // *******************************************************************************************************************************************
                if (this.DocumentoVenda.Tipodoc == "FAC")
                {
                    if (FACAcabadaDeCriar)
                    {
                        StdBELista ListArm;
                        StdBELista ListLinhas;
                        ListArm = BSO.Consulta("SELECT distinct ln.Armazem FROM CabecDoc cd inner join LinhasDoc ln on ln.IdCabecDoc=cd.Id Where ln.Armazem is not null and cd.Id=" + "'" + this.DocumentoVenda.ID + "'");
                        ListArm.Inicio();

                        for (var j = 1; j <= ListArm.NumLinhas(); j++)
                        {
                            ListLinhas = BSO.Consulta("SELECT ln.Artigo, ln.Quantidade, ln.Armazem, ln.PrecUnit, ln.Lote, ln.Localizacao  FROM CabecDoc cd inner join LinhasDoc ln on ln.IdCabecDoc=cd.Id Where cd.Id=" + "'" + this.DocumentoVenda.ID + "' and ln.Armazem='" + ListArm.Valor(0) + "'");
                            CriaTransArmCliente(ListArm.Valor(0), this.DocumentoVenda.Serie, ListLinhas, this.DocumentoVenda.DataDoc);

                            ListArm.Seguinte();
                        }
                    }
                }
            }
        }

        private void CriaTransArmCliente(string TRA_Arm, string TRA_Serie, StdBELista TRA_Linhas, DateTime TRA_Data)
        {
            InvBEDocumentoTransf DocStk;

            string strDetalhe;

            try
            {
                BSO.IniciaTransaccao();
                DocStk = new InvBEDocumentoTransf();
                DocStk.ID = PSO.FuncoesGlobais.CriaGuid(true);
                DocStk.Tipodoc = "TRA";
                DocStk.Serie = TRA_Serie;
                //DocStocks.armazemorigem = TRA_Arm;

                // Preenche a restante informação no documento
                BSO.Inventario.Transferencias.PreencheDadosRelacionados(DocStk);
                DocStk.Data = TRA_Data;
                TRA_Linhas.Inicio();


                //'Linha nº1 de texto
                InvBELinhaOrigemTransf LinhaStk = new InvBELinhaOrigemTransf();
                LinhaStk.IdLinha = Guid.NewGuid().ToString();
                LinhaStk.TipoLinha = ConstantesPrimavera100.Documentos.TipoLinComentario;
                LinhaStk.Descricao = "";
                LinhaStk.Lote = "<L01>";
                DocStk.LinhasOrigem.Insere(LinhaStk);

                //'Linha nº2 de texto
                LinhaStk = new InvBELinhaOrigemTransf();
                LinhaStk.IdLinha = Guid.NewGuid().ToString();
                LinhaStk.TipoLinha = ConstantesPrimavera100.Documentos.TipoLinComentario;
                LinhaStk.Descricao = "";
                LinhaStk.Lote = "<L01>";
                DocStk.LinhasOrigem.Insere(LinhaStk);



                for (int i = 1; i <= TRA_Linhas.NumLinhas(); i++)
                {
                    BSO.Inventario.Transferencias.AdicionaLinhaOrigem(DocStk, TRA_Linhas.Valor("Artigo"), TRA_Arm, TRA_Linhas.Valor("Localizacao"), "DISP", TRA_Linhas.Valor("Quantidade"), TRA_Linhas.Valor("Lote"));

                    LinhaStk = DocStk.LinhasOrigem.GetEdita(DocStk.LinhasOrigem.NumItens);
                    LinhaStk.DataStock = DocumentoVenda.DataDoc.Date.Add(DocumentoVenda.DataHoraCarga.TimeOfDay);

                    InvBELinhaDestinoTransf linhaStkDst = LinhaStk.LinhasDestino.GetEdita(1);
                    linhaStkDst.Armazem = "FC";
                    linhaStkDst.Localizacao = "FC";
                    linhaStkDst.DataStock = DocumentoVenda.DataDoc.Date.Add(DocumentoVenda.DataHoraCarga.TimeOfDay);


                    TRA_Linhas.Seguinte();

                    //InvBELinhaDestinoTransf linhatransf = new InvBELinhaDestinoTransf();
                    //linhatransf.IdLinha = Guid.NewGuid().ToString();
                    //linhatransf.IdCabecTransferencias = DocStk.ID;
                    //linhatransf.Artigo = TRA_Linhas.Valor("Artigo");
                    //linhatransf.Quantidade = TRA_Linhas.Valor("Quantidade");
                    //linhatransf.Armazem = TRA_Arm;
                    //linhatransf.PrecUnit = TRA_Linhas.Valor("PrecUnit");
                    //linhatransf.Lote = TRA_Linhas.Valor("Lote");
                    //linhatransf.Localizacao = TRA_Linhas.Valor("Localizacao");
                    //DocStk.LinhasOrigem.GetEdita(DocStk.LinhasOrigem.NumItens).LinhasDestino.Insere(linhatransf);
                    ////ln.Artigo, ln.Quantidade, ln.Armazem, ln.PrecUni, ln.Lote, ln.Localizacao;
                    ////BSO.Inventario.Transferencias.AdicionaLinhaOrigem(DocStocks, TRA_Linhas.Valor(0), , TRA_Linhas.Valor(1), "FC", TRA_Linhas.Valor(3), , TRA_Linhas.Valor(4), "FC");
                    ////BSO.Comercial.Stocks.AdicionaLinha DocStocks, Artigo, EntradaSaida, Quantidade, Armazem, PrecUnit, Desconto, Lote, Localizacao, QntVA, QntdVb, QntVc;
                }

                // ----------------------------------
                // GRAVAÇÃO DO DOCUMENTO
                string erros = "";
                BSO.Inventario.Transferencias.Actualiza(DocStk, ref erros);
                // GRAVAÇÃO DO DOCUMENTO
                // ----------------------------------

                // Termina a transação
                BSO.TerminaTransaccao();
                // ----------------------------------
                // MENSAGEM FINAL

                strDetalhe = Constants.vbNullString;

                strDetalhe = strDetalhe + "Documento de Stock: " + DocStk.Tipodoc + " Nº " + System.Convert.ToString(DocStk.NumDoc) + "/" + DocStk.Serie + Constants.vbCrLf;

                MessageBox.Show("Documento gerado com sucesso.\ninformações: " + strDetalhe, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // MENSAGEM FINAL
                // ----------------------------------

                DocStk = null;
            }
            catch (Exception ex)
            {
                BSO.DesfazTransaccao();

                DocStk = new InvBEDocumentoTransf();
                MessageBox.Show("Erro ao gerar o documento. Exceção: " + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}