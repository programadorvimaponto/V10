using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;

namespace LoteEncomendaIgualGuia
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            if (Module1.VerificaToken("LoteEncomendaIgualGuia") == 1)
            {
                // *******************************************************************************************************************************************
                // #### COLOCAR LOTE DA ENCOMENDA IGUAL AO DA GUIA #### Pedido pela Dª Goretti dia 19/03/2015
                // *******************************************************************************************************************************************
                int j;
                if ((this.DocumentoVenda.Tipodoc == "GR" | this.DocumentoVenda.Tipodoc == "GT"))
                {
                    for (j = 1; j <= this.DocumentoVenda.Linhas.NumItens; j++)
                    {
                        if (this.DocumentoVenda.Linhas.GetEdita(j).Artigo + "" != "" & this.DocumentoVenda.Linhas.GetEdita(j).Lote + "" != "")
                            BSO.DSO.ExecuteSQL("UPDATE LinhasDoc SET Lote = '" + this.DocumentoVenda.Linhas.GetEdita(j).Lote + "' WHERE Id = '" + this.DocumentoVenda.Linhas.GetEdita(j).IDLinhaOriginal + "'");
                    }
                }
            }
        }
    }
}