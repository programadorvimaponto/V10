using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.Base.Editors;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using System.Windows.Forms;

namespace CondPagRQ
{
    public class BasIsFichaCondsPag : FichaCondsPag
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("CondPagRQ") == 1)
            {
                if ((bool)this.CondPag.CamposUtil["CDU_RQ"].Valor == true & (bool)this.CondPag.CamposUtil["CDU_RM"].Valor == true)
                {
                    MessageBox.Show("N�o pode na mesma condi��o de pagamento estar escolhida a op��o resumo quinzenal e resumo mensal." + Strings.Chr(13) + Strings.Chr(13) + "Deve apenas seleccionar uma das op��es.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cancel = true;
                }
            }
        }
    }
}