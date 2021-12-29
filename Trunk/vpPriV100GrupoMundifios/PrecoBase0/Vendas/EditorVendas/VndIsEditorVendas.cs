using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using System.Windows.Forms;

namespace PrecoBase
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("PrecoBase0") == 1)
            {
                // ##############################################################################################
                // ##      Não deixa gravar nenhuma fatura caso o Preço Base seja 0   (BRUNO - 25/09/2020)     ##
                // ##############################################################################################
                // Editado para bloquear a em qualquer tipo de documento por forma a garantir que o email diário de margens vai com o PrBase preenchido. - JFC 28/07/2021
                // If BSO.Comercial.TabVendas.Edita(Me.DocumentoVenda.tipoDoc).TipoDocumento = 4 Then

                for (var i = 1; i <= this.DocumentoVenda.Linhas.NumItens; i++)
                {
                    if (this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "" & this.DocumentoVenda.Linhas.GetEdita(i).Lote + "" != "")
                    {
                        if (this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_PrecoBase"].Valor == "0")
                        {
                            Cancel = true;
                            MessageBox.Show("Il campo CDU_PrecoBase non è compilato. Il documento non verrà salvato! Ctrl + U sulla linea e riempire il campo." + Strings.Chr(13) + Strings.Chr(13) + "Linha: " + i + " - " + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + " - " + this.DocumentoVenda.Linhas.GetEdita(i).Lote, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }
    }
}