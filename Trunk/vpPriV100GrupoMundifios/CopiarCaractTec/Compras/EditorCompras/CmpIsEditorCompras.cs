using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Purchases.Editors;
using System.Windows.Forms;

namespace CopiarCaractTec
{
    public class CmpIsEditorCompras : EditorCompras
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            
            if (Module1.VerificaToken("CopiarCaractTec") == 1)
            {
                // If Not CopiarCaractTec Then Cancel = True
                if (!Mdi_CopiaCaracteristicasTecnicas.CopiarCaractTec(BSO.Contexto.CodEmp, this.DocumentoCompra))
                {
                    if (MessageBox.Show("Não foi possível realizar a cópia de características! \n Deseja mesmo assim gravar o documento?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)

                        Cancel = true;
                }
            }
        }

    }
}