Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Primavera.Extensibility.Purchases.Editors
Imports Primavera.Extensibility.Attributes
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports StdBE100

Namespace DefaultToken
    Public Class CmpIsEditorCompras
        Inherits EditorCompras

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)

            If Module1.VerificaToken("Default") = 1 Then


                If Me.DocumentoCompra.Entidade = "" Then

                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor não está preenchido.", vbCritical + vbOKOnly)
                    Cancel = True
                    Exit Sub

                End If

                If (BSO.Base.Fornecedores.Edita(Me.DocumentoCompra.Entidade).TipoMercado = 1) And (Me.DocumentoCompra.Tipodoc = "VFA" Or Me.DocumentoCompra.Tipodoc = "VNC") Then

                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor é intracomunitário, não deve ser usado neste documento.", vbCritical + vbOKOnly)
                    Cancel = True
                    Exit Sub

                ElseIf (BSO.Base.Fornecedores.Edita(Me.DocumentoCompra.Entidade).TipoMercado = 2) And (Me.DocumentoCompra.Tipodoc = "VFA" Or Me.DocumentoCompra.Tipodoc = "VNC") Then

                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor é extracomunitário, não deve ser usado neste documento.", vbCritical + vbOKOnly)
                    Cancel = True
                    Exit Sub

                ElseIf (BSO.Base.Fornecedores.Edita(Me.DocumentoCompra.Entidade).TipoMercado = 0) And (Me.DocumentoCompra.Tipodoc = "VFI" Or Me.DocumentoCompra.Tipodoc = "VCI") Then

                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor é nacional, não deve ser usado neste documento.", vbCritical + vbOKOnly)
                    Cancel = True
                    Exit Sub

                ElseIf (BSO.Base.Fornecedores.Edita(Me.DocumentoCompra.Entidade).TipoMercado = 2) And (Me.DocumentoCompra.Tipodoc = "VFI" Or Me.DocumentoCompra.Tipodoc = "VCI") Then

                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor é extracomunitário, não deve ser usado neste documento.", vbCritical + vbOKOnly)
                    Cancel = True
                    Exit Sub

                ElseIf (BSO.Base.Fornecedores.Edita(Me.DocumentoCompra.Entidade).TipoMercado = 0) And (Me.DocumentoCompra.Tipodoc = "VFO" Or Me.DocumentoCompra.Tipodoc = "VCO") Then

                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor é nacional, não deve ser usado neste documento.", vbCritical + vbOKOnly)
                    Cancel = True
                    Exit Sub

                ElseIf (BSO.Base.Fornecedores.Edita(Me.DocumentoCompra.Entidade).TipoMercado = 1) And (Me.DocumentoCompra.Tipodoc = "VFO" Or Me.DocumentoCompra.Tipodoc = "VCO") Then

                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor é intracomunitário, não deve ser usado neste documento.", vbCritical + vbOKOnly)
                    Cancel = True
                    Exit Sub

                End If


                If Me.DocumentoCompra.UtilizaMoradaAlternativaEntrega = False And Me.DocumentoCompra.LocalDescarga & "" = "" Then

                    If Me.DocumentoCompra.Tipodoc = "NGS" Or Me.DocumentoCompra.Tipodoc = "NGT" Then
                        Me.DocumentoCompra.LocalDescarga = "V/ Morada"
                    Else
                        Me.DocumentoCompra.LocalDescarga = "N/ Morada"
                    End If

                End If

                '################################################################################################################################################################
                '# Verificar se o documento a gravar tem moeda diferente de EURO com cambio a 1                          BMP - 06/05/2021                                       #
                '################################################################################################################################################################

                If Me.DocumentoCompra.Moeda <> "EUR" And Me.DocumentoCompra.Cambio = 1 Then
                    MsgBox("Atenção, não foi possivel gravar o documento, corrigir a moeda ou cambio antes de gravar! A moeda " & Me.DocumentoCompra.Moeda & " tem o cambio " & Me.DocumentoCompra.Cambio & "", vbCritical + vbOKOnly)
                    Cancel = True
                End If

                '################################################################################################################################################################
                '# Verificar se o documento a gravar tem moeda diferente de EURO com cambio a 1                          BMP - 06/05/2021                                       #
                '################################################################################################################################################################

            End If



        End Sub


        Public Overrides Sub ArtigoIdentificado(Artigo As String, NumLinha As Integer, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.ArtigoIdentificado(Artigo, NumLinha, Cancel, e)
            If Module1.VerificaToken("Default") = 1 Then

                If BSO.Base.Artigos.DaValorAtributo(Me.DocumentoCompra.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtra") & "" <> "" Then
                    Me.DocumentoCompra.Linhas.GetEdita(NumLinha).Descricao = BSO.Base.Artigos.DaValorAtributo(Artigo, "Descricao") & " " & BSO.Base.Artigos.DaValorAtributo(Me.DocumentoCompra.Linhas.GetEdita(NumLinha).Artigo, "CDU_DescricaoExtra")
                End If
            End If

        End Sub

        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)
            If Module1.VerificaToken("Default") = 1 Then

                ActualizaQtdSatisfeitaContrato()
            End If

        End Sub

        Dim NaoGravar As Boolean
        Public Overrides Sub DepoisDeTransformar(e As ExtensibilityEventArgs)
            MyBase.DepoisDeTransformar(e)

            If Module1.VerificaToken("Default") = 1 Then

                '##############################################################################################
                '###Validar criação de documentos de Compras se foi transformado de ECF --- Bruno 18/12/2019###
                '##############################################################################################
                If Me.DocumentoCompra.Tipodoc = "VFO" Or Me.DocumentoCompra.Tipodoc = "VFI" Or Me.DocumentoCompra.Tipodoc = "WE" Or Me.DocumentoCompra.Tipodoc = "WEI" Or Me.DocumentoCompra.Tipodoc = "WEO" Or Me.DocumentoCompra.Tipodoc = "VIT" Then
                    Dim i As Long
                    Dim list As StdBELista


                    For i = 1 To Me.DocumentoCompra.Linhas.NumItens
                        If Me.DocumentoCompra.Linhas.GetEdita(i).IDLinhaOriginal <> String.Empty Then
                            list = BSO.Consulta("select cc.TipoDoc from LinhasCompras lc inner join CabecCompras cc on cc.Id=lc.IdCabecCompras where lc.Id='" & Me.DocumentoCompra.Linhas.GetEdita(i).IDLinhaOriginal & "'")
                            list.Inicio()

                            If list.Valor("TipoDoc") <> "ECF" Then
                                MsgBox("Não vai ser possivel gravar o documento! Este documento não foi transformado por uma ECF! ", vbCritical + vbOKOnly)
                                NaoGravar = True
                            End If
                        End If
                    Next i

                End If

                '###############################################################################################
                '###Validar criação de documentos de Compras se foi transformado de ECF --- Bruno 18/12/2019###
                '###############################################################################################

            End If

        End Sub


        Public Overrides Sub FornecedorIdentificado(Fornecedor As String, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.FornecedorIdentificado(Fornecedor, Cancel, e)

            If Module1.VerificaToken("Default") = 1 Then

                If Me.DocumentoCompra.Tipodoc = "NGS" Or Me.DocumentoCompra.Tipodoc = "NGT" Then

                    Me.DocumentoCompra.LocalCarga = "N/ Morada"
                    Me.DocumentoCompra.LocalDescarga = "V/ Morada"

                End If

                If (BSO.Base.Fornecedores.Edita(Fornecedor).TipoMercado = 1) And (Me.DocumentoCompra.Tipodoc = "VFA" Or Me.DocumentoCompra.Tipodoc = "VNC") Then

                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor é intracomunitário, não deve ser usado neste documento.", vbCritical + vbOKOnly)

                ElseIf (BSO.Base.Fornecedores.Edita(Fornecedor).TipoMercado = 2) And (Me.DocumentoCompra.Tipodoc = "VFA" Or Me.DocumentoCompra.Tipodoc = "VNC") Then

                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor é extracomunitário, não deve ser usado neste documento.", vbCritical + vbOKOnly)

                ElseIf (BSO.Base.Fornecedores.Edita(Fornecedor).TipoMercado = 0) And (Me.DocumentoCompra.Tipodoc = "VFI" Or Me.DocumentoCompra.Tipodoc = "VCI") Then

                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor é nacional, não deve ser usado neste documento.", vbCritical + vbOKOnly)

                ElseIf (BSO.Base.Fornecedores.Edita(Fornecedor).TipoMercado = 2) And (Me.DocumentoCompra.Tipodoc = "VFI" Or Me.DocumentoCompra.Tipodoc = "VCI") Then

                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor é extracomunitário, não deve ser usado neste documento.", vbCritical + vbOKOnly)

                ElseIf (BSO.Base.Fornecedores.Edita(Fornecedor).TipoMercado = 0) And (Me.DocumentoCompra.Tipodoc = "VFO" Or Me.DocumentoCompra.Tipodoc = "VCO") Then

                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor é nacional, não deve ser usado neste documento.", vbCritical + vbOKOnly)

                ElseIf (BSO.Base.Fornecedores.Edita(Fornecedor).TipoMercado = 1) And (Me.DocumentoCompra.Tipodoc = "VFO" Or Me.DocumentoCompra.Tipodoc = "VCO") Then

                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor é intracomunitário, não deve ser usado neste documento.", vbCritical + vbOKOnly)

                End If

            End If

        End Sub
        Private Function ActualizaQtdSatisfeitaContrato()

            If Me.DocumentoCompra.Tipodoc = "ECF" Then

                Module1.SqlStringIdCopia = "SELECT dbo.LinhasCompras.IdLinhaOrigemCopia " _
                                   & "FROM dbo.CabecCompras INNER JOIN dbo.LinhasCompras ON dbo.CabecCompras.Id = dbo.LinhasCompras.IdCabecCompras " _
                                   & "WHERE (dbo.CabecCompras.TipoDoc = '" & Me.DocumentoCompra.Tipodoc & "') AND (dbo.CabecCompras.Serie = '" & Me.DocumentoCompra.Serie & "') AND (dbo.CabecCompras.NumDoc = " & Me.DocumentoCompra.NumDoc & ") AND (dbo.LinhasCompras.IdLinhaOrigemCopia IS NOT NULL) " _
                                   & "GROUP BY dbo.LinhasCompras.IdLinhaOrigemCopia"

                Module1.ListaIdCopia = BSO.Consulta(Module1.SqlStringIdCopia)

                If Module1.ListaIdCopia.Vazia = False Then

                    Module1.ListaIdCopia.Inicio()

                    For i = 1 To Module1.ListaIdCopia.NumLinhas

                        Module1.SqlStringQtdIdCopia = "SELECT SUM(Quantidade) AS TOTAL " _
                                              & "FROM dbo.LinhasCompras " _
                                              & "WHERE (IdLinhaOrigemCopia = '" & Module1.ListaIdCopia.Valor("IdLinhaOrigemCopia") & "')"

                        Module1.ListaQtdIdCopia = BSO.Consulta(Module1.SqlStringQtdIdCopia)

                        If Module1.ListaQtdIdCopia.Vazia = False Then

                            Module1.ListaQtdIdCopia.Inicio()

                            BSO.DSO.ExecuteSQL("UPDATE LINHASCOMPRASSTATUS SET QUANTTRANS = " & Replace(Module1.ListaQtdIdCopia.Valor("TOTAL"), ",", ".") & " WHERE IDLINHASCOMPRAS = '" & Module1.ListaIdCopia.Valor("IdLinhaOrigemCopia") & "'")

                        End If

                        Module1.ListaIdCopia.Seguinte()

                    Next i

                End If

            End If

        End Function


    End Class
End Namespace