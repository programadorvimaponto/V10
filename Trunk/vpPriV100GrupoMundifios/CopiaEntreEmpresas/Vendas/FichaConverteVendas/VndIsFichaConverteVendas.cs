using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using Primavera.Platform.Collections;

namespace CopiaEntreEmpresas
{
    public class VndIsFichaConverteVendas : FichaConverteVendas
    {
        public override void DepoisDeConverter(PrimaveraOrderedDictionary colDocumentosGerados, ExtensibilityEventArgs e)
        {
            base.DepoisDeConverter(colDocumentosGerados, e);

            if (Module1.VerificaToken("CopiaEntreEmpresas") == 1)
            {
                VndBE100.VndBEDocumentoVenda DocVendaRQRM = new VndBE100.VndBEDocumentoVenda();

                foreach (object itdoc in colDocumentosGerados)
                {
                    // JFC - 02/09/2020 - No caso de faturas entre empresas de grupo colocar a morada de descarga da empresa. Pedido de Ana Castro.
                    // Ao inserir as faturas na control-union apareciam locais de descarga de clientes.
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