using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Inventory.Editors;

namespace DevolucaoRemonta
{
    public class InvIsEditorTransferenciasStock : EditorTransferenciasStock
    {
        public override void DepoisDeGravar(string Filial, string Serie, string Tipo, int Numdoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Serie, Tipo, Numdoc, e);
            if (Module1.VerificaToken("DevolucaoRemonta") == 1)
            {
                int j;
                if (this.DocumentoTransferencia.Tipodoc == "DEV")
                {
                    var loopTo = this.DocumentoTransferencia.LinhasOrigem.NumItens;
                    for (j = 1; j <= loopTo; j++)
                    {
                        if (this.DocumentoTransferencia.LinhasOrigem.GetEdita(j).Artigo + "" != "" & this.DocumentoTransferencia.LinhasOrigem.GetEdita(j).Lote + "" != "")
                        {
                            BSO.DSO.ExecuteSQL("UPDATE ArtigoMoeda SET PvP6 = '" + Strings.Replace(this.DocumentoTransferencia.LinhasOrigem.GetEdita(j).PrecUnit.ToString(), ",", ".") + "' WHERE Artigo = '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(j).Artigo + "'");
                        }
                    }
                }
            }
        }
    }
}