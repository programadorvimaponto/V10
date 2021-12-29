'Imports System
'Imports System.Collections.Generic
'Imports System.Linq
'Imports System.Text
'Imports System.Threading.Tasks
'Imports Primavera.Extensibility.Sales.Editors
'Imports Primavera.Extensibility.BusinessEntities
'Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgsPublic
'Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
'Imports StdBE100
'Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
'Imports StdPlatBS100.StdBSTipos
'Imports Primavera.Extensibility.Constants.ExtensibilityService
'Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService

'Namespace CertificadosOrg


'    Public Class VndNsEditorVendas
'        Inherits EditorVendas

'        Public Overrides Sub ArtigoIdentificado(Artigo As String, NumLinha As Integer, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
'            MyBase.ArtigoIdentificado(Artigo, NumLinha, Cancel, e)

'            If Module1.VerificaToken("CertificadosOrg") = 1 Then

'                'Ao identificar o Artigo, caso o mesmo tenha GOTS, OCS ou GRS na descrição, identifica a linha para emissão de certificado. JFC 22/07/2019
'                If (UCase(BSO.Base.Artigos.DaValorAtributo(Artigo, "Descricao")) Like "*RAMA*" And UCase(BSO.Base.Artigos.DaValorAtributo(Artigo, "Descricao")) Like "*ORG*") Or UCase(BSO.Base.Artigos.DaValorAtributo(Artigo, "Descricao") & " " & BSO.Base.Artigos.DaValorAtributo(Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtraExterna")) Like "*GOTS*" Or UCase(BSO.Base.Artigos.DaValorAtributo(Artigo, "Descricao") & " " & BSO.Base.Artigos.DaValorAtributo(Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtraExterna")) Like "*GRS*" Or UCase(BSO.Base.Artigos.DaValorAtributo(Artigo, "Descricao") & " " & BSO.Base.Artigos.DaValorAtributo(Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtraExterna")) Like "*OCS*" Then
'                    Me.DocumentoVenda.Linhas.GetEdita(NumLinha).CamposUtil("CDU_EmitirCertificado").Valor = True
'                End If
'            End If

'        End Sub

'        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
'            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

'            If Module1.VerificaToken("CertificadosOrg") = 1 Then

'                '####################################################################################################################################
'                '#Recalculo de saldos de certificados disponiveis JFC - 22/07/2019                                                                  #
'                '####################################################################################################################################

'                Dim RecalculaCerts As Boolean
'                RecalculaCerts = False
'                For i = 1 To Me.DocumentoVenda.Linhas.NumItens
'                    If Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_NumCertificadoTrans").Valor & "" <> "" Or Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_NumCertificadoTrans2").Valor & "" <> "" Or Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_NumCertificadoTrans3").Valor & "" <> "" Or Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_EmitirCertificado").Valor Then

'                        RecalculaCerts = True

'                    End If
'                Next i

'                If RecalculaCerts Then
'                    BSO.DSO.ExecuteSQL("exec [dbo].[spInserirCert]")
'                End If


'                '####################################################################################################################################
'                '#Recalculo de saldos de certificados disponiveis JFC - 22/07/2019                                                                  #
'                '####################################################################################################################################

'                If Me.DocumentoVenda.Tipodoc = "ECL" Or Me.DocumentoVenda.Tipodoc = "GR" Or BSO.Vendas.TabVendas.Edita(Me.DocumentoVenda.Tipodoc).TipoDocumento = 4 Then
'                    RecalculaCerts = False
'                    'Dim CertLinha As StdBELista
'                    For i = 1 To Me.DocumentoVenda.Linhas.NumItens
'                        If Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_EmitirCertificado").Valor Then
'                            'Set CertLinha = Aplicacao.BSO.Consulta("select * from cabecdoc cd inner join linhasdoc ln on ln.idcabecdoc=cd.id where cd.tipodoc='ECL' and ln.Id='" & Me.DocumentoVenda.Linhas(i).IdLinha & "'")

'                            'CertLinha.Inicio
'                            ' If CertLinha.Vazia Then
'                            RecalculaCerts = True
'                            'End If
'                        End If

'                    Next i
'                End If
'                If BSO.Vendas.TabVendas.Edita(Me.DocumentoVenda.Tipodoc).TipoDocumento = 4 Then

'                    If RecalculaCerts Then
'                        EnviaEmailCertificado()
'                    End If
'                End If

'                NotaCreditoComCertificado()

'            End If
'        End Sub

'        Public Overrides Sub DepoisDeTransformar(e As ExtensibilityEventArgs)
'            MyBase.DepoisDeTransformar(e)
'            If Module1.VerificaToken("CertificadosOrg") = 1 Then

'                NotaCreditoComCertificado()

'            End If

'        End Sub




'        Public Overrides Sub TeclaPressionada(KeyCode As Integer, Shift As Integer, e As ExtensibilityEventArgs)
'            MyBase.TeclaPressionada(KeyCode, Shift, e)

'            If Module1.VerificaToken("CertificadosOrg") = 1 Then

'                '#################################################################################################
'                '# Inserir Certificados de Transação, Formulário FrmAlteraCertificadoTransacao2 (JFC 13/05/2019) #
'                '#################################################################################################
'                'Crtl+F- AlteraCertificadoTransacao
'                If Me.LinhaActual > 0 Then
'                    If KeyCode = 70 Then

'                        Module1.certArtigo = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).Artigo
'                        Module1.certDocumento = Me.DocumentoVenda.Tipodoc & " " & Me.DocumentoVenda.NumDoc & "/" & Me.DocumentoVenda.Serie
'                        Module1.certLote = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).Lote
'                        Module1.certArmazem = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).Armazem
'                        Module1.certCertificadoTransacao = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).CamposUtil("CDU_NumCertificadoTrans").Valor
'                        Module1.certCertificadoTransacao2 = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).CamposUtil("CDU_NumCertificadoTrans2").Valor
'                        Module1.certCertificadoTransacao3 = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).CamposUtil("CDU_NumCertificadoTrans3").Valor
'                        Module1.certQtdTransacao = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).CamposUtil("CDU_QtdCertificadoTrans").Valor
'                        Module1.certQtdTransacao2 = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).CamposUtil("CDU_QtdCertificadoTrans2").Valor
'                        Module1.certQtdTransacao3 = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).CamposUtil("CDU_QtdCertificadoTrans3").Valor
'                        Module1.certIDlinha = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).IdLinha
'                        Module1.certEmitido = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).CamposUtil("CDU_CertificadoEmitido").Valor
'                        Module1.certBCIEmitido = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).CamposUtil("CDU_BCIEmitido").Valor
'                        Module1.certEmitir = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).CamposUtil("CDU_EmitirCertificado").Valor
'                        Module1.certDescricao = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).Descricao
'                        Module1.certObs = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).CamposUtil("CDU_ObsCertificadoTrans").Valor


'                        Dim result As ExtensibilityResult = PriV100Api.BSO.Extensibility.CreateCustomFormInstance(GetType(FrmAlteraCertificadoTransacao2View))

'                        If result.ResultCode = ExtensibilityResultCode.Ok Then

'                            Dim frm As FrmAlteraCertificadoTransacao2View = result.Result
'                            frm.ShowDialog()

'                        End If

'                    End If
'                End If
'                '#################################################################################################
'                '# Inserir Certificados de Transação, Formulário FrmAlteraCertificadoTransacao2 (JFC 13/05/2019) #
'                '#################################################################################################
'            End If
'        End Sub

'        Private Function NotaCreditoComCertificado()

'            '################################################################################################################
'            '##Envia e-mail caso uma nota de credito contenha um artigo certificado. Pedido de Ana Castro. JFC - 03/04/2020##
'            '################################################################################################################
'            If Left(Me.DocumentoVenda.Tipodoc, 2) = "NC" Then
'                For i = 0 To Me.DocumentoVenda.Linhas.NumItens
'                    If Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "" <> "" Then
'                        If UCase(Me.DocumentoVenda.Linhas.GetEdita(i).Descricao) Like "*BCI*" Or UCase(Me.DocumentoVenda.Linhas.GetEdita(i).Descricao) Like "*OCS*" Or UCase(Me.DocumentoVenda.Linhas.GetEdita(i).Descricao) Like "*GRS*" Or UCase(Me.DocumentoVenda.Linhas.GetEdita(i).Descricao) Like "*GOTS*" Then
'                            EnviaEmailCertificadoNC()
'                            Exit For
'                        End If
'                    End If
'                Next

'            End If
'            '################################################################################################################
'            '##Envia e-mail caso uma nota de credito contenha um artigo certificado. Pedido de Ana Castro. JFC - 03/04/2020##
'            '################################################################################################################


'        End Function

'        '*******************************************************************************************************************************************
'        '#### Enviar Mail para Qualidade para emissão de Certificados GOTS, OCS e GRS - 22/07/2019(JFC)                                         ####
'        '*******************************************************************************************************************************************

'        Dim VarCliente As String
'        Dim VarFrom As String
'        Dim VarTo As String
'        Dim VarAssunto As String
'        Dim VarTextoInicialMsg As String
'        Dim VarMensagem As String
'        Dim VarArmazem As String
'        Dim VarLinhas As String
'        Dim VarUtilizador As String
'        Dim VarLocalTeste As Integer '0 - ao seleccionar o cliente; 1 - antes de gravar o documento(ECL ou GR)
'        Dim VarCancelaDoc As Boolean
'        Dim VarNetTrans As Boolean
'        Private Function EnviaEmailCertificado()
'            VarCliente = Me.DocumentoVenda.Entidade
'            Dim ln As Long
'            VarFrom = ""

'            VarTo = "informatica@mundifios.pt; certificados@mundifios.pt;"

'            If Format(Now, "HH:mm") >= "07:00" And Format(Now, "HH:mm") <= "12:59" Then
'                VarTextoInicialMsg = "Bom dia,"
'            ElseIf Format(Now, "HH:mm") >= "13:00" And Format(Now, "HH:mm") <= "19:59" Then
'                VarTextoInicialMsg = "Boa tarde,"
'            Else
'                VarTextoInicialMsg = "Boa noite,"
'            End If

'            VarAssunto = "(Cert) Documento: " & Me.DocumentoVenda.Tipodoc & " " & Format(Me.DocumentoVenda.NumDoc, "####") & "/" & Me.DocumentoVenda.Serie & " (" & Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") & ")"

'            VarUtilizador = Aplicacao.Utilizador.Utilizador

'            VarLinhas = ""
'            For ln = 1 To Me.DocumentoVenda.Linhas.NumItens
'                If Me.DocumentoVenda.Linhas.GetEdita(ln).Artigo & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(ln).CamposUtil("CDU_EmitirCertificado").Valor Then
'                    VarLinhas = VarLinhas & "Linha " & ln & ":                         " & Me.DocumentoVenda.Linhas.GetEdita(ln).Artigo & " - Lote:" & Me.DocumentoVenda.Linhas.GetEdita(ln).Lote & " - Desc:" & Me.DocumentoVenda.Linhas.GetEdita(ln).Descricao & " - Quantidade:" & Me.DocumentoVenda.Linhas.GetEdita(ln).Quantidade & Me.DocumentoVenda.Linhas.GetEdita(ln).Unidade & " - Cert:" & Me.DocumentoVenda.Linhas.GetEdita(ln).CamposUtil("CDU_QtdCertificadoTrans").Valor + Me.DocumentoVenda.Linhas.GetEdita(ln).CamposUtil("CDU_QtdCertificadoTrans2").Valor + Me.DocumentoVenda.Linhas.GetEdita(ln).CamposUtil("CDU_QtdCertificadoTrans3").Valor & Me.DocumentoVenda.Linhas.GetEdita(ln).Unidade & Chr(13) & ""
'                End If

'            Next ln


'            VarMensagem = VarTextoInicialMsg & Chr(13) & Chr(13) & Chr(13) & "Foi emitido um Documento no Primavera, por favor valide os Certificados de Transação:" & Chr(13) & Chr(13) & "" _
'                    & "Empresa:                         " & BSO.Contexto.CodEmp & " - " & BSO.Contexto.IDNome & Chr(13) & "" _
'                    & "Utilizador:                      " & VarUtilizador & Chr(13) & Chr(13) & "" _
'                    & "Cliente:                         " & VarCliente & " - " & Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") & Chr(13) & "" _
'                    & "Documento:                       " & Me.DocumentoVenda.Tipodoc & " " & Format(Me.DocumentoVenda.NumDoc, "#,###") & "/" & Me.DocumentoVenda.Serie & Chr(13) & Chr(13) & "" _
'                    & "Local Descarga:                  " & Me.DocumentoVenda.LocalDescarga & Chr(13) & "" _
'                    & "Morada Entrega:                  " & Replace(Me.DocumentoVenda.MoradaEntrega, "'", "") & Chr(13) & Chr(13) & "" _
'                    & VarLinhas & Chr(13) & "" _
'                    & "Cumprimentos"



'            BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL] ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "', '" & VarFrom & "', '" & VarTo & "','','','" & VarAssunto & "','" & VarMensagem & "','',0,'" & VarUtilizador & "' )")



'        End Function
'        '*******************************************************************************************************************************************
'        '#### Enviar Mail para Qualidade para emissão de Certificados GOTS, OCS e GRS - 22/07/2019(JFC)                                         ####
'        '*******************************************************************************************************************************************


'        '*******************************************************************************************************************************************
'        '#### Enviar Mail para Certificados quando existe uma nota de credito - 03/04/2020(JFC)                                                 ####
'        '*******************************************************************************************************************************************

'        Private Function EnviaEmailCertificadoNC()
'            VarCliente = Me.DocumentoVenda.Entidade
'            Dim ln As Long
'            VarFrom = ""

'            VarTo = "informatica@mundifios.pt; certificados@mundifios.pt;"

'            If Format(Now, "HH:mm") >= "07:00" And Format(Now, "HH:mm") <= "12:59" Then
'                VarTextoInicialMsg = "Bom dia,"
'            ElseIf Format(Now, "HH:mm") >= "13:00" And Format(Now, "HH:mm") <= "19:59" Then
'                VarTextoInicialMsg = "Boa tarde,"
'            Else
'                VarTextoInicialMsg = "Boa noite,"
'            End If

'            VarAssunto = "(Cert) Nota de Credito: " & Me.DocumentoVenda.Tipodoc & " " & Format(Me.DocumentoVenda.NumDoc, "####") & "/" & Me.DocumentoVenda.Serie & " (" & Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") & ")"

'            VarUtilizador = Aplicacao.Utilizador.Utilizador

'            VarLinhas = ""
'            For ln = 1 To Me.DocumentoVenda.Linhas.NumItens
'                If Me.DocumentoVenda.Linhas.GetEdita(ln).Artigo & "" <> "" Then
'                    VarLinhas = VarLinhas & "Linha " & ln & ":                         " & Me.DocumentoVenda.Linhas.GetEdita(ln).Artigo & " - Lote:" & Me.DocumentoVenda.Linhas.GetEdita(ln).Lote & " - Desc:" & Me.DocumentoVenda.Linhas.GetEdita(ln).Descricao & " - Quantidade:" & Me.DocumentoVenda.Linhas.GetEdita(ln).Quantidade & Me.DocumentoVenda.Linhas.GetEdita(ln).Unidade & " - Cert:" & Me.DocumentoVenda.Linhas.GetEdita(ln).CamposUtil("CDU_QtdCertificadoTrans").Valor + Me.DocumentoVenda.Linhas.GetEdita(ln).CamposUtil("CDU_QtdCertificadoTrans2").Valor + Me.DocumentoVenda.Linhas.GetEdita(ln).CamposUtil("CDU_QtdCertificadoTrans3").Valor & Me.DocumentoVenda.Linhas.GetEdita(ln).Unidade & Chr(13) & ""
'                End If

'            Next ln


'            VarMensagem = VarTextoInicialMsg & Chr(13) & Chr(13) & Chr(13) & "Foi emitido uma Nota de Credito, por favor valide os Certificados de Transação:" & Chr(13) & Chr(13) & "" _
'                    & "Empresa:                         " & BSO.Contexto.CodEmp & " - " & BSO.Contexto.IDNome & Chr(13) & "" _
'                    & "Utilizador:                      " & VarUtilizador & Chr(13) & Chr(13) & "" _
'                    & "Cliente:                         " & VarCliente & " - " & Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") & Chr(13) & "" _
'                    & "Documento:                       " & Me.DocumentoVenda.Tipodoc & " " & Format(Me.DocumentoVenda.NumDoc, "#,###") & "/" & Me.DocumentoVenda.Serie & Chr(13) & Chr(13) & "" _
'                    & "Local Descarga:                  " & Me.DocumentoVenda.LocalDescarga & Chr(13) & "" _
'                    & "Morada Entrega:                  " & Replace(Me.DocumentoVenda.MoradaEntrega, "'", "") & Chr(13) & Chr(13) & "" _
'                    & VarLinhas & Chr(13) & "" _
'                    & "Cumprimentos"



'            BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL] ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "', '" & VarFrom & "', '" & VarTo & "','','','" & VarAssunto & "','" & VarMensagem & "','',0,'" & VarUtilizador & "' )")


'        End Function
'        '*******************************************************************************************************************************************
'        '#### Enviar Mail para Certificados quando existe uma nota de credito - 03/04/2020(JFC)                                                 ####
'        '*******************************************************************************************************************************************




'    End Class
'End Namespace
