using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.Attributes;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Purchases.Editors;

namespace ArmazemEntreposto
{
    public class CmpIsEditorCompras : EditorCompras
    {
        //Primeiro a correr porque mexe com Artigos Lotes. Segundo a correr será no Comprafio. No copiar Lotes irá copiar os lotes para a mundifios com todos os dados.
        [Order(0)]
        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            if (Module1.VerificaToken("ArmazemEntreposto") == 1)
            {
                if (BSO.Compras.TabCompras.Edita(this.DocumentoCompra.Tipodoc).TipoDocumento == 4 & BSO.Compras.TabCompras.Edita(this.DocumentoCompra.Tipodoc).PagarReceber == "P")
                {
                    for (var i = 1; i <= this.DocumentoCompra.Linhas.NumItens; i++)
                    {
                        if (this.DocumentoCompra.Linhas.GetEdita(i).Artigo + "" != "" & this.DocumentoCompra.Linhas.GetEdita(i).Armazem == "AEP")
                        {
                            BSO.Inventario.ArtigosLotes.ActualizaValorAtributo(this.DocumentoCompra.Linhas.GetEdita(i).Artigo, this.DocumentoCompra.Linhas.GetEdita(i).Lote, "CDU_DespDAU", this.DocumentoCompra.Linhas.GetEdita(i).CamposUtil["CDU_DespDAU"].Valor);
                            BSO.Inventario.ArtigosLotes.ActualizaValorAtributo(this.DocumentoCompra.Linhas.GetEdita(i).Artigo, this.DocumentoCompra.Linhas.GetEdita(i).Lote, "CDU_Regime", this.DocumentoCompra.Linhas.GetEdita(i).CamposUtil["CDU_Regime"].Valor);
                            BSO.Inventario.ArtigosLotes.ActualizaValorAtributo(this.DocumentoCompra.Linhas.GetEdita(i).Artigo, this.DocumentoCompra.Linhas.GetEdita(i).Lote, "CDU_CodMerc", this.DocumentoCompra.Linhas.GetEdita(i).CamposUtil["CDU_CODMERC"].Valor);
                            BSO.Inventario.ArtigosLotes.ActualizaValorAtributo(this.DocumentoCompra.Linhas.GetEdita(i).Artigo, this.DocumentoCompra.Linhas.GetEdita(i).Lote, "CDU_Contramarca", this.DocumentoCompra.Linhas.GetEdita(i).CamposUtil["CDU_Contramarca"].Valor);
                            BSO.Inventario.ArtigosLotes.ActualizaValorAtributo(this.DocumentoCompra.Linhas.GetEdita(i).Artigo, this.DocumentoCompra.Linhas.GetEdita(i).Lote, "CDU_ContramarcaData", Strings.Format(this.DocumentoCompra.Linhas.GetEdita(i).CamposUtil["CDU_ContramarcaData"].Valor, "yyyy-MM-dd"));
                        }
                    }
                }
            }
        }
    }
}