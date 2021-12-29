Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Primavera.Extensibility.Purchases.Editors
Imports Primavera.Extensibility.Attributes
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports StdBE100
Imports BasBE100.BasBETiposGcp

Namespace Inovafil
    Public Class CmpIsEditorCompras
        Inherits EditorCompras

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("Inovafil") = 1 Then



                '##############################################################################################################
                '#Envia e-mail para Joaquim António com Encomendas a Fornecedor que contenham o artigo Rama. JFC - 31/01/2019 #
                '##############################################################################################################
                If Me.DocumentoCompra.Tipodoc = "ECF" Then
                    Dim EnviaEmailRamaBolean As Boolean
                    Dim EnviaEmailCertificadoBolean As Boolean
                    EnviaEmailRamaBolean = False
                    EnviaEmailCertificadoBolean = False
                    Dim listValidaCabec As StdBELista

                    listValidaCabec = BSO.Consulta("select * from CabecCompras cc where cc.Id='" & Me.DocumentoCompra.ID & "'")

                    If listValidaCabec.Vazia = True Then
                        For i = 1 To Me.DocumentoCompra.Linhas.NumItens

                            If BSO.Base.Artigos.DaValorAtributo(Me.DocumentoCompra.Linhas.GetEdita(i).Artigo, "Descricao") Like "Rama*" Then

                                EnviaEmailRamaBolean = True

                            End If

                            If Me.DocumentoCompra.Linhas.GetEdita(i).CamposUtil("CDU_Artigo").Valor & "" = "" And Me.DocumentoCompra.Linhas.GetEdita(i).Artigo & "" <> "" Then
                                If UCase(Me.DocumentoCompra.Linhas.GetEdita(i).Descricao) Like "*RECYCLED*" Or UCase(Me.DocumentoCompra.Linhas.GetEdita(i).Descricao) Like "*RECICLADO*" Or UCase(Me.DocumentoCompra.Linhas.GetEdita(i).Descricao) Like "*REPREVE*" Or UCase(Me.DocumentoCompra.Linhas.GetEdita(i).Descricao) Like "*GRS*" Or UCase(Me.DocumentoCompra.Linhas.GetEdita(i).Descricao) Like "*GOTS*" Or UCase(Me.DocumentoCompra.Linhas.GetEdita(i).Descricao) Like "*OCS*" Or UCase(Me.DocumentoCompra.Linhas.GetEdita(i).Descricao) Like "*BCI*" Or UCase(Me.DocumentoCompra.Linhas.GetEdita(i).Descricao) Like "*SUPIMA*" Then


                                    EnviaEmailCertificadoBolean = True
                                End If
                            End If
                        Next i
                    End If


                    If EnviaEmailRamaBolean = True Then
                        EnvioEmailRama
                    End If
                    ''---Após alteração de funçoes da Filipa----Codigo comentado porque quem trata dos Certificados é a Ana e o Ricardo e são os mesmo que criam as ECF
                    '     If EnviaEmailCertificadoBolean = True Then
                    '    EnvioEmailCertificado
                    '    End If



                End If
                '##############################################################################################################
                '#Envia e-mail para Joaquim António com Encomendas a Fornecedor que contenham o artigo Rama. JFC - 31/01/2019 #
                '##############################################################################################################

            End If

        End Sub


        Public Overrides Sub ArtigoIdentificado(Artigo As String, NumLinha As Integer, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.ArtigoIdentificado(Artigo, NumLinha, Cancel, e)

            If Module1.VerificaToken("Inovafil") = 1 Then

                '#####################################################################################################################
                '####    Bruno 30/01/2020 Adiciona linha especial a pedido pela Andreia em casos de artigos Lã ou Cashemira  #########
                '#####################################################################################################################
                If Me.DocumentoCompra.Tipodoc = "ECF" Then


                    If Me.DocumentoCompra.Linhas.GetEdita(NumLinha).Descricao Like "*Lã*" Then
                        If Me.DocumentoCompra.Pais = "PT" Then
                            BSO.Compras.Documentos.AdicionaLinhaEspecial(Me.DocumentoCompra, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "Artigo composto por Lã. Por favor envie o Relatório de Qualidade e a Declaração origem e de Non Mulesing.")
                        Else
                            BSO.Compras.Documentos.AdicionaLinhaEspecial(Me.DocumentoCompra, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "This Product is composed by WO. Please send Quality Report and Declaration of Origin and Not Mulesing.")
                        End If
                    End If
                    If Me.DocumentoCompra.Linhas.GetEdita(NumLinha).Descricao Like "*Cashemira*" Then
                        If Me.DocumentoCompra.Pais = "PT" Then
                            BSO.Compras.Documentos.AdicionaLinhaEspecial(Me.DocumentoCompra, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "Artigo composto por Cashemira. Por favor envie o Relatório de Qualidade e Certificado Laboratorial de composição.")
                        Else
                            BSO.Compras.Documentos.AdicionaLinhaEspecial(Me.DocumentoCompra, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "This Product is composed by Cashemira. Please send Quality Report and Laboratory Certificate of composition")
                        End If
                    End If


                End If

                '#####################################################################################################################
                '####    Bruno 30/01/2020 Adiciona linha especial a pedido pela Andreia em casos de artigos Lã ou Cashemira  #########
                '#####################################################################################################################
            End If

        End Sub

        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

            If Module1.VerificaToken("Inovafil") Then

                '*******************************************************************************************************************************************
                '#### Atualizar o Armazem Stock Service na Mundifios 14/12/2018 - JFC
                '*******************************************************************************************************************************************
                Dim j As Long
                Dim AtualizaSS As Boolean
                AtualizaSS = False
                For j = 1 To Me.DocumentoCompra.Linhas.NumItens

                    If Me.DocumentoCompra.Linhas(j).Armazem = "INO" Then
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


        Dim VarFrom As String
        Dim VarTo As String
        Dim VarAssunto As String
        Dim VarTextoInicialMsg As String
        Dim VarMensagem As String
        Dim VarUtilizador As String
        Dim VarLinhas As String

        Private Function EnvioEmailRama()



            Dim ln As Long
            VarFrom = ""

            VarTo = "informatica@mundifios.pt;jafernandes@mundifios.pt"

            If Format(Now, "HH:mm") >= "07:00" And Format(Now, "HH:mm") <= "12:59" Then
                VarTextoInicialMsg = "Bom dia,"
            ElseIf Format(Now, "HH:mm") >= "13:00" And Format(Now, "HH:mm") <= "19:59" Then
                VarTextoInicialMsg = "Boa tarde,"
            Else
                VarTextoInicialMsg = "Boa noite,"
            End If

            VarAssunto = "(Rama Inovafil) Encomenda a Fornecedor: " & Format(Me.DocumentoCompra.NumDoc, "####") & "/" & Me.DocumentoCompra.Serie

            VarUtilizador = Aplicacao.Utilizador.Utilizador

            VarLinhas = ""

            For ln = 1 To Me.DocumentoCompra.Linhas.NumItens

                VarLinhas = VarLinhas & "Linha " & ln & ":                         " & Me.DocumentoCompra.Linhas.GetEdita(ln).Artigo & " - Armazem:" & Me.DocumentoCompra.Linhas.GetEdita(ln).Armazem & " - Lote:" & Me.DocumentoCompra.Linhas.GetEdita(ln).Lote & " - Desc:" & Me.DocumentoCompra.Linhas.GetEdita(ln).Descricao & " - Quantidade:" & Me.DocumentoCompra.Linhas.GetEdita(ln).Quantidade & Me.DocumentoCompra.Linhas.GetEdita(ln).Unidade & " - Prec.Unit:" & Me.DocumentoCompra.Linhas.GetEdita(ln).PrecUnit & Me.DocumentoCompra.Moeda & " - Data Entrega:" & Me.DocumentoCompra.Linhas.GetEdita(ln).DataEntrega & Chr(13) & ""


            Next ln


            VarMensagem = VarTextoInicialMsg & Chr(13) & Chr(13) & Chr(13) & "Foi emitido uma Encomenda a Fornecedor no Primavera:" & Chr(13) & Chr(13) & "" _
                        & "Empresa:                         " & BSO.Contexto.CodEmp & " - " & BSO.Contexto.IDNome & Chr(13) & "" _
                        & "Utilizador:                      " & VarUtilizador & Chr(13) & Chr(13) & "" _
                        & "Fornecedor:                      " & Me.DocumentoCompra.Entidade & " - " & Replace(BSO.Base.Fornecedores.Edita(Me.DocumentoCompra.Entidade).Nome, "'", "") & Chr(13) & "" _
                        & "Documento:                       " & Me.DocumentoCompra.Tipodoc & " " & Format(Me.DocumentoCompra.NumDoc, "#,###") & "/" & Me.DocumentoCompra.Serie & Chr(13) & Chr(13) & Chr(13) & "" _
                        & VarLinhas & Chr(13) & "" _
                        & "Cumprimentos"




            BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL]  ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "', '" & VarFrom & "', '" & VarTo & "','','','" & VarAssunto & "','" & VarMensagem & "','',0,'" & VarUtilizador & "' )")




        End Function

    End Class
End Namespace
