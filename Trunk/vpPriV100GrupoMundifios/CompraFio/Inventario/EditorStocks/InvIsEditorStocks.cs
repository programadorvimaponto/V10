using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Inventory.Editors;

namespace CompraFio
{
    public class InvIsEditorStocks : EditorTransferenciasStock
    {
        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);

            if (Module1.VerificaToken("CompraFio") == 1)
            {
                if (BSO.Base.Artigos.DaValorAtributo(DocumentoTransferencia.LinhasOrigem.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtra") + "" != "")
                    this.DocumentoTransferencia.LinhasOrigem.GetEdita(NumLinha).Descricao = BSO.Base.Artigos.DaValorAtributo(Artigo, "Descricao") + " " + BSO.Base.Artigos.DaValorAtributo(DocumentoTransferencia.LinhasOrigem.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtra");
            }
        }
    }
}