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
Imports Primavera.Extensibility.Attributes

Namespace SerieC
    Public Class VndIsEditorVendas
        Inherits EditorVendas
        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("SerieC") = 1 Then


                'JFC - Antes de gravar garantir que um documento da serie C é transformado para outro documento da serie C - Pedido de Mafalda 15/10/2018
                If Me.DocumentoVenda.Tipodoc = "GR" Or Me.DocumentoVenda.Tipodoc = "FA" Then
                    Dim j As Long
                    Dim SerieC As StdBELista

                    If Right(Me.DocumentoVenda.Serie, 1) <> "C" Then
                        For j = 1 To Me.DocumentoVenda.Linhas.NumItens
                            If Me.DocumentoVenda.Linhas.GetEdita(j).IDLinhaOriginal & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & "" <> "" Then


                                SerieC = BSO.Consulta("select top 1 right(cd.serie,1) as Serie from cabecdoc cd inner join linhasdoc ln on ln.idcabecdoc=cd.id where ln.id='" & Me.DocumentoVenda.Linhas.GetEdita(j).IDLinhaOriginal & "'")
                                SerieC.Inicio()

                                If SerieC.Valor("Serie") = "C" Then

                                    MsgBox("Atenção está a transformar um documento da Serie C para outra Serie: " & Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & " - " & Me.DocumentoVenda.Linhas.GetEdita(j).Lote, vbCritical + vbOKOnly)
                                    Cancel = True
                                End If
                            End If
                        Next j
                    End If

                End If
            End If

        End Sub

    End Class
End Namespace