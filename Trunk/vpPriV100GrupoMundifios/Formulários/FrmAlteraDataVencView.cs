using Microsoft.VisualBasic;
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

namespace GrupoMundifios.Formulários
{
    public partial class FrmAlteraDataVencView : CustomForm
    {
        public FrmAlteraDataVencView()
        {
            InitializeComponent();
        }
        DataTable dtListaTipoDoc;
        string SqlStringTipoDoc;
        DataTable dtListaSerieDoc;
        StdBELista ListaLetra;
        string SqlStringLetra;
        private void lookUpEditTipoDoc_EditValueChanged(object sender, EventArgs e)
        {

            lookUpEditSerieDoc.Text="";
            string SqlStringSerieDoc = "SELECT Serie FROM [SERIESCCT] " + "WHERE TIPODOC = '" + this.lookUpEditTipoDoc.Text + "'";


            dtListaSerieDoc = BSO.ConsultaDataTable(SqlStringSerieDoc);

            if (dtListaSerieDoc.Rows.Count > 0)
            {
                lookUpEditSerieDoc.Properties.DataSource = dtListaSerieDoc;
                lookUpEditSerieDoc.Properties.ValueMember = "Serie";
                lookUpEditSerieDoc.Properties.DisplayMember = "Serie";
            }

        }

        private void barButtonItemCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void barButtonItemConfirmar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (this.lookUpEditTipoDoc.Text == "" | this.lookUpEditSerieDoc.Text == "" | this.spinEditNumAno.EditValue.ToString() == "0")
            {
                Interaction.MsgBox("Dados preenchidos inválidos.", (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly));
                return;
            }

            SqlStringLetra = "SELECT * FROM [PENDENTES] " + "WHERE TIPODOC = '" + this.lookUpEditTipoDoc.Text + "' AND SERIE = '" + this.lookUpEditSerieDoc.Text + "' AND NUMDOC = " + this.spinEditNumAno.EditValue + " AND TIPOCONTA = 'CLR' AND ESTADO IN ('EAC', 'ACT')";


            ListaLetra = BSO.Consulta(SqlStringLetra);

            if(ListaLetra.Vazia() == false)
            {
                ListaLetra.Inicio();
                BSO.DSO.ExecuteSQL("UPDATE PENDENTES SET DATAVENC = CONVERT(DATETIME, '" + this.dateEditDataVenc.EditValue + "',102) WHERE TIPODOC = '" + this.lookUpEditTipoDoc.Text + "' AND SERIE = '" + this.lookUpEditSerieDoc.Text + "' AND NUMDOC = " + this.spinEditNumAno.EditValue + "");
                BSO.DSO.ExecuteSQL("UPDATE HISTORICO SET DATAVENC = CONVERT(DATETIME, '" + this.dateEditDataVenc.EditValue + "',102) WHERE TIPODOC = '" + this.lookUpEditTipoDoc.Text + "' AND SERIE = '" + this.lookUpEditSerieDoc.Text + "' AND NUMDOC = " + this.spinEditNumAno.EditValue + "");
                Interaction.MsgBox("Nova data de vencimento atualizada com sucesso!", (MsgBoxStyle)((int)Constants.vbInformation + (int)Constants.vbOKOnly));
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                Interaction.MsgBox("Não foi encontrado nenhum documento do tipo letra " + '\r' + "no estado 'Enviado p/ aceite' ou 'Aceite'.", (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly));
            }

        }

        private void FrmAlteraDataVencView_Load(object sender, EventArgs e)
        {
            if (FindForm() is Form pai)
            {
                pai.MinimumSize = pai.Size;
                pai.MaximumSize = pai.Size;
            }

            this.dateEditDataVenc.EditValue = Strings.Format(DateAndTime.Now, "yyyy-MM-dd");
            SqlStringTipoDoc = "SELECT dbo.DocumentosCCT.Documento, dbo.DocumentosCCT.Descricao, dbo.DocumentosCCContaEstado.TipoConta, dbo.DocumentosCCContaEstado.Estado " + "FROM dbo.DocumentosCCT INNER JOIN dbo.DocumentosCCContaEstado ON dbo.DocumentosCCT.Documento = dbo.DocumentosCCContaEstado.Documento " + "WHERE (dbo.DocumentosCCT.TipoDocumento = 1) AND (dbo.DocumentosCCT.Clientes = 1) AND (dbo.DocumentosCCContaEstado.TipoConta = 'CLR')";

            dtListaTipoDoc = BSO.ConsultaDataTable(SqlStringTipoDoc);


            if(dtListaTipoDoc.Rows.Count > 0)
            {
                lookUpEditTipoDoc.Properties.DataSource = dtListaTipoDoc;
                lookUpEditTipoDoc.Properties.DisplayMember = "Documento";
                lookUpEditTipoDoc.Properties.ValueMember = "Documento";


            }
        }
    }
}
