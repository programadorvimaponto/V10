Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Sales.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgsPublic
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports StdPlatBS100.StdBSTipos

Namespace LoteEncomendaIgualGuia
    Public Class VndNsEditorVendas
        Inherits EditorVendas

        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)


            If Module1.VerificaToken("LoteEncomendaIgualGuia") = 1 Then

                '*******************************************************************************************************************************************
                '#### COLOCAR LOTE DA ENCOMENDA IGUAL AO DA GUIA #### Pedido pela Dª Goretti dia 19/03/2015
                '*******************************************************************************************************************************************
                Dim j As Long
                Dim asd As String
                If (Me.DocumentoVenda.Tipodoc = "GR" Or Me.DocumentoVenda.Tipodoc = "GT") Then

                    For j = 1 To Me.DocumentoVenda.Linhas.NumItens

                        If Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(j).Lote & "" <> "" Then
                            BSO.DSO.ExecuteSQL("UPDATE LinhasDoc SET Lote = '" & Me.DocumentoVenda.Linhas.GetEdita(j).Lote & "' WHERE Id = '" & Me.DocumentoVenda.Linhas.GetEdita(j).IDLinhaOriginal & "'")

                        End If

                    Next j

                End If
                '*******************************************************************************************************************************************
                '#### COLOCAR LOTE DA ENCOMENDA IGUAL AO DA GUIA #### Pedido pela Dª Goretti dia 19/03/2015
                '*******************************************************************************************************************************************
            End If
        End Sub

    End Class
End Namespace
