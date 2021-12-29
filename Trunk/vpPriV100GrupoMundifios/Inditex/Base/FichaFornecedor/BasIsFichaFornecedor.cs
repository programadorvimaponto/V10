using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.Base.Editors;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Constants.ExtensibilityService;
using System.Windows.Forms;

namespace Inditex
{
    public class BasIsFichaFornecedor : FichaFornecedores
    {
        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(KeyCode, Shift, e);

            if (Module1.VerificaToken("Inditex") == 1)
            {
                // Bruno Peixoto 02/09/2020 - CTRL+O para abrir o formumlario de Fiações Inditex
                if (KeyCode == 79 & this.Fornecedor.Inactivo == false & (Strings.UCase(Aplicacao.Utilizador.Utilizador) == "ANA" | Strings.UCase(Aplicacao.Utilizador.Utilizador) == "RICARDO" | Strings.UCase(Aplicacao.Utilizador.Utilizador) == "SUPORTE" | Strings.UCase(Aplicacao.Utilizador.Utilizador) == "INFORMATICA"))
                {
                    Module1.certFiacoes = this.Fornecedor.Fornecedor;

                    ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmInditex));

                    if (result.ResultCode == ExtensibilityResultCode.Ok)
                    {
                        FrmInditex frm = result.Result;
                        frm.ShowDialog();
                    }
                }

                if (KeyCode == 79 & this.Fornecedor.Inactivo == true)
                    MessageBox.Show("Fornecedor Anulado! Não é possível abrir o formulário de Fiações Inditex!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}