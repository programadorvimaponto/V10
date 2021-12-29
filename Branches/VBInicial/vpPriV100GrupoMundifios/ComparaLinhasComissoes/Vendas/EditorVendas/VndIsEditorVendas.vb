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

Namespace ComparaLinhasComissoes
    Public Class VndNsVendas
        Inherits EditorVendas

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("ComparaLinhasComissoes") = 1 Then

                '##########################################################################################
                '##Valida se alguma das linhas tem comissão diferente  - Pedido D. Goreti -25/03/2019 JFC##
                '##########################################################################################
                If Me.DocumentoVenda.Tipodoc = "ECL" Then
                    Dim comissaoAux As Long
                    Dim comissaoBolean As Boolean
                    Dim comissaoStr As String
                    comissaoAux = 2365479
                    comissaoBolean = False
                    For j = 1 To Me.DocumentoVenda.Linhas.NumItens
                        If Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & "" <> "" Then
                            If comissaoAux = 2365479 Then
                                comissaoAux = Me.DocumentoVenda.Linhas.GetEdita(j).Comissao

                            Else
                                If comissaoAux <> Me.DocumentoVenda.Linhas.GetEdita(j).Comissao Then
                                    comissaoBolean = True
                                End If
                            End If

                        End If
                    Next j

                    If comissaoBolean = True Then
                        comissaoStr = "Atenção existem comissões diferentes nas diversas linhas" & Chr(13) & Chr(13) & "Linha - Artigo - Lote - Comissão" & Chr(13)
                        For j = 1 To Me.DocumentoVenda.Linhas.NumItens
                            If Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & "" <> "" Then
                                comissaoStr = comissaoStr & j & " - " & Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & " - " & Me.DocumentoVenda.Linhas.GetEdita(j).Lote & " - " & Me.DocumentoVenda.Linhas.GetEdita(j).Comissao & Chr(13)
                            End If
                        Next j
                        If (MsgBox(comissaoStr & Chr(13) & "Deseja continuar com a gravação?", vbYesNo)) = vbNo Then
                            Cancel = True
                        End If
                    End If

                End If
                '##########################################################################################
                '##Valida se alguma das linhas tem comissão diferente  - Pedido D. Goreti -25/03/2019 JFC##
                '##########################################################################################

            End If

        End Sub

    End Class
End Namespace
