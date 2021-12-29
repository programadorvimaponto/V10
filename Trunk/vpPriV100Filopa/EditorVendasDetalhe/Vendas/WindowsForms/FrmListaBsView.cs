using System;
using System.Collections.Generic;
using Primavera.Extensibility.CustomForm;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorVendasDetalhe
{
    public partial class FrmListaBsView : CustomForm
    {
        private BindingSource _GridDataSource = null;

        public FrmListaBsView()
        {
            InitializeComponent();
        }
        public void IniciaListaBs(string FrmText, BindingSource GridDataSource)
        {

            // Add any initialization after the InitializeComponent() call.

            _GridDataSource = GridDataSource;


            Text = FrmText;
            barHeaderItemTotalRegistos.Caption = GridDataSource.Count + " registos.";


            vmpGridControlListaBs.DataSource = GridDataSource;
            vmpGridViewListaBs.OptionsView.ShowAutoFilterRow = true;
        }


        private void barButtonItemConfirmar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Validacoes())
            {
                DialogResult = DialogResult.OK;
                Close();
            }

        }

        private void barButtonItemCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public bool Validacoes()
        {
            if (vmpGridViewListaBs.RowCount == 0)
            {
                MessageBox.Show("Grelha sem registos", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        public object GetSelectedRow()
        {
            return vmpGridViewListaBs.GetSelectedRows();
        }


        private void vmpGridControlListaBs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

    }
}
