using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using System.Windows.Forms;

namespace ValidaGrupoFG
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void ClienteIdentificado(string Cliente, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ClienteIdentificado(Cliente, ref Cancel, e);

            if (Module1.VerificaToken("ValidaGrupoFG") == 1)
            {
                if ((this.DocumentoVenda.Tipodoc == "FA" | this.DocumentoVenda.Tipodoc == "FES" | this.DocumentoVenda.Tipodoc == "NC" | this.DocumentoVenda.Tipodoc == "NCS"))
                {
                    if (this.DocumentoVenda.Entidade == "0707" | this.DocumentoVenda.Entidade == "1207" | this.DocumentoVenda.Entidade == "0580" | this.DocumentoVenda.Entidade == "0248" | this.DocumentoVenda.Entidade == "2492")
                        MessageBox.Show("Atenção:" + Strings.Chr(13) + "Empresa do Grupo, não deve ser usado neste documento. Utilizar o documento FG, FGS ou NCG", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}