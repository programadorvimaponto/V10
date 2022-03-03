using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.Attributes;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Purchases.Editors;
using StdBE100;

namespace CopiarLotes
{
    public class CmpIsEditorCompras : EditorCompras
    {
        //último a correr porque mexe com Artigos Lotes. Primeiro a correr será no ArmazemEntreposto. O segundo será no comprafio.
        [Order(100)]
        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            if (Module1.VerificaToken("CopiarLotes") == 1)
            {
                if (DocumentoCompra.Entidade + "" != "" && BSO.Compras.TabCompras.Edita(DocumentoCompra.Tipodoc).TipoDocumento == 2)
                {
                    if (Module1.AbreEmpresa("MUNDIFIOS"))
                    {
                        for (int i = 1; i <= DocumentoCompra.Linhas.NumItens; i++)
                        {
                            if (DocumentoCompra.Linhas.GetEdita(i).Artigo + "" != "" && DocumentoCompra.Linhas.GetEdita(i).Lote != "" && DocumentoCompra.Linhas.GetEdita(i).Lote != "<L01>")
                            {
                                if (BSO.Base.Artigos.Existe(DocumentoCompra.Linhas.GetEdita(i).Artigo) == true && (BSO.Base.Artigos.Edita(DocumentoCompra.Linhas.GetEdita(i).Artigo).Descricao.StartsWith("Fio") || BSO.Base.Artigos.Edita(DocumentoCompra.Linhas.GetEdita(i).Artigo).Descricao.StartsWith("Rama")))
                                {
                                    CopiaLote(this.DocumentoCompra.Linhas.GetEdita(i).Artigo, this.DocumentoCompra.Linhas.GetEdita(i).Lote);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void CopiaLote(string str_Artigo, string str_Lote)
        {
            if (str_Lote == "")
                return;

            if (BSO.Inventario.ArtigosLotes.Existe(str_Artigo, str_Lote) == false)
            {
                InvBE100.InvBEArtigoLote ArtigoLote = new InvBE100.InvBEArtigoLote();

                StdBELista stdBE_ListaLote;
                stdBE_ListaLote = BSO.Consulta(" SELECT * FROM ArtigoLote " + " WHERE Artigo = '" + str_Artigo + "' " + " AND Lote = '" + str_Lote + "'");

                if (!stdBE_ListaLote.Vazia())
                {
                    stdBE_ListaLote.Inicio();

                    ArtigoLote.Artigo = stdBE_ListaLote.Valor("Artigo");
                    ArtigoLote.Lote = stdBE_ListaLote.Valor("Lote");
                    ArtigoLote.Descricao = BSO.Contexto.CodEmp + ", " + BSO.Contexto.UtilizadorActual;
                    if (Strings.Len(stdBE_ListaLote.Valor("DataFabrico")) > 0)
                        ArtigoLote.DataFabrico = stdBE_ListaLote.Valor("DataFabrico");
                    if (Strings.Len(stdBE_ListaLote.Valor("Validade")) > 0)
                        ArtigoLote.Validade = stdBE_ListaLote.Valor("Validade");

                    if (stdBE_ListaLote.Valor("Controlador") is string controlador) ArtigoLote.Controlador = controlador; else ArtigoLote.Controlador = string.Empty;

                    ArtigoLote.Activo = stdBE_ListaLote.Valor("Activo");

                    if (stdBE_ListaLote.Valor("Observacoes") is string obs) ArtigoLote.Observacoes = obs; else ArtigoLote.Observacoes = string.Empty;

                    if (stdBE_ListaLote.Valor("CDU_TipoQualidade") is string tipoqualidade) ArtigoLote.CamposUtil["CDU_TipoQualidade"].Valor = tipoqualidade; else ArtigoLote.CamposUtil["CDU_TipoQualidade"].Valor = string.Empty;

                    ArtigoLote.CamposUtil["CDU_Parafinado"].Valor = stdBE_ListaLote.Valor("CDU_Parafinado");

                    if (stdBE_ListaLote.Valor("CDU_LoteForn") is string loteforn) ArtigoLote.CamposUtil["CDU_LoteForn"].Valor = loteforn; else ArtigoLote.CamposUtil["CDU_LoteForn"].Valor = string.Empty;

                    if (stdBE_ListaLote.Valor("CDU_Fornecedor") is string fornecedor) ArtigoLote.CamposUtil["CDU_Fornecedor"].Valor = fornecedor; else ArtigoLote.CamposUtil["CDU_Fornecedor"].Valor = string.Empty;
                    BSO.Inventario.ArtigosLotes.Actualiza(ArtigoLote);
                }
            }
        }
    }
}