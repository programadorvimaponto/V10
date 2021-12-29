'Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
'Imports Primavera.Extensibility.Purchases.Editors
'Imports Primavera.Extensibility.Attributes
'Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
'Imports StdBE100
'Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService
'Imports Primavera.Extensibility.Constants.ExtensibilityService

'Namespace certificadosorg
'    Public Class cmpnseditorcompras
'        Inherits EditorCompras

'        Public Overrides Sub antesdegravar(ByRef cancel As Boolean, e As ExtensibilityEventArgs)
'            MyBase.AntesDeGravar(cancel, e)

'            If Module1.VerificaToken("certificadosorg") = 1 Then
'                '    '*******************************************************************************************************************************************
'                '    '#### bruno 18/02/2020 #### validar em cnt/ecf tem produtos certificados e se o fornecedor é certificado ####
'                '    '*******************************************************************************************************************************************

'                If Me.DocumentoCompra.Tipodoc = "cnt" Or Me.DocumentoCompra.Tipodoc = "ecf" Then
'                    Dim l As Long
'                    Dim listforncert As StdBELista

'                    listforncert = BSO.Consulta("select *, getdate() as 'hoje' from fornecedores where fornecedor='" & Me.DocumentoCompra.Entidade & "'")
'                    listforncert.Inicio()

'                    For l = 1 To Me.DocumentoCompra.Linhas.NumItens
'                        If Me.DocumentoCompra.Linhas.GetEdita(l).Artigo & "" <> "" Then
'                            If UCase(BSO.Base.Artigos.Edita(Me.DocumentoCompra.Linhas.GetEdita(l).Artigo).CamposUtil("cdu_descricaoextra").Valor) Like "*gots*" Then
'                                If listforncert.Valor("cdu_gots") <> True Then
'                                    MsgBox("atenção:" & Chr(13) & "o fornecedor não está identificado como certificado gots.", vbCritical + vbOKOnly)
'                                ElseIf listforncert.Valor("cdu_gotsdata") < listforncert.Valor("hoje") Then
'                                    MsgBox("atenção:" & Chr(13) & "o fornecedor tem o certificado gots expirado.", vbCritical + vbOKOnly)
'                                End If
'                            End If

'                            If UCase(BSO.Base.Artigos.Edita(Me.DocumentoCompra.Linhas.GetEdita(l).Artigo).CamposUtil("cdu_descricaoextra").Valor) Like "*grs*" Then
'                                If listforncert.Valor("cdu_grs") <> True Then
'                                    MsgBox("atenção:" & Chr(13) & "o fornecedor não está identificado como certificado grs.", vbCritical + vbOKOnly)
'                                ElseIf listforncert.Valor("cdu_grsdata") < listforncert.Valor("hoje") Then
'                                    MsgBox("atenção:" & Chr(13) & "o fornecedor tem o certificado grs expirado.", vbCritical + vbOKOnly)
'                                End If
'                            End If

'                            If UCase(BSO.Base.Artigos.Edita(Me.DocumentoCompra.Linhas.GetEdita(l).Artigo).CamposUtil("cdu_descricaoextra").Valor) Like "*ocs*" Then
'                                If listforncert.Valor("cdu_ocs") <> True Then
'                                    MsgBox("atenção:" & Chr(13) & "o fornecedor não está identificado como certificado ocs.", vbCritical + vbOKOnly)
'                                ElseIf listforncert.Valor("cdu_ocsdata") < listforncert.Valor("hoje") Then
'                                    MsgBox("atenção:" & Chr(13) & "o fornecedor tem o certificado ocs expirado.", vbCritical + vbOKOnly)
'                                End If
'                            End If

'                            If UCase(BSO.Base.Artigos.Edita(Me.DocumentoCompra.Linhas.GetEdita(l).Artigo).CamposUtil("cdu_descricaoextra").Valor) Like "*bci*" Then
'                                If listforncert.Valor("cdu_bci") <> True Then
'                                    MsgBox("atenção:" & Chr(13) & "o fornecedor não está identificado como certificado bci.", vbCritical + vbOKOnly)
'                                ElseIf listforncert.Valor("cdu_bcidata") < listforncert.Valor("hoje") Then
'                                    MsgBox("atenção:" & Chr(13) & "o fornecedor tem o certificado bci expirado.", vbCritical + vbOKOnly)
'                                End If
'                            End If
'                            'jfc 03/09/2020 alerta da ana castro, os bci's são identificados nas observações do lote.
'                            If UCase(BSO.Inventario.ArtigosLotes.Edita(Me.DocumentoCompra.Linhas.GetEdita(l).Artigo, Me.DocumentoCompra.Linhas.GetEdita(l).Lote).Observacoes) Like "*bci*" Then
'                                If listforncert.Valor("cdu_bci") <> True Then
'                                    MsgBox("atenção:" & Chr(13) & "o fornecedor não está identificado como certificado bci.", vbCritical + vbOKOnly)
'                                ElseIf listforncert.Valor("cdu_bcidata") < listforncert.Valor("hoje") Then
'                                    MsgBox("atenção:" & Chr(13) & "o fornecedor tem o certificado bci expirado.", vbCritical + vbOKOnly)
'                                End If
'                            End If

'                            If UCase(BSO.Base.Artigos.Edita(Me.DocumentoCompra.Linhas.GetEdita(l).Artigo).CamposUtil("cdu_descricaoextra").Valor) Like "*fsc*" Then
'                                If listforncert.Valor("cdu_fsc") <> True Then
'                                    MsgBox("atenção:" & Chr(13) & "o fornecedor não está identificado como certificado fsc.", vbCritical + vbOKOnly)
'                                ElseIf listforncert.Valor("cdu_fscdata") < listforncert.Valor("hoje") Then
'                                    MsgBox("atenção:" & Chr(13) & "o fornecedor tem o certificado fsc expirado.", vbCritical + vbOKOnly)
'                                End If
'                            End If
'                        End If
'                    Next l


'                End If



'                '    '*******************************************************************************************************************************************
'                '    '#### bruno 18/02/2020 #### validar em cnt/ecf tem produtos certificados e se o fornecedor é certificado ####
'                '    '*******************************************************************************************************************************************

'            End If
'        End Sub


'        Public Overrides Sub depoisdegravar(filial As String, tipo As String, serie As String, numdoc As Integer, e As ExtensibilityEventArgs)
'            MyBase.DepoisDeGravar(filial, tipo, serie, numdoc, e)

'            If Module1.VerificaToken("certificadosorg") = 1 Then

'                '####################################################################################################################################
'                '#recalculo de saldos de certificados disponiveis jfc - 22/07/2019                                                                  #
'                '####################################################################################################################################
'                If Me.DocumentoCompra.Tipodoc = "ecf" Then
'                    Dim recalculacerts As Boolean
'                    recalculacerts = False
'                    For i = 1 To Me.DocumentoCompra.Linhas.NumItens
'                        If Me.DocumentoCompra.Linhas.GetEdita(i).CamposUtil("cdu_numcertificadotrans").Valor & "" <> "" Then

'                            recalculacerts = True

'                        End If
'                    Next i

'                    If recalculacerts Then
'                        BSO.DSO.ExecuteSQL("exec [dbo].[spinserircert]")
'                    End If
'                End If
'                '####################################################################################################################################
'                '#recalculo de saldos de certificados disponiveis jfc - 22/07/2019                                                                  #
'                '####################################################################################################################################

'            End If
'        End Sub


'        Public Overrides Sub teclapressionada(keycode As Integer, shift As Integer, e As ExtensibilityEventArgs)
'            MyBase.TeclaPressionada(keycode, shift, e)

'            If Module1.VerificaToken("certificadosorg") = 1 Then

'                '################################################################################################
'                '# inserir certificados de transação, formulário frmalteracertificadotransacao (jfc 20/03/2019) #
'                '################################################################################################
'                'crtl+f- alteracertificadotransacao
'                If Me.LinhaActual > 0 Then
'                    If keycode = 70 And Me.DocumentoCompra.Tipodoc = "ecf" Then

'                        Module1.certArtigo = Me.DocumentoCompra.Linhas.GetEdita(Me.LinhaActual).Artigo
'                        Module1.certDescricao = Me.DocumentoCompra.Linhas.GetEdita(Me.LinhaActual).Descricao
'                        Module1.certDocumento = Me.DocumentoCompra.Tipodoc & " " & Me.DocumentoCompra.NumDoc & "/" & Me.DocumentoCompra.Serie
'                        Module1.certLote = Me.DocumentoCompra.Linhas.GetEdita(Me.LinhaActual).Lote
'                        Module1.certArmazem = Me.DocumentoCompra.Linhas.GetEdita(Me.LinhaActual).Armazem
'                        Module1.certCertificadoTransacao = Me.DocumentoCompra.Linhas.GetEdita(Me.LinhaActual).CamposUtil("cdu_numcertificadotrans").Valor
'                        Module1.certDataCertificado = Me.DocumentoCompra.Linhas.GetEdita(Me.LinhaActual).CamposUtil("cdu_datacertificadotrans").Valor
'                        Module1.certIDlinha = Me.DocumentoCompra.Linhas.GetEdita(Me.LinhaActual).IdLinha
'                        Module1.certProgramLabel = Me.DocumentoCompra.Linhas.GetEdita(Me.LinhaActual).CamposUtil("cdu_programlabels").Valor
'                        Module1.certBCI = Me.DocumentoCompra.Linhas.GetEdita(Me.LinhaActual).CamposUtil("cdu_bci").Valor


'                        Dim result As ExtensibilityResult = PriV100Api.BSO.Extensibility.CreateCustomFormInstance(GetType(frmalteracertificadotransacao))

'                        If result.ResultCode = ExtensibilityResultCode.Ok Then

'                            Dim frm As frmalteracertificadotransacao = result.Result
'                            frm.showdialog()

'                        End If
'                    End If
'                End If
'                '################################################################################################
'                '# inserir certificados de transação, formulário frmalteracertificadotransacao (jfc 20/03/2019) #
'                '################################################################################################

'            End If

'        End Sub

'    End Class
'End Namespace