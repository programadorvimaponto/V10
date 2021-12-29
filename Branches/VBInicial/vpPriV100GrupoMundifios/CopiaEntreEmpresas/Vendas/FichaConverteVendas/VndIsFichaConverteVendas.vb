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
Imports Primavera.Platform.Collections

Namespace CopiaEntreEmpresas
    Public Class VndIsFichaConverteVendas
        Inherits FichaConverteVendas

        Public Overrides Sub DepoisDeConverter(colDocumentosGerados As PrimaveraOrderedDictionary, e As ExtensibilityEventArgs)
            MyBase.DepoisDeConverter(colDocumentosGerados, e)

            If Module1.VerificaToken("CopiaEntreEmpresas") = 1 Then

                Dim DocVendaRQRM As New VndBE100.VndBEDocumentoVenda
                Dim itdoc As Object

                For Each itdoc In colDocumentosGerados

                    'JFC - 02/09/2020 - No caso de faturas entre empresas de grupo colocar a morada de descarga da empresa. Pedido de Ana Castro.
                    'Ao inserir as faturas na control-union apareciam locais de descarga de clientes.
                    If DocVendaRQRM.Tipodoc = "FG" Then

                        DocVendaRQRM.CargaDescarga.MoradaEntrega = BSO.Base.Clientes.Edita(DocVendaRQRM.Entidade).Morada
                        DocVendaRQRM.CargaDescarga.Morada2Entrega = BSO.Base.Clientes.Edita(DocVendaRQRM.Entidade).Morada2
                        DocVendaRQRM.CargaDescarga.LocalidadeEntrega = BSO.Base.Clientes.Edita(DocVendaRQRM.Entidade).Localidade
                        DocVendaRQRM.CargaDescarga.DistritoEntrega = BSO.Base.Clientes.Edita(DocVendaRQRM.Entidade).Distrito
                        DocVendaRQRM.CargaDescarga.CodPostalEntrega = BSO.Base.Clientes.Edita(DocVendaRQRM.Entidade).CodigoPostal
                        DocVendaRQRM.CargaDescarga.CodPostalLocalidadeEntrega = BSO.Base.Clientes.Edita(DocVendaRQRM.Entidade).LocalidadeCodigoPostal
                        DocVendaRQRM.CargaDescarga.PaisEntrega = BSO.Base.Clientes.Edita(DocVendaRQRM.Entidade).Pais

                        BSO.Vendas.Documentos.Actualiza(DocVendaRQRM)
                    End If



                Next

            End If

        End Sub

    End Class
End Namespace
