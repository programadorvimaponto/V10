using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.Attributes;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using System.Windows.Forms;

namespace InfoFioValidaLinha
{
    public class VndIsEditorVendas : EditorVendas
    {
        private int MsgErro;
        private StdBELista ListaPTB;
        private StdBELista ListaPCU;
        private StdBELista ListaPCU_ArtigoArmazem;
        private string SqlStringPCU;
        private StdBELista ListaFichaLAB;
        private string SqlStringFichaLAB;
        private StdBELista ListaArtLoteRestricoes;
        private string SqlStringArtLoteRestricoes;
        private StdBELista ListaCliLevouArtLote;
        private string SqlStringCliLevouArtLote;

        [ContextSyncManagement(ContextSyncManagement.Manual)]
        public override void ValidaLinha(int NumLinha, ExtensibilityEventArgs e)
        {
            base.RefreshContext();
            base.ValidaLinha(NumLinha, e);

            if (Module1.VerificaToken("InfoFioValidaLinha") == 1)
            {
                if (BSO.Inventario.ArtigosLotes.Existe(this.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo, this.DocumentoVenda.Linhas.GetEdita(NumLinha).Lote) == true)
                {
                    // Comentada dia 28/01/2014 - permitir Angelo alterar Tipo Qualidade no editor de vendas diferente do Tipo Qualidade na ficha do artigo/lote
                    this.DocumentoVenda.Linhas.GetEdita(NumLinha).CamposUtil["CDU_TipoQualidade"].Valor = BSO.Inventario.ArtigosLotes.DaValorAtributo(this.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo, this.DocumentoVenda.Linhas.GetEdita(NumLinha).Lote, "CDU_TipoQualidade");



                    Module1.EntidadeVerifica = this.DocumentoVenda.Entidade;
                    Module1.ArtigoVerifica = this.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo;
                    Module1.LoteVerifica = this.DocumentoVenda.Linhas.GetEdita(NumLinha).Lote;
                    Module1.PrecoUnitVerifica = this.DocumentoVenda.Linhas.GetEdita(NumLinha).PrecUnit;
                    Module1.ArmazemVerifica = this.DocumentoVenda.Linhas.GetEdita(NumLinha).Armazem;

                    VerificaPrecoAbaixoCustoUltimo();

                    VerificaExisteFichaLaboratorio();

                    VerificaExisteRestricoesArtigoLote();

                    VerificaClenteLevouArtLote();
                }

                if (this.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo + "" != "")
                {
                    if (this.DocumentoVenda.Linhas.GetEdita(NumLinha).Lote + "" != "" & this.DocumentoVenda.Linhas.GetEdita(NumLinha).Lote + "" != "<L01>")
                    {
                        if (BSO.Inventario.ArtigosLotes.Existe(this.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo, this.DocumentoVenda.Linhas.GetEdita(NumLinha).Lote) == false)
                            MsgErro = (int)MessageBox.Show("Atenção o Lote não existe!" + Strings.Chr(13) + "Artigo: " + this.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo + " Lote: " + this.DocumentoVenda.Linhas.GetEdita(NumLinha).Lote, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // ######################################################################
                // ##Verifica Alteração de Comissão  - Pedido D. Goreti -25/03/2019 JFC##
                // ######################################################################
                if (this.DocumentoVenda.Tipodoc == "ECL" & this.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo + "" != "")
                {
                    StdBELista sqlComissao;

                    sqlComissao = BSO.Consulta("select top 1 ln.NumLinha, ln.Artigo, ln.Lote, ln.Comissao from LinhasDoc ln where ln.Id='" + this.DocumentoVenda.Linhas.GetEdita(NumLinha).IdLinha + "'");
                    if (sqlComissao.Vazia() == false)
                    {
                        sqlComissao.Inicio();

                        if (sqlComissao.Valor("Comissao") != this.DocumentoVenda.Linhas.GetEdita(NumLinha).Comissao)
                            MessageBox.Show("ATENÇÃO COMISSÃO ALTERADA!" + Strings.Chr(13) + Strings.Chr(13) + "Linha: " + NumLinha + Strings.Chr(13) + "" + " Artigo: " + this.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo + "" + " Lote: " + this.DocumentoVenda.Linhas.GetEdita(NumLinha).Lote + "" + " Comissao: " + this.DocumentoVenda.Linhas.GetEdita(NumLinha).Comissao + "" + Strings.Chr(13) + Strings.Chr(13) + "" + "Antes:" + Strings.Chr(13) + "" + " Artigo: " + sqlComissao.Valor("Artigo") + "" + " Lote: " + sqlComissao.Valor("Lote") + "" + " Comissao: " + sqlComissao.Valor("Comissao") + "", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            base.CommitContext();
        }

        private void VerificaPrecoAbaixoCustoUltimo()
        {
            // SqlStringPCU = "SELECT     ISNULL(ROUND " _
            // & "((SELECT     TOP (1) CASE moeda WHEN 'EUR' THEN LinhasCompras.PrecUnit ELSE isnull(CASE isnull ((SELECT     TOP 1 CDU_CambioFixo FROM TDU_CabecCustosEncomendas WHERE     (CDU_Artigo = '" & ArtigoVerifica & "') AND (CDU_Lote = '" & LoteVerifica & "') AND CDU_TipoDoc = 'ECF'), (SELECT     TOP 1 CDU_CambioFixo FROM TDU_CabecCustosEncomendas WHERE      (CDU_Artigo = '" & ArtigoVerifica & "') AND (CDU_Lote = '" & LoteVerifica & "') AND CDU_TipoDoc = 'CNT')) WHEN 0 THEN LinhasCompras.PrecUnit / (SELECT     TOP 1 Compra FROM MoedasHistorico WHERE (MoedasHistorico.moeda = CabecCompras.moeda) ORDER BY DataCambio DESC) ELSE LinhasCompras.PrecUnit / isnull " _
            // & "((SELECT     TOP 1 CDU_CambioFixo FROM TDU_CabecCustosEncomendas WHERE     (CDU_Artigo = '" & ArtigoVerifica & "') AND (CDU_Lote = '" & LoteVerifica & "') AND CDU_TipoDoc = 'ECF'), (SELECT     TOP 1 CDU_CambioFixo FROM TDU_CabecCustosEncomendas WHERE      (CDU_Artigo = '" & ArtigoVerifica & "') AND (CDU_Lote = '" & LoteVerifica & "') AND CDU_TipoDoc = 'CNT')) END, LinhasCompras.PrecUnit / (SELECT     TOP 1 Compra FROM MoedasHistorico WHERE (MoedasHistorico.moeda = CabecCompras.moeda) ORDER BY DataCambio DESC)) END AS Expr1 FROM         dbo.LinhasCompras INNER JOIN dbo.CabecCompras ON dbo.LinhasCompras.IdCabecCompras = dbo.CabecCompras.Id INNER JOIN dbo.DocumentosCompra ON dbo.CabecCompras.TipoDoc = dbo.DocumentosCompra.Documento INNER JOIN dbo.CabecComprasStatus ON dbo.CabecCompras.Id = dbo.CabecComprasStatus.IdCabecCompras " _
            // & "WHERE     (dbo.DocumentosCompra.TipoDocumento = 2) AND (dbo.LinhasCompras.Artigo = '" & ArtigoVerifica & "') AND (dbo.LinhasCompras.Lote = '" & LoteVerifica & "') AND (dbo.CabecComprasStatus.Anulado = 0) AND (dbo.CabecComprasStatus.Fechado = 0) ORDER BY dbo.CabecCompras.TipoDoc DESC, dbo.CabecCompras.NumDoc DESC) + ISNULL ((SELECT     TOP (1) CDU_TotalCusto FROM dbo.TDU_CabecCustosEncomendas WHERE     (CDU_Artigo = '" & ArtigoVerifica & "') AND (CDU_Lote = '" & LoteVerifica & "') AND (CDU_TipoDoc = 'ECF')), (SELECT     TOP (1) TDU_CabecCustosEncomendas_1.CDU_TotalCusto " _
            // & "FROM          dbo.TDU_CabecCustosEncomendas AS TDU_CabecCustosEncomendas_1 INNER JOIN dbo.CabecCompras AS CabecCompras_1 ON TDU_CabecCustosEncomendas_1.CDU_NumDoc = CabecCompras_1.NumDoc AND TDU_CabecCustosEncomendas_1.CDU_TipoDoc = CabecCompras_1.TipoDoc AND TDU_CabecCustosEncomendas_1.CDU_Serie = CabecCompras_1.Serie INNER JOIN dbo.CabecComprasStatus AS CabecComprasStatus_1 ON CabecCompras_1.Id = CabecComprasStatus_1.IdCabecCompras " _
            // & "WHERE      (TDU_CabecCustosEncomendas_1.CDU_Artigo = '" & ArtigoVerifica & "') AND (TDU_CabecCustosEncomendas_1.CDU_Lote = '" & LoteVerifica & "') AND (TDU_CabecCustosEncomendas_1.CDU_TipoDoc = 'CNT') AND (CabecComprasStatus_1.Fechado = 0) AND (CabecComprasStatus_1.Anulado = 0) ORDER BY CabecCompras_1.NumDoc DESC)), 3), (SELECT     TOP (1) dbo.LinhasSTK.CDU_PrCusto FROM          dbo.LinhasSTK INNER JOIN dbo.DocumentosStk ON dbo.LinhasSTK.TipoDoc = dbo.DocumentosStk.Documento WHERE      (dbo.LinhasSTK.Artigo = '" & ArtigoVerifica & "') AND (dbo.LinhasSTK.Lote = '" & LoteVerifica & "') AND (dbo.DocumentosStk.TipoDocumento = 0) OR (dbo.LinhasSTK.Artigo = '" & ArtigoVerifica & "') AND (dbo.LinhasSTK.Lote = '" & LoteVerifica & "') AND (dbo.DocumentosStk.TipoDocumento = 1) ORDER BY dbo.LinhasSTK.Data DESC)) AS PCusto"

            SqlStringPCU = "select dbo.VMP_IEXF_DaPrecoCusto ('" + Module1.ArtigoVerifica + "','" + Module1.LoteVerifica + "','3') as 'PCusto'";

            ListaPCU = BSO.Consulta(SqlStringPCU);

            if (ListaPCU.Vazia() == false)
            {
                ListaPCU.Inicio();

                if (Module1.PrecoUnitVerifica <= ListaPCU.Valor("PCusto"))
                {
                    if (ListaPCU.Valor("PCusto") == "")
                    {
                        // Eduardo Sampaio 03/03/2017
                        // Por indicação do Rui Fernandes,faço como está no IE. Atualizo com o valor da tabela ArtigoArmazem caso a query acima nao retorne valores
                        ListaPCU_ArtigoArmazem = BSO.Consulta("SELECT PCMedio as PCusto  FROM ArtigoArmazem WHERE Artigo = '" + Module1.ArtigoVerifica + "' AND Armazem = '" + Module1.ArmazemVerifica + "' AND  Lote = '" + Module1.LoteVerifica + "'");
                        if (ListaPCU_ArtigoArmazem.Vazia() == false)
                        {
                            ListaPCU_ArtigoArmazem.Inicio();

                            if (Module1.PrecoUnitVerifica <= ListaPCU_ArtigoArmazem.Valor("PCusto"))
                            {
                                MsgErro = (int)MessageBox.Show("O Artigo: " + Module1.ArtigoVerifica + ", Lote: " + Module1.LoteVerifica + " está a ser vendido abaixo do preço de custo: " + ListaPCU_ArtigoArmazem.Valor("PCusto") + " €", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            else
                                return;
                        }
                    }

                    MsgErro = (int)MessageBox.Show("O Artigo: " + Module1.ArtigoVerifica + ", Lote: " + Module1.LoteVerifica + " está a ser vendido abaixo do preço de custo: " + ListaPCU.Valor("PCusto") + " €", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void VerificaExisteFichaLaboratorio()
        {
            SqlStringFichaLAB = "SELECT * FROM [TDU_LABORATORIOLOTE] "
                            + "WHERE CDU_RSSitFinFio!='SPECS' and cdu_codARTIGO = '" + Module1.ArtigoVerifica + "' and cdu_loteart = '" + Module1.LoteVerifica + "'";

            ListaFichaLAB = BSO.Consulta(SqlStringFichaLAB);

            if (ListaFichaLAB.Vazia() == true)

                MsgErro = (int)MessageBox.Show("O Artigo: " + Module1.ArtigoVerifica + ", Lote: " + Module1.LoteVerifica + " não possui características técnicas", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void VerificaExisteRestricoesArtigoLote()
        {
            SqlStringArtLoteRestricoes = "SELECT * FROM [TDU_LABORATORIOLOTE] "
                            + "WHERE cdu_codARTIGO = '" + Module1.ArtigoVerifica + "' and cdu_loteart = '" + Module1.LoteVerifica + "'";

            ListaArtLoteRestricoes = BSO.Consulta(SqlStringArtLoteRestricoes);

            if (ListaArtLoteRestricoes.Vazia() == false)
            {
                ListaArtLoteRestricoes.Inicio();

                if (ListaArtLoteRestricoes.Valor("CDU_RSSitFinFioObs") + "" != "")

                    MsgErro = (int)MessageBox.Show("O Artigo: " + Module1.ArtigoVerifica + ", Lote: " + Module1.LoteVerifica + " tem restrições: " + Strings.Chr(13) + Strings.Chr(13) + ListaArtLoteRestricoes.Valor("CDU_RSSitFinFioObs"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void VerificaClenteLevouArtLote()
        {
            SqlStringCliLevouArtLote = "SELECT dbo.CabecDoc.Entidade, dbo.LinhasDoc.Artigo, dbo.LinhasDoc.Lote "
                                + "FROM dbo.CabecDoc INNER JOIN dbo.LinhasDoc ON dbo.CabecDoc.Id = dbo.LinhasDoc.IdCabecDoc "
                                + "WHERE (dbo.LinhasDoc.Artigo = '" + Module1.ArtigoVerifica + "') AND (dbo.LinhasDoc.Lote = '" + Module1.LoteVerifica + "') AND (dbo.CabecDoc.Entidade = '" + Module1.EntidadeVerifica + "')";

            ListaCliLevouArtLote = BSO.Consulta(SqlStringCliLevouArtLote);

            if (ListaCliLevouArtLote.Vazia() == true)

                MsgErro = (int)MessageBox.Show("É a primeira vez que o Cliente " + BSO.Base.Clientes.Edita(Module1.EntidadeVerifica).Nome + " leva o Artigo: " + Module1.ArtigoVerifica + ", do Lote: " + Module1.LoteVerifica, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}