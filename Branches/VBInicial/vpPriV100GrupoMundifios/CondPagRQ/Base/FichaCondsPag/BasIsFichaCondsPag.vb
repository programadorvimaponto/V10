Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Base.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace CondPagRQ
    Public Class BasIsFichaCondsPag
        Inherits FichaCondsPag

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("CondPagRQ") = 1 Then

                If Me.CondPag.CamposUtil("CDU_RQ").Valor = True And Me.CondPag.CamposUtil("CDU_RM").Valor = True Then

                    MsgBox("N�o pode na mesma condi��o de pagamento estar escolhida a op��o resumo quinzenal e resumo mensal." & Chr(13) & Chr(13) & "Deve apenas seleccionar uma das op��es.", vbInformation + vbOKOnly)
                    Cancel = True

                End If
            End If

        End Sub

    End Class
End Namespace