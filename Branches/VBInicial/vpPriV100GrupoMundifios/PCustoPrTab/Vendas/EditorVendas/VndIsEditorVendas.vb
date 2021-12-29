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

Namespace PCustoPrTab
    Public Class VndIsEditorVendas
        Inherits EditorVendas
        Dim NovaEncomenda As Boolean
        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

            If Module1.VerificaToken("PCustoPrTab") = 1 Then

                '*******************************************************************************************************************************************
                '#### Verifica e envia email se uma GR ou ECL for criada com o preço unitario inferior ao preço de custo - Bruno 05/02/2020 ####
                '*******************************************************************************************************************************************
                If (Me.DocumentoVenda.Tipodoc = "ECL" And Right(Me.DocumentoVenda.Serie, 1) <> "B" And NovaEncomenda = True) Then

                    VerificaPrecoAbaixoCustoEEnviaEmail()
                    VerificaPrecoCustoEEnviaEmail()
                End If
                '*******************************************************************************************************************************************
                '#### Verifica e envia email se uma GR ou ECL for criada com o preço unitario inferior ao preço de custo - Bruno 05/02/2020 ####
                '*******************************************************************************************************************************************


                '#################################################################################################
                '####### Coloca Pr. Tabela na Linha da ECL. Pedido de Mafalda - JFC 11-09-2020        ############
                '#################################################################################################


                If Me.DocumentoVenda.Tipodoc = "ECL" Then
                    Dim prTab As StdBELista
                    Dim PrecTab As Double


                    For j = 1 To Me.DocumentoVenda.Linhas.NumItens

                        If Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(j).Lote & "" <> "" Then

                            'Se a linha não tiver Pr. Tabela atribuido, atribuir preço atual. 99 - Nunca foi atribuido preço, 0 - Já houve tentativa mas não havia preço.
                            If IsNothing(Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_PrecTab")) Or Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_PrecTab").Valor = 99 Then

                                prTab = BSO.Consulta("select primundifios.dbo.VMP_IEXF_DaPrecoTabela('" & Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & "','" & Me.DocumentoVenda.Linhas.GetEdita(j).Lote & "',3) as 'PrecTab'")

                                If prTab.Vazia = False Then
                                    prTab.Inicio()
                                    PrecTab = prTab.Valor("PrecTab")
                                    '  Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_PrecTab") = prTab("PrecTab")
                                    BSO.DSO.ExecuteSQL("update ln set ln.CDU_PrecTab=replace('" & PrecTab & "',',','.') from LinhasDoc ln where ln.Id='" & Me.DocumentoVenda.Linhas.GetEdita(j).IdLinha & "'")
                                    '   MsgBox "" & Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_PrecTab") & " - " & prTab("PrecTab")
                                Else
                                    ' Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_PrecTab") = 0
                                    BSO.DSO.ExecuteSQL("update ln set ln.CDU_PrecTab=0 from LinhasDoc ln where ln.Id=" & Me.DocumentoVenda.Linhas.GetEdita(j).IdLinha & "")

                                End If


                            End If
                        End If

                    Next j
                End If
                '#################################################################################################
                '####### Coloca Pr. Tabela na Linha da ECL. Pedido de Mafalda - JFC 11-09-2020        ############
                '#################################################################################################

            End If

        End Sub
        Dim VarCliente As String
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
        '*******************************************************************************************************************************************
        '#### Bruno - Verifica preço de custo para enviar email #### - 05/02/2020
        '#### JFC   - Revisão do código                         #### - 10/05/2021
        '*******************************************************************************************************************************************
        Dim ListaPCU As StdBELista
        Dim SqlStringPCU As String
        Dim ListaPTB As StdBELista
        Private Function VerificaPrecoAbaixoCustoEEnviaEmail()
            Dim i As Long
            Dim ln As Long
            Dim enviarmail As Boolean
            Dim qualidade As String
            Dim PrUnit, PCusto As Double
            Dim Header, Footer, Table, Nome As String
            VarCliente = Me.DocumentoVenda.Entidade
            Nome = Me.DocumentoVenda.Nome
            If Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupoNome").Valor & "" <> "" Then
                VarCliente = Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupo").Valor
                Nome = Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupoNome").Valor
            End If

            Header = "<html><head><style>" &
                 "td {border: solid black;border-width: 1px;padding-left:5px;padding-right:5px;padding-top:1px;padding-bottom:1px;font: 11px arial} " &
                 "</style></head><body><b>Mundifios, S.A.</b> <br>"


            VarFrom = ""

            VarTo = "export1@mundifios.pt; angelo@mundifios.pt; informatica@mundifios.pt"

            If Format(Now, "HH:mm") >= "07:00" And Format(Now, "HH:mm") <= "12:59" Then
                VarTextoInicialMsg = "Bom dia,"
                Header = Header & "<br>Bom dia,<br><br>Foi lançado um documento no Primavera onde o preço unitário é inferior ao preço de custo<br>"
            ElseIf Format(Now, "HH:mm") >= "13:00" And Format(Now, "HH:mm") <= "19:59" Then
                VarTextoInicialMsg = "Boa tarde,"
                Header = Header & "<br>Boa tarde,<br><br>Foi lançado um documento no Primavera onde o preço unitário é inferior ao preço de custo<br>"
            Else
                VarTextoInicialMsg = "Boa noite,"
                Header = Header & "<br>Boa noite,<br><br>Foi lançado um documento no Primavera onde o preço unitário é inferior ao preço de custo<br>"
            End If

            VarAssunto = "[Preço de Custo]Documento: " & Me.DocumentoVenda.Tipodoc & " " & Format(Me.DocumentoVenda.NumDoc, "####") & "/" & Me.DocumentoVenda.Serie & " (" & Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") & ")"

            VarUtilizador = Aplicacao.Utilizador.Utilizador

            VarMensagem = VarTextoInicialMsg & Chr(13) & Chr(13) & Chr(13) & "Foi lançado um documento no Primavera onde o preço unitário é inferior ao preço de custo" & Chr(13) & Chr(13) & "" _
                          & "Empresa:                         " & BSO.Contexto.CodEmp & " - " & BSO.Contexto.IDNome & Chr(13) & "" _
                          & "Utilizador:                      " & VarUtilizador & Chr(13) & Chr(13) & "" _
                          & "Cliente:                         " & VarCliente & " - " & Replace(Nome, "'", "") & Chr(13) & ""



            Header = Header & "<br>" &
                         "<b>Empresa:</b>                         " & BSO.Contexto.CodEmp & " - " & BSO.Contexto.IDNome & "<br>" &
                         "<b>Utilizador:</b>                      " & VarUtilizador & "<br><br>" &
                         "<b>Cliente:</b>                         " & VarCliente & " - " & Replace(Nome, "'", "") & "<br>"


            VarLinhas = ""
            Table = "<table cellpadding=0 cellspacing=0 border=0>" &
                     "<td bgcolor=#72C6FF><b>Artigo</b></td>" &
                     "<td bgcolor=#72C6FF><b>Lote</b></td>" &
                     "<td bgcolor=#72C6FF><b>T.Qual</b></td>" &
                     "<td bgcolor=#72C6FF><b>Descrição</b></td>" &
                     "<td bgcolor=#72C6FF><b>Qtd</b></td>" &
                     "<td bgcolor=#72C6FF><b>Preço</b></td>" &
                     "<td bgcolor=#FF5019><b>PCusto</b></td>" &
                     "<td bgcolor=#FF5019><b>PrTab</b></td></tr>"

            enviarmail = False
            ''Verifica se o preço de custo é superior ao Unitario
            For i = 1 To Me.DocumentoVenda.Linhas.NumItens

                qualidade = "--"
                If Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_TipoQualidade").Valor = "002" Then
                    qualidade = "c/Gar. p/Branco"
                End If
                If Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_TipoQualidade").Valor = "003" Then
                    qualidade = "Baixa Contamin."
                End If

                If Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "" <> "" Then
                    SqlStringPCU = "select primundifios.dbo.VMP_IEXF_DaPrecoCusto ('" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "','" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "','3') as 'PCusto'"
                    ListaPCU = BSO.Consulta(SqlStringPCU)
                    If ListaPCU.Vazia = False Then
                        ListaPCU.Inicio()
                        PCusto = ListaPCU.Valor("PCusto")
                        '1 - definir preçounitário (prunit+margempassagem ou precobase, / cambio)
                        PrUnit = 0
                        If (Me.DocumentoVenda.Pais <> "PT" And (IsNothing(Me.DocumentoVenda.CamposUtil("CDU_IdiomaEntidadeFinalGrupo").Valor) Or Me.DocumentoVenda.CamposUtil("CDU_IdiomaEntidadeFinalGrupo").Valor <> "PT")) Then
                            PrUnit = Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_PrecoBase").Valor / Me.DocumentoVenda.Cambio
                        Else
                            PrUnit = (Me.DocumentoVenda.Linhas.GetEdita(i).PrecUnit + Me.DocumentoVenda.CamposUtil("CDU_MargemPassagemArtigo").Valor) / Me.DocumentoVenda.Cambio
                        End If

                        '2 - confirmar armazem, se aep descontar valor no pcusto

                        If Me.DocumentoVenda.Linhas.GetEdita(i).Armazem = "AEP" Then
                            Dim sql As String
                            Dim ListSQL As StdBELista

                            sql = "select top 1 (le.CDU_CustoValor) as 'Direitos' " &
                                    "from TDU_CabecCustosEncomendas ce " &
                                    "inner join TDU_LinhasCustosEncomenda le on le.CDU_Serie=ce.CDU_Serie and le.CDU_NumDoc=ce.CDU_NumDoc and le.CDU_TipoDoc=ce.CDU_TipoDoc and le.CDU_NumLinha=ce.CDU_NumLinha " &
                                    "inner join CabecCompras cc2 on cc2.TipoDoc=ce.CDU_TipoDoc and cc2.NumDoc=ce.CDU_NumDoc and cc2.Serie=ce.CDU_Serie " &
                                    "where cc2.DataDoc <= '" & Format(Me.DocumentoVenda.DataDoc, "yyyy-MM-dd HH:mm:ss") & "'" &
                                    "and ce.CDU_Artigo='" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "'" &
                                    "and ce.CDU_Lote='" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "'" &
                                    "and ce.CDU_TipoDoc='ECF' " &
                                    "and le.CDU_Descricao like '%Custos Alfandegarios%' "
                            ListSQL = BSO.Consulta(sql)
                            If ListSQL.Vazia = False Then
                                ListSQL.Inicio()
                                PCusto = PCusto - ListSQL.Valor("Direitos")
                            End If

                        End If
                        '3 - comparar as duas variaveis

                        If PrUnit <= PCusto Then
                            enviarmail = True
                            ListaPTB = BSO.Consulta("select primundifios.dbo.VMP_IEXF_DaPrecoTabela ('" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "','" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "','3') as 'PrecoTab'")
                            ListaPTB.Inicio()
                            VarLinhas = VarLinhas & "Linha " & i & ": " & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "-" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & " - Desc:" & Me.DocumentoVenda.Linhas.GetEdita(i).Descricao & " - Qtd:" & Me.DocumentoVenda.Linhas.GetEdita(i).Quantidade & "  - Preço Unit:" & PrUnit & "  - Preço Custo:" & PCusto & "  - Preço Tabela:" & ListaPTB.Valor("PrecoTab") & "  - Tipo de qualidade:" & qualidade & Chr(13) & ""


                            Table = Table & "<tr>" &
                                    "<td>" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "</td>" &
                                    "<td>" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "</td>" &
                                    "<td>" & qualidade & "</td>" &
                                    "<td>" & Me.DocumentoVenda.Linhas.GetEdita(i).Descricao & "</td>" &
                                    "<td>" & Me.DocumentoVenda.Linhas.GetEdita(i).Quantidade & Me.DocumentoVenda.Linhas.GetEdita(i).Unidade & "</td>" &
                                    "<td>" & PrUnit & "</td>" &
                                    "<td>" & PCusto & "</td>" &
                                    "<td>" & ListaPTB.Valor("PrecoTab") & "</td></tr>"
                        End If
                    End If
                End If
            Next i

            Footer = "</table><br><br></body></html>"


            VarMensagem = Header & Table & Footer

            ''Envia emailcom indicacao de preço custo
            If enviarmail = True Then

                BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL]  ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "', '" & VarFrom & "', '" & VarTo & "','','','" & VarAssunto & "','" & VarMensagem & "','',0,'" & VarUtilizador & "' )")


            End If


        End Function


        '*******************************************************************************************************************************************
        '#### JFC - Verifica se não há preço de custo e envia email #### - 10/05/2021
        '*******************************************************************************************************************************************
        Private Function VerificaPrecoCustoEEnviaEmail()
            Dim i As Long
            Dim ln As Long
            Dim enviarmail As Boolean
            Dim qualidade As String
            Dim PrUnit, PCusto As Double
            Dim Header, Footer, Table, Nome As String
            VarCliente = Me.DocumentoVenda.Entidade
            Nome = Me.DocumentoVenda.Nome
            If Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupoNome").Valor & "" <> "" Then
                VarCliente = Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupo").Valor
                Nome = Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupoNome").Valor
            End If

            Header = "<html><head><style>" &
                 "td {border: solid black;border-width: 1px;padding-left:5px;padding-right:5px;padding-top:1px;padding-bottom:1px;font: 11px arial} " &
                 "</style></head><body><b>Mundifios, S.A.</b> <br>"


            VarFrom = ""

            VarTo = "mafaldamachado@mundifios.pt; suporte@mundifios.pt; informatica@mundifios.pt"

            If Format(Now, "HH:mm") >= "07:00" And Format(Now, "HH:mm") <= "12:59" Then
                VarTextoInicialMsg = "Bom dia,"
                Header = Header & "<br>Bom dia,<br><br>Foi lançado um documento no Primavera onde o preço custo é 0<br>"
            ElseIf Format(Now, "HH:mm") >= "13:00" And Format(Now, "HH:mm") <= "19:59" Then
                VarTextoInicialMsg = "Boa tarde,"
                Header = Header & "<br>Boa tarde,<br><br>Foi lançado um documento no Primavera onde o preço custo é 0<br>"
            Else
                VarTextoInicialMsg = "Boa noite,"
                Header = Header & "<br>Boa noite,<br><br>Foi lançado um documento no Primavera onde o preço custo é 0<br>"
            End If

            VarAssunto = "[PCusto 0]Documento: " & Me.DocumentoVenda.Tipodoc & " " & Format(Me.DocumentoVenda.NumDoc, "####") & "/" & Me.DocumentoVenda.Serie & " (" & Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") & ")"

            VarUtilizador = Aplicacao.Utilizador.Utilizador

            VarMensagem = VarTextoInicialMsg & Chr(13) & Chr(13) & Chr(13) & "Foi lançado um documento no Primavera onde o preço unitário é inferior ao preço de custo" & Chr(13) & Chr(13) & "" _
                          & "Empresa:                         " & BSO.Contexto.CodEmp & " - " & BSO.Contexto.IDNome & Chr(13) & "" _
                          & "Utilizador:                      " & VarUtilizador & Chr(13) & Chr(13) & "" _
                          & "Cliente:                         " & VarCliente & " - " & Replace(Nome, "'", "") & Chr(13) & ""



            Header = Header & "<br>" &
                         "<b>Empresa:</b>                         " & BSO.Contexto.CodEmp & " - " & BSO.Contexto.IDNome & "<br>" &
                         "<b>Utilizador:</b>                      " & VarUtilizador & "<br><br>" &
                         "<b>Cliente:</b>                         " & VarCliente & " - " & Replace(Nome, "'", "") & "<br>"


            VarLinhas = ""
            Table = "<table cellpadding=0 cellspacing=0 border=0>" &
                     "<td bgcolor=#72C6FF><b>Artigo</b></td>" &
                     "<td bgcolor=#72C6FF><b>Lote</b></td>" &
                     "<td bgcolor=#72C6FF><b>T.Qual</b></td>" &
                     "<td bgcolor=#72C6FF><b>Descrição</b></td>" &
                     "<td bgcolor=#72C6FF><b>Qtd</b></td>" &
                     "<td bgcolor=#72C6FF><b>Preço</b></td>" &
                     "<td bgcolor=#FF5019><b>PCusto</b></td>" &
                     "<td bgcolor=#FF5019><b>PrTab</b></td></tr>"

            enviarmail = False
            ''Verifica se o preço de custo é superior ao Unitario
            For i = 1 To Me.DocumentoVenda.Linhas.NumItens

                qualidade = "--"
                If Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_TipoQualidade").Valor = "002" Then
                    qualidade = "c/Gar. p/Branco"
                End If
                If Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_TipoQualidade").Valor = "003" Then
                    qualidade = "Baixa Contamin."
                End If

                If Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "" <> "" Then
                    SqlStringPCU = "select primundifios.dbo.VMP_IEXF_DaPrecoCusto ('" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "','" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "','3') as 'PCusto'"
                    ListaPCU = BSO.Consulta(SqlStringPCU)
                    If ListaPCU.Vazia = False Then
                        ListaPCU.Inicio()
                        PCusto = ListaPCU.Valor("PCusto")
                        '1 - definir preçounitário (prunit+margempassagem ou precobase, / cambio)
                        PrUnit = 0
                        If (Me.DocumentoVenda.Pais <> "PT" And (IsNothing(Me.DocumentoVenda.CamposUtil("CDU_IdiomaEntidadeFinalGrupo").Valor) Or Me.DocumentoVenda.CamposUtil("CDU_IdiomaEntidadeFinalGrupo").Valor <> "PT")) Then
                            PrUnit = Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_PrecoBase").Valor / Me.DocumentoVenda.Cambio
                        Else
                            PrUnit = (Me.DocumentoVenda.Linhas.GetEdita(i).PrecUnit + Me.DocumentoVenda.CamposUtil("CDU_MargemPassagemArtigo").Valor) / Me.DocumentoVenda.Cambio
                        End If


                        '2 - verifica se o PCusto é 0
                        If PCusto = 0 Then
                            enviarmail = True
                            ListaPTB = BSO.Consulta("select primundifios.dbo.VMP_IEXF_DaPrecoTabela ('" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "','" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "','3') as 'PrecoTab'")
                            ListaPTB.Inicio()
                            VarLinhas = VarLinhas & "Linha " & i & ": " & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "-" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & " - Desc:" & Me.DocumentoVenda.Linhas.GetEdita(i).Descricao & " - Qtd:" & Me.DocumentoVenda.Linhas.GetEdita(i).Quantidade & "  - Preço Unit:" & PrUnit & "  - Preço Custo:" & PCusto & "  - Preço Tabela:" & ListaPTB.Valor("PrecoTab") & "  - Tipo de qualidade:" & qualidade & Chr(13) & ""


                            Table = Table & "<tr>" &
                                    "<td>" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "</td>" &
                                    "<td>" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "</td>" &
                                    "<td>" & qualidade & "</td>" &
                                    "<td>" & Me.DocumentoVenda.Linhas.GetEdita(i).Descricao & "</td>" &
                                    "<td>" & Me.DocumentoVenda.Linhas.GetEdita(i).Quantidade & Me.DocumentoVenda.Linhas.GetEdita(i).Unidade & "</td>" &
                                    "<td>" & PrUnit & "</td>" &
                                    "<td>" & PCusto & "</td>" &
                                    "<td>" & ListaPTB.Valor("PrecoTab") & "</td></tr>"
                        End If
                    End If
                End If
            Next i

            Footer = "</table><br><br></body></html>"


            VarMensagem = Header & Table & Footer

            ''Envia email com indicacao de preço custo
            If enviarmail = True Then

                BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL] ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "', '" & VarFrom & "', '" & VarTo & "','','','" & VarAssunto & "','" & VarMensagem & "','',0,'" & VarUtilizador & "' )")

            End If


        End Function



    End Class
End Namespace
