Imports System.Windows
Imports Primavera.Extensibility.CustomForm
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

Public Class Form1
    Inherits CustomForm

    Dim ListaTipoIdentificacao As StdBELista
    Dim SqlTipoIdentificacao As String

    Private Sub barButtonItemGravar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barButtonItemGravar.ItemClick

        If Me.TxtCodigoCliente.Text <> "" Then
            AlteraFiacoesClientes()

        Else

            MsgBox("O cliente não está identificado.", vbInformation + vbOKOnly)
        End If

    End Sub

    Private Sub CarregaDados()





        If Me.textEditCliente.EditValue = String.Empty Then
            Me.TxtCodigoCliente.Text = String.Empty
            Exit Sub
        End If

        Dim listFiacoes As StdBELista
        Dim sql As String
        sql = "SELECT cliente, Nome, CDU_FiacoesObs, CDU_FiacoesNIdentificacao, CDU_FiacoesTIdentificacao, CDU_FiacoesPreAuditado, CDU_FiacoesAuditado, CDU_FiacoesAprovado, CDU_FiacoesClassificacao, CDU_FiacoesData, Fac_Mor, Fac_Local, Paises.Descricao  FROM " & PRIFilopa & ".dbo.Clientes inner join prifilopa.dbo.Paises  on prifilopa.dbo.Paises.Pais=Clientes.Pais WHERE Cliente= '" & Me.TxtCodigoCliente.Text & "'"

        listFiacoes = BSO.Consulta(sql)
        listFiacoes.Inicio()





        'Data Atualizacao
        Me.dateEditFiacoes.EditValue = listFiacoes.Valor("CDU_FiacoesData")
        'Dados
        'Me.txtCliente = listFiacoes("Nome")
        'Me.txtFiacoes = listFiacoes("CDU_Fiacoes")
        'Me.txtFiacoesAprovadas = listFiacoes("CDU_FiacoesAprovadas")
        'Me.txtFiacoesAuditoria = listFiacoes("CDU_FiacoesAuditoria")
        'Me.txtFiacoesNaoAprovadas = listFiacoes("CDU_FiacoesNaoaprovadas")
        'Me.txtFiacoesFonte = listFiacoes("CDU_FiacoesFonte")
        Me.memoEditFiacoesObs.EditValue = listFiacoes.Valor("CDU_FiacoesObs")

        Me.textEditNIdentificacao.EditValue = listFiacoes.Valor("CDU_FiacoesNIdentificacao")
        Me.lookUpEditTipoIdentificacao.EditValue = listFiacoes.Valor("CDU_FiacoesTIdentificacao")
        Me.lookUpEditPreAuditado.EditValue = listFiacoes.Valor("CDU_FiacoesPreAuditado")
        Me.lookUpEditAuditado.EditValue = listFiacoes.Valor("CDU_FiacoesAuditado")
        Me.lookUpEditAprovado.EditValue = listFiacoes.Valor("CDU_FiacoesAprovado")
        Me.lookUpEditClassificacao.EditValue = listFiacoes.Valor("CDU_FiacoesClassificacao")

        Me.TextEditPais.EditValue = listFiacoes.Valor("Descricao")
        Me.TextEditFacMor.EditValue = listFiacoes.Valor("Fac_Mor")
        Me.TextEditFacLocal.EditValue = listFiacoes.Valor("Fac_Local")

    End Sub

    Function AlteraFiacoesClientes()


        On Error GoTo Erro
        Dim CamposFiacoes As New StdBECampos

        CamposFiacoes = BSO.Base.Clientes.DaValorAtributos(Me.TxtCodigoCliente, "CDU_FiacoesObs", "CDU_FiacoesNIdentificacao", "CDU_FiacoesTIdentificacao", "CDU_FiacoesPreAuditado", "CDU_FiacoesAuditado", "CDU_FiacoesAprovado", "CDU_FiacoesClassificacao", "CDU_FiacoesData")


        'Data Atualizacao
        CamposFiacoes("CDU_FiacoesData").Valor = Format(Me.dateEditFiacoes.EditValue, "yyyy-MM-dd")
        'Dados


        'CamposFiacoes("CDU_Fiacoes") = Me.txtFiacoes
        'CamposFiacoes("CDU_FiacoesAprovadas") = Me.txtFiacoesAprovadas
        'CamposFiacoes("CDU_FiacoesAuditoria") = Me.txtFiacoesAuditoria
        'CamposFiacoes("CDU_FiacoesNaoaprovadas") = Me.txtFiacoesNaoAprovadas
        'CamposFiacoes("CDU_FiacoesFonte") = Me.txtFiacoesFonte
        CamposFiacoes("CDU_FiacoesObs").Valor = Me.memoEditFiacoesObs.EditValue

        CamposFiacoes("CDU_FiacoesNIdentificacao").Valor = Me.textEditNIdentificacao.EditValue
        CamposFiacoes("CDU_FiacoesTIdentificacao").Valor = Me.lookUpEditTipoIdentificacao.EditValue
        CamposFiacoes("CDU_FiacoesPreAuditado").Valor = Me.lookUpEditPreAuditado.EditValue
        CamposFiacoes("CDU_FiacoesAuditado").Valor = Me.lookUpEditAuditado.EditValue
        CamposFiacoes("CDU_FiacoesAprovado").Valor = Me.lookUpEditAprovado.EditValue
        CamposFiacoes("CDU_FiacoesClassificacao").Valor = Me.lookUpEditClassificacao.EditValue

        BSO.Base.Clientes.ActualizaValorAtributos(Me.TxtCodigoCliente, CamposFiacoes)

        CopiarMundifios()

        Exit Function

Erro:
        MsgBox("Filopa - Erro ao gravar: " & vbCrLf & Err.Number & " - " & Err.Description, vbExclamation)




    End Function


    Function CopiarMundifios()

        On Error GoTo TrataErro
        Dim ent As String
        ent = BSO.Base.Clientes.DaValorAtributo(Me.TxtCodigoCliente, "CDU_EntidadeInterna")

        If AbreObjEmpresa("MUNDIFIOS") Then
            Dim Forn As New StdBELista
            Forn = BSO.Consulta("select f.Fornecedor from Fornecedores f where f.FornecedorAnulado='0' and f.CDU_EntidadeInterna='" & ent & "'")
            Forn.Inicio()

            If Forn.Vazia = False Then
                Dim Campos As New StdBECampos
                Campos = BSO.Base.Clientes.DaValorAtributos(Me.TxtCodigoCliente, "CDU_FiacoesObs", "CDU_FiacoesNIdentificacao", "CDU_FiacoesTIdentificacao", "CDU_FiacoesPreAuditado", "CDU_FiacoesAuditado", "CDU_FiacoesAprovado", "CDU_FiacoesClassificacao", "CDU_FiacoesData")
                BSO.Base.Fornecedores.ActualizaValorAtributos(Forn.Valor("Fornecedor"), Campos)
                MsgBox("Filopa - Dados gravados com sucesso!" & Chr(13) & Chr(13) & "Mundifios - Dados gravados com sucesso!", vbInformation)
            Else
                MsgBox("Filopa - Dados gravados com sucesso!" & Chr(13) & Chr(13) & "Mundifios - Erro:Fornecedor inexistente(EntidadeInterna " & BSO.Base.Clientes.DaValorAtributo(Me.TxtCodigoCliente, "CDU_EntidadeInterna") & ")", vbInformation)
            End If
            FechaObjEmpresa
        End If
        Exit Function

TrataErro:
        MsgBox("Filopa - Dados gravados com sucesso!" & Chr(13) & Chr(13) & "Mundifios - Erro:" & vbCrLf & Err.Number & " - " & Err.Description, vbExclamation)


    End Function

    Private Sub barButtonItemCopiarInformacao_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barButtonItemCopiarInformacao.ItemClick


        Dim copiar As Forms.DataObject
        Dim resumo As String

        copiar = New Forms.DataObject
        resumo = (Me.textEditCliente.EditValue & vbCrLf & Me.TextEditFacMor.EditValue & vbCrLf & Me.TextEditFacLocal.EditValue & vbCrLf & Me.TextEditPais.EditValue & vbCrLf & vbCrLf & Me.lookUpEditTipoIdentificacao.EditValue & ": " & Me.textEditNIdentificacao.EditValue)



        copiar.SetText(resumo)
        copiar.PutInClipboard


    End Sub

    Private Sub textEditCliente_EditValueChanged(sender As Object, e As EventArgs) Handles textEditCliente.EditValueChanged

        CarregaDados()

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Me.TxtCodigoCliente = Module1.certFiacoes



        SqlTipoIdentificacao = "SELECT * FROM prifilopa.dbo.tdu_tipoidentificacaoinditex"

        ListaTipoIdentificacao = BSO.Consulta(SqlTipoIdentificacao)

        If ListaTipoIdentificacao.Vazia = False Then

            ListaTipoIdentificacao.Inicio()

            For k = 1 To ListaTipoIdentificacao.NumLinhas

                Me.lookUpEditTipoIdentificacao.AddItem(ListaTipoIdentificacao.Valor("CDU_TipoIdentificacao"), 0)
                ListaTipoIdentificacao.Seguinte()

            Next k

        End If
        lookUpEditClassificacao.AddItem("A")
        lookUpEditClassificacao.AddItem("B")
        lookUpEditClassificacao.AddItem("C")
        lookUpEditClassificacao.AddItem("D")

        lookUpEditPreAuditado.AddItem("Sim")
        lookUpEditPreAuditado.AddItem("Não")
        lookUpEditPreAuditado.AddItem("Aguarda")

        lookUpEditAuditado.AddItem("Sim")
        lookUpEditAuditado.AddItem("Não")
        lookUpEditAuditado.AddItem("Aguarda")

        lookUpEditAprovado.AddItem("Sim")
        lookUpEditAprovado.AddItem("Não")
        lookUpEditAprovado.AddItem("Aguarda")

    End Sub


    'Private Sub TxtCodigoCliente_Change()

    '    Me.txtCliente.Text = BSO.Comercial.Clientes.DaValorAtributo(Me.TxtCodigoCliente.Text, "Nome")

    'End Sub

    'Private Sub TxtCodigoCliente_KeyUp(KeyCode As Integer, Shift As Integer)
    '    If KeyCode = vbKeyF4 Then

    '        Aplicacao.PlataformaPRIMAVERA.AbreLista 0, "Clientes", "Cliente", Me, Me.TxtCodigoCliente, "mnuTabCliente", , , , , , True

    '  End If
    'End Sub




End Class