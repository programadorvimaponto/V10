using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using System.Windows.Forms;

namespace QtdMinCliente
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void ClienteIdentificado(string Cliente, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ClienteIdentificado(Cliente, ref Cancel, e);

            if (Module1.VerificaToken("QtdMinCliente") == 1)
            {
                // *******************************************************************************************************************************************
                // #### QUANTIDADES MINIMAS PARA CLIENTES - Pedido de Joaquim António 30/01/2017 (JFC) ####
                // *******************************************************************************************************************************************
                StdBELista lista;
                string ent;
                if ((this.DocumentoVenda.Tipodoc == "ECL" | this.DocumentoVenda.Tipodoc == "GC"))
                {
                    ent = this.DocumentoVenda.Entidade;
                    lista = BSO.Consulta("SELECT Entidade FROM TDU_QntMinimas Where Entidade=" + "'" + ent + "'");

                    if ((lista.Vazia() == false))
                        MessageBox.Show("Atenção:" + Strings.Chr(13) + "Cliente com quantidade minima de 1 Palete ou Caixa", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}