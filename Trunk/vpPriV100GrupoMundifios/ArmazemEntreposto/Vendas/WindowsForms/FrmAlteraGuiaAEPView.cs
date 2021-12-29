using Generico;
using Primavera.Extensibility.CustomForm;
using System;
using System.Windows.Forms;

namespace ArmazemEntreposto
{
    public partial class FrmAlteraGuiaAEPView : CustomForm
    {

        public VndBE100.VndBEDocumentoVenda DocumentoVenda { get; set; }
        public int LinhaActual { get; set; }
        public FrmAlteraGuiaAEPView()
        {
            InitializeComponent();
        }
        private void barButtonItemGravar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BSO.DSO.ExecuteSQL("update LinhasDoc set CDU_Regime='" + textEditRegime.EditValue + "', CDU_DespDAU='" + textEditDespDAU.EditValue + "' where Id='" + Module1.aepIDlinha + "'");
            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_Regime"].Valor = textEditRegime.EditValue;
            DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_DespDAU"].Valor = textEditDespDAU.EditValue;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void barButtonItemFechar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FrmAlteraGuiaAEPView_Load(object sender, EventArgs e)
        {
            textEditArtigo.EditValue = Module1.aepArtigo;
            textEditArm.EditValue = Module1.aepArmazem;
            textEditLote.EditValue = Module1.aepLote;
            textEditDespDAU.EditValue = Module1.aepDespDAU;
            textEditRegime.EditValue = Module1.aepRegime;
            textEditDocumento.EditValue = Module1.aepDocumento;
            //txtIDlinha = Module1.aepIDlinha;
        }
    }
}