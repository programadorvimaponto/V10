using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Constants.ExtensibilityService;
using Primavera.Extensibility.Sales.Editors;
using Vimaponto.PrimaveraV100;

namespace ArmazemEntreposto
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(KeyCode, Shift, e);

            if (Module1.VerificaToken("ArmazemEntreposto") == 1)
            {
                if (this.LinhaActual > 0)
                {
                    if (KeyCode == 68 & this.DocumentoVenda.Tipodoc == "GR" & this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Armazem == "AEP")
                    {
                        Module1.aepArtigo = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Artigo;
                        Module1.aepDocumento = this.DocumentoVenda.Tipodoc + " " + this.DocumentoVenda.NumDoc + "/" + this.DocumentoVenda.Serie;
                        Module1.aepLote = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Lote;
                        Module1.aepArmazem = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Armazem;
                        Module1.aepDespDAU = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_DespDAU"].Valor.ToString();
                        Module1.aepRegime = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_Regime"].Valor.ToString();
                        Module1.aepIDlinha = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).IdLinha;

                        ExtensibilityResult result = PriV100Api.BSO.Extensibility.CreateCustomFormInstance(typeof(FrmAlteraGuiaAEPView));

                        if (result.ResultCode == ExtensibilityResultCode.Ok)
                        {
                            FrmAlteraGuiaAEPView frm = result.Result;
                            frm.DocumentoVenda = DocumentoVenda;
                            frm.LinhaActual = LinhaActual;
                            frm.ShowDialog();
                        }
                    }
                }
            }
        }
    }
}