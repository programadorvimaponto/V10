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
Namespace Inovafil
    Public Class VndIsEditorVendas
        Inherits EditorVendas
        Dim AspectoMalha As Boolean
        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("Inovafil") = 1 Then

                '#########################################################################################################################
                '##Verifica se na Ficha Laboratorio/Lote o campo Aspecto da Malha está preenchido. A pedido de Dr.Rita - JFC 27/10/2017 ##
                '#########################################################################################################################
                If Me.DocumentoVenda.Tipodoc = "GR" Then
                    AspectoMalha = True

                    ValidaMalha



                    If AspectoMalha = False Then

                        MsgBox("Atenção:" & Chr(13) & "Aspecto da malha não está preenchido.", vbCritical + vbOKOnly)
                        Cancel = True
                        Exit Sub
                    End If
                End If
                '#########################################################################################################################
                '##Verifica se na Ficha Laboratorio/Lote o campo Aspecto da Malha está preenchido. A pedido de Dr.Rita - JFC 27/10/2017 ##
                '#########################################################################################################################

                'JFC 14/09/2020 Colocar a Carga com a morada da Fiação.
                Me.DocumentoVenda.CargaDescarga.MoradaCarga = "Rua Comendador Manuel Gonçalves nº 25"
                Me.DocumentoVenda.CargaDescarga.Morada2Carga = ""
                Me.DocumentoVenda.CargaDescarga.LocalidadeCarga = "S Cosme do Vale"
                Me.DocumentoVenda.CargaDescarga.DistritoCarga = "03"
                Me.DocumentoVenda.CargaDescarga.PaisCarga = "PT"
                Me.DocumentoVenda.CargaDescarga.CodPostalCarga = "4770-583"
                Me.DocumentoVenda.CargaDescarga.CodPostalLocalidadeCarga = "V N Famalicão"
            End If

        End Sub

        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)


            If Module1.VerificaToken("Inovafil") = 1 Then
                '*******************************************************************************************************************************************
                '#### Enviar E-mail para o laboratorio para validar percentagens das mesclas. JFC - 20/11/2018
                '*******************************************************************************************************************************************

                If Me.DocumentoVenda.Tipodoc = "ECL" Then

                    EnviaMailPreMescla

                End If

                '*******************************************************************************************************************************************
                '#### Enviar E-mail para o laboratorio para validar percentagens das mesclas. JFC - 20/11/2018
                '*******************************************************************************************************************************************


                '*******************************************************************************************************************************************
                '#### Atualizar o Armazem Stock Service na Mundifios 14/12/2018 - JFC
                '*******************************************************************************************************************************************

                Dim AtualizaSS As Boolean
                AtualizaSS = False
                For j = 1 To Me.DocumentoVenda.Linhas.NumItens

                    If Me.DocumentoVenda.Linhas.GetEdita(j).Armazem = "INO" Then
                        AtualizaSS = True

                    End If

                Next j

                If AtualizaSS = True Then
                    BSO.DSO.ExecuteSQL("exec priinovafil.dbo.spAtualizaSS")
                End If

                '*******************************************************************************************************************************************
                '#### Atualizar o Armazem Stock Service na Mundifios 14/12/2018 - JFC
                '*******************************************************************************************************************************************
            End If
        End Sub

        Public Overrides Sub DepoisDeTransformar(e As ExtensibilityEventArgs)
            MyBase.DepoisDeTransformar(e)

            If Module1.VerificaToken("Inovafil") = 1 Then
                'JFC 14/09/2020 Colocar a Carga com a morada da Fiação.
                If BSO.Vendas.TabVendas.Edita(Me.DocumentoVenda.Tipodoc).TipoDocumento = "3" Then

                    Me.DocumentoVenda.CargaDescarga.MoradaCarga = "Rua Comendador Manuel Gonçalves nº 25"
                    Me.DocumentoVenda.CargaDescarga.Morada2Carga = ""
                    Me.DocumentoVenda.CargaDescarga.LocalidadeCarga = "S Cosme do Vale"
                    Me.DocumentoVenda.CargaDescarga.DistritoCarga = "03"
                    Me.DocumentoVenda.CargaDescarga.PaisCarga = "PT"
                    Me.DocumentoVenda.CargaDescarga.CodPostalCarga = "4770-583"
                    Me.DocumentoVenda.CargaDescarga.CodPostalLocalidadeCarga = "V N Famalicão"

                End If

                Dim linha As Long
                For linha = 1 To Me.DocumentoVenda.Linhas.NumItens

                    Me.DocumentoVenda.Linhas.GetEdita(linha).Quantidade = Math.Round(Me.DocumentoVenda.Linhas.GetEdita(linha).Quantidade, 2)

                Next linha


                '*******************************************************************************************************************************************
                '### Adicionado por Bruno 14/10/2019 ###depois de transformar, validar se o documento final é uma GR, se sim colocar o campo LinhasDoc.CDU_Observacoes a vazio
                '*******************************************************************************************************************************************

                If Me.DocumentoVenda.Tipodoc = "GR" Then
                    For linha = 1 To Me.DocumentoVenda.Linhas.NumItens
                        ' BSO.DSO.BDAPL.Execute ("UPDATE LinhasDoc(linhai) SET CDU_Obeservacopes = "" where  WHERE IDLINHASDOC = '")
                        Me.DocumentoVenda.Linhas.GetEdita(linha).CamposUtil("cdu_observacoes").Valor = ""

                    Next linha
                End If

            End If

        End Sub


        Public Overrides Sub ValidaLinha(NumLinha As Integer, e As ExtensibilityEventArgs)
            MyBase.ValidaLinha(NumLinha, e)

            If Module1.VerificaToken("Inovafil") = 1 Then
                '#################################################################
                '##Verificação de Artigo em Stock - Pedido Dra. Rita -22/03/2019##
                '#################################################################


                If Me.DocumentoVenda.Tipodoc = "ECL" And Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Armazem <> "PLA" Then
                    Dim j As Long
                    Dim sqlArtigoStock As StdBELista
                    Dim strArtigoStock As String

                    strArtigoStock = "Atenção! Artigo em Stock" & Chr(13) & Chr(13) & " -     Artigo       -   Lote   - Arm - Stock"

                    sqlArtigoStock = BSO.Consulta("select top 1 * from ArtigoArmazem aa where aa.StkActual>'0' and aa.Artigo='" & Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo & "' and aa.Lote='" & Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Lote & "' and aa.Armazem='" & Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Armazem & "'")
                    If sqlArtigoStock.Vazia = True Then

                        sqlArtigoStock = BSO.Consulta("select aa.Artigo, aa.Lote, aa.Armazem, aa.StkActual from ArtigoArmazem aa where aa.StkActual>'0' and aa.Artigo='" & Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Artigo & "'")
                        If sqlArtigoStock.Vazia = False Then
                            sqlArtigoStock.Inicio()

                            For j = 1 To sqlArtigoStock.NumLinhas


                                strArtigoStock = strArtigoStock & Chr(13) & " - " & sqlArtigoStock.Valor("Artigo") & " - " & sqlArtigoStock.Valor("Lote") & " - " & sqlArtigoStock.Valor("Armazem") & " - " & sqlArtigoStock.Valor("StkActual") & " Kg"
                                sqlArtigoStock.Seguinte()
                            Next j
                            MsgBox(strArtigoStock, vbCritical + vbOKOnly)
                        End If

                    End If


                End If
                '#####################################################################
                '##Verificação de Artigo em Stock - Pedido Dra. Rita -22/03/2019 JFC##
                '#####################################################################


                Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Quantidade = Math.Round(Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Quantidade, 2)
            End If

        End Sub



        Dim QntECL As StdBELista
        Dim SqlQntECL As String
        Dim SqlStringArtLoteRestricoes As String
        Dim ListaArtLoteRestricoes As StdBELista
        Dim MsgErro As Integer
        Private Function ValidaMalha()
            Dim j As Long

            For j = 1 To Me.DocumentoVenda.Linhas.NumItens

                If Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(j).Lote & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(j).Lote & "" <> "<L01>" And Me.DocumentoVenda.Linhas.GetEdita(j).Armazem = "PA" Then

                    If Me.DocumentoVenda.Linhas.GetEdita(j).IDLinhaOriginal & "" <> "" Then
                        SqlQntECL = "select Quantidade from LinhasDoc where ID='" & Me.DocumentoVenda.Linhas.GetEdita(j).IDLinhaOriginal & "'"
                        QntECL = BSO.Consulta(SqlQntECL)

                        QntECL.Inicio

                        If QntECL.Valor("Quantidade") > 499 Then

                            SqlStringArtLoteRestricoes = "SELECT * FROM [TDU_LABORATORIOLOTE] " _
                                 & "WHERE cdu_codARTIGO = '" & Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & "' and cdu_loteart = '" & Me.DocumentoVenda.Linhas.GetEdita(j).Lote & "'"

                            ListaArtLoteRestricoes = BSO.Consulta(SqlStringArtLoteRestricoes)

                            If ListaArtLoteRestricoes.Vazia = False Then

                                ListaArtLoteRestricoes.Inicio

                                If ListaArtLoteRestricoes.Valor("CDU_TQAspMalha") & "" = "" Then

                                    AspectoMalha = False

                                    MsgErro = MsgBox("O Artigo: " & Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & ", Lote: " & Me.DocumentoVenda.Linhas.GetEdita(j).Lote & " não tem aspecto da malha preenchido!", vbInformation + vbOKOnly)

                                End If
                            Else

                                AspectoMalha = False
                                MsgErro = MsgBox("O Artigo: " & Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & ", Lote: " & Me.DocumentoVenda.Linhas.GetEdita(j).Lote & " não possui características técnicas", vbInformation + vbOKOnly)


                            End If
                        End If

                    Else

                        If Me.DocumentoVenda.Linhas.GetEdita(j).Quantidade > 499 Then

                            SqlStringArtLoteRestricoes = "SELECT * FROM [TDU_LABORATORIOLOTE] " _
                                 & "WHERE cdu_codARTIGO = '" & Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & "' and cdu_loteart = '" & Me.DocumentoVenda.Linhas.GetEdita(j).Lote & "'"

                            ListaArtLoteRestricoes = BSO.Consulta(SqlStringArtLoteRestricoes)

                            If ListaArtLoteRestricoes.Vazia = False Then

                                ListaArtLoteRestricoes.Inicio()

                                If ListaArtLoteRestricoes.Valor("CDU_TQAspMalha") & "" = "" Then

                                    AspectoMalha = False

                                    MsgErro = MsgBox("O Artigo: " & Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & ", Lote: " & Me.DocumentoVenda.Linhas.GetEdita(j).Lote & " não tem aspecto da malha preenchido!", vbInformation + vbOKOnly)




                                End If
                            Else
                                AspectoMalha = False
                                MsgErro = MsgBox("O Artigo: " & Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & ", Lote: " & Me.DocumentoVenda.Linhas.GetEdita(j).Lote & " não possui características técnicas", vbInformation + vbOKOnly)

                            End If
                        End If





                    End If

                End If



            Next j


        End Function

        Dim OpBalanca As StdBELista
        Dim SqlOpBalanca As String
        Dim VarMensagem As String
        Dim VarFrom As String
        Dim VarTo As String
        Dim VarAssunto As String
        Dim VarUtilizador As String
        Dim VarTextoInicialMsg As String
        Private Function EnviaMailPreMescla()
            Dim EnviaMail As Boolean
            Dim DescExtra As String
            Dim j As Long
            VarMensagem = ""
            EnviaMail = False
            For j = 1 To Me.DocumentoVenda.Linhas.NumItens

                If Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(j).Lote & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(j).Lote & "" <> "<L01>" And Me.DocumentoVenda.Linhas.GetEdita(j).Armazem = "PA" Then

                    DescExtra = BSO.Base.Artigos.DaValorAtributo(Me.DocumentoVenda.Linhas.GetEdita(j).Artigo, "CDU_DescricaoExtra")

                    If DescExtra Like "*JP*" Or DescExtra Like "*Injetado*" Or DescExtra Like "*Moulinet*" Or DescExtra Like "*FT*" Or DescExtra Like "*RB*" Or DescExtra Like "*Mosaic*" Then

                        EnviaMail = True
                        SqlOpBalanca = "select ob.PRD_IDOperacao, ao.PRD_Artigo, ob.Mescla, ob.Ordem, ob.Descricao, ob.Percentagem, ob.PercentagemMescla from VIM_ArtigoOperacoes ao " _
                                        & "inner join VMP_PLA_OpBalanca ob on  ob.PRD_IDOperacao=ao.PRD_IDOperacao " _
                                        & "where  ao.PRD_Operacao='10' and  ao.PRD_Artigo = '" & Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & "' " _
                                        & "order by ob.PRD_IDOperacao, ao.PRD_Artigo, ob.Mescla, ob.Ordem asc"
                        OpBalanca = BSO.Consulta(SqlOpBalanca)

                        OpBalanca.Inicio()

                        VarMensagem = VarMensagem & Chr(13) & Chr(13) & Chr(13) & "Artigo: " & Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & " - " & Me.DocumentoVenda.Linhas.GetEdita(j).Descricao & Chr(13) & "" _
                                        & "Mescla - PercentagemMescla - Componente" & Chr(13) & "" _
                                        & "     " & OpBalanca.Valor("Mescla") & " -     " & OpBalanca.Valor("PercentagemMescla") & "          - " & OpBalanca.Valor("Descricao") & Chr(13) & ""

                        OpBalanca.Seguinte()

                        VarMensagem = VarMensagem & "" _
                        & "     " & OpBalanca.Valor("Mescla") & " -     " & OpBalanca.Valor("PercentagemMescla") & "          - " & OpBalanca.Valor("Descricao") & Chr(13) & ""

                        OpBalanca.Seguinte()

                        VarMensagem = VarMensagem & "" _
                        & "     " & OpBalanca.Valor("Mescla") & " -     " & OpBalanca.Valor("PercentagemMescla") & "          - " & OpBalanca.Valor("Descricao") & Chr(13) & ""

                        OpBalanca.Seguinte()

                        VarMensagem = VarMensagem & "" _
                        & "     " & OpBalanca.Valor("Mescla") & " -     " & OpBalanca.Valor("PercentagemMescla") & "          - " & OpBalanca.Valor("Descricao") & Chr(13) & ""

                        OpBalanca.Seguinte()

                        VarMensagem = VarMensagem & "" _
                        & "     " & OpBalanca.Valor("Mescla") & " -     " & OpBalanca.Valor("PercentagemMescla") & "          - " & OpBalanca.Valor("Descricao") & Chr(13) & ""

                        OpBalanca.Seguinte()

                        VarMensagem = VarMensagem & "" _
                        & "     " & OpBalanca.Valor("Mescla") & " -     " & OpBalanca.Valor("PercentagemMescla") & "          - " & OpBalanca.Valor("Descricao") & Chr(13) & ""

                        OpBalanca.Seguinte()

                        VarMensagem = VarMensagem & "" _
                       & "     " & OpBalanca.Valor("Mescla") & " -     " & OpBalanca.Valor("PercentagemMescla") & "          - " & OpBalanca.Valor("Descricao") & Chr(13) & ""

                        OpBalanca.Seguinte()

                        VarMensagem = VarMensagem & "" _
                        & "     " & OpBalanca.Valor("Mescla") & " -     " & OpBalanca.Valor("PercentagemMescla") & "          - " & OpBalanca.Valor("Descricao") & Chr(13) & ""

                        OpBalanca.Seguinte()

                        VarMensagem = VarMensagem & "" _
                        & "     " & OpBalanca.Valor("Mescla") & " -     " & OpBalanca.Valor("PercentagemMescla") & "          - " & OpBalanca.Valor("Descricao") & Chr(13) & ""

                    End If



                End If


            Next j





            If EnviaMail = True Then


                VarFrom = ""
                'VarTo = "mgoretti@mundifios.pt"
                VarTo = "informatica@mundifios.pt; edite.ferreira@inovafil.pt; paulo.araujo@inovafil.pt"
                'VarTo = "jafernandes@mundifios.pt; mgoretti@mundifios.pt;"

                If Format(Now, "HH:mm") >= "07:00" And Format(Now, "HH:mm") <= "12:59" Then
                    VarTextoInicialMsg = "Bom dia,"
                ElseIf Format(Now, "HH:mm") >= "13:00" And Format(Now, "HH:mm") <= "19:59" Then
                    VarTextoInicialMsg = "Boa tarde,"
                Else
                    VarTextoInicialMsg = "Boa noite,"
                End If

                VarAssunto = "ECL " & Me.DocumentoVenda.NumDoc & "/" & Me.DocumentoVenda.Serie & " Validar Mescla Percentagem"

                VarUtilizador = Aplicacao.Utilizador.Utilizador


                BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL]  ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "', '" & VarFrom & "', '" & VarTo & "','','','" & VarAssunto & "','" & VarMensagem & "','',0,'" & VarUtilizador & "' )")


            End If

        End Function



    End Class
End Namespace
