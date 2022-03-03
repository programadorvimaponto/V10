using ADODB;
using Microsoft.VisualBasic;
using Primavera.Extensibility.CustomForm;
using System;
using System.Data;
using System.Windows.Forms;

namespace GrupoMundifios.Formulários
{
    public partial class FrmPesagensPendentesView : CustomForm
    {
        // --------------------------------------------------------------------------
        // # variáveis fixas
        // --------------------------------------------------------------------------
        // ---------------------------------------------------------------------------
        // # Variavel que indica o ID de Pesagem Selecionada na Lista
        private string ID_PESAGEM;

        public FrmPesagensPendentesView()
        {
            InitializeComponent();
        }

        private void barButtonItemApagar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (vmpGridViewPesagens.SelectedRowsCount >0)
            {
                if (MessageBox.Show("Deseja eliminar a pesagem selecionada na lista ?", "Informação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ApagaRegisto();
                }
            }
        }

        private void barButtonItemAtualizar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ID_PESAGEM = "";
            // # carregar lista de documentos
            CarregarDocumentos();
        }

        private void FrmPesagensPendentesView_Load(object sender, EventArgs e)
        {
            if (FindForm() is Form pai)
            {
                pai.MinimumSize = pai.Size;
                pai.MaximumSize = pai.Size;
            }

            // # datas default
            this.dateEditInicio.EditValue = DateAndTime.Day(new DateTime()) + "/" + DateAndTime.Month(new DateTime()) + "/" + DateAndTime.Year(DateAndTime.Now);
            this.dateEditFim.EditValue = DateAndTime.Day(DateAndTime.Now) + "/" + DateAndTime.Month(DateAndTime.Now) + "/" + DateAndTime.Year(DateAndTime.Now);
            // # Inicia Variavel a Vazio
            ID_PESAGEM = "";

            CarregarDocumentos();
        }

        public void CarregarDocumentos()
        {
            DataTable dtCarregaArtigos = new DataTable();

            string str_dtpini;
            string str_dtpfim;

            str_dtpini = dateEditInicio.DateTime.Year + "-" + dateEditInicio.DateTime.Month + "-" + dateEditInicio.DateTime.Day + " 00:00:00";
            str_dtpfim = dateEditFim.DateTime.Year + "-" + dateEditFim.DateTime.Month + "-" + dateEditFim.DateTime.Day + " 23:59:59";
            dtCarregaArtigos = BSO.ConsultaDataTable("SELECT SCR.CDU_ArtigoReceber Artigo, A.Descricao Descrição, " + "SCR.CDU_Lote Lote, P.PesoLiquido [Peso Líquido], P.PesoBruto [Peso Bruto], " + "T.Turno Turno, O.PRD_Nome Operador, CONVERT(DATETIME, P.DataIntroducao, 102) [Data Pesagem], " + "ttr.descricao,tr.descricao, pd.quantidade,P.Id ID " + " FROM " + " VMP_PLA_Pesagem P " + " LEFT OUTER JOIN VMP_PLA_PesagemDetalhe PD ON p.id=pd.idpesagem " + " LEFT OUTER JOIN VMP_PLA_Tara TR ON pd.idtara=tr.id " + " LEFT OUTER JOIN VMP_PLA_TiposTara TTR ON tr.idtipotara=ttr.id " + " INNER JOIN VMP_PLA_Operadores O ON O.PRD_Operador = P.Utilizador " + " INNER JOIN VMP_PLA_Turnos T ON T.Id = O.IdTurno " + " INNER JOIN TDU_SecLinhasSubContratRecepcao SCR ON SCR.CDU_Id = P.IdLinhaArtigo " + " INNER JOIN Artigo A ON A.Artigo = SCR.CDU_ArtigoReceber " + " WHERE " + " P.Estado = 0 " + " AND P.DataIntroducao >= '" + str_dtpini + "' AND P.DataIntroducao <= '" + str_dtpfim + "'" + " ORDER BY P.DataIntroducao ");

            vmpGridControlPesagens.DataSource = dtCarregaArtigos;

            FormatarGrelha();
        }

        private static object afetados = 0;

        public void ApagaRegisto()
        {
            string sSQL;
            string sID;
            var StringSQL = new Command();
            if (ID_PESAGEM == "")
            {
                Interaction.MsgBox("Não foi selecionada uma Pesagem para apagar." + Constants.vbNewLine + "Operação cancelada", Constants.vbInformation);
                return;
            }

            // # não faço validação pois o ecrã só apanha estados a zero e significa que o fabrico foi anulado...
            sSQL = "DELETE FROM VMP_PLA_MovimentosPesagem WHERE IdPesagem ='" + ID_PESAGEM + "'";
            BSO.DSO.ExecuteSQL(sSQL);

            // ' Elimina VMP_PLA_PesagemDetalhe pelo ID de Pesagem selecionado  na lista
            sSQL = "DELETE FROM VMP_PLA_PesagemDetalhe WHERE IdPesagem ='" + ID_PESAGEM + "'";
            BSO.DSO.ExecuteSQL(sSQL);


            // ' Elimina VMP_PLA_Pesagem pelo ID de Pesagem selecionado  na lista
            sSQL = "DELETE FROM VMP_PLA_Pesagem WHERE Id ='" + ID_PESAGEM + "'";
            BSO.DSO.ExecuteSQL(sSQL);
            ID_PESAGEM = "";
            CarregarDocumentos();
        }

        public void FormatarGrelha()
        {
            var withBlock = vmpGridViewPesagens;
            {
                withBlock.IniciarFormatacao(true);
                withBlock.FormatarColuna("Artigo", true, false, true, "Artigo", 100, DevExpress.Utils.FormatType.None, default);
                withBlock.FormatarColuna("Descrição", true, false, true, "Descrição", 200, DevExpress.Utils.FormatType.None, default);
                withBlock.FormatarColuna("Lote", true, true, true, "Lote", 80, DevExpress.Utils.FormatType.None, default);
                withBlock.FormatarColuna("Peso Líquido", true, false, true, "Peso Líquido", 50, DevExpress.Utils.FormatType.None, default);
                withBlock.FormatarColuna("Peso Bruto", true, false, true, "Peso Bruto", 50, DevExpress.Utils.FormatType.None, default);
                withBlock.FormatarColuna("Turno", true, false, true, "Turno", 80, DevExpress.Utils.FormatType.None, default);
                withBlock.FormatarColuna("Operador", true, false, true, "Operador", 80, DevExpress.Utils.FormatType.None, default);
                withBlock.FormatarColuna("Data Pesagem", true, false, true, "Data Pesagem", 100, DevExpress.Utils.FormatType.None, default);
                withBlock.FormatarColuna("ID", false, false, true, "ID", 0, DevExpress.Utils.FormatType.None, default);

                withBlock.AutoFillColumn = withBlock.Columns["Data Pesagem"];
                withBlock.FinalizarFormatacao();
            }
        }

        private void vmpGridViewPesagens_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ID_PESAGEM = vmpGridViewPesagens.GetFocusedRowCellValue("ID").ToString();

        }

    }
}