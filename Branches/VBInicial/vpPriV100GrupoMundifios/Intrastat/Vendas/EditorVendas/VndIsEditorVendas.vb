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

Namespace Intrastat
    Public Class VndIsEditorVendas
        Inherits EditorVendas
        Dim i As Long
        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("Intrastat") = 1 Then

                If BSO.Vendas.TabVendas.DaValorAtributo(Me.DocumentoVenda.Tipodoc, "IntrastatDoc") = True Then

                    i = 0

                    For i = 1 To Me.DocumentoVenda.Linhas.NumItens
                        If Me.DocumentoVenda.Linhas.GetEdita(i).TipoLinha >= 10 And Me.DocumentoVenda.Linhas.GetEdita(i).TipoLinha <= 29 And Me.DocumentoVenda.Linhas.GetEdita(i).Unidade = "KG" Then
                            Me.DocumentoVenda.Linhas.GetEdita(i).IntrastatRegiao = "80"
                            Me.DocumentoVenda.Linhas.GetEdita(i).IntrastatMassaLiq = 1

                        End If
                    Next i
                End If

            End If

        End Sub

        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

            If Module1.VerificaToken("Intrastat") = 1 Then

                If BSO.Vendas.TabVendas.DaValorAtributo(Me.DocumentoVenda.Tipodoc, "IntrastatDoc") = True Then
                    BSO.DSO.ExecuteSQL("UPDATE CABECDOC SET INTRASTATNATA='1', INTRASTATNATB='1',IntrastatCondEnt='CIP',IntrastatModoTransp='3' WHERE ID='" & Me.DocumentoVenda.ID & "'")
                End If

            End If

        End Sub

    End Class
End Namespace
