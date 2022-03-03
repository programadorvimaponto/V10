using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using Primavera.Platform.Collections;
using System.Collections;
using VndBE100;

namespace condpagrq
{
    public class vndisfichaconvertevendas : FichaConverteVendas
    {
        public override void DepoisDeConverter(PrimaveraOrderedDictionary colDocumentosGerados, ExtensibilityEventArgs e)
        {
            base.DepoisDeConverter(colDocumentosGerados, e);

            if (Module1.VerificaToken("CondPagRQ") == 1)
            {
                VndBEDocumentoVenda DocVendaRqRm = new VndBEDocumentoVenda();

                foreach (DictionaryEntry itdoc in colDocumentosGerados)
                {
                    BasBE100.BasBETiposGcp.TPDocumentos docs = (BasBE100.BasBETiposGcp.TPDocumentos)itdoc.Value;


                    DocVendaRqRm = BSO.Vendas.Documentos.Edita(docs.Filail,docs.Tipodoc,docs.Serie,docs.NumDoc);

                    if (DocVendaRqRm.CondPag + "" != "")
                    {
                        if ((bool)BSO.Base.CondsPagamento.Edita(DocVendaRqRm.CondPag).CamposUtil["CDU_RQ"].Valor == true | (bool)BSO.Base.CondsPagamento.Edita(DocVendaRqRm.CondPag).CamposUtil["CDU_RM"].Valor == true)
                        {
                            DocVendaRqRm.DataVenc = Module1.NovaDataVencimento(DocVendaRqRm.DataDoc, DocVendaRqRm.CondPag, DocVendaRqRm.TipoEntidade, DocVendaRqRm.Entidade);
                            DocVendaRqRm.CamposUtil["CDU_AlteradaDataVenc"].Valor = 1;
                            BSO.Vendas.Documentos.Actualiza(DocVendaRqRm);
                        }
                    }
                }
            }
        }
    }
}