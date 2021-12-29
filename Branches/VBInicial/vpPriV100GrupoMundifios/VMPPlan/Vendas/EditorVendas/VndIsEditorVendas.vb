Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Sales.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace VMPPlan
    Public Class VndIsEditorVendas
        Inherits EditorVendas

        Public Overrides Sub AntesDeAnular(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeAnular(Cancel, e)

            If Module1.VerificaToken("VMPPlan") Then

                '#########################################################################################################################
                '##Antes de anular uma ECL verifica se tem alguma produção associada. Pedido do Eng. Manuel Martins - Bruno 12/02/2021  ##
                '#########################################################################################################################

                Dim j As Long
                Dim t As Integer
                If Me.DocumentoVenda.Tipodoc = "ECL" Then
                    For j = 1 To Me.DocumentoVenda.Linhas.NumItens
                        If Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(j).Lote & "" <> "" Then
                            If Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_PLANumDoc").Valor <> 0 Then
                                t = MsgBox("Atenção:" & Chr(13) & "O Artigo " & Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & " tem documentos de produção associados,(" & Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_PLATipoDoc").Valor & "/" & Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_PLANumDoc").Valor & "), tem a certeza que deseja anular?", vbCritical + vbOKCancel)
                                If t = vbCancel Then
                                    Cancel = True
                                End If


                            End If
                        End If


                    Next j
                End If
                '#########################################################################################################################
                '##Antes de anular uma ECL verifica se tem alguma produção associada. Pedido do Eng. Manuel Martins - Bruno 12/02/2021  ##
                '#########################################################################################################################

            End If

        End Sub

    End Class
End Namespace
