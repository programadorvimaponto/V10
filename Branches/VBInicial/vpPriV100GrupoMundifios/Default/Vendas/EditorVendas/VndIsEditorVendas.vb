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

Namespace DefaultToken
    Public Class VndIsEditorVendas
        Inherits EditorVendas
        Dim i As Long
        Dim r As Integer
        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("Default") = 1 Then

                If Me.DocumentoVenda.Entidade = "" Then

                    MsgBox("Atenção:" & Chr(13) & "O Cliente não está preenchido.", vbCritical + vbOKOnly)
                    Cancel = True
                    Exit Sub

                End If

                If (BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).TipoMercado = 1) And (Me.DocumentoVenda.Tipodoc = "FA" Or Me.DocumentoVenda.Tipodoc = "NC") Then

                    MsgBox("Atenção:" & Chr(13) & "O Cliente é intracomunitário, não deve ser usado neste documento.", vbCritical + vbOKOnly)
                    Cancel = True
                    Exit Sub

                ElseIf (BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).TipoMercado = 2) And (Me.DocumentoVenda.Tipodoc = "FA" Or Me.DocumentoVenda.Tipodoc = "NC") Then

                    MsgBox("Atenção:" & Chr(13) & "O Cliente é extracomunitário, não deve ser usado neste documento.", vbCritical + vbOKOnly)
                    Cancel = True
                    Exit Sub

                ElseIf (BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).TipoMercado = 0) And (Me.DocumentoVenda.Tipodoc = "FI" Or Me.DocumentoVenda.Tipodoc = "NCI") Then

                    MsgBox("Atenção:" & Chr(13) & "O Cliente é nacional, não deve ser usado neste documento.", vbCritical + vbOKOnly)
                    Cancel = True
                    Exit Sub

                ElseIf (BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).TipoMercado = 2) And (Me.DocumentoVenda.Tipodoc = "FI" Or Me.DocumentoVenda.Tipodoc = "NCI") Then

                    MsgBox("Atenção:" & Chr(13) & "O Cliente é extracomunitário, não deve ser usado neste documento.", vbCritical + vbOKOnly)
                    Cancel = True
                    Exit Sub

                ElseIf (BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).TipoMercado = 0) And (Me.DocumentoVenda.Tipodoc = "FO" Or Me.DocumentoVenda.Tipodoc = "NCO") Then

                    MsgBox("Atenção:" & Chr(13) & "O Cliente é nacional, não deve ser usado neste documento.", vbCritical + vbOKOnly)
                    Cancel = True
                    Exit Sub

                ElseIf (BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).TipoMercado = 1) And (Me.DocumentoVenda.Tipodoc = "FO" Or Me.DocumentoVenda.Tipodoc = "NCO") Then

                    MsgBox("Atenção:" & Chr(13) & "O Cliente é intracomunitário, não deve ser usado neste documento.", vbCritical + vbOKOnly)
                    Cancel = True
                    Exit Sub

                End If


                If (BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).TipoMercado = 0) And (Me.DocumentoVenda.Tipodoc = "GR") Then
                    i = 0

                    For i = 1 To Me.DocumentoVenda.Linhas.NumItens
                        If Me.DocumentoVenda.Linhas.GetEdita(i).Armazem = "AEP" Then
                            MsgBox("Atenção:" & Chr(13) & "O Cliente é Nacional e o armazem é AEP por isso não deve ser usado neste documento.", vbCritical + vbOKOnly)
                            Cancel = True
                        End If

                    Next i
                End If


                '#################################################################
                '####### Verifica se documento está valorizado - JFC 13-06-2017 ##
                '#################################################################
                'Desconsidera Guias de Carga a pedido de Angelo Lemos - JFC 16/05/2019
                If Me.DocumentoVenda.Tipodoc <> "GC" Then
                    For j = 1 To Me.DocumentoVenda.Linhas.NumItens

                        If IsNothing(Me.DocumentoVenda.Linhas.GetEdita(j).Artigo) Or Me.DocumentoVenda.Linhas.GetEdita(j).Artigo = "" Then

                        Else
                            If Me.DocumentoVenda.Linhas.GetEdita(j).PrecUnit <= 0 Then
                                r = MsgBox("Atenção:" & Chr(13) & "Artigo " & Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & "-" & Me.DocumentoVenda.Linhas.GetEdita(j).Lote & " está sem preço, tem a certeza que deseja gravar?", vbCritical + vbOKCancel)
                                If r = vbCancel Then
                                    Cancel = True
                                End If
                            End If
                        End If

                    Next j

                End If

                '#################################################################
                '####### Verifica se documento está valorizado - JFC 13-06-2017 ##
                '#################################################################


                If Me.DocumentoVenda.UtilizaMoradaAlternativaEntreg = False And Me.DocumentoVenda.LocalDescarga & "" = "" Then

                    Me.DocumentoVenda.LocalDescarga = "V/ Morada"

                End If



                '*******************************************************************************************************************************************
                '#### ARMAZEM ENTREPOSTO ####
                '*******************************************************************************************************************************************

                'Retirado para ECL 23/7/2015 Alexandre: Sr. Angelo quer fazer ECL sem pôr os dados'

                If Me.DocumentoVenda.Tipodoc <> "ECL" And Me.DocumentoVenda.Tipodoc <> "GC" Then

                    For i = 1 To Me.DocumentoVenda.Linhas.NumItens

                        If Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(i).Armazem = Module1.ArmEntreposto And Me.DocumentoVenda.Linhas.GetEdita(i).Armazem & "" <> "" Then
                            'Retirado para GR 16/10/2019 Bruno: A pedido de José Luis para fazer GR sem pôr os dados'
                            If Me.DocumentoVenda.Tipodoc <> "GR" Then

                                If Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_DespDAU").Valor & "" = "" Then
                                    MsgBox("Atenção:" & Chr(13) & "O Código DAU na linha " & i & " para o artigo '" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "' não está preenchido.", vbCritical + vbOKOnly)
                                    Cancel = True
                                    Exit Sub
                                End If
                                If Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_CODMERC").Valor & "" = "" Then
                                    MsgBox("Atenção:" & Chr(13) & "O Código da Mercadoria na linha " & i & " para o artigo '" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "' não está preenchido.", vbCritical + vbOKOnly)
                                    Cancel = True
                                    Exit Sub
                                End If
                                If Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_Contramarca").Valor & "" = "" Then
                                    MsgBox("Atenção:" & Chr(13) & "A Contramarca na linha " & i & " para o artigo '" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "' não está preenchida.", vbCritical + vbOKOnly)
                                    Cancel = True
                                    Exit Sub
                                End If
                                If Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_Regime").Valor & "" = "" Then
                                    MsgBox("Atenção:" & Chr(13) & "O Código do Regime na linha " & i & " para o artigo '" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "' não está preenchido.", vbCritical + vbOKOnly)
                                    Cancel = True
                                    Exit Sub
                                End If


                            End If


                            If Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_DespTipoImportacao").Valor & "" = "" Then
                                MsgBox("Atenção:" & Chr(13) & "O Tipo de Importação na linha " & i & " para o artigo '" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "' não está preenchido.", vbCritical + vbOKOnly)
                                Cancel = True
                                Exit Sub
                            End If
                            If Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_Volumes").Valor & "" = "" Then
                                MsgBox("Atenção:" & Chr(13) & "Os Volumes na linha " & i & " para o artigo '" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "' não estão preenchidos.", vbCritical + vbOKOnly)
                                Cancel = True
                                Exit Sub
                            End If
                            If Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_MeioTransporte").Valor & "" = "" Then
                                MsgBox("Atenção:" & Chr(13) & "O Meio de Transporte na linha " & i & " para o artigo '" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "' não estão preenchidos.", vbCritical + vbOKOnly)
                                Cancel = True
                                Exit Sub
                            End If
                            If Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_MassaBruta").Valor = 0 Then
                                MsgBox("Atenção:" & Chr(13) & "A Massa Bruta na linha " & i & " para o artigo '" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "' não está preenchida.", vbCritical + vbOKOnly)
                                Cancel = True
                                Exit Sub
                            End If
                            If Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_MassaLiq").Valor = 0 Then
                                MsgBox("Atenção:" & Chr(13) & "A Massa Líquida na linha " & i & " para o artigo '" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "' não está preenchida.", vbCritical + vbOKOnly)
                                Cancel = True
                                Exit Sub
                            End If
                            If Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_ContramarcaData").Valor & "" = "" Then
                                MsgBox("Atenção:" & Chr(13) & "A Data da Contramarca na linha " & i & " para o artigo '" & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "' e lote '" & Me.DocumentoVenda.Linhas.GetEdita(i).Lote & "' não está preenchida.", vbCritical + vbOKOnly)
                                Cancel = True
                                Exit Sub
                            End If

                        End If

                    Next i

                End If
                '*******************************************************************************************************************************************
                '#### ARMAZEM ENTREPOSTO ####
                '*******************************************************************************************************************************************


                '###################################################################################
                '####### Verifica a data de expedição é igual à data de entregao - JFC 11/02/2019 ##
                '###################################################################################
                If Me.DocumentoVenda.Tipodoc = "GC" And Right(Me.DocumentoVenda.Serie, 1) = "E" Then
                    Dim Data As StdBELista

                    For j = 1 To Me.DocumentoVenda.Linhas.NumItens

                        If IsNothing(Me.DocumentoVenda.Linhas.GetEdita(j).Artigo) Or Me.DocumentoVenda.Linhas.GetEdita(j).Artigo = "" Then

                        Else

                            Data = BSO.Consulta("select DataEntrega from LinhasDoc where Id='" & Me.DocumentoVenda.Linhas.GetEdita(j).IdLinhaOrigemCopia & "'")
                            Data.Inicio()
                            Dim dt1 As Date
                            Dim dt As Date
                            dt1 = Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_DataExp").Valor
                            dt = Data.Valor("DataEntrega")
                            If dt = dt1 Then
                                MsgBox("Atenção:" & Chr(13) & "Artigo " & Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & "-" & Me.DocumentoVenda.Linhas.GetEdita(j).Lote & " está com a DATA de EXPEDIÇÃO igual à DATA de ENTREGA na Encomenda, tem a certeza que deseja gravar?", vbInformation)
                                Cancel = True

                                '               Codigo comentado a pedido de José Luis, pretende que seja sempre obrigatório alterar a data de expedição. Garantiu que a mesma nunca será igual à data de entrega na encomenda.
                                '               r = MsgBox("Atenção:" & Chr(13) & "Artigo " & Me.DocumentoVenda.Linhas(j).Artigo & "-" & Me.DocumentoVenda.Linhas(j).Lote & " está com a DATA de EXPEDIÇÃO igual à DATA de ENTREGA na Encomenda, tem a certeza que deseja gravar?", vbInformation + vbOKCancel)
                                '                   If r = vbCancel Then
                                '                    Cancel = True
                                '                    End If

                            End If
                        End If

                    Next j
                End If
                '###################################################################################
                '####### Verifica a data de expedição é igual à data de entregao - JFC 11/02/2019 ##
                '###################################################################################


                '################################################################################################################################################################
                '# Verificar se o documento a gravar tem moeda diferente de EURO com cambio a 1                          BMP - 06/05/2021                                       #
                '################################################################################################################################################################

                If Me.DocumentoVenda.Moeda <> "EUR" And Me.DocumentoVenda.Cambio = 1 Then
                    MsgBox("Atenção, não foi possivel gravar o documento, corrigir a moeda ou cambio antes de gravar! A moeda " & Me.DocumentoVenda.Moeda & " tem o cambio " & Me.DocumentoVenda.Cambio & "", vbCritical + vbOKOnly)
                    Cancel = True
                End If
                '################################################################################################################################################################
                '# Verificar se o documento a gravar tem moeda diferente de EURO com cambio a 1                          BMP - 06/05/2021                                       #
                '################################################################################################################################################################



            End If
        End Sub


        Public Overrides Sub ClienteIdentificado(Cliente As String, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.ClienteIdentificado(Cliente, Cancel, e)

            If Module1.VerificaToken("Default") = 1 Then

                If (BSO.Base.Clientes.Edita(Cliente).TipoMercado = 1) And (Me.DocumentoVenda.Tipodoc = "FA" Or Me.DocumentoVenda.Tipodoc = "NC") Then

                    MsgBox("Atenção:" & Chr(13) & "O Cliente é intracomunitário, não deve ser usado neste documento.", vbCritical + vbOKOnly)

                ElseIf (BSO.Base.Clientes.Edita(Cliente).TipoMercado = 2) And (Me.DocumentoVenda.Tipodoc = "FA" Or Me.DocumentoVenda.Tipodoc = "NC") Then

                    MsgBox("Atenção:" & Chr(13) & "O Cliente é extracomunitário, não deve ser usado neste documento.", vbCritical + vbOKOnly)

                ElseIf (BSO.Base.Clientes.Edita(Cliente).TipoMercado = 0) And (Me.DocumentoVenda.Tipodoc = "FI" Or Me.DocumentoVenda.Tipodoc = "NCI") Then

                    MsgBox("Atenção:" & Chr(13) & "O Cliente é nacional, não deve ser usado neste documento.", vbCritical + vbOKOnly)

                ElseIf (BSO.Base.Clientes.Edita(Cliente).TipoMercado = 2) And (Me.DocumentoVenda.Tipodoc = "FI" Or Me.DocumentoVenda.Tipodoc = "NCI") Then

                    MsgBox("Atenção:" & Chr(13) & "O Cliente é extracomunitário, não deve ser usado neste documento.", vbCritical + vbOKOnly)

                ElseIf (BSO.Base.Clientes.Edita(Cliente).TipoMercado = 0) And (Me.DocumentoVenda.Tipodoc = "FO" Or Me.DocumentoVenda.Tipodoc = "NCO") Then

                    MsgBox("Atenção:" & Chr(13) & "O Cliente é nacional, não deve ser usado neste documento.", vbCritical + vbOKOnly)

                ElseIf (BSO.Base.Clientes.Edita(Cliente).TipoMercado = 1) And (Me.DocumentoVenda.Tipodoc = "FO" Or Me.DocumentoVenda.Tipodoc = "NCO") Then

                    MsgBox("Atenção:" & Chr(13) & "O Cliente é intracomunitário, não deve ser usado neste documento.", vbCritical + vbOKOnly)

                End If

            End If
        End Sub

        Dim VarCliente As String
        Dim VarFrom As String
        Dim VarTo As String
        Dim VarAssunto As String
        Dim VarTextoInicialMsg As String
        Dim VarUtilizador As String
        Dim VarMensagem As String
        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

            If Module1.VerificaToken("Default") = 1 Then

                '################################################################################################################
                '##Envia e-mail no caso de uma GR com vencimento inferior a 8 dias. Pedido de Sofia Mendes. JFC - 24/04/2020   ##
                '################################################################################################################
                If Me.DocumentoVenda.Tipodoc = "GR" And DateDiff("d", Me.DocumentoVenda.DataDoc, Me.DocumentoVenda.DataVenc) < 9 Then


                    VarCliente = Me.DocumentoVenda.Entidade
                    VarFrom = ""

                    VarTo = "informatica@mundifios.pt; tesouraria.clientes@mundifios.pt; faturacao@mundifios.pt"

                    If Format(Now, "HH:mm") >= "07:00" And Format(Now, "HH:mm") <= "12:59" Then
                        VarTextoInicialMsg = "Bom dia,"
                    ElseIf Format(Now, "HH:mm") >= "13:00" And Format(Now, "HH:mm") <= "19:59" Then
                        VarTextoInicialMsg = "Boa tarde,"
                    Else
                        VarTextoInicialMsg = "Boa noite,"
                    End If

                    VarAssunto = "Emitir Fatura: " & Me.DocumentoVenda.Tipodoc & " " & Format(Me.DocumentoVenda.NumDoc, "####") & "/" & Me.DocumentoVenda.Serie & " (" & Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") & ")"

                    VarUtilizador = Aplicacao.Utilizador.Utilizador



                    VarMensagem = VarTextoInicialMsg & Chr(13) & Chr(13) & Chr(13) & "Foi emitido uma Guia com prazo de pagamento inferior ou igual a 8 dias, por favor emita a respetiva fatura:" & Chr(13) & Chr(13) & "" _
                        & "Empresa:                         " & BSO.Contexto.CodEmp & " - " & BSO.Contexto.IDNome & Chr(13) & "" _
                        & "Utilizador:                      " & VarUtilizador & Chr(13) & Chr(13) & "" _
                        & "Cliente:                         " & VarCliente & " - " & Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") & Chr(13) & "" _
                        & "Documento:                       " & Me.DocumentoVenda.Tipodoc & " " & Format(Me.DocumentoVenda.NumDoc, "#,###") & "/" & Me.DocumentoVenda.Serie & Chr(13) & Chr(13) & "" _
                        & "Data Vencimento:                 " & Me.DocumentoVenda.DataVenc & Chr(13) & Chr(13) & "" _
                        & "Local Descarga:                  " & Me.DocumentoVenda.LocalDescarga & Chr(13) & "" _
                        & "Morada Entrega:                  " & Replace(Me.DocumentoVenda.MoradaEntrega, "'", "") & Chr(13) & Chr(13) & "" _
                        & "Cumprimentos"



                    BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL] ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "', '" & VarFrom & "', '" & VarTo & "','','','" & VarAssunto & "','" & VarMensagem & "','',0,'" & VarUtilizador & "' )")



                End If
                '################################################################################################################
                '##Envia e-mail no caso de uma GR com vencimento inferior a 8 dias. Pedido de Sofia Mendes. JFC - 24/04/2020   ##
                '################################################################################################################




                '****************************************************************************************************************************************************
                '#### Enviar e-mail de alerta aquando da emissão de ECL/GR para clientes espanhóis, que não tenham vendas nos ultimos 12 meses -03/11/2020 Bruno ####
                '****************************************************************************************************************************************************

                Dim SQLList As String
                Dim listadocs As StdBELista


                If (Me.DocumentoVenda.Tipodoc = "GR" Or Me.DocumentoVenda.Tipodoc = "ECL") And Me.DocumentoVenda.Pais = "ES" Then
                    SQLList = "select * from Primundifios.dbo.CabecDoc cd inner join DocumentosVenda dv on dv.Documento = cd.TipoDoc where dv.TipoDocumento='4' and cd.data > DATEADD(dd,-365,GETDATE()) and cd.Entidade='" & Me.DocumentoVenda.Entidade & "'"

                    listadocs = BSO.Consulta(SQLList)

                    If listadocs.Vazia Then
                        EnviaEmailVIES
                    End If
                End If


                '****************************************************************************************************************************************************
                '#### Enviar e-mail de alerta aquando da emissão de ECL/GR para clientes espanhóis, que não tenham vendas nos ultimos 12 meses -03/11/2020 Bruno ####
                '****************************************************************************************************************************************************

            End If

        End Sub


        '*********************************************************************************************************************************************
        '#### Enviar Mail para a Sofia para verificar se o cliente está inscrito no VIES se nao tiver vendas nos ultimos 12meses - 03/11/2020 BRUNO###
        '*********************************************************************************************************************************************
        Private Function EnviaEmailVIES()
            VarCliente = Me.DocumentoVenda.Entidade

            VarFrom = ""
            VarTo = "tesouraria.clientes@mundifios.pt;"

            If Format(Now, "HH:mm") >= "07:00" And Format(Now, "HH:mm") <= "12:59" Then
                VarTextoInicialMsg = "Bom dia,"
            ElseIf Format(Now, "HH:mm") >= "13:00" And Format(Now, "HH:mm") <= "19:59" Then
                VarTextoInicialMsg = "Boa tarde,"
            Else
                VarTextoInicialMsg = "Boa noite,"
            End If

            VarAssunto = "Documento: " & Me.DocumentoVenda.Tipodoc & " " & Format(Me.DocumentoVenda.NumDoc, "####") & "/" & Me.DocumentoVenda.Serie & " (" & Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") & ")"

            VarUtilizador = Aplicacao.Utilizador.Utilizador




            VarMensagem = VarTextoInicialMsg & Chr(13) & Chr(13) & Chr(13) & "Foi lançado um Documento no Primavera com cliente espanhol sem documentos emitidos no ultimo ano, verifique se o VAT está valido no VIES" & Chr(13) & Chr(13) & "" _
                    & "Empresa:                         " & BSO.Contexto.CodEmp & " - " & BSO.Contexto.IDNome & Chr(13) & "" _
                    & "Utilizador:                      " & VarUtilizador & Chr(13) & Chr(13) & "" _
                    & "Cliente:                         " & VarCliente & " - " & Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") & Chr(13) & "" _
                    & "Documento:                       " & Me.DocumentoVenda.Tipodoc & " " & Format(Me.DocumentoVenda.NumDoc, "#,###") & "/" & Me.DocumentoVenda.Serie & Chr(13) & Chr(13) & "" _
                    & "Cumprimentos"

            BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL]  ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "', '" & VarFrom & "', '" & VarTo & "','','','" & VarAssunto & "','" & VarMensagem & "','',0,'" & VarUtilizador & "' )")

        End Function
        '*********************************************************************************************************************************************
        '#### Enviar Mail para a Sofia para verificar se o cliente está inscrito no VIES se nao tiver vendas nos ultimos 12meses - 03/11/2020 BRUNO###
        '*********************************************************************************************************************************************

    End Class
End Namespace
