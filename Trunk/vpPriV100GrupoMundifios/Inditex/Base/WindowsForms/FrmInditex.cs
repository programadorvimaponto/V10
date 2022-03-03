using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.CustomForm;
using StdBE100;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Vimaponto.PrimaveraV100;

namespace Inditex
{
    public partial class FrmInditex : CustomForm
    {
        public FrmInditex()
        {
            InitializeComponent();

        }
        StdBELista ListaTipoIdentificacao;
        string SqlTipoIdentificacao;
        private void FrmInditex_Load(object sender, System.EventArgs e)
        {
            if (FindForm() is Form pai)
            {
                pai.MinimumSize = pai.Size;
                pai.MaximumSize = pai.Size;
            }

            string sql = "SELECT CDU_TipoIdentificacao as 'Tipo Identificação' FROM prifilopa.dbo.tdu_tipoidentificacaoinditex";

            DataTable dtTipoId = new DataTable();
            dtTipoId = BSO.ConsultaDataTable(sql);
            lookUpEditTipoIdentificacao.Properties.DataSource = dtTipoId;
            lookUpEditTipoIdentificacao.Properties.DisplayMember = "Tipo Identificação";
            lookUpEditTipoIdentificacao.Properties.ValueMember = "Tipo Identificação";



            var lista = new List<string>();
            lista.Add("A");
            lista.Add("B");
            lista.Add("C");
            lista.Add("D");
            lookUpEditClassificacao.Properties.DataSource = lista;

            lista = new List<string>();

            lista.Add("Sim");
            lista.Add("Não");
            lista.Add("Aguarda");

            lookUpEditPreAuditado.Properties.DataSource = lista;
            lookUpEditAuditado.Properties.DataSource = lista;
            lookUpEditAprovado.Properties.DataSource = lista;

            if (Module1.certFiacoes is string)
                textEditCodigoFornecedor.Text = Module1.certFiacoes;
            else
                textEditCodigoFornecedor.Text = "";

        }

        private void barButtonItemFechar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void textEditCodigoFornecedor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F4)
                PSO.AbreLista(0, "Fornecedores", "Fornecedor", this.FindForm(), textEditCodigoFornecedor, "mnuTabFornecedor", blnModal: true);

        }

        private void textEditCodigoFornecedor_EditValueChanged(object sender, System.EventArgs e)
        {
            textEditFornecedor.Text = BSO.Base.Fornecedores.DaValorAtributo(textEditCodigoFornecedor.Text, "Nome");
        }

        private void textEditFornecedor_EditValueChanged(object sender, System.EventArgs e)
        {
            CarregaDados();
        }

        private void barButtonItemCopiaInformacao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //DataObject copiar = new DataObject();
            string resumo;
            resumo = this.textEditFornecedor.Text + Constants.vbCrLf + this.memoEditMorada.EditValue + Constants.vbCrLf + this.memoEditCpLoc.EditValue + Constants.vbCrLf + this.memoEditPais.EditValue + Constants.vbCrLf + Constants.vbCrLf + this.lookUpEditTipoIdentificacao.EditValue + ": " + this.textEditNIdentificacao.EditValue; 
            //copiar.PutInClipboard();
            Clipboard.SetText(resumo);

        }

        private void CopiarFilopa()
        {

            try
            {
                string ent;
                ent = BSO.Base.Fornecedores.DaValorAtributo(textEditCodigoFornecedor.Text, "CDU_EntidadeInterna");
                if (Module1.AbreEmpresa("FILOPA"))
                {
                    var Cli = new StdBELista();
                    Cli = Module1.emp.Consulta("select f.Cliente from Clientes f where f.ClienteAnulado='0' and f.CDU_EntidadeInterna='" + ent + "'");
                    Cli.Inicio();

                    if (Cli.Vazia() == false)
                    {
                        var Campos = new StdBECampos();

                        Campos = BSO.Base.Fornecedores.DaValorAtributos(textEditCodigoFornecedor.Text, "CDU_FiacoesObs", "CDU_FiacoesNIdentificacao", "CDU_FiacoesTIdentificacao", "CDU_FiacoesPreAuditado", "CDU_FiacoesAuditado", "CDU_FiacoesAprovado", "CDU_FiacoesClassificacao", "CDU_FiacoesData");
                        Module1.emp.Base.Clientes.ActualizaValorAtributos(Cli.Valor("Cliente"), Campos);
                        MessageBox.Show("Mundifios - Dados gravados com sucesso!" + Strings.Chr(13) + Strings.Chr(13) + "Filopa - Dados gravados com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Mundifios - Dados gravados com sucesso!" + Strings.Chr(13) + Strings.Chr(13) + "Filopa - Erro:Cliente inexistente(EntidadeInterna " + BSO.Base.Clientes.DaValorAtributo(textEditCodigoFornecedor.Text, "CDU_EntidadeInterna") + ")");
                     }

                    Module1.FechaEmpresa();
                }

                return;
            }
            catch
            {
                MessageBox.Show("Mundifios - Dados gravados com sucesso!" + Strings.Chr(13) + Strings.Chr(13) + "Filopa - Erro:");
            }

        }

        public void AlteraFiacoesClientes()
        {

            try
            {

                StdBECampos CamposFiacoes = new StdBECampos();

                CamposFiacoes = BSO.Base.Fornecedores.DaValorAtributos(textEditCodigoFornecedor.Text, "CDU_FiacoesObs", "CDU_FiacoesNIdentificacao", "CDU_FiacoesTIdentificacao", "CDU_FiacoesPreAuditado", "CDU_FiacoesAuditado", "CDU_FiacoesAprovado", "CDU_FiacoesClassificacao", "CDU_FiacoesData");



                // Data Atualizacao
                CamposFiacoes["CDU_FiacoesData"].Valor = Strings.Format(this.dateEditFiacoes.EditValue, "yyyy-MM-dd");
                // Dados


                // CamposFiacoes("CDU_Fiacoes") = Me.txtFiacoes
                // CamposFiacoes("CDU_FiacoesAprovadas") = Me.txtFiacoesAprovadas
                // CamposFiacoes("CDU_FiacoesAuditoria") = Me.txtFiacoesAuditoria
                // CamposFiacoes("CDU_FiacoesNaoaprovadas") = Me.txtFiacoesNaoAprovadas
                // CamposFiacoes("CDU_FiacoesFonte") = Me.txtFiacoesFonte
                CamposFiacoes["CDU_FiacoesObs"].Valor = this.memoEditFiacoesObs.EditValue;

                CamposFiacoes["CDU_FiacoesNIdentificacao"].Valor = this.textEditNIdentificacao.EditValue;
                CamposFiacoes["CDU_FiacoesTIdentificacao"].Valor = this.lookUpEditTipoIdentificacao.EditValue;
                CamposFiacoes["CDU_FiacoesPreAuditado"].Valor = this.lookUpEditPreAuditado.EditValue;
                CamposFiacoes["CDU_FiacoesAuditado"].Valor = this.lookUpEditAuditado.EditValue;
                CamposFiacoes["CDU_FiacoesAprovado"].Valor = this.lookUpEditAprovado.EditValue;
                CamposFiacoes["CDU_FiacoesClassificacao"].Valor = this.lookUpEditClassificacao.EditValue;


                BSO.Base.Fornecedores.ActualizaValorAtributos(textEditCodigoFornecedor.Text, CamposFiacoes);

                CopiarFilopa();

                return;

            }
            catch(Exception ex)
            {
                MessageBox.Show("Mundifios - Erro ao gravar", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show(ex.ToString());
            }

        }


        private void CarregaDados()
        {
            if (this.textEditFornecedor.EditValue.ToString() == string.Empty)
            {
                this.textEditCodigoFornecedor.EditValue = string.Empty;
                return;
            }

            StdBELista listFiacoes;
            string sql;
            sql = "SELECT Fornecedor, Nome, CDU_FiacoesObs, CDU_FiacoesNIdentificacao, CDU_FiacoesTIdentificacao, CDU_FiacoesPreAuditado, CDU_FiacoesAuditado, CDU_FiacoesAprovado, CDU_FiacoesClassificacao, CDU_FiacoesData, Morada, CpLoc, Paises.Descricao FROM Fornecedores inner join Paises  on Paises.Pais=Fornecedores.Pais WHERE Fornecedor= '" + textEditCodigoFornecedor.Text + "'";

            listFiacoes = BSO.Consulta(sql);
            listFiacoes.Inicio();





            // Data Atualizacao
            this.dateEditFiacoes.EditValue = listFiacoes.Valor("CDU_FiacoesData");
            // Dados
            // Me.txtCliente = listFiacoes("Nome")
            // Me.txtFiacoes = listFiacoes("CDU_Fiacoes")
            // Me.txtFiacoesAprovadas = listFiacoes("CDU_FiacoesAprovadas")
            // Me.txtFiacoesAuditoria = listFiacoes("CDU_FiacoesAuditoria")
            // Me.txtFiacoesNaoAprovadas = listFiacoes("CDU_FiacoesNaoaprovadas")
            // Me.txtFiacoesFonte = listFiacoes("CDU_FiacoesFonte")
            this.memoEditFiacoesObs.EditValue = listFiacoes.Valor("CDU_FiacoesObs");

            this.textEditNIdentificacao.EditValue = listFiacoes.Valor("CDU_FiacoesNIdentificacao");
            this.lookUpEditTipoIdentificacao.EditValue = listFiacoes.Valor("CDU_FiacoesTIdentificacao");
            this.lookUpEditPreAuditado.EditValue = listFiacoes.Valor("CDU_FiacoesPreAuditado");
            this.lookUpEditAuditado.EditValue = listFiacoes.Valor("CDU_FiacoesAuditado");
            this.lookUpEditAprovado.EditValue = listFiacoes.Valor("CDU_FiacoesAprovado");
            this.lookUpEditClassificacao.EditValue = listFiacoes.Valor("CDU_FiacoesClassificacao");

            memoEditPais.EditValue = listFiacoes.Valor("Descricao");
        memoEditMorada.EditValue = listFiacoes.Valor("Morada");
        memoEditCpLoc.EditValue = listFiacoes.Valor("CpLoc");
        }

        private void barButtonItemGravar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (this.textEditCodigoFornecedor.EditValue.ToString() != "")
            {
                AlteraFiacoesClientes();
            }
            else
            {
                MessageBox.Show("O fornecedor não está identificado.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}