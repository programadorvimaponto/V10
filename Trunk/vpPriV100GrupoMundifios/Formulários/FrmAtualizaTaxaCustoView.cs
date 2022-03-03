using Microsoft.VisualBasic;
using Primavera.Extensibility.CustomForm;
using StdBE100;
using System;
using System.Windows.Forms;

namespace GrupoMundifios.Formulários
{
    public partial class FrmAtualizaTaxaCustoView : CustomForm
    {
        public FrmAtualizaTaxaCustoView()
        {
            InitializeComponent();
        }

        private int p_CasasDecimaisQtd;
        private int p_CasasDecimaisPerc;
        private bool p_MargemForte;

        private int p_CasasDecimaisDesperdicio;

        private void FrmAtualizaTaxaCustoView_Load(object sender, EventArgs e)
        {
            if (FindForm() is Form pai)
            {
                pai.MinimumSize = pai.Size;
                pai.MaximumSize = pai.Size;
            }
        }

        private void barButtonItemCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void barButtonItemConfirmar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bool.Parse(this.checkEditTaxaDesperdicio.EditValue.ToString()) == true)
            {
                AtualizaTaxaDesperdicio(p_MargemForte);
            }
            else if (bool.Parse(this.checkEditCustosProcesso.EditValue.ToString()) == true)
            {
                AtualizaCustoProcesso();
            }
        }

        public void CarregaParametrosVMPPlan()
        {
            StdBELista Lista_VMPPlan_Paramteros;
            string SqlString_VMPPlan_Paramteros;

            // *****************************************************************************************************************************************************************
            // ###  Parâmetro das casas décimais usadas na quantidade e na percentagem do desperdício
            // *****************************************************************************************************************************************************************
            SqlString_VMPPlan_Paramteros = "SELECT CAST(ISNULL(CDU_Valor3, 3) AS INT) CasasDecimaisQtd, CAST(ISNULL(CDU_Valor4, 1) AS INT) CasasDecimaisPerc "
                                        + "FROM TDU_SecParametros "
                                        + "WHERE CDU_Parametro = 'CasasDecimais'";

            Lista_VMPPlan_Paramteros = BSO.Consulta(SqlString_VMPPlan_Paramteros);

            if (Lista_VMPPlan_Paramteros.NumLinhas() > 0)
            {
                Lista_VMPPlan_Paramteros.Inicio();
                p_CasasDecimaisQtd = Lista_VMPPlan_Paramteros.Valor("CasasDecimaisQtd");
                p_CasasDecimaisPerc = Lista_VMPPlan_Paramteros.Valor("CasasDecimaisPerc");
            }
            else
                MessageBox.Show("Não estão definidos os parâmetros no VMP Plan", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // *****************************************************************************************************************************************************************
            // ###  Parâmetro das casas décimais usadas na quantidade e na percentagem do desperdício
            // *****************************************************************************************************************************************************************

            // *****************************************************************************************************************************************************************
            // ###  Parâmetro da margem forte
            // *****************************************************************************************************************************************************************
            SqlString_VMPPlan_Paramteros = "SELECT CAST(ISNULL(CDU_Valor10, 0) AS BIT) MargemForte "
                                        + "FROM TDU_SecParametros "
                                        + "WHERE CDU_Parametro = 'OrdensFabrico'";

            Lista_VMPPlan_Paramteros = BSO.Consulta(SqlString_VMPPlan_Paramteros);

            if (Lista_VMPPlan_Paramteros.NumLinhas() > 0)
            {
                Lista_VMPPlan_Paramteros.Inicio();
                p_MargemForte = Lista_VMPPlan_Paramteros.Valor("MargemForte");
            }
            else
                MessageBox.Show("Não estão definidos os parâmetros no VMP Plan", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // *****************************************************************************************************************************************************************
            // ###  Parâmetro da margem forte
            // *****************************************************************************************************************************************************************

            // *****************************************************************************************************************************************************************
            // ###  Parâmetro das casas décimais usadas no desperdício
            // *****************************************************************************************************************************************************************
            p_CasasDecimaisDesperdicio = 5;
        }

        public void AtualizaTaxaDesperdicio(bool v_MargemForte)
        {
            StdBELista Lista_VMPPlan_ArtigosFT;
            string SqlString_VMPPlan_ArtigosFT;
            int i;
            double Consumo;

            SqlString_VMPPlan_ArtigosFT = "SELECT A.Artigo, A.Descricao, A.CDU_GrupoTaxaDesperdicio, GTD.CDU_Taxa, AC.PRD_IDArtigoComponente, AC.PRD_Componente, AC.PRD_Consumo, AC.PRD_ConsumoPor, AC.PRD_Desperdicio "
                                        + "FROM Artigo A INNER JOIN TDU_GrupoTaxaDesperdicio GTD ON GTD.CDU_Codigo = A.CDU_GrupoTaxaDesperdicio INNER JOIN VIM_Alternativas AL ON A.Artigo = AL.PRD_Artigo INNER JOIN VIM_ArtigoOperacoes AO ON AO.PRD_Artigo = AL.PRD_Artigo AND AO.PRD_Alternativa = AL.PRD_Alternativa INNER JOIN VIM_ArtigoComponentes AC ON AC.PRD_Artigo = AO.PRD_Artigo AND AC.PRD_IDOperacao = AO.PRD_IDOperacao "
                                        + "WHERE A.CDU_GrupoTaxaDesperdicio <> '' OR A.CDU_GrupoTaxaDesperdicio IS NOT NULL";

            Lista_VMPPlan_ArtigosFT = BSO.Consulta(SqlString_VMPPlan_ArtigosFT);

            if (Lista_VMPPlan_ArtigosFT.NumLinhas() > 0)
            {
                Lista_VMPPlan_ArtigosFT.Inicio();
                for (i = 1; i <= Lista_VMPPlan_ArtigosFT.NumLinhas(); i++)
                {
                    // *****************************************************************************************************************************************************************
                    // ###  Cálculo do Despercício
                    // *****************************************************************************************************************************************************************
                    if (v_MargemForte == true)
                        Consumo = Arredondamento((Lista_VMPPlan_ArtigosFT.DaValor<double>("PRD_Consumo") / (1 - Arredondamento(Lista_VMPPlan_ArtigosFT.DaValor<double>("CDU_Taxa"), p_CasasDecimaisPerc) / 100)) - Lista_VMPPlan_ArtigosFT.DaValor<double>("PRD_Consumo"), p_CasasDecimaisDesperdicio);
                    else
                        Consumo = Arredondamento((Arredondamento(Lista_VMPPlan_ArtigosFT.DaValor<double>("CDU_Taxa"), p_CasasDecimaisPerc) / 100D) * Lista_VMPPlan_ArtigosFT.DaValor<double>("PRD_Consumo"), p_CasasDecimaisDesperdicio);                    // *****************************************************************************************************************************************************************
                                                                                                                                                                                                                                        // ###  Cálculo do Despercício
                                                                                                                                                                                                                                        // *****************************************************************************************************************************************************************

                    // *****************************************************************************************************************************************************************
                    // ###  Atualizado o Despercício na Ficha Técnica
                    // *****************************************************************************************************************************************************************
                    BSO.DSO.ExecuteSQL("UPDATE VIM_ArtigoComponentes SET PRD_ConsumoPor = 1, PRD_Desperdicio = " + Strings.Replace(Consumo.ToString(), ",", ".") + " WHERE PRD_IDArtigoComponente = '" + Lista_VMPPlan_ArtigosFT.Valor("PRD_IDArtigoComponente") + "'");
                    // *****************************************************************************************************************************************************************
                    // ###  Atualizado o Despercício na Ficha Técnica
                    // *****************************************************************************************************************************************************************

                    Lista_VMPPlan_ArtigosFT.Seguinte();
                }
                MessageBox.Show("Taxas de desperdício atualizadas com sucesso.", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Não existem artigos para atualizar.", "ERRO!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void AtualizaCustoProcesso()
        {
            StdBELista Lista_VMPPlan_ArtigosFT;
            string SqlString_VMPPlan_ArtigosFT;
            int i;
            double Consumo;

            SqlString_VMPPlan_ArtigosFT = "SELECT A.Artigo, A.Descricao, A.CDU_GrupoCustoProcesso, GCP.CDU_Custo, AO.PRD_IDOperacao "
                                    + "FROM Artigo A INNER JOIN TDU_GrupoCustoProcesso GCP ON GCP.CDU_Codigo = A.CDU_GrupoCustoProcesso INNER JOIN VIM_Alternativas AL ON A.Artigo = AL.PRD_Artigo INNER JOIN VIM_ArtigoOperacoes AO ON AO.PRD_Artigo = AL.PRD_Artigo AND AO.PRD_Alternativa = AL.PRD_Alternativa INNER JOIN VIM_OperacoesProducao OP ON OP.PRD_Operacao = AO.PRD_OperacaoProducao AND OP.PRD_Operacao = 'OP.0001' "
                                    + "WHERE A.CDU_GrupoCustoProcesso <> '' OR A.CDU_GrupoCustoProcesso IS NOT NULL";

            Lista_VMPPlan_ArtigosFT = BSO.Consulta(SqlString_VMPPlan_ArtigosFT);

            if (Lista_VMPPlan_ArtigosFT.NumLinhas() > 0)
            {
                Lista_VMPPlan_ArtigosFT.Inicio();
                for (i = 1; i <= Lista_VMPPlan_ArtigosFT.NumLinhas(); i++)
                {
                    // *****************************************************************************************************************************************************************
                    // ###  Atualiza o Preço da operação Fiação na Ficha Técnica
                    // *****************************************************************************************************************************************************************
                    BSO.DSO.ExecuteSQL("UPDATE VIM_ArtigoOperacoes SET PRD_Preco = " + Strings.Replace(Lista_VMPPlan_ArtigosFT.Valor("CDU_Custo"), ",", ".") + " WHERE PRD_IDOperacao = '" + Lista_VMPPlan_ArtigosFT.Valor("PRD_IDOperacao") + "'");
                    // *****************************************************************************************************************************************************************
                    // ###  Atualiza o Preço da operação Fiação na Ficha Técnica
                    // *****************************************************************************************************************************************************************

                    Lista_VMPPlan_ArtigosFT.Seguinte();
                }
                MessageBox.Show("Custos da operação 'Fiação' atualizadas com sucesso.", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Não existem artigos para atualizar.", "ERRO!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public double Arredondamento(double Numero, int Decimais)
        {
            return Convert.ToDouble(Arredondamento(Convert.ToDecimal(Numero), Decimais));
        }

        public float Arredondamento(float Numero, int Decimais)
        {
            return Convert.ToSingle(Arredondamento(Convert.ToDecimal(Numero), Decimais));
        }

        public decimal Arredondamento(decimal Numero, int Decimais)
        {
            switch (Decimais)
            {
                case 0:
                    {
                        return System.Convert.ToDecimal(Strings.Format(Numero, "0"));
                        break;
                    }

                case 1:
                    {
                        return System.Convert.ToDecimal(Strings.Format(Numero, "0.0"));
                        break;
                    }

                case 2:
                    {
                        return System.Convert.ToDecimal(Strings.Format(Numero, "0.00"));
                        break;
                    }

                case 3:
                    {
                        return System.Convert.ToDecimal(Strings.Format(Numero, "0.000"));
                        break;
                    }

                case 4:
                    {
                        return System.Convert.ToDecimal(Strings.Format(Numero, "0.0000"));
                        break;
                    }

                case 5:
                    {
                        return System.Convert.ToDecimal(Strings.Format(Numero, "0.00000"));
                        break;
                    }

                case 6:
                    {
                        return System.Convert.ToDecimal(Strings.Format(Numero, "0.000000"));
                        break;
                    }

                case 7:
                    {
                        return System.Convert.ToDecimal(Strings.Format(Numero, "0.0000000"));
                        break;
                    }

                case 8:
                    {
                        return System.Convert.ToDecimal(Strings.Format(Numero, "0.00000000"));
                        break;
                    }

                case 9:
                    {
                        return System.Convert.ToDecimal(Strings.Format(Numero, "0.000000000"));
                        break;
                    }

                case 10:
                    {
                        return System.Convert.ToDecimal(Strings.Format(Numero, "0.0000000000"));
                        break;
                    }
                default:
                    return System.Convert.ToDecimal(Strings.Format(Numero, "0"));
                    break;
            }
        }

    }
}