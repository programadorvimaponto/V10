using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using System;
using System.Windows.Forms;

namespace Default
{
    public class VndIsEditorVendas : EditorVendas
    {
        private int i;
        private int r;

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("Default") == 1)
            {
                if (this.DocumentoVenda.Entidade == "")
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Cliente não está preenchido.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }

                if ((BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).TipoMercado == "1") & (this.DocumentoVenda.Tipodoc == "FA" | this.DocumentoVenda.Tipodoc == "NC"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Cliente é intracomunitário, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
                else if ((BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).TipoMercado == "2") & (this.DocumentoVenda.Tipodoc == "FA" | this.DocumentoVenda.Tipodoc == "NC"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Cliente é extracomunitário, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
                else if ((BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).TipoMercado == "0") & (this.DocumentoVenda.Tipodoc == "FI" | this.DocumentoVenda.Tipodoc == "NCI"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Cliente é nacional, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
                else if ((BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).TipoMercado == "2") & (this.DocumentoVenda.Tipodoc == "FI" | this.DocumentoVenda.Tipodoc == "NCI"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Cliente é extracomunitário, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
                else if ((BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).TipoMercado == "0") & (this.DocumentoVenda.Tipodoc == "FO" | this.DocumentoVenda.Tipodoc == "NCO"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Cliente é nacional, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }
                else if ((BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).TipoMercado == "1") & (this.DocumentoVenda.Tipodoc == "FO" | this.DocumentoVenda.Tipodoc == "NCO"))
                {
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Cliente é intracomunitário, não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                    return;
                }

                if ((BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).TipoMercado == "0") & (this.DocumentoVenda.Tipodoc == "GR"))
                {
                    int i = 0;

                    for (i = 1; i <= this.DocumentoVenda.Linhas.NumItens; i++)
                    {
                        if (this.DocumentoVenda.Linhas.GetEdita(i).Armazem == "AEP")
                        {
                            MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Cliente é Nacional e o armazem é AEP por isso não deve ser usado neste documento.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Cancel = true;
                        }
                    }
                }

                // #################################################################
                // ####### Verifica se documento está valorizado - JFC 13-06-2017 ##
                // #################################################################
                // Desconsidera Guias de Carga a pedido de Angelo Lemos - JFC 16/05/2019
                if (this.DocumentoVenda.Tipodoc != "GC")
                {
                    for (var j = 1; j <= this.DocumentoVenda.Linhas.NumItens; j++)
                    {
                        if (Information.IsNothing(this.DocumentoVenda.Linhas.GetEdita(j).Artigo) | this.DocumentoVenda.Linhas.GetEdita(j).Artigo == "")
                        {
                        }
                        else if (this.DocumentoVenda.Linhas.GetEdita(j).PrecUnit <= 0)
                        {
                            if (MessageBox.Show("Atenção:" + Strings.Chr(13) + "Artigo " + this.DocumentoVenda.Linhas.GetEdita(j).Artigo + "-" + this.DocumentoVenda.Linhas.GetEdita(j).Lote + " está sem preço, tem a certeza que deseja gravar?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                                Cancel = true;
                        }
                    }
                }

                // #################################################################
                // ####### Verifica se documento está valorizado - JFC 13-06-2017 ##
                // #################################################################

                if (this.DocumentoVenda.UtilizaMoradaAlternativaEntreg == false & this.DocumentoVenda.LocalDescarga + "" == "")
                    this.DocumentoVenda.LocalDescarga = "V/ Morada";

                // *******************************************************************************************************************************************
                // #### ARMAZEM ENTREPOSTO ####
                // *******************************************************************************************************************************************

                // Retirado para ECL 23/7/2015 Alexandre: Sr. Angelo quer fazer ECL sem pôr os dados'

                if (this.DocumentoVenda.Tipodoc != "ECL" & this.DocumentoVenda.Tipodoc != "GC")
                {
                    for (i = 1; i <= this.DocumentoVenda.Linhas.NumItens; i++)
                    {
                        if (this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "" & this.DocumentoVenda.Linhas.GetEdita(i).Armazem == Module1.ArmEntreposto & this.DocumentoVenda.Linhas.GetEdita(i).Armazem + "" != "")
                        {
                            // Retirado para GR 16/10/2019 Bruno: A pedido de José Luis para fazer GR sem pôr os dados'
                            if (this.DocumentoVenda.Tipodoc != "GR")
                            {
                                if (this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_DespDAU"].Valor + "" == "")
                                {
                                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Código DAU na linha " + i + " para o artigo '" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "' e lote '" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "' não está preenchido.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Cancel = true;
                                    return;
                                }
                                if (this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_CODMERC"].Valor + "" == "")
                                {
                                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Código da Mercadoria na linha " + i + " para o artigo '" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "' e lote '" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "' não está preenchido.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Cancel = true;
                                    return;
                                }
                                if (this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Contramarca"].Valor + "" == "")
                                {
                                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "A Contramarca na linha " + i + " para o artigo '" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "' e lote '" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "' não está preenchida.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Cancel = true;
                                    return;
                                }
                                if (this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Regime"].Valor + "" == "")
                                {
                                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Código do Regime na linha " + i + " para o artigo '" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "' e lote '" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "' não está preenchido.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Cancel = true;
                                    return;
                                }
                            }

                            if (this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_DespTipoImportacao"].Valor + "" == "")
                            {
                                MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Tipo de Importação na linha " + i + " para o artigo '" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "' e lote '" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "' não está preenchido.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Cancel = true;
                                return;
                            }
                            if (this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Volumes"].Valor + "" == "")
                            {
                                MessageBox.Show("Atenção:" + Strings.Chr(13) + "Os Volumes na linha " + i + " para o artigo '" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "' e lote '" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "' não estão preenchidos.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Cancel = true;
                                return;
                            }
                            if (this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_MeioTransporte"].Valor + "" == "")
                            {
                                MessageBox.Show("Atenção:" + Strings.Chr(13) + "O Meio de Transporte na linha " + i + " para o artigo '" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "' e lote '" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "' não estão preenchidos.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Cancel = true;
                                return;
                            }
                            if (this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_MassaBruta"].Valor.ToString() == "0")
                            {
                                MessageBox.Show("Atenção:" + Strings.Chr(13) + "A Massa Bruta na linha " + i + " para o artigo '" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "' e lote '" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "' não está preenchida.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Cancel = true;
                                return;
                            }
                            if (this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_MassaLiq"].Valor.ToString() == "0")
                            {
                                MessageBox.Show("Atenção:" + Strings.Chr(13) + "A Massa Líquida na linha " + i + " para o artigo '" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "' e lote '" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "' não está preenchida.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Cancel = true;
                                return;
                            }
                            if (this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_ContramarcaData"].Valor + "" == "")
                            {
                                MessageBox.Show("Atenção:" + Strings.Chr(13) + "A Data da Contramarca na linha " + i + " para o artigo '" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "' e lote '" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "' não está preenchida.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Cancel = true;
                                return;
                            }
                        }
                    }
                }
                // *******************************************************************************************************************************************
                // #### ARMAZEM ENTREPOSTO ####
                // *******************************************************************************************************************************************

                // ###################################################################################
                // ####### Verifica a data de expedição é igual à data de entregao - JFC 11/02/2019 ##
                // ###################################################################################
                if (this.DocumentoVenda.Tipodoc == "GC" & Strings.Right(this.DocumentoVenda.Serie, 1) == "E")
                {
                    StdBELista Data;

                    for (var j = 1; j <= this.DocumentoVenda.Linhas.NumItens; j++)
                    {
                        if (Information.IsNothing(this.DocumentoVenda.Linhas.GetEdita(j).Artigo) | this.DocumentoVenda.Linhas.GetEdita(j).Artigo == "")
                        {
                        }
                        else
                        {
                            Data = BSO.Consulta("select DataEntrega from LinhasDoc where Id='" + this.DocumentoVenda.Linhas.GetEdita(j).IdLinhaOrigemCopia + "'");
                            Data.Inicio();
                            DateTime dt1;
                            DateTime dt;
                            dt1 = DateTime.Parse(DocumentoVenda.Linhas.GetEdita(j).CamposUtil["CDU_DataExp"].Valor.ToString());
                            dt = Data.Valor("DataEntrega");
                            if (dt == dt1)
                            {
                                MessageBox.Show("Atenção:" + Strings.Chr(13) + "Artigo " + this.DocumentoVenda.Linhas.GetEdita(j).Artigo + "-" + this.DocumentoVenda.Linhas.GetEdita(j).Lote + " está com a DATA de EXPEDIÇÃO igual à DATA de ENTREGA na Encomenda, tem a certeza que deseja gravar?", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Cancel = true;
                            }
                        }
                    }
                }
                // ###################################################################################
                // ####### Verifica a data de expedição é igual à data de entregao - JFC 11/02/2019 ##
                // ###################################################################################

                // ################################################################################################################################################################
                // # Verificar se o documento a gravar tem moeda diferente de EURO com cambio a 1                          BMP - 06/05/2021                                       #
                // ################################################################################################################################################################

                if (this.DocumentoVenda.Moeda != "EUR" & this.DocumentoVenda.Cambio == 1)
                {
                    MessageBox.Show("Atenção, não foi possivel gravar o documento, corrigir a moeda ou cambio antes de gravar! A moeda " + this.DocumentoVenda.Moeda + " tem o cambio " + this.DocumentoVenda.Cambio + "", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cancel = true;
                }
            }
        }
    }
}