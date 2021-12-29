using Generico;
using Primavera.Extensibility.CustomForm;
using StdBE100;
using System;
using System.Windows.Forms;

namespace Facol
{
    public partial class FrmFacolPagoView : CustomForm
    {
        public FrmFacolPagoView()
        {
            InitializeComponent();
        }

        private void barButtonItemAplicar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
                BSO.DSO.ExecuteSQL("update CabecDoc set CDU_ComissaoFacolPago='" + CheckEditFaturadoFacol.EditValue + "' where TipoDoc='" + Module1.dsptipoDoc + "' and NumDoc='" + Module1.dspNumDoc + "' and Serie='" + Module1.dspSerie + "'");
                BSO.DSO.ExecuteSQL("update CabecDoc set CDU_ComissaoAgentePaga='" + CheckEditPagoAgente.EditValue + "' where TipoDoc='" + Module1.dsptipoDoc + "' and NumDoc='" + Module1.dspNumDoc + "' and Serie='" + Module1.dspSerie + "'");

                this.DialogResult = DialogResult.OK;
                this.Close();
        }

        private void FrmFacolPagoView_Activated(object sender, EventArgs e)
        {
            DaValores();
        }

        private void DaValores()
        {
            StdBELista lista;
            string sql;

            sql = "select isnull(CDU_ComissaoFacolPago,0) as R, isnull(CDU_ComissaoAgentePaga,0) as A from CabecDoc where TipoDoc='" + Module1.dsptipoDoc + "' and NumDoc='" + Module1.dspNumDoc + "' and Serie='" + Module1.dspSerie + "'";
            lista = BSO.Consulta(sql);

            lista.Inicio();

            Module1.dspDisputa = lista.Valor("R");

            CheckEditFaturadoFacol.EditValue = Module1.dspDisputa;
            CheckEditPagoAgente.EditValue = lista.Valor("A");
        }

        private void barButtonItemFechar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FrmFacolPagoView_Load(object sender, EventArgs e)
        {
            DaValores();
        }
    }
}