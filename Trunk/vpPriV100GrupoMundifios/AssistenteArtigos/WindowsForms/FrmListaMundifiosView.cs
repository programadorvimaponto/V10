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
using Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.Class;
using Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.DataSource;
using static Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.Class.Geral;

namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos
{
    public partial class frmListaMundifiosView : CustomForm
    {
        private DsListasMundifios Listagem;
        private ListagemMundifios ListagemTipo;
        private ListParameter ListaParametros;
        private string filter;
        private string ConnectionString;

        public frmListaMundifiosView()
        {

            // This call is required by the designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            Listagem = new DsListasMundifios();
        }

        public DialogResult ShowDialog(ListagemMundifios ListagemTipo, ListParameter ListaParametros, string Connection) // , Optional ByVal Filtro As String = "")
        {
            this.ListagemTipo = ListagemTipo;
            this.ListaParametros = ListaParametros;
            this.filter = ""; // Filtro
                              // # instanciar dataset geral
            Listagem = new DsListasMundifios();
            ConnectionString = Connection;
            try
            {
                this.Text = ListagemTipo.ToString();

                switch (ListagemTipo)
                {
                    case ListagemMundifios.Armazens:
                        {
                            DataSource.DsListasMundifiosTableAdapters.ArmazensTableAdapter adpt = new DataSource.DsListasMundifiosTableAdapters.ArmazensTableAdapter();
                            adpt.Connection.ConnectionString = ConnectionString;
                            adpt.Fill(Listagem.Armazens);
                            vmpGridControlListaMundifios.DataSource = Listagem.Armazens;
                            break;
                        }

                    case ListagemMundifios.TipoArtigo:
                        {
                            DataSource.DsListasMundifiosTableAdapters.TiposArtigoTableAdapter adpt = new DataSource.DsListasMundifiosTableAdapters.TiposArtigoTableAdapter();
                            adpt.Connection.ConnectionString = ConnectionString;
                            adpt.Fill(Listagem.TiposArtigo);
                            vmpGridControlListaMundifios.DataSource = Listagem.TiposArtigo;
                            break;
                        }

                    case ListagemMundifios.GrupoTaxaDesperdicio:
                        {
                            DataSource.DsListasMundifiosTableAdapters.TDU_GrupoTaxaDesperdicioTableAdapter adpt = new DataSource.DsListasMundifiosTableAdapters.TDU_GrupoTaxaDesperdicioTableAdapter();
                            adpt.Connection.ConnectionString = ConnectionString;
                            adpt.Fill(Listagem.TDU_GrupoTaxaDesperdicio);
                            vmpGridControlListaMundifios.DataSource = Listagem.TDU_GrupoTaxaDesperdicio;
                            break;
                        }

                    case ListagemMundifios.IntrastatPautal:
                        {
                            DataSource.DsListasMundifiosTableAdapters.IntrastatMercadoriaTableAdapter adpt = new DataSource.DsListasMundifiosTableAdapters.IntrastatMercadoriaTableAdapter();
                            adpt.Connection.ConnectionString = ConnectionString;
                            adpt.Fill(Listagem.IntrastatMercadoria);
                            vmpGridControlListaMundifios.DataSource = Listagem.IntrastatMercadoria;
                            break;
                        }

                    case ListagemMundifios.CodigoAntigo:
                        {
                            DataSource.DsListasMundifiosTableAdapters.ArtigoTableAdapter adpt = new DataSource.DsListasMundifiosTableAdapters.ArtigoTableAdapter();
                            adpt.Connection.ConnectionString = ConnectionString;
                            adpt.Fill(Listagem.Artigo);
                            vmpGridControlListaMundifios.DataSource = Listagem.Artigo;
                            break;
                        }

                    default:
                        {
                            throw new Exception("Não existe lista definida para " + ListagemTipo.ToString() + ".");

                        }
                }

                FormatarGrelhas();
            }

            // Me.lblCount.Text = Me.GridList.RowCount & " registo(s)."

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return base.ShowDialog();
        }

        public void Show(ListagemMundifios ListagemTipo, ListParameter ListaParametros, string Filtro = "")
        {
            this.ListagemTipo = ListagemTipo;
            this.ListaParametros = ListaParametros;
            this.filter = Filtro;
            base.Show();
        }

        private void FrmListaMundifios_Load(object sender, EventArgs e)
        {
           
        }

        private void FormatarGrelhas()
        {
            try
            {


                // # ======================================================================================
                {


                    switch (ListagemTipo)
                    {
                        case ListagemMundifios.Armazens:
                            {
                                vmpGridViewListaMundifios.IniciarFormatacao(true);

                                vmpGridViewListaMundifios.FormatarColuna("Armazem",Visivel:true, Caption: "Armazém");

                                vmpGridViewListaMundifios.FormatarColuna("Morada", Visivel: true, Caption: "Morada");

                                vmpGridViewListaMundifios.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição", LarguraFixa: 0);

                                vmpGridViewListaMundifios.AutoFillColumn = vmpGridViewListaMundifios.Columns["Descricao"];

                                vmpGridViewListaMundifios.FinalizarFormatacao();

                                break;
                            }

                        case ListagemMundifios.TipoArtigo:
                            {

                                vmpGridViewListaMundifios.IniciarFormatacao(true);

                                vmpGridViewListaMundifios.FormatarColuna("TipoArtigo", Visivel: true, Caption: "Tipo Artigo");

                                vmpGridViewListaMundifios.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição", LarguraFixa: 0);

                                vmpGridViewListaMundifios.AutoFillColumn = vmpGridViewListaMundifios.Columns["Descricao"];

                                vmpGridViewListaMundifios.FinalizarFormatacao();

                                break;
                            }

                        case ListagemMundifios.GrupoTaxaDesperdicio:
                            {
                                vmpGridViewListaMundifios.IniciarFormatacao(true);

                                vmpGridViewListaMundifios.FormatarColuna("CDU_Codigo", Visivel: true, Caption: "Código");

                                vmpGridViewListaMundifios.FormatarColuna("CDU_Taxa", Visivel: true, Caption: "Taxa");

                                vmpGridViewListaMundifios.FormatarColuna("CDU_Descricao", Visivel: true, Caption: "Descrição", LarguraFixa: 0);

                                vmpGridViewListaMundifios.AutoFillColumn = vmpGridViewListaMundifios.Columns["Descricao"];

                                vmpGridViewListaMundifios.FinalizarFormatacao();
                                break;
                            }

                        case ListagemMundifios.IntrastatPautal:
                            {
                                vmpGridViewListaMundifios.IniciarFormatacao(true);

                                vmpGridViewListaMundifios.FormatarColuna("Mercadoria", Visivel: true, Caption: "Intrastat");

                                vmpGridViewListaMundifios.FormatarColuna("DescricaoInterna", Visivel: true, Caption: "Descrição Interna");

                                vmpGridViewListaMundifios.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição", LarguraFixa: 0);

                                vmpGridViewListaMundifios.AutoFillColumn = vmpGridViewListaMundifios.Columns["Descricao"];

                                vmpGridViewListaMundifios.FinalizarFormatacao();
                                break;
                            }

                        case ListagemMundifios.CodigoAntigo:
                            {
                                vmpGridViewListaMundifios.IniciarFormatacao(true);

                                vmpGridViewListaMundifios.FormatarColuna("Artigo", Visivel: true, Caption: "Artigo");

                                vmpGridViewListaMundifios.FormatarColuna("UnidadeVenda", Visivel: true, Caption: "Unidade Venda");

                                vmpGridViewListaMundifios.FormatarColuna("UnidadeBase", Visivel: true, Caption: "Unidade Base");

                                vmpGridViewListaMundifios.FormatarColuna("Iva", Visivel: true, Caption: "Iva");

                                vmpGridViewListaMundifios.FormatarColuna("Descricao", Visivel: true, Caption: "Descrição", LarguraFixa: 0);

                                vmpGridViewListaMundifios.AutoFillColumn = vmpGridViewListaMundifios.Columns["Descricao"];

                                vmpGridViewListaMundifios.FinalizarFormatacao();

                                break;
                            }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DevolveValores()
        {
            try
            {
                // # apanhar linha do dataset

                DataRow _r;
                if (vmpGridViewListaMundifios.FocusedRowHandle == 0)
                    return;
                _r = vmpGridViewListaMundifios.GetDataRow(vmpGridViewListaMundifios.FocusedRowHandle);

                string sId = "";
                string sCodigo = "";
                string sDescricao = "";


                switch (ListagemTipo)
                {
                    case ListagemMundifios.Armazens:
                        {
                            sCodigo =  vmpGridViewListaMundifios.GetRowCellValue(vmpGridViewListaMundifios.FocusedRowHandle, "Armazem").ToString();
                            sDescricao = vmpGridViewListaMundifios.GetRowCellValue(vmpGridViewListaMundifios.FocusedRowHandle, "Descricao").ToString();
                            break;
                        }

                    case ListagemMundifios.TipoArtigo:
                        {
                            sCodigo = vmpGridViewListaMundifios.GetRowCellValue(vmpGridViewListaMundifios.FocusedRowHandle, "TipoArtigo").ToString();
                            sDescricao = vmpGridViewListaMundifios.GetRowCellValue(vmpGridViewListaMundifios.FocusedRowHandle, "Descricao").ToString();
                            break;
                        }

                    case ListagemMundifios.GrupoTaxaDesperdicio:
                        {
                            sCodigo = vmpGridViewListaMundifios.GetRowCellValue(vmpGridViewListaMundifios.FocusedRowHandle, "CDU_Codigo").ToString();
                            sDescricao = vmpGridViewListaMundifios.GetRowCellValue(vmpGridViewListaMundifios.FocusedRowHandle, "CDU_Descricao").ToString();
                            sId = vmpGridViewListaMundifios.GetRowCellValue(vmpGridViewListaMundifios.FocusedRowHandle, "CDU_Taxa").ToString();
                            break;
                        }

                    case ListagemMundifios.IntrastatPautal:
                        {
                            sCodigo = vmpGridViewListaMundifios.GetRowCellValue(vmpGridViewListaMundifios.FocusedRowHandle, "Mercadoria").ToString();
                            sDescricao = vmpGridViewListaMundifios.GetRowCellValue(vmpGridViewListaMundifios.FocusedRowHandle, "Descricao").ToString();
                            break;
                        }

                    case ListagemMundifios.CodigoAntigo:
                        {
                            sCodigo = vmpGridViewListaMundifios.GetRowCellValue(vmpGridViewListaMundifios.FocusedRowHandle, "Artigo").ToString();
                            sDescricao = vmpGridViewListaMundifios.GetRowCellValue(vmpGridViewListaMundifios.FocusedRowHandle, "Descricao").ToString();
                            break;
                        }

                    default:
                        {
                            //sId = vmpGridViewListaMundifios.GetRowCellValue(vmpGridViewListaMundifios.FocusedRowHandle, 0).ToString();
                            //sCodigo = vmpGridViewListaMundifios.GetRowCellValue(vmpGridViewListaMundifios.FocusedRowHandle, 1).ToString();
                            //sDescricao = vmpGridViewListaMundifios.GetRowCellValue(vmpGridViewListaMundifios.FocusedRowHandle, 2).ToString();
                            break;
                        }
                }

                ListaParametros.SetData(sCodigo, sDescricao, sId);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Botoes Menu

        private void barButtonItemConfirmar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevolveValores();
        }

        private void barButtonItemFechar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region
        private void vmpGridViewListaMundifios_DoubleClick(object sender, EventArgs e)
        {
            DevolveValores();
        }

        private void vmpGridViewListaMundifios_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F4)
                    DevolveValores();
                else if (e.KeyCode == Keys.Escape)
                    this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion



    }
}
