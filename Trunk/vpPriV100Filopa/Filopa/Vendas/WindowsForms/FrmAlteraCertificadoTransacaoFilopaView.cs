using Generico;
using Primavera.Extensibility.CustomForm;
using Primavera.Extensibility.Sales.Editors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VndBE100;

namespace Filopa
{
    public partial class FrmAlteraCertificadoTransacaoFilopaView : CustomForm
    {
        public FrmAlteraCertificadoTransacaoFilopaView()
        {
            InitializeComponent();
        }

        public static  VndBEDocumentoVenda DocumentoVenda { get; set; }
        public static  int LinhaActual { get; set; }

        private void barButtonItemClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

                BSO.DSO.ExecuteSQL("update LinhasDoc set CDU_CertificadoRecebido='0' where Id='" + Module1.certIDlinha + "'");
                // Acrescentado dia 27/01/2021 - Bruno
                BSO.DSO.ExecuteSQL("update LinhasDoc set CDU_CertificadoCancelado=' ' where Id='" + Module1.certIDlinha + "'");


                DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_CertificadoRecebido"].Valor = "0";
                // Acrescentado dia 27/01/2021 - Bruno
                DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_CertificadoCancelado"].Valor = " ";

                CheckEditCertificadoEmitido.EditValue = false;


        }

        private void barButtonItemAplicar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

                BSO.DSO.ExecuteSQL("update LinhasDoc set CDU_CertificadoRecebido='" + CheckEditCertificadoEmitido.EditValue + "' where Id='" + Module1.certIDlinha + "'");
                // Acrescentado dia 27/01/2021 - Bruno
                BSO.DSO.ExecuteSQL("update LinhasDoc set CDU_CertificadoCancelado='" + TextEditCancelado.EditValue + "' where Id='" + Module1.certIDlinha + "'");


                DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_CertificadoRecebido"].Valor = CheckEditCertificadoEmitido.EditValue;
                // Acrescentado dia 27/01/2021 - Bruno
                DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_CertificadoCancelado"].Valor = TextEditCancelado.EditValue;


                this.DialogResult = DialogResult.OK;
                this.Close();


        }

        private void FrmAlteraCertificadoTransacaoFilopaView_Load(object sender, EventArgs e)
        {
            if (FindForm() is Form pai)
            {
                pai.MinimumSize = pai.Size;
                pai.MaximumSize = pai.Size;
            }
            // Add any initialization after the InitializeComponent() call.
            TextEditArtigo.EditValue = Module1.certArtigo;
                // txtLote = certLote
                TextEditDocumento.EditValue = Module1.certDocumento;
                //txtIDlinha = Module1.certIDlinha;
                CheckEditCertificadoEmitido.EditValue = Module1.certEmitido;
                TextEditDescricao.EditValue = Module1.certDescricao;
                // Acrescentado dia 27/01/2021 - Bruno
                TextEditCancelado.EditValue = Module1.certCancelado;



        }

        private void barButtonItemFechar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            this.DialogResult = DialogResult.Cancel;

        }
    }
}
