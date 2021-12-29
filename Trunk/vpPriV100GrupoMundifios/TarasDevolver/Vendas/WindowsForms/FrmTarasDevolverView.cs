//using Microsoft.VisualBasic;
//using Primavera.Extensibility.CustomForm;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using Generico;

//namespace TarasDevolver
//{
//    public partial class FrmTarasDevolverView : CustomForm
//    {
//        public FrmTarasDevolverView()
//        {
//            InitializeComponent();
//        }

//        private void barButtonItemCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
//        {

//            DialogResult = DialogResult.Cancel;
//            this.Close();

//        }

//        private void barButtonItemConfirmar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
//        {
//                // 1
//                Module1.ConesCartao = int.Parse(textEditNumConesCartao.EditValue.ToString());
//            Module1.Devolver_ConesCartao = bool.Parse(checkEditConesCartao.EditValue.ToString());
//            // 2
//            Module1.ConesPlastico = int.Parse(textEditNumConesPlastico.EditValue.ToString());
//            Module1.Devolver_ConesPlastico = bool.Parse(checkEditConesPlastico.EditValue.ToString());
//            // 3
//            Module1.TubosCartao = int.Parse(textEditNumTubosCartao.EditValue.ToString());
//            Module1.Devolver_TubosCartao = bool.Parse(checkEditTubosCartao.EditValue.ToString());
//            // 4
//            Module1.TubosPlastico = int.Parse(textEditNumTubosPlastico.EditValue.ToString());
//            Module1.Devolver_TubosPlastico = bool.Parse(checkEditTubosPlastico.EditValue.ToString());
//            // 5
//            Module1.PaletesMadeira = int.Parse(textEditNumPaletesMadeira.EditValue.ToString());
//            Module1.Devolver_PaletesMadeira = bool.Parse(checkEditPaletesMadeira.EditValue.ToString());
//            // 6
//            Module1.PaletesPlastico = int.Parse(textEditNumPaletesPlastico.EditValue.ToString());
//            Module1.Devolver_PaletesPlastico = bool.Parse(checkEditPaletesPlastico.EditValue.ToString());
//            // 7
//            Module1.SeparadoresCartao = int.Parse(textEditNumSeparadoresCartao.EditValue.ToString());
//            Module1.Devolver_SeparadoresCartao = bool.Parse(checkEditSeparadoresCartao.EditValue.ToString());

//            Module1.TotalTaras = Module1.ConesCartao + Module1.ConesPlastico + Module1.TubosCartao + Module1.TubosPlastico + Module1.PaletesMadeira + Module1.PaletesPlastico + Module1.SeparadoresCartao;

//            Module1.TotalTaras_a_Devolver = Interaction.IIf(checkEditConesCartao.EditValue, 1, 0) + Interaction.IIf(checkEditConesPlastico.EditValue, 1, 0) + Interaction.IIf(this.checkEditTubosCartao.EditValue, 1, 0) + Interaction.IIf(this.checkEditTubosPlastico.EditValue, 1, 0) + Interaction.IIf(this.checkEditPaletesMadeira.EditValue, 1, 0) + Interaction.IIf(this.checkEditPaletesPlastico.EditValue, 1, 0) + Interaction.IIf(this.checkEditSeparadoresCartao.EditValue, 1, 0);

//            DialogResult = DialogResult.OK;
//            this.Close();

//        }

//        private void FrmTarasDevolverView_Load(object sender, EventArgs e)
//        {

//            this.textEditNumConesCartao.EditValue = Module1.ConesCartao;
//            this.textEditNumConesPlastico.EditValue = Module1.ConesPlastico;
//            this.textEditNumTubosCartao.EditValue = Module1.TubosCartao;
//            this.textEditNumTubosPlastico.EditValue = Module1.TubosPlastico;
//            this.textEditNumPaletesMadeira.EditValue = Module1.PaletesMadeira;
//            this.textEditNumPaletesPlastico.EditValue = Module1.PaletesPlastico;
//            this.textEditNumSeparadoresCartao.EditValue = Module1.SeparadoresCartao;

//            this.checkEditConesCartao.EditValue = Module1.Devolver_ConesCartao;
//            this.checkEditConesPlastico.EditValue = Module1.Devolver_ConesPlastico;
//            this.checkEditTubosCartao.EditValue = Module1.Devolver_TubosCartao;
//            this.checkEditTubosPlastico.EditValue = Module1.Devolver_TubosPlastico;
//            this.checkEditPaletesMadeira.EditValue = Module1.Devolver_PaletesMadeira;
//            this.checkEditPaletesPlastico.EditValue = Module1.Devolver_PaletesPlastico;
//            this.checkEditSeparadoresCartao.EditValue = Module1.Devolver_SeparadoresCartao;

//        }
//    }
//}
