using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.CustomForm;
using Primavera.Extensibility.Sales.Editors;
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
    public partial class FrmAlteraCertificadoTransacao2View : CustomForm
    {
        public FrmAlteraCertificadoTransacao2View()
        {
            InitializeComponent();
        }
        StdBELista ListaCert;
        string SqlCert;
        public VndBE100.VndBEDocumentoVenda DocumentoVenda { get; set; }
        public int LinhaActual { get; set; }
        private void barButtonItemAplicar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            // Comentado por JFC 25/07/2019 - Antiga listbox. Passamos para combobox.
            // For x = 0 To ListCert.ListCount - 1
            // If ListCert.Selected(x) = True Then
            // BSO.DSO.BDAPL.Execute ("update LinhasDoc set CDU_CertificadoEmitido='" & chkCertificadoEmitido & "', CDU_NumCertificadoTrans='" & ListCert.List(x, 0) & "' where Id='" & certIDlinha & "'")
            // 
            // EditorVendas.DocumentoVenda.Linhas(EditorVendas.LinhaActual).CamposUtil("CDU_NumCertificadoTrans") = ListCert.List(x, 0)
            // EditorVendas.DocumentoVenda.Linhas(EditorVendas.LinhaActual).CamposUtil("CDU_CertificadoEmitido") = chkCertificadoEmitido
            // End If
            // Next x

            BSO.DSO.ExecuteSQL("update LinhasDoc set CDU_CertificadoEmitido='" + checkEditCertEmitido.EditValue + "', CDU_BCIEmitido='" + checkEditBCIEmitido.EditValue + "', CDU_EmitirCertificado='" + checkEditEmitirCertificado.EditValue + "', CDU_NumCertificadoTrans='" + this.textEditTrans1.EditValue + "', CDU_NumCertificadoTrans2='" + this.textEditTrans2.EditValue + "', CDU_NumCertificadoTrans3='" + this.textEditTrans3.EditValue + "', CDU_QtdCertificadoTrans='" + Strings.Replace(this.textEditQtd1.EditValue.ToString(), ",", ".") + "', CDU_QtdCertificadoTrans2='" + Strings.Replace(this.textEditQtd2.EditValue.ToString(), ",", ".") + "', CDU_QtdCertificadoTrans3='" + Strings.Replace(this.textEditQtd3.EditValue.ToString(), ",", ".") + "', CDU_ObsCertificadoTrans='" + (this.textEditObservacao.EditValue) + "' where Id='" + Module1.certIDlinha + "'");


            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_EmitirCertificado"].Valor = checkEditEmitirCertificado.EditValue;
            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_CertificadoEmitido"].Valor = checkEditCertEmitido.EditValue;
            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_BCIEmitido"].Valor = checkEditBCIEmitido.EditValue;
            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_NumCertificadoTrans"].Valor = this.textEditTrans1.EditValue;
            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_NumCertificadoTrans2"].Valor = this.textEditTrans2.EditValue;
            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_NumCertificadoTrans3"].Valor = this.textEditTrans3.EditValue;
            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_QtdCertificadoTrans"].Valor = Strings.Replace(this.textEditQtd1.EditValue.ToString(), ".", ",");
            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_QtdCertificadoTrans2"].Valor = Strings.Replace(this.textEditQtd2.EditValue.ToString(), ".", ",");
            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_QtdCertificadoTrans3"].Valor = Strings.Replace(this.textEditQtd3.EditValue.ToString(), ".", ",");
            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_ObsCertificadoTrans"].Valor = this.textEditObservacao.EditValue;


            // FrmAlteraCertificadoTransacao2.Hide
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void barButtonItemClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BSO.DSO.ExecuteSQL("update LinhasDoc set CDU_CertificadoEmitido='', CDU_EmitirCertificado='', CDU_BCIEmitido='', CDU_NumCertificadoTrans='', CDU_NumCertificadoTrans2='', CDU_NumCertificadoTrans3='', CDU_QtdCertificadoTrans='0', CDU_QtdCertificadoTrans2='0', CDU_QtdCertificadoTrans3='0', CDU_ObsCertificadoTrans='' where Id='" + Module1.certIDlinha + "'");

            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_NumCertificadoTrans"].Valor = "";
            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_NumCertificadoTrans2"].Valor = "";
            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_NumCertificadoTrans3"].Valor = "";
            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_QtdCertificadoTrans"].Valor = "0";
            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_QtdCertificadoTrans2"].Valor = "0";
            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_QtdCertificadoTrans3"].Valor = "0";
            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_CertificadoEmitido"].Valor = "";
            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_EmitirCertificado"].Valor = "";
            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_BCIEmitido"].Valor = "";
            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_ObsCertificadoTrans"].Valor = "";

            checkEditEmitirCertificado.EditValue = false;
            checkEditCertEmitido.EditValue = false;
            checkEditBCIEmitido.EditValue = false;
            this.textEditTrans1.EditValue = "";
            this.textEditTrans2.EditValue = "";
            this.textEditTrans3.EditValue = "";
            this.textEditQtd1.EditValue = 0;
            this.textEditQtd2.EditValue = 0;
            this.textEditQtd3.EditValue = 0;
            this.textEditObservacao.EditValue = "";
        }

        private void barButtonItemAtualizaLista_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            BSO.DSO.ExecuteSQL("exec [dbo].[spInserirCert]");

        }

        private void textEditQtd1_EditValueChanged(object sender, EventArgs e)
        {

            if (!int.TryParse(textEditQtd1.EditValue.ToString(), out int qtd1))
            {
                MessageBox.Show("Only numbers allowed!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textEditQtd1.EditValue = 0;
            }

        }

        private void textEditQtd2_EditValueChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textEditQtd2.EditValue.ToString(), out int qtd2))
            {
                MessageBox.Show("Only numbers allowed!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textEditQtd2.EditValue = "0";
            }
        }

        private void textEditQtd3_EditValueChanged(object sender, EventArgs e)
        {
            if(!int.TryParse(textEditQtd3.EditValue.ToString(),out int qtd3))
            {
                MessageBox.Show("Only numbers allowed!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textEditQtd3.EditValue = 0;
            }
        }

        private void FrmAlteraCertificadoTransacao2View_Load(object sender, EventArgs e)
        {
            textEditArtigo.EditValue = Module1.certArtigo;
            textEditLote.EditValue = Module1.certLote;


            this.textEditTrans1.EditValue = Module1.certCertificadoTransacao;
            this.textEditTrans2.EditValue = Module1.certCertificadoTransacao2;
            this.textEditTrans3.EditValue = Module1.certCertificadoTransacao3;
            this.textEditQtd1.EditValue = Module1.certQtdTransacao;
            this.textEditQtd2.EditValue = Module1.certQtdTransacao2;
            this.textEditQtd3.EditValue = Module1.certQtdTransacao3;
            textEditDocumento.EditValue = Module1.certDocumento;
            //txtIDlinha = Module1.certIDlinha;
            checkEditCertEmitido.EditValue = Module1.certEmitido;
            checkEditEmitirCertificado.EditValue = Module1.certEmitir;
            textEditDescricao.EditValue = Module1.certDescricao;
            textEditObservacao.EditValue = Module1.certObs;
            checkEditBCIEmitido.EditValue = Module1.certBCIEmitido;

        }
    }
}
