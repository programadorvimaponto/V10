Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Production.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace ArmazemEntreposto
    Public Class InvIsEditorStocks
        Inherits EditorStocksProducao

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityService.EventArgs.ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("ArmazemEntreposto") = 1 Then


                For i = 1 To DocumentoStock.Linhas.NumItens

                    If Me.DocumentoStock.Linhas.GetEdita(i).Artigo & "" <> "" And Me.DocumentoStock.Linhas.GetEdita(i).Armazem = Module1.ArmEntreposto And Me.DocumentoStock.Tipodoc <> "ENCG" Then

                        If Me.DocumentoStock.Linhas.GetEdita(i).CamposUtil("CDU_DespTipoImportacao").Valor & "" = "" Then
                            MsgBox("Atenção:" & Chr(13) & "O Tipo de Importação na linha " & i & " para o artigo '" & Me.DocumentoStock.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoStock.Linhas.GetEdita(i).Lote & "' não está preenchido.", vbCritical + vbOKOnly)
                            Cancel = True
                            Exit Sub
                        End If
                        If Me.DocumentoStock.Linhas.GetEdita(i).CamposUtil("CDU_DespDAU").Valor & "" = "" Then
                            MsgBox("Atenção:" & Chr(13) & "O Código DAU na linha " & i & " para o artigo '" & Me.DocumentoStock.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoStock.Linhas.GetEdita(i).Lote & "' não está preenchido.", vbCritical + vbOKOnly)
                            Cancel = True
                            Exit Sub
                        End If
                        If Me.DocumentoStock.Linhas.GetEdita(i).CamposUtil("CDU_Volumes").Valor & "" = "" Then
                            MsgBox("Atenção:" & Chr(13) & "Os Volumes na linha " & i & " para o artigo '" & Me.DocumentoStock.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoStock.Linhas.GetEdita(i).Lote & "' não estão preenchidos.", vbCritical + vbOKOnly)
                            Cancel = True
                            Exit Sub
                        End If
                        If Me.DocumentoStock.Linhas.GetEdita(i).CamposUtil("CDU_CODMERC").Valor & "" = "" Then
                            MsgBox("Atenção:" & Chr(13) & "O Código da Mercadoria na linha " & i & " para o artigo '" & Me.DocumentoStock.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoStock.Linhas.GetEdita(i).Lote & "' não está preenchido.", vbCritical + vbOKOnly)
                            Cancel = True
                            Exit Sub
                        End If
                        If Me.DocumentoStock.Linhas.GetEdita(i).CamposUtil("CDU_Regime").Valor & "" = "" Then
                            MsgBox("Atenção:" & Chr(13) & "O Código do Regime na linha " & i & " para o artigo '" & Me.DocumentoStock.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoStock.Linhas.GetEdita(i).Lote & "' não está preenchido.", vbCritical + vbOKOnly)
                            Cancel = True
                            Exit Sub
                        End If
                        If Me.DocumentoStock.Linhas.GetEdita(i).CamposUtil("CDU_MassaBruta").Valor = 0 Then
                            MsgBox("Atenção:" & Chr(13) & "A Massa Bruta na linha " & i & " para o artigo '" & Me.DocumentoStock.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoStock.Linhas.GetEdita(i).Lote & "' não está preenchida.", vbCritical + vbOKOnly)
                            Cancel = True
                            Exit Sub
                        End If
                        If Me.DocumentoStock.Linhas.GetEdita(i).CamposUtil("CDU_MassaLiq").Valor = 0 Then
                            MsgBox("Atenção:" & Chr(13) & "A Massa Líquida na linha " & i & " para o artigo '" & Me.DocumentoStock.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoStock.Linhas.GetEdita(i).Lote & "' não está preenchida.", vbCritical + vbOKOnly)
                            Cancel = True
                            Exit Sub
                        End If
                        If Me.DocumentoStock.Linhas.GetEdita(i).CamposUtil("CDU_Contramarca").Valor & "" = "" Then
                            MsgBox("Atenção:" & Chr(13) & "A Contramarca na linha " & i & " para o artigo '" & Me.DocumentoStock.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoStock.Linhas.GetEdita(i).Lote & "' não está preenchida.", vbCritical + vbOKOnly)
                            Cancel = True
                            Exit Sub
                        End If
                        If Me.DocumentoStock.Linhas.GetEdita(i).CamposUtil("CDU_ContramarcaData").Valor & "" = "" Then
                            MsgBox("Atenção:" & Chr(13) & "A Data da Contramarca na linha " & i & " para o artigo '" & Me.DocumentoStock.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoStock.Linhas.GetEdita(i).Lote & "' não está preenchida.", vbCritical + vbOKOnly)
                            Cancel = True
                            Exit Sub
                        End If
                        If Me.DocumentoStock.Linhas.GetEdita(i).CamposUtil("CDU_ValorAduaneiro").Valor = 0 Then
                            MsgBox("Atenção:" & Chr(13) & "O Valor Aduaneiro na linha " & i & " para o artigo '" & Me.DocumentoStock.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoStock.Linhas.GetEdita(i).Lote & "' não está preenchido.", vbCritical + vbOKOnly)
                            Cancel = True
                            Exit Sub
                        End If
                        If Me.DocumentoStock.Linhas.GetEdita(i).CamposUtil("CDU_IvaDAU").Valor = 0 Then
                            MsgBox("Atenção:" & Chr(13) & "O Valor do Iva da DAU na linha " & i & " para o artigo '" & Me.DocumentoStock.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoStock.Linhas.GetEdita(i).Lote & "' não está preenchido.", vbCritical + vbOKOnly)
                            Cancel = True
                            Exit Sub
                        End If
                        If Me.DocumentoStock.Linhas.GetEdita(i).CamposUtil("CDU_DireitosDAU").Valor = 0 Then
                            MsgBox("Atenção:" & Chr(13) & "Os Direitos da DAU na linha " & i & " para o artigo '" & Me.DocumentoStock.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoStock.Linhas.GetEdita(i).Lote & "' não estão preenchidos.", vbCritical + vbOKOnly)
                            Cancel = True
                            Exit Sub
                        End If

                    End If

                Next i

            End If

        End Sub

    End Class
End Namespace