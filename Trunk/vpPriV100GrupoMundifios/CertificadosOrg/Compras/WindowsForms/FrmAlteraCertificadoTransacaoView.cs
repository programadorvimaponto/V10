using Generico;
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

namespace CertificadosOrg
{
    public partial class FrmAlteraCertificadoTransacaoView : CustomForm
    {
        public FrmAlteraCertificadoTransacaoView()
        {
            InitializeComponent();
        }
        StdBELista ListaProgramLabel;
        string SqlStringProgramLabel;
        private void FrmAlteraCertificadoTransacaoView_Load(object sender, EventArgs e)
        {
            if (FindForm() is Form pai)
            {
                pai.MinimumSize = pai.Size;
                pai.MaximumSize = pai.Size;
            }

            TextEditArtigo.EditValue = Module1.certArtigo;
                TextEditArmazem.EditValue = Module1.certArmazem;
                TextEditlote.EditValue = Module1.certLote;
                TextEditNumCert.EditValue = Module1.certCertificadoTransacao;
                dateEditDataCert.EditValue = Module1.certDataCertificado;
                TextEditDocumento.EditValue = Module1.certDocumento;
                //txtIDlinha = Module1.certIDlinha;
                CheckEditBCI.EditValue = Module1.certBCI;
                if (Strings.UCase(BSO.Inventario.ArtigosLotes.Edita(TextEditArtigo.EditValue.ToString(), TextEditlote.EditValue.ToString()).Observacoes).Contains("BCI") || Module1.certDescricao.Contains("BCI"))
                {
                    CheckEditBCI.Enabled = true;
                }
                else
                {
                    CheckEditBCI.Enabled = false;
                }

                // Preenche combo das Program Labels
                SqlStringProgramLabel = "SELECT * FROM TDU_CertificadosLabels ORDER BY CDU_Id ASC";
                ListaProgramLabel = BSO.Consulta(SqlStringProgramLabel);
                if (ListaProgramLabel.Vazia() == false)
                {
                    ListaProgramLabel.Inicio();
                    for (int k = 1, loopTo = ListaProgramLabel.NumLinhas(); k <= loopTo; k++)
                    {
                        var dt = default(DataTable);
                        dt.Rows.Add(ListaProgramLabel.Valor("CDU_Id") + " - " + ListaProgramLabel.Valor("CDU_Program") + " - " + ListaProgramLabel.Valor("CDU_Label"));
                        LookUpEditProgramLabel.Properties.DataSource = dt;
                        ListaProgramLabel.Seguinte();
                    }
                }


                // Preenche texto default da combo igual ao cert na encomenda
                SqlStringProgramLabel = "SELECT * FROM TDU_CertificadosLabels where CDU_Id='" + Module1.certProgramLabel + "' ORDER BY CDU_Id ASC";
                ListaProgramLabel = BSO.Consulta(SqlStringProgramLabel);
                if (ListaProgramLabel.Vazia() == false)
                {
                    ListaProgramLabel.Inicio();
                    this.LookUpEditProgramLabel.EditValue = ListaProgramLabel.Valor("CDU_Id") + " - " + ListaProgramLabel.Valor("CDU_Program") + " - " + ListaProgramLabel.Valor("CDU_Label");
                }

        }
        public CmpBE100.CmpBEDocumentoCompra DocumentoCompra { get; set; }
        public int LinhaActual { get; set; }
        private void BarButtonItemGravar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BSO.DSO.ExecuteSQL("update LinhasCompras set CDU_DataCertificadoTrans=convert(datetime,'" + dateEditDataCert.EditValue + "',105), CDU_NumCertificadoTrans='" + TextEditNumCert.EditValue + "', CDU_ProgramLabels='" + Strings.Left(this.LookUpEditProgramLabel.EditValue.ToString(), 1) + "', CDU_BCI='" + CheckEditBCI.EditValue + "' where Id='" + Module1.certIDlinha + "'");
            DocumentoCompra.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_DataCertificadoTrans"].Valor = dateEditDataCert.EditValue;
            DocumentoCompra.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_NumCertificadoTrans"].Valor = TextEditNumCert.EditValue;
            DocumentoCompra.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_ProgramLabels"].Valor = Strings.Left(this.LookUpEditProgramLabel.EditValue.ToString(), 1);
            DocumentoCompra.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_BCI"].Valor = CheckEditBCI.EditValue;

            BSO.DSO.ExecuteSQL("exec [dbo].[spInserirCert]");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BarButtonItemFechar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
