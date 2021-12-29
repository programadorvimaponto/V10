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

Namespace ValidaPrBase
    Public Class VndIsEditorVendas
        Inherits EditorVendas

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)


            If Module1.VerificaToken("ValidaPrBase") = 1 Then

                'JFC 10/12/2020 Só valida caso o cliente não seja PT. Isto porque algumas encomendas entre empresas com local descarga em PT teriam preço base superior ao preço unitário.
                If Me.DocumentoVenda.Pais <> "PT" Then
                    'JFC 04/11/2019 - Não gravar documento caso o preço base seja superior ao preço unitário. Pedido de Mafalda.
                    For i = 1 To Me.DocumentoVenda.Linhas.NumItens
                        If Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "" <> "" Then
                            If Me.DocumentoVenda.Linhas.GetEdita(i).PrecUnit < Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_PrecoBase").Valor Then
                                Cancel = True
                                MsgBox("CDU_PrecoBase superior ao Pr. Unit. O documento não será gravado!" & Chr(13) & Chr(13) & "Linha: " & i & " - " & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & " - " & Me.DocumentoVenda.Linhas.GetEdita(i).Lote, vbCritical)
                            End If
                        End If
                    Next i
                End If
            End If
        End Sub

    End Class
End Namespace
