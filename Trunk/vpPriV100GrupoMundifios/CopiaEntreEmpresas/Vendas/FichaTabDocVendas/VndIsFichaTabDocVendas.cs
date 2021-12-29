using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using System.Windows.Forms;

namespace CopiaEntreEmpresas
{
    public class VndIsFichaTabDocVendas : FichaTabDocVendas
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);
            if (Module1.VerificaToken("CopiaEntreEmpresas") == 1)
            {
                if (!ValidacaoCamposUtilVenda())
                    Cancel = true;

                if (Cancel == false)
                {
                    if (!ValidacaoCamposUtilCompra())
                        Cancel = true;
                }
            }
        }

        private bool ValidacaoCamposUtilVenda()
        {
            string DocVendaDestino;
            string SerieVendaDestino;
            try
            {
                this.Documento.CamposUtil["CDU_TipoDocVendasDestino"].Valor = Strings.UCase(this.Documento.CamposUtil["CDU_TipoDocVendasDestino"].Valor + "");

                this.Documento.CamposUtil["CDU_SerieVendasDestino"].Valor = Strings.UCase(this.Documento.CamposUtil["CDU_SerieVendasDestino"].Valor + "");

                DocVendaDestino = Strings.UCase(this.Documento.CamposUtil["CDU_TipoDocVendasDestino"].Valor + "");

                if (Strings.Len(DocVendaDestino) > 0)
                {
                    if (Documento.TipoDocumento != BSO.Vendas.TabVendas.DaValorAtributo(Strings.UCase(Documento.CamposUtil["CDU_TipoDocVendasDestino"].Valor + ""), "TipoDocumento"))
                    {
                        MessageBox.Show("O tipo de documento de Venda configurado não é permitido.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }

                    SerieVendaDestino = Strings.UCase(Documento.CamposUtil["CDU_SerieVendasDestino"].Valor + "");

                    if (Strings.Len(SerieVendaDestino) > 0)
                        return true;
                    else
                    {
                        MessageBox.Show("Série não preenchida para o Documento de Venda " + DocVendaDestino + "." + "Campos de utilizador Doc. Venda incompletos", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return false;
                    }
                }
                else
                {
                    Documento.CamposUtil["CDU_SerieVendasDestino"].Valor = "";
                    return true;
                }
            }
            catch
            {
                MessageBox.Show("Erro nos campos de utilizador Doc. Venda", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool ValidacaoCamposUtilCompra()
        {
            try
            {
                string DocCompraDestino;
                string SerieCompraDestino;

                this.Documento.CamposUtil["CDU_TipoDocComprasDestino"].Valor = Strings.UCase(this.Documento.CamposUtil["CDU_TipoDocComprasDestino"].Valor + "");
                this.Documento.CamposUtil["CDU_SerieComprasDestino"].Valor = Strings.UCase(this.Documento.CamposUtil["CDU_SerieComprasDestino"].Valor + "");

                DocCompraDestino = Strings.UCase(this.Documento.CamposUtil["CDU_TipoDocComprasDestino"].Valor + "");

                if (Strings.Len(DocCompraDestino) > 0)
                {
                    if (Documento.TipoDocumento != BSO.Compras.TabCompras.DaValorAtributo(Strings.UCase(Documento.CamposUtil["CDU_TipoDocComprasDestino"].Valor + ""), "TipoDocumento"))
                    {
                        MessageBox.Show("O tipo de documento de Compra configurado não é permitido.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return false;
                    }

                    SerieCompraDestino = Strings.UCase(Documento.CamposUtil["CDU_SerieComprasDestino"].Valor + "");

                    if (Strings.Len(SerieCompraDestino) > 0)
                        return true;
                    else
                    {
                        MessageBox.Show("Série não preenchida para o Documento de Compra " + DocCompraDestino, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    Documento.CamposUtil["CDU_SerieComprasDestino"].Valor = "";
                    return true;
                }
            }
            catch
            {
                MessageBox.Show("Erro nos campos de utilizador Doc. Compra ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}