//using Generico;
//using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
//using Primavera.Extensibility.Sales.Editors;
//using Primavera.Platform.Collections;
//using VndBE100;

//namespace CondPagRQ
//{
//    public class VndIsFichaConverteVendas : FichaConverteVendas
//    {
//        public override void DepoisDeConverter(PrimaveraOrderedDictionary colDocumentosGerados, ExtensibilityEventArgs e)
//        {
//            base.DepoisDeConverter(colDocumentosGerados, e);

//            if (Module1.VerificaToken("CondPagRQ") == 1)
//            {
//                VndBEDocumentoVenda DocVendaRQRM = new VndBEDocumentoVenda();

//                foreach (var itdoc in colDocumentosGerados)
//                {
//                    DocVendaRQRM = BSO.Vendas.Documentos.Edita(itdoc.Filial, itdoc.Tipodoc, itdoc.Serie, itdoc.NumDoc);

//                    if (DocVendaRQRM.CondPag + "" != "")
//                    {
//                        if ((bool)BSO.Base.CondsPagamento.Edita(DocVendaRQRM.CondPag).CamposUtil["CDU_RQ"].Valor == true | (bool)BSO.Base.CondsPagamento.Edita(DocVendaRQRM.CondPag).CamposUtil["CDU_RM"].Valor == true)
//                        {
//                            DocVendaRQRM.DataVenc = Module1.NovaDataVencimento(DocVendaRQRM.DataDoc, DocVendaRQRM.CondPag, DocVendaRQRM.TipoEntidade, DocVendaRQRM.Entidade);
//                            DocVendaRQRM.CamposUtil["CDU_AlteradaDataVenc"].Valor = 1;
//                            BSO.Vendas.Documentos.Actualiza(DocVendaRQRM);
//                        }
//                    }
//                }
//            }
//        }
//    }
//}