using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using System.Windows.Forms;

namespace ComparaLinhasComissoes
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("ComparaLinhasComissoes") == 1)
            {
                // ##########################################################################################
                // ##Valida se alguma das linhas tem comissão diferente  - Pedido D. Goreti -25/03/2019 JFC##
                // ##########################################################################################
                if (this.DocumentoVenda.Tipodoc == "ECL")
                {
                    double comissaoAux;
                    bool comissaoBolean;
                    string comissaoStr;
                    comissaoAux = 2365479;
                    comissaoBolean = false;
                    for (var j = 1; j <= this.DocumentoVenda.Linhas.NumItens; j++)
                    {
                        if (this.DocumentoVenda.Linhas.GetEdita(j).Artigo + "" != "")
                        {
                            if (comissaoAux == 2365479)
                                comissaoAux = this.DocumentoVenda.Linhas.GetEdita(j).Comissao;
                            else if (comissaoAux != this.DocumentoVenda.Linhas.GetEdita(j).Comissao)
                                comissaoBolean = true;
                        }
                    }

                    if (comissaoBolean == true)
                    {
                        comissaoStr = "Atenção existem comissões diferentes nas diversas linhas" + Strings.Chr(13) + Strings.Chr(13) + "Linha - Artigo - Lote - Comissão" + Strings.Chr(13);
                        for (var j = 1; j <= this.DocumentoVenda.Linhas.NumItens; j++)
                        {
                            if (this.DocumentoVenda.Linhas.GetEdita(j).Artigo + "" != "")
                                comissaoStr = comissaoStr + j + " - " + this.DocumentoVenda.Linhas.GetEdita(j).Artigo + " - " + this.DocumentoVenda.Linhas.GetEdita(j).Lote + " - " + this.DocumentoVenda.Linhas.GetEdita(j).Comissao + Strings.Chr(13);
                        }
                        if (MessageBox.Show(comissaoStr + Strings.Chr(13) + "Deseja continuar com a gravação?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                            Cancel = true;
                    }
                }
            }
        }
    }
}