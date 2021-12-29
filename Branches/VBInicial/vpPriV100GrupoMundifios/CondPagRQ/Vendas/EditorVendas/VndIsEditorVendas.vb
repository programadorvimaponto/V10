Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Sales.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico


Namespace CondPagRQ
    Public Class VndIsEditorVendas
        Inherits EditorVendas

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("CondPagRQ") = 1 Then

                If Me.DocumentoVenda.CondPag & "" <> "" Then
                    If BSO.Base.CondsPagamento.Edita(Me.DocumentoVenda.CondPag).CamposUtil("CDU_RQ").Valor = True Or BSO.Base.CondsPagamento.Edita(Me.DocumentoVenda.CondPag).CamposUtil("CDU_RM").Valor = True Then
                        Me.DocumentoVenda.DataVenc = Module1.NovaDataVencimento(Me.DocumentoVenda.DataDoc, Me.DocumentoVenda.CondPag, Me.DocumentoVenda.TipoEntidade, Me.DocumentoVenda.Entidade)
                    End If
                End If

            End If

        End Sub

        Public Overrides Sub ClienteIdentificado(Cliente As String, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.ClienteIdentificado(Cliente, Cancel, e)

            If Module1.VerificaToken("CondPagRQ") = 1 Then

                If Me.DocumentoVenda.CondPag & "" <> "" Then
                    If BSO.Base.CondsPagamento.Edita(Me.DocumentoVenda.CondPag).CamposUtil("CDU_RQ").Valor = True Or BSO.Base.CondsPagamento.Edita(Me.DocumentoVenda.CondPag).CamposUtil("CDU_RM").Valor = True Then
                        Me.DocumentoVenda.DataVenc = Module1.NovaDataVencimento(Me.DocumentoVenda.DataDoc, Me.DocumentoVenda.CondPag, Me.DocumentoVenda.TipoEntidade, Me.DocumentoVenda.Entidade)
                    End If
                End If
            End If

        End Sub

        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

            If Module1.VerificaToken("CondPagRQ") = 1 Then

                If Me.DocumentoVenda.CondPag & "" <> "" Then
                    If BSO.Base.CondsPagamento.Edita(Me.DocumentoVenda.CondPag).CamposUtil("CDU_RQ").Valor = True Or BSO.Base.CondsPagamento.Edita(Me.DocumentoVenda.CondPag).CamposUtil("CDU_RM").Valor = True Then
                        BSO.DSO.ExecuteSQL("UPDATE CabecDoc SET CDU_AlteradaDataVenc = 1 WHERE Id = '" & Me.DocumentoVenda.ID & "'")
                    End If
                End If
            End If

        End Sub

    End Class
End Namespace