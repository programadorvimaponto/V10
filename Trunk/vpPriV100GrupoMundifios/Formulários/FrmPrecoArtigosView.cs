using Microsoft.VisualBasic;
using Primavera.Extensibility.CustomForm;
using StdBE100;
using System;
using System.Data;
using System.Windows.Forms;
namespace GrupoMundifios.Formulários
{
    public partial class FrmPrecoArtigosView : CustomForm
    {
        public FrmPrecoArtigosView()
        {
            InitializeComponent();
        }

        private void FrmPrecoArtigosView_Load(object sender, EventArgs e)
        {
            if (FindForm() is Form pai)
            {
                pai.MinimumSize = pai.Size;
                pai.MaximumSize = pai.Size;
            }
        }

        private string AlternativaSel;
        private double Desp;

        private void checkEditCalcPrecoMercado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                PreencheComponentes(AlternativaSel);
                CalcularTextBox();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lookUpEditAlternativas_EditValueChanged(object sender, EventArgs e)
        {
            string[] AltNovaSel;
            if (this.lookUpEditAlternativas.EditValue + "" != "")
            {
                AltNovaSel = Strings.Split(this.lookUpEditAlternativas.Text, " - ");
                AlternativaSel = AltNovaSel[0];
                PreencheComponentes(AlternativaSel);
                CalcularTextBox();
            }
        }

        private void botaoAtualizar()
        {
            try
            {
                PreencheComponentes(AlternativaSel);
                CalcularTextBox();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void barButtonItemAtualizar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            botaoAtualizar();
        }

        public void PreencheComboAlternativas(string v_Artigo)
        {
            DataTable dtAlternativas = new DataTable();

            StdBELista listaAlternativas = new StdBELista();

            string SqlStringAlternativas;
            int k;

            AlternativaSel = "";

            // Preenche dados na combo das alternativas
            SqlStringAlternativas = "SELECT PRD_Alternativa, PRD_Descricao, PRD_Principal FROM VIM_Alternativas WHERE PRD_Artigo = '" + v_Artigo + "' AND PRD_Desactivada = 0 ORDER BY PRD_Alternativa";

            dtAlternativas = BSO.ConsultaDataTable(SqlStringAlternativas);

            listaAlternativas = BSO.Consulta(SqlStringAlternativas);

            if (dtAlternativas.Rows.Count > 0)
            {
                lookUpEditAlternativas.Properties.DataSource = dtAlternativas;
                lookUpEditAlternativas.Properties.DisplayMember = "PRD_Alternativa";
                lookUpEditAlternativas.Properties.ValueMember = "PRD_Alternativa";

                listaAlternativas.Inicio();

                for (k = 1; k <= listaAlternativas.NumLinhas(); k++)
                {
                    if (listaAlternativas.Valor("PRD_Principal") == true)
                    {
                        this.lookUpEditAlternativas.Text = listaAlternativas.Valor("PRD_Alternativa").ToString();
                        AlternativaSel = listaAlternativas.Valor("PRD_Alternativa");
                    }

                    listaAlternativas.Seguinte();
                }
            }
        }

        private void FormatarGrelhaComponente()
        {
            //List_Componentes.Clear();
            //this.List_Componentes.ColumnCount = 9;
            //this.List_Componentes.ColumnWidths = "94 pt; 240 pt; 43 pt; 47 pt; 50 pt; 52 pt; 40 pt; 60 pt; 30pt;";
            //this.List_Componentes.AddItem();
            //this.List_Componentes.List(i, 0) = ListaArtigos("Componente");
            //this.List_Componentes.List(i, 1) = ListaArtigos("Descricao");
            //this.List_Componentes.List(i, 2) = ListaArtigos("Consumo");
            //this.List_Componentes.List(i, 3) = ListaArtigos("PercentagemDesp") + " %";
            //this.List_Componentes.List(i, 4) = ListaArtigos("PCM");
            //this.List_Componentes.List(i, 5) = ListaArtigos("PM");
            //this.List_Componentes.List(i, 6) = ListaArtigos("Valor");
            //this.List_Componentes.List(i, 7) = ListaArtigos("StkAtual");
            //this.List_Componentes.List(i, 8) = ListaArtigos("StkMinimo");

            var withBlock = vmpGridViewComponentes;
            {
                withBlock.IniciarFormatacao(true);
                withBlock.FormatarColuna("Componente", true, false, true, "Artigo", 94, DevExpress.Utils.FormatType.None, default);
                withBlock.FormatarColuna("Descricao", true, false, true, "Descrição", 180, DevExpress.Utils.FormatType.None, default);
                withBlock.FormatarColuna("Consumo", true, false, true, "Qtd", 43, DevExpress.Utils.FormatType.None, default);
                withBlock.FormatarColuna("PercentagemDesp", false, false, true, "Desperdício", 47, DevExpress.Utils.FormatType.None, default);
                withBlock.FormatarColuna("PCM", true, false, false, "PCM", 50, DevExpress.Utils.FormatType.None, default);
                withBlock.FormatarColuna("PM", true, false, true, "PM", 52, DevExpress.Utils.FormatType.None, default);
                withBlock.FormatarColuna("Valor", true, false, true, "Valor", 40, DevExpress.Utils.FormatType.None, default);
                withBlock.FormatarColuna("StkAtual", true, false, true, "StkAtual", 60, DevExpress.Utils.FormatType.None, default);
                withBlock.FormatarColuna("StkMinimo", true, false, true, "StkMinimo", 60, DevExpress.Utils.FormatType.None, default);

                withBlock.AutoFillColumn = withBlock.Columns["StkMinimo"];
                withBlock.FinalizarFormatacao();
            }
        }

        public void PreencheComponentes(string v_Alternativa)
        {
            StdBELista ListaArtigos = new StdBELista();
            DataTable dtArtigos = new DataTable();
            string SqlStringArtigos;
            int i;

            try
            {
                if (checkEditCalcPrecoMercado.EditValue.ToString() == "false")
                    SqlStringArtigos = " SELECT TOTAL.ARTIGO, Total.Componente, Total.Descricao, Total.Quantidade as Consumo, Total.Desperdicio , Total.ConsumoComDesperdicio as ConsumoComDesperdicio, " + " ROUND(Total.PercentagemDesp,0) as PercentagemDesp, " + " CASE ROUND(Total.PCM, 2) WHEN '0' THEN isnull((select top 1 round(aa.PCMedio,2) from LinhasSTK lk inner join ArtigoArmazem aa on aa.Artigo=lk.Artigo and aa.Lote=lk.Lote where lk.Artigo=Total.Componente and lk.TipoDoc in ('FPE','FPI', 'VFA', 'VFI', 'VFO', 'ES') and lk.EntradaSaida='E' order by lk.data desc),0) else COALESCE(ROUND(Total.PCM, 2), 0) end AS PCM, " + " COALESCE(ROUND(Total.PM, 2), 0) AS PM, " + " COALESCE(ROUND(Total.StkAtual, 2), 0) AS StkAtual, " + " COALESCE(ROUND(Total.StkMinimo, 2), 0) AS StkMinimo, " + " COALESCE(ROUND(Total.Valor1, 2), 0) AS Valor1, " + " COALESCE(ROUND(Total.Valor2, 2), 0) AS Valor2, " + " Case when (select top 1 artigo from artigoarmazem where Artigo=total.componente) is not null and ROUND(Total.Valor2, 2)>0 then ROUND(Total.Valor2, 2) else ROUND(Total.Valor1, 2) end AS Valor  " + " FROM (SELECT A.Artigo ,AC.PRD_Consumo AS Quantidade ,AC.PRD_Componente AS Componente, ArtigoC.Descricao AS Descricao , " + " ( SELECT COALESCE(AVG(PCMedio), 0) FROM ArtigoArmazem AA WHERE AA.ARTIGO = ArtigoC.Artigo AND AA.StkActual > 0 ) AS PCM, " + " ( SELECT CDU_Preco FROM TDU_PrecosMercado PM WHERE PM.CDU_Artigo = AC.PRD_componente ) AS PM ,  " + " (AC.PRD_Consumo + AC.prd_desperdicio ) * COALESCE(( SELECT CDU_Preco FROM TDU_PrecosMercado PM WHERE PM.CDU_Artigo = AC.PRD_componente ), 0) AS Valor1, " + " (AC.PRD_Consumo + AC.prd_desperdicio ) * ( case (COALESCE(( SELECT COALESCE(AVG(PCMedio), 0) FROM ArtigoArmazem AA WHERE AA.ARTIGO = ArtigoC.Artigo AND AA.StkActual > 0 ), 0)) when '0' then (select top 1 round(aa.PCMedio,2) from LinhasSTK lk inner join ArtigoArmazem aa on aa.Artigo=lk.Artigo and aa.Lote=lk.Lote where lk.Artigo=ArtigoC.Artigo and lk.TipoDoc in ('FPE','FPI', 'VFA', 'VFI', 'VFO', 'ES') and lk.EntradaSaida='E' order by lk.data desc) else COALESCE(( SELECT COALESCE(AVG(PCMedio), 0) FROM ArtigoArmazem AA WHERE AA.ARTIGO = ArtigoC.Artigo AND AA.StkActual > 0 ), 0) end)  AS Valor2, " + " ( SELECT COALESCE(SUM(StkActual), 0) FROM ArtigoArmazem AA WHERE AA.ARTIGO = ArtigoC.Artigo AND StkActual > 0 ) AS StkAtual, " + " AC.prd_desperdicio as Desperdicio , " + " AC.PRD_Consumo + AC.prd_desperdicio as ConsumoComDesperdicio , " + " 100-(100*AC.PRD_Consumo)/(AC.prd_desperdicio +AC.PRD_Consumo) as PercentagemDesp , " + " COALESCE(AASC.StkMinimo, 0) AS StkMinimo  " + " FROM Artigo A " + " INNER JOIN VIM_Alternativas AL ON A.Artigo = AL.PRD_Artigo AND AL.PRD_Alternativa = '" + v_Alternativa + "' INNER JOIN VIM_ArtigoOperacoes AO ON AO.PRD_Artigo = AL.PRD_Artigo AND AO.PRD_Alternativa = AL.PRD_Alternativa INNER JOIN VIM_ArtigoComponentes AC ON AC.PRD_IDOperacao = AO.PRD_IDOperacao " + " INNER JOIN Artigo ArtigoC ON ArtigoC.Artigo = AC.PRD_Componente " + " LEFT OUTER JOIN ArtigoArmazemStocks AASC ON AASC.Armazem = ArtigoC.ArmazemSugestao and AASC.Artigo = ArtigoC.Artigo  " + " WHERE A.ARTIGO = '" + this.textEditArtigo.Text + "') AS Total ORDER BY Total.Componente";
                else
                    SqlStringArtigos = " SELECT TOTAL.ARTIGO, Total.Componente, Total.Descricao, Total.Quantidade as Consumo, Total.Desperdicio , Total.ConsumoComDesperdicio as ConsumoComDesperdicio, " + " ROUND(Total.PercentagemDesp,0) as PercentagemDesp, " + " CASE ROUND(Total.PCM, 2) WHEN '0' THEN isnull((select top 1 round(aa.PCMedio,2) from LinhasSTK lk inner join ArtigoArmazem aa on aa.Artigo=lk.Artigo and aa.Lote=lk.Lote where lk.Artigo=Total.Componente and lk.TipoDoc in ('FPE','FPI', 'VFA', 'VFI', 'VFO', 'ES') and lk.EntradaSaida='E' order by lk.data desc),0) else COALESCE(ROUND(Total.PCM, 2), 0) end AS PCM, " + " COALESCE(ROUND(Total.PM, 2), 0) AS PM, " + " COALESCE(ROUND(Total.StkAtual, 2), 0) AS StkAtual, " + " COALESCE(ROUND(Total.StkMinimo, 2), 0) AS StkMinimo, " + " COALESCE(ROUND(Total.Valor1, 2), 0) AS Valor1, " + " COALESCE(ROUND(Total.Valor2, 2), 0) AS Valor2, " + " Case when (SELECT CDU_Preco FROM TDU_PrecosMercado PM WHERE PM.CDU_Artigo = Total.Componente ) is not null and ROUND(Total.Valor1, 2)>0 then ROUND(Total.Valor1, 2) else ROUND(Total.Valor2, 2) end AS Valor  " + " FROM (SELECT A.Artigo ,AC.PRD_Consumo AS Quantidade ,AC.PRD_Componente AS Componente, ArtigoC.Descricao AS Descricao , " + " ( SELECT COALESCE(AVG(PCMedio), 0) FROM ArtigoArmazem AA WHERE AA.ARTIGO = ArtigoC.Artigo AND AA.StkActual > 0 ) AS PCM, " + " ( SELECT CDU_Preco FROM TDU_PrecosMercado PM WHERE PM.CDU_Artigo = AC.PRD_componente ) AS PM ,  " + " (AC.PRD_Consumo + AC.prd_desperdicio ) * COALESCE(( SELECT CDU_Preco FROM TDU_PrecosMercado PM WHERE PM.CDU_Artigo = AC.PRD_componente ), 0) AS Valor1, " + " (AC.PRD_Consumo + AC.prd_desperdicio ) * ( case (COALESCE(( SELECT COALESCE(AVG(PCMedio), 0) FROM ArtigoArmazem AA WHERE AA.ARTIGO = ArtigoC.Artigo AND AA.StkActual > 0 ), 0)) when '0' then (select top 1 round(aa.PCMedio,2) from LinhasSTK lk inner join ArtigoArmazem aa on aa.Artigo=lk.Artigo and aa.Lote=lk.Lote where lk.Artigo=ArtigoC.Artigo and lk.TipoDoc in ('FPE','FPI', 'VFA', 'VFI', 'VFO', 'ES') and lk.EntradaSaida='E' order by lk.data desc) else COALESCE(( SELECT COALESCE(AVG(PCMedio), 0) FROM ArtigoArmazem AA WHERE AA.ARTIGO = ArtigoC.Artigo AND AA.StkActual > 0 ), 0) end)  AS Valor2, " + " ( SELECT COALESCE(SUM(StkActual), 0) FROM ArtigoArmazem AA WHERE AA.ARTIGO = ArtigoC.Artigo AND StkActual > 0 ) AS StkAtual, " + " AC.prd_desperdicio as Desperdicio , " + " AC.PRD_Consumo + AC.prd_desperdicio as ConsumoComDesperdicio , " + " 100-(100*AC.PRD_Consumo)/(AC.prd_desperdicio +AC.PRD_Consumo) as PercentagemDesp , " + " COALESCE(AASC.StkMinimo, 0) AS StkMinimo  " + " FROM Artigo A " + " INNER JOIN VIM_Alternativas AL ON A.Artigo = AL.PRD_Artigo AND AL.PRD_Alternativa = '" + v_Alternativa + "' INNER JOIN VIM_ArtigoOperacoes AO ON AO.PRD_Artigo = AL.PRD_Artigo AND AO.PRD_Alternativa = AL.PRD_Alternativa INNER JOIN VIM_ArtigoComponentes AC ON AC.PRD_IDOperacao = AO.PRD_IDOperacao " + " INNER JOIN Artigo ArtigoC ON ArtigoC.Artigo = AC.PRD_Componente " + " LEFT OUTER JOIN ArtigoArmazemStocks AASC ON AASC.Armazem = ArtigoC.ArmazemSugestao and AASC.Artigo = ArtigoC.Artigo  " + " WHERE A.ARTIGO = '" + this.textEditArtigo.Text + "') AS Total ORDER BY Total.Componente";

                ListaArtigos = BSO.Consulta(SqlStringArtigos);
                dtArtigos = BSO.ConsultaDataTable(SqlStringArtigos);

                if (ListaArtigos.NumLinhas() > 0)
                {
                    vmpGridControlComponentes.DataSource = dtArtigos;

                    FormatarGrelhaComponente();

                    Desp = 0;
                    ListaArtigos.Inicio();
                    Desp = Double.Parse(ListaArtigos.Valor("PercentagemDesp").ToString());
                    double VerificaConsumo;

                    VerificaConsumo = 0;
                    for (i = 0; i <= ListaArtigos.NumLinhas() - 1; i++)
                    {
                        VerificaConsumo = VerificaConsumo + Double.Parse(ListaArtigos.Valor("Consumo").ToString());
                        ListaArtigos.Seguinte();
                    }
                    if (VerificaConsumo < 1 | VerificaConsumo > 1)
                        MessageBox.Show("Atenção Consumo diferente de 100%:  " + VerificaConsumo, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void FormatarGrelhaVariacoes()
        {
            //this.List_Variacoes.List(i, 0) = ListaVariacoes("PRD_EscalaInferior");
            //this.List_Variacoes.List(i, 1) = ListaVariacoes("PRD_EscalaSuperior");
            //this.List_Variacoes.List(i, 2) = ListaVariacoes("PRD_VariacaoPercent");

            //this.List_Variacoes.ColumnWidths = "70 pt; 70 pt; 25 pt; 40 pt; 40 pt; 40 pt;";

            var withBlock = vmpGridViewVariacoes;
            {
                withBlock.IniciarFormatacao(true);
                withBlock.FormatarColuna("PRD_EscalaInferior", true, false, true, "Min", 100, DevExpress.Utils.FormatType.None, default);
                withBlock.FormatarColuna("PRD_EscalaSuperior", true, false, true, "Max", 120, DevExpress.Utils.FormatType.None, default);
                withBlock.FormatarColuna("PRD_VariacaoPercent", true, false, true, "Desp", 80, DevExpress.Utils.FormatType.None, default);
                withBlock.FormatarColuna("Valor", true, false, true, "Valor", 80, DevExpress.Utils.FormatType.None, default);
                withBlock.FormatarColuna("Preço Final", true, false, true, "Preço Final", 80, DevExpress.Utils.FormatType.None, default);

                withBlock.AutoFillColumn = withBlock.Columns["StkMinimo"];
                withBlock.FinalizarFormatacao();
            }
        }

        public void PreencheVariacoes(string v_Alternativa)
        {
            StdBELista ListaVariacoes = new StdBELista();
            DataTable dtVariacoes = new DataTable();
            string SqlStringVariacoes;
            int i;
            double v;

            try
            {
                SqlStringVariacoes = "select v.PRD_EscalaInferior, v.PRD_EscalaSuperior, v.PRD_VariacaoPercent from VIM_ArtigoVariacaoDesperdicio v where v.PRD_Artigo=''";

                ListaVariacoes = BSO.Consulta(SqlStringVariacoes);
                dtVariacoes = BSO.ConsultaDataTable(SqlStringVariacoes);

                if (ListaVariacoes.NumLinhas() > 0)
                {
                    if (vmpGridViewVariacoes.Columns.Count != 5)
                    {
                        dtVariacoes.Columns.Add("Valor");
                        dtVariacoes.Columns.Add("Preço Final");
                        vmpGridControlVariacoes.DataSource = dtVariacoes;
                        //vmpGridViewVariacoes.Columns.AddVisible("Valor");
                        //vmpGridViewVariacoes.Columns.AddVisible("Preço Final");
                        FormatarGrelhaVariacoes();
                    }


                    ListaVariacoes.Inicio();

                    for (i = 0; i <= ListaVariacoes.NumLinhas() - 1; i++)
                    {
                        //this.List_Variacoes.AddItem();
                        //this.List_Variacoes.List(i, 0) = ListaVariacoes("PRD_EscalaInferior");
                        //this.List_Variacoes.List(i, 1) = ListaVariacoes("PRD_EscalaSuperior");
                        //this.List_Variacoes.List(i, 2) = ListaVariacoes("PRD_VariacaoPercent");
                        //this.List_Variacoes.List(i, 3) = "%";

                        int j;
                        Desp = 0;
                        for (j = 0; j <= vmpGridViewComponentes.RowCount -1 ; j++)
                        {
                            // Me.List_Componentes.List(i, 0) = ListaArtigos("Componente")
                            // Me.List_Componentes.List(i, 1) = ListaArtigos("Descricao")
                            // Me.List_Componentes.List(i, 2) = ListaArtigos("Consumo")
                            // Me.List_Componentes.List(i, 3) = ListaArtigos("PercentagemDesp") & " %"
                            // Me.List_Componentes.List(i, 4) = ListaArtigos("PCM")
                            // Me.List_Componentes.List(i, 5) = ListaArtigos("PM")
                            // Me.List_Componentes.List(i, 6) = ListaArtigos("Valor")
                            // Me.List_Componentes.List(i, 7) = ListaArtigos("StkAtual")
                            // Me.List_Componentes.List(i, 8) = ListaArtigos("StkMinimo")
                            v = 0;
                            if (checkEditCalcPrecoMercado.EditValue.ToString() == "true")
                            {
                                v = Double.Parse(vmpGridViewComponentes.GetRowCellValue(j, "PM").ToString());
                                if (v == 0)
                                    v = Double.Parse(vmpGridViewComponentes.GetRowCellValue(j, "PCM").ToString());
                            }
                            else
                            {
                                v = Double.Parse(vmpGridViewComponentes.GetRowCellValue(j, "PCM").ToString());
                                if (v == 0)
                                    v = Double.Parse(vmpGridViewComponentes.GetRowCellValue(j, "PM").ToString());
                            }
                            // Consumo                                              PercentagemDesp                                     VarDepsPercentagem                                             PM/PCM

                            Desp = Math.Round(Desp, 2) + (((Double.Parse(vmpGridViewComponentes.GetRowCellValue(j, "Consumo").ToString()) / (1 - ((Double.Parse(vmpGridViewComponentes.GetRowCellValue(j, "PercentagemDesp").ToString()) + Double.Parse(ListaVariacoes.Valor("PRD_VariacaoPercent").ToString())) / (double)100)))) * v);
                        }

                        vmpGridViewVariacoes.SetRowCellValue(i, "Valor", Math.Round(Desp, 2));

                        if (ListaVariacoes.Valor("PRD_EscalaSuperior") <= 500)

                            vmpGridViewVariacoes.SetRowCellValue(i, "Preço Final", Math.Round(((Desp) + (double.Parse(this.textEditCustoProd.Text) * 1.35) + double.Parse(this.textEditCustoFixo.Text)) * (Double.Parse(this.textEditMargem.Text) / (double)100 + 1), 3));
                        else if (ListaVariacoes.Valor("PRD_EscalaSuperior") > 500 & ListaVariacoes.Valor("PRD_EscalaSuperior") <= 750)

                            vmpGridViewVariacoes.SetRowCellValue(i, "Preço Final", Math.Round(((Desp) + (double.Parse(this.textEditCustoProd.Text) * 1.15) + double.Parse(this.textEditCustoFixo.Text)) * (Double.Parse(this.textEditMargem.Text) / (double)100 + 1), 3));
                        else if (ListaVariacoes.Valor("PRD_EscalaSuperior") > 750 & ListaVariacoes.Valor("PRD_EscalaSuperior") <= 1000)

                            vmpGridViewVariacoes.SetRowCellValue(i, "Preço Final", Math.Round(((Desp) + (double.Parse(this.textEditCustoProd.Text) * 1.08) + double.Parse(this.textEditCustoFixo.Text)) * (Double.Parse(this.textEditMargem.Text) / (double)100 + 1), 3));
                        else if (ListaVariacoes.Valor("PRD_EscalaSuperior") > 1000 & ListaVariacoes.Valor("PRD_EscalaSuperior") <= 2000)

                            vmpGridViewVariacoes.SetRowCellValue(i, "Preço Final", Math.Round(((Desp) + (double.Parse(this.textEditCustoProd.Text) * 1.03) + double.Parse(this.textEditCustoFixo.Text)) * (Double.Parse(this.textEditMargem.Text) / (double)100 + 1), 3));
                        else

                            vmpGridViewVariacoes.SetRowCellValue(i, "Preço Final", Math.Round(((Desp) + double.Parse(this.textEditCustoProd.Text) + double.Parse(this.textEditCustoFixo.Text)) * (Double.Parse(this.textEditMargem.Text) / (double)100 + 1), 3));
                        ListaVariacoes.Seguinte();
                    }
                }


                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcularTextBox()
        {
            CalcularValores();
            CalcularCustoProducao();
            CalcularCustoFixoMargem();
        }

        private void CalcularTotaisTextBox()
        {
            CalcularSubTotal();
            CalcularTotal();
            CalcularPrecoFinal();
        }

        private void CalcularValores()
        {
            double Total;
            Total = 0;
            for (int i = 0; i <= vmpGridViewComponentes.RowCount - 1; i++)
            {
                if (vmpGridViewComponentes.GetRowCellValue(i, "Valor") == null)
                    vmpGridViewComponentes.SetRowCellValue(i, "Valor", 0);
                Total = Total + Double.Parse(vmpGridViewComponentes.GetRowCellValue(i, "Valor").ToString());
            }

            this.textEditValores.EditValue = Total;
        }

        private void CalcularCustoProducao()
        {
            StdBELista ListaArtigos;
            string SqlStringArtigos;
            try
            {
                SqlStringArtigos = " SELECT TOP 1 COALESCE(PRD_Preco,0) as PRD_Preco FROM VIM_ArtigoOperacoes WHERE PRD_OperacaoProducao in ('OP.0001','OP.0008','OP.0004') and PRD_Artigo = '" + this.textEditArtigo.Text + "' and PRD_Alternativa='" + AlternativaSel + "'";

                ListaArtigos = BSO.Consulta(SqlStringArtigos);

                if (ListaArtigos.NumLinhas() > 0)
                {
                    ListaArtigos.Inicio();
                    textEditCustoProd.EditValue = Double.Parse(ListaArtigos.Valor("PRD_Preco").ToString());
                }
                else
                    textEditCustoProd.EditValue = 0;

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcularSubTotal()
        {
            if (this.textEditValores.Text == string.Empty)
                textEditValores.EditValue = 0;
            if (this.textEditCustoProd.Text == string.Empty)
                textEditCustoProd.EditValue = 0;

            this.textEditSubTotal.EditValue = Double.Parse(this.textEditValores.Text) + Double.Parse(this.textEditCustoProd.Text);
        }

        private void CalcularCustoFixoMargem()
        {
            StdBELista ListaArtigos;
            string SqlStringArtigos;
            StdBELista VerificarTI;
            string SqlVerificarTI;
            int i;

            try
            {
                SqlStringArtigos = " SELECT TOP 1 CDU_Custo, CDU_Margem FROM TDU_CustoFixoMargem ";
                SqlVerificarTI = "select a.artigo from dbo.Artigo a inner join dbo.VMP_ART_CaracteristicaArtigo ca on ca.CodigoArtigo=a.Artigo inner join priinovafil.dbo.VMP_ART_Caracteristica c on c.Id=ca.IdCaracteristica where c.Codigo='079' and a.artigo='" + this.textEditArtigo.Text + "'";

                ListaArtigos = BSO.Consulta(SqlStringArtigos);

                VerificarTI = BSO.Consulta(SqlVerificarTI);

                if (ListaArtigos.NumLinhas() > 0)
                {
                    ListaArtigos.Inicio();
                    if (VerificarTI.NumLinhas() > 0)

                        if (textEditDescricao.Text.Contains("Dri Release"))
                        {
                            textEditCustoFixo.EditValue = 1.5;
                            textEditMargem.EditValue = 15;
                        }
                        else
                        {
                            textEditCustoFixo.EditValue = 0;
                            textEditMargem.EditValue = 30;
                        }
                    else
                    {
                        textEditCustoFixo.EditValue = 0;

                        // JFC verifica se é Mescla = 20% senão 10%
                        // JFC verifica se é Mescla ou F9999 ou A = 20% senão 10% - Pedido Eng. Manuel email 18/09/2020
                        if (Strings.Left(this.textEditArtigo.Text, 3) == "F02" | Strings.Left(this.textEditArtigo.Text, 1) == "2" | this.textEditArtigo.Text == "F9999" | Strings.Left(this.textEditArtigo.Text, 1) == "A")
                            textEditMargem.EditValue = 20;
                        else
                            textEditMargem.EditValue = ListaArtigos.Valor("CDU_Margem");
                    }
                }
                else
                {
                    this.textEditMargem.EditValue = 0;
                    this.textEditCustoFixo.EditValue = 0;
                }

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcularTotal()
        {
            try
            {
                if (textEditCustoFixo.Text == string.Empty)
                    textEditCustoFixo.EditValue = 0;
                if (textEditSubTotal.Text == string.Empty)
                    textEditSubTotal.EditValue = 0;

                this.textEditTotal.EditValue = Double.Parse(this.textEditCustoFixo.Text) + Double.Parse(this.textEditSubTotal.Text);

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcularPrecoFinal()
        {
            try
            {
                if (textEditTotal.Text == string.Empty)
                    textEditTotal.EditValue = 0;
                if (textEditMargem.Text == string.Empty)
                    textEditMargem.EditValue = 0;

                textEditPrecoFinal.EditValue = Double.Parse(this.textEditTotal.Text) * (1 + (Double.Parse(this.textEditMargem.Text) / (double)100));
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApagarCampos(bool Grelha)
        {
            if (Grelha)
                this.textEditValores.EditValue = 0;
            this.textEditCustoProd.EditValue = 0;
            this.textEditSubTotal.EditValue = 0;
            this.textEditCustoFixo.EditValue = 0;
            this.textEditMargem.EditValue = 0;
            this.textEditTotal.EditValue = 0;
        }

        private void textEditArtigo_EditValueChanged(object sender, EventArgs e)
        {
            CarregaDescricaoArtigo();
        }

        private void CarregaDescricaoArtigo()
        {
            if (this.textEditArtigo.Text == String.Empty)
            {
                this.textEditDescricao.Text = String.Empty;
                ApagarCampos(true);
                return;
            }
            if (BSO.Base.Artigos.Existe(this.textEditArtigo.Text) == true)
            {
                this.textEditDescricao.Text = BSO.Base.Artigos.Edita(this.textEditArtigo.Text).Descricao + " " + BSO.Base.Artigos.Edita(this.textEditArtigo.Text).CamposUtil["CDU_DescricaoExtra"].Valor;
                PreencheComboAlternativas(this.textEditArtigo.Text);
                botaoAtualizar();
            }
            else
            {
                this.textEditDescricao.Text = String.Empty;
                ApagarCampos(true);
            }

            StdBELista ListaExclusividade;
            string SqlStringExclusividade;

            SqlStringExclusividade = "SELECT * FROM [Artigo] "
                                             + "WHERE Artigo = '" + this.textEditArtigo.Text + "' and Artigo in (select ca.CodigoArtigo from VMP_ART_CaracteristicaArtigo ca where ca.IdCaracteristica in ('C5C36D88-65E9-4965-A793-CA23CED0A657', 'F1BC7CCC-C1D9-47CA-B9F8-62ECF58D1749'))";

            ListaExclusividade = BSO.Consulta(SqlStringExclusividade);

            if (ListaExclusividade.Vazia() == false)
                MessageBox.Show("O Artigo: " + this.textEditArtigo.Text + " tem acordos estipulados!", "Atenção!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                SqlStringExclusividade = "SELECT * FROM [Artigo] WHERE Artigo = '" + this.textEditArtigo.Text + "' and (CDU_Ref='08479' or CDU_DescricaoReferencia in ('08479','01357'))";

                ListaExclusividade = BSO.Consulta(SqlStringExclusividade);

                if (ListaExclusividade.Vazia() == false)
                    MessageBox.Show("O Artigo: " + this.textEditArtigo.Text + " tem acordos estipulados!", "Atenção!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textEditArtigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F4)
                    this.textEditArtigo.EditValue = PSO.Listas.GetF4SQL("Artigo", "Select Artigo, (Descricao + ' ' + CDU_DescricaoExtra) as Descricao, CDU_DescricaoReferencia as Referencia, CDU_DescricaoNE as 'NE Desc'  from  Artigo where ArtigoAnulado=0 and Artigo in (select distinct PRD_Artigo from VIM_ArtigoOperacoes)", "Artigo");
            }
        }

        private void textEditCustoFixo_EditValueChanged(object sender, EventArgs e)
        {
            CalcularTotaisTextBox();
        }

        private void textEditCustoProd_EditValueChanged(object sender, EventArgs e)
        {
            CalcularTotaisTextBox();
        }

        private void textEditMargem_EditValueChanged(object sender, EventArgs e)
        {
            CalcularTotaisTextBox();
        }

        private void textEditPrecoFinal_EditValueChanged(object sender, EventArgs e)
        {
            PreencheVariacoes(AlternativaSel);
        }

        private void textEditValores_EditValueChanged(object sender, EventArgs e)
        {
            CalcularTotaisTextBox();
        }

        private void barButtonItemCopiarTabela_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.List_Variacoes.List(i, 0) = ListaVariacoes("PRD_EscalaInferior");
            //this.List_Variacoes.List(i, 1) = ListaVariacoes("PRD_EscalaSuperior");
            //this.List_Variacoes.List(i, 2) = ListaVariacoes("PRD_VariacaoPercent");

            if (this.textEditArtigo.Text + "" != "")
            {
                string tabela;
                double min, max;
                tabela = "<html><head><style>" + "td {border: solid black;border-width: 1px;padding-left:5px;padding-right:5px;padding-top:1px;padding-bottom:1px;font: 11px arial}" + "</style></head> <body><b>Inovafil, S.A.<b> <br>" + "<br> " + this.textEditArtigo.Text + " - " + this.textEditDescricao.Text + "  <br> <br>" + "<table cellpadding=0 cellspacing=0 border=0>" + "    <td style=\"text-align:center\" colspan=\"3\" bgcolor=#72C6FF><b>Tabela de Preços</b></td></tr>" + " <tr>   <td bgcolor=#72C6FF><b>Min</b></td>" + "    <td bgcolor=#72C6FF><b>Max</b></td>" + " <td bgcolor=#72C6FF><b>Preço</b></td></tr>";

                for (int i = 4, loopTo = vmpGridViewVariacoes.RowCount - 4; i <= loopTo; i++)
                {
                    min = Double.Parse(vmpGridViewVariacoes.GetRowCellValue(i, "PRD_EscalaInferior").ToString());
                    max = Double.Parse(vmpGridViewVariacoes.GetRowCellValue(i, "PRD_EscalaSuperior").ToString());
                    if (min == Convert.ToDouble("150,01"))
                    {
                        min = 250d;
                    }

                    if (min == Convert.ToDouble("1200"))
                    {
                        min = 2000d;
                    }

                    if (max == Convert.ToDouble("1199,99"))
                    {
                        max = 1999d;
                    }

                    if (max == Convert.ToDouble("1499,99"))
                    {
                        max = 10000d;
                    }

                    tabela = tabela + "<tr><td>" + min + "</td>" + "<td>" + max + "</td>" + "<td>" + Math.Ceiling(Decimal.Parse(vmpGridViewVariacoes.GetRowCellValue(i, "Preço Final").ToString())) + "€</td></tr>";
                }

                tabela = tabela + "    </table><br><br>" + "   </body></html>";

                Clipboard.SetDataObject(tabela, true);


            }
        }
    }
}