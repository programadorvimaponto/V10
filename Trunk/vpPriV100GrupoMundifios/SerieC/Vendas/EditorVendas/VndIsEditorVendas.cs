using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using System.Windows.Forms;

namespace SerieC
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("SerieC") == 1)
            {
                // JFC - Antes de gravar garantir que um documento da serie C é transformado para outro documento da serie C - Pedido de Mafalda 15/10/2018
                if (this.DocumentoVenda.Tipodoc == "GR" | this.DocumentoVenda.Tipodoc == "FA")
                {
                    int j;
                    StdBELista SerieC;

                    if (Strings.Right(this.DocumentoVenda.Serie, 1) != "C")
                    {
                        for (j = 1; j <= this.DocumentoVenda.Linhas.NumItens; j++)
                        {
                            if (this.DocumentoVenda.Linhas.GetEdita(j).IDLinhaOriginal + "" != "" & this.DocumentoVenda.Linhas.GetEdita(j).Artigo + "" != "")
                            {
                                SerieC = BSO.Consulta("select top 1 right(cd.serie,1) as Serie from cabecdoc cd inner join linhasdoc ln on ln.idcabecdoc=cd.id where ln.id='" + this.DocumentoVenda.Linhas.GetEdita(j).IDLinhaOriginal + "'");
                                SerieC.Inicio();

                                if (SerieC.Valor("Serie") == "C")
                                {
                                    MessageBox.Show("Atenção está a transformar um documento da Serie C para outra Serie: " + this.DocumentoVenda.Linhas.GetEdita(j).Artigo + " - " + this.DocumentoVenda.Linhas.GetEdita(j).Lote, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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