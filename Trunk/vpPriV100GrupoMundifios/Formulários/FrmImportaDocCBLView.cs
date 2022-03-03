using Generico;
using Primavera.Extensibility.CustomForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrupoMundifios.Formulários
{
    public partial class FrmImportaDocCBLView : CustomForm
    {
        public FrmImportaDocCBLView()
        {
            InitializeComponent();
        }

        private void barButtonItemImportar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.lookUpEditEmpresaOrigem.Text == "")
            {
                MessageBox.Show("A empresa origem não está selecionada. " + "\nDeve selecionar uma empresa origem para poder efetuar a importação dos movimentos da contabilidade.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ImportaDocCBL.Movimentos(this.lookUpEditEmpresaOrigem.Text, int.Parse(this.spinEditAno.EditValue.ToString()), int.Parse(this.spinEditNumPeriodoInicial.EditValue.ToString()), int.Parse(this.spinEditNumPeriodoFinal.EditValue.ToString()));
            }
        }

        private void barButtonItemCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.Cancel;

        }

        private void FrmImportaDocCBLView_Load(object sender, EventArgs e)
        {
            if (FindForm() is Form pai)
            {
                pai.MinimumSize = pai.Size;
                pai.MaximumSize = pai.Size;
            }

            this.spinEditAno.EditValue = DateTime.Now.Year;
            this.spinEditNumPeriodoInicial.EditValue = 1;
            this.spinEditNumPeriodoFinal.EditValue = 12;
            PreencheEmpresas();

        }

        public void PreencheEmpresas()
        {
            DataTable dtListaEmpresas;
            string SqlStringEmpresas;
            long k;

            SqlStringEmpresas = "SELECT CDU_Empresa as Empresa FROM TDU_CBLImportaMovEmpresa ORDER BY CDU_Empresa";


            dtListaEmpresas = BSO.ConsultaDataTable(SqlStringEmpresas);

            if(dtListaEmpresas.Rows.Count>0)
            {
                lookUpEditEmpresaOrigem.Properties.DataSource = dtListaEmpresas;
                lookUpEditEmpresaOrigem.Properties.ValueMember = "Empresa";
                lookUpEditEmpresaOrigem.Properties.DisplayMember = "Empresa";
            }
        }

    }
}
