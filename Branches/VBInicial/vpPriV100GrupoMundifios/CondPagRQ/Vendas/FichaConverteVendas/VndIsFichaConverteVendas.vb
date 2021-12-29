Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Sales.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports VndBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Namespace CondPagRQ
    Public Class VndIsFichaConverteVendas
        Inherits FichaConverteVendas

        Public Overrides Sub DepoisDeConverter(colDocumentosGerados As Primavera.Platform.Collections.PrimaveraOrderedDictionary, e As ExtensibilityEventArgs)
            MyBase.DepoisDeConverter(colDocumentosGerados, e)

            If Module1.VerificaToken("CondPagRQ") = 1 Then

                Dim DocVendaRQRM As New VndBEDocumentoVenda

                Dim itdoc As Object

                For Each itdoc In colDocumentosGerados

                    DocVendaRQRM = BSO.Vendas.Documentos.Edita(itdoc.Filial, itdoc.Tipodoc, itdoc.Serie, itdoc.NumDoc)

                    If DocVendaRQRM.CondPag & "" <> "" Then

                        If BSO.Base.CondsPagamento.Edita(DocVendaRQRM.CondPag).CamposUtil("CDU_RQ").Valor = True Or BSO.Base.CondsPagamento.Edita(DocVendaRQRM.CondPag).CamposUtil("CDU_RM").Valor = True Then

                            DocVendaRQRM.DataVenc = Module1.NovaDataVencimento(DocVendaRQRM.DataDoc, DocVendaRQRM.CondPag, DocVendaRQRM.TipoEntidade, DocVendaRQRM.Entidade)
                            DocVendaRQRM.CamposUtil("CDU_AlteradaDataVenc").Valor = 1
                            BSO.Vendas.Documentos.Actualiza(DocVendaRQRM)
                        End If

                    End If
                Next
            End If
        End Sub

    End Class
End Namespace