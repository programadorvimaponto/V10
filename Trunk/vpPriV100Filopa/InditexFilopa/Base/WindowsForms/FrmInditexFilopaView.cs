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

namespace InditexFilopa
{
    public partial class FrmInditexFilopaView : CustomForm
    {
        public FrmInditexFilopaView()
        {
            InitializeComponent();
        }
        StdBELista ListaTipoIdentificacao;
        string SqlTipoIdentificacao;
        private void barButtonItemGravar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (this.textEditCodigoCliente.EditValue.ToString()!= "")
            {

                Module1.FechaEmpresa();
                Module1.AbreEmpresa("FILOPA");
                AlteraFiacoesClientes();

            }
            else
            {
                MessageBox.Show("O cliente não está identificado.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void CarregaDados()
        {
            if (this.textEditCliente.EditValue.ToString() == string.Empty)
            {
                this.textEditCodigoCliente.EditValue = string.Empty;
                return;
            }

            StdBELista listFiacoes;
            string sql;
            sql = "SELECT cliente, Nome, CDU_FiacoesObs, CDU_FiacoesNIdentificacao, CDU_FiacoesTIdentificacao, CDU_FiacoesPreAuditado, CDU_FiacoesAuditado, CDU_FiacoesAprovado, CDU_FiacoesClassificacao, CDU_FiacoesData, Fac_Mor, Fac_Local, Paises.Descricao  FROM " + "PRIFilopa" + ".dbo.Clientes inner join prifilopa.dbo.Paises  on prifilopa.dbo.Paises.Pais=Clientes.Pais WHERE Cliente= '" + this.textEditCodigoCliente.EditValue + "'";

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

            this.TextEditPais.EditValue = listFiacoes.Valor("Descricao");
            this.TextEditFacMor.EditValue = listFiacoes.Valor("Fac_Mor");
            this.TextEditFacLocal.EditValue = listFiacoes.Valor("Fac_Local");
        }

        public void AlteraFiacoesClientes()
        {

            try
            {

                StdBECampos CamposFiacoes = new StdBECampos();

                CamposFiacoes = BSO.Base.Clientes.DaValorAtributos(textEditCodigoCliente.EditValue.ToString(), "CDU_FiacoesObs", "CDU_FiacoesNIdentificacao", "CDU_FiacoesTIdentificacao", "CDU_FiacoesPreAuditado", "CDU_FiacoesAuditado", "CDU_FiacoesAprovado", "CDU_FiacoesClassificacao", "CDU_FiacoesData");

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

                BSO.Base.Clientes.ActualizaValorAtributos(this.textEditCodigoCliente.EditValue.ToString(), CamposFiacoes);

                CopiarMundifios();

                return;

            }
            catch ( Exception ex)
            {
                MessageBox.Show("Filopa - Erro ao gravar", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void CopiarMundifios()
        {

            try
            {
                string ent;
                ent = BSO.Base.Clientes.DaValorAtributo(this.textEditCodigoCliente.EditValue.ToString(), "CDU_EntidadeInterna");
                if (Module1.AbreEmpresa("MUNDIFIOS"))
                {
                    var Forn = new StdBELista();
                    Forn = BSO.Consulta("select f.Fornecedor from Fornecedores f where f.FornecedorAnulado='0' and f.CDU_EntidadeInterna='" + ent + "'");
                    Forn.Inicio();
                    if (Forn.Vazia() == false)
                    {
                        var Campos = new StdBECampos();
                        Campos = BSO.Base.Fornecedores.DaValorAtributos(this.textEditCodigoCliente.EditValue.ToString(), "CDU_FiacoesObs", "CDU_FiacoesNIdentificacao", "CDU_FiacoesTIdentificacao", "CDU_FiacoesPreAuditado", "CDU_FiacoesAuditado", "CDU_FiacoesAprovado", "CDU_FiacoesClassificacao", "CDU_FiacoesData");
                        BSO.Base.Fornecedores.ActualizaValorAtributos(Forn.Valor("Fornecedor"), Campos);
                        MessageBox.Show("Filopa - Dados gravados com sucesso!" + '\r' + '\r' + "Mundifios - Dados gravados com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Filopa - Dados gravados com sucesso!" + '\r' + '\r' + "Mundifios - Erro:Fornecedor inexistente(EntidadeInterna " + BSO.Base.Clientes.DaValorAtributo(this.textEditCodigoCliente.EditValue.ToString(), "CDU_EntidadeInterna") + ")", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    Module1.FechaEmpresa();
                }

                return;
            }
            catch
            {
                MessageBox.Show("Filopa - Dados gravados com sucesso!" + Strings.Chr(13) + Strings.Chr(13) + "Mundifios - Erro.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void barButtonItemCopiarInformacao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //DataObject copiar = new DataObject();
            string resumo;
            resumo = (this.textEditCliente.EditValue + Constants.vbCrLf + this.TextEditFacMor.EditValue + Constants.vbCrLf + this.TextEditFacLocal.EditValue + Constants.vbCrLf + this.TextEditPais.EditValue + Constants.vbCrLf + Constants.vbCrLf + this.lookUpEditTipoIdentificacao.EditValue + ": " + this.textEditNIdentificacao.EditValue);

            //copiar.SetText(resumo);
            //copiar.PutInClipboard();
            Clipboard.SetText(resumo);
        }



        private void FrmInditexFilopaView_Load(object sender, EventArgs e)
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
                textEditCodigoCliente.Text = Module1.certFiacoes;
            else
                textEditCodigoCliente.Text = "";

        }

        private void barButtonItemFechar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;


        }

        private void textEditCliente_EditValueChanged(object sender, EventArgs e)
        {

            CarregaDados();

        }

        private void textEditCodigoCliente_EditValueChanged(object sender, EventArgs e)
        {
            textEditCliente.EditValue = BSO.Base.Clientes.DaValorAtributo(textEditCodigoCliente.Text, "Nome");

        }

        private void textEditCodigoCliente_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F4)
                PSO.AbreLista(0, "Clientes", "Cliente", this.FindForm(), textEditCodigoCliente, "mnuTabCliente", blnModal: true);

        }
    }
}
