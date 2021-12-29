using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Constants.ExtensibilityService;
using Primavera.Extensibility.Sales.Editors;

namespace CustoTransportes
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(KeyCode, Shift, e);

            if (Module1.VerificaToken("CustoTransportes") == 1)
            {
                // #################################################################################################
                // # Inserir Certificados de Transação, Formulário FrmAlteraCertificadoTransacao2 (JFC 13/05/2019) #
                // #################################################################################################

                // JFC 18/12/2019 Ctrl + W - Custo Transportes
                if (KeyCode == 87 & BSO.Vendas.TabVendas.Edita(this.DocumentoVenda.Tipodoc).TipoDocumento == 3)
                {
                    ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmCustoTransportesView));

                    if (result.ResultCode == ExtensibilityResultCode.Ok)
                    {
                        FrmCustoTransportesView frm = result.Result;
                        frm.DocumentoVenda = DocumentoVenda;
                        frm.ShowDialog();
                    }
                }
            }
        }
    }
}