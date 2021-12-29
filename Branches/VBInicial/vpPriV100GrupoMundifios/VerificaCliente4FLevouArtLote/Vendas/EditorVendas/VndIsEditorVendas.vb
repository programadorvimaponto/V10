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

Namespace VerificaCliente4FLevouArtLote

    Public Class VndIsEditorVendas
        Inherits EditorVendas

        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

            If Module1.VerificaToken("VerificaCliente4FLevouArtLote") = 1 Then

                If Me.DocumentoVenda.Tipodoc = "GR" And Me.DocumentoVenda.Entidade = "0958" Then

                    VerificaCliente4FLevouArtLote

                End If
            End If

        End Sub

        Dim ListaCliLevouArtLote As StdBELista
        Dim SqlStringCliLevouArtLote As String
        'Variáveis para e-mail
        Dim VarFrom As String
        Dim VarTo As String
        Dim VarAssunto As String
        Dim VarTextoInicialMsg As String
        Dim VarMensagem As String
        Dim VarArmazem As String
        Dim VarLinhas As String
        Dim VarUtilizador As String
        Dim VarLocalTeste As Integer '0 - ao seleccionar o cliente; 1 - antes de gravar o documento(ECL ou GR)
        Dim VarCancelaDoc As Boolean
        Dim VarNetTrans As Boolean

        '###JFC pedido de Carina. Verificar se cliente 4F já levou o lote. Envia email a lembrar necessidade de enviar Caracteristicas Tecnicas.
        Private Function VerificaCliente4FLevouArtLote()

            For i = 1 To Me.DocumentoVenda.Linhas.NumItens

                If Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "" <> "<L01>" Then

                    SqlStringCliLevouArtLote = "SELECT dbo.CabecDoc.Entidade, dbo.LinhasDoc.Artigo, dbo.LinhasDoc.Lote " _
                                            & "FROM dbo.CabecDoc INNER JOIN dbo.LinhasDoc ON dbo.CabecDoc.Id = dbo.LinhasDoc.IdCabecDoc " _
                                            & "WHERE (dbo.CabecDoc.Tipodoc in ('FI', 'FA', 'FO', 'FIT')) and (dbo.LinhasDoc.Artigo = '" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "') AND (dbo.LinhasDoc.Lote = '" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "') AND (dbo.CabecDoc.Entidade = '" & Me.DocumentoVenda.Entidade & "')"

                    ListaCliLevouArtLote = BSO.Consulta(SqlStringCliLevouArtLote)

                    If ListaCliLevouArtLote.Vazia = True Then


                        VarFrom = ""
                        VarTo = "marketing@mundifios.pt;"

                        If Format(Now, "HH:mm") >= "07:00" And Format(Now, "HH:mm") <= "12:59" Then
                            VarTextoInicialMsg = "Bom dia,"
                        ElseIf Format(Now, "HH:mm") >= "13:00" And Format(Now, "HH:mm") <= "19:59" Then
                            VarTextoInicialMsg = "Boa tarde,"
                        Else
                            VarTextoInicialMsg = "Boa noite,"
                        End If

                        VarAssunto = "Novo lote: (" & Me.DocumentoVenda.Entidade & ") - " & Replace(BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).Nome, "'", "")

                        VarUtilizador = Aplicacao.Utilizador.Utilizador

                        VarMensagem = VarTextoInicialMsg & Chr(13) & Chr(13) & Chr(13) & "Foi emitido uma Guia com um lote novo para o cliente, pfv enviar caracteristicas tecnicas:" & Chr(13) & Chr(13) & "" _
                                    & "Empresa:                         " & BSO.Contexto.CodEmp & " - " & BSO.Contexto.IDNome & Chr(13) & "" _
                                    & "Utilizador:                      " & VarUtilizador & Chr(13) & Chr(13) & "" _
                                    & "Cliente:                         " & Me.DocumentoVenda.Entidade & " - " & Replace(BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).Nome, "'", "") & Chr(13) & "" _
                                    & "Documento:                       " & Me.DocumentoVenda.Tipodoc & " " & Format(Me.DocumentoVenda.NumDoc, "#,###") & "/" & Me.DocumentoVenda.Serie & Chr(13) & "" _
                                    & "Artigo:                           " & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & Chr(13) & "" _
                                    & "Desc:                             " & Me.DocumentoVenda.Linhas.GetEdita(i).Descricao & Chr(13) & "" _
                                    & "Lote:                             " & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & Chr(13) & "" _
                                    & "Cumprimentos"


                        BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL]  ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "', '" & VarFrom & "', '" & VarTo & "','','','" & VarAssunto & "','" & VarMensagem & "','',0,'" & VarUtilizador & "' )")


                    End If
                End If

            Next i


        End Function



    End Class
End Namespace