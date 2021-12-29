using Primavera.Extensibility.CustomForm;
using StdBE100;
using System;
using System.Data;
using System.Windows.Forms;

namespace Vatbook
{
    public partial class FrmVatbookView : CustomForm
    {
        public FrmVatbookView()
        {
            InitializeComponent();
        }

        private StdBELista ListaAno;
        private string SqlStringAno;



        private void simpleButtonRicalcolare_Click(object sender, EventArgs e)
        {
            try
            {
                BSO.DSO.ExecuteSQL("dbo.spFattura_Num_Recalcula");
                MessageBox.Show("Ricalcolato con successo!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Errore durante il Ricalcolato!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButtonBlocco_Click(object sender, EventArgs e)
        {
            try
            {
                BSO.DSO.ExecuteSQL("update h set h.CDU_Fattura_Bloccato='1' from Historico h inner join DocumentosCCT ct on ct.Documento=h.TipoDoc where ct.CDU_Fattura_SezionaleIVA is not null and ct.CDU_Fattura_SezionaleIVA != '' and year(h.DataIntroducao)='" + lookUpEditAnno.EditValue + "' and month(h.DataIntroducao)='" + lookUpEditAnno.EditValue + "'");
                BSO.DSO.ExecuteSQL("update cc set cc.CDU_Fattura_Bloccato='1' from CabecCompras cc inner join CabecComprasStatus ccs on ccs.IdCabecCompras=cc.Id inner join DocumentosCompra dc on dc.Documento=cc.TipoDoc where dc.CDU_Fattura_SezionaleIVA is not null and dc.CDU_Fattura_SezionaleIVA != '' and year(cc.DataIntroducao)='" + lookUpEditAnno.EditValue + "' and month(cc.DataIntroducao)='" + lookUpEditMese.EditValue + "' and ccs.Anulado='0'");
                BSO.DSO.ExecuteSQL("update cd set cd.CDU_Fattura_Bloccato='1' from CabecDoc cd inner join CabecDocStatus cds on cds.IdCabecDoc=cd.Id inner join DocumentosVenda dv on dv.Documento=cd.TipoDoc where dv.CDU_Fattura_SezionaleIVA is not null and dv.CDU_Fattura_SezionaleIVA != '' and year(cd.Data)='" + lookUpEditAnno.EditValue + "' and month(cd.Data)='" + lookUpEditMese.EditValue + "' and cds.Anulado='0'");
                MessageBox.Show("Bloccato con successo!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "VatBook Bloccato!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmVatbookView_Load(object sender, EventArgs e)
        {
            SqlStringAno = "select year(getdate()) as 'Ano'";

            DataTable dtdata = new DataTable();
            dtdata = BSO.ConsultaDataTable(SqlStringAno);
            SqlStringAno = "select year(getdate())-1 as 'Ano'";
            dtdata.Load(BSO.ConsultaDataTable(SqlStringAno).CreateDataReader());
                lookUpEditAnno.Properties.DataSource = dtdata;
            lookUpEditAnno.Properties.DisplayMember = "Ano";
            lookUpEditAnno.Properties.ValueMember = "Ano";

            int[] meses = new int[13];
            for (var i = 1; i < meses.Length; i += 1)
                meses[i] = i;

            lookUpEditMese.Properties.DataSource = meses;

                    this.lookUpEditMese.EditValue = 1;
        }
    }
}