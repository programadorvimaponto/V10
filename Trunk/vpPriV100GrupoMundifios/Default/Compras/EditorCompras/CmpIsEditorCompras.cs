    using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Purchases.Editors;
using System.Windows.Forms;

namespace Default
{
    public class CmpIsEditorCompras : EditorCompras
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("Default") == 1)
            {
                if (this.DocumentoCompra.Entidade == "")
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor não está preenchido.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }

                if ((BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).TipoMercado == "1") & (this.DocumentoCompra.Tipodoc == "VFA" | this.DocumentoCompra.Tipodoc == "VNC"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor é intracomunitário, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
                else if ((BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).TipoMercado == "2") & (this.DocumentoCompra.Tipodoc == "VFA" | this.DocumentoCompra.Tipodoc == "VNC"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor é extracomunitário, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
                else if ((BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).TipoMercado == "0") & (this.DocumentoCompra.Tipodoc == "VFI" | this.DocumentoCompra.Tipodoc == "VCI"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor é nacional, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
                else if ((BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).TipoMercado == "2") & (this.DocumentoCompra.Tipodoc == "VFI" | this.DocumentoCompra.Tipodoc == "VCI"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor é extracomunitário, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
                else if ((BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).TipoMercado == "0") & (this.DocumentoCompra.Tipodoc == "VFO" | this.DocumentoCompra.Tipodoc == "VCO"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor é nacional, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
                else if ((BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).TipoMercado == "1") & (this.DocumentoCompra.Tipodoc == "VFO" | this.DocumentoCompra.Tipodoc == "VCO"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Fornecedor é intracomunitário, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }

                if (this.DocumentoCompra.UtilizaMoradaAlternativaEntrega == false & this.DocumentoCompra.LocalDescarga + "" == "")
                {
                    if (this.DocumentoCompra.Tipodoc == "NGS" | this.DocumentoCompra.Tipodoc == "NGT")
                        this.DocumentoCompra.LocalDescarga = "V/ Morada";
                    else
                        this.DocumentoCompra.LocalDescarga = "N/ Morada";
                }

                // ################################################################################################################################################################
                // # Verificar se o documento a gravar tem moeda diferente de EURO com cambio a 1                          BMP - 06/05/2021                                       #
                // ################################################################################################################################################################

                if (this.DocumentoCompra.Moeda != "EUR" & this.DocumentoCompra.Cambio == 1)
                {
                    MessageBox.Show("Atenção, não foi possivel gravar o documento, corrigir a moeda ou cambio antes de gravar! A moeda " + this.DocumentoCompra.Moeda + " tem o cambio " + this.DocumentoCompra.Cambio + "", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                }
            }
        }
    }
}