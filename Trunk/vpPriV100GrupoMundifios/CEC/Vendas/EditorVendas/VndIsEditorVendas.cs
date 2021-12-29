    using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using System;
using System.IO;
using VndBE100;
using static StdPlatBS100.StdBSTipos;

namespace CEC
{
    public class VndIsEditorVendas : EditorVendas
    {
        private bool NovaEncomenda;

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("CEC") == 1)
            {
                // #################################################################################################
                // ####### Verifica se documento já exite, senão zera os campos da CEC - JFC 24-01-2019 ############
                // #################################################################################################

                StdBELista EncomendaExiste;

                if ((this.DocumentoVenda.Tipodoc == "ECL"))
                {
                    EncomendaExiste = BSO.Consulta("SELECT Entidade FROM CabecDoc cd Where cd.TipoDoc='ECL' and cd.Serie=" + "'" + this.DocumentoVenda.Serie + "' and cd.NumDoc=" + "'" + this.DocumentoVenda.NumDoc + "'");

                    // 10/05/2021 - JFC aproveito esta query para guardar bolean
                    NovaEncomenda = false;

                    if ((EncomendaExiste.Vazia() == true))
                    {
                        this.DocumentoVenda.CamposUtil["CDU_TipoDocRastreabilidade"].Valor = 0;
                        this.DocumentoVenda.CamposUtil["CDU_NumDocRastreabilidade"].Valor = 0;
                        this.DocumentoVenda.CamposUtil["CDU_SerieDocRastreabilidade"].Valor = 0;
                        // 10/05/2021 - JFC aproveito esta query para guardar bolean
                        NovaEncomenda = true;
                    }
                }
            }
        }

        public override void AntesDeImprimir(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeImprimir(ref Cancel, e);

            if (Module1.VerificaToken("CEC") == 1)
            {
                // ############################################################################################################################################################
                // #### Rui Fernandes (2019/01/14)
                // #### Colocado o seguinte código para que ao imprimir o documento "ECL" e na série interna, verifique se o documento CEC já foi criado e caso não tenha sido,
                // #### o documento "CEC" é criado automaticamente por duplicação e é efetuada a pré-visualização do documento duplicado.
                // #### INICIO
                // ############################################################################################################################################################
                if (this.DocumentoVenda.Tipodoc == "ECL" & BSO.Base.Series.DaValorAtributo("V", this.DocumentoVenda.Tipodoc, this.DocumentoVenda.Serie, "Interna") == true & this.DocumentoVenda.Serie != "RSVS")
                {
                    // Verifica se o documento está anulado, e só duplica se o mesmo não foi anulado
                    if (this.DocumentoVenda.Anulado == false)
                        DuplicaDoc(true);

                    Cancel = true;
                }
            }
        }

        public override void TeclaPressionada(int KeyCode, int Shift, ExtensibilityEventArgs e)
        {
            base.TeclaPressionada(KeyCode, Shift, e);
            if (Module1.VerificaToken("CEC") == 1)
            {
                // ############################################################################################################################################################
                // #### Rui Fernandes (2019/01/14)
                // #### Colocado o seguinte para que caso execute o comando "CTRL + E", no documento "ECL" e na série interna, verifique se o documento CEC já foi criado e caso não tenha sido,
                // #### o documento "CEC" é criado automaticamente por duplicação e caso o utilizador tenha a configuração de email parametrizada é gerado automaticamente o pdf do documento
                // #### associado nos campos "CDU_TipoDocRastreabilidade", "CDU_SerieDocRastreabilidade" e "CDU_NumDocRastreabilidade" e enviado o respetivo email com as configurações
                // #### existentes no documento destino "CDU_TipoDocRastreabilidade".
                // #### INICIO
                // ############################################################################################################################################################
                if (KeyCode == 69 & Shift == 2 & this.DocumentoVenda.Tipodoc == "ECL" & BSO.Base.Series.DaValorAtributo("V", this.DocumentoVenda.Tipodoc, this.DocumentoVenda.Serie, "Interna") == true & this.DocumentoVenda.Serie != "RSVS")
                {
                    // Verifica se o documento está anulado, e só envia email se o mesmo não foi anulado
                    if (this.DocumentoVenda.Anulado == false)
                    {
                        // Verifica se a configuração do email existe para o utilizador atual
                        if (PSO.PrefUtilStd.EmailServSMTP != "")
                        {
                            string CaminhoFicheiro;
                            string NomeFicheiro;
                            string EmailTo;
                            string EmailCC;
                            string EmailBCC;
                            string EmailAssunto;
                            string EmailMsg;
                            EmailTo = "";
                            EmailCC = "";
                            EmailBCC = "";
                            EmailAssunto = "";
                            EmailMsg = "";

                            // Verifica se o documento foi duplicado com sucesso, e caso tenha sido gera o pdf do mesmo e abre formulário para envio do email
                            if (DuplicaDoc(false) == true)
                            {
                                CaminhoFicheiro = @"C:\Temp\";
                                // Verifica se a pasta existe.
                                if (Directory.Exists(CaminhoFicheiro) == false)
                                {
                                    // caso não exista, cria a pasta
                                    Directory.CreateDirectory(CaminhoFicheiro);
                                }
                                // Acrescenta ao caminho a subpasta da empresa
                                CaminhoFicheiro = CaminhoFicheiro + BSO.Contexto.CodEmp + @"\";
                                // Verifica se a pasta existe.
                                if (Directory.Exists(CaminhoFicheiro) == false)
                                {
                                    // caso não exista, cria a pasta
                                    Directory.CreateDirectory(CaminhoFicheiro);
                                }

                                this.DocumentoVenda.CamposUtil["CDU_TipoDocRastreabilidade"].Valor = BSO.Vendas.Documentos.DaValorAtributo("000", this.DocumentoVenda.Tipodoc, this.DocumentoVenda.Serie, this.DocumentoVenda.NumDoc, "CDU_TipoDocRastreabilidade");
                                this.DocumentoVenda.CamposUtil["CDU_SerieDocRastreabilidade"].Valor = BSO.Vendas.Documentos.DaValorAtributo("000", this.DocumentoVenda.Tipodoc, this.DocumentoVenda.Serie, this.DocumentoVenda.NumDoc, "CDU_SerieDocRastreabilidade");
                                this.DocumentoVenda.CamposUtil["CDU_NumDocRastreabilidade"].Valor = BSO.Vendas.Documentos.DaValorAtributo("000", this.DocumentoVenda.Tipodoc, this.DocumentoVenda.Serie, this.DocumentoVenda.NumDoc, "CDU_NumDocRastreabilidade");
                                NomeFicheiro = this.DocumentoVenda.CamposUtil["CDU_TipoDocRastreabilidade"].Valor + "_" + this.DocumentoVenda.CamposUtil["CDU_SerieDocRastreabilidade"].Valor + "_" + Strings.Format(this.DocumentoVenda.CamposUtil["CDU_NumDocRastreabilidade"].Valor, "00000") + ".pdf";
                                if (BSO.Vendas.TabVendas.DaValorAtributo(this.DocumentoVenda.CamposUtil["CDU_TipoDocRastreabilidade"].Valor.ToString(), "EmailFixo") == true)
                                {
                                    EmailTo = BSO.Vendas.TabVendas.DaValorAtributo(this.DocumentoVenda.CamposUtil["CDU_TipoDocRastreabilidade"].Valor.ToString(), "EmailTo");
                                }
                                else
                                {
                                    EmailTo = DaListaEmailContatoClienteRegra(BSO.Vendas.Documentos.DaValorAtributo("000", this.DocumentoVenda.CamposUtil["CDU_TipoDocRastreabilidade"].Valor.ToString(), this.DocumentoVenda.CamposUtil["CDU_SerieDocRastreabilidade"].Valor.ToString(), int.Parse(this.DocumentoVenda.CamposUtil["CDU_NumDocRastreabilidade"].Valor.ToString()), "TipoEntidade"), BSO.Vendas.Documentos.DaValorAtributo("000", this.DocumentoVenda.CamposUtil["CDU_TipoDocRastreabilidade"].Valor.ToString(), this.DocumentoVenda.CamposUtil["CDU_SerieDocRastreabilidade"].Valor.ToString(), int.Parse(this.DocumentoVenda.CamposUtil["CDU_NumDocRastreabilidade"].Valor.ToString()), "Entidade"), BSO.Vendas.TabVendas.DaValorAtributo(this.DocumentoVenda.CamposUtil["CDU_TipoDocRastreabilidade"].Valor.ToString(), "EmailTo"));
                                }

                                EmailCC = BSO.Vendas.TabVendas.DaValorAtributo(this.DocumentoVenda.CamposUtil["CDU_TipoDocRastreabilidade"].Valor.ToString(), "EMailCC");
                                EmailBCC = BSO.Vendas.TabVendas.DaValorAtributo(this.DocumentoVenda.CamposUtil["CDU_TipoDocRastreabilidade"].Valor.ToString(), "EMailBCC");
                                EmailAssunto = this.DocumentoVenda.Tipodoc + " " + this.DocumentoVenda.NumDoc + "/" + this.DocumentoVenda.Serie;
                                EmailMsg = BSO.Vendas.TabVendas.DaValorAtributo(this.DocumentoVenda.CamposUtil["CDU_TipoDocRastreabilidade"].Valor.ToString(), "EMailTexto");
                                ImprimeEnc(this.DocumentoVenda.CamposUtil["CDU_TipoDocRastreabilidade"].Valor.ToString(), this.DocumentoVenda.CamposUtil["CDU_SerieDocRastreabilidade"].Valor.ToString(), int.Parse(this.DocumentoVenda.CamposUtil["CDU_NumDocRastreabilidade"].Valor.ToString()), BSO.Vendas.TabVendas.DaValorAtributo(this.DocumentoVenda.CamposUtil["CDU_TipoDocRastreabilidade"].Valor.ToString(), "Descricao") + "Nº " + this.DocumentoVenda.CamposUtil["CDU_NumDocRastreabilidade"].Valor + " (" + BSO.Base.Series.DaValorAtributo("V", this.DocumentoVenda.CamposUtil["CDU_TipoDocRastreabilidade"].Valor.ToString(), this.DocumentoVenda.CamposUtil["CDU_SerieDocRastreabilidade"].Valor.ToString(), "DescricaoVia01") + ")", BSO.Base.Series.DaValorAtributo("V", this.DocumentoVenda.CamposUtil["CDU_TipoDocRastreabilidade"].Valor.ToString(), this.DocumentoVenda.CamposUtil["CDU_SerieDocRastreabilidade"].Valor.ToString(), "Config"), false, 1, CaminhoFicheiro);

                                // JFC 31/05/2019 Ficha de Acabamentos
                                bool seacellMT, seacellLT, cell, sensitive;
                                seacellMT = false;
                                seacellLT = false;
                                cell = false;
                                sensitive = false;
                                for (int i = 1, loopTo = this.DocumentoVenda.Linhas.NumItens; i <= loopTo; i++)
                                {
                                    if (this.DocumentoVenda.Linhas.GetEdita(i).Descricao.Contains("Seacell"))
                                    {
                                        string seaStr;
                                        StdBELista seaList;
                                        seaStr = "select ac.PRD_Componente as 'Componente' from priinovafil.dbo.VIM_ArtigoComponentes ac where ac.PRD_Artigo='" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "'";
                                        seaList = BSO.Consulta(seaStr);
                                        seaList.Inicio();
                                        long j;
                                        var loopTo1 = seaList.NumLinhas();
                                        for (j = 1L; j <= loopTo1; j++)
                                        {
                                            if (seaList.Valor("Componente") == "SEAMD")
                                            {
                                                seacellMT = true;
                                            }

                                            if (seaList.Valor("Componente") == "SEALY")
                                            {
                                                seacellLT = true;
                                            }

                                            seaList.Seguinte();
                                        }
                                    }

                                    if (this.DocumentoVenda.Linhas.GetEdita(i).Descricao.Contains("Sensitive"))
                                    {
                                        sensitive = true;
                                    }

                                    if (this.DocumentoVenda.Linhas.GetEdita(i).Descricao.Contains("Protection"))
                                    {
                                        cell = true;
                                    }

                                    if (this.DocumentoVenda.Linhas.GetEdita(i).Descricao.Contains("Clima"))
                                    {
                                        cell = true;
                                    }

                                    if (this.DocumentoVenda.Linhas.GetEdita(i).Descricao.Contains("Skin Care"))
                                    {
                                        cell = true;
                                    }
                                }

                                string Anexos;
                                Anexos = CaminhoFicheiro + NomeFicheiro;
                                if (cell)
                                {
                                    Anexos = Anexos + @";\\192.168.1.6\primavera\FichasAcabamentos\CellSolution.pdf";
                                }

                                if (sensitive)
                                {
                                    Anexos = Anexos + @";\\192.168.1.6\primavera\FichasAcabamentos\Sensitive.pdf";
                                }

                                if (seacellMT)
                                {
                                    Anexos = Anexos + @";\\192.168.1.6\primavera\FichasAcabamentos\SeaCellMT.pdf";
                                }

                                if (seacellLT)
                                {
                                    Anexos = Anexos + @";\\192.168.1.6\primavera\FichasAcabamentos\SeaCellLT.pdf";
                                }

                                EnviaEmail(EmailTo, EmailCC, EmailBCC, EmailAssunto, EmailMsg, CaminhoFicheiro + NomeFicheiro);

                                // Depois de fechar o formulário de enviar o email elimina o ficheiro pdf
                                File.Delete(CaminhoFicheiro + NomeFicheiro);
                            }
                        }
                        else
                        {
                            PSO.Dialogos.MostraMensagem(TipoMsg.PRI_Detalhe, Constants.vbNewLine + "A configuração do email para o utilizador '" + BSO.Contexto.UtilizadorActual + "' não existe.", IconId.PRI_Informativo, "Deve configurar o perfil do email para o utilizador '" + BSO.Contexto.UtilizadorActual + "' no menu 'Preferências => Sistema => Email => Microsoft Outlook: Perfil'.", bActivaDetalhe: true);
                        }
                    }
                }
                // ############################################################################################################################################################
                // #### Rui Fernandes (2019/01/14)
                // #### Colocado o seguinte para que caso execute o comando "CTRL + E", no documento "ECL" e na série interna, verifique se o documento CEC já foi criado e caso não tenha sido,
                // #### o documento "CEC" é criado automaticamente por duplicação e caso o utilizador tenha a configuração de email parametrizada é gerado automaticamente o pdf do documento
                // #### associado nos campos "CDU_TipoDocRastreabilidade", "CDU_SerieDocRastreabilidade" e "CDU_NumDocRastreabilidade" e enviado o respetivo email com as configurações
                // #### existentes no documento destino "CDU_TipoDocRastreabilidade".
                // #### FIM
                // ############################################################################################################################################################
            }
        }

        public string DaListaEmailContatoClienteRegra(string pTipoEntidade, string pEntidade, string pTipoContato)
        {
            StdBELista ListaRegraContatoEmail;
            string SqlListaRegraContatoEmail;

            SqlListaRegraContatoEmail = "SELECT COALESCE(CASE Dados.Email WHEN '' THEN '' ELSE LEFT(Dados.Email, LEN(RTRIM(Dados.Email))-1) END, '') Email "
                                        + "FROM ( "
                                        + "SELECT COALESCE(( "
                                        + "SELECT CONCAT(LCE.Email, '; ') "
                                        + "FROM "
                                        + "Contactos CT "
                                        + "INNER JOIN LinhasContactoEntidades LCE ON LCE.IDContacto = CT.Id "
                                        + "WHERE "
                                        + "LCE.TipoEntidade = '" + pTipoEntidade + "' "
                                        + "AND LCE.Entidade = '" + pEntidade + "' "
                                        + "AND LCE.TipoContacto = '" + pTipoContato + "' "
                                        + "ORDER BY "
                                        + "CT.Contacto "
                                        + "FOR XML PATH(''), TYPE).value('.[1]', 'VARCHAR(MAX)'), '') Email "
                                        + ") Dados";

            ListaRegraContatoEmail = BSO.Consulta(SqlListaRegraContatoEmail);
            if (ListaRegraContatoEmail.Vazia() == false)
                return ListaRegraContatoEmail.Valor("Email");
            else
                return "";
        }

        private void EnviaEmail(string v_To, string v_CC, string v_BCC, string v_Assunto, string v_Mensagem, string v_Anexos)
        {
            string strTo;
            string strCC;
            string strBCC;
            string strSubject;
            string strMessage;
            string strPerfilOutlook;

            try
            {
                strTo = v_To;
                strCC = v_CC;
                strBCC = v_BCC;
                strSubject = v_Assunto;
                strMessage = v_Mensagem;
                strPerfilOutlook = PSO.PrefUtilStd.EmailMAPIProfile;
                PSO.Mail.CleanDestinatarios();
                PSO.Mail.CleanCC();
                PSO.Mail.CleanBCC();
                PSO.Mail.CleanFicheirosAnexados();

                PSO.Mail.EnviaMail(Perfil: strPerfilOutlook, ParaQuem: strTo, CC: strCC, BCC: strBCC, Assunto: strSubject, Mensagem: strMessage, AttachFich: v_Anexos, MostraJanela: true, useSMTP: false);
            }
            catch
            {
                PSO.Dialogos.MostraMensagem(TipoMsg.PRI_Detalhe, "Erro ao enviar a mensagem.", IconId.PRI_Informativo, bActivaDetalhe: false);
            }
        }

        public bool DuplicaDoc(bool p_ImprimeDoc)
        {
            string TipodocDestino;
            string SerieDocDestino;
            int NumDocDestino;
            VndBEDocumentoVenda DocVenda_AtualizaCDU = new VndBEDocumentoVenda();

            TipodocDestino = "CEC";
            SerieDocDestino = DateAndTime.Year(DateTime.Now).ToString();

            if (BSO.Vendas.TabVendas.Existe(TipodocDestino) == false)
            {
                PSO.Dialogos.MostraMensagem(TipoMsg.PRI_Detalhe, Constants.vbNewLine + "O documento '" + TipodocDestino + "' não existe.", IconId.PRI_Informativo, "Deve criar/configurar o documento '" + TipodocDestino + "' para que funcionalidade de duplicação do documento funcione corretamente.", bActivaDetalhe: true);
                return false;
            }
            else if (BSO.Base.Series.Existe("V", TipodocDestino, SerieDocDestino) == false)
            {
                PSO.Dialogos.MostraMensagem(TipoMsg.PRI_Detalhe, Constants.vbNewLine + "A Série '" + SerieDocDestino + "' para o documento '" + TipodocDestino + "' não existe.", IconId.PRI_Informativo, "Deve criar/configurar a série '" + SerieDocDestino + "' para o documento '" + TipodocDestino + "' para que funcionalidade de duplicação do documento funcione corretamente.", bActivaDetalhe: true);
                return false;
            }

            if (this.DocumentoVenda.CamposUtil["CDU_NumDocRastreabilidade"].Valor.ToString() == "0")
            {
                NumDocDestino = Convert.ToInt32(CriaConfirmacaoEncomenda(TipodocDestino, SerieDocDestino));

                if (NumDocDestino > 0)
                {
                    DocVenda_AtualizaCDU = BSO.Vendas.Documentos.Edita("000", this.DocumentoVenda.Tipodoc, this.DocumentoVenda.Serie, this.DocumentoVenda.NumDoc);
                    DocVenda_AtualizaCDU.CamposUtil["CDU_TipoDocRastreabilidade"].Valor = TipodocDestino;
                    DocVenda_AtualizaCDU.CamposUtil["CDU_SerieDocRastreabilidade"].Valor = SerieDocDestino;
                    DocVenda_AtualizaCDU.CamposUtil["CDU_NumDocRastreabilidade"].Valor = NumDocDestino;
                    // Verifica se pode gravar o documento (por exemplo se o mesmo já foi transformado não pode ser regravado)
                    string vazia = string.Empty;
                    if (BSO.Vendas.Documentos.ValidaActualizacao(DocVenda_AtualizaCDU, BSO.Vendas.TabVendas.Edita(TipodocDestino), ref vazia, ref vazia) == true)
                        BSO.Vendas.Documentos.Actualiza(DocVenda_AtualizaCDU);
                    else
                        BSO.DSO.ExecuteSQL("UPDATE CabecDoc SET CDU_TipoDocRastreabilidade = '" + TipodocDestino + "', CDU_SerieDocRastreabilidade = '" + SerieDocDestino + "', CDU_NumDocRastreabilidade = " + NumDocDestino + " WHERE Id = '" + DocVenda_AtualizaCDU.ID + "'");
                    DocVenda_AtualizaCDU = null/* TODO Change to default(_) if this is not a reference type */;
                }
            }
            else
            {
                NumDocDestino = int.Parse(this.DocumentoVenda.CamposUtil["CDU_NumDocRastreabilidade"].Valor.ToString());
                SerieDocDestino = this.DocumentoVenda.CamposUtil["CDU_SerieDocRastreabilidade"].Valor.ToString();
            }

            if (p_ImprimeDoc == true)
            {
                ImprimeEnc(TipodocDestino, SerieDocDestino, NumDocDestino, BSO.Vendas.TabVendas.DaValorAtributo(TipodocDestino, "Descricao") + "Nº " + NumDocDestino + " (" + BSO.Base.Series.DaValorAtributo("V", TipodocDestino, SerieDocDestino, "DescricaoVia01") + ")", BSO.Base.Series.DaValorAtributo("V", TipodocDestino, SerieDocDestino, "Config"), false, 1, "");
                string ds;
                DateTime dd;

                // Atualiza o documento como impresso e a data em que o mesmo foi impresso
                // BSO.DSO.BDAPL.Execute "UPDATE CabecDocStatus SET DocImp = 1, DataImp = '" & Format(Now, "yyyy-MM-dd HH:mm:ss.ms") & "' WHERE IdCabecDoc = '" & Me.DocumentoVenda.Id & "'"
                // Comentado por JFC. Estava a dar erro do Format(Now), erro na conversão de string para date. Substituido por getdate().
                BSO.DSO.ExecuteSQL("UPDATE CabecDocStatus SET DocImp = 1, DataImp = getdate() WHERE IdCabecDoc = '" + this.DocumentoVenda.ID + "'");
            }

            return true;
        }

        public long CriaConfirmacaoEncomenda(string p_TipoDocDestindo, string p_SerieDocDestindo)
        {
            VndBEDocumentoVenda DocVenda_Origem = new VndBEDocumentoVenda();
            VndBEDocumentoVenda DocVenda_Destino = new VndBEDocumentoVenda();
            int lDest;
            DocVenda_Origem = BSO.Vendas.Documentos.Edita("000", this.DocumentoVenda.Tipodoc, this.DocumentoVenda.Serie, this.DocumentoVenda.NumDoc);
            DocVenda_Destino = (VndBEDocumentoVenda)BSO.DSO.Plat.FuncoesGlobais.ClonaObjecto(DocVenda_Origem);
            DocVenda_Destino.EmModoEdicao = false;
            DocVenda_Destino.Utilizador = BSO.Contexto.UtilizadorActual;
            DocVenda_Destino.DataHoraCarga = DateTime.Parse(Strings.Format(DateTime.Now, "yyyy-MM-dd"));
            // DataDoc adicionado por JFC 28/03/2019
            DocVenda_Destino.DataDoc = DateTime.Parse(Strings.Format(DateTime.Now, "yyyy-MM-dd"));

            DocVenda_Destino.Tipodoc = p_TipoDocDestindo;
            DocVenda_Destino.Serie = p_SerieDocDestindo;
            DocVenda_Destino.NumDoc = 0;
            DocVenda_Destino.ID = "";
            DocVenda_Destino.IDCabecMovCbl = "";
            DocVenda_Destino.IdDocOrigem = "";
            DocVenda_Destino.ModuloOrigem = "";
            DocVenda_Destino.IDEstorno = "";
            DocVenda_Destino.CamposUtil["CDU_TipoDocRastreabilidade"].Valor = DocVenda_Origem.Tipodoc;
            DocVenda_Destino.CamposUtil["CDU_SerieDocRastreabilidade"].Valor = DocVenda_Origem.Serie;
            DocVenda_Destino.CamposUtil["CDU_NumDocRastreabilidade"].Valor = DocVenda_Origem.NumDoc;

            // Campos de utilizador de controlo para cópia de documentos entre empresas, tem que ser limpo, senão dáva erro ao registar rastreabilidade de chaves duplicadas
            DocVenda_Destino.CamposUtil["CDU_DocumentoCompraDestino"].Valor = "";
            DocVenda_Destino.CamposUtil["CDU_DocumentoVendaDestino"].Valor = "";
            DocVenda_Destino.CamposUtil["CDU_DocumentoOrigem"].Valor = "";

            for (lDest = 1; lDest <= DocVenda_Destino.Linhas.NumItens; lDest++)
            {
                DocVenda_Destino.Linhas.GetEdita(lDest).IdLinha = "";
                DocVenda_Destino.Linhas.GetEdita(lDest).IdLinhaEstorno = "";
                DocVenda_Destino.Linhas.GetEdita(lDest).IdLinhaOrigemCopia = "";
                DocVenda_Destino.Linhas.GetEdita(lDest).IDLinhaOriginal = "";
                // Campos de utilizador de controlo para cópia de documentos entre empresas, tem que ser limpo, senão dáva erro ao registar rastreabilidade de chaves duplicadas
                DocVenda_Destino.Linhas.GetEdita(lDest).CamposUtil["CDU_IDLinhaOriginalGrupo"].Valor = "";
            }

            BSO.Vendas.Documentos.Actualiza(DocVenda_Destino);

            return DocVenda_Destino.NumDoc;

            DocVenda_Origem = null/* TODO Change to default(_) if this is not a reference type */;
            DocVenda_Destino = null/* TODO Change to default(_) if this is not a reference type */;
        }

        public void ImprimeEnc(string v_TipoDoc, string v_Serie, int v_NumDoc, string v_TituloMapa, string v_NomeReport, bool v_Encomenda, int v_NumCopias, string v_CaminhoFicheiro)
        {
            string NomeFicheiro;

            NomeFicheiro = v_TipoDoc + "_" + v_Serie + "_" + Strings.Format(v_NumDoc, "00000") + ".pdf";

            PSO.Mapas.Inicializar("VND");

            // Se o caminho do ficheiro está definido então vai ser gerado o pdf
            if (v_CaminhoFicheiro != "")
            {
                PSO.Mapas.Destino = CRPEExportDestino.edFicheiro;
                PSO.Mapas.SetFileProp(CRPEExportFormat.efPdf, v_CaminhoFicheiro + NomeFicheiro);
            }

            PSO.Mapas.AddFormula("Nome", "'" + BSO.Contexto.IDNome + "'");
            PSO.Mapas.AddFormula("Contribuinte", "'" + "Contribuinte N.º: " + BSO.Contexto.IFNIF + "'");
            if (BSO.Contexto.IDNumPorta + "" != "")
                PSO.Mapas.AddFormula("Morada", "'" + BSO.Contexto.IDMorada + ", " + BSO.Contexto.IDNumPorta + "'");
            else
                PSO.Mapas.AddFormula("Morada", "'" + BSO.Contexto.IDMorada + "'");
            PSO.Mapas.AddFormula("Localidade", "'" + BSO.Contexto.IDLocalidade + "'");
            PSO.Mapas.AddFormula("CodPostal", "'" + BSO.Contexto.IDCodPostal + " " + BSO.Contexto.IDCodPostalLocal + "'");
            PSO.Mapas.AddFormula("Telefone", "'" + "Telef. " + BSO.Contexto.IDIndicativoTelefone + "  " + BSO.Contexto.IDTelefone + "  Fax. " + BSO.Contexto.IDIndicativoFax + "  " + BSO.Contexto.IDFax + "'");

            PSO.Mapas.AddFormula("CapitalSocial", "'" + "Capital Social  " + Strings.Format(BSO.Contexto.ICCapitalSocial, "#,###.00") + " " + BSO.Contexto.ICMoedaCapSocial + "'");
            PSO.Mapas.AddFormula("Conservatoria", "'" + "Cons. Reg. Com. " + BSO.Contexto.ICConservatoria + "'");
            PSO.Mapas.AddFormula("Matricula", "'" + "Matricula N.º " + BSO.Contexto.ICMatricula + "'");
            PSO.Mapas.AddFormula("EMailEmpresa", "'" + BSO.Contexto.IDEmail + "'");

            PSO.Mapas.AddFormula("WebEmpresa", "'" + BSO.Contexto.IDWeb + "'");

            PSO.Mapas.AddFormula("NumVia", "'" + BSO.Base.Series.Edita("V", v_TipoDoc, v_Serie).DescricaoVia01 + "'");

            if (v_Encomenda == true)
                PSO.Mapas.AddFormula("lbl_Text23", "'" + BSO.Vendas.Documentos.DevolveTextoAssinaturaDoc(v_TipoDoc, v_Serie, v_NumDoc, "000") + "'");
            else
                PSO.Mapas.AddFormula("lbl_Text22", "'" + BSO.Vendas.Documentos.DevolveTextoAssinaturaDoc(v_TipoDoc, v_Serie, v_NumDoc, "000") + "'");

            PSO.Mapas.AddFormula("NomeLicenca", "''");

            PSO.Mapas.AddFormula("Documento", "'" + BSO.Vendas.TabVendas.DaValorAtributo(v_TipoDoc, "Descricao") + " " + v_TipoDoc + " " + v_Serie + "/" + Strings.Format(v_NumDoc, "0") + "'");

            PSO.Mapas.SelectionFormula = "{CabecDoc.Filial} = '000' AND {CabecDoc.TipoDoc} = '" + v_TipoDoc + "' AND {CabecDoc.Serie} = '" + v_Serie + "' AND {CabecDoc.NumDoc} = " + v_NumDoc + "";

            // Se o caminho do ficheiro está definido então imprime o mapa, sem pré-visualizar o documento, pois apenas vai gerar pdf
            if (v_CaminhoFicheiro != "")
                PSO.Mapas.ImprimeListagem(v_NomeReport, v_TituloMapa, "P", v_NumCopias, bMapaSistema: false);
            else
                PSO.Mapas.ImprimeListagem(v_NomeReport, v_TituloMapa, "W", v_NumCopias, bMapaSistema: false);
        }
    }
}