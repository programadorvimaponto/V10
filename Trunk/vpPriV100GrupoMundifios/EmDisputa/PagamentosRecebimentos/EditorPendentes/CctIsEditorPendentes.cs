using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Constants.ExtensibilityService;
using Primavera.Extensibility.PayablesReceivables.Editors;
using StdBE100;

namespace EmDisputa
{
    public class CctIsEditorPendentes : EditorPendentes
    {
        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(KeyCode, Shift, e);

            // #############################################################################
            // # Faturas em Disputa, Formulário FrmEmDisputa (JFC 26/07/2018) #
            // #############################################################################
            // Crtl+D- EmDisputa

            if (Module1.VerificaToken("EmDisputa") == 1)
            {
                if (KeyCode == 68 && Shift == 2)
                {
                    Module1.dspModulo = "M";
                    Module1.dsptipoDoc = DocumentoPendente.Tipodoc;
                    Module1.dspSerie = DocumentoPendente.Serie;
                    Module1.dspNumDoc = DocumentoPendente.NumDocInt.ToString();

                    StdBELista listaPen;
                    bool Pen;
                    Pen = false;

                    listaPen = BSO.Consulta("select * from Pendentes p where p.TipoDoc='" + this.DocumentoPendente.Tipodoc + "' and p.NumDocInt='" + this.DocumentoPendente.NumDocInt + "' and p.Serie='" + this.DocumentoPendente.Serie + "'");

                    if ((listaPen.Vazia() == false))
                        Pen = true;

                    if (Pen)
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
}