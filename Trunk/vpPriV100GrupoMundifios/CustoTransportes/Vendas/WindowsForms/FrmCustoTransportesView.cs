using Microsoft.VisualBasic;
using Primavera.Extensibility.CustomForm;
using Primavera.Extensibility.Sales.Editors;
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

namespace CustoTransportes
{
    public partial class FrmCustoTransportesView : CustomForm
    {
        public FrmCustoTransportesView()
        {
            InitializeComponent();
        }
        double c;
        double c2;
        public VndBE100.VndBEDocumentoVenda DocumentoVenda { get; set; }
        private void barButtonItemRemover_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

                this.TextEditCodigoFornecedor.EditValue = string.Empty;
                this.TextEditIdFatura.EditValue = string.Empty;
                this.TextEditFatura.EditValue = string.Empty;
                this.TextEditCusto.EditValue = "0";

                this.TextEditFatura2.EditValue = string.Empty;
                this.TextEditCusto2.EditValue = "0";
                this.TextEditTotal.EditValue = "0";
                this.TextEditIdFatura2.EditValue = string.Empty;
                this.MemoEditObsTransporte.EditValue = string.Empty;



                BSO.DSO.ExecuteSQL("update CabecDoc set CDU_EntidadeTransporte='" + this.TextEditCodigoFornecedor.EditValue + "', CDU_IdFaturaTransporte='" + this.TextEditIdFatura.EditValue + "', CDU_IdFaturaTransporte2='" + this.TextEditIdFatura2.EditValue + "', CDU_FaturaTransporte='" + this.TextEditFatura.EditValue + "', CDU_FaturaTransporte2='" + this.TextEditFatura2.EditValue + "', CDU_CustoTransporte='" + this.TextEditCusto.EditValue + "', CDU_CustoTransporte2='" + this.TextEditCusto2.EditValue + "', CDU_CustoTransporteTotal='" + this.TextEditTotal.EditValue + "', CDU_Obstransporte='" + this.MemoEditObsTransporte.EditValue + "' where Id='" + DocumentoVenda.ID + "'");
                BSO.DSO.ExecuteSQL("update ln set ln.CDU_CustoTransporte=ln.Quantidade/(select sum(ln2.Quantidade) from LinhasDoc ln2 inner join Artigo a2 on a2.Artigo=ln2.Artigo where ln2.IdCabecDoc=cd.Id)*cd.CDU_CustoTransporte  from CabecDoc cd inner join LinhasDoc ln on ln.IdCabecDoc=cd.Id inner join Artigo a on a.Artigo=ln.Artigo where cd.Id='" + DocumentoVenda.ID + "'");


            DialogResult = DialogResult.Cancel;

        }

        private void barButtonItemGravar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (TextEditCodigoFornecedor.EditValue.ToString() != string.Empty)
                Gravar();
            else
                MessageBox.Show("O Fornecedor não está identificado.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void CarregaDados()
        {
            if (this.TextEditFornecedor.EditValue.ToString() == string.Empty)
            {
                this.TextEditCodigoFornecedor.EditValue = string.Empty;
                return;
            }
        }

        private void Gravar()
        {
            try
            {
                BSO.DSO.ExecuteSQL("update CabecDoc set CDU_EntidadeTransporte='" + this.TextEditCodigoFornecedor.EditValue + "', CDU_IdFaturaTransporte='" + this.TextEditIdFatura.EditValue + "', CDU_FaturaTransporte='" + this.TextEditFatura.EditValue + "', CDU_FaturaTransporte2='" + this.TextEditFatura2.EditValue + "', CDU_CustoTransporteTotal=replace('" + this.TextEditTotal.EditValue + "',',','.'), CDU_CustoTransporte=replace('" + this.TextEditCusto.EditValue + "',',','.'), CDU_CustoTransporte2=replace('" + this.TextEditCusto2.EditValue + "',',','.'), CDU_Obstransporte='" + this.MemoEditObsTransporte.EditValue + "' where Id='" + DocumentoVenda.ID + "'");

                BSO.DSO.ExecuteSQL("update ln set ln.CDU_CustoTransporte=ln.Quantidade/(select sum(ln2.Quantidade) from LinhasDoc ln2 inner join Artigo a2 on a2.Artigo=ln2.Artigo where ln2.IdCabecDoc=cd.Id)*cd.CDU_CustoTransporteTotal/ln.Quantidade  from CabecDoc cd inner join LinhasDoc ln on ln.IdCabecDoc=cd.Id inner join Artigo a on a.Artigo=ln.Artigo where cd.Id='" + DocumentoVenda.ID + "'");

                DialogResult = DialogResult.OK;

            }
            catch
            {
                MessageBox.Show("Erro ao Gravar", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void TextEditCusto_EditValueChanged(object sender, EventArgs e)
        {

            if (!Information.IsNumeric(TextEditCusto.EditValue))
            {
                MessageBox.Show("Só são permitidos numeros!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TextEditCusto.EditValue = "0";

            }

            if(TextEditTotal.EditValue is int)
            {
                c = double.Parse(Strings.Replace(TextEditCusto.EditValue.ToString(), ".", ","));
                c2 = double.Parse(Strings.Replace(TextEditCusto2.EditValue.ToString(), ".", ","));
                this.TextEditTotal.EditValue = c + c2;
            }


        }

        private void TextEditCusto2_EditValueChanged(object sender, EventArgs e)
        {


            if (!Information.IsNumeric(TextEditCusto2.EditValue))
            {
                MessageBox.Show("Só são permitidos numeros!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TextEditCusto2.EditValue = "0";

            }

            if (TextEditTotal.EditValue is int)
            {
                c = double.Parse(Strings.Replace(TextEditCusto.EditValue.ToString(), ".", ","));
                c2 = double.Parse(Strings.Replace(TextEditCusto2.EditValue.ToString(), ".", ","));
                this.TextEditTotal.EditValue = c + c2;
            }

        }

        private void TextEditFornecedor_EditValueChanged(object sender, EventArgs e)
        {
            CarregaDados();
        }

        private void TextEditCodigoFornecedor_EditValueChanged(object sender, EventArgs e)
        {

            this.TextEditFornecedor.EditValue = BSO.Base.Fornecedores.DaValorAtributo(this.TextEditCodigoFornecedor.EditValue.ToString(), "Nome");
        }

        private void TextEditCodigoFornecedor_KeyDown(object sender, KeyEventArgs e)
        {

            if(e.KeyCode==Keys.F4)
            {
                this.TextEditCodigoFornecedor.EditValue = PSO.Listas.GetF4SQL("Fornecedores", "select fornecedor, nome as 'Nome Fornecedor' from fornecedores where CDU_TipoFornecedor='001'", "Fornecedor");
            }

        }

        private void TextEditFatura_KeyDown(object sender, KeyEventArgs e)
        {

            if(e.KeyCode==Keys.F4)
            {
                if(TextEditCodigoFornecedor.EditValue.ToString() == string.Empty)
                    MessageBox.Show("O Fornecedor não está identificado.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    this.TextEditIdFatura.EditValue = PSO.Listas.GetF4SQL("Lista Faturas Pendentes", "select h.DataDoc as 'Data', concat(h.TipoDoc,' ', h.NumDocint,'/', h.Serie) as 'N/Doc', h.NumDoc as 'V/Doc',abs(h.ValorTotal)-abs(h.TotalIva) as 'SemIVA', h.ValorTotal*-1 as 'ValorTotal', h.Id from Historico h inner join DocumentosCCT ct on ct.Documento=h.TipoDoc where ct.Natureza='D' and h.Entidade='" + this.TextEditCodigoFornecedor.EditValue + "' and h.DataLiq is null and h.TipoEntidade='F'", "Id");
                    if(TextEditIdFatura.EditValue.ToString() != string.Empty)
                    {
                        StdBELista listFaf;
                        listFaf = BSO.Consulta("select h.NumDoc as 'Doc',abs(h.ValorTotal)-abs(h.TotalIva) as 'SemIVA', h.ValorTotal*-1 as 'ValorTotal'  from Historico h where h.Id='" + this.TextEditIdFatura.Text + "'");
                        listFaf.Inicio();
                        this.TextEditFatura.EditValue = listFaf.Valor("Doc");
                        this.TextEditCusto.EditValue = listFaf.Valor("SemIVA");
                    }
                }
            }

        }

        private void TextEditFatura2_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F4)
            {
                if (TextEditCodigoFornecedor.EditValue.ToString() == string.Empty)
                    MessageBox.Show("O Fornecedor não está identificado.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    this.TextEditIdFatura2.EditValue = PSO.Listas.GetF4SQL("Lista Faturas Pendentes", "select h.DataDoc as 'Data', concat(h.TipoDoc,' ', h.NumDocint,'/', h.Serie) as 'N/Doc', h.NumDoc as 'V/Doc',abs(h.ValorTotal)-abs(h.TotalIva) as 'SemIVA', h.ValorTotal*-1 as 'ValorTotal', h.Id from Historico h inner join DocumentosCCT ct on ct.Documento=h.TipoDoc where ct.Natureza='D' and h.Entidade='" + this.TextEditCodigoFornecedor.EditValue + "' and h.DataLiq is null and h.TipoEntidade='F'", "Id");
                    if (TextEditIdFatura2.EditValue.ToString() != string.Empty)
                    {
                        StdBELista listFaf;
                        listFaf = BSO.Consulta("select h.NumDoc as 'Doc',abs(h.ValorTotal)-abs(h.TotalIva) as 'SemIVA', h.ValorTotal*-1 as 'ValorTotal'  from Historico h where h.Id='" + this.TextEditIdFatura2.EditValue + "'");
                        listFaf.Inicio();
                        this.TextEditFatura2.EditValue = listFaf.Valor("Doc");
                        this.TextEditCusto2.EditValue = listFaf.Valor("SemIVA");
                    }
                }
            }

        }

        private void TextEditFatura_EditValueChanged(object sender, EventArgs e)
        {

            StdBELista listGRs;
            if(TextEditIdFatura.EditValue.ToString() != string.Empty)
            {
                listGRs = BSO.Consulta("select  concat(h.TipoDoc,' ', h.NumDoc,'/', h.Serie) as 'Doc', h.Data, h.Nome  from CabecDoc h where h.CDU_IdFaturaTransporte='" + this.TextEditIdFatura.EditValue + "' and h.Id!='" + DocumentoVenda.ID + "'");
                listGRs.Inicio();

                if(listGRs.Vazia()==false)
                {
                    string msg;
                    long i;
                    msg = "Já existem os seguintes documentos associados a esta fatura:" + Strings.Chr(13) + Strings.Chr(13);
                    for (i = 1; i <= listGRs.NumLinhas(); i++)
                    {
                        msg = msg + listGRs.Valor("Data") + " " + listGRs.Valor("Doc") + " - " + listGRs.Valor("Nome") + Strings.Chr(13);
                        listGRs.Seguinte();
                    }
                    MessageBox.Show(msg, "Fatura Associada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void FrmCustoTransportesView_Load(object sender, EventArgs e)
        {

            StdBELista listGR;

            listGR = BSO.Consulta("select  h.CDU_EntidadeTransporte, h.CDU_IdFaturaTransporte, h.CDU_FaturaTransporte, isnull(h.CDU_CustoTransporte,0) + 0 as 'CDU_CustoTransporte' , h.CDU_IdFaturaTransporte2, h.CDU_FaturaTransporte2, isnull(h.CDU_CustoTransporte2,0) + 0 as 'CDU_CustoTransporte2', isnull(h.cdu_custotransportetotal,0) + 0 as 'cdu_custotransportetotal' , h.cdu_obstransporte  from CabecDoc h where h.Id='" + DocumentoVenda.ID + "'");
            listGR.Inicio();

            if(listGR.Vazia()==false)
            {
                if (listGR.Valor("CDU_EntidadeTransporte") is string entidade) this.TextEditCodigoFornecedor.EditValue = entidade; else this.TextEditCodigoFornecedor.EditValue = "";
                if (listGR.Valor("CDU_IdFaturaTransporte") is string idfatura) this.TextEditIdFatura.EditValue = idfatura; else this.TextEditIdFatura.EditValue = "";
                if (listGR.Valor("CDU_FaturaTransporte") is string fatura) this.TextEditFatura.EditValue = fatura; else this.TextEditFatura.EditValue = 0;

                if (listGR.Valor("CDU_CustoTransporte") is string custo) this.TextEditCusto.EditValue = custo; else this.TextEditCusto.EditValue = 0;

                if (listGR.Valor("CDU_IdFaturaTransporte2") is string idfatura2) this.TextEditIdFatura2.EditValue = idfatura2; else this.TextEditIdFatura2.EditValue = "";
                if (listGR.Valor("CDU_FaturaTransporte2") is string fatura2) this.TextEditFatura2.EditValue = fatura2; else this.TextEditFatura2.EditValue = "";
                if (listGR.Valor("CDU_CustoTransporte2") is string custo2) this.TextEditCusto2.EditValue = custo2; else this.TextEditCusto2.EditValue = 0;
                if (listGR.Valor("cdu_custotransportetotal") is string custotransp) this.TextEditTotal.EditValue = custotransp; else this.TextEditTotal.EditValue = "";
                if (listGR.Valor("CDU_Obstransporte") is string obstransp) this.MemoEditObsTransporte.EditValue = obstransp; else this.MemoEditObsTransporte.EditValue = "";

            }

        }

        private void barButtonItemFechar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }
    }
}
