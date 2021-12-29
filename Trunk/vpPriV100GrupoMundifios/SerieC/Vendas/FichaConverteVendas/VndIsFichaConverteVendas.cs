using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using System.Windows.Forms;

namespace SerieC
{
    public class VndIsFichaConverteVendas : FichaConverteVendas
    {
        public override void AntesDeConverter(int NumDoc, string Tipodoc, string Serie, string Filial, string TipodocDestino, string SerieDestino, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeConverter(NumDoc, Tipodoc, Serie, Filial, TipodocDestino, SerieDestino, ref Cancel, e);

            if (Module1.VerificaToken("SerieC") == 1)
            {
                if (Strings.Right(Serie, 1) == "C" & Strings.Right(SerieDestino, 1) != "C")
                {
                    MessageBox.Show("Não é permitida conversões de documentos da serie C para outras series." + Strings.Chr(13) + Strings.Chr(13) + "Está a converter da serie " + Serie + " para a serie " + SerieDestino + Strings.Chr(13) + "Tera de convertar para uma serie destino " + Serie, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                }
            }
        }
    }
}