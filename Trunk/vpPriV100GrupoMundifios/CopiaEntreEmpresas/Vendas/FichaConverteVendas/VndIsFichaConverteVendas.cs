using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using Primavera.Platform.Collections;
using System.Collections;
using VndBE100;

namespace CopiaEntreEmpresas
{
    public class VndIsFichaConverteVendas : FichaConverteVendas
    {
        public override void DepoisDeConverter(PrimaveraOrderedDictionary colDocumentosGerados, ExtensibilityEventArgs e)
        {
            base.DepoisDeConverter(colDocumentosGerados, e);

            if (Module1.VerificaToken("CondPagRQ") == 1)
            {
                VndBEDocumentoVenda DocVendaRQRM = new VndBEDocumentoVenda();

                foreach (DictionaryEntry itdoc in colDocumentosGerados)
                {
                    BasBE100.BasBETiposGcp.TPDocumentos docs = (BasBE100.BasBETiposGcp.TPDocumentos)itdoc.Value;


                    DocVendaRQRM = BSO.Vendas.Documentos.Edita(docs.Filail, docs.Tipodoc, docs.Serie, docs.NumDoc);

                    if (DocVendaRQRM.Tipodoc == "FG")
                    {
                        DocVendaRQRM.CargaDescarga.MoradaEntrega = BSO.Base.Clientes.Edita(DocVendaRQRM.Entidade).Morada;
                        DocVendaRQRM.CargaDescarga.Morada2Entrega = BSO.Base.Clientes.Edita(DocVendaRQRM.Entidade).Morada2;
                        DocVendaRQRM.CargaDescarga.LocalidadeEntrega = BSO.Base.Clientes.Edita(DocVendaRQRM.Entidade).Localidade;
                        DocVendaRQRM.CargaDescarga.DistritoEntrega = BSO.Base.Clientes.Edita(DocVendaRQRM.Entidade).Distrito;
                        DocVendaRQRM.CargaDescarga.CodPostalEntrega = BSO.Base.Clientes.Edita(DocVendaRQRM.Entidade).CodigoPostal;
                        DocVendaRQRM.CargaDescarga.CodPostalLocalidadeEntrega = BSO.Base.Clientes.Edita(DocVendaRQRM.Entidade).LocalidadeCodigoPostal;
                        DocVendaRQRM.CargaDescarga.PaisEntrega = BSO.Base.Clientes.Edita(DocVendaRQRM.Entidade).Pais;
                        BSO.Vendas.Documentos.Actualiza(DocVendaRQRM);
                    }
                }
            }
        }
    }
}