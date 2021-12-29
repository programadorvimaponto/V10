using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Purchases.Editors;
using System;

namespace MyTools
{
    public class CmpIsEditorCompras : EditorCompras
    {
        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            if (Module1.VerificaToken("MyTools") == 1)
            {
                // ####################################################################################################################################
                // #Atualizar o CDU_DataUltimaAtualizacao apos gravaçao de uma ECFBRUNO - 04/03/2020                                                  #
                // ####################################################################################################################################
                if (this.DocumentoCompra.Tipodoc == "ECF")
                {
                    for (var i = 1; i <= this.DocumentoCompra.Linhas.NumItens; i++)
                    {
                        if (this.DocumentoCompra.Linhas.GetEdita(i).Artigo + "" != "")
                            this.DocumentoCompra.Linhas.GetEdita(i).CamposUtil["CDU_DataUltimaAtualizacao"].Valor = Strings.Format(DateTime.Now, "yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
        }
    }
}