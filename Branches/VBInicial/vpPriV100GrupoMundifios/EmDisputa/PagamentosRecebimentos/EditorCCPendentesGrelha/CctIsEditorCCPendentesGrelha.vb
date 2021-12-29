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

    Public Class CctNsEditorCCPendentesGrelha
        Inherits EditorCCPendentesGrelha

        Public Overrides Sub PendenteSeleccionado(NumLinha As Integer, objBeCampos As StdBECampos, e As ExtensibilityEventArgs)
            MyBase.PendenteSeleccionado(NumLinha, objBeCampos, e)
            If Module1.VerificaToken("EmDisputa") = 1 Then

                Module1.dsptipoDoc = objBeCampos.Item(4).Valor
                Module1.dspSerie = objBeCampos.Item(6).Valor
                Module1.dspNumDoc = objBeCampos.Item(8).Valor

            End If

        End Sub

        Public Overrides Sub TeclaPressionada(KeyCode As Integer, Shift As Integer, e As ExtensibilityEventArgs)
            MyBase.TeclaPressionada(KeyCode, Shift, e)

            '#############################################################################
            '# Faturas em Disputa, Formulário FrmEmDisputa (JFC 26/07/2018) #
            '#############################################################################
            'Crtl+D- EmDisputa

            If Module1.VerificaToken("EmDisputa") = 1 Then

                If KeyCode = 68 Then



                    Dim result As ExtensibilityResult = PriV100Api.BSO.Extensibility.CreateCustomFormInstance(GetType(FrmEmDisputaView))

                    If result.ResultCode = ExtensibilityResultCode.Ok Then

                        Dim frm As FrmEmDisputaView = result.Result
                        frm.ShowDialog()

                    End If


                End If

                '#############################################################################
                '# Faturas em Disputa, Formulário FrmEmDisputa (JFC 26/07/2018) #
                '#############################################################################

            End If

        End Sub

    End Class
End Namespace