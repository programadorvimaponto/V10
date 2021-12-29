using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Constants.ExtensibilityService;
using Primavera.Extensibility.Sales.Editors;
using System;

namespace CertificadosOrg
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);
            if (Module1.VerificaToken("CertificadosOrg") == 1)
            {
                string descricao = Strings.UCase(BSO.Base.Artigos.DaValorAtributo(Artigo, "Descricao"));
                string descricaocomp = Strings.UCase(BSO.Base.Artigos.DaValorAtributo(Artigo, "Descricao") + " " + BSO.Base.Artigos.DaValorAtributo(this.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtraExterna"));
                // Ao identificar o Artigo, caso o mesmo tenha GOTS, OCS ou GRS na descrição, identifica a linha para emissão de certificado. JFC 22/07/2019
                if (descricao.Contains("RAMA") && descricao.Contains("ORG") || descricaocomp.Contains("GOTS") || descricaocomp.Contains("GRS") || descricaocomp.Contains("OCS"))
                {
                    this.DocumentoVenda.Linhas.GetEdita(NumLinha).CamposUtil["CDU_EmitirCertificado"].Valor = true;
                }
            }
        }

        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);
            if (Module1.VerificaToken("CertificadosOrg") == 1)
            {
                // ####################################################################################################################################
                // #Recalculo de saldos de certificados disponiveis JFC - 22/07/2019                                                                  #
                // ####################################################################################################################################

                bool RecalculaCerts;
                RecalculaCerts = false;
                for (int i = 1, loopTo = this.DocumentoVenda.Linhas.NumItens; i <= loopTo; i++)
                {
                    if (this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_NumCertificadoTrans"].Valor == null) this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_NumCertificadoTrans"].Valor = string.Empty;

                        if (this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_NumCertificadoTrans"].Valor.ToString() + "" != "" || this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_NumCertificadoTrans2"].Valor + "" != "" | this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_NumCertificadoTrans3"].Valor + "" != "" || (bool)this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_EmitirCertificado"].Valor == true)
                        {
                            RecalculaCerts = true;
                        }
                }

                if (RecalculaCerts)
                {
                    BSO.DSO.ExecuteSQL("exec [dbo].[spInserirCert]");
                }

                // ####################################################################################################################################
                // #Recalculo de saldos de certificados disponiveis JFC - 22/07/2019                                                                  #
                // ####################################################################################################################################

                if (this.DocumentoVenda.Tipodoc == "ECL" | this.DocumentoVenda.Tipodoc == "GR" | BSO.Vendas.TabVendas.Edita(this.DocumentoVenda.Tipodoc).TipoDocumento == 4)
                {
                    RecalculaCerts = false;
                    // Dim CertLinha As StdBELista
                    for (int i = 1, loopTo1 = this.DocumentoVenda.Linhas.NumItens; i <= loopTo1; i++)
                    {
                        if ((bool)this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_EmitirCertificado"].Valor == true)
                        {
                            // Set CertLinha = Aplicacao.BSO.Consulta("select * from cabecdoc cd inner join linhasdoc ln on ln.idcabecdoc=cd.id where cd.tipodoc='ECL' and ln.Id='" & Me.DocumentoVenda.Linhas(i).IdLinha & "'")

                            // CertLinha.Inicio
                            // If CertLinha.Vazia Then
                            RecalculaCerts = true;
                            // End If
                        }
                    }
                }

                if (BSO.Vendas.TabVendas.Edita(this.DocumentoVenda.Tipodoc).TipoDocumento == 4)
                {
                    if (RecalculaCerts)
                    {
                        EnviaEmailCertificado();
                    }
                }

                NotaCreditoComCertificado();
            }
        }

        public override void DepoisDeTransformar(ExtensibilityEventArgs e)
        {
            base.DepoisDeTransformar(e);
            if (Module1.VerificaToken("CertificadosOrg") == 1)
            {
                NotaCreditoComCertificado();
            }
        }

        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(KeyCode, Shift, e);
            if (Module1.VerificaToken("CertificadosOrg") == 1)
            {
                // #################################################################################################
                // # Inserir Certificados de Transação, Formulário FrmAlteraCertificadoTransacao2 (JFC 13/05/2019) #
                // #################################################################################################
                // Crtl+F- AlteraCertificadoTransacao
                if (this.LinhaActual > 0)
                {
                    if (KeyCode == 70)
                    {
                        Module1.certArtigo = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Artigo;
                        Module1.certDocumento = this.DocumentoVenda.Tipodoc + " " + this.DocumentoVenda.NumDoc + "/" + this.DocumentoVenda.Serie;
                        Module1.certLote = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Lote;
                        Module1.certArmazem = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Armazem;
                        Module1.certIDlinha = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).IdLinha;
                        Module1.certDescricao = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).Descricao;

                        if (this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_NumCertificadoTrans"].Valor.ToString() is string trans) Module1.certCertificadoTransacao = trans; else Module1.certCertificadoTransacao = string.Empty;
                        if (this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_NumCertificadoTrans2"].Valor.ToString() is string trans2) Module1.certCertificadoTransacao2 = trans2; else Module1.certCertificadoTransacao2 = string.Empty;
                        if (this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_NumCertificadoTrans3"].Valor.ToString() is string trans3) Module1.certCertificadoTransacao3 = trans3; else Module1.certCertificadoTransacao3 = string.Empty;
                        if (this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_QtdCertificadoTrans"].Valor is double certificadotrans) Module1.certQtdTransacao = certificadotrans; else Module1.certQtdTransacao = 0;
                        if (this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_QtdCertificadoTrans2"].Valor is double certificadotrans2) Module1.certQtdTransacao2 = certificadotrans2; else Module1.certQtdTransacao2 = 0;
                        if (this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_QtdCertificadoTrans3"].Valor is double certificadotrans3) Module1.certQtdTransacao3 = certificadotrans3; else Module1.certQtdTransacao3 = 0;
                        if (this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_CertificadoEmitido"].Valor is bool certemitido) Module1.certEmitido = certemitido; else Module1.certEmitido = false;
                        if (this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_BCIEmitido"].Valor is bool bciemitido) Module1.certBCIEmitido = bciemitido; else Module1.certBCIEmitido = false;
                        if (this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_EmitirCertificado"].Valor is bool certemitir) Module1.certEmitir = certemitir; else Module1.certEmitir = false;
                        if (this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_ObsCertificadoTrans"].Valor.ToString() is string certobs) Module1.certObs = certobs; else Module1.certObs = string.Empty;

                        //Module1.certCertificadoTransacao = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_NumCertificadoTrans"].Valor.ToString();
                        //Module1.certCertificadoTransacao2 = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_NumCertificadoTrans2"].Valor.ToString();
                        //Module1.certCertificadoTransacao3 = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_NumCertificadoTrans3"].Valor.ToString();
                        //Module1.certQtdTransacao = double.Parse(this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_QtdCertificadoTrans"].Valor.ToString());
                        //Module1.certQtdTransacao2 = double.Parse(this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_QtdCertificadoTrans2"].Valor.ToString());
                        //Module1.certQtdTransacao3 = double.Parse(this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_QtdCertificadoTrans3"].Valor.ToString());
                        //Module1.certEmitido = bool.Parse(this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_CertificadoEmitido"].Valor.ToString());
                        //Module1.certBCIEmitido = bool.Parse(this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_BCIEmitido"].Valor.ToString());
                        //Module1.certEmitir = bool.Parse(this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_EmitirCertificado"].Valor.ToString());
                        //Module1.certObs = this.DocumentoVenda.Linhas.GetEdita(this.LinhaActual).CamposUtil["CDU_ObsCertificadoTrans"].Valor.ToString();

                        ExtensibilityResult result = BSO.Extensibility.CreateCustomFormInstance(typeof(FrmAlteraCertificadoTransacao2View));
                        if (result.ResultCode == ExtensibilityResultCode.Ok)
                        {
                            FrmAlteraCertificadoTransacao2View frm = result.Result;
                            frm.DocumentoVenda = DocumentoVenda;
                            frm.LinhaActual = LinhaActual;
                            frm.ShowDialog();
                        }
                    }
                }
                // #################################################################################################
                // # Inserir Certificados de Transação, Formulário FrmAlteraCertificadoTransacao2 (JFC 13/05/2019) #
                // #################################################################################################
            }
        }

        private object NotaCreditoComCertificado()
        {
            // ################################################################################################################
            // ##Envia e-mail caso uma nota de credito contenha um artigo certificado. Pedido de Ana Castro. JFC - 03/04/2020##
            // ################################################################################################################
            if (Strings.Left(this.DocumentoVenda.Tipodoc, 2) == "NC")
            {
                for (int i = 1, loopTo = this.DocumentoVenda.Linhas.NumItens; i <= loopTo; i++)
                {
                    if (this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "")
                    {
                        if (Strings.UCase(this.DocumentoVenda.Linhas.GetEdita(i).Descricao).Contains("BCI") | Strings.UCase(this.DocumentoVenda.Linhas.GetEdita(i).Descricao).Contains("OCS") | Strings.UCase(this.DocumentoVenda.Linhas.GetEdita(i).Descricao).Contains("GRS") | Strings.UCase(this.DocumentoVenda.Linhas.GetEdita(i).Descricao).Contains("GOTS"))
                        {
                            EnviaEmailCertificadoNC();
                            break;
                        }
                    }
                }
            }

            return default;
            // ################################################################################################################
            // ##Envia e-mail caso uma nota de credito contenha um artigo certificado. Pedido de Ana Castro. JFC - 03/04/2020##
            // ################################################################################################################
        }

        // *******************************************************************************************************************************************
        // #### Enviar Mail para Qualidade para emissão de Certificados GOTS, OCS e GRS - 22/07/2019(JFC)                                         ####
        // *******************************************************************************************************************************************

        private string VarCliente;
        private string VarFrom;
        private string VarTo;
        private string VarAssunto;
        private string VarTextoInicialMsg;
        private string VarMensagem;
        private string VarArmazem;
        private string VarLinhas;
        private string VarUtilizador;
        private int VarLocalTeste; // 0 - ao seleccionar o cliente; 1 - antes de gravar o documento(ECL ou GR)
        private bool VarCancelaDoc;
        private bool VarNetTrans;

        private object EnviaEmailCertificado()
        {
            VarCliente = this.DocumentoVenda.Entidade;
            int ln;
            VarFrom = "";
            VarTo = "informatica@mundifios.pt; certificados@mundifios.pt;";
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

            VarAssunto = "(Cert) Documento: " + this.DocumentoVenda.Tipodoc + " " + Strings.Format(this.DocumentoVenda.NumDoc, "####") + "/" + this.DocumentoVenda.Serie + " (" + Strings.Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") + ")";
            VarUtilizador = Aplicacao.Utilizador.Utilizador;
            VarLinhas = "";
            var loopTo = this.DocumentoVenda.Linhas.NumItens;
            for (ln = 1; ln <= loopTo; ln++)
            {
                if (this.DocumentoVenda.Linhas.GetEdita(ln).Artigo + "" != "" + this.DocumentoVenda.Linhas.GetEdita(ln).CamposUtil["CDU_EmitirCertificado"].Valor)
                {
                    VarLinhas = VarLinhas + "Linha " + ln + ":                         " + this.DocumentoVenda.Linhas.GetEdita(ln).Artigo + " - Lote:" + this.DocumentoVenda.Linhas.GetEdita(ln).Lote + " - Desc:" + this.DocumentoVenda.Linhas.GetEdita(ln).Descricao + " - Quantidade:" + this.DocumentoVenda.Linhas.GetEdita(ln).Quantidade + this.DocumentoVenda.Linhas.GetEdita(ln).Unidade + " - Cert:" + this.DocumentoVenda.Linhas.GetEdita(ln).CamposUtil["CDU_QtdCertificadoTrans"].Valor + this.DocumentoVenda.Linhas.GetEdita(ln).CamposUtil["CDU_QtdCertificadoTrans2"].Valor + this.DocumentoVenda.Linhas.GetEdita(ln).CamposUtil["CDU_QtdCertificadoTrans3"].Valor + this.DocumentoVenda.Linhas.GetEdita(ln).Unidade + '\r' + "";
                }
            }

            VarMensagem = VarTextoInicialMsg + '\r' + '\r' + '\r' + "Foi emitido um Documento no Primavera, por favor valide os Certificados de Transação:" + '\r' + '\r' + "" + "Empresa:                         " + BSO.Contexto.CodEmp + " - " + BSO.Contexto.IDNome + '\r' + "" + "Utilizador:                      " + VarUtilizador + '\r' + '\r' + "" + "Cliente:                         " + VarCliente + " - " + Strings.Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") + '\r' + "" + "Documento:                       " + this.DocumentoVenda.Tipodoc + " " + Strings.Format(this.DocumentoVenda.NumDoc, "#,###") + "/" + this.DocumentoVenda.Serie + '\r' + '\r' + "" + "Local Descarga:                  " + this.DocumentoVenda.LocalDescarga + '\r' + "" + "Morada Entrega:                  " + Strings.Replace(this.DocumentoVenda.MoradaEntrega, "'", "") + '\r' + '\r' + "" + VarLinhas + '\r' + "" + "Cumprimentos";

            BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL] ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" + Strings.Format(DateAndTime.Now, "yyyy-MM-dd HH:mm:ss") + "', '" + VarFrom + "', '" + VarTo + "','','','" + VarAssunto + "','" + VarMensagem + "','',0,'" + VarUtilizador + "' )");
            return default;
        }

        // *******************************************************************************************************************************************
        // #### Enviar Mail para Qualidade para emissão de Certificados GOTS, OCS e GRS - 22/07/2019(JFC)                                         ####
        // *******************************************************************************************************************************************

        // *******************************************************************************************************************************************
        // #### Enviar Mail para Certificados quando existe uma nota de credito - 03/04/2020(JFC)                                                 ####
        // *******************************************************************************************************************************************

        private object EnviaEmailCertificadoNC()
        {
            VarCliente = this.DocumentoVenda.Entidade;
            int ln;
            VarFrom = "";
            VarTo = "informatica@mundifios.pt; certificados@mundifios.pt;";
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

            VarAssunto = "(Cert) Nota de Credito: " + this.DocumentoVenda.Tipodoc + " " + Strings.Format(this.DocumentoVenda.NumDoc, "####") + "/" + this.DocumentoVenda.Serie + " (" + Strings.Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") + ")";
            VarUtilizador = Aplicacao.Utilizador.Utilizador;
            VarLinhas = "";
            var loopTo = this.DocumentoVenda.Linhas.NumItens;
            for (ln = 1; ln <= loopTo; ln++)
            {
                if (this.DocumentoVenda.Linhas.GetEdita(ln).Artigo + "" != "")
                {
                    VarLinhas = VarLinhas + "Linha " + ln + ":                         " + this.DocumentoVenda.Linhas.GetEdita(ln).Artigo + " - Lote:" + this.DocumentoVenda.Linhas.GetEdita(ln).Lote + " - Desc:" + this.DocumentoVenda.Linhas.GetEdita(ln).Descricao + " - Quantidade:" + this.DocumentoVenda.Linhas.GetEdita(ln).Quantidade + this.DocumentoVenda.Linhas.GetEdita(ln).Unidade + " - Cert:" + this.DocumentoVenda.Linhas.GetEdita(ln).CamposUtil["CDU_QtdCertificadoTrans"].Valor + this.DocumentoVenda.Linhas.GetEdita(ln).CamposUtil["CDU_QtdCertificadoTrans2"].Valor + this.DocumentoVenda.Linhas.GetEdita(ln).CamposUtil["CDU_QtdCertificadoTrans3"].Valor + this.DocumentoVenda.Linhas.GetEdita(ln).Unidade + '\r' + "";
                }
            }

            VarMensagem = VarTextoInicialMsg + '\r' + '\r' + '\r' + "Foi emitido uma Nota de Credito, por favor valide os Certificados de Transação:" + '\r' + '\r' + "" + "Empresa:                         " + BSO.Contexto.CodEmp + " - " + BSO.Contexto.IDNome + '\r' + "" + "Utilizador:                      " + VarUtilizador + '\r' + '\r' + "" + "Cliente:                         " + VarCliente + " - " + Strings.Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") + '\r' + "" + "Documento:                       " + this.DocumentoVenda.Tipodoc + " " + Strings.Format(this.DocumentoVenda.NumDoc, "#,###") + "/" + this.DocumentoVenda.Serie + '\r' + '\r' + "" + "Local Descarga:                  " + this.DocumentoVenda.LocalDescarga + '\r' + "" + "Morada Entrega:                  " + Strings.Replace(this.DocumentoVenda.MoradaEntrega, "'", "") + '\r' + '\r' + "" + VarLinhas + '\r' + "" + "Cumprimentos";

            BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL] ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" + Strings.Format(DateAndTime.Now, "yyyy-MM-dd HH:mm:ss") + "', '" + VarFrom + "', '" + VarTo + "','','','" + VarAssunto + "','" + VarMensagem + "','',0,'" + VarUtilizador + "' )");
            return default;
        }

        // *******************************************************************************************************************************************
        // #### Enviar Mail para Certificados quando existe uma nota de credito - 03/04/2020(JFC)                                                 ####
        // *******************************************************************************************************************************************
    }
}