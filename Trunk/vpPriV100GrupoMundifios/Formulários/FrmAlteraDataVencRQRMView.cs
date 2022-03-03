using Generico;
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
using VndBE100;

namespace GrupoMundifios.Formulários
{
    public partial class FrmAlteraDataVencRQRMView : CustomForm
    {
        public FrmAlteraDataVencRQRMView()
        {
            InitializeComponent();
        }


        StdBELista listadocs;
        string SqlStringDocs;
        long k;
        VndBEDocumentoVenda DocVendaRQRM = new VndBEDocumentoVenda();

        private void barButtonItemCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void barButtonItemConfirmar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            try
            {
                SqlStringDocs = "SELECT dbo.CabecDoc.Id, dbo.CabecDoc.Filial, dbo.CabecDoc.TipoDoc, dbo.CabecDoc.Serie, dbo.CabecDoc.NumDoc, dbo.CabecDoc.CondPag, dbo.CondPag.CDU_RQ, dbo.CondPag.CDU_RM, dbo.CabecDoc.CDU_AlteradaDataVenc " + "FROM dbo.CabecDoc INNER JOIN dbo.CondPag ON dbo.CabecDoc.CondPag = dbo.CondPag.CondPag INNER JOIN dbo.DocumentosVenda ON dbo.CabecDoc.TipoDoc = dbo.DocumentosVenda.Documento " + "WHERE (dbo.DocumentosVenda.TipoDocumento = 4) AND (dbo.CabecDoc.CDU_AlteradaDataVenc = 0) AND (dbo.CondPag.CDU_RQ = 1) OR " + "(dbo.DocumentosVenda.TipoDocumento = 4) AND (dbo.CabecDoc.CDU_AlteradaDataVenc = 0) AND (dbo.CondPag.CDU_RM = 1)";


                listadocs = BSO.Consulta(SqlStringDocs);


                if (listadocs.Vazia() == false)
                {
                    listadocs.Inicio();
                    for (int i = 0, loopTo = listadocs.NumLinhas() - 1; i <= loopTo; i++)
                    {


                        DocVendaRQRM = null;


                        DocVendaRQRM = BSO.Vendas.Documentos.Edita(listadocs.Valor("Filial"), listadocs.Valor("TipoDoc"), listadocs.Valor("Serie"), listadocs.Valor("NumDoc"));

                        DocVendaRQRM.DataVenc = Module1.NovaDataVencimento(DocVendaRQRM.DataDoc, DocVendaRQRM.CondPag, DocVendaRQRM.TipoEntidade, DocVendaRQRM.Entidade);
                        BSO.Vendas.Documentos.Actualiza(DocVendaRQRM);
                        BSO.DSO.ExecuteSQL("UPDATE CabecDoc SET CDU_AlteradaDataVenc = 1 WHERE Id = '" + listadocs.Valor("Id") + "'");
                        listadocs.Seguinte();
                    }

                    MessageBox.Show("Documentos corrigidos com sucesso.", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Não existem documentos a corrigir.", "Informação!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                return;


            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.ToString() + '\r' + DocVendaRQRM.Tipodoc + " " + DocVendaRQRM.NumDoc + "/" + DocVendaRQRM.Serie,"Erro!",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }

        }

        private void FrmAlteraDataVencRQRMView_Load(object sender, EventArgs e)
        {
            if (FindForm() is Form pai)
            {
                pai.MinimumSize = pai.Size;
                pai.MaximumSize = pai.Size;
            }
        }
    }
}
