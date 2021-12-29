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

Namespace ValidaGrupoFG
    Public Class VndIsEditorVendas
        Inherits EditorVendas

        Public Overrides Sub ClienteIdentificado(Cliente As String, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.ClienteIdentificado(Cliente, Cancel, e)

            If Module1.VerificaToken("ValidaGrupoFG") = 1 Then
                If (Me.DocumentoVenda.Tipodoc = "FA" Or Me.DocumentoVenda.Tipodoc = "FES" Or Me.DocumentoVenda.Tipodoc = "NC" Or Me.DocumentoVenda.Tipodoc = "NCS") Then
                    If Me.DocumentoVenda.Entidade = "0707" Or Me.DocumentoVenda.Entidade = "1207" Or Me.DocumentoVenda.Entidade = "0580" Or Me.DocumentoVenda.Entidade = "0248" Or Me.DocumentoVenda.Entidade = "2492" Then
                        MsgBox("Atenção:" & Chr(13) & "Empresa do Grupo, não deve ser usado neste documento. Utilizar o documento FG, FGS ou NCG", vbCritical + vbOKOnly)
                    End If
                End If
            End If
        End Sub


    End Class
End Namespace