using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Constants.ExtensibilityService;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using System;
using System.Windows.Forms;
using VndBE100;

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

        private VndBEDocumentoVenda DocVenda = new VndBEDocumentoVenda();

        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(KeyCode, Shift, e);

            if (Module1.VerificaToken("Default") == 1)
            {

                // Alt+Q - Altera Encomenda
                if (KeyCode == 81 & BSO.Vendas.TabVendas.Edita(this.DocumentoVenda.Tipodoc).TipoDocumento == 2)
                {
                    // Verifica se é uma linha que não existe na tabela linhascompras
                    if (this.LinhaActual == -1)
                    {
                        return;
                    }

                    // Verifica se é uma linha de texto, sem artigo
                    if (this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Artigo + "" == "")
                    {
                        return;
                    }

                    Module1.NovaQuantidadeEnc = 0;
                    Module1.NovaQtReservadaEnc = 0;
                    Module1.NovoPrecoEnc = 0;
                    Module1.ArtigoEnc = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Artigo;
                    Module1.DescArtEnc = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Descricao;
                    Module1.LoteEnc = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Lote;
                    Module1.QuantidadeEnc = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Quantidade;
                    Module1.QtReservadaEnc = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).QuantReservada;
                    Module1.QtSatisfeitaEnc = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).QuantSatisfeita;
                    Module1.PrecoEnc = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).PrecUnit;
                    Module1.IdLinhaEnc = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).IdLinha;
                    Module1.ObsEnc = DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_Observacoes"].Valor.ToString();

                    ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmAlteraEstadoEncomendaView));

                    if (result.ResultCode == ExtensibilityResultCode.Ok)
                    {
                        FrmAlteraEstadoEncomendaView frm = result.Result;
                        frm.desativarOF = false;
                        frm.ShowDialog();
                    }

                    if (Module1.Opcao == 1)
                    {
                        BSO.DSO.ExecuteSQL("UPDATE CABECDOCSTATUS SET FECHADO = 0 WHERE IDCABECDOC = '" + this.DocumentoVenda.ID + "'");

                        DocVenda = BSO.Vendas.Documentos.Edita(DocumentoVenda.Filial, DocumentoVenda.Tipodoc, DocumentoVenda.Serie, DocumentoVenda.NumDoc);

                        for (int i = 1, loopTo = DocVenda.Linhas.NumItens; i <= loopTo; i++)
                        {
                            if (DocVenda.Linhas.GetEdita(i).Estado == "P")
                            {
                                BSO.DSO.ExecuteSQL("UPDATE LINHASDOCSTATUS SET FECHADO = 0 WHERE IDLINHASDOC = '" + Module1.IdLinhaEnc + "'");
                            }
                        }
                    }
                    else if (Module1.Opcao == 2)
                    {
                        BSO.DSO.ExecuteSQL("UPDATE LINHASDOCSTATUS SET FECHADO = 0 WHERE IDLINHASDOC = '" + Module1.IdLinhaEnc + "'");
                    }
                    else if (Module1.Opcao == 3)
                    {
                        BSO.DSO.ExecuteSQL("UPDATE LINHASDOCSTATUS SET FECHADO = 1 WHERE IDLINHASDOC = '" + Module1.IdLinhaEnc + "'");
                    }
                    else if (Module1.Opcao == 4)
                    {
                        if (Module1.NovaQuantidadeEnc != 0)
                        {
                            this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Quantidade = Module1.NovaQuantidadeEnc;
                            BSO.DSO.ExecuteSQL("UPDATE LINHASDOC SET QUANTIDADE = " + Strings.Replace(Module1.NovaQuantidadeEnc.ToString(), ",", ".") + " WHERE ID = '" + Module1.IdLinhaEnc + "'");
                            BSO.DSO.ExecuteSQL("UPDATE LINHASDOCSTATUS SET FECHADO = 0, QUANTIDADE = " + Strings.Replace(Module1.NovaQuantidadeEnc.ToString(), ",", ".") + " WHERE IDLINHASDOC = '" + Module1.IdLinhaEnc + "'");

                            DocVenda = BSO.Vendas.Documentos.Edita(DocumentoVenda.Filial, DocumentoVenda.Tipodoc, DocumentoVenda.Serie, DocumentoVenda.NumDoc);

                            BSO.Vendas.Documentos.Actualiza(DocVenda);

                            DocVenda = BSO.Vendas.Documentos.Edita(DocumentoVenda.Filial, DocumentoVenda.Tipodoc, DocumentoVenda.Serie, DocumentoVenda.NumDoc);

                            for (int i = 1, loopTo1 = DocVenda.Linhas.NumItens; i <= loopTo1; i++)
                            {
                                if (DocVenda.Linhas.GetEdita(i).Estado == "P")
                                {
                                    BSO.DSO.ExecuteSQL("UPDATE CABECDOCSTATUS SET ESTADO = 'P', ANULADO = 0, FECHADO = 0 WHERE IDCABECDOC = '" + DocVenda.ID + "'");
                                    break;
                                }

                                BSO.DSO.ExecuteSQL("UPDATE CABECDOCSTATUS SET ESTADO = 'T' WHERE IDCABECDOC = '" + DocVenda.ID + "'");
                            }
                        }

                        if (Module1.NovaQtReservadaEnc != 0)
                        {
                            this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Quantidade = Module1.NovaQtReservadaEnc;
                            BSO.DSO.ExecuteSQL("UPDATE LINHASDOCSTATUS SET QUANTRESERV = " + Strings.Replace(Module1.NovaQtReservadaEnc.ToString(), ",", ".") + " WHERE IDLINHASDOC = '" + Module1.IdLinhaEnc + "'");

                            DocVenda = BSO.Vendas.Documentos.Edita(DocumentoVenda.Filial, DocumentoVenda.Tipodoc, DocumentoVenda.Serie, DocumentoVenda.NumDoc);

                            BSO.Vendas.Documentos.Actualiza(DocVenda);
                        }

                        if (Module1.NovoPrecoEnc != 0)
                        {
                            this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).PrecUnit = Module1.NovoPrecoEnc;
                            BSO.DSO.ExecuteSQL("UPDATE LINHASDOC SET PRECUNIT = " + Strings.Replace(Module1.NovoPrecoEnc.ToString(), ",", ".") + " WHERE ID = '" + Module1.IdLinhaEnc + "'");

                            DocVenda = BSO.Vendas.Documentos.Edita(DocumentoVenda.Filial, DocumentoVenda.Tipodoc, DocumentoVenda.Serie, DocumentoVenda.NumDoc);

                            BSO.Vendas.Documentos.Actualiza(DocVenda);
                        }
                    }
                    else if (Module1.Opcao == 5)
                    {
                        BSO.DSO.ExecuteSQL("UPDATE CabecDoc SET CDU_FechadoOF = 1 WHERE Id = '" + this.DocumentoVenda.ID + "'");
                    }
                    else if (Module1.Opcao == 6)
                    {
                        BSO.DSO.ExecuteSQL("UPDATE CabecDoc SET CDU_FechadoOF = 0 WHERE Id = '" + this.DocumentoVenda.ID + "'");
                    }
                    else if (Module1.Opcao == 7)
                    {
                        BSO.DSO.ExecuteSQL("UPDATE CABECDOCSTATUS SET FECHADO = 1 WHERE IDCABECDOC = '" + this.DocumentoVenda.ID + "'");

                        DocVenda = BSO.Vendas.Documentos.Edita(DocumentoVenda.Filial, DocumentoVenda.Tipodoc, DocumentoVenda.Serie, DocumentoVenda.NumDoc);

                        for (int i = 1, loopTo2 = DocVenda.Linhas.NumItens; i <= loopTo2; i++)
                        {
                            if (DocVenda.Linhas.GetEdita(i).Estado == "P")
                            {
                                BSO.DSO.ExecuteSQL("UPDATE LINHASDOCSTATUS SET FECHADO = 1 WHERE IDLINHASDOC = '" + Module1.IdLinhaEnc + "'");
                            }
                        }
                    }
                    else if (Module1.Opcao == 8)
                    {
                        BSO.DSO.ExecuteSQL("UPDATE LinhasDoc SET CDU_FechadoOF = 1 WHERE Id = '" + Module1.IdLinhaEnc + "'");
                    }
                    else if (Module1.Opcao == 9)
                    {
                        BSO.DSO.ExecuteSQL("UPDATE LinhasDoc SET CDU_FechadoOF = 0 WHERE Id = '" + Module1.IdLinhaEnc + "'");
                    }
                    BSO.DSO.ExecuteSQL("UPDATE LinhasDoc SET CDU_Observacoes = '" + Module1.ObsEnc + "' WHERE Id = '" + Module1.IdLinhaEnc + "'");
                    DocumentoVenda.Linhas.GetEdita(LinhaActual).CamposUtil["CDU_Observacoes"].Valor = Module1.ObsEnc;
                }
            }
        }
        public override void ClienteIdentificado(string Cliente, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ClienteIdentificado(Cliente, ref Cancel, e);

            if (Module1.VerificaToken("Default") == 1)
            {

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
            }

        }
        // Variáveis para e-mail
        string VarCliente;
        string VarFrom;
        string VarTo;
        string VarAssunto;
        string VarTextoInicialMsg;
        string VarMensagem;
        string VarArmazem;
        string VarLinhas;
        string VarUtilizador;
        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            if (Module1.VerificaToken("Default") == 1)
            {
                base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

                // ################################################################################################################
                // ##Envia e-mail no caso de uma GR com vencimento inferior a 8 dias. Pedido de Sofia Mendes. JFC - 24/04/2020   ##
                // ################################################################################################################
                if (this.DocumentoVenda.Tipodoc == "GR" & DateAndTime.DateDiff("d", this.DocumentoVenda.DataDoc, this.DocumentoVenda.DataVenc) < 9)
                {
                    VarCliente = this.DocumentoVenda.Entidade;
                    VarFrom = "";
                    VarTo = "informatica@mundifios.pt; tesouraria.clientes@mundifios.pt; faturacao@mundifios.pt";
                    if (DateTime.Now.TimeOfDay >= new TimeSpan(7, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(12, 59, 0))
                    {
                        VarTextoInicialMsg = "Bom dia,";
                    }
                    else if (DateTime.Now.TimeOfDay >= new TimeSpan(13, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(19, 59, 0))
                    {
                        VarTextoInicialMsg = "Boa tarde,";
                    }
                    else
                    {
                        VarTextoInicialMsg = "Boa noite,";
                    }

                    VarAssunto = "Emitir Fatura: " + this.DocumentoVenda.Tipodoc + " " + Strings.Format(this.DocumentoVenda.NumDoc, "####") + "/" + this.DocumentoVenda.Serie + " (" + Strings.Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") + ")";
                    VarUtilizador = Aplicacao.Utilizador.Utilizador;
                    VarMensagem = VarTextoInicialMsg + '\r' + '\r' + '\r' + "Foi emitido uma Guia com prazo de pagamento inferior ou igual a 8 dias, por favor emita a respetiva fatura:" + '\r' + '\r' + "" + "Empresa:                         " + BSO.Contexto.CodEmp + " - " + BSO.Contexto.IDNome + '\r' + "" + "Utilizador:                      " + VarUtilizador + '\r' + '\r' + "" + "Cliente:                         " + VarCliente + " - " + Strings.Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") + '\r' + "" + "Documento:                       " + this.DocumentoVenda.Tipodoc + " " + Strings.Format(this.DocumentoVenda.NumDoc, "#,###") + "/" + this.DocumentoVenda.Serie + '\r' + '\r' + "" + "Data Vencimento:                 " + this.DocumentoVenda.DataVenc + '\r' + '\r' + "" + "Local Descarga:                  " + this.DocumentoVenda.LocalDescarga + '\r' + "" + "Morada Entrega:                  " + Strings.Replace(this.DocumentoVenda.MoradaEntrega, "'", "") + '\r' + '\r' + "" + "Cumprimentos";

                    BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL] ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" + Strings.Format(DateAndTime.Now, "yyyy-MM-dd HH:mm:ss") + "', '" + VarFrom + "', '" + VarTo + "','','','" + VarAssunto + "','" + VarMensagem + "','',0,'" + VarUtilizador + "' )");

                }
            }
            // ################################################################################################################
            // ##Envia e-mail no caso de uma GR com vencimento inferior a 8 dias. Pedido de Sofia Mendes. JFC - 24/04/2020   ##
            // ################################################################################################################



            // ****************************************************************************************************************************************************
            // #### Enviar e-mail de alerta aquando da emissão de ECL/GR para clientes espanhóis, que não tenham vendas nos ultimos 12 meses -03/11/2020 Bruno ####
            // ****************************************************************************************************************************************************

            string SQLList;
            var listadocs = default(StdBELista);
            if ((this.DocumentoVenda.Tipodoc == "GR" | this.DocumentoVenda.Tipodoc == "ECL") & this.DocumentoVenda.Pais == "ES")
            {
                SQLList = "select * from Primundifios.dbo.CabecDoc cd inner join DocumentosVenda dv on dv.Documento = cd.TipoDoc where dv.TipoDocumento='4' and cd.data > DATEADD(dd,-365,GETDATE()) and cd.Entidade='" + this.DocumentoVenda.Entidade + "'";


                listadocs = BSO.Consulta(SQLList);

                if (listadocs.Vazia())
                {
                    EnviaEmailVIES();
                }
            }


            // ****************************************************************************************************************************************************
            // #### Enviar e-mail de alerta aquando da emissão de ECL/GR para clientes espanhóis, que não tenham vendas nos ultimos 12 meses -03/11/2020 Bruno ####
            // ****************************************************************************************************************************************************


        }

        // *********************************************************************************************************************************************
        // #### Enviar Mail para a Sofia para verificar se o cliente está inscrito no VIES se nao tiver vendas nos ultimos 12meses - 03/11/2020 BRUNO###
        // *********************************************************************************************************************************************
        private object EnviaEmailVIES()
        {
            VarCliente = this.DocumentoVenda.Entidade;
            VarFrom = "";
            VarTo = "tesouraria.clientes@mundifios.pt;";
            if (DateTime.Now.TimeOfDay >= new TimeSpan(7, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(12, 59, 0))
            {
                VarTextoInicialMsg = "Bom dia,";
            }
            else if (DateTime.Now.TimeOfDay >= new TimeSpan(13, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(19, 59, 0))
            {
                VarTextoInicialMsg = "Boa tarde,";
            }
            else
            {
                VarTextoInicialMsg = "Boa noite,";
            }

            VarAssunto = "Documento: " + this.DocumentoVenda.Tipodoc + " " + Strings.Format(this.DocumentoVenda.NumDoc, "####") + "/" + this.DocumentoVenda.Serie + " (" + Strings.Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") + ")";
            VarUtilizador = Aplicacao.Utilizador.Utilizador;
            VarMensagem = VarTextoInicialMsg + '\r' + '\r' + '\r' + "Foi lançado um Documento no Primavera com cliente espanhol sem documentos emitidos no ultimo ano, verifique se o VAT está valido no VIES" + '\r' + '\r' + "" + "Empresa:                         " + BSO.Contexto.CodEmp + " - " + BSO.Contexto.IDNome + '\r' + "" + "Utilizador:                      " + VarUtilizador + '\r' + '\r' + "" + "Cliente:                         " + VarCliente + " - " + Strings.Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") + '\r' + "" + "Documento:                       " + this.DocumentoVenda.Tipodoc + " " + Strings.Format(this.DocumentoVenda.NumDoc, "#,###") + "/" + this.DocumentoVenda.Serie + '\r' + '\r' + "" + "Cumprimentos";


            BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL] ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" + Strings.Format(DateAndTime.Now, "yyyy-MM-dd HH:mm:ss") + "', '" + VarFrom + "', '" + VarTo + "','','','" + VarAssunto + "','" + VarMensagem + "','',0,'" + VarUtilizador + "' )");


            return default;
        }
        // *********************************************************************************************************************************************
        // #### Enviar Mail para a Sofia para verificar se o cliente está inscrito no VIES se nao tiver vendas nos ultimos 12meses - 03/11/2020 BRUNO###
        // *********************************************************************************************************************************************

    }
}