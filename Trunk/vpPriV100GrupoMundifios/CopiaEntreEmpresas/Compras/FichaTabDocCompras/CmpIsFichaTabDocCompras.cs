using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Purchases.Editors;
using System.Windows.Forms;

namespace CopiaEntreEmpresas
{
    public class CmpIsFichaTabDocCompras : FichaTabDocCompras
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("CopiaEntreEmpresas") == 1)
            {
                if (!ValidacaoCamposUtilVenda())
                    Cancel = true;
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
                        MessageBox.Show("Série não preenchida para o Documento de Compra " + DocVendaDestino + "." + "Campos de utilizador Doc. Venda incompletos", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
    }
}