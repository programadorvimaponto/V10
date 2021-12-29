using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;

namespace IdiomaArtigo
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);

            if (Module1.VerificaToken("IdiomaArtigo") == 1)
            {
                // Se    Tiver entidade final
                // Idioma do cliente Final <> Idioma do cliente Principal
                // Se existir tradução na tabela de idiomas
                if (this.DocumentoVenda.CamposUtil["CDU_EntidadeFinalGrupo"].Valor + "" != "" & this.DocumentoVenda.CamposUtil["CDU_IdiomaEntidadeFinalGrupo"].Valor + "" != "" & this.DocumentoVenda.CamposUtil["CDU_IdiomaEntidadeFinalGrupo"].Valor + "" != BSO.Base.Clientes.DaValorAtributo(this.DocumentoVenda.Entidade, "Idioma") & BSO.Base.ArtigosIdiomas.DaValorAtributo(Artigo, BSO.Base.Clientes.DaValorAtributo(this.DocumentoVenda.Entidade, "Idioma"), "Descricao") + "" != "")
                {
                    if (BSO.Base.ArtigosIdiomas.DaValorAtributo(Artigo, this.DocumentoVenda.CamposUtil["CDU_IdiomaEntidadeFinalGrupo"].Valor + "", "Descricao") != "")
                        this.DocumentoVenda.Linhas.GetEdita(NumLinha).Descricao = BSO.Base.ArtigosIdiomas.DaValorAtributo(Artigo, this.DocumentoVenda.CamposUtil["CDU_IdiomaEntidadeFinalGrupo"].Valor + "", "Descricao");
                    else
                        this.DocumentoVenda.Linhas.GetEdita(NumLinha).Descricao = BSO.Base.Artigos.DaValorAtributo(Artigo, "Descricao") + " " + BSO.Base.Artigos.DaValorAtributo(this.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtraExterna");
                }
                else if (BSO.Base.Clientes.DaValorAtributo(this.DocumentoVenda.Entidade, "Idioma") != "PT" & BSO.Base.ArtigosIdiomas.DaValorAtributo(Artigo, BSO.Base.Clientes.DaValorAtributo(this.DocumentoVenda.Entidade, "Idioma"), "Descricao") + "" != "")
                {
                }
                else if (BSO.Base.Artigos.DaValorAtributo(this.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtraExterna") + "" != "")
                {
                    this.DocumentoVenda.Linhas.GetEdita(NumLinha).Descricao = BSO.Base.Artigos.DaValorAtributo(Artigo, "Descricao") + " " + BSO.Base.Artigos.DaValorAtributo(this.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtraExterna");
                    this.DocumentoVenda.Linhas.GetEdita(NumLinha).CamposUtil["CDU_ReferenciaCliente"].Valor = BSO.Base.Artigos.DaValorAtributo(this.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtraExterna");
                }
            }
        }
    }
}