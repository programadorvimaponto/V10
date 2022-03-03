using Generico;
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

namespace Default
{
    public partial class FrmAlteraEstadoEncomendaView : CustomForm
    {
        public FrmAlteraEstadoEncomendaView()
        {
            InitializeComponent();
        }
        public bool desativarOF { get; set; }
        private void barButtonItemCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            DialogResult = DialogResult.Cancel;

        }

        private void barButtonItemGravar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Module1.NovaQuantidadeEnc = double.Parse(this.spinEditNovaQuantidadeEncomendada.EditValue.ToString());
            Module1.NovaQtReservadaEnc = double.Parse(this.spinEditNovaQuantidadeReservada.EditValue.ToString());
            Module1.NovoPrecoEnc = double.Parse(this.spinEditNovoPreco.EditValue.ToString());
            Module1.ObsEnc = memoEditObservacoes.EditValue.ToString();

            if (Module1.NovaQuantidadeEnc != 0)
            {
                if (Module1.NovaQuantidadeEnc <= Module1.QtSatisfeitaEnc)
                {
                    MessageBox.Show("A nova quantidade (" + Module1.NovaQuantidadeEnc + ") não pode ser menor ou igual que a quantidade já satisfeita (" + Module1.QtSatisfeitaEnc + ")", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Module1.NovaQuantidadeEnc <= Module1.QtReservadaEnc)
                {
                    MessageBox.Show("A nova quantidade (" + Module1.NovaQuantidadeEnc + ") não pode ser menor ou igual que a quantidade reservada (" + Module1.QtReservadaEnc + ")", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (Module1.NovaQtReservadaEnc != 0)
            {
                if (Module1.NovaQuantidadeEnc == 0)
                {
                    if (Module1.NovaQtReservadaEnc > Module1.QuantidadeEnc)
                    {
                        MessageBox.Show("A quantidade reservada (" + Module1.NovaQtReservadaEnc + ") não pode ser maior que a quantidade encomendada (" + Module1.QuantidadeEnc + ")", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (Module1.NovaQtReservadaEnc > Module1.NovaQuantidadeEnc)
                {
                    MessageBox.Show("A quantidade reservada (" + Module1.NovaQtReservadaEnc + ") não pode ser maior que a nova quantidade encomendada (" + Module1.NovaQuantidadeEnc + ")", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if ((bool)this.checkEditAbrirEncomenda.EditValue == true)
            {
                Module1.Opcao = 1;
            }
            else if ((bool)this.checkEditAbrirLinha.EditValue == true)
            {
                Module1.Opcao = 2;
            }
            else if ((bool)this.checkEditFecharLinha.EditValue == true)
            {
                Module1.Opcao = 3;
            }
            else if ((bool)this.checkEditAlteraLinha.EditValue == true)
            {
                Module1.Opcao = 4;
            }
            else if ((bool)this.checkEditFecharOF.EditValue == true)
            {
                Module1.Opcao = 5;
            }
            else if ((bool)this.checkEditAbrirOF.EditValue == true)
            {
                Module1.Opcao = 6;
            }
            else if ((bool)this.checkEditFecharEncomenda.EditValue == true)
            {
                Module1.Opcao = 7;
            }
            else if ((bool)this.checkEditFecharLinhaOF.EditValue == true)
            {
                Module1.Opcao = 8;
            }
            else if ((bool)this.checkEditAbrirLinhaOF.EditValue == true)
            {
                Module1.Opcao = 9;
            }

            DialogResult = DialogResult.OK;
        }

        private void FrmAlteraEstadoEncomendaView_Load(object sender, EventArgs e)
        {

            this.checkEditAbrirEncomenda.EditValue = false;
            this.checkEditAbrirLinha.EditValue = false;
            this.checkEditFecharLinha.EditValue = false;
            this.checkEditAlteraLinha.EditValue = true;
            DesbloquearCampos();
            LimpaCampos();
            this.textEditArtigo.EditValue = Module1.ArtigoEnc;
            this.textEditDescricao.EditValue = Module1.DescArtEnc;
            this.textEditLote.EditValue = Module1.LoteEnc;
            this.spinEditQuantidadeEncomendada.EditValue = Module1.QuantidadeEnc;
            this.spinEditQuantidadeReservada.EditValue = Module1.QtReservadaEnc;
            this.spinEditPreco.EditValue = Module1.PrecoEnc;
            memoEditObservacoes.EditValue = Module1.ObsEnc;

            this.checkEditAlteraLinha.Focus();

            if(desativarOF)
            {
                checkEditFecharOF.Enabled = false;
                checkEditAbrirOF.Enabled = false;
                checkEditFecharLinhaOF.Enabled = false;
                checkEditAbrirLinhaOF.Enabled = false;
            }
            else
            {
                checkEditFecharOF.Enabled = true;
                checkEditAbrirOF.Enabled = true;
                checkEditFecharLinhaOF.Enabled = true;
                checkEditAbrirLinhaOF.Enabled = true;
            }

        }

        private void LimpaCampos()
        {
            this.textEditArtigo.EditValue = "";
            this.textEditDescricao.EditValue = "";
            this.textEditLote.EditValue = "";
            this.spinEditQuantidadeEncomendada.EditValue = 0;
            this.spinEditNovaQuantidadeEncomendada.EditValue = 0;
            this.spinEditQuantidadeReservada.EditValue = 0;
            this.spinEditNovaQuantidadeReservada.EditValue = 0;
            this.spinEditPreco.EditValue = 0;
            this.spinEditNovoPreco.EditValue = 0;
        }

        private void BloquearCampos()
        {
            this.textEditArtigo.Enabled = false;
            this.textEditDescricao.Enabled = false;
            this.textEditLote.Enabled = false;
            this.spinEditQuantidadeEncomendada.Enabled = false;
            this.spinEditNovaQuantidadeEncomendada.Enabled = false;
            this.spinEditQuantidadeReservada.Enabled = false;
            this.spinEditNovaQuantidadeReservada.Enabled = false;
            this.spinEditPreco.Enabled = false;
            this.spinEditNovoPreco.Enabled = false;
        }

        private void DesbloquearCampos()
        {
            this.textEditArtigo.Enabled = true;
            this.textEditDescricao.Enabled = true;
            this.textEditLote.Enabled = true;
            this.spinEditQuantidadeEncomendada.Enabled = true;
            this.spinEditNovaQuantidadeEncomendada.Enabled = true;
            this.spinEditQuantidadeReservada.Enabled = true;
            this.spinEditNovaQuantidadeReservada.Enabled = true;
            this.spinEditPreco.Enabled = true;
            this.spinEditNovoPreco.Enabled = true;
        }

        private void checkEditAlteraLinha_CheckedChanged(object sender, EventArgs e)
        {
            if((bool)checkEditAlteraLinha.EditValue==true)
            {
                DesbloquearCampos();
            }
            else
            {
                BloquearCampos();

            }
        }
    }
}
