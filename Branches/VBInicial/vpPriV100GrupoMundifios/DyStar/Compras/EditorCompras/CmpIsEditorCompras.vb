Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Primavera.Extensibility.Purchases.Editors
Imports Primavera.Extensibility.Attributes
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports StdBE100
Imports BasBE100.BasBETiposGcp

Namespace DyStar
    Public Class CmpIsEditorCompras
        Inherits EditorCompras

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("DyStar") = 1 Then

                '#######################################################################################################################################################
                '#Adiciona na ultima linha de artigos um comentário se os fornecedore forem DyStar ou TCC. Bruno - 08/08/2019, tem de ser no fim de todas as condições #
                '#######################################################################################################################################################

                If ((Me.DocumentoCompra.Entidade = "1809") Or (Me.DocumentoCompra.Entidade = "1812")) And (Me.DocumentoCompra.Tipodoc = "ECF") Then
                    If Not BSO.Compras.Documentos.Existe(Me.DocumentoCompra.Filial, Me.DocumentoCompra.Tipodoc, Me.DocumentoCompra.Serie, Me.DocumentoCompra.NumDoc) Then
                        BSO.Compras.Documentos.AdicionaLinhaEspecial(Me.DocumentoCompra, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "Os artigos incluídos nesta ECF devem ser entregues na sua embalagem original, com o rótulo original, incluindo o nome do produto, o nome do fabricante/distribuidor e número do lote do produto químico.")
                    End If
                End If

                '#######################################################################################################################################################
                '#Adiciona na ultima linha de artigos um comentário se os fornecedore forem DyStar ou TCC. Bruno - 08/08/2019, tem de ser no fim de todas as condições #
                '#######################################################################################################################################################

            End If

        End Sub

    End Class
End Namespace
