//using Generico;
//using InvBE100;
//using Microsoft.VisualBasic;
//using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
//using Primavera.Extensibility.Sales.Editors;
//using StdBE100;
//using System;
//using System.Windows.Forms;

//namespace FAC
//{
//    public class VndIsEditorVendas : EditorVendas
//    {
//        private bool FACAcabadaDeCriar;

//        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
//        {
//            base.AntesDeGravar(ref Cancel, e);

//            if (Module1.VerificaToken("FAC") == 1)
//            {
//                // JFC Valida de o documento FAC foi criado pela primeira vez.
//                FACAcabadaDeCriar = false;
//                if (this.DocumentoVenda.Tipodoc == "FAC")
//                {
//                    StdBELista ListFAC;

//                    ListFAC = BSO.Consulta("SELECT distinct ln.Artigo FROM CabecDoc cd inner join LinhasDoc ln on ln.IdCabecDoc=cd.Id Where cd.Id=" + "'" + this.DocumentoVenda.ID + "'");

//                    if (ListFAC.Vazia())
//                        FACAcabadaDeCriar = true;
//                    else
//                        FACAcabadaDeCriar = false;
//                }
//            }
//        }

//        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
//        {
//            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

//            if (Module1.VerificaToken("FAC") == 1)
//            {
//                // *******************************************************************************************************************************************
//                // #### Criar Transferencia de Armazém Cliente (FAC) - JFC 08/07/2019 ####
//                // *******************************************************************************************************************************************
//                if (this.DocumentoVenda.Tipodoc == "FAC")
//                {
//                    if (FACAcabadaDeCriar)
//                    {
//                        StdBELista ListArm;
//                        StdBELista ListLinhas;
//                        ListArm = BSO.Consulta("SELECT distinct ln.Armazem FROM CabecDoc cd inner join LinhasDoc ln on ln.IdCabecDoc=cd.Id Where ln.Armazem is not null and cd.Id=" + "'" + this.DocumentoVenda.ID + "'");
//                        ListArm.Inicio();

//                        for (var j = 1; j <= ListArm.NumLinhas(); j++)
//                        {
//                            ListLinhas = BSO.Consulta("SELECT ln.Artigo, ln.Quantidade, ln.Armazem, ln.PrecUnit, ln.Lote, ln.Localizacao  FROM CabecDoc cd inner join LinhasDoc ln on ln.IdCabecDoc=cd.Id Where cd.Id=" + "'" + this.DocumentoVenda.ID + "' and ln.Armazem='" + ListArm.Valor(0) + "'");
//                            CriaTransArmCliente(ListArm.Valor(0), this.DocumentoVenda.Serie, ListLinhas, this.DocumentoVenda.DataDoc);

//                            ListArm.Seguinte();
//                        }
//                    }
//                }
//            }
//        }

//        private void CriaTransArmCliente(string TRA_Arm, string TRA_Serie, StdBELista TRA_Linhas, DateTime TRA_Data)
//        {
//            InvBEDocumentoTransf DocStocks;

//            string strDetalhe;

//            try
//            {
//                BSO.IniciaTransaccao();
//                DocStocks = new InvBEDocumentoTransf();
//                DocStocks.ID = PSO.FuncoesGlobais.CriaGuid(true);
//                DocStocks.Tipodoc = "TRA";
//                DocStocks.Serie = TRA_Serie;
//                DocStocks.armazemorigem = TRA_Arm;

//                // Preenche a restante informação no documento
//                BSO.Comercial.Stocks.PreencheDadosRelacionados(DocStocks);
//                DocStocks.Data = TRA_Data;
//                TRA_Linhas.Inicio();

//                for (int i = 1; i <= TRA_Linhas.NumLinhas(); i++)
//                {
//                    //ln.Artigo, ln.Quantidade, ln.Armazem, ln.PrecUni, ln.Lote, ln.Localizacao;
//                    BSO.Comercial.Stocks.AdicionaLinha(DocStocks, TRA_Linhas.Valor(0), , TRA_Linhas.Valor(1), "FC", TRA_Linhas.Valor(3), , TRA_Linhas.Valor(4), "FC");
//                    //BSO.Comercial.Stocks.AdicionaLinha DocStocks, Artigo, EntradaSaida, Quantidade, Armazem, PrecUnit, Desconto, Lote, Localizacao, QntVA, QntdVb, QntVc;
//                    TRA_Linhas.Seguinte();
//                }

//                // ----------------------------------
//                // GRAVAÇÃO DO DOCUMENTO
//                BSO.Comercial.Stocks.actualiza(DocStocks);
//                // GRAVAÇÃO DO DOCUMENTO
//                // ----------------------------------

//                // Termina a transação
//                BSO.TerminaTransaccao();
//                // ----------------------------------
//                // MENSAGEM FINAL

//                strDetalhe = Constants.vbNullString;

//                strDetalhe = strDetalhe + "Documento de Stock: " + DocStocks.Tipodoc + " Nº " + System.Convert.ToString(DocStocks.NumDoc) + "/" + DocStocks.Serie + Constants.vbCrLf;

//                MessageBox.Show("Documento gerado com sucesso.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

//                // MENSAGEM FINAL
//                // ----------------------------------

//                DocStocks = null;
//            }
//            catch
//            {
//                BSO.DesfazTransaccao();

//                DocStocks = new InvBEDocumentoTransf();
//                MessageBox.Show("Erro ao gerar o documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//    }
//}