Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Primavera.Extensibility.Purchases.Editors
Imports Primavera.Extensibility.Attributes
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports StdBE100

Namespace CopiarCaractTec
    Public Class CmpIsEditorCompras
        Inherits EditorCompras

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("CopiarCaractTec") = 1 Then


                ''If Not CopiarCaractTec Then Cancel = True
                'If Not Mdi_CopiaCaracteristicasTecnicas.CopiarCaractTec(BSO.Contexto.CodEmp, Me.DocumentoCompra) Then


                '    If MsgBox("Não foi possível realizar a cópia de características!" & vbNewLine & "Deseja mesmo assim gravar o documento?", vbQuestion + vbYesNo) = vbNo Then
                '        Cancel = True
                '    End If

                'End If

            End If
        End Sub

    End Class
End Namespace