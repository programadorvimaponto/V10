using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Constants.ExtensibilityService;
using Primavera.Extensibility.PayablesReceivables.Editors;
using StdBE100;

namespace EmDisputa
{
    public class CctIsEditorCCPendentesGrelha : EditorCCPendentesGrelha
    {
        public override void PendenteSeleccionado(int NumLinha, StdBECampos objBeCampos, ExtensibilityEventArgs e)
        {
            base.PendenteSeleccionado(NumLinha, objBeCampos, e);
            if (Module1.VerificaToken("EmDisputa") == 1)
            {
                Module1.dsptipoDoc = objBeCampos[4].Valor.ToString();
                Module1.dspSerie = objBeCampos[6].Valor.ToString();
                Module1.dspNumDoc = objBeCampos[8].Valor.ToString();
            }
        }

        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(KeyCode, Shift, e);

            // #############################################################################
            // # Faturas em Disputa, Formulário FrmEmDisputa (JFC 26/07/2018) #
            // #############################################################################
            // Crtl+D- EmDisputa

            if (Module1.VerificaToken("EmDisputa") == 1)
            {
                if (KeyCode == 68)
                {
                    ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmEmDisputaView));

                    if (result.ResultCode == ExtensibilityResultCode.Ok)
                    {
                        FrmEmDisputaView frm = result.Result;
                        frm.ShowDialog();
                    }
                }
            }
        }
    }
}