Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Sales.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace AlertaObsCliente
    Public Class VndIsEditorVendas
        Inherits EditorVendas

        Public Overrides Sub ClienteIdentificado(Cliente As String, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.ClienteIdentificado(Cliente, Cancel, e)

            If Module1.VerificaToken("AlertaObsCliente") = 1 Then
                If Me.DocumentoVenda.Tipodoc = "ECL" Or Me.DocumentoVenda.Tipodoc = "GC" Then

                    If BSO.Base.Clientes.Edita(Cliente).CamposUtil("CDU_ObsEncomenda").Valor & "" <> "" Then
                        MsgBox(BSO.Base.Clientes.Edita(Cliente).CamposUtil("CDU_ObsEncomenda").Valor, vbInformation + vbOKOnly)
                    End If

                End If
            End If
        End Sub

    End Class
End Namespace
