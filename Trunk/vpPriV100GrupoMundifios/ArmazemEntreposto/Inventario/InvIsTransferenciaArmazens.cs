using Primavera.Extensibility.Inventory.Editors;
using Primavera.Extensibility.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using Generico;

namespace ArmazemEntreposto
{
    public class InvIsTransferenciaArmazens : EditorTransferenciasStock
    {


        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("ArmazemEntreposto") == 1)
            {
                for (var i = 1; i <= DocumentoTransferencia.LinhasOrigem.NumItens; i++)
                {
                    if (this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Artigo + "" != "" & this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Armazem == "AEP" & this.DocumentoTransferencia.Tipodoc != "ENCG")
                    {
                        if (this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).CamposUtil["CDU_DespTipoImportacao"].Valor + "" == "")
                        {
                            MessageBox.Show("Aten��o:" + Strings.Chr(13) + "O Tipo de Importa��o na linha " + i + " para o artigo '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Artigo + "' e lote '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Lote + "' n�o est� preenchido.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Cancel = true;
                            return;
                        }
                        if (this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).CamposUtil["CDU_DespDAU"].Valor + "" == "")
                        {
                            MessageBox.Show("Aten��o:" + Strings.Chr(13) + "O C�digo DAU na linha " + i + " para o artigo '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Artigo + "' e lote '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Lote + "' n�o est� preenchido.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            Cancel = true;
                            return;
                        }
                        if (this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).CamposUtil["CDU_Volumes"].Valor + "" == "")
                        {
                            MessageBox.Show("Aten��o:" + Strings.Chr(13) + "Os Volumes na linha " + i + " para o artigo '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Artigo + "' e lote '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Lote + "' n�o est�o preenchidos.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Cancel = true;
                            return;
                        }
                        if (this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).CamposUtil["CDU_CODMERC"].Valor + "" == "")
                        {
                            MessageBox.Show("Aten��o:" + Strings.Chr(13) + "O C�digo da Mercadoria na linha " + i + " para o artigo '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Artigo + "' e lote '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Lote + "' n�o est� preenchido.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Cancel = true;
                            return;
                        }
                        if (this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).CamposUtil["CDU_Regime"].Valor + "" == "")
                        {
                            MessageBox.Show("Aten��o:" + Strings.Chr(13) + "O C�digo do Regime na linha " + i + " para o artigo '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Artigo + "' e lote '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Lote + "' n�o est� preenchido.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Cancel = true;
                            return;
                        }
                        if ((int)this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).CamposUtil["CDU_MassaBruta"].Valor == 0)
                        {
                            MessageBox.Show("Aten��o:" + Strings.Chr(13) + "A Massa Bruta na linha " + i + " para o artigo '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Artigo + "' e lote '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Lote + "' n�o est� preenchida.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            Cancel = true;
                            return;
                        }
                        if ((int)DocumentoTransferencia.LinhasOrigem.GetEdita(i).CamposUtil["CDU_MassaLiq"].Valor == 0)
                        {
                            MessageBox.Show("Aten��o:" + Strings.Chr(13) + "A Massa L�quida na linha " + i + " para o artigo '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Artigo + "' e lote '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Lote + "' n�o est� preenchida.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Cancel = true;
                            return;
                        }
                        if (this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).CamposUtil["CDU_Contramarca"].Valor + "" == "")
                        {
                            MessageBox.Show("Aten��o:" + Strings.Chr(13) + "A Contramarca na linha " + i + " para o artigo '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Artigo + "' e lote '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Lote + "' n�o est� preenchida.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Cancel = true;
                            return;
                        }
                        if (this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).CamposUtil["CDU_ContramarcaData"].Valor + "" == "")
                        {
                            MessageBox.Show("Aten��o:" + Strings.Chr(13) + "A Data da Contramarca na linha " + i + " para o artigo '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Artigo + "' e lote '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Lote + "' n�o est� preenchida.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Cancel = true;
                            return;
                        }
                        if ((int)this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).CamposUtil["CDU_ValorAduaneiro"].Valor == 0)
                        {
                            MessageBox.Show("Aten��o:" + Strings.Chr(13) + "O Valor Aduaneiro na linha " + i + " para o artigo '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Artigo + "' e lote '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Lote + "' n�o est� preenchido.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Cancel = true;
                            return;
                        }
                        if ((int)this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).CamposUtil["CDU_IvaDAU"].Valor == 0)
                        {
                            MessageBox.Show("Aten��o:" + Strings.Chr(13) + "O Valor do Iva da DAU na linha " + i + " para o artigo '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Artigo + "' e lote '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Lote + "' n�o est� preenchido.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Cancel = true;
                            return;
                        }
                        if ((int)this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).CamposUtil["CDU_DireitosDAU"].Valor == 0)
                        {
                            MessageBox.Show("Aten��o:" + Strings.Chr(13) + "Os Direitos da DAU na linha " + i + " para o artigo '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Artigo + "' e lote '" + this.DocumentoTransferencia.LinhasOrigem.GetEdita(i).Lote + "' n�o est�o preenchidos.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Cancel = true;
                            return;
                        }
                    }
                }
            }

        }

    }
}
