Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Primavera.Extensibility.Purchases.Editors
Imports Primavera.Extensibility.Attributes
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports StdBE100

Namespace Encargos
    Public Class CmpIsEditorCompras
        Inherits EditorCompras

        '        Dim NaoGravar As Boolean
        '        Public Overrides Sub AntesDeEditar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
        '            MyBase.AntesDeEditar(Filial, Tipo, Serie, NumDoc, Cancel, e)

        '            If Module1.VerificaToken("Encargos") = 1 Then

        '                NaoGravar = False

        '            End If
        '        End Sub

        '        Public Overrides Sub DepoisDeTransformar(e As ExtensibilityEventArgs)
        '            MyBase.DepoisDeTransformar(e)

        '            If Module1.VerificaToken("Encargos") = 1 Then

        '                NaoGravar = False

        '            End If
        '        End Sub

        '        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
        '            MyBase.AntesDeGravar(Cancel, e)

        '            If Module1.VerificaToken("Encargos") = 1 Then
        '                If NaoGravar Then
        '                    Cancel = True
        '                End If

        '                '################################################################################################################################################################
        '                '# Verificar se a Data de Introdução é igual à Data Movimento das Linhas   'JFC - 09/04/2021                                                                    #
        '                '# (verificou-se situações onde os ENCG estavam lançados antes da entrade de stock, motivo foram algumas VF's com datas diferentes na Intodução vs LinhaEntrada #
        '                '################################################################################################################################################################

        '                If Me.DocumentoCompra.Tipodoc = "VFA" Or Me.DocumentoCompra.Tipodoc = "VFO" Or Me.DocumentoCompra.Tipodoc = "VFI" Or Me.DocumentoCompra.Tipodoc = "VIT" Or Me.DocumentoCompra.Tipodoc = "VFE" Or Me.DocumentoCompra.Tipodoc = "WE" Or Me.DocumentoCompra.Tipodoc = "WEI" Or Me.DocumentoCompra.Tipodoc = "WEO" Then

        '                    For i = 1 To Me.DocumentoCompra.Linhas.NumItens
        '                        If Me.DocumentoCompra.Linhas.GetEdita(i).Artigo & "" <> "" And Me.DocumentoCompra.Linhas.GetEdita(i).Lote & "" <> "" Then
        '                            If CDate(Int(Me.DocumentoCompra.Linhas.GetEdita(i).DataStock)) <> Me.DocumentoCompra.DataIntroducao Then
        '                                MsgBox("Atenção:" & Chr(13) & "Não foi possivel gravar o documento.A Linha " & i & " está com Data de Entrada(F10) diferente da Data de Introdução!" & Chr(13) & Chr(13) & "Linha " & i & ": " & CDate(Int(Me.DocumentoCompra.Linhas.GetEdita(i).DataStock)) & Chr(13) & "DataIntrodução: " & Me.DocumentoCompra.DataIntroducao & Chr(13) & Chr(13) & "Por favor corrija a data na Linha (usando a tecla F10) ou a Data de Introdução no cabeçalho do documento.", vbInformation + vbOKOnly)
        '                                Cancel = True
        '                            End If
        '                        End If

        '                    Next i
        '                End If
        '                '################################################################################################################################################################
        '                '# Verificar se a Data de Introdução é igual à Data Movimento das Linhas   'JFC - 09/04/2021                                                                    #
        '                '# (verificou-se situações onde os ENCG estavam lançados antes da entrade de stock, motivo foram algumas VF's com datas diferentes na Intodução vs LinhaEntrada #
        '                '################################################################################################################################################################

        '            End If

        '        End Sub

        '        Public Overrides Sub AntesDeRemoverLinha(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
        '            MyBase.AntesDeRemoverLinha(Cancel, e)
        '            If Module1.VerificaToken("Encargos") = 1 Then
        '                'JFC 13/12/2019 - Encargos Automáticos, devido à complexidade em identificar remoções efectivas, decidiu-se colocar apenas um alerta quando a linha é removida.
        '                If Me.DocumentoCompra.Linhas.GetEdita(Me.LinhaActual).CamposUtil("CDU_DocEncargo").Valor & "" <> "" Then
        '                    NaoGravar = True
        '                    MsgBox("Atenção!" & Chr(13) & "Linha com Encargo associado, não vai ser possivel gravar o documento!" & Chr(13) & "Encargo: " & Me.DocumentoCompra.Linhas.GetEdita(Me.LinhaActual).CamposUtil("CDU_DocEncargo").Valor, vbCritical, "Encargos Automaticos")
        '                End If
        '            End If

        '        End Sub

        '        Public Overrides Sub DepoisDeAnular(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
        '            MyBase.DepoisDeAnular(Filial, Tipo, Serie, NumDoc, e)

        '            If Module1.VerificaToken("Encargos") = 1 Then

        '                'JFC 13/12/2019 - Encargos Automáticos: remove as linhas do Encargo gerado.
        '                Dim j As Long

        '                For j = 1 To Me.DocumentoCompra.Linhas.NumItens
        '                    If Me.DocumentoCompra.Linhas.GetEdita(j).CamposUtil("CDU_DocEncargo").Valor & "" <> "" Then
        '                        'JFC 03/09/2019
        '                        Dim TipoDocFinal As String
        '                        Dim SerieFinal As String
        '                        Dim NumDocFinal As Long

        '                        Dim PosBarra As Long
        '                        Dim PosEsp As Long

        '                        'JFC 04/09/2019 - Identificar a posição da barra e do espaço (TipoDoc NumDoc/Serie)
        '                        PosBarra = InStr(1, Me.DocumentoCompra.Linhas.GetEdita(j).CamposUtil("CDU_DocEncargo").Valor, "/", vbTextCompare)
        '                        PosEsp = InStr(1, Me.DocumentoCompra.Linhas.GetEdita(j).CamposUtil("CDU_DocEncargo").Valor, " ", vbTextCompare)

        '                        TipoDocFinal = Left(Me.DocumentoCompra.Linhas.GetEdita(j).CamposUtil("CDU_DocEncargo").Valor, PosEsp - 1)
        '                        NumDocFinal = Mid(Me.DocumentoCompra.Linhas.GetEdita(j).CamposUtil("CDU_DocEncargo").Valor, PosEsp + 1, PosBarra - PosEsp - 1)
        '                        SerieFinal = Mid(Me.DocumentoCompra.Linhas.GetEdita(j).CamposUtil("CDU_DocEncargo").Valor, PosBarra + 1)

        '                        Dim listDocEnc As StdBELista

        '                        listDocEnc = BSO.Consulta("select lk.NumLinha from LinhasSTK lk where lk.Modulo='S' and lk.TipoDoc='" & TipoDocFinal & "' and lk.NumDoc='" & NumDocFinal & "' and lk.Serie='" & SerieFinal & "' and lk.Artigo='" & Me.DocumentoCompra.Linhas.GetEdita(j).Artigo & "' and lk.Lote='" & Me.DocumentoCompra.Linhas.GetEdita(j).Lote & "'")
        '                        listDocEnc.Inicio()

        '                        If listDocEnc.Vazia = True Then
        '                            MsgBox("Não foi possivel remover a linha do documento: " & Me.DocumentoCompra.Linhas.GetEdita(j).CamposUtil("CDU_DocEncargo").Valor, vbInformation, "Encargos Automaticos")
        '                        Else
        '                            Dim i As Long
        '                            For i = 1 To listDocEnc.NumLinhas
        '                                BSO.Comercial.Stocks.Remove("000", "S", TipoDocFinal, SerieFinal, NumDocFinal, listDocEnc.Valor("NumLinha"))
        '                                listDocEnc.Seguinte()
        '                            Next i
        '                        End If

        '                    End If
        '                Next j

        '            End If

        '        End Sub

        '        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
        '            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

        '            If Module1.VerificaToken("Encargos") = 1 Then

        '                '####################################################################################################################################
        '                '# Encargos Automáticos 'JFC - 13/12/2019                                                                                           #
        '                '####################################################################################################################################

        '                If Me.DocumentoCompra.Tipodoc = "VFA" Or Me.DocumentoCompra.Tipodoc = "VFO" Or Me.DocumentoCompra.Tipodoc = "VFI" Or Me.DocumentoCompra.Tipodoc = "VIT" Or Me.DocumentoCompra.Tipodoc = "VFE" Or Me.DocumentoCompra.Tipodoc = "WE" Or Me.DocumentoCompra.Tipodoc = "WEI" Or Me.DocumentoCompra.Tipodoc = "WEO" Then

        '                    If Year(Of Date)() >= 2020 Then

        '                        Dim listEnc As StdBELista
        '                        'Todo o desenvolvimento dos Encargos Automáticos depende da query abaixo. Qualquer alteração na mesma obrigará a validar todo o desenvolvimento.
        '                        'Armazem AEP não considera Direitos. Utilizado VFE para o efeito. Pedido de Mafalda. - JFC 23/04/2020
        '                        If Me.DocumentoCompra.Tipodoc = "VFE" Then
        '                            listEnc = BSO.Consulta("select lc.Id, lc.NumLinha, lc.Artigo,  lc.Lote, lc.Armazem, lce.CDU_Descricao, (-1*lc.Quantidade* lce.CDU_CustoValor) as 'Custo' from " _
        '                        & "CabecCompras cc " _
        '                    & "inner join LinhasCompras lc on lc.IdCabecCompras=cc.Id " _
        '                    & "inner join LinhasComprasTrans lt on lt.IdLinhasCompras=lc.Id " _
        '                    & "inner join linhascompras lc2 on lc2.Id=lt.IdLinhasComprasOrigem " _
        '                    & "inner join CabecCompras cc2 on cc2.Id=lc2.IdCabecCompras " _
        '                    & "inner join TDU_CabecCustosEncomendas cce on cce.CDU_TipoDoc=cc2.TipoDoc and cce.CDU_NumDoc=cc2.NumDoc and cce.CDU_Serie=cc2.Serie and cce.CDU_NumLinha=lc2.NumLinha " _
        '                    & "inner join TDU_LinhasCustosEncomenda lce on lce.CDU_NumDoc=cc2.NumDoc and lce.CDU_Serie=cc2.Serie and lce.CDU_NumLinha=lc2.NumLinha " _
        '                    & "where (lc.CDU_DocEncargo is null or lc.CDU_DocEncargo='') and cc.Id='" & Me.DocumentoCompra.ID & "' " _
        '                    & "and lce.CDU_Descricao in ('Custos Companhia Maritima') " _
        '                    & "order by lc.NumLinha")
        '                        Else

        '                            listEnc = BSO.Consulta("select lc.Id, lc.NumLinha, lc.Artigo,  lc.Lote, lc.Armazem, lce.CDU_Descricao, (-1*lc.Quantidade* lce.CDU_CustoValor) as 'Custo' from " _
        '                        & "CabecCompras cc " _
        '                    & "inner join LinhasCompras lc on lc.IdCabecCompras=cc.Id " _
        '                    & "inner join LinhasComprasTrans lt on lt.IdLinhasCompras=lc.Id " _
        '                    & "inner join linhascompras lc2 on lc2.Id=lt.IdLinhasComprasOrigem " _
        '                    & "inner join CabecCompras cc2 on cc2.Id=lc2.IdCabecCompras " _
        '                    & "inner join TDU_CabecCustosEncomendas cce on cce.CDU_TipoDoc=cc2.TipoDoc and cce.CDU_NumDoc=cc2.NumDoc and cce.CDU_Serie=cc2.Serie and cce.CDU_NumLinha=lc2.NumLinha " _
        '                    & "inner join TDU_LinhasCustosEncomenda lce on lce.CDU_NumDoc=cc2.NumDoc and lce.CDU_Serie=cc2.Serie and lce.CDU_NumLinha=lc2.NumLinha " _
        '                    & "where (lc.CDU_DocEncargo is null or lc.CDU_DocEncargo='') and cc.Id='" & Me.DocumentoCompra.ID & "' " _
        '                    & "and lce.CDU_Descricao in ('Custos Companhia Maritima','Custos Alfandegarios') " _
        '                    & "order by lc.NumLinha")
        '                        End If
        '                        'Se a query devolver resultados, então cria o Encargo
        '                        If listEnc.Vazia = False Then
        '                            CriarDocEncargo(Me.DocumentoCompra.Serie, listEnc, DateAdd("n", 1, (DateAdd("n", Minute(Now), DateAdd("h", Hour(Now), Me.DocumentoCompra.DataIntroducao)))))
        '                        End If

        '                    End If
        '                End If

        '                '####################################################################################################################################
        '                '# Encargos Automáticos 'JFC - 13/12/2019                                                                                           #
        '                '####################################################################################################################################

        '            End If

        '        End Sub


        '        '####################################################################################################################################
        '        '# Encargos Automáticos 'JFC - 13/12/2019                                                                                           #
        '        '####################################################################################################################################
        '        Private Sub CriarDocEncargo(ByVal VFA_Serie As String, VFA_Linhas As StdBELista, VFA_Data As Date)

        '            Dim DocStocks As GcpBEDocumentoStock
        '            Dim strDetalhe As String

        '            On Error GoTo Erro

        '            'Inicia uma transação
        '            BSO.IniciaTransaccao()

        '            DocStocks = New GcpBEDocumentoStock

        '            With DocStocks

        '                'Muito importante que o identificador do documento esteja já preenchido
        '                .Id = Guid.NewGuid()

        '                .Tipodoc = "ENCG"
        '                .Serie = VFA_Serie


        '            End With

        '            'Preenche a restante informação no documento
        '            BSO.Comercial.Stocks.PreencheDadosRelacionados(DocStocks)
        '            'Data de Introdução
        '            DocStocks.DataDoc = VFA_Data


        '            VFA_Linhas.Inicio()
        '            Dim j As Long
        '            For j = 1 To VFA_Linhas.NumLinhas

        '                BSO.Comercial.Stocks.AdicionaLinha(DocStocks, VFA_Linhas.Valor("Artigo"), , 0, VFA_Linhas.Valor("Armazem"), VFA_Linhas.Valor("Custo"), , VFA_Linhas.Valor("Lote"), VFA_Linhas.Valor("Armazem"))
        '                VFA_Linhas.Seguinte()
        '            Next j



        '            '----------------------------------
        '            '   GRAVAÇÃO DO DOCUMENTO
        '            BSO.Comercial.Stocks.actualiza(DocStocks)
        '            '   GRAVAÇÃO DO DOCUMENTO
        '            '----------------------------------

        '            'Termina a transação
        '            BSO.TerminaTransaccao()
        '            '----------------------------------
        '            '   MENSAGEM FINAL

        '            'Preencher as Descrições no Documento de Stock e preencher o CDU_DocEncargo no Documento de Compra
        '            VFA_Linhas.Inicio()

        '            For j = 1 To VFA_Linhas.NumLinhas
        '                BSO.DSO.ExecuteSQL("update LinhasSTK set Descricao='" & VFA_Linhas.Valor("CDU_Descricao") & "' where NumLinha='" & j & "' and Modulo='S' and TipoDoc='" & DocStocks.Tipodoc & "' and NumDoc='" & DocStocks.NumDoc & "' and Serie='" & DocStocks.Serie & "'")
        '                BSO.DSO.ExecuteSQL("update LinhasCompras set CDU_DocEncargo='" & DocStocks.Tipodoc & " " & CStr(DocStocks.NumDoc) & "/" & DocStocks.Serie & "' where Id='" & VFA_Linhas.Valor("Id") & "'")
        '                VFA_Linhas.Seguinte()
        '            Next j

        '            strDetalhe = vbNullString

        '            strDetalhe = strDetalhe & "Documento de Stock: " & DocStocks.Tipodoc & " Nº " & CStr(DocStocks.NumDoc) & "/" & DocStocks.Serie & vbCrLf

        '            MsgBox("Documento gerado com sucesso." & vbCrLf & strDetalhe, vbInformation, "Documento de Stock")

        '            '   MENSAGEM FINAL
        '            '----------------------------------

        '            DocStocks = Nothing


        '            Exit Sub

        'Erro:
        '            'Desfaz a transação
        '            BSO.DesfazTransaccao()

        '            DocStocks = Nothing


        '            MsgBox("Erro ao gerar o documento." & vbCrLf & Err.Description, vbCritical, "Erro")


        '        End Sub
        '        '####################################################################################################################################
        '        '# Encargos Automáticos 'JFC - 13/12/2019                                                                                           #
        '        '####################################################################################################################################


        '        Public Overrides Sub TipoDocumentoIdentificado(Tipo As String, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
        '            MyBase.TipoDocumentoIdentificado(Tipo, Cancel, e)

        '            If Module1.VerificaToken("Encargos") = 1 Then

        '                NaoGravar = False

        '            End If
        '        End Sub

    End Class
End Namespace