    using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Purchases.Editors;

namespace CompraFio
{
    public class CmpIsEditorCompras : EditorCompras
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("CompraFio") == 1)
            {
                if (BSO.Compras.TabCompras.Edita(this.DocumentoCompra.Tipodoc).TipoDocumento == 2)
                {
                    for (var i = 1; i <= this.DocumentoCompra.Linhas.NumItens; i++)
                    {
                        if (BSO.Inventario.ArtigosLotes.Existe(this.DocumentoCompra.Linhas.GetEdita(i).Artigo, this.DocumentoCompra.Linhas.GetEdita(i).Lote) == true & this.DocumentoCompra.Linhas.GetEdita(i).Estado == "P" & this.DocumentoCompra.Linhas.GetEdita(i).Fechado == false)
                        {
                            BSO.DSO.ExecuteSQL("UPDATE ARTIGOLOTE SET CDU_TIPOQUALIDADE = '" + this.DocumentoCompra.Linhas.GetEdita(i).CamposUtil["CDU_TIPOQUALIDADE"].Valor + "', "
                            + " CDU_Parafinado = '" + this.DocumentoCompra.Linhas.GetEdita(i).CamposUtil["CDU_Parafinado"].Valor + "' WHERE ARTIGO = '" + this.DocumentoCompra.Linhas.GetEdita(i).Artigo + "' AND LOTE = '" + this.DocumentoCompra.Linhas.GetEdita(i).Lote + "'");

                            if (BSO.Inventario.ArtigosLotes.Edita(this.DocumentoCompra.Linhas.GetEdita(i).Artigo, this.DocumentoCompra.Linhas.GetEdita(i).Lote).CamposUtil["CDU_LOTEFORN"].Valor + "" == "")
                                BSO.DSO.ExecuteSQL("UPDATE ARTIGOLOTE SET CDU_LOTEFORN = '" + this.DocumentoCompra.Linhas.GetEdita(i).CamposUtil["CDU_LOTEFORN"].Valor + "' "
    + "WHERE ARTIGO = '" + this.DocumentoCompra.Linhas.GetEdita(i).Artigo + "' AND LOTE = '" + this.DocumentoCompra.Linhas.GetEdita(i).Lote + "'");
                        }
                    }
                }

                if (BSO.Compras.TabCompras.Edita(this.DocumentoCompra.Tipodoc).TipoDocumento == 4)
                {
                    for (var i = 1; i <= this.DocumentoCompra.Linhas.NumItens; i++)
                    {
                        if (BSO.Inventario.ArtigosLotes.Existe(this.DocumentoCompra.Linhas.GetEdita(i).Artigo, this.DocumentoCompra.Linhas.GetEdita(i).Lote) == true)
                        {
                            if (BSO.Inventario.ArtigosLotes.Edita(this.DocumentoCompra.Linhas.GetEdita(i).Artigo, this.DocumentoCompra.Linhas.GetEdita(i).Lote).CamposUtil["CDU_LOTEFORN"].Valor + "" == "")
                                BSO.DSO.ExecuteSQL("UPDATE ARTIGOLOTE SET CDU_LOTEFORN = '" + this.DocumentoCompra.Linhas.GetEdita(i).CamposUtil["CDU_LOTEFORN"].Valor + "' "
                                + "WHERE ARTIGO = '" + this.DocumentoCompra.Linhas.GetEdita(i).Artigo + "' AND LOTE = '" + this.DocumentoCompra.Linhas.GetEdita(i).Lote + "'");
                        }
                    }
                }
            }
        }

        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            if (Module1.VerificaToken("CompraFio") == 1)
            {
                if (BSO.Compras.TabCompras.Edita(this.DocumentoCompra.Tipodoc).TipoDocumento == 2)
                {
                    for (var i = 1; i <= this.DocumentoCompra.Linhas.NumItens; i++)
                    {
                        if (this.DocumentoCompra.Linhas.GetEdita(i).Artigo + "" != "" & this.DocumentoCompra.Linhas.GetEdita(i).Lote != "")
                            BSO.Inventario.ArtigosLotes.ActualizaValorAtributo(this.DocumentoCompra.Linhas.GetEdita(i).Artigo, this.DocumentoCompra.Linhas.GetEdita(i).Lote, "CDU_Fornecedor", this.DocumentoCompra.Entidade);
                    }
                }
            }
        }
    }
}