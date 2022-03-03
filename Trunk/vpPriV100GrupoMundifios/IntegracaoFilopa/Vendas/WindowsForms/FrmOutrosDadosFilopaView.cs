using CmpBE100;
using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.CustomForm;
using StdBE100;
using System;
using System.Data;
using System.Windows.Forms;
using VndBE100;

namespace IntegracaoFilopa
{
    public partial class FrmOutrosDadosFilopaView : CustomForm
    {
        public FrmOutrosDadosFilopaView()
        {
            InitializeComponent();
        }

        private int j;
        private int LinhaActualizar;
        private int Verifica;
        private StdBELista ListaSituacao;
        private string SqlStringSituacao;
        private StdBELista ListaTipoQualidade;
        private string SqlStringTipoQualidade;
        private StdBELista ListaPais;
        private string SqlStringPais;
        private StdBELista ListaCompMaritima;
        private string SqlStringCompMaritima;
        private StdBELista ListaPorto;
        private string SqlStringPorto;

        // Rui Fernandes: 2019/08/06
        private StdBELista ListaDestino;

        private string SqlStringDestino;
        private string Situacao;
        private string TipoQualidade;
        private string Pais;
        private string CompMaritima;
        private string Porto;

        // Rui Fernandes: 2019/08/06
        private string Destino;

        private string TestaStr;
        private string ValidaStr;
        private string Companhia;
        private string CompanhiaStr;

        private string SituacaoStr;
        private string TipoQualidadeStr;
        private string PaisStr;
        private string CompMaritimaStr;
        private string PortoStr;
        DateTime DataPrevistaChegada;

        // Rui Fernandes: 2019/08/06
        private string DestinoStr;

        public VndBEDocumentoVenda DocumentoVenda { get; set; }

        private void checkEditCompMaritima_EditValueChanged(object sender, EventArgs e)
        {
            if (Verifica == 0)
            {
                if ((bool)this.checkEditCompMaritima.EditValue == false)
                {
                    this.checkEditPorto.EditValue = false;
                }
            }
        }

        private void checkEditPorto_EditValueChanged(object sender, EventArgs e)
        {
            if (Verifica == 0)
            {
                if ((bool)this.checkEditPorto.EditValue == true)
                {
                    this.checkEditCompMaritima.EditValue = true;
                }
            }
        }



        private void FrmOutrosDadosView_Load(object sender, EventArgs e)
        {
            if (FindForm() is Form pai)
            {
                pai.MinimumSize = pai.Size;
                pai.MaximumSize = pai.Size;
            }
            LimpaCampos();
            CarregaCombos();

            this.textEditArtigo.EditValue = Module1.ArtigoEnc;
            this.textEditDescArt.EditValue = Module1.DescArtEnc;
            this.textEditLote.EditValue = Module1.LoteEnc;
            this.textEditLinha.EditValue = Module1.LinhaEnc;

            this.textEditLoteForn.EditValue = DocumentoVenda.Linhas.GetEdita(Module1.LinhaEnc).CamposUtil["CDU_LoteFornecedor"].Valor + "";
            this.textEditNumContentor.EditValue = DocumentoVenda.Linhas.GetEdita(Module1.LinhaEnc).CamposUtil["CDU_NContentor"].Valor + "";
            this.textEditNumBL.EditValue = DocumentoVenda.Linhas.GetEdita(Module1.LinhaEnc).CamposUtil["CDU_NumBL"].Valor + "";
            // Rui Fernandes: 2019/08/06
            // Me.TxtDestino.Text = EditorCompras.DocumentoCompra.Linhas(LinhaEnc).CamposUtil("CDU_Destino").Valor & ""
            this.textEditSeguradora.EditValue = DocumentoVenda.Linhas.GetEdita(Module1.LinhaEnc).CamposUtil["CDU_Seguradora"].Valor + "";
            this.textEditNumCertificado.EditValue = DocumentoVenda.Linhas.GetEdita(Module1.LinhaEnc).CamposUtil["CDU_NumCertificado"].Valor + "";
            this.memoEditObservacoes.EditValue = DocumentoVenda.Linhas.GetEdita(Module1.LinhaEnc).CamposUtil["CDU_ObsLote"].Valor + "";
            textEditFaturaForn.EditValue = DocumentoVenda.Linhas.GetEdita(Module1.LinhaEnc).CamposUtil["CDU_NFatura"].Valor + "";

            Situacao = DocumentoVenda.Linhas.GetEdita(Module1.LinhaEnc).CamposUtil["CDU_Situacao"].Valor + "";
            TipoQualidade = DocumentoVenda.Linhas.GetEdita(Module1.LinhaEnc).CamposUtil["CDU_TipoQualidade"].Valor + "";
            Pais = DocumentoVenda.Linhas.GetEdita(Module1.LinhaEnc).CamposUtil["CDU_PaisOrigem"].Valor + "";
            CompMaritima = DocumentoVenda.Linhas.GetEdita(Module1.LinhaEnc).CamposUtil["CDU_CompanhiaMaritima"].Valor + "";
            Porto = DocumentoVenda.Linhas.GetEdita(Module1.LinhaEnc).CamposUtil["CDU_Porto"].Valor + "";
            // Rui Fernandes: 2019/08/06
            Destino = DocumentoVenda.Linhas.GetEdita(Module1.LinhaEnc).CamposUtil["CDU_Destino"].Valor + "";

            ColocaDadosCombo();

            if (DocumentoVenda.Linhas.GetEdita(Module1.LinhaEnc).DataEntrega + "" == "")
            {
                this.dateEditEmbarque.EditValue = DateAndTime.Now;
            }
            else
            {
                this.dateEditEmbarque.EditValue = Strings.Format(DocumentoVenda.Linhas.GetEdita(Module1.LinhaEnc).DataEntrega, "yyyy-MM-dd");
            }

            if (DocumentoVenda.Linhas.GetEdita(Module1.LinhaEnc).CamposUtil["CDU_DataPrevistaChegada"].Valor + "" == "")
            {
                this.dateEditLimiteEmbarque.EditValue = DateAndTime.Now;
            }
            else
            {
                this.dateEditLimiteEmbarque.EditValue = Strings.Format(DateTime.Parse(DocumentoVenda.Linhas.GetEdita(Module1.LinhaEnc).CamposUtil["CDU_DataPrevistaChegada"].Valor.ToString()), "yyyy-MM-dd");
            }

            //this.simpleButtonTodas.Text = "Todas as" + '\r' + "Linhas";

            Strings.Format(DateTime.Parse(this.dateEditLimiteEmbarque.EditValue.ToString()), "yyyy-MM-dd");

            this.lookUpEditSituacao.Focus();
        }

        private void ColocaDadosCombo()
        {
            SqlStringSituacao = "SELECT * FROM TDU_SITUACOESLOTE WHERE CDU_SITUACAO = '" + Situacao + "' ORDER BY CDU_SITUACAO DESC";

            ListaSituacao = BSO.Consulta(SqlStringSituacao);

            if (ListaSituacao.Vazia() == false)
            {
                ListaSituacao.Inicio();

                lookUpEditSituacao.EditValue = ListaSituacao.Valor("CDU_Situacao") + " - " + ListaSituacao.Valor("CDU_Descricao");
                lookUpEditSituacao.Properties.DisplayMember = "CDU_Situacao";
                lookUpEditSituacao.Properties.ValueMember = "CDU_Situacao";
            }

            SqlStringTipoQualidade = "SELECT * FROM TDU_TIPOQUALIDADE WHERE CDU_TIPOQUALIDADE = '" + TipoQualidade + "' ORDER BY CDU_TIPOQUALIDADE DESC";

            ListaTipoQualidade = BSO.Consulta(SqlStringTipoQualidade);

            if (ListaTipoQualidade.Vazia() == false)
            {
                ListaTipoQualidade.Inicio();

                lookUpEditTipoQualidade.EditValue = ListaTipoQualidade.Valor("CDU_TipoQualidade") + " - " + ListaTipoQualidade.Valor("CDU_Descricao");
                lookUpEditTipoQualidade.Properties.DisplayMember = "CDU_TipoQualidade";
                lookUpEditTipoQualidade.Properties.ValueMember = "CDU_TipoQualidade";
            }

            SqlStringPais = "SELECT * FROM PAISES WHERE PAIS = '" + Pais + "' ORDER BY PAIS DESC";

            ListaPais = BSO.Consulta(SqlStringPais);

            if (ListaPais.Vazia() == false)
            {
                ListaPais.Inicio();

                lookUpEditPais.EditValue = ListaPais.Valor("Pais") + " - " + ListaPais.Valor("Descricao");
                lookUpEditPais.Properties.DisplayMember = "Pais";
                lookUpEditPais.Properties.ValueMember = "Pais";
            }

            SqlStringCompMaritima = "SELECT * FROM TDU_CompanhiasMaritimas WHERE CDU_CODIGO = '" + CompMaritima + "' ORDER BY CDU_CODIGO DESC";

            ListaCompMaritima = BSO.Consulta(SqlStringCompMaritima);

            if (ListaCompMaritima.Vazia() == false)
            {
                ListaCompMaritima.Inicio();

                lookUpEditCompMaritima.EditValue = ListaCompMaritima.Valor("CDU_CODIGO") + " - " + ListaCompMaritima.Valor("CDU_Descricao");
                lookUpEditCompMaritima.Properties.DisplayMember = "CDU_CODIGO";
                lookUpEditCompMaritima.Properties.ValueMember = "CDU_CODIGO";
            }

            SqlStringPorto = "SELECT * FROM TDU_Locais WHERE CDU_Local = '" + Porto + "' ORDER BY CDU_Local DESC";

            ListaPorto = BSO.Consulta(SqlStringPorto);

            if (ListaPorto.Vazia() == false)
            {
                ListaPorto.Inicio();

                lookUpEditPorto.EditValue = ListaPorto.Valor("CDU_Local") + " - " + ListaPorto.Valor("CDU_Descricao");
                lookUpEditPorto.Properties.DisplayMember = "CDU_Local";
                lookUpEditPorto.Properties.ValueMember = "CDU_Local";
            }

            SqlStringDestino = "SELECT * FROM TDU_Locais WHERE CDU_Local = '" + Destino + "' ORDER BY CDU_Local DESC";

            ListaDestino = BSO.Consulta(SqlStringDestino);

            if (ListaDestino.Vazia() == false)
            {
                ListaDestino.Inicio();

                lookUpEditDestino.EditValue = ListaDestino.Valor("CDU_Local") + " - " + ListaDestino.Valor("CDU_Descricao");
                lookUpEditDestino.Properties.DisplayMember = "CDU_Local";
                lookUpEditDestino.Properties.ValueMember = "CDU_Local";
            }
        }

        private void LimpaCampos()
        {
            this.textEditLoteForn.EditValue = "";
            this.textEditNumContentor.EditValue = "";
            this.textEditNumBL.EditValue = "";
            // Rui Fernandes: 2019/08/06
            // Me.TxtDestino.Text = ""
            this.textEditSeguradora.EditValue = "";
            this.textEditNumCertificado.EditValue = "";
            this.memoEditObservacoes.EditValue = "";
            this.textEditFaturaForn.EditValue = "";

            this.lookUpEditSituacao.EditValue = "";
            this.lookUpEditTipoQualidade.EditValue = "";
            this.lookUpEditPais.EditValue = "";
            this.lookUpEditCompMaritima.EditValue = "";
            this.lookUpEditPorto.EditValue = "";
            // Rui Fernandes: 2019/08/06
            this.lookUpEditDestino.EditValue = "";
        }



        private void VerificaSituacao()
        {
            TestaStr = "";
            ValidaStr = "";

            TestaStr = this.lookUpEditSituacao.EditValue.ToString();

            for (var j = 1; j <= Strings.Len(this.lookUpEditSituacao.EditValue); j++)
            {
                ValidaStr = Strings.Left(TestaStr, 1);

                if (ValidaStr == " ")
                {
                    SituacaoStr = Strings.Left(this.lookUpEditSituacao.EditValue.ToString(), j - 1);
                    break;
                }
                else
                    TestaStr = Strings.Right(TestaStr, Strings.Len(this.lookUpEditSituacao.EditValue) - j);
            }
        }

        private void VerificaTipoQualidade()
        {
            TestaStr = "";
            ValidaStr = "";

            TestaStr = this.lookUpEditTipoQualidade.EditValue.ToString();

            for (var j = 1; j <= Strings.Len(this.lookUpEditTipoQualidade.EditValue); j++)
            {
                ValidaStr = Strings.Left(TestaStr, 1);

                if (ValidaStr == " ")
                {
                    TipoQualidadeStr = Strings.Left(this.lookUpEditTipoQualidade.EditValue.ToString(), j - 1);
                    break;
                }
                else
                    TestaStr = Strings.Right(TestaStr, Strings.Len(this.lookUpEditTipoQualidade.EditValue) - j);
            }
        }

        private void VerificaPais()
        {
            TestaStr = "";
            ValidaStr = "";

            TestaStr = this.lookUpEditPais.EditValue.ToString();

            for (var j = 1; j <= Strings.Len(this.lookUpEditPais.EditValue); j++)
            {
                ValidaStr = Strings.Left(TestaStr, 1);

                if (ValidaStr == " ")
                {
                    PaisStr = Strings.Left(this.lookUpEditPais.EditValue.ToString(), j - 1);
                    break;
                }
                else
                    TestaStr = Strings.Right(TestaStr, Strings.Len(this.lookUpEditPais.EditValue) - j);
            }
        }

        private void VerificaCompMaritima()
        {
            TestaStr = "";
            ValidaStr = "";
            if (lookUpEditCompMaritima.EditValue != null)
                TestaStr = this.lookUpEditCompMaritima.EditValue.ToString();

            for (var j = 1; j <= Strings.Len(this.lookUpEditCompMaritima.EditValue); j++)
            {
                ValidaStr = Strings.Left(TestaStr, 1);

                if (ValidaStr == " ")
                {
                    CompMaritimaStr = Strings.Left(this.lookUpEditCompMaritima.EditValue.ToString(), j - 1);
                    break;
                }
                else
                    TestaStr = Strings.Right(TestaStr, Strings.Len(this.lookUpEditCompMaritima.EditValue) - j);
            }
        }

        private void VerificaPorto()
        {
            TestaStr = "";
            ValidaStr = "";

            TestaStr = this.lookUpEditPorto.EditValue.ToString();

            for (var j = 1; j <= Strings.Len(this.lookUpEditPorto.EditValue); j++)
            {
                ValidaStr = Strings.Left(TestaStr, 1);

                if (ValidaStr == " ")
                {
                    PortoStr = Strings.Left(this.lookUpEditPorto.EditValue.ToString(), j - 1);
                    break;
                }
                else
                    TestaStr = Strings.Right(TestaStr, Strings.Len(this.lookUpEditPorto.EditValue) - j);
            }
        }

        // Rui Fernandes: 2019/08/06
        private void VerificaDestino()
        {
            TestaStr = "";
            ValidaStr = "";

            TestaStr = this.lookUpEditDestino.EditValue.ToString();

            for (var j = 1; j <= Strings.Len(this.lookUpEditDestino.EditValue); j++)
            {
                ValidaStr = Strings.Left(TestaStr, 1);

                if (ValidaStr == " ")
                {
                    DestinoStr = Strings.Left(this.lookUpEditDestino.EditValue.ToString(), j - 1);
                    break;
                }
                else
                    TestaStr = Strings.Right(TestaStr, Strings.Len(this.lookUpEditDestino.EditValue) - j);
            }
        }

        private void FrmOutrosDadosView_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {

            LinhaActualizar = int.Parse(textEditLinha.Text);
            GravarDados();
            DialogResult = System.Windows.Forms.DialogResult.Cancel;

        }


        private void GravarDados()
        {

            // Rui Fernandes: 2019/08/06
            // EditorCompras.DocumentoCompra.Linhas(LinhaActualizar).CamposUtil("CDU_Destino").Valor = Me.TxtDestino.Text
            DocumentoVenda.Linhas.GetEdita(LinhaActualizar).CamposUtil["CDU_LoteFornecedor"].Valor = this.textEditLoteForn.EditValue;
            DocumentoVenda.Linhas.GetEdita(LinhaActualizar).CamposUtil["CDU_NumBL"].Valor = this.textEditNumBL.EditValue;
            DocumentoVenda.Linhas.GetEdita(LinhaActualizar).CamposUtil["CDU_NumCertificado"].Valor = this.textEditNumCertificado.EditValue;
            DocumentoVenda.Linhas.GetEdita(LinhaActualizar).CamposUtil["CDU_NContentor"].Valor = this.textEditNumContentor.EditValue;
            DocumentoVenda.Linhas.GetEdita(LinhaActualizar).CamposUtil["CDU_Seguradora"].Valor = this.textEditSeguradora.EditValue;
            DocumentoVenda.Linhas.GetEdita(LinhaActualizar).CamposUtil["CDU_ObsLote"].Valor = this.memoEditObservacoes.EditValue;
            DocumentoVenda.Linhas.GetEdita(LinhaActualizar).CamposUtil["CDU_NFatura"].Valor = textEditFaturaForn.EditValue;
            VerificaSituacao();
            DocumentoVenda.Linhas.GetEdita(LinhaActualizar).CamposUtil["CDU_Situacao"].Valor = SituacaoStr;
            VerificaTipoQualidade();
            DocumentoVenda.Linhas.GetEdita(LinhaActualizar).CamposUtil["CDU_TipoQualidade"].Valor = TipoQualidadeStr;
            VerificaPais();
            DocumentoVenda.Linhas.GetEdita(LinhaActualizar).CamposUtil["CDU_PaisOrigem"].Valor = PaisStr;
            VerificaCompMaritima();
            DocumentoVenda.Linhas.GetEdita(LinhaActualizar).CamposUtil["CDU_CompanhiaMaritima"].Valor = CompMaritimaStr;
            VerificaPorto();
            DocumentoVenda.Linhas.GetEdita(LinhaActualizar).CamposUtil["CDU_Porto"].Valor = PortoStr;
            // Rui Fernandes: 2019/08/06
            VerificaDestino();
            DocumentoVenda.Linhas.GetEdita(LinhaActualizar).CamposUtil["CDU_Destino"].Valor = DestinoStr;

            DocumentoVenda.Linhas.GetEdita(LinhaActualizar).CamposUtil["CDU_DataPrevistaChegada"].Valor = DataPrevistaChegada;
        }




        private void CarregaCombos()
        {
            // Preenche combo das situações
            SqlStringSituacao = "SELECT CDU_Situacao,CDU_Descricao FROM TDU_SITUACOESLOTE ORDER BY CDU_SITUACAO DESC";

            DataTable situacoesLoteDt;

            situacoesLoteDt = BSO.ConsultaDataTable(SqlStringSituacao);

            lookUpEditSituacao.Properties.DataSource = situacoesLoteDt;
            lookUpEditSituacao.Properties.DisplayMember = "CDU_Situacao";
            lookUpEditSituacao.Properties.ValueMember = "CDU_Situacao";


            // Preenche combo tipo de qualidade
            SqlStringTipoQualidade = "SELECT CDU_TipoQualidade,CDU_Descricao FROM TDU_TIPOQUALIDADE ORDER BY CDU_TIPOQUALIDADE DESC";

            DataTable tipoqualidadeDt;

            tipoqualidadeDt = BSO.ConsultaDataTable(SqlStringTipoQualidade);

            lookUpEditTipoQualidade.Properties.DataSource = tipoqualidadeDt;
            lookUpEditTipoQualidade.Properties.DisplayMember = "CDU_TipoQualidade";
            lookUpEditTipoQualidade.Properties.ValueMember = "CDU_TipoQualidade";

            // Preenche combo dos países
            SqlStringPais = "SELECT Pais,Descricao FROM PAISES ORDER BY PAIS DESC";

            DataTable paisesDt;

            paisesDt = BSO.ConsultaDataTable(SqlStringPais);

            lookUpEditPais.Properties.DataSource = paisesDt;
            lookUpEditPais.Properties.DisplayMember = "Pais";
            lookUpEditPais.Properties.ValueMember = "Pais";

            // Preenche combo companhias maritimas
            SqlStringCompMaritima = "SELECT CDU_Codigo,CDU_Descricao FROM TDU_CompanhiasMaritimas ORDER BY CDU_Codigo DESC";

            DataTable compmaritimasDt;

            compmaritimasDt = BSO.ConsultaDataTable(SqlStringCompMaritima);

            lookUpEditCompMaritima.Properties.DataSource = compmaritimasDt;
            lookUpEditCompMaritima.Properties.DisplayMember = "CDU_Codigo";
            lookUpEditCompMaritima.Properties.ValueMember = "CDU_Codigo";


            // Rui Fernandes: 2019/08/06
            // Preenche combo dos portos
            SqlStringPorto = "SELECT CDU_Local,CDU_Descricao FROM TDU_Locais ORDER BY CDU_Local DESC";

            DataTable locaisDt;

            locaisDt = BSO.ConsultaDataTable(SqlStringPorto);

            lookUpEditPorto.Properties.DataSource = locaisDt;
            lookUpEditPorto.Properties.DisplayMember = "CDU_Local";
            lookUpEditPorto.Properties.ValueMember = "CDU_Local";

            lookUpEditDestino.Properties.DataSource = locaisDt;
            lookUpEditDestino.Properties.DisplayMember = "CDU_Local";
            lookUpEditDestino.Properties.ValueMember = "CDU_Local";
        }

        private void checkEditLimiteEmbarque_EditValueChanged(object sender, EventArgs e)
        {

            DataPrevistaChegada = DateTime.Parse(this.dateEditLimiteEmbarque.EditValue.ToString());


        }

        private void barButtonItemLinhaAnterior_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LinhaActualizar = int.Parse(this.textEditLinha.EditValue.ToString());
            GravarDados();
            for (int i = int.Parse(this.textEditLinha.Text) - 1; i >= 1; i += -1)
            //for (int i = int.Parse(this.textEditLinha.EditValue.ToString()) -1 ;i<=DocumentoVenda.Linhas.NumItens;i++)
            {
                LimpaCampos();
                textEditLinha.EditValue = "";
                this.textEditLinha.EditValue = i;
                if (DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "")
                {
                    this.textEditArtigo.EditValue = DocumentoVenda.Linhas.GetEdita(i).Artigo;
                    this.textEditDescArt.EditValue = DocumentoVenda.Linhas.GetEdita(i).Descricao;
                    this.textEditLote.EditValue = DocumentoVenda.Linhas.GetEdita(i).Lote;
                    this.textEditLoteForn.EditValue = DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_LoteFornecedor"].Valor + "";
                    this.textEditNumContentor.EditValue = DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_NContentor"].Valor + "";
                    this.textEditNumBL.EditValue = DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_NumBL"].Valor + "";
                    // Rui Fernandes: 2019/08/06
                    // Me.TxtDestino.Text = EditorVendas.DocumentoVenda.Linhas(i).CamposUtil("CDU_Destino").Valor & ""
                    this.textEditSeguradora.EditValue = DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Seguradora"].Valor + "";
                    this.textEditNumCertificado.EditValue = DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_NumCertificado"].Valor + "";
                    this.memoEditObservacoes.EditValue = DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_ObsLote"].Valor + "";
                    this.textEditFaturaForn.EditValue = DocumentoVenda.Linhas.GetEdita(Module1.LinhaEnc).CamposUtil["CDU_NFatura"].Valor + "";
                    Situacao = DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Situacao"].Valor + "";
                    TipoQualidade = DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_TipoQualidade"].Valor + "";
                    Pais = DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_PaisOrigem"].Valor + "";
                    CompMaritima = DocumentoVenda.CamposUtil["CDU_CompanhiaMaritima"].Valor + "";
                    Porto = DocumentoVenda.CamposUtil["CDU_Porto"].Valor + "";
                    // Rui Fernandes: 2019/08/06
                    Destino = DocumentoVenda.CamposUtil["CDU_Destino"].Valor + "";
                    ColocaDadosCombo();
                    if (DocumentoVenda.Linhas.GetEdita(i).DataEntrega + "" == "")
                    {
                        this.dateEditEmbarque.EditValue = DateAndTime.Now;
                    }
                    else
                    {
                        this.dateEditEmbarque.EditValue = Strings.Format(DocumentoVenda.Linhas.GetEdita(i).DataEntrega, "yyyy-MM-dd");
                    }

                    if (DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_DataPrevistaChegada"].Valor + "" == "")
                    {
                        this.dateEditLimiteEmbarque.EditValue = DateAndTime.Now;
                    }
                    else
                    {
                        this.dateEditLimiteEmbarque.EditValue = Strings.Format(DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_DataPrevistaChegada"].Valor, "yyyy-MM-dd");
                    }

                    DataPrevistaChegada = DateTime.Parse(this.dateEditLimiteEmbarque.EditValue.ToString());
                    return;
                }
            }
        }

        private void barButtonItemLinhaSeguinte_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            LinhaActualizar = int.Parse(this.textEditLinha.EditValue.ToString());
            GravarDados();
            for (int i = int.Parse(this.textEditLinha.Text) + 1; i <= DocumentoVenda.Linhas.NumItens; i++)
            //for (int i = int.Parse(this.textEditLinha.EditValue.ToString()) + 1; i <= DocumentoVenda.Linhas.NumItens; i++)
            {
                LimpaCampos();
                this.textEditLinha.EditValue = i;
                if (DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "")
                {
                    this.textEditArtigo.EditValue = DocumentoVenda.Linhas.GetEdita(i).Artigo;
                    this.textEditDescArt.EditValue = DocumentoVenda.Linhas.GetEdita(i).Descricao;
                    this.textEditLote.EditValue = DocumentoVenda.Linhas.GetEdita(i).Lote;
                    this.textEditLoteForn.EditValue = DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_LoteFornecedor"].Valor + "";
                    this.textEditNumContentor.EditValue = DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_NContentor"].Valor + "";
                    this.textEditNumBL.EditValue = DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_NumBL"].Valor + "";
                    // Rui Fernandes: 2019/08/06
                    // Me.TxtDestino.Text = EditorVendas.DocumentoVenda.Linhas(i).CamposUtil("CDU_Destino").Valor & ""
                    this.textEditSeguradora.EditValue = DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Seguradora"].Valor + "";
                    this.textEditNumCertificado.EditValue = DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_NumCertificado"].Valor + "";
                    this.memoEditObservacoes.EditValue = DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_ObsLote"].Valor + "";
                    this.textEditFaturaForn.EditValue = DocumentoVenda.Linhas.GetEdita(Module1.LinhaEnc).CamposUtil["CDU_NFatura"].Valor + "";
                    Situacao = DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Situacao"].Valor + "";
                    TipoQualidade = DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_TipoQualidade"].Valor + "";
                    Pais = DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_PaisOrigem"].Valor + "";
                    CompMaritima = DocumentoVenda.CamposUtil["CDU_CompanhiaMaritima"].Valor + "";
                    Porto = DocumentoVenda.CamposUtil["CDU_Porto"].Valor + "";
                    // Rui Fernandes: 2019/08/06
                    Destino = DocumentoVenda.CamposUtil["CDU_Destino"].Valor + "";
                    ColocaDadosCombo();
                    if (DocumentoVenda.Linhas.GetEdita(i).DataEntrega + "" == "")
                    {
                        this.dateEditEmbarque.EditValue = DateAndTime.Now;
                    }
                    else
                    {
                        this.dateEditEmbarque.EditValue = Strings.Format(DocumentoVenda.Linhas.GetEdita(i).DataEntrega, "yyyy-MM-dd");
                    }

                    if (DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_DataPrevistaChegada"].Valor + "" == "")
                    {
                        this.dateEditLimiteEmbarque.EditValue = DateAndTime.Now;
                    }
                    else
                    {
                        this.dateEditLimiteEmbarque.EditValue = Strings.Format(DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_DataPrevistaChegada"].Valor, "yyyy-MM-dd");
                    }

                    DataPrevistaChegada = DateTime.Parse(this.dateEditLimiteEmbarque.EditValue.ToString());
                    return;
                }


            }

        }

        private void barButtonItemSelTodos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            Verifica = 1;

            this.checkEditCompMaritima.EditValue = true;
            this.checkEditDestino.EditValue = true;
            this.checkEditEmbarque.EditValue = true;
            this.checkEditLimiteEmbarque.EditValue = true;
            this.checkEditLoteForn.EditValue = true;
            this.checkEditNumBL.EditValue = true;
            this.checkEditNumCertificado.EditValue = true;
            this.checkEditNumContentor.EditValue = true;
            this.checkEditObservacoes.EditValue = true;
            this.checkEditPais.EditValue = true;
            this.checkEditPorto.EditValue = true;
            this.checkEditSeguradora.EditValue = true;
            this.checkEditSituacao.EditValue = true;
            this.checkEditTipoQualidade.EditValue = true;
            this.checkEditFaturaForn.EditValue = true;
            Verifica = 0;

        }

        private void barButtonItemAnularSel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            Verifica = 1;

            this.checkEditCompMaritima.EditValue = false;
            this.checkEditDestino.EditValue = false;
            this.checkEditEmbarque.EditValue = false;
            this.checkEditLimiteEmbarque.EditValue = false;
            this.checkEditLoteForn.EditValue = false;
            this.checkEditNumBL.EditValue = false;
            this.checkEditNumCertificado.EditValue = false;
            this.checkEditNumContentor.EditValue = false;
            this.checkEditObservacoes.EditValue = false;
            this.checkEditPais.EditValue = false;
            this.checkEditPorto.EditValue = false;
            this.checkEditSeguradora.EditValue = false;
            this.checkEditSituacao.EditValue = false;
            this.checkEditTipoQualidade.EditValue = false;
            this.checkEditFaturaForn.EditValue = false;
            Verifica = 0;

        }

        private void barButtonItemInverteSel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            Verifica = 1;
            if ((bool)this.checkEditCompMaritima.EditValue == true)
            {
                this.checkEditCompMaritima.EditValue = false;
                this.checkEditPorto.EditValue = false;
            }
            else
            {
                this.checkEditCompMaritima.EditValue = true;
                this.checkEditPorto.EditValue = true;
            }

            if ((bool)this.checkEditDestino.EditValue == true)
            {
                this.checkEditDestino.EditValue = false;
            }
            else
            {
                this.checkEditDestino.EditValue = true;
            }

            if ((bool)this.checkEditEmbarque.EditValue == true)
            {
                this.checkEditEmbarque.EditValue = false;
            }
            else
            {
                this.checkEditEmbarque.EditValue = true;
            }

            if ((bool)this.checkEditLimiteEmbarque.EditValue == true)
            {
                this.checkEditLimiteEmbarque.EditValue = false;
            }
            else
            {
                this.checkEditLimiteEmbarque.EditValue = true;

            }
            if ((bool)this.checkEditLoteForn.EditValue == true)
            {
                this.checkEditLoteForn.EditValue = false;
            }
            else
            {
                this.checkEditLoteForn.EditValue = true;
            }

            if ((bool)this.checkEditNumBL.EditValue == true)
            {
                this.checkEditNumBL.EditValue = false;
            }
            else
            {
                this.checkEditNumBL.EditValue = true;
            }

            if ((bool)this.checkEditNumCertificado.EditValue == true)
            {
                this.checkEditNumCertificado.EditValue = false;
            }
            else
            {
                this.checkEditNumCertificado.EditValue = true;
            }

            if ((bool)this.checkEditNumContentor.EditValue == true)
            {
                this.checkEditNumContentor.EditValue = false;
            }
            else
            {
                this.checkEditNumContentor.EditValue = true;
            }

            if ((bool)this.checkEditObservacoes.EditValue == true)
            {
                this.checkEditObservacoes.EditValue = false;
            }
            else
            {
                this.checkEditObservacoes.EditValue = true;
            }

            if ((bool)this.checkEditPais.EditValue == true)
            {
                this.checkEditPais.EditValue = false;
            }
            else
            {
                this.checkEditPais.EditValue = true;
            }

            if ((bool)this.checkEditSeguradora.EditValue == true)
            {
                this.checkEditSeguradora.EditValue = false;
            }
            else
            {
                this.checkEditSeguradora.EditValue = true;
            }

            if ((bool)this.checkEditSituacao.EditValue == true)
            {
                this.checkEditSituacao.EditValue = false;
            }
            else
            {
                this.checkEditSituacao.EditValue = true;
            }

            if ((bool)this.checkEditTipoQualidade.EditValue == true)
            {
                this.checkEditTipoQualidade.EditValue = false;
            }
            else
            {
                this.checkEditTipoQualidade.EditValue = true;
            }


            if ((bool)this.checkEditFaturaForn.EditValue == true)
            {
                this.checkEditFaturaForn.EditValue = false;
            }
            else
            {
                this.checkEditFaturaForn.EditValue = true;
            }


            Verifica = 0;

        }

        private void barButtonItemTodas_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            for (int k = 1, loopTo = DocumentoVenda.Linhas.NumItens; k <= loopTo; k++)
            {
                if (DocumentoVenda.Linhas.GetEdita(k).Artigo + "" != "")
                {

                    // Rui Fernandes: 2019/08/06
                    // If Me.ChkBoxDestino.Value = True Then
                    // EditorVendas.DocumentoVenda.Linhas(k).CamposUtil("CDU_Destino").Valor = Me.TxtDestino.Text
                    // End If
                    if ((bool)this.checkEditLoteForn.EditValue == true)
                    {
                        DocumentoVenda.Linhas.GetEdita(k).CamposUtil["CDU_LoteFornecedor"].Valor = this.textEditLoteForn.EditValue;
                    }

                    if ((bool)this.checkEditNumBL.EditValue == true)
                    {
                        DocumentoVenda.Linhas.GetEdita(k).CamposUtil["CDU_NumBL"].Valor = this.textEditNumBL.EditValue;
                    }

                    if ((bool)this.checkEditNumCertificado.EditValue == true)
                    {
                        DocumentoVenda.Linhas.GetEdita(k).CamposUtil["CDU_NumCertificado"].Valor = this.textEditNumCertificado.EditValue;
                    }

                    if ((bool)this.checkEditNumContentor.EditValue == true)
                    {
                        DocumentoVenda.Linhas.GetEdita(k).CamposUtil["CDU_NContentor"].Valor = this.textEditNumContentor.EditValue;
                    }

                    if ((bool)this.checkEditSeguradora.EditValue == true)
                    {
                        DocumentoVenda.Linhas.GetEdita(k).CamposUtil["CDU_Seguradora"].Valor = this.textEditSeguradora.EditValue;
                    }

                    if ((bool)this.checkEditObservacoes.EditValue == true)
                    {
                        DocumentoVenda.Linhas.GetEdita(k).CamposUtil["CDU_ObsLote"].Valor = this.memoEditObservacoes.EditValue;
                    }

                    if ((bool)this.checkEditSituacao.EditValue == true)
                    {
                        VerificaSituacao();
                        DocumentoVenda.Linhas.GetEdita(k).CamposUtil["CDU_Situacao"].Valor = SituacaoStr;
                    }

                    if ((bool)this.checkEditTipoQualidade.EditValue == true)
                    {
                        VerificaTipoQualidade();
                        DocumentoVenda.Linhas.GetEdita(k).CamposUtil["CDU_TipoQualidade"].Valor = TipoQualidadeStr;
                    }

                    if ((bool)this.checkEditPais.EditValue == true)
                    {
                        VerificaPais();
                        DocumentoVenda.Linhas.GetEdita(k).CamposUtil["CDU_PaisOrigem"].Valor = PaisStr;
                    }

                    if ((bool)this.checkEditCompMaritima.EditValue == true)
                    {
                        VerificaCompMaritima();
                        DocumentoVenda.CamposUtil["CDU_CompanhiaMaritima"].Valor = CompMaritimaStr;
                    }

                    if ((bool)this.checkEditPorto.EditValue == true)
                    {
                        VerificaPorto();
                        DocumentoVenda.CamposUtil["CDU_Porto"].Valor = PortoStr;
                    }
                    // Rui Fernandes: 2019/08/06
                    if ((bool)this.checkEditDestino.EditValue == true)
                    {
                        VerificaDestino();
                        DocumentoVenda.CamposUtil["CDU_Destino"].Valor = DestinoStr;
                    }

                    if ((bool)this.checkEditLimiteEmbarque.EditValue == true)
                    {
                        DocumentoVenda.Linhas.GetEdita(k).CamposUtil["CDU_DataPrevistaChegada"].Valor = DataPrevistaChegada;
                    }
                }
            }

        }
    }
}