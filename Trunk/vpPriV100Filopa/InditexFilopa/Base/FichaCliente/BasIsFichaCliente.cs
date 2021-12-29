using Generico;
using Primavera.Extensibility.Base.Editors;
using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InditexFilopa
{
    public class BasIsFichaCliente : FichaClientes
    {

        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(KeyCode, Shift, e);

            // Bruno Peixoto 27/04/2020 - CTRL+O para abrir o formumlario de Fiações Inditex
            if (KeyCode == 79 & this.Cliente.Inactivo == false)
            {
                Module1.certFiacoes = this.Cliente.Cliente;

                ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmInditexFilopaView));
                FrmInditexFilopaView frm = result.Result;
                frm.ShowDialog();
            }

            if (KeyCode == 79 & this.Cliente.Inactivo == true)
                MessageBox.Show("Cliente Anulado! Não é possível abrir o formulário de Fiações Inditex!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

    }
}
