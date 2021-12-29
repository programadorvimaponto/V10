using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using static BasBE100.BasBETiposGcp;

namespace InstrucaoAcabamento
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);

            if (Module1.VerificaToken("InstrucaoAcabamento") == 1)
            {
                if (DocumentoVenda.Tipodoc == "ECL" || DocumentoVenda.Tipodoc == "GR")
                {
                    DocumentoVenda.Linhas.GetEdita(NumLinha).CamposUtil["CDU_DataEntregaCliente"].Valor = DocumentoVenda.DataDoc;

                    if (DocumentoVenda.Linhas.GetEdita(NumLinha).Descricao.Contains("Seacell"))
                    {
                        if (DocumentoVenda.Pais == "PT")
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "Artigo composto por Seacell por favor tenha em atenção as instruções de acabamento. Por favor solicite as instruções de acabamento caso não tenha.");
                        else
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "This Product is composed by Seacell fiber please follow the finishing instructions. Please ask for the finishing instructions if you don't have them.");
                    }

                    if (DocumentoVenda.Linhas.GetEdita(NumLinha).Descricao.Contains("Sensitive"))
                    {
                        if (DocumentoVenda.Pais == "PT")
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "Artigo composto por SmartCel Sensitive por favor tenha em atenção as instruções de acabamento. Por favor solicite as instruções de acabamento caso não tenha.");
                        else
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "This Product is composed by SmartCel Sensitive fiber please follow the finishing instructions. Please ask for the finishing instructions if you don't have them.");
                    }

                    if (DocumentoVenda.Linhas.GetEdita(NumLinha).Descricao.Contains("Protection"))
                    {
                        if (DocumentoVenda.Pais == "PT")
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "Artigo composto por CellSolution Protection por favor tenha em atenção as instruções de acabamento. Por favor solicite as instruções de acabamento caso não tenha.");
                        else
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "This Product is composed by CellSolution Protection fiber please follow the finishing instructions. Please ask for the finishing instructions if you don't have them.");
                    }

                    if (DocumentoVenda.Linhas.GetEdita(NumLinha).Descricao.Contains("Clima"))
                    {
                        if (DocumentoVenda.Pais == "PT")
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "Artigo composto por CellSolution Clima por favor tenha em atenção as instruções de acabamento. Por favor solicite as instruções de acabamento caso não tenha.");
                        else
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "This Product is composed by CellSolution Clima fiber please follow the finishing instructions. Please ask for the finishing instructions if you don't have them.");
                    }

                    if (DocumentoVenda.Linhas.GetEdita(NumLinha).Descricao.Contains("Skin Care"))
                    {
                        if (DocumentoVenda.Pais == "PT")
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "Artigo composto por CellSolution Skin Care por favor tenha em atenção as instruções de acabamento. Por favor solicite as instruções de acabamento caso não tenha.");
                        else
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "This Product is composed by CellSolution Skin Care fiber please follow the finishing instructions. Please ask for the finishing instructions if you don't have them.");
                    }
                }
            }
        }
    }
}