using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Purchases.Editors;
using StdBE100;
using System;

namespace SugereLotes
{
    public class CmpIsEditorCompras : EditorCompras
    {
        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
            {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);

            if (Module1.VerificaToken("SugereLotes") == 1)
            {
                if (DocumentoCompra.Entidade + "" != "" && BSO.Compras.TabCompras.Edita(DocumentoCompra.Tipodoc).TipoDocumento == 2 && (BSO.Base.Artigos.Edita(Artigo).Descricao.StartsWith("Fio") || BSO.Base.Artigos.Edita(Artigo).Descricao.StartsWith("Rama")))
                {
                    // Sugestão de lote
                    int i;
                    string ent;
                    int lote;
                    int loteAux;
                    StdBELista listLote;

                    ent = this.DocumentoCompra.Entidade;
                    loteAux = 0;
                    // Consulta à função dbo.fnProximoLote de qual o proximo lote a utilizar.
                    listLote = BSO.Consulta("select PRIMUNDIFIOS.dbo.fnProximoLote('" + BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).CamposUtil["CDU_EntidadeInterna"].Valor + "','" + Artigo + "') as 'Lote'");

                    listLote.Inicio();
                    for (i = 1; i <= DocumentoCompra.Linhas.NumItens; i++)
                    {
                        if (DocumentoCompra.Linhas.GetEdita(i).Artigo == Artigo && i != NumLinha && DocumentoCompra.Linhas.GetEdita(i).Lote.Length == 8)
                        {
                            lote = Convert.ToInt32(Strings.Right(this.DocumentoCompra.Linhas.GetEdita(i).Lote, 4));

                            if (lote > loteAux)
                                loteAux = lote;
                        }
                    }

                    if (loteAux != 0)
                    {
                        loteAux++;

                        if (listLote.Vazia() || listLote.Valor("Lote") == string.Empty)
                        {
                            i = 4 - Strings.Len(Convert.ToString(loteAux));
                            this.DocumentoCompra.Linhas.GetEdita(NumLinha).Lote = "" + ent + Strings.Left("0000", i) + loteAux;
                        }
                        else
                        {
                            if (loteAux <= Convert.ToInt32(Strings.Right(listLote.Valor("Lote"), 4)))
                                this.DocumentoCompra.Linhas.GetEdita(NumLinha).Lote = listLote.Valor("Lote");
                            else
                            {
                                i = 4 - Strings.Len(Convert.ToString(loteAux));
                                this.DocumentoCompra.Linhas.GetEdita(NumLinha).Lote = "" + ent + Strings.Left("0000", i) + loteAux;
                            }
                        }
                    }
                    else
                    {
                        if (listLote.Vazia() || listLote.Valor("Lote") == string.Empty)
                            DocumentoCompra.Linhas.GetEdita(NumLinha).Lote = "" + ent + "0001";
                        else
                            DocumentoCompra.Linhas.GetEdita(NumLinha).Lote = listLote.Valor("Lote");
                    }
                }
            }
        }
    }
}