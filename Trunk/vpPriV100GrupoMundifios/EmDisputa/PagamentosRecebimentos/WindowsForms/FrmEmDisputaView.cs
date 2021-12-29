using Generico;
using Primavera.Extensibility.CustomForm;
using StdBE100;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmDisputa
{
    public partial class FrmEmDisputaView : CustomForm
    {
        public FrmEmDisputaView()
        {
            InitializeComponent();


        }

        private void barButtonItemAplicar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            BSO.DSO.ExecuteSQL("update Historico set CDU_EmDisputa='" + CheckEditFaturaDisputa.EditValue + "' where TipoDoc='" + Module1.dsptipoDoc + "' and NumDocInt='" + Module1.dspNumDoc + "' and Serie='" + Module1.dspSerie + "'");

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void FrmEmDisputaView_Activated(object sender, EventArgs e)
        {

                StdBELista lista;
                string sql;

                sql = "select isnull(CDU_EmDisputa,0) as R from Historico where TipoDoc='" + Module1.dsptipoDoc + "' and NumDocInt='" + Module1.dspNumDoc + "' and Serie='" + Module1.dspSerie + "'";
                lista = BSO.Consulta(sql);

                lista.Inicio();


            if (lista.Vazia() == false)
                Module1.dspDisputa = lista.Valor("R");

            CheckEditFaturaDisputa.EditValue = Module1.dspDisputa;


        }

        private void FrmEmDisputaView_Load(object sender, EventArgs e)
        {
            StdBELista lista;
            string sql;

            sql = "select isnull(CDU_EmDisputa,0) as R from Historico where TipoDoc='" + Module1.dsptipoDoc + "' and NumDocInt='" + Module1.dspNumDoc + "' and Serie='" + Module1.dspSerie + "'";
            lista = BSO.Consulta(sql);

            lista.Inicio();

            // If lista("R") & "" = "" Then
            // dspDisputa = False
            // Else
            if(lista.Vazia() == false)
            Module1.dspDisputa = lista.Valor("R");
            // End If

            CheckEditFaturaDisputa.EditValue = Module1.dspDisputa;
        }

        private void barButtonItemFechar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }
    }
}
