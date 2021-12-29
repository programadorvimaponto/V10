using Generico;
using Primavera.Extensibility.Base.Editors;
using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Constants.ExtensibilityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FornecedoresCertificadosFilopa
{
    public class BasIsFichaCliente : FichaClientes
    {

        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(KeyCode, Shift, e);

            if (KeyCode == 81 & this.Cliente.Inactivo == false)
            {
                Module1.certEntidade = this.Cliente.Cliente;


                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmFornecedoresCertsFilopaView));

                if (result.ResultCode == ExtensibilityResultCode.Ok)
                {
                    FrmFornecedoresCertsFilopaView frm = result.Result;
                    frm.ShowDialog();
                }
            }

            if (KeyCode == 81 & this.Cliente.Inactivo == true)
                MessageBox.Show("Cliente Anulado! Não é possível abrir o formulário de certificados!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


    }
}
