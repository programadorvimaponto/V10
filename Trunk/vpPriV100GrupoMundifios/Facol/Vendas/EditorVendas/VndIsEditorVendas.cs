using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Constants.ExtensibilityService;
using Primavera.Extensibility.Sales.Editors;

namespace Facol
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("Facol") == 1)
            {
                if (this.DocumentoVenda.Tipodoc == "NEF")
                {
                    if (this.DocumentoVenda.Estado == "G")
                        this.DocumentoVenda.Estado = "P";
                }
            }
        }

        private bool VarNetTrans;

        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            if (Module1.VerificaToken("Facol") == 1)
            {
                // Se o documento for NET fecha o documento. Isto porque é um documento do tipo Encomenda e aparece no Mapa de Bordo. JFC
                if (this.DocumentoVenda.Tipodoc == "NET" & VarNetTrans == true)
                {
                    string StringFechaDoc;

                    BSO.DSO.ExecuteSQL("UPDATE cds set cds.fechado='1' from CabecDocStatus cds inner join CabecDoc cd on cd.Id=cds.IdCabecDoc where cd.TipoDoc='NET' and cd.NumDoc='" + this.DocumentoVenda.NumDoc + "' and cd.Serie='" + this.DocumentoVenda.Serie + "'");
                    VarNetTrans = false;
                }
            }
        }

        public override void DepoisDeTransformar(ExtensibilityEventArgs e)
        {
            base.DepoisDeTransformar(e);

            if (Module1.VerificaToken("Facol") == 1)
            {
                long i;
                if (this.DocumentoVenda.Tipodoc == "NET")
                    VarNetTrans = true;
            }
        }

        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(KeyCode, Shift, e);

            if (Module1.VerificaToken("Facol") == 1)
            {
                // #############################################################################
                // # NET pagas pela Facol, Formulário FrmFacolPago (JFC 19/10/2018)            #
                // #############################################################################
                // Crtl+D- Comissao Facol Pago

                if (KeyCode == 68 & this.DocumentoVenda.Tipodoc == "NET")
                {
                    Module1.dsptipoDoc = this.DocumentoVenda.Tipodoc;
                    Module1.dspSerie = this.DocumentoVenda.Serie;
                    Module1.dspNumDoc = this.DocumentoVenda.NumDoc.ToString();

                    ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmFacolPagoView));

                    if (result.ResultCode == ExtensibilityResultCode.Ok)
                    {
                        FrmFacolPagoView frm = result.Result;
                        frm.ShowDialog();
                    }
                }
            }
        }
    }
}