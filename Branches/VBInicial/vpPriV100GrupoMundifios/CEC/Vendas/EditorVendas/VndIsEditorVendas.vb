Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Sales.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgsPublic
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports VndBE100
Imports StdPlatBS100
Imports StdPlatBS100.StdBSTipos
Imports System.IO

Namespace CEC
    Public Class VndIsEditorVendas
        Inherits EditorVendas

        Dim NovaEncomenda As Boolean
        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("CEC") = 1 Then

                '#################################################################################################
                '####### Verifica se documento já exite, senão zera os campos da CEC - JFC 24-01-2019 ############
                '#################################################################################################

                Dim EncomendaExiste As StdBELista

                If (Me.DocumentoVenda.Tipodoc = "ECL") Then


                    EncomendaExiste = BSO.Consulta("SELECT Entidade FROM CabecDoc cd Where cd.TipoDoc='ECL' and cd.Serie=" & "'" & Me.DocumentoVenda.Serie & "' and cd.NumDoc=" & "'" & Me.DocumentoVenda.NumDoc & "'")

                    '10/05/2021 - JFC aproveito esta query para guardar bolean
                    NovaEncomenda = False

                    If (EncomendaExiste.Vazia = True) Then
                        Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor = "0"
                        Me.DocumentoVenda.CamposUtil("CDU_NumDocRastreabilidade").Valor = "0"
                        Me.DocumentoVenda.CamposUtil("CDU_SerieDocRastreabilidade").Valor = "0"
                        '10/05/2021 - JFC aproveito esta query para guardar bolean
                        NovaEncomenda = True

                    End If


                End If





                '#################################################################################################
                '####### Verifica se documento já exite, senão zera os campos da CEC - JFC 24-01-2019 ############
                '#################################################################################################
            End If
        End Sub


        Public Overrides Sub AntesDeImprimir(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeImprimir(Cancel, e)

            If Module1.VerificaToken("CEC") = 1 Then

                '############################################################################################################################################################
                '#### Rui Fernandes (2019/01/14)
                '#### Colocado o seguinte código para que ao imprimir o documento "ECL" e na série interna, verifique se o documento CEC já foi criado e caso não tenha sido,
                '#### o documento "CEC" é criado automaticamente por duplicação e é efetuada a pré-visualização do documento duplicado.
                '#### INICIO
                '############################################################################################################################################################
                If Me.DocumentoVenda.Tipodoc = "ECL" And BSO.Base.Series.DaValorAtributo("V", Me.DocumentoVenda.Tipodoc, Me.DocumentoVenda.Serie, "Interna") = True And Me.DocumentoVenda.Serie <> "RSVS" Then

                    'Verifica se o documento está anulado, e só duplica se o mesmo não foi anulado
                    If Me.DocumentoVenda.Anulado = False Then

                        DuplicaDoc(True)

                    End If

                    Cancel = True

                End If
                '############################################################################################################################################################
                '#### Rui Fernandes (2019/01/14)
                '#### Colocado o seguinte código para que ao imprimir o documento "ECL" e na série interna, verifique se o documento CEC já foi criado e caso não tenha sido,
                '#### o documento "CEC" é criado automaticamente por duplicação e é efetuada a pré-visualização do documento duplicado.
                '#### FIM
                '############################################################################################################################################################
            End If

        End Sub


        Public Overrides Sub TeclaPressionada(KeyCode As Integer, Shift As Integer, e As ExtensibilityEventArgs)
            MyBase.TeclaPressionada(KeyCode, Shift, e)

            If Module1.VerificaToken("CEC") = 1 Then

                '############################################################################################################################################################
                '#### Rui Fernandes (2019/01/14)
                '#### Colocado o seguinte para que caso execute o comando "CTRL + E", no documento "ECL" e na série interna, verifique se o documento CEC já foi criado e caso não tenha sido,
                '#### o documento "CEC" é criado automaticamente por duplicação e caso o utilizador tenha a configuração de email parametrizada é gerado automaticamente o pdf do documento
                '#### associado nos campos "CDU_TipoDocRastreabilidade", "CDU_SerieDocRastreabilidade" e "CDU_NumDocRastreabilidade" e enviado o respetivo email com as configurações
                '#### existentes no documento destino "CDU_TipoDocRastreabilidade".
                '#### INICIO
                '############################################################################################################################################################
                If KeyCode = 69 And Shift = 2 And Me.DocumentoVenda.Tipodoc = "ECL" And BSO.Base.Series.DaValorAtributo("V", Me.DocumentoVenda.Tipodoc, Me.DocumentoVenda.Serie, "Interna") = True And Me.DocumentoVenda.Serie <> "RSVS" Then

                    'Verifica se o documento está anulado, e só envia email se o mesmo não foi anulado
                    If Me.DocumentoVenda.Anulado = False Then

                        'Verifica se a configuração do email existe para o utilizador atual
                        If PSO.PrefUtilStd.EmailServSMTP <> "" Then

                            Dim CaminhoFicheiro As String
                            Dim NomeFicheiro As String
                            Dim EmailTo As String
                            Dim EmailCC As String
                            Dim EmailBCC As String
                            Dim EmailAssunto As String
                            Dim EmailMsg As String

                            EmailTo = ""
                            EmailCC = ""
                            EmailBCC = ""
                            EmailAssunto = ""
                            EmailMsg = ""

                            'Verifica se o documento foi duplicado com sucesso, e caso tenha sido gera o pdf do mesmo e abre formulário para envio do email
                            If DuplicaDoc(False) = True Then

                                CaminhoFicheiro = "C:\Temp\"
                                'Verifica se a pasta existe.
                                If Directory.Exists(CaminhoFicheiro) = False Then
                                    'caso não exista, cria a pasta
                                    Directory.CreateDirectory(CaminhoFicheiro)
                                End If
                                'Acrescenta ao caminho a subpasta da empresa
                                CaminhoFicheiro = CaminhoFicheiro & BSO.Contexto.CodEmp & "\"
                                'Verifica se a pasta existe.
                                If Directory.Exists(CaminhoFicheiro) = False Then
                                    'caso não exista, cria a pasta
                                    Directory.CreateDirectory(CaminhoFicheiro)
                                End If

                                Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor = BSO.Vendas.Documentos.DaValorAtributo("000", Me.DocumentoVenda.Tipodoc, Me.DocumentoVenda.Serie, Me.DocumentoVenda.NumDoc, "CDU_TipoDocRastreabilidade")
                                Me.DocumentoVenda.CamposUtil("CDU_SerieDocRastreabilidade").Valor = BSO.Vendas.Documentos.DaValorAtributo("000", Me.DocumentoVenda.Tipodoc, Me.DocumentoVenda.Serie, Me.DocumentoVenda.NumDoc, "CDU_SerieDocRastreabilidade")
                                Me.DocumentoVenda.CamposUtil("CDU_NumDocRastreabilidade").Valor = BSO.Vendas.Documentos.DaValorAtributo("000", Me.DocumentoVenda.Tipodoc, Me.DocumentoVenda.Serie, Me.DocumentoVenda.NumDoc, "CDU_NumDocRastreabilidade")

                                NomeFicheiro = Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor & "_" & Me.DocumentoVenda.CamposUtil("CDU_SerieDocRastreabilidade").Valor & "_" & Format(Me.DocumentoVenda.CamposUtil("CDU_NumDocRastreabilidade").Valor, "00000") & ".pdf"

                                If BSO.Vendas.TabVendas.DaValorAtributo(Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor, "EmailFixo") = True Then
                                    EmailTo = BSO.Vendas.TabVendas.DaValorAtributo(Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor, "EmailTo")
                                Else
                                    EmailTo = DaListaEmailContatoClienteRegra(BSO.Vendas.Documentos.DaValorAtributo("000", Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor, Me.DocumentoVenda.CamposUtil("CDU_SerieDocRastreabilidade").Valor, Me.DocumentoVenda.CamposUtil("CDU_NumDocRastreabilidade").Valor, "TipoEntidade"), BSO.Vendas.Documentos.DaValorAtributo("000", Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor, Me.DocumentoVenda.CamposUtil("CDU_SerieDocRastreabilidade").Valor, Me.DocumentoVenda.CamposUtil("CDU_NumDocRastreabilidade").Valor, "Entidade"), BSO.Vendas.TabVendas.DaValorAtributo(Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor, "EmailTo"))
                                End If

                                EmailCC = BSO.Vendas.TabVendas.DaValorAtributo(Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor, "EMailCC")
                                EmailBCC = BSO.Vendas.TabVendas.DaValorAtributo(Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor, "EMailBCC")
                                EmailAssunto = Me.DocumentoVenda.Tipodoc & " " & Me.DocumentoVenda.NumDoc & "/" & Me.DocumentoVenda.Serie
                                EmailMsg = BSO.Vendas.TabVendas.DaValorAtributo(Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor, "EMailTexto")

                                ImprimeEnc(Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor, Me.DocumentoVenda.CamposUtil("CDU_SerieDocRastreabilidade").Valor, Me.DocumentoVenda.CamposUtil("CDU_NumDocRastreabilidade").Valor, BSO.Vendas.TabVendas.DaValorAtributo(Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor, "Descricao") & "Nº " & Me.DocumentoVenda.CamposUtil("CDU_NumDocRastreabilidade").Valor & " (" & BSO.Base.Series.DaValorAtributo("V", Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor, Me.DocumentoVenda.CamposUtil("CDU_SerieDocRastreabilidade").Valor, "DescricaoVia01") & ")", BSO.Base.Series.DaValorAtributo("V", Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor, Me.DocumentoVenda.CamposUtil("CDU_SerieDocRastreabilidade").Valor, "Config"), False, 1, CaminhoFicheiro)

                                'JFC 31/05/2019 Ficha de Acabamentos
                                Dim seacellMT, seacellLT, cell, sensitive As Boolean
                                seacellMT = False
                                seacellLT = False
                                cell = False
                                sensitive = False

                                For i = 1 To Me.DocumentoVenda.Linhas.NumItens
                                    If Me.DocumentoVenda.Linhas.GetEdita(i).Descricao Like "*Seacell*" Then
                                        Dim seaStr As String
                                        Dim seaList As StdBELista

                                        seaStr = "select ac.PRD_Componente as 'Componente' from priinovafil.dbo.VIM_ArtigoComponentes ac where ac.PRD_Artigo='" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "'"

                                        seaList = BSO.Consulta(seaStr)
                                        seaList.Inicio()
                                        Dim j As Long
                                        For j = 1 To seaList.NumLinhas
                                            If seaList.Valor("Componente") = "SEAMD" Then
                                                seacellMT = True
                                            End If
                                            If seaList.Valor("Componente") = "SEALY" Then
                                                seacellLT = True
                                            End If
                                            seaList.Seguinte()
                                        Next j


                                    End If
                                    If Me.DocumentoVenda.Linhas.GetEdita(i).Descricao Like "*Sensitive*" Then
                                        sensitive = True

                                    End If
                                    If Me.DocumentoVenda.Linhas.GetEdita(i).Descricao Like "*Protection*" Then
                                        cell = True
                                    End If
                                    If Me.DocumentoVenda.Linhas.GetEdita(i).Descricao Like "*Clima*" Then
                                        cell = True
                                    End If
                                    If Me.DocumentoVenda.Linhas.GetEdita(i).Descricao Like "*Skin Care*" Then
                                        cell = True
                                    End If


                                Next



                                Dim Anexos As String

                                Anexos = CaminhoFicheiro & NomeFicheiro

                                If cell Then
                                    Anexos = Anexos & ";\\192.168.1.6\primavera\FichasAcabamentos\CellSolution.pdf"
                                End If

                                If sensitive Then
                                    Anexos = Anexos & ";\\192.168.1.6\primavera\FichasAcabamentos\Sensitive.pdf"
                                End If

                                If seacellMT Then
                                    Anexos = Anexos & ";\\192.168.1.6\primavera\FichasAcabamentos\SeaCellMT.pdf"
                                End If

                                If seacellLT Then
                                    Anexos = Anexos & ";\\192.168.1.6\primavera\FichasAcabamentos\SeaCellLT.pdf"
                                End If



                                EnviaEmail(EmailTo, EmailCC, EmailBCC, EmailAssunto, EmailMsg, CaminhoFicheiro & NomeFicheiro)

                                'Depois de fechar o formulário de enviar o email elimina o ficheiro pdf
                                File.Delete(CaminhoFicheiro & NomeFicheiro)

                            End If

                        Else

                            PriV100Api.PSO.Dialogos.MostraMensagem(TipoMsg.PRI_Detalhe, vbNewLine & "A configuração do email para o utilizador '" & BSO.Contexto.UtilizadorActual & "' não existe.", IconId.PRI_Informativo, "Deve configurar o perfil do email para o utilizador '" & BSO.Contexto.UtilizadorActual & "' no menu 'Preferências => Sistema => Email => Microsoft Outlook: Perfil'.", "Atenção!", True)
                        End If

                    End If

                End If
                '############################################################################################################################################################
                '#### Rui Fernandes (2019/01/14)
                '#### Colocado o seguinte para que caso execute o comando "CTRL + E", no documento "ECL" e na série interna, verifique se o documento CEC já foi criado e caso não tenha sido,
                '#### o documento "CEC" é criado automaticamente por duplicação e caso o utilizador tenha a configuração de email parametrizada é gerado automaticamente o pdf do documento
                '#### associado nos campos "CDU_TipoDocRastreabilidade", "CDU_SerieDocRastreabilidade" e "CDU_NumDocRastreabilidade" e enviado o respetivo email com as configurações
                '#### existentes no documento destino "CDU_TipoDocRastreabilidade".
                '#### FIM
                '############################################################################################################################################################

            End If

        End Sub

        Function DaListaEmailContatoClienteRegra(ByVal pTipoEntidade As String, ByVal pEntidade As String, ByVal pTipoContato As String) As String

            Dim ListaRegraContatoEmail As StdBELista
            Dim SqlListaRegraContatoEmail As String

            SqlListaRegraContatoEmail = "SELECT COALESCE(CASE Dados.Email WHEN '' THEN '' ELSE LEFT(Dados.Email, LEN(RTRIM(Dados.Email))-1) END, '') Email " _
                                        & "FROM ( " _
                                        & "SELECT COALESCE(( " _
                                        & "SELECT CONCAT(LCE.Email, '; ') " _
                                        & "FROM " _
                                        & "Contactos CT " _
                                        & "INNER JOIN LinhasContactoEntidades LCE ON LCE.IDContacto = CT.Id " _
                                        & "WHERE " _
                                        & "LCE.TipoEntidade = '" & pTipoEntidade & "' " _
                                        & "AND LCE.Entidade = '" & pEntidade & "' " _
                                        & "AND LCE.TipoContacto = '" & pTipoContato & "' " _
                                        & "ORDER BY " _
                                        & "CT.Contacto " _
                                        & "FOR XML PATH(''), TYPE).value('.[1]', 'VARCHAR(MAX)'), '') Email " _
                                        & ") Dados"

            ListaRegraContatoEmail = PriV100Api.BSO.Consulta(SqlListaRegraContatoEmail)
            If ListaRegraContatoEmail.Vazia = False Then
                DaListaEmailContatoClienteRegra = ListaRegraContatoEmail.Valor("Email")
            Else
                DaListaEmailContatoClienteRegra = ""
            End If

        End Function


        Private Sub EnviaEmail(ByVal v_To As String, ByVal v_CC As String, ByVal v_BCC As String, ByVal v_Assunto As String, ByVal v_Mensagem As String, ByVal v_Anexos As String)

            Dim strTo As String
            Dim strCC As String
            Dim strBCC As String
            Dim strSubject As String
            Dim strMessage As String
            Dim strPerfilOutlook As String

            On Error GoTo Erro

            strTo = v_To
            strCC = v_CC
            strBCC = v_BCC
            strSubject = v_Assunto
            strMessage = v_Mensagem
            strPerfilOutlook = PSO.PrefUtilStd.EmailMAPIProfile
            PSO.Mail.CleanDestinatarios()
            PSO.Mail.CleanCC()
            PSO.Mail.CleanBCC()
            PSO.Mail.CleanFicheirosAnexados()

            PSO.Mail.EnviaMail(Perfil:=strPerfilOutlook, ParaQuem:=strTo, CC:=strCC, BCC:=strBCC, Assunto:=strSubject, Mensagem:=strMessage, AttachFich:=v_Anexos, MostraJanela:=True, useSMTP:=False)

            Exit Sub

Erro:

            PSO.Dialogos.MostraMensagem(TipoMsg.PRI_Detalhe, vbNewLine & "Erro ao enviar a mensagem.", IconId.PRI_Informativo, Err.Number & " - " & Err.Description, "Atenção!", True)

        End Sub
        '############################################################################################################################################################
        '#### Rui Fernandes (2019/01/14)
        '#### Funções necessárias para a duplicação de encomendas e envio das mesmas.
        '#### FIM
        '############################################################################################################################################################


        '############################################################################################################################################################
        '#### Rui Fernandes (2019/01/14)
        '#### Funções necessárias para a duplicação de encomendas e envio das mesmas.
        '#### INICIO
        '############################################################################################################################################################
        Function DuplicaDoc(ByVal p_ImprimeDoc As Boolean) As Boolean

            Dim TipodocDestino As String
            Dim SerieDocDestino As String
            Dim NumDocDestino As Long
            Dim DocVenda_AtualizaCDU As New VndBEDocumentoVenda

            TipodocDestino = "CEC"
            SerieDocDestino = Year(Now)

            If BSO.Vendas.TabVendas.Existe(TipodocDestino) = False Then
                PSO.Dialogos.MostraMensagem(TipoMsg.PRI_Detalhe, vbNewLine & "O documento '" & TipodocDestino & "' não existe.", IconId.PRI_Informativo, "Deve criar/configurar o documento '" & TipodocDestino & "' para que funcionalidade de duplicação do documento funcione corretamente.", "Atenção!", True)
                DuplicaDoc = False
                Exit Function
            ElseIf BSO.Base.Series.Existe("V", TipodocDestino, SerieDocDestino) = False Then
                PSO.Dialogos.MostraMensagem(TipoMsg.PRI_Detalhe, vbNewLine & "A Série '" & SerieDocDestino & "' para o documento '" & TipodocDestino & "' não existe.", IconId.PRI_Informativo, "Deve criar/configurar a série '" & SerieDocDestino & "' para o documento '" & TipodocDestino & "' para que funcionalidade de duplicação do documento funcione corretamente.", "Atenção!", True)
                DuplicaDoc = False
                Exit Function
            End If

            If Me.DocumentoVenda.CamposUtil("CDU_NumDocRastreabilidade").Valor = 0 Then

                NumDocDestino = CriaConfirmacaoEncomenda(TipodocDestino, SerieDocDestino)

                If NumDocDestino > 0 Then
                    DocVenda_AtualizaCDU = BSO.Vendas.Documentos.Edita("000", Me.DocumentoVenda.Tipodoc, Me.DocumentoVenda.Serie, Me.DocumentoVenda.NumDoc)
                    DocVenda_AtualizaCDU.CamposUtil("CDU_TipoDocRastreabilidade").Valor = TipodocDestino
                    DocVenda_AtualizaCDU.CamposUtil("CDU_SerieDocRastreabilidade").Valor = SerieDocDestino
                    DocVenda_AtualizaCDU.CamposUtil("CDU_NumDocRastreabilidade").Valor = NumDocDestino
                    'Verifica se pode gravar o documento (por exemplo se o mesmo já foi transformado não pode ser regravado)
                    If BSO.Vendas.Documentos.ValidaActualizacao(DocVenda_AtualizaCDU, BSO.Vendas.TabVendas.Edita(TipodocDestino), "", "") = True Then
                        BSO.Vendas.Documentos.Actualiza(DocVenda_AtualizaCDU)
                        'Caso o documento não possa ser regravado é efetuada a atualização dos campos por update
                    Else
                        BSO.DSO.ExecuteSQL("UPDATE CabecDoc SET CDU_TipoDocRastreabilidade = '" & TipodocDestino & "', CDU_SerieDocRastreabilidade = '" & SerieDocDestino & "', CDU_NumDocRastreabilidade = " & NumDocDestino & " WHERE Id = '" & DocVenda_AtualizaCDU.ID & "'")
                    End If
                    DocVenda_AtualizaCDU = Nothing
                End If

            Else

                NumDocDestino = Me.DocumentoVenda.CamposUtil("CDU_NumDocRastreabilidade").Valor
                SerieDocDestino = Me.DocumentoVenda.CamposUtil("CDU_SerieDocRastreabilidade").Valor
            End If

            If p_ImprimeDoc = True Then

                ImprimeEnc(TipodocDestino, SerieDocDestino, NumDocDestino, BSO.Vendas.TabVendas.DaValorAtributo(TipodocDestino, "Descricao") & "Nº " & NumDocDestino & " (" & BSO.Base.Series.DaValorAtributo("V", TipodocDestino, SerieDocDestino, "DescricaoVia01") & ")", BSO.Base.Series.DaValorAtributo("V", TipodocDestino, SerieDocDestino, "Config"), False, 1, "")
                Dim ds As String
                Dim dd As Date


                'Atualiza o documento como impresso e a data em que o mesmo foi impresso
                'BSO.DSO.BDAPL.Execute "UPDATE CabecDocStatus SET DocImp = 1, DataImp = '" & Format(Now, "yyyy-MM-dd HH:mm:ss.ms") & "' WHERE IdCabecDoc = '" & Me.DocumentoVenda.Id & "'"
                'Comentado por JFC. Estava a dar erro do Format(Now), erro na conversão de string para date. Substituido por getdate().
                BSO.DSO.ExecuteSQL("UPDATE CabecDocStatus SET DocImp = 1, DataImp = getdate() WHERE IdCabecDoc = '" & Me.DocumentoVenda.ID & "'")


            End If

            DuplicaDoc = True

        End Function

        Function CriaConfirmacaoEncomenda(ByVal p_TipoDocDestindo As String, ByVal p_SerieDocDestindo As String) As Long

            Dim DocVenda_Origem As New VndBEDocumentoVenda
            Dim DocVenda_Destino As New VndBEDocumentoVenda
            Dim lDest As Long

            DocVenda_Origem = BSO.Vendas.Documentos.Edita("000", Me.DocumentoVenda.Tipodoc, Me.DocumentoVenda.Serie, Me.DocumentoVenda.NumDoc)
            DocVenda_Destino = BSO.DSO.Plat.FuncoesGlobais.ClonaObjecto(DocVenda_Origem)

            DocVenda_Destino.EmModoEdicao = False
            DocVenda_Destino.Utilizador = BSO.Contexto.UtilizadorActual
            DocVenda_Destino.DataHoraCarga = Format(Now, "yyyy-MM-dd")
            'DataDoc adicionado por JFC 28/03/2019
            DocVenda_Destino.DataDoc = Format(Now, "yyyy-MM-dd")

            DocVenda_Destino.Tipodoc = p_TipoDocDestindo
            DocVenda_Destino.Serie = p_SerieDocDestindo
            DocVenda_Destino.NumDoc = 0
            DocVenda_Destino.ID = ""
            DocVenda_Destino.IDCabecMovCbl = ""
            DocVenda_Destino.IdDocOrigem = ""
            DocVenda_Destino.ModuloOrigem = ""
            DocVenda_Destino.IDEstorno = ""
            DocVenda_Destino.CamposUtil("CDU_TipoDocRastreabilidade").Valor = DocVenda_Origem.Tipodoc
            DocVenda_Destino.CamposUtil("CDU_SerieDocRastreabilidade").Valor = DocVenda_Origem.Serie
            DocVenda_Destino.CamposUtil("CDU_NumDocRastreabilidade").Valor = DocVenda_Origem.NumDoc

            'Campos de utilizador de controlo para cópia de documentos entre empresas, tem que ser limpo, senão dáva erro ao registar rastreabilidade de chaves duplicadas
            DocVenda_Destino.CamposUtil("CDU_DocumentoCompraDestino").Valor = ""
            DocVenda_Destino.CamposUtil("CDU_DocumentoVendaDestino").Valor = ""
            DocVenda_Destino.CamposUtil("CDU_DocumentoOrigem").Valor = ""

            For lDest = 1 To DocVenda_Destino.Linhas.NumItens
                DocVenda_Destino.Linhas.GetEdita(lDest).IdLinha = ""
                DocVenda_Destino.Linhas.GetEdita(lDest).IdLinhaEstorno = ""
                DocVenda_Destino.Linhas.GetEdita(lDest).IdLinhaOrigemCopia = ""
                DocVenda_Destino.Linhas.GetEdita(lDest).IDLinhaOriginal = ""
                'Campos de utilizador de controlo para cópia de documentos entre empresas, tem que ser limpo, senão dáva erro ao registar rastreabilidade de chaves duplicadas
                DocVenda_Destino.Linhas.GetEdita(lDest).CamposUtil("CDU_IDLinhaOriginalGrupo").Valor = ""
            Next lDest

            BSO.Vendas.Documentos.Actualiza(DocVenda_Destino)

            CriaConfirmacaoEncomenda = DocVenda_Destino.NumDoc

            DocVenda_Origem = Nothing
            DocVenda_Destino = Nothing

        End Function


        Sub ImprimeEnc(ByVal v_TipoDoc As String, ByVal v_Serie As String, ByVal v_NumDoc As Long, ByVal v_TituloMapa As String, ByVal v_NomeReport As String, ByVal v_Encomenda As Boolean, ByVal v_NumCopias As Integer, ByVal v_CaminhoFicheiro As String)

            Dim NomeFicheiro As String

            NomeFicheiro = v_TipoDoc & "_" & v_Serie & "_" & Format(v_NumDoc, "00000") & ".pdf"

            PSO.Mapas.Inicializar("VND")

            'Se o caminho do ficheiro está definido então vai ser gerado o pdf
            If v_CaminhoFicheiro <> "" Then
                PSO.Mapas.Destino = CRPEExportDestino.edFicheiro
                PSO.Mapas.SetFileProp(CRPEExportFormat.efPdf, v_CaminhoFicheiro & NomeFicheiro)
            End If

            PSO.Mapas.AddFormula("Nome", "'" + BSO.Contexto.IDNome + "'")
            PSO.Mapas.AddFormula("Contribuinte", "'" + "Contribuinte N.º: " + BSO.Contexto.IFNIF + "'")
            If BSO.Contexto.IDNumPorta & "" <> "" Then
                PSO.Mapas.AddFormula("Morada", "'" + BSO.Contexto.IDMorada + ", " + BSO.Contexto.IDNumPorta + "'")
            Else
                PSO.Mapas.AddFormula("Morada", "'" + BSO.Contexto.IDMorada + "'")
            End If
            PSO.Mapas.AddFormula("Localidade", "'" + BSO.Contexto.IDLocalidade + "'")
            PSO.Mapas.AddFormula("CodPostal", "'" + BSO.Contexto.IDCodPostal & " " & BSO.Contexto.IDCodPostalLocal + "'")
            PSO.Mapas.AddFormula("Telefone", "'" + "Telef. " + BSO.Contexto.IDIndicativoTelefone + "  " + BSO.Contexto.IDTelefone + "  Fax. " + BSO.Contexto.IDIndicativoFax + "  " + BSO.Contexto.IDFax + "'")

            PSO.Mapas.AddFormula("CapitalSocial", "'" + "Capital Social  " + Format(BSO.Contexto.ICCapitalSocial, "#,###.00") + " " + BSO.Contexto.ICMoedaCapSocial + "'")
            PSO.Mapas.AddFormula("Conservatoria", "'" + "Cons. Reg. Com. " + BSO.Contexto.ICConservatoria + "'")
            PSO.Mapas.AddFormula("Matricula", "'" + "Matricula N.º " + BSO.Contexto.ICMatricula + "'")
            PSO.Mapas.AddFormula("EMailEmpresa", "'" + BSO.Contexto.IDEmail + "'")

            PSO.Mapas.AddFormula("WebEmpresa", "'" + BSO.Contexto.IDWeb + "'")

            PSO.Mapas.AddFormula("NumVia", "'" + BSO.Base.Series.Edita("V", v_TipoDoc, v_Serie).DescricaoVia01 + "'")

            If v_Encomenda = True Then
                PSO.Mapas.AddFormula("lbl_Text23", "'" + BSO.Vendas.Documentos.DevolveTextoAssinaturaDoc(v_TipoDoc, v_Serie, v_NumDoc, "000") + "'")
            Else
                PSO.Mapas.AddFormula("lbl_Text22", "'" + BSO.Vendas.Documentos.DevolveTextoAssinaturaDoc(v_TipoDoc, v_Serie, v_NumDoc, "000") + "'")
            End If

            PSO.Mapas.AddFormula("NomeLicenca", "''")

            PSO.Mapas.AddFormula("Documento", "'" + BSO.Vendas.TabVendas.DaValorAtributo(v_TipoDoc, "Descricao") + " " + v_TipoDoc + " " + v_Serie + "/" + Format(v_NumDoc, 0) + "'")

            PSO.Mapas.SelectionFormula = "{CabecDoc.Filial} = '000' AND {CabecDoc.TipoDoc} = '" & v_TipoDoc & "' AND {CabecDoc.Serie} = '" & v_Serie & "' AND {CabecDoc.NumDoc} = " & v_NumDoc & ""

            'Se o caminho do ficheiro está definido então imprime o mapa, sem pré-visualizar o documento, pois apenas vai gerar pdf
            If v_CaminhoFicheiro <> "" Then
                PSO.Mapas.ImprimeListagem(v_NomeReport, v_TituloMapa, "P", v_NumCopias, , , , , False)
                'Se o caminho do ficheiro não está definido então pré-visualiza o documento
            Else
                PSO.Mapas.ImprimeListagem(v_NomeReport, v_TituloMapa, "W", v_NumCopias, , , , , False)
            End If

        End Sub




    End Class
End Namespace