using Generico;
using Primavera.Extensibility.Base.Editors;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Constants.ExtensibilityService;
using System.Windows.Forms;

namespace FornecedoresCertificados
{
    public class BasIsFichaFornecedor : FichaFornecedores
    {
        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(KeyCode, Shift, e);

            if (Module1.VerificaToken("FornecedoresCertificados") == 1)
            {
                //
                // Crtl + R JFC 04/11/2019
                if (KeyCode == 82 & this.Fornecedor.Inactivo == false)
                {
                    Module1.certEntidade = this.Fornecedor.Fornecedor;

                    ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmFornecedoresCertsView));

                    if (result.ResultCode == ExtensibilityResultCode.Ok)
                    {
                        FrmFornecedoresCertsView frm = result.Result;
                        frm.ShowDialog();
                    }
                }

                if (KeyCode == 81 & this.Fornecedor.Inactivo == true)
                    MessageBox.Show("Fornecedor Anulado! Não é possível abrir o formulário de certificados!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}