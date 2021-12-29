using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using System.Windows.Forms;
using Vimaponto.PrimaveraV100;

namespace Arxivar
{
    public class VndIsEditorVendas : EditorVendas
    {
        private bool VarArxivar;

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            VarArxivar = false;

            // ##########################################################################################
            // ##Valida se a fatura já foi convertida para ficheiro XML         -        15/05/2019 JFC##
            // ##########################################################################################
            if (BSO.Vendas.TabVendas.DaValorAtributo(this.DocumentoVenda.Tipodoc, "TipoDocumento") == 4)
            {
                if (MessageBox.Show("Export to Arxivar?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    VarArxivar = true;

                if (VarArxivar)
                {
                    string strExportXML;
                    StdBELista listaExportXML;

                    strExportXML = "select ExportXML from Arxivar.Arxivar.dbo.Fatture_Testata where Fatture_ID='" + this.DocumentoVenda.Tipodoc + this.DocumentoVenda.NumDoc + this.DocumentoVenda.Serie + "'";

                    listaExportXML = BSO.Consulta(strExportXML);

                    if (listaExportXML.Vazia() == false)
                    {
                        listaExportXML.Inicio();

                        if (listaExportXML.Valor("ExportXML") == "1")
                        {
                            MessageBox.Show("La fattura è già stata convertita in XML, non è possibile salvare le modifiche!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            VarArxivar = false;
                        }
                    }
                }
            }
        }

        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            // ##########################################################################################
            // ##Upload dos dados para o Azure        -        15/05/2019 JFC                          ##
            // ##########################################################################################

            if (VarArxivar)
            {
                string strAzure;

                PriV100Api.BSO.DSO.ExecuteSQL("exec primunditalia.dbo.spArxivar '" + this.DocumentoVenda.ID + "','" + this.DocumentoVenda.Tipodoc + this.DocumentoVenda.NumDoc + this.DocumentoVenda.Serie + "'");
            }
        }
    }
}