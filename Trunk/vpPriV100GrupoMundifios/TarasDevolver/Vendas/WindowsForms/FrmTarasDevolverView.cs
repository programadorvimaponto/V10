using Microsoft.VisualBasic;
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
using Generico;

namespace TarasDevolver
{
    public partial class FrmTarasDevolverView : CustomForm
    {
        public FrmTarasDevolverView()
        {
            InitializeComponent();
        }

        private void barButtonItemCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            DialogResult = DialogResult.Cancel;
            this.Close();

        }

        private void barButtonItemConfirmar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // 1
            Module1.ConesCartao = int.Parse(textEditNumConesCartao.EditValue.ToString());
            Module1.Devolver_ConesCartao = bool.Parse(checkEditConesCartao.EditValue.ToString());
            // 2
            Module1.ConesPlastico = int.Parse(textEditNumConesPlastico.EditValue.ToString());
            Module1.Devolver_ConesPlastico = bool.Parse(checkEditConesPlastico.EditValue.ToString());
            // 3
            Module1.TubosCartao = int.Parse(textEditNumTubosCartao.EditValue.ToString());
            Module1.Devolver_TubosCartao = bool.Parse(checkEditTubosCartao.EditValue.ToString());
            // 4
            Module1.TubosPlastico = int.Parse(textEditNumTubosPlastico.EditValue.ToString());
            Module1.Devolver_TubosPlastico = bool.Parse(checkEditTubosPlastico.EditValue.ToString());
            // 5
            Module1.PaletesMadeira = int.Parse(textEditNumPaletesMadeira.EditValue.ToString());
            Module1.Devolver_PaletesMadeira = bool.Parse(checkEditPaletesMadeira.EditValue.ToString());
            // 6
            Module1.PaletesPlastico = int.Parse(textEditNumPaletesPlastico.EditValue.ToString());
            Module1.Devolver_PaletesPlastico = bool.Parse(checkEditPaletesPlastico.EditValue.ToString());
            // 7
            Module1.SeparadoresCartao = int.Parse(textEditNumSeparadoresCartao.EditValue.ToString());
            Module1.Devolver_SeparadoresCartao = bool.Parse(checkEditSeparadoresCartao.EditValue.ToString());

            Module1.TotalTaras = Module1.ConesCartao + Module1.ConesPlastico + Module1.TubosCartao + Module1.TubosPlastico + Module1.PaletesMadeira + Module1.PaletesPlastico + Module1.SeparadoresCartao;

            Module1.TotalTaras_a_Devolver = int.Parse(Interaction.IIf(bool.Parse(checkEditConesCartao.EditValue.ToString()), 1, 0).ToString()) + int.Parse(Interaction.IIf(bool.Parse(checkEditConesPlastico.EditValue.ToString()), 1, 0).ToString()) + int.Parse(Interaction.IIf(bool.Parse(this.checkEditTubosCartao.EditValue.ToString()), 1, 0).ToString()) + int.Parse(Interaction.IIf(bool.Parse(this.checkEditTubosPlastico.EditValue.ToString()), 1, 0).ToString()) + int.Parse(Interaction.IIf(bool.Parse(this.checkEditPaletesMadeira.EditValue.ToString()), 1, 0).ToString()) + int.Parse(Interaction.IIf(bool.Parse(this.checkEditPaletesPlastico.EditValue.ToString()), 1, 0).ToString()) + int.Parse(Interaction.IIf(bool.Parse(this.checkEditSeparadoresCartao.EditValue.ToString()), 1, 0).ToString());

            DialogResult = DialogResult.OK;
            this.Close();

        }

        private void FrmTarasDevolverView_Load(object sender, EventArgs e)
        {
            if (FindForm() is Form pai)
            {
                pai.MinimumSize = pai.Size;
                pai.MaximumSize = pai.Size;
            }
            this.textEditNumConesCartao.EditValue = Module1.ConesCartao;
            this.textEditNumConesPlastico.EditValue = Module1.ConesPlastico;
            this.textEditNumTubosCartao.EditValue = Module1.TubosCartao;
            this.textEditNumTubosPlastico.EditValue = Module1.TubosPlastico;
            this.textEditNumPaletesMadeira.EditValue = Module1.PaletesMadeira;
            this.textEditNumPaletesPlastico.EditValue = Module1.PaletesPlastico;
            this.textEditNumSeparadoresCartao.EditValue = Module1.SeparadoresCartao;

            this.checkEditConesCartao.EditValue = Module1.Devolver_ConesCartao;
            this.checkEditConesPlastico.EditValue = Module1.Devolver_ConesPlastico;
            this.checkEditTubosCartao.EditValue = Module1.Devolver_TubosCartao;
            this.checkEditTubosPlastico.EditValue = Module1.Devolver_TubosPlastico;
            this.checkEditPaletesMadeira.EditValue = Module1.Devolver_PaletesMadeira;
            this.checkEditPaletesPlastico.EditValue = Module1.Devolver_PaletesPlastico;
            this.checkEditSeparadoresCartao.EditValue = Module1.Devolver_SeparadoresCartao;

        }
    }
}
