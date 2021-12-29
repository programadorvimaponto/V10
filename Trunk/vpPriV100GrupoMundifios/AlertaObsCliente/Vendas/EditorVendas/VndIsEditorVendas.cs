using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using System.Windows.Forms;

namespace AlertaObsCliente
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void ClienteIdentificado(string Cliente, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ClienteIdentificado(Cliente, ref Cancel, e);

            if (Module1.VerificaToken("AlertaObsCliente") == 1)
            {
                if (this.DocumentoVenda.Tipodoc == "ECL" | this.DocumentoVenda.Tipodoc == "GC")
                {
                    if (BSO.Base.Clientes.Edita(Cliente).CamposUtil["CDU_ObsEncomenda"].Valor + "" != "")
                        MessageBox.Show(BSO.Base.Clientes.Edita(Cliente).CamposUtil["CDU_ObsEncomenda"].Valor.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}