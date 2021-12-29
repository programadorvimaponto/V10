    using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Constants.ExtensibilityService;
using Primavera.Extensibility.Purchases.Editors;
using StdBE100;
using System;
using System.Windows.Forms;

namespace CertificadosOrg
{
    public class CmpIsEditorCompras : EditorCompras
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);
            if (Module1.VerificaToken("CertificadosOrg") == 1)
            {
                // '*******************************************************************************************************************************************
                // '#### bruno 18/02/2020 #### validar em cnt/ecf tem produtos certificados e se o fornecedor é certificado ####
                // '*******************************************************************************************************************************************

                if (this.DocumentoCompra.Tipodoc == "CNT" | this.DocumentoCompra.Tipodoc == "ECF")
                {
                    int l;
                    StdBELista listforncert;
                    listforncert = BSO.Consulta("select *, getdate() as 'hoje' from fornecedores where fornecedor='" + this.DocumentoCompra.Entidade + "'");
                    listforncert.Inicio();
                    var loopTo = this.DocumentoCompra.Linhas.NumItens;
                    for (l = 1; l <= loopTo; l++)
                    {
                        if (this.DocumentoCompra.Linhas.GetEdita(l).Artigo + "" != "")
                        {
                            if (Strings.UCase(BSO.Base.Artigos.Edita(this.DocumentoCompra.Linhas.GetEdita(l).Artigo).CamposUtil["cdu_descricaoextra"].Valor.ToString()).Contains("GOTS"))
                            {
                                if (listforncert.Valor("cdu_gots") != true)
                                {
                                    MessageBox.Show("atenção:" + '\r' + "o fornecedor não está identificado como certificado gots.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else if (listforncert.Valor("cdu_gotsdata") < listforncert.Valor("hoje"))
                                {
                                    MessageBox.Show("atenção:" + '\r' + "o fornecedor tem o certificado gots expirado.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                            if (Strings.UCase(BSO.Base.Artigos.Edita(this.DocumentoCompra.Linhas.GetEdita(l).Artigo).CamposUtil["cdu_descricaoextra"].Valor.ToString()).Contains("GRS"))
                            {
                                if (listforncert.Valor("cdu_grs") != true)
                                {
                                    MessageBox.Show("atenção: " + '\r' + "o fornecedor não está identificado como certificado grs.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else if (listforncert.Valor("cdu_grsdata") < listforncert.Valor("hoje"))
                                {
                                    MessageBox.Show("atenção:" + '\r' + "o fornecedor tem o certificado grs expirado.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                            if (Strings.UCase(BSO.Base.Artigos.Edita(this.DocumentoCompra.Linhas.GetEdita(l).Artigo).CamposUtil["cdu_descricaoextra"].Valor.ToString()).Contains("OCS"))
                            {
                                if (listforncert.Valor("cdu_ocs") != true)
                                {
                                    MessageBox.Show("atenção:" + '\r' + "o fornecedor não está identificado como certificado ocs.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else if (listforncert.Valor("cdu_ocsdata") < listforncert.Valor("hoje"))
                                {
                                    MessageBox.Show("atenção:" + '\r' + "o fornecedor tem o certificado ocs expirado.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                            if (Strings.UCase(BSO.Base.Artigos.Edita(this.DocumentoCompra.Linhas.GetEdita(l).Artigo).CamposUtil["cdu_descricaoextra"].Valor.ToString()).Contains("BCI"))
                            {
                                if (listforncert.Valor("cdu_bci") != true)
                                {
                                    MessageBox.Show("atenção:" + '\r' + "o fornecedor não está identificado como certificado bci.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else if (listforncert.Valor("cdu_bcidata") < listforncert.Valor("hoje"))
                                {
                                    MessageBox.Show("atenção:" + '\r' + "o fornecedor tem o certificado bci expirado.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            // jfc 03/09/2020 alerta da ana castro, os bci's são identificados nas observações do lote.
                            if (Strings.UCase(BSO.Inventario.ArtigosLotes.Edita(this.DocumentoCompra.Linhas.GetEdita(l).Artigo, this.DocumentoCompra.Linhas.GetEdita(l).Lote).Observacoes).Contains("BCI"))
                            {
                                if (listforncert.Valor("cdu_bci") != true)
                                {
                                    MessageBox.Show("atenção:" + '\r' + "o fornecedor não está identificado como certificado bci.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else if (listforncert.Valor("cdu_bcidata") < listforncert.Valor("hoje"))
                                {
                                    MessageBox.Show("atenção:" + '\r' + "o fornecedor tem o certificado bci expirado.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                            if (Strings.UCase(BSO.Base.Artigos.Edita(this.DocumentoCompra.Linhas.GetEdita(l).Artigo).CamposUtil["cdu_descricaoextra"].Valor.ToString()).Contains("FSC"))
                            {
                                if (listforncert.Valor("cdu_fsc") != true)
                                {
                                    MessageBox.Show("atenção: " + '\r' + "o fornecedor não está identificado como certificado fsc.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else if (listforncert.Valor("cdu_fscdata") < listforncert.Valor("hoje"))
                                {
                                    MessageBox.Show("atenção:" + '\r' + "o fornecedor tem o certificado fsc expirado.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }

                // '*******************************************************************************************************************************************
                // '#### bruno 18/02/2020 #### validar em cnt/ecf tem produtos certificados e se o fornecedor é certificado ####
                // '*******************************************************************************************************************************************
            }
        }

        public override void DepoisDeGravar(string filial, string tipo, string serie, int numdoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(filial, tipo, serie, numdoc, e);
            if (Module1.VerificaToken("CertificadosOrg") == 1)
            {
                // ####################################################################################################################################
                // #recalculo de saldos de certificados disponiveis jfc - 22/07/2019                                                                  #
                // ####################################################################################################################################
                if (this.DocumentoCompra.Tipodoc == "ECF")
                {
                    bool recalculacerts;
                    recalculacerts = false;
                    for (int i = 1, loopTo = this.DocumentoCompra.Linhas.NumItens; i <= loopTo; i++)
                    {
                        if (this.DocumentoCompra.Linhas.GetEdita(i).CamposUtil["cdu_numcertificadotrans"].Valor + "" != "")
                        {
                            recalculacerts = true;
                        }
                    }

                    if (recalculacerts)
                    {
                        BSO.DSO.ExecuteSQL("exec [dbo].[spinserircert]");
                    }
                }
                // ####################################################################################################################################
                // #recalculo de saldos de certificados disponiveis jfc - 22/07/2019                                                                  #
                // ####################################################################################################################################
            }
        }

        public override void TeclaPressionada(int keycode, int shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(keycode, shift, e);
            if (Module1.VerificaToken("CertificadosOrg") == 1)
            {
                // ################################################################################################
                // # inserir certificados de transação, formulário frmalteracertificadotransacao (jfc 20/03/2019) #
                // ################################################################################################
                // crtl+f- alteracertificadotransacao
                if (this.LinhaActual > 0)
                {
                    if (keycode == 70 & this.DocumentoCompra.Tipodoc == "ECF")
                    {
                        Module1.certArtigo = this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).Artigo;
                        Module1.certDescricao = this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).Descricao;
                        Module1.certDocumento = this.DocumentoCompra.Tipodoc + " " + this.DocumentoCompra.NumDoc + "/" + this.DocumentoCompra.Serie;
                        Module1.certLote = this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).Lote;
                        Module1.certArmazem = this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).Armazem;
                        Module1.certIDlinha = this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).IdLinha;

                        if (this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).CamposUtil["cdu_numcertificadotrans"].Valor is string certificado) Module1.certCertificadoTransacao = certificado; else Module1.certCertificadoTransacao = string.Empty;
                        if (this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).CamposUtil["cdu_datacertificadotrans"].Valor is DateTime data) Module1.certDataCertificado = data; else Module1.certDataCertificado = DateTime.Now;
                        if (this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).CamposUtil["cdu_programlabels"].Valor is Int32 program) Module1.certProgramLabel = program; else Module1.certProgramLabel = 0;
                        if (this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).CamposUtil["cdu_bci"].Valor is bool bci) Module1.certBCI = bci; else Module1.certBCI = false;

                        //Module1.certCertificadoTransacao = this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).CamposUtil["cdu_numcertificadotrans"].Valor.ToString();
                        //Module1.certDataCertificado = DateTime.Parse(this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).CamposUtil["cdu_datacertificadotrans"].Valor.ToString());
                        //Module1.certProgramLabel = int.Parse(this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).CamposUtil["cdu_programlabels"].Valor.ToString());
                        //Module1.certBCI = bool.Parse(this.DocumentoCompra.Linhas.GetEdita(this.LinhaActual).CamposUtil["cdu_bci"].Valor.ToString());



                        ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmAlteraCertificadoTransacaoView));
                        if (result.ResultCode == ExtensibilityResultCode.Ok)
                        {
                            FrmAlteraCertificadoTransacaoView frm = result.Result;
                            frm.DocumentoCompra = DocumentoCompra;
                            frm.LinhaActual = LinhaActual;
                            frm.ShowDialog();
                        }
                    }
                }
                // ################################################################################################
                // # inserir certificados de transação, formulário frmalteracertificadotransacao (jfc 20/03/2019) #
                // ################################################################################################
            }
        }
    }
}