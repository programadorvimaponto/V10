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
using Vimaponto.Componentes.Sdk.Controlos.VmpFormListaBs;
using Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.Class;
using Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.DataSource;
using static Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.Class.Enums;

namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos
{
    public partial class frmListaGeral : CustomForm
    {
        public frmListaGeral()
        {
            InitializeComponent();
            // # instanciar dataset geral
            Listagem = new DsListas();
        }
        private DsListas Listagem;
        private Listagem ListagemTipo;
        private ListParameter ListaParametros;
        private Form frmAnterior;
        private string codigoAnt;
        private string filter;
        private string filter2 = "";
        private Guid idProcessoDetalhe = Guid.Empty;
        private string Operacao = "";
        private string Familia;
        private string Maquina;
        public decimal TotlaPesoM2;
        public string Cliente = "";

        private string ListaArtigosExistentes;
        private string ListaClientesExistentes;
        private int NumClientes = 0;

        public frmListaGeral(Form FormAnt)
        {
            InitializeComponent();

            this.frmAnterior = FormAnt;
            // # instanciar dataset geral
            Listagem = new DsListas();
        }

        public frmListaGeral(Listagem ListagemTipo, ref ListParameter ListaParametros, string Filtro, bool RececaoSemReceita)
        {

            // This call is required by the designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            this.ListagemTipo = ListagemTipo;
            this.ListaParametros = ListaParametros;
            this.filter = Filtro;

            // # instanciar dataset geral
            Listagem = new DsListas();
        }
        public frmListaGeral(Listagem ListagemTipo, ref ListParameter ListaParametros, string Filtro = "", Form formAnt = null, bool UserPassoPrecedente = false)
        {
            InitializeComponent();
            this.frmAnterior = formAnt;
            this.ListagemTipo = ListagemTipo;
            this.ListaParametros = ListaParametros;
            this.filter = Filtro;
            // # instanciar dataset geral
            Listagem = new DsListas();
        }

        public new void Show()
        {
            base.Show();
        }

        public new void Show(ListParameter ListaParametros, Listagem ListagemTipo = 0, string Filtro = "", string Operacao = "", string codFamilia = "", Form FormAnt = null/* TODO Change to default(_) if this is not a reference type */)
        {
            this.frmAnterior = FormAnt;
            this.ListagemTipo = ListagemTipo;
            this.ListaParametros = ListaParametros;
            this.filter = Filtro;
            this.Operacao = Operacao;
            this.Familia = codFamilia;
            // #
            base.Show();
        }

        public new void ShowDialog(Listagem ListagemTipo = 0, ListParameter ListaParametros = null, string Filtro = "", string Operacao = "", string codFamilia = "", Form FormAnt = null, string Maquina = "", string Filtro2 = "", Guid? idProcessoDetalhe = default(Guid?))
        {
            this.frmAnterior = FormAnt;
            this.ListagemTipo = ListagemTipo;
            this.ListaParametros = ListaParametros;
            this.filter = Filtro;
            this.filter2 = Filtro2;
            if (!(idProcessoDetalhe == null))
                this.idProcessoDetalhe = (Guid)idProcessoDetalhe;
            this.Operacao = Operacao;
            this.Familia = codFamilia;
            this.Maquina = Maquina;
            this.StartPosition = FormStartPosition.CenterScreen;
            // #
            base.ShowDialog();
        }

        private void FrmListaGeral_Load(object sender, EventArgs e)
        {

            try
            {
                // # icone do form
                //this.Icon = Icon.FromHandle(My.Resources.png_lista_32.GetHicon());

                switch (ListagemTipo)
                {
                    case Enums.Listagem.TiposDimensao:
                        {
                            // #
                            DataSource.DsListasTableAdapters.TiposDimensaoTableAdapter adp = new DataSource.DsListasTableAdapters.TiposDimensaoTableAdapter();
                            adp.Fill(Listagem.TiposDimensao);
                            vmpGridControlListaGeral.DataSource = Listagem.TiposDimensao;
                            this.Text = "Tipos Dimensão";
                            break;
                        }

                    case Enums.Listagem.Dimensao:
                        {
                            // #
                            DataSource.DsListasTableAdapters.DimensaoTableAdapter adp = new DataSource.DsListasTableAdapters.DimensaoTableAdapter();

                            if (filter.Trim() != "")
                                adp.FillByTipo(Listagem.Dimensao, filter);
                            else
                                adp.Fill(Listagem.Dimensao);

                            vmpGridControlListaGeral.DataSource = Listagem.Dimensao;
                            this.Text = "Dimensao";
                            break;
                        }

                    case Enums.Listagem.ArtigoGeradorSeq:
                        {
                            DataSource.DsListasTableAdapters.ArtigoGeradorSeqTableAdapter adp = new DataSource.DsListasTableAdapters.ArtigoGeradorSeqTableAdapter();

                            if (filter != "")
                            {
                                filter += "%";
                                adp.FillByArtigo(Listagem.ArtigoGeradorSeq, filter);
                            }
                            else
                                adp.FillBy(Listagem.ArtigoGeradorSeq);

                            vmpGridControlListaGeral.DataSource = Listagem.ArtigoGeradorSeq;
                            this.Text = "Artigos";
                            break;
                        }


                    case Enums.Listagem.Familia:
                        {
                            // #
                            DataSource.DsListasTableAdapters.FamiliasTableAdapter adp = new DataSource.DsListasTableAdapters.FamiliasTableAdapter();
                            adp.Fill(Listagem.Familias);
                            vmpGridControlListaGeral.DataSource = Listagem.Familias;
                            this.Text = "Familias";
                            break;
                        }

                    case Enums.Listagem.Unidade:
                        {
                            // #
                            DataSource.DsListasTableAdapters.UnidadesTableAdapter adp = new DataSource.DsListasTableAdapters.UnidadesTableAdapter();
                            adp.Fill(Listagem.Unidades);
                            vmpGridControlListaGeral.DataSource = Listagem.Unidades;
                            this.Text = "Unidades";
                            break;
                        }

                    case Enums.Listagem.Armazem:
                        {
                            // #
                            DataSource.DsListasTableAdapters.ArmazensTableAdapter adp = new DataSource.DsListasTableAdapters.ArmazensTableAdapter();
                            adp.Fill(Listagem.Armazens);
                            vmpGridControlListaGeral.DataSource = Listagem.Armazens;
                            this.Text = "Tipos de Armazéns";
                            break;
                        }

                    case Enums.Listagem.TipoArtigo:
                        {
                            // #
                            DataSource.DsListasTableAdapters.TiposArtigoTableAdapter adp = new DataSource.DsListasTableAdapters.TiposArtigoTableAdapter();
                            adp.Fill(Listagem.TiposArtigo);
                            vmpGridControlListaGeral.DataSource = Listagem.TiposArtigo;
                            this.Text = "Tipos de Artigo";
                            break;
                        }

                    case Enums.Listagem.Marca:
                        {
                            DataSource.DsListasTableAdapters.MarcasTableAdapter adp = new DataSource.DsListasTableAdapters.MarcasTableAdapter();
                            if (Familia != "")
                                adp.FillByFamilia(Listagem.Marcas, Familia);
                            else
                                adp.Fill(Listagem.Marcas);
                            vmpGridControlListaGeral.DataSource = Listagem.Marcas;
                            this.Text = "Marcas";
                            break;
                        }


                    case Enums.Listagem.Cliente:
                        {
                            DataSource.DsListasTableAdapters.ClientesTableAdapter adp = new DataSource.DsListasTableAdapters.ClientesTableAdapter();
                            if (this.filter != "")
                                adp.FillBy(Listagem.Clientes, this.filter);
                            else
                                adp.Fill(Listagem.Clientes);

                            vmpGridControlListaGeral.DataSource = Listagem.Clientes;
                            this.Text = "Clientes";

                            if ((Cliente != ""))
                            {
                                vmpGridViewListaGeral.FocusedRowHandle = Listagem.Clientes.Rows.IndexOf(Listagem.Clientes.FindByCliente(Cliente));

                                if ((vmpGridViewListaGeral.FocusedRowHandle >= 0))
                                    DevolveValores();
                                else
                                    this.Close();
                            }

                            break;
                        }

                    case Enums.Listagem.Fornecedor:
                        {
                            DataSource.DsListasTableAdapters.FornecedoresTableAdapter adp = new DataSource.DsListasTableAdapters.FornecedoresTableAdapter();
                            if (this.filter != "")
                                adp.FillByFiltro(Listagem.Fornecedores, this.filter);
                            else
                                adp.Fill(Listagem.Fornecedores);

                            vmpGridControlListaGeral.DataSource = Listagem.Fornecedores;
                            this.Text = "Fornecedores";
                            break;
                        }


                    case Enums.Listagem.SubFamilia:
                        {
                            DataSource.DsListasTableAdapters.SubFamiliasTableAdapter adp = new DataSource.DsListasTableAdapters.SubFamiliasTableAdapter();
                            if (this.filter != "")
                                adp.FillByFamilia(Listagem.SubFamilias, this.filter);
                            else if (this.filter == "" & Familia != "")
                                adp.FillByFamilia(Listagem.SubFamilias, Familia);
                            else
                                adp.Fill(Listagem.SubFamilias);

                            vmpGridControlListaGeral.DataSource = Listagem.SubFamilias;
                            this.Text = "Subfamília";
                            break;
                        }

                    case Enums.Listagem.Maquina:
                        {
                            DataSource.DsListasTableAdapters.CentrosTrabalhoOperacaoRotaTableAdapter adp = new DataSource.DsListasTableAdapters.CentrosTrabalhoOperacaoRotaTableAdapter();
                            if (this.filter != "" && this.Operacao != "")
                            {
                                adp.FillByIdRotaOperacao(Listagem.CentrosTrabalhoOperacaoRota, this.filter, this.Operacao);
                                vmpGridControlListaGeral.DataSource = Listagem.CentrosTrabalhoOperacaoRota;
                            }
                            else
                            {
                                Listagem.EnforceConstraints = false;
                                adp.Fill(Listagem.CentrosTrabalhoOperacaoRota);
                                vmpGridControlListaGeral.DataSource = Listagem.CentrosTrabalhoOperacaoRota;
                            }

                            this.Text = "Máquinas Operação Rota";
                            break;
                        }

                    case Enums.Listagem.Motivo:
                        {
                            DataSource.DsListasTableAdapters.MotivoTableAdapter adp = new DataSource.DsListasTableAdapters.MotivoTableAdapter();
                            adp.Fill(Listagem.Motivo);
                            vmpGridControlListaGeral.DataSource = Listagem.Motivo;
                            this.Text = "Motivo";
                            break;
                        }




                    case Enums.Listagem.ArtigoMundifios:
                        {
                            DataSource.DsListasTableAdapters.ArtigoMundifiosTableAdapter adp = new DataSource.DsListasTableAdapters.ArtigoMundifiosTableAdapter();
                            adp.Fill(Listagem.ArtigoMundifios);
                            vmpGridControlListaGeral.DataSource = Listagem.ArtigoMundifios;
                            this.Text = "Artigos";
                            break;
                        }

                    default:
                        {
                            throw new Exception("Não existe lista definida para " + ListagemTipo.ToString() + ".");

                        }
                }

                FormatarGrelhas();

                barStaticItemCount.Caption = vmpGridViewListaGeral.RowCount + " registo(s).";



                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DevolveValores()
        {
            try
            {
                // # apanhar linha do dataset
                if (vmpGridViewListaGeral.RowCount <= 0)
                    return;

                DataRow activeRow;
                activeRow = vmpGridViewListaGeral.GetDataRow(vmpGridViewListaGeral.FocusedRowHandle);

                string sDescricao = "";
                string sCodigo = "";
                string sId = "";

                switch (ListagemTipo)
                {

                    case Enums.Listagem.Dimensao:
                        {
                            sCodigo = activeRow["Dimensao"].ToString();
                            sDescricao = activeRow["Descricao"].ToString();
                            break;
                        }
                    case Enums.Listagem.TiposDimensao:
                        {
                            sCodigo = activeRow["Codigo"].ToString();
                            sDescricao = activeRow["Descricao"].ToString();
                            break;
                        }

                    case Enums.Listagem.Marca:
                        {
                            sCodigo = activeRow["Marca"].ToString();
                            sDescricao = activeRow["Descricao"] == DBNull.Value ? "" : activeRow["Descricao"].ToString();
                            break;
                        }

                    case Enums.Listagem.Programa:
                        {
                            sId = activeRow["Id"].ToString();
                            sCodigo = activeRow["Codigo"].ToString();
                            sDescricao = activeRow["Descricao"].ToString();
                            break;
                        }

                    case Enums.Listagem.MoradasAlternativas:
                        {
                            sCodigo = activeRow["MoradaAlternativa"].ToString();
                            sDescricao = activeRow["Morada"].ToString();
                            break;
                        }

                    case Enums.Listagem.Cliente:
                        {
                            sCodigo = activeRow["Cliente"].ToString();
                            sDescricao = activeRow["Nome"].ToString();
                            break;
                        }

                    case Enums.Listagem.Fornecedor:
                        {
                            sCodigo = activeRow["Fornecedor"].ToString();
                            sDescricao = activeRow["Nome"].ToString();
                            break;
                        }

                    case Enums.Listagem.CentroTrabalho:
                        {
                            sId = activeRow["PRD_IDCentroTrabalho"].ToString();
                            sCodigo = activeRow["PRD_CentroTrabalho"].ToString();
                            sDescricao = activeRow["PRD_Descricao"] == DBNull.Value ? "" : activeRow["PRD_Descricao"].ToString();
                            break;
                        }

                    case Enums.Listagem.SubFamilia:
                        {
                            sCodigo = activeRow["SubFamilia"].ToString();
                            sDescricao = activeRow["Descricao"].ToString();
                            break;
                        }

                    case Enums.Listagem.Qualidade:
                        {
                            sCodigo = activeRow["Qualidade"].ToString();

                            sId = activeRow["Onda"] == DBNull.Value ? "" : activeRow["Onda"].ToString();
                            TotlaPesoM2 = activeRow["TotalPesoM2"] == DBNull.Value ? 0 : Convert.ToDecimal(activeRow["TotalPesoM2"]);

                            string descricao = string.Empty;

                            if (activeRow["OuterLiner"].ToString() != "")
                                descricao = activeRow["OuterLiner"].ToString();

                            if (activeRow["Fluting1"].ToString() != "")
                            {
                                if (descricao == "")
                                    descricao = activeRow["Fluting1"].ToString();
                                else
                                    descricao += " - " + activeRow["Fluting1"].ToString();
                            }

                            if (activeRow["InnerLiner1"].ToString() != "")
                            {
                                if (descricao == "")
                                    descricao = activeRow["InnerLiner1"].ToString();
                                else
                                    descricao += " - " + activeRow["InnerLiner1"].ToString();
                            }

                            if (activeRow["Fluting2"].ToString() != "")
                            {
                                if (descricao == "")
                                    descricao = activeRow["Fluting2"].ToString();
                                else
                                    descricao += " - " + activeRow["Fluting2"].ToString();
                            }

                            if (activeRow["InnerLiner2"].ToString() != "")
                            {
                                if (descricao == "")
                                    descricao = activeRow["InnerLiner2"].ToString();
                                else
                                    descricao += " - " + activeRow["InnerLiner2"].ToString();
                            }

                            if (activeRow["Fluting3"].ToString() != "")
                            {
                                if (descricao == "")
                                    descricao = activeRow["Fluting3"].ToString();
                                else
                                    descricao += " - " + activeRow["Fluting3"].ToString();
                            }

                            if (activeRow["InnerLiner3"].ToString() != "")
                            {
                                if (descricao == "")
                                    descricao = activeRow["InnerLiner3"].ToString();
                                else
                                    descricao += " - " + activeRow["InnerLiner3"].ToString();
                            }

                            if (activeRow["Fluting4"].ToString() != "")
                            {
                                if (descricao == "")
                                    descricao = activeRow["Fluting4"].ToString();
                                else
                                    descricao += " - " + activeRow["Fluting4"].ToString();
                            }

                            if (activeRow["InnerLiner4"].ToString() != "")
                            {
                                if (descricao == "")
                                    descricao = activeRow["InnerLiner4"].ToString();
                                else
                                    descricao += " - " + activeRow["InnerLiner4"].ToString();
                            }

                            sDescricao = activeRow["descricao"].ToString(); // descricao
                            break;
                        }

                    case Enums.Listagem.Maquina:
                        {
                            sId = activeRow["PRD_IDCentroTrabalho"].ToString();
                            sCodigo = activeRow["PRD_CentroTrabalho"].ToString();
                            sDescricao = activeRow["PRD_Descricao"] == DBNull.Value ? "" : activeRow["PRD_Descricao"].ToString();
                            break;
                        }

                    case Enums.Listagem.Motivo:
                        {
                            sId = activeRow["Id"].ToString();
                            sCodigo = activeRow["Codigo"].ToString();
                            sDescricao = activeRow["Motivo"] == DBNull.Value ? "" : activeRow["Motivo"].ToString();
                            break;
                        }

                    case Enums.Listagem.Norma:
                        {
                            sId = activeRow["Id"].ToString();
                            sCodigo = activeRow["Codigo"].ToString();
                            sDescricao = activeRow["Descricao"] == DBNull.Value ? "" : activeRow["Descricao"].ToString();
                            break;
                        }

                    case Enums.Listagem.Rotulo:
                        {
                            sId = activeRow["Id"].ToString();
                            sCodigo = activeRow["Codigo"].ToString();
                            sDescricao = activeRow["Descricao"] == DBNull.Value ? "" : activeRow["Descricao"].ToString();
                            break;
                        }

                    case Enums.Listagem.Rota:
                        {
                            sId = activeRow["Id"].ToString();
                            sCodigo = activeRow["codigo"].ToString();
                            sDescricao = activeRow["Descricao"] == DBNull.Value ? "" : activeRow["Descricao"].ToString();
                            break;
                        }


                    case Enums.Listagem.ArtigoMundifios:
                        {
                            sCodigo = activeRow["Artigo"].ToString();
                            sDescricao = activeRow["Descricao"].ToString();
                            break;
                        }

                    case Enums.Listagem.ArtigoGeradorSeq:
                        {
                            sCodigo = activeRow["Artigo"].ToString();
                            break;
                        }

                    default:
                        break;
                }

                ListaParametros.SetData(sCodigo, sDescricao, sId);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Exception EX2 = ex;
            }
        }

        private void barButtonItemConfirmar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (vmpGridViewListaGeral.FocusedRowHandle < 0)
                return;
            DevolveValores();
            this.DialogResult = DialogResult.OK;
        }

        private void barButtonItemFechar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void FormatarGrelhas()
        {

            // # ======================================================================================
            {


                switch (ListagemTipo)
                {

                    case Enums.Listagem.ArtigoGeradorSeq:
                        {

                            vmpGridViewListaGeral.IniciarFormatacao(true);

                            vmpGridViewListaGeral.FormatarColuna("UnidadeBase", true, Caption: "Unidade Base");

                            vmpGridViewListaGeral.FormatarColuna("Familia", true, Caption: "Família");

                            vmpGridViewListaGeral.FormatarColuna("Artigo", true, LarguraFixa: 100);

                            vmpGridViewListaGeral.FormatarColuna("SubFamilia", true, Caption: "Sub Família");

                            vmpGridViewListaGeral.FormatarColuna("Descricao", true, Caption: "Descrição", LarguraFixa: 0);

                            vmpGridViewListaGeral.AutoFillColumn = vmpGridViewListaGeral.Columns["Descricao"];

                            vmpGridViewListaGeral.FinalizarFormatacao();
                            break;
                        }

                    case Enums.Listagem.Dimensao:
                        {
                            vmpGridViewListaGeral.IniciarFormatacao(true);

                            vmpGridViewListaGeral.FormatarColuna("Dimensao", true, Caption: "Dimensão", LarguraFixa: 60);

                            vmpGridViewListaGeral.FormatarColuna("TipoDim", true, LarguraFixa: 50, Caption: "Tipo");

                            vmpGridViewListaGeral.FormatarColuna("Descricao", true, Caption: "Descrição", LarguraFixa: 0);

                            vmpGridViewListaGeral.AutoFillColumn = vmpGridViewListaGeral.Columns["Descricao"];

                            vmpGridViewListaGeral.FinalizarFormatacao();

                            //withBlock.ColunasNaoPersonalizaveis.Add("Dimensao");
                            //withBlock.ColunasNaoPersonalizaveis.Add("Descricao");

                            break;
                        }

                    case Enums.Listagem.Marca:
                        {
                            vmpGridViewListaGeral.IniciarFormatacao(true);

                            vmpGridViewListaGeral.FormatarColuna("Marca", true, LarguraFixa: 90);

                            vmpGridViewListaGeral.FormatarColuna("CDU_Descricao", true, Caption: "Descrição", LarguraFixa: 0);

                            vmpGridViewListaGeral.AutoFillColumn = vmpGridViewListaGeral.Columns["Descricao"];

                            vmpGridViewListaGeral.FinalizarFormatacao();

                            break;
                        }

                    case Enums.Listagem.Cliente:
                        {
                            vmpGridViewListaGeral.IniciarFormatacao(true);

                            vmpGridViewListaGeral.FormatarColuna("Cliente", true, LarguraFixa: 60);

                            vmpGridViewListaGeral.FormatarColuna("Nome", true, LarguraFixa: 150);

                            vmpGridViewListaGeral.AutoFillColumn = vmpGridViewListaGeral.Columns["Nome"];

                            vmpGridViewListaGeral.FinalizarFormatacao();

                            break;
                        }

                    case Enums.Listagem.Fornecedor:
                        {

                            vmpGridViewListaGeral.IniciarFormatacao(true);

                            vmpGridViewListaGeral.FormatarColuna("Fornecedor", LarguraFixa: 60);

                            vmpGridViewListaGeral.FormatarColuna("Nome", true, LarguraFixa: 150);

                            vmpGridViewListaGeral.AutoFillColumn = vmpGridViewListaGeral.Columns["Nome"];

                            vmpGridViewListaGeral.FinalizarFormatacao();

                            break;
                        }

                    case Enums.Listagem.CentroTrabalho:
                        {

                            vmpGridViewListaGeral.IniciarFormatacao(true);

                            vmpGridViewListaGeral.FormatarColuna("PRD_IDCentroTrabalho", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("PRD_CentroTrabalho", Visivel: true, Caption: "Máquina", LarguraFixa: 60);

                            vmpGridViewListaGeral.FormatarColuna("PRD_Descricao", Visivel: true, Caption: "Descrição", LarguraFixa: 0);

                            vmpGridViewListaGeral.AutoFillColumn = vmpGridViewListaGeral.Columns["PRD_Descricao"];

                            vmpGridViewListaGeral.FinalizarFormatacao();

                            //withBlock.ColunasNaoPersonalizaveis.Add("PRD_IDCentroTrabalho");

                            break;
                        }

                    case Enums.Listagem.Maquina:
                        {
                            vmpGridViewListaGeral.IniciarFormatacao(true);

                            vmpGridViewListaGeral.FormatarColuna("PRD_IDCentroTrabalho", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("Codigo", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("PRD_CentroTrabalho", Visivel: true, Caption: "Máquina", LarguraFixa: 60);

                            vmpGridViewListaGeral.FormatarColuna("PRD_Descricao", Visivel: true, Caption: "Descrição", LarguraFixa: 0);

                            vmpGridViewListaGeral.AutoFillColumn = vmpGridViewListaGeral.Columns["PRD_Descricao"];

                            vmpGridViewListaGeral.FinalizarFormatacao();

                            //withBlock.ColunasNaoPersonalizaveis.Add("PRD_IDCentroTrabalho");

                            break;
                        }

                    case Enums.Listagem.Motivo:
                        {
                            vmpGridViewListaGeral.IniciarFormatacao(true);

                            vmpGridViewListaGeral.FormatarColuna("Id", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("IdTipoMotivo", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("Codigo", Visivel: true, Caption: "Código", LarguraFixa: 60);

                            vmpGridViewListaGeral.FormatarColuna("Motivo", Visivel: true, Caption: "Descrição", LarguraFixa: 0);

                            vmpGridViewListaGeral.AutoFillColumn = vmpGridViewListaGeral.Columns["Motivo"];

                            vmpGridViewListaGeral.FinalizarFormatacao();

                            //withBlock.ColunasNaoPersonalizaveis.Add("Id");
                            //withBlock.ColunasNaoPersonalizaveis.Add("IdTipoMotivo");
                            break;
                        }

                    case Enums.Listagem.Norma:
                        {
                            vmpGridViewListaGeral.IniciarFormatacao(true);

                            vmpGridViewListaGeral.FormatarColuna("Id", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("Codigo", Visivel: true, Caption: "Código", LarguraFixa: 60);

                            vmpGridViewListaGeral.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição", LarguraFixa: 0);

                            vmpGridViewListaGeral.AutoFillColumn = vmpGridViewListaGeral.Columns["Descricao"];

                            vmpGridViewListaGeral.FinalizarFormatacao();

                            //withBlock.ColunasNaoPersonalizaveis.Add("Id");
                            break;
                        }

                    case Enums.Listagem.Programa:
                        {
                            vmpGridViewListaGeral.IniciarFormatacao(true);

                            vmpGridViewListaGeral.FormatarColuna("Id", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("Codigo", Visivel: true, Caption: "Código", LarguraFixa: 60);

                            vmpGridViewListaGeral.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição", LarguraFixa: 0);

                            vmpGridViewListaGeral.AutoFillColumn = vmpGridViewListaGeral.Columns["Descricao"];

                            vmpGridViewListaGeral.FinalizarFormatacao();

                            //withBlock.ColunasNaoPersonalizaveis.Add("Id");

                            break;
                        }

                    case Enums.Listagem.Qualidade:
                        {
                            vmpGridViewListaGeral.IniciarFormatacao(true);

                            vmpGridViewListaGeral.FormatarColuna("IdOnda", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("Tipo1", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("Tipo2", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("Tipo3", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("Tipo4", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("Tipo5", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("Tipo6", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("Tipo7", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("Tipo8", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("Tipo9", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("PressaoVincos", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("EspessuraCartao", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("GramagemCola", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("ECT", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("UtilizaCustosGenericos", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("Onda", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("Utilizador", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("UtilizadorAtualizacao", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("Posto", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("PostoAtualizacao", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("DataIntroducao", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("DataAtualizacao", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("EnviaPCTOPP", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("TotalPesoM2", Visivel: false);

                            vmpGridViewListaGeral.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição", LarguraFixa: 0);

                            vmpGridViewListaGeral.AutoFillColumn = vmpGridViewListaGeral.Columns["Descricao"];

                            vmpGridViewListaGeral.FormatarColuna("Qualidade", Visivel: true, LarguraFixa: 80);

                            vmpGridViewListaGeral.FormatarColuna("Descricao", Visivel: true, LarguraFixa: 200);

                            vmpGridViewListaGeral.FormatarColuna("OuterLiner", Visivel: true, LarguraFixa: 60);

                            vmpGridViewListaGeral.FormatarColuna("Fluting1", true, LarguraFixa: 50);

                            vmpGridViewListaGeral.FormatarColuna("Fluting2", true, LarguraFixa: 50);

                            vmpGridViewListaGeral.FormatarColuna("Fluting3", true, LarguraFixa: 50);

                            vmpGridViewListaGeral.FormatarColuna("Fluting4", true, LarguraFixa: 50);

                            vmpGridViewListaGeral.FormatarColuna("InnerLiner1", true, LarguraFixa: 60);

                            vmpGridViewListaGeral.FormatarColuna("InnerLiner2", true, LarguraFixa: 60);

                            vmpGridViewListaGeral.FormatarColuna("InnerLiner3", true, LarguraFixa: 60);

                            vmpGridViewListaGeral.FormatarColuna("InnerLiner4", true, LarguraFixa: 60);

                            vmpGridViewListaGeral.FormatarColuna("EspessuraCartao", true, LarguraFixa: 150);

                            vmpGridViewListaGeral.FinalizarFormatacao();
                            break;
                        }

                    case Enums.Listagem.SubFamilia:
                        {
                            vmpGridViewListaGeral.IniciarFormatacao(true);

                            vmpGridViewListaGeral.FormatarColuna("Familia", Visivel: false, LarguraFixa: 50);

                            vmpGridViewListaGeral.FormatarColuna("DescFamilia", Caption: "Descrição", Visivel: false, LarguraFixa: 150);

                            vmpGridViewListaGeral.FormatarColuna("SubFamilia", Visivel: false, LarguraFixa: 100);

                            vmpGridViewListaGeral.FormatarColuna("Descricao", Caption: "Descrição", LarguraFixa: 0);

                            vmpGridViewListaGeral.AutoFillColumn = vmpGridViewListaGeral.Columns["Descricao"];

                            vmpGridViewListaGeral.FinalizarFormatacao();

                            break;
                        }

                    case Enums.Listagem.Rota:
                        {

                            vmpGridViewListaGeral.IniciarFormatacao(true);


                            vmpGridViewListaGeral.FormatarColuna("Codigo", Caption: "Código", Visivel: false, LarguraFixa: 50);

                            vmpGridViewListaGeral.FormatarColuna("Observacoes", true, LarguraFixa: 550);

                            vmpGridViewListaGeral.FormatarColuna("Descricao", true, Caption: "Descrição", LarguraFixa: 0);

                            vmpGridViewListaGeral.AutoFillColumn = vmpGridViewListaGeral.Columns["Descricao"];

                            vmpGridViewListaGeral.FinalizarFormatacao();

                            //withBlock.ColunasNaoPersonalizaveis.Add("Id");

                            break;
                        }

                    case Enums.Listagem.Rotulo:
                        {

                            vmpGridViewListaGeral.IniciarFormatacao(true);


                            vmpGridViewListaGeral.FormatarColuna("Codigo", Caption: "Código", Visivel: false, LarguraFixa: 50);

                            vmpGridViewListaGeral.FormatarColuna("Observacoes", true, LarguraFixa: 550);

                            vmpGridViewListaGeral.FormatarColuna("Descricao", true, Caption: "Descrição", LarguraFixa: 0);

                            vmpGridViewListaGeral.AutoFillColumn = vmpGridViewListaGeral.Columns["Descricao"];

                            vmpGridViewListaGeral.FinalizarFormatacao();

                            //withBlock.ColunasNaoPersonalizaveis.Add("Id");

                            break;
                        }


                    case Enums.Listagem.ArtigoMundifios:
                        {
                            vmpGridViewListaGeral.IniciarFormatacao(true);

                            vmpGridViewListaGeral.FormatarColuna("Artigo", Visivel: true);

                            vmpGridViewListaGeral.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição", LarguraFixa: 0);

                            vmpGridViewListaGeral.FormatarColuna("UnidadeBase", Visivel: true, Caption: "Unidade Base");

                            vmpGridViewListaGeral.FormatarColuna("Familia", Visivel: false, Caption: "Família");

                            //vmpGridViewListaGeral.FormatarColuna("subFamilia", Visivel: false, Caption: "Sub Família");

                            vmpGridViewListaGeral.FormatarColuna("CDU_NE", Visivel: false, Caption: "NE");

                            vmpGridViewListaGeral.FormatarColuna("NEDesc", Visivel: true, Caption: "NE");

                            vmpGridViewListaGeral.FormatarColuna("CDU_Texturizacao", Visivel: false, Caption: "Texturização");

                            vmpGridViewListaGeral.FormatarColuna("TexturizacaoDesc", Visivel: true, Caption: "Texturização");

                            vmpGridViewListaGeral.FormatarColuna("Componentes", Visivel: true, Caption: "Componentes");

                            vmpGridViewListaGeral.FormatarColuna("FamiliaDesc", true, Caption: "Família");

                            vmpGridViewListaGeral.FormatarColuna("SubFamiliaDesc", true, Caption: "Sub Família");

                            vmpGridViewListaGeral.FormatarColuna("PercentagemComponentes", Visivel: true, Caption: "% Componentes");

                            vmpGridViewListaGeral.AutoFillColumn = vmpGridViewListaGeral.Columns["Descricao"];

                            vmpGridViewListaGeral.FinalizarFormatacao();
                            break;
                        }
                    default:
                        break;
                }
            }
        }


    }
}
