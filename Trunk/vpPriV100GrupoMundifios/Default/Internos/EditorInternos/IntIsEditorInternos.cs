using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Internal.Editors;

namespace Default
{
    public class IntIsEditorInternos : EditorInternos
    {
        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
            {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);

            if (Module1.VerificaToken("Default") == 1)
            {
                if (BSO.Base.Artigos.DaValorAtributo(this.DocumentoInterno.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtra") + "" != "")
                {
                    this.DocumentoInterno.Linhas.GetEdita(NumLinha).Descricao = BSO.Base.Artigos.DaValorAtributo(Artigo, "Descricao") + " " + BSO.Base.Artigos.DaValorAtributo(this.DocumentoInterno.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtra");
                    this.DocumentoInterno.Linhas.GetEdita(NumLinha).CamposUtil["CDU_ReferenciaCliente"].Valor = BSO.Base.Artigos.DaValorAtributo(this.DocumentoInterno.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtra");
                }
            }
        }
    }
}