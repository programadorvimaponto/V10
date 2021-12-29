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

Namespace VerificaTesteQuimico

    Public Class VndIsEditorVendas
        Inherits EditorVendas

        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

            If Module1.VerificaToken("VerificaTesteQuimico") = 1 Then
                '*******************************************************************************************************************************************
                '#### Verificar se TesteQuimico foi realizado - Pedido de Vitor Passos 26/07/2018 (JFC) ####
                '*******************************************************************************************************************************************

                Dim lista2 As StdBELista
                Dim malha As Boolean

                If (Me.DocumentoVenda.Tipodoc = "GR") Then

                    malha = False
                    For ln2 = 1 To Me.DocumentoVenda.Linhas.NumItens

                        lista2 = BSO.Consulta("select * from TDU_LaboratorioLote lb where lb.CDU_RSSitFinFio='MALHA' and lb.CDU_CodArtigo='" & Me.DocumentoVenda.Linhas.GetEdita(ln2).Artigo & "' and lb.CDU_LoteArt='" & Me.DocumentoVenda.Linhas.GetEdita(ln2).Lote & "'")

                        If (lista2.Vazia = False) Then
                            malha = True
                        End If
                    Next ln2



                    If malha = True Then
                        EnvioEmailLab()
                    End If




                End If

                '*******************************************************************************************************************************************
                '#### Verificar se TesteQuimico foi realizado - Pedido de Vitor Passos 26/07/2018 (JFC)                             ####
                '*******************************************************************************************************************************************
            End If
        End Sub

        'Variáveis para e-mail
        Dim VarFrom As String
        Dim VarTo As String
        Dim VarAssunto As String
        Dim VarTextoInicialMsg As String
        Dim VarMensagem As String
        Dim VarLinhas As String
        Dim VarUtilizador As String
        Dim VarCliente As String
        Private Function EnvioEmailLab()



            '*******************************************************************************************************************************************
            '#### Enviar Mail para Vitor Passos - Pedido de Vitor Passos 26/07/2018 (JFC) ####
            '*******************************************************************************************************************************************
            VarCliente = Me.DocumentoVenda.Entidade
            Dim ln As Long
            VarFrom = ""

            VarTo = "informatica@mundifios.pt; vitorpassos@mundifios.pt"

            If Format(Now, "HH:mm") >= "07:00" And Format(Now, "HH:mm") <= "12:59" Then
                VarTextoInicialMsg = "Bom dia,"
            ElseIf Format(Now, "HH:mm") >= "13:00" And Format(Now, "HH:mm") <= "19:59" Then
                VarTextoInicialMsg = "Boa tarde,"
            Else
                VarTextoInicialMsg = "Boa noite,"
            End If

            VarAssunto = "(Malha) Guia de Remessa: " & Format(Me.DocumentoVenda.NumDoc, "####") & "/" & Me.DocumentoVenda.Serie & " (" & Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") & ")"

            VarUtilizador = Aplicacao.Utilizador.Utilizador

            VarLinhas = ""
            For ln = 1 To Me.DocumentoVenda.Linhas.NumItens

                VarLinhas = VarLinhas & "Linha " & ln & ":                         " & Me.DocumentoVenda.Linhas.GetEdita(ln).Artigo & " - Armazem:" & Me.DocumentoVenda.Linhas.GetEdita(ln).Armazem & " - Lote:" & Me.DocumentoVenda.Linhas.GetEdita(ln).Lote & " - Desc:" & Me.DocumentoVenda.Linhas.GetEdita(ln).Descricao & " - Quantidade:" & Me.DocumentoVenda.Linhas.GetEdita(ln).Quantidade & Me.DocumentoVenda.Linhas.GetEdita(ln).Unidade & Chr(13) & ""


            Next ln


            VarMensagem = VarTextoInicialMsg & Chr(13) & Chr(13) & Chr(13) & "Foi emitido uma Guia de Remessa no Primavera, pedir malha ao cliente:" & Chr(13) & Chr(13) & "" _
                    & "Empresa:                         " & BSO.Contexto.CodEmp & " - " & BSO.Contexto.IDNome & Chr(13) & "" _
                    & "Utilizador:                      " & VarUtilizador & Chr(13) & Chr(13) & "" _
                    & "Cliente:                         " & VarCliente & " - " & Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") & Chr(13) & "" _
                    & "Documento:                       " & Me.DocumentoVenda.Tipodoc & " " & Format(Me.DocumentoVenda.NumDoc, "#,###") & "/" & Me.DocumentoVenda.Serie & Chr(13) & Chr(13) & "" _
                    & "Local Descarga:                  " & Me.DocumentoVenda.LocalDescarga & Chr(13) & "" _
                    & "Morada Entrega:                  " & Replace(Me.DocumentoVenda.MoradaEntrega, "'", "") & Chr(13) & Chr(13) & "" _
                    & VarLinhas & Chr(13) & "" _
                    & "Cumprimentos"


            BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL] ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "', '" & VarFrom & "', '" & VarTo & "','','','" & VarAssunto & "','" & VarMensagem & "','',0,'" & VarUtilizador & "' )")



        End Function
        '*******************************************************************************************************************************************
        '#### Enviar Mail para Vitor Passos - Pedido de Vitor Passos 26/07/2018 (JFC) ####
        '*******************************************************************************************************************************************


    End Class
End Namespace
