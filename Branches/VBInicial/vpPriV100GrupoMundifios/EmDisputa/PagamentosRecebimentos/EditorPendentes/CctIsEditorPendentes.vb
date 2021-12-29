Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.PayablesReceivables.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports StdBE100
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService
Imports Primavera.Extensibility.Constants.ExtensibilityService

Namespace EmDisputa
    Public Class CctIsEditorPendentes
        Inherits EditorPendentes

        Public Overrides Sub TeclaPressionada(KeyCode As Integer, Shift As Integer, e As ExtensibilityEventArgs)
            MyBase.TeclaPressionada(KeyCode, Shift, e)

            '#############################################################################
            '# Faturas em Disputa, Formulário FrmEmDisputa (JFC 26/07/2018) #
            '#############################################################################
            'Crtl+D- EmDisputa

            If Module1.VerificaToken("EmDisputa") = 1 Then

                If KeyCode = 68 Then
                    Module1.dspModulo = "M"



                    Dim listaPen As StdBELista
                    Dim Pen As Boolean

                    Pen = False

                    listaPen = BSO.Consulta("select * from Pendentes p where p.TipoDoc='" & Me.DocumentoPendente.Tipodoc & "' and p.NumDocInt='" & Me.DocumentoPendente.NumDocInt & "' and p.Serie='" & Me.DocumentoPendente.Serie & "'")

                    If (listaPen.Vazia = False) Then
                        Pen = True
                    End If

                    If Pen Then


                        Dim result As ExtensibilityResult = PriV100Api.BSO.Extensibility.CreateCustomFormInstance(GetType(FrmEmDisputaView))

                        If result.ResultCode = ExtensibilityResultCode.Ok Then

                            Dim frm As FrmEmDisputaView = result.Result
                            frm.ShowDialog()

                        End If

                    End If

                End If

                '#############################################################################
                '# Faturas em Disputa, Formulário FrmEmDisputa (JFC 26/07/2018) #
                '#############################################################################
            End If
        End Sub

    End Class
End Namespace