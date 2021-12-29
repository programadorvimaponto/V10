using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Purchases.Editors;
using System.Windows.Forms;

namespace IntegracaoFilopaDestino
{
    public class CmpIsEditorCompras : EditorCompras
    {
        public override void AntesDeAnular(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeAnular(ref Cancel, e);

            if (Module1.VerificaToken("IntegracaoFilopaDestino") == 1)
            {
                // --------------------------------------------------------------
                // --- VIMAPONTO - Gualter Costa - 2019-06-13 - RedMine #1558 ---
                // --------------------------------------------------------------
                //
                // Se o documento de compra do tipo "CNT" ou "ECF" foi gerado automaticamente a partir de um documento de venda da Filopa
                // Informa o utilizador que n�o � poss�vel anular o documento de compra. A anula��o dever� ser efectuada a partir do documento de venda Filopa que lhe deu origem
                //

                if ((this.DocumentoCompra.Tipodoc.Trim() == "CNT" | this.DocumentoCompra.Tipodoc.Trim() == "ECF") & this.DocumentoCompra.CamposUtil["CDU_DocumentoOrigem"].Valor.ToString().Length > 1 & this.DocumentoCompra.CamposUtil["CDU_BaseDadosOrigem"].Valor.ToString().Length > 1)
                {
                    MessageBox.Show("N�O � POSS�VEL ANULAR O DOCUMENTO ATUAL!" + Strings.Chr(13) + Strings.Chr(13) + "O Primavera detectou que o documento de compra atual (" + this.DocumentoCompra.Tipodoc.Trim() + " " + this.DocumentoCompra.Serie.Trim() + "/" + this.DocumentoCompra.NumDoc.ToString().Trim() + ")" + Strings.Chr(13) + "foi gerado autom�ticamente a partir do documento de venda (" + this.DocumentoCompra.CamposUtil["CDU_DocumentoOrigem"].Valor.ToString().Trim() + ") de origem FILOPA" + Strings.Chr(13) + Strings.Chr(13) + "Assim, n�o � poss�vel anular este documento de compra nesta base de dados." + Strings.Chr(13) + Strings.Chr(13) + "A anula��o dever� ser feita a partir do documento de venda que lhe deu origem (" + this.DocumentoCompra.CamposUtil["CDU_DocumentoOrigem"].Valor.ToString().Trim() + ") na base de dados da FILOPA.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Cancel = true; // Cancela a anula��o do documento
                }
            }
        }

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("IntegracaoFilopaDestino") == 1)
            {
                // VIMAPONTO - GMC 2019.05.31 (ver especifica��o no redmine #1558)
                // Verifica se o documento de compra foi gerado a partir da integra��o autom�tica de um documento de venda da Filopa.
                // (Se tiver preenchido o campo CDU_DocumentoOrigem n�o deixa gravar!)

                string Aux_DocumentoOrigem;
                string Aux_BaseDadosOrigem;

                // Verifica se o documento teve origem no mecanismo numa c�pia de documentos FILOPA --> Outras Empresas do Grupo Mundifios
                Aux_DocumentoOrigem = BSO.Compras.Documentos.DaValorAtributo(this.DocumentoCompra.Tipodoc, this.DocumentoCompra.NumDoc, this.DocumentoCompra.Serie, this.DocumentoCompra.Filial, "CDU_DocumentoOrigem");
                Aux_BaseDadosOrigem = BSO.Compras.Documentos.DaValorAtributo(this.DocumentoCompra.Tipodoc, this.DocumentoCompra.NumDoc, this.DocumentoCompra.Serie, this.DocumentoCompra.Filial, "CDU_BaseDadosOrigem");

                // Se sim (se tiver o campo CDU_DocumentoOrigem preenchido)
                if (Strings.Len(Strings.Trim(Aux_DocumentoOrigem)) > 0 & Strings.Len(Strings.Trim(Aux_BaseDadosOrigem)) > 0)
                {
                    // Comentado por JFC 30/10/2019
                    // Cancel = True 'Cancela a grava��o
                    // MsgBox "O documento de compra atual foi gerado a partir da integra��o autom�tica do documento FILOPA " & Aux_DocumentoOrigem & "." & Chr(13) & Chr(13) & "As altera��es dever�o ser efectuadas no documento de origem." & Chr(13) & Chr(13) & "As altera��es no documento atual n�o ser�o gravadas!", vbCritical
                    // Exit Sub
                    if (Interaction.MsgBox("O documento de compra atual foi gerado a partir da integra��o autom�tica do documento FILOPA " + Aux_DocumentoOrigem + "." + Strings.Chr(13) + Strings.Chr(13) + "As altera��es dever�o ser efectuadas no documento de origem. Caso contr�io poder� gerar erros." + Strings.Chr(13) + Strings.Chr(13) + "Tem a certeza que deseja continuar com a grava��o?", Constants.vbYesNo) == Constants.vbNo)
                        Cancel = true;
                }
            }
        }

        public override void DepoisDeDuplicar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeDuplicar(Filial, Tipo, Serie, NumDoc, e);

            if (Module1.VerificaToken("IntegracaoFilopaDestino") == 1)
            {
                // --------------------------------------------------------------
                // --- VIMAPONTO - Gualter Costa - 2019-06-14 - RedMine #1558 ---
                // --------------------------------------------------------------

                // Verifica se no novo documento que acabou ser criado por duplica��o, tem preenchido o campo CDU_DocumentoOrigem.
                // Se sim limpa-o no novo documento.

                if (this.DocumentoCompra.CamposUtil["CDU_DocumentoOrigem"].Valor.ToString().Trim() != "")
                    this.DocumentoCompra.CamposUtil["CDU_DocumentoOrigem"].Valor = "";
            }
        }
    }
}