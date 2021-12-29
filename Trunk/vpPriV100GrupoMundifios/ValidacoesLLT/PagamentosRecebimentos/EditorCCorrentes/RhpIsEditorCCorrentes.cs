using BasBE100;
using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.PayablesReceivables.Editors;
using System.Windows.Forms;

namespace ValidacoesLLT
{
    public class RhpIsEditorCCorrentes : EditorCCorrentes
    {
        public override void AntesDeGravar(BasBETiposGcp.TE_DocCCorrentes TDocumento, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(TDocumento, ref Cancel, e);

            if (Module1.VerificaToken("ValidacoesLLT") == 1)
            {
                if (this.DocumentoLiquidacao.Tipodoc == "LLT" & BSO.Base.Clientes.DaValorAtributo(this.DocumentoLiquidacao.Entidade, "TipoMercado") != "0")
                {
                    MessageBox.Show("A liquidação por letra só pode ser efectuada para clientes nacionais.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                }

                if (this.DocumentoLiquidacao.Tipodoc == "LLR" & BSO.Base.Clientes.DaValorAtributo(this.DocumentoLiquidacao.Entidade, "TipoMercado") == "0")
                {
                    MessageBox.Show("A liquidação por remessa só pode ser efectuada para clientes intracomunitários e outros mercados.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                }
            }
        }
    }
}