Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports Primavera.Extensibility.Purchases.Editors
Imports Primavera.Extensibility.Attributes
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports StdBE100
Namespace IntegracaoFilopaDestino
    Public Class CmpIsEditorCompras
        Inherits EditorCompras

        Public Overrides Sub AntesDeAnular(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeAnular(Cancel, e)

            If Module1.VerificaToken("IntegracaoFilopaDestino") = 1 Then

                '--------------------------------------------------------------
                '--- VIMAPONTO - Gualter Costa - 2019-06-13 - RedMine #1558 ---
                '--------------------------------------------------------------
                '
                'Se o documento de compra do tipo "CNT" ou "ECF" foi gerado automaticamente a partir de um documento de venda da Filopa
                'Informa o utilizador que não é possível anular o documento de compra. A anulação deverá ser efectuada a partir do documento de venda Filopa que lhe deu origem
                '


                If (Trim(Me.DocumentoCompra.Tipodoc) = "CNT" Or Trim(Me.DocumentoCompra.Tipodoc) = "ECF") And Len(Me.DocumentoCompra.CamposUtil("CDU_DocumentoOrigem").Valor) > 1 And Len(Me.DocumentoCompra.CamposUtil("CDU_BaseDadosOrigem").Valor) > 1 Then

                    MsgBox("NÃO É POSSÍVEL ANULAR O DOCUMENTO ATUAL!" & Chr(13) & Chr(13) & "O Primavera detectou que o documento de compra atual (" & Trim(Me.DocumentoCompra.Tipodoc) & " " & Trim(Me.DocumentoCompra.Serie) & "/" & Trim(Me.DocumentoCompra.NumDoc) & ")" & Chr(13) & "foi gerado automáticamente a partir do documento de venda (" & Trim(Me.DocumentoCompra.CamposUtil("CDU_DocumentoOrigem").Valor) & ") de origem FILOPA" & Chr(13) & Chr(13) & "Assim, não é possível anular este documento de compra nesta base de dados." & Chr(13) & Chr(13) & "A anulação deverá ser feita a partir do documento de venda que lhe deu origem (" & Trim(Me.DocumentoCompra.CamposUtil("CDU_DocumentoOrigem").Valor) & ") na base de dados da FILOPA.", vbCritical + vbOKOnly)

                    Cancel = True 'Cancela a anulação do documento

                End If

            End If

        End Sub

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)


            If Module1.VerificaToken("IntegracaoFilopaDestino") = 1 Then

                'VIMAPONTO - GMC 2019.05.31 (ver especificação no redmine #1558)
                'Verifica se o documento de compra foi gerado a partir da integração automática de um documento de venda da Filopa.
                '(Se tiver preenchido o campo CDU_DocumentoOrigem não deixa gravar!)


                Dim Aux_DocumentoOrigem As String
                Dim Aux_BaseDadosOrigem As String

                'Verifica se o documento teve origem no mecanismo numa cópia de documentos FILOPA --> Outras Empresas do Grupo Mundifios
                Aux_DocumentoOrigem = BSO.Compras.Documentos.DaValorAtributo(Me.DocumentoCompra.Tipodoc, Me.DocumentoCompra.NumDoc, Me.DocumentoCompra.Serie, Me.DocumentoCompra.Filial, "CDU_DocumentoOrigem")
                Aux_BaseDadosOrigem = BSO.Compras.Documentos.DaValorAtributo(Me.DocumentoCompra.Tipodoc, Me.DocumentoCompra.NumDoc, Me.DocumentoCompra.Serie, Me.DocumentoCompra.Filial, "CDU_BaseDadosOrigem")

                'Se sim (se tiver o campo CDU_DocumentoOrigem preenchido)
                If Len(Trim(Aux_DocumentoOrigem)) > 0 And Len(Trim(Aux_BaseDadosOrigem)) > 0 Then

                    '       Comentado por JFC 30/10/2019
                    '       Cancel = True 'Cancela a gravação
                    '       MsgBox "O documento de compra atual foi gerado a partir da integração automática do documento FILOPA " & Aux_DocumentoOrigem & "." & Chr(13) & Chr(13) & "As alterações deverão ser efectuadas no documento de origem." & Chr(13) & Chr(13) & "As alterações no documento atual não serão gravadas!", vbCritical
                    '       Exit Sub
                    If MsgBox("O documento de compra atual foi gerado a partir da integração automática do documento FILOPA " & Aux_DocumentoOrigem & "." & Chr(13) & Chr(13) & "As alterações deverão ser efectuadas no documento de origem. Caso contráio poderá gerar erros." & Chr(13) & Chr(13) & "Tem a certeza que deseja continuar com a gravação?", vbYesNo) = vbNo Then
                        Cancel = True
                    End If

                End If

            End If

        End Sub


        Public Overrides Sub DepoisDeDuplicar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeDuplicar(Filial, Tipo, Serie, NumDoc, e)


            If Module1.VerificaToken("IntegracaoFilopaDestino") = 1 Then
                '--------------------------------------------------------------
                '--- VIMAPONTO - Gualter Costa - 2019-06-14 - RedMine #1558 ---
                '--------------------------------------------------------------


                'Verifica se no novo documento que acabou ser criado por duplicação, tem preenchido o campo CDU_DocumentoOrigem.
                'Se sim limpa-o no novo documento.

                If Trim(Me.DocumentoCompra.CamposUtil("CDU_DocumentoOrigem").Valor) <> "" Then
                    Me.DocumentoCompra.CamposUtil("CDU_DocumentoOrigem").Valor = ""
                End If

            End If

        End Sub

    End Class
End Namespace
