using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Purchases.Editors;
using StdBE100;
using System.Windows.Forms;

namespace SerieC
{
    public class CmpIsEditorCompras : EditorCompras
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("SerieC") == 1)
            {
                // JFC - Antes de gravar garantir que um documento da serie C é transformado para outro documento da serie C - Pedido de Mafalda 15/10/2018
                if (this.DocumentoCompra.Tipodoc == "VGR" | this.DocumentoCompra.Tipodoc == "VFA")
                {
                    int j;
                    StdBELista SerieC;

                    if (Strings.Right(this.DocumentoCompra.Serie, 1) != "C")
                    {
                        for (j = 1; j <= this.DocumentoCompra.Linhas.NumItens; j++)
                        {
                            if (this.DocumentoCompra.Linhas.GetEdita(j).IDLinhaOriginal + "" != "" & this.DocumentoCompra.Linhas.GetEdita(j).Artigo + "" != "")
                            {
                                SerieC = BSO.Consulta("select top 1 right(cd.serie,1) as Serie from cabeccompras cd inner join linhascompras ln on ln.idcabeccompras=cd.id where ln.id='" + this.DocumentoCompra.Linhas.GetEdita(j).IDLinhaOriginal + "'");
                                SerieC.Inicio();

                                if (SerieC.Valor("Serie") == "C")
                                {
                                    MessageBox.Show("Atenção está a transformar um documento da Serie C para outra Serie: " + this.DocumentoCompra.Linhas.GetEdita(j).Artigo + " - " + this.DocumentoCompra.Linhas.GetEdita(j).Lote, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Cancel = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}