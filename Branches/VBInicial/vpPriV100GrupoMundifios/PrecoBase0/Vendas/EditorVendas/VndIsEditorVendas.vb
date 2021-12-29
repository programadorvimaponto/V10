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

Namespace PrecoBase0
    Public Class VndIsEditorVendas
        Inherits EditorVendas

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("PrecoBase0") = 1 Then

                '##############################################################################################
                '##      Não deixa gravar nenhuma fatura caso o Preço Base seja 0   (BRUNO - 25/09/2020)     ##
                '##############################################################################################
                'Editado para bloquear a em qualquer tipo de documento por forma a garantir que o email diário de margens vai com o PrBase preenchido. - JFC 28/07/2021
                '  If BSO.Comercial.TabVendas.Edita(Me.DocumentoVenda.tipoDoc).TipoDocumento = 4 Then

                For i = 1 To Me.DocumentoVenda.Linhas.NumItens
                    If Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "" <> "" Then
                        If Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_PrecoBase").Valor = 0 Then
                            Cancel = True
                            MsgBox("Il campo CDU_PrecoBase non è compilato. Il documento non verrà salvato! Ctrl + U sulla linea e riempire il campo." & Chr(13) & Chr(13) & "Linha: " & i & " - " & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & " - " & Me.DocumentoVenda.Linhas.GetEdita(i).Lote, vbCritical)
                        End If
                    End If
                Next i

                '  End If

                '##############################################################################################
                '##      Não deixa gravar nenhuma fatura caso o Preço Base seja 0   (BRUNO - 25/09/2020)     ##
                '##############################################################################################




            End If

        End Sub

    End Class
End Namespace