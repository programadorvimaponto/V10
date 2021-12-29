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
Namespace QtdMinCliente
    Public Class VndIsEditorVendas
        Inherits EditorVendas

        Public Overrides Sub ClienteIdentificado(Cliente As String, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.ClienteIdentificado(Cliente, Cancel, e)

            If Module1.VerificaToken("QtdMinCliente") = 1 Then

                '*******************************************************************************************************************************************
                '#### QUANTIDADES MINIMAS PARA CLIENTES - Pedido de Joaquim António 30/01/2017 (JFC) ####
                '*******************************************************************************************************************************************
                Dim n As Long
                Dim lista As StdBELista
                Dim ent As String
                If (Me.DocumentoVenda.Tipodoc = "ECL" Or Me.DocumentoVenda.Tipodoc = "GC") Then

                    ent = Me.DocumentoVenda.Entidade
                    lista = BSO.Consulta("SELECT Entidade FROM TDU_QntMinimas Where Entidade=" & "'" & ent & "'")



                    If (lista.Vazia = False) Then
                        MsgBox("Atenção:" & Chr(13) & "Cliente com quantidade minima de 1 Palete ou Caixa", vbInformation + vbOKOnly)
                    End If


                End If

                '*******************************************************************************************************************************************
                '#### QUANTIDADES MINIMAS PARA CLIENTES - Pedido de Joaquim António 30/01/2017 (JFC) ####
                '*******************************************************************************************************************************************

            End If

        End Sub


    End Class
End Namespace