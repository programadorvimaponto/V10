using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using System;
using System.Windows.Forms;

namespace ValidaPrBase
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("ValidaPrBase") == 1)
            {
                // JFC 10/12/2020 Só valida caso o cliente não seja PT. Isto porque algumas encomendas entre empresas com local descarga em PT teriam preço base superior ao preço unitário.
                if (this.DocumentoVenda.Pais != "PT")
                {
                    // JFC 04/11/2019 - Não gravar documento caso o preço base seja superior ao preço unitário. Pedido de Mafalda.
                    for (var i = 1; i <= this.DocumentoVenda.Linhas.NumItens; i++)
                    {
                        if (this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "")
                        {
                            if (this.DocumentoVenda.Linhas.GetEdita(i).PrecUnit < Convert.ToDouble((this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_PrecoBase"].Valor)))
                            {
                                Cancel = true;
                                MessageBox.Show("CDU_PrecoBase superior ao Pr. Unit. O documento não será gravado!" + Strings.Chr(13) + Strings.Chr(13) + "Linha: " + i + " - " + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + " - " + this.DocumentoVenda.Linhas.GetEdita(i).Lote, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }
    }
}