Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Primavera.Extensibility.Purchases.Editors
Imports Primavera.Extensibility.Attributes
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports StdBE100

Namespace CopiarLotes

    Public Class CmpIsEditorCompras
        Inherits EditorCompras

        'Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
        '    MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

        '    If Module1.VerificaToken("CopiarLotes") = 1 Then




        '        'JFC 28/10/2019 - Cria Lotes na Mundifios
        '        If Me.DocumentoCompra.Entidade & "" <> "" And BSO.Compras.TabCompras.Edita(Me.DocumentoCompra.Tipodoc).TipoDocumento = 2 Then
        '            If AbreObjEmpresa("MUNDIFIOS") Then
        '                For i = 1 To Me.DocumentoCompra.Linhas.NumItens
        '                    If Me.DocumentoCompra.Linhas.GetEdita(i).Artigo & "" <> "" And Me.DocumentoCompra.Linhas.GetEdita(i).Lote <> "" And Me.DocumentoCompra.Linhas.GetEdita(i).Lote <> "<L01>" Then
        '                        If BSO.Base.Artigos.Existe(Me.DocumentoCompra.Linhas.GetEdita(i).Artigo) = True And (BSO.Base.Artigos.Edita(Me.DocumentoCompra.Linhas.GetEdita(i).Artigo).Descricao Like "Fio*" Or BSO.Base.Artigos.Edita(Me.DocumentoCompra.Linhas.GetEdita(i).Artigo).Descricao Like "Rama*") Then
        '                            CopiaLote(Me.DocumentoCompra.Linhas.GetEdita(i).Artigo, Me.DocumentoCompra.Linhas.GetEdita(i).Lote)
        '                        End If
        '                    End If
        '                Next i
        '            End If
        '        End If
        '    End If
        'End Sub

        'Public Function CopiaLote(ByVal str_Artigo As String, ByVal str_Lote As String)



        '    If str_Lote = "" Then Exit Function

        '    If BSO.Inventario.ArtigosLotes.Existe(str_Artigo, str_Lote) = False Then

        '        Dim ArtigoLote As New InvBE100.InvBEArtigoLote

        '        Dim stdBE_ListaLote As StdBELista
        '        stdBE_ListaLote = BSO.Consulta(" SELECT * FROM ArtigoLote " &
        '                                             " WHERE Artigo = '" & str_Artigo & "' " &
        '                                             " AND Lote = '" & str_Lote & "'")

        '        If Not stdBE_ListaLote.Vazia Then

        '            stdBE_ListaLote.Inicio()

        '            ArtigoLote.Artigo = stdBE_ListaLote.Valor("Artigo")
        '            ArtigoLote.Lote = stdBE_ListaLote.Valor("Lote")
        '            ArtigoLote.Descricao = BSO.Contexto.CodEmp & ", " & BSO.Contexto.UtilizadorActual
        '            If Len(stdBE_ListaLote.Valor("DataFabrico")) > 0 Then ArtigoLote.DataFabrico = stdBE_ListaLote.Valor("DataFabrico")
        '            If Len(stdBE_ListaLote.Valor("Validade")) > 0 Then ArtigoLote.Validade = stdBE_ListaLote.Valor("Validade")
        '            ArtigoLote.Controlador = stdBE_ListaLote.Valor("Controlador")
        '            ArtigoLote.Activo = stdBE_ListaLote.Valor("Activo")
        '            ArtigoLote.Observacoes = stdBE_ListaLote.Valor("Observacoes")
        '            ArtigoLote.CamposUtil("CDU_TipoQualidade").Valor = stdBE_ListaLote.Valor("CDU_TipoQualidade")
        '            ArtigoLote.CamposUtil("CDU_Parafinado").Valor = stdBE_ListaLote.Valor("CDU_Parafinado")
        '            ArtigoLote.CamposUtil("CDU_LoteForn").Valor = stdBE_ListaLote.Valor("CDU_LoteForn")
        '            ArtigoLote.CamposUtil("CDU_Fornecedor").Valor = stdBE_ListaLote.Valor("CDU_Fornecedor")
        '            BSO.Inventario.ArtigosLotes.Actualiza(ArtigoLote)

        '        End If

        '    End If
        'End Function


    End Class
End Namespace