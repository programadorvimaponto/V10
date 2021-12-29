'Imports System
'Imports System.Collections.Generic
'Imports System.Linq
'Imports System.Text
'Imports System.Threading.Tasks
'Imports Primavera.Extensibility.Sales.Editors
'Imports Primavera.Extensibility.BusinessEntities
'Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
'Imports StdBE100
'Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico

'Namespace TestaPlafond
'    Public Class VndIsEditorVendas
'        Inherits EditorVendas

'        Dim VarCancelaDoc As Boolean
'        Dim VarLocalTeste As Integer '0 - ao seleccionar o cliente; 1 - antes de gravar o documento(ECL ou GR)
'        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
'            MyBase.AntesDeGravar(Cancel, e)

'            If Module1.VerificaToken("TestaPlafond") = 1 Then

'                If Me.DocumentoVenda.Tipodoc = "ECL" Or Me.DocumentoVenda.Tipodoc = "GC" Or (Me.DocumentoVenda.Tipodoc = "GR" And Right(Me.DocumentoVenda.Serie, 1) = "E") Then

'                    VarCancelaDoc = False
'                    VarLocalTeste = 1
'                    If Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupo").Valor & "" <> "" Then
'                        TestaPlafondCopiaEntreEmpresas()
'                    Else
'                        TestaPlafond()
'                    End If

'                    If VarCancelaDoc = True Then
'                        Cancel = True
'                        Exit Sub
'                    End If

'                End If

'            End If

'        End Sub


'        Public Overrides Sub ClienteIdentificado(Cliente As String, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
'            MyBase.ClienteIdentificado(Cliente, Cancel, e)

'            If Module1.VerificaToken("TestaPlafond") = 1 Then

'                If Me.DocumentoVenda.Tipodoc = "ECL" Or Me.DocumentoVenda.Tipodoc = "GC" Or (Me.DocumentoVenda.Tipodoc = "GR" And Right(Me.DocumentoVenda.Serie, 1) = "E") Then

'                    VarLocalTeste = 0
'                    TestaPlafond()

'                End If

'            End If

'        End Sub
'        Public Overrides Sub AntesDeEditar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
'            MyBase.AntesDeEditar(Filial, Tipo, Serie, NumDoc, Cancel, e)

'            If Module1.VerificaToken("TestaPlafond") = 1 Then

'                If Me.DocumentoVenda.Tipodoc = "ECL" Or Me.DocumentoVenda.Tipodoc = "GC" Then

'                    VarTotalDocGravado = 0

'                End If

'            End If

'        End Sub

'        Public Overrides Sub DepoisDeEditar(e As ExtensibilityEventArgs)
'            MyBase.DepoisDeEditar(e)

'            If Module1.VerificaToken("TestaPlafond") = 1 Then

'                If Me.DocumentoVenda.Tipodoc = "ECL" Or Me.DocumentoVenda.Tipodoc = "GC" Then
'                    VarTotalDocGravado = Me.DocumentoVenda.TotalDocumento
'                End If

'            End If

'        End Sub
'        'Variáveis para o Plafond
'        Dim ListaTotalDeb As StdBELista
'        Dim SqlStringTotalDeb As String
'        Dim VarCliente As String
'        Dim VarClienteNome As String
'        Dim VarPlafondCliente As Double
'        Dim VarPlafondCred As Double
'        Dim VarPlafondExtra As Double
'        Dim VarTotRespLetras As Double
'        Dim VarTotValorCC As Double
'        Dim VarTotGuias As Double
'        Dim VarTotCheques As Double
'        Dim VarTotEncomendas As Double
'        Dim VarTotParaEncomenda As Double
'        Dim VarTotParaGuia As Double
'        Dim VarTotalDoc As Double
'        Dim VarTotalDocGravado As Double
'        Private Function TestaPlafondCopiaEntreEmpresas()

'            If Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupo").Valor & "" <> "" Then

'                VarCliente = Me.DocumentoVenda.CamposUtil("CDU_EntidadeFinalGrupo").Valor

'                Module1.AbreObjEmpresa(BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).CamposUtil("CDU_NomeEmpresaGrupo").Valor)
'                VarClienteNome = BSO.Base.Clientes.Edita(VarCliente).Nome
'                If BSO.Base.Clientes.Edita(VarCliente).Limitecredito & "" = "" Then
'                    VarPlafondCred = 0
'                Else
'                    VarPlafondCred = BSO.Base.Clientes.Edita(VarCliente).Limitecredito
'                End If


'                '######  Retirada a soma do VarPlafondExtra no dia 04/11/2016 a pedido da Dona Goretti. #######
'                'If BSO.Comercial.Clientes.Edita(VarCliente).CamposUtil("CDU_PlafondExtra").Valor & "" = "" Then
'                'VarPlafondExtra = 0
'                ' Else
'                'VarPlafondExtra = BSO.Comercial.Clientes.Edita(VarCliente).CamposUtil("CDU_PlafondExtra").Valor
'                ' End If

'                'VarPlafondCliente = VarPlafondCred + VarPlafondExtra

'                VarPlafondCliente = VarPlafondCred

'                '1. Responsabilidade em Letras
'                '2. Valor em  Conta Corrente
'                '3. Guias de Remessa Por Facturar
'                '4. Cheques Pré-Datados
'                '5. Encomendas em Carteira

'                SqlStringTotalDeb = "SELECT TOP 1 (SELECT ISNULL(SUM(CASE Moeda WHEN 'EUR' THEN ValorPendente ELSE ValorPendente / Cambio END), 0) AS ValorContaCorrente " _
'                                & "FROM Pendentes WHERE (TipoEntidade = 'C') AND (TipoConta = 'CCC') AND estado = 'PEN' AND Entidade = '" & VarCliente & "' " _
'                                & "AND DataVenc < '2099-12-31') AS ValorContaCorrente " _
'                                & ", (SELECT ISNULL(SUM(CASE Moeda WHEN 'EUR' THEN ValorPendente ELSE ValorPendente / Cambio END), 0) AS ValorContaCorrente " _
'                                & "FROM Pendentes WHERE (TipoEntidade = 'C') AND (TipoConta = 'CCC') AND estado = 'CPD' AND Entidade = '" & VarCliente & "' " _
'                                & "AND DataVenc < '2099-12-31') AS ValorCPD " _
'                                & ", (SELECT ISNULL(SUM(CASE Moeda WHEN 'EUR' THEN ValorPendente ELSE ValorPendente / Cambio END), 0) AS ValorContaCorrente " _
'                                & "FROM Pendentes WHERE (TipoEntidade = 'C') AND (TipoConta = 'CLR') AND Entidade = '" & VarCliente & "' " _
'                                & "AND DataVenc < '2099-12-31') AS ValorLetras " _
'                                & "FROM Pendentes AS Pendentes_1"

'                ListaTotalDeb = BSO.Consulta(SqlStringTotalDeb)

'                If ListaTotalDeb.Vazia = True Then

'                    VarTotValorCC = 0
'                    VarTotCheques = 0
'                    VarTotRespLetras = 0

'                Else

'                    VarTotValorCC = Math.Round(ListaTotalDeb.Valor("ValorContaCorrente"), 2)
'                    VarTotCheques = Math.Round(ListaTotalDeb.Valor("ValorCPD"), 2)
'                    VarTotRespLetras = Math.Round(ListaTotalDeb.Valor("ValorLetras"), 2)

'                End If

'                SqlStringTotalDeb = "SELECT TOP 1 (SELECT ISNULL(SUM(CASE moeda WHEN 'EUR' THEN ((LinhasDoc.TaxaIva / 100) + 1) * (LinhasDoc.PrecoLiquido - (LinhasDocStatus.QuantTrans * LinhasDoc.PrecUnit)) " _
'                                & "ELSE ((LinhasDoc.TaxaIva / 100) + 1) * ((LinhasDoc.PrecoLiquido - (LinhasDocStatus.QuantTrans * LinhasDoc.PrecUnit))) / cambio END), 0) AS ValorGuiasRemessa " _
'                                & "FROM CabecDoc INNER JOIN LinhasDoc ON CabecDoc.Id = LinhasDoc.IdCabecDoc INNER JOIN LinhasDocStatus ON LinhasDoc.Id = LinhasDocStatus.IdLinhasDoc INNER JOIN DocumentosVenda ON CabecDoc.TipoDoc = DocumentosVenda.Documento INNER JOIN CabecDocStatus ON LinhasDoc.IdCabecDoc = CabecDocStatus.IdCabecDoc " _
'                                & "WHERE (DocumentosVenda.TipoDocumento = 3) AND (CabecDocStatus.Estado = 'P') AND (LinhasDocStatus.EstadoTrans = 'P') AND (CabecDoc.Entidade = '" & VarCliente & "') " _
'                                & "AND CabecDoc.DataVencimento < '2099-12-31' AND (CabecDocStatus.Fechado = 0) AND (LinhasDocStatus.Fechado = 0) AND (CabecDocStatus.Anulado = 0) " _
'                                & "AND cabecdoc.tipodoc = 'GR') AS ValorGuiasRemessa " _
'                                & ", (SELECT ISNULL(SUM(CASE moeda WHEN 'EUR' THEN ((LinhasDoc.TaxaIva / 100) + 1) * (LinhasDoc.PrecoLiquido - (LinhasDocStatus.QuantTrans * LinhasDoc.PrecUnit)) " _
'                                & "ELSE ((LinhasDoc.TaxaIva / 100) + 1) * ((LinhasDoc.PrecoLiquido - (LinhasDocStatus.QuantTrans * LinhasDoc.PrecUnit)) / cambio) END), 0) AS ValorEncomedasCarteira " _
'                                & "FROM CabecDoc INNER JOIN LinhasDoc ON CabecDoc.Id = LinhasDoc.IdCabecDoc INNER JOIN LinhasDocStatus ON LinhasDoc.Id = LinhasDocStatus.IdLinhasDoc INNER JOIN DocumentosVenda ON CabecDoc.TipoDoc = DocumentosVenda.Documento INNER JOIN CabecDocStatus ON LinhasDoc.IdCabecDoc = CabecDocStatus.IdCabecDoc " _
'                                & "WHERE (DocumentosVenda.TipoDocumento = 2) AND (CabecDocStatus.Estado = 'P') AND (LinhasDocStatus.EstadoTrans = 'P') AND (CabecDoc.Entidade = '" & VarCliente & "') " _
'                                & "AND (CabecDocStatus.Fechado = 0) AND (LinhasDocStatus.Fechado = 0) AND (CabecDocStatus.Anulado = 0) " _
'                                & "AND CabecDoc.DataVencimento < '2099-12-31') AS ValorEncomendasCarteira " _
'                                & "FROM CabecDoc AS CabecDoc_1"

'                ListaTotalDeb = BSO.Consulta(SqlStringTotalDeb)

'                If ListaTotalDeb.Vazia = True Then

'                    VarTotGuias = 0
'                    VarTotEncomendas = 0

'                Else

'                    VarTotGuias = Math.Round(ListaTotalDeb.Valor("ValorGuiasRemessa"), 2)
'                    VarTotEncomendas = Math.Round(ListaTotalDeb.Valor("ValorEncomendasCarteira"), 2)

'                End If

'                VarTotalDoc = Me.DocumentoVenda.TotalDocumento

'                VarTotParaEncomenda = VarTotRespLetras + VarTotValorCC + VarTotGuias + VarTotCheques + VarTotEncomendas + VarTotalDoc
'                VarTotParaGuia = VarTotRespLetras + VarTotValorCC + VarTotGuias + VarTotCheques + VarTotalDoc

'                If Me.DocumentoVenda.Tipodoc = "ECL" And VarTotalDoc <> VarTotalDocGravado Then

'                    If VarTotParaEncomenda > VarPlafondCliente Then

'                        If VarLocalTeste = 0 Then
'                            MsgBox("Cliente com plafond ultrapassado!", vbInformation + vbOKOnly)
'                        ElseIf VarLocalTeste = 1 Then
'                            If MsgBox("Cliente com plafond ultrapassado!" & Chr(13) & "Deseja mesmo assim gravar o documento?", vbCritical + vbYesNo) = vbYes Then
'                                EnvioEmailPlafond()
'                            Else
'                                VarCancelaDoc = True
'                            End If
'                        End If

'                    End If

'                ElseIf (Me.DocumentoVenda.Tipodoc = "GC" Or Me.DocumentoVenda.Tipodoc = "GR") And VarTotalDoc <> VarTotalDocGravado Then

'                    If VarTotParaGuia > VarPlafondCliente Then

'                        If VarLocalTeste = 0 Then
'                            MsgBox("Cliente com plafond ultrapassado!", vbInformation + vbOKOnly)
'                        ElseIf VarLocalTeste = 1 Then
'                            If MsgBox("Cliente com plafond ultrapassado!" & Chr(13) & "Deseja mesmo assim gravar o documento?", vbCritical + vbYesNo) = vbYes Then
'                                EnvioEmailPlafond()
'                            Else
'                                VarCancelaDoc = True
'                            End If
'                        End If

'                    End If

'                End If

'                VarTotalDocGravado = 0

'                Module1.FechaObjEmpresa()
'            End If

'        End Function

'        Dim VarFrom As String
'        Dim VarTo As String
'        Dim VarAssunto As String
'        Dim VarTextoInicialMsg As String
'        Dim VarMensagem As String
'        Dim VarUtilizador As String
'        Private Function EnvioEmailPlafond()

'            VarFrom = ""
'            'VarTo = "mgoretti@mundifios.pt"
'            VarTo = "tesouraria.clientes@mundifios.pt;"
'            'VarTo = "jafernandes@mundifios.pt; mgoretti@mundifios.pt;"

'            If Format(Now, "HH:mm") >= "07:00" And Format(Now, "HH:mm") <= "12:59" Then
'                VarTextoInicialMsg = "Bom dia,"
'            ElseIf Format(Now, "HH:mm") >= "13:00" And Format(Now, "HH:mm") <= "19:59" Then
'                VarTextoInicialMsg = "Boa tarde,"
'            Else
'                VarTextoInicialMsg = "Boa noite,"
'            End If

'            VarAssunto = "Plafond Ultrapassado: (" & VarCliente & ") - " & Replace(VarClienteNome, "'", "")

'            VarUtilizador = Aplicacao.Utilizador.Utilizador
'            Dim i As Long
'            For i = 1 To Me.DocumentoVenda.Linhas.NumItens
'                If Me.DocumentoVenda.Linhas.GetEdita(i).Artigo <> "" And Me.DocumentoVenda.Linhas.GetEdita(i).Lote <> "" Then
'                    Exit For
'                End If
'            Next i


'            If Me.DocumentoVenda.Tipodoc = "ECL" Then

'                VarMensagem = VarTextoInicialMsg & Chr(13) & Chr(13) & Chr(13) & "Foi emitido um documento no Primavera para um cliente com o plafond ultrapassado:" & Chr(13) & Chr(13) & "" _
'                            & "Empresa:                         " & BSO.Contexto.CodEmp & " - " & BSO.Contexto.IDNome & Chr(13) & "" _
'                            & "Empresa Destino:                 " & BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).CamposUtil("CDU_NomeEmpresaGrupo").Valor & Chr(13) & "" _
'                            & "Utilizador:                      " & VarUtilizador & Chr(13) & Chr(13) & "" _
'                            & "Cliente:                         " & VarCliente & " - " & Replace(VarClienteNome, "'", "") & Chr(13) & "" _
'                            & "Palfond:                         " & IIf(VarPlafondCliente = 0, "0,00", Format(VarPlafondCliente, "#,###.00")) & Chr(13) & "" _
'                            & "Documento:                       " & Me.DocumentoVenda.Tipodoc & " " & Format(Me.DocumentoVenda.NumDoc, "#,###") & "/" & Me.DocumentoVenda.Serie & Chr(13) & "" _
'                            & "Valor Documento:                 " & IIf(VarTotalDoc = 0, "0,00", Format(VarTotalDoc, "#,###.00")) & Chr(13) & Chr(13) & "" _
'                            & "Responsabilidade em Letras:      " & IIf(VarTotRespLetras = 0, "0,00", Format(VarTotRespLetras, "#,###.00")) & Chr(13) & "" _
'                            & "Valor em  Conta Corrente:        " & IIf(VarTotValorCC = 0, "0,00", Format(VarTotValorCC, "#,###.00")) & Chr(13) & "" _
'                            & "Guias de Remessa Por Facturar:   " & IIf(VarTotGuias = 0, "0,00", Format(VarTotGuias, "#,###.00")) & Chr(13) & "" _
'                            & "Cheques Pré-Datados:             " & IIf(VarTotCheques = 0, "0,00", Format(VarTotCheques, "#,###.00")) & Chr(13) & "" _
'                            & "Encomendas em Carteira:          " & IIf(VarTotEncomendas = 0, "0,00", Format(VarTotEncomendas, "#,###.00")) & Chr(13) & Chr(13) & "" _
'                            & "Totais + Valor Documento:        " & IIf(VarTotParaEncomenda = 0, "0,00", Format(VarTotParaEncomenda, "#,###.00")) & Chr(13) & Chr(13) & Chr(13) & "" _
'                            & "Cumprimentos"

'            ElseIf Me.DocumentoVenda.Tipodoc = "GC" Then

'                VarMensagem = VarTextoInicialMsg & Chr(13) & Chr(13) & Chr(13) & "Foi emitido um documento no Primavera para um cliente com o plafond ultrapassado:" & Chr(13) & Chr(13) & "" _
'                            & "Empresa:                         " & BSO.Contexto.CodEmp & " - " & BSO.Contexto.IDNome & Chr(13) & "" _
'                            & "Empresa Destino:                 " & BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).CamposUtil("CDU_NomeEmpresaGrupo").Valor & Chr(13) & "" _
'                            & "Utilizador:                      " & VarUtilizador & Chr(13) & Chr(13) & "" _
'                            & "Cliente:                         " & VarCliente & " - " & Replace(VarClienteNome, "'", "") & Chr(13) & "" _
'                            & "Palfond:                         " & IIf(VarPlafondCliente = 0, "0,00", Format(VarPlafondCliente, "#,###.00")) & Chr(13) & "" _
'                            & "Documento:                       " & Me.DocumentoVenda.Tipodoc & " " & Format(Me.DocumentoVenda.NumDoc, "#,###") & "/" & Me.DocumentoVenda.Serie & Chr(13) & "" _
'                            & "Data Expedição:                  " & Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_DataExp") & "" _
'                            & "Valor Documento:                 " & IIf(VarTotalDoc = 0, "0,00", Format(VarTotalDoc, "#,###.00")) & Chr(13) & Chr(13) & "" _
'                            & "Responsabilidade em Letras:      " & IIf(VarTotRespLetras = 0, "0,00", Format(VarTotRespLetras, "#,###.00")) & Chr(13) & "" _
'                            & "Valor em  Conta Corrente:        " & IIf(VarTotValorCC = 0, "0,00", Format(VarTotValorCC, "#,###.00")) & Chr(13) & "" _
'                            & "Guias de Remessa Por Facturar:   " & IIf(VarTotGuias = 0, "0,00", Format(VarTotGuias, "#,###.00")) & Chr(13) & "" _
'                            & "Cheques Pré-Datados:             " & IIf(VarTotCheques = 0, "0,00", Format(VarTotCheques, "#,###.00")) & Chr(13) & Chr(13) & Chr(13) & "" _
'                            & "Totais + Valor Documento:        " & IIf(VarTotParaGuia = 0, "0,00", Format(VarTotParaGuia, "#,###.00")) & Chr(13) & Chr(13) & Chr(13) & "" _
'                            & "Cumprimentos"

'            ElseIf Me.DocumentoVenda.Tipodoc = "GR" Then

'                VarMensagem = VarTextoInicialMsg & Chr(13) & Chr(13) & Chr(13) & "Foi emitido um documento no Primavera para um cliente com o plafond ultrapassado:" & Chr(13) & Chr(13) & "" _
'                            & "Empresa:                         " & BSO.Contexto.CodEmp & " - " & BSO.Contexto.IDNome & Chr(13) & "" _
'                            & "Empresa Destino:                 " & BSO.Base.Clientes.Edita(Me.DocumentoVenda.Entidade).CamposUtil("CDU_NomeEmpresaGrupo").Valor & Chr(13) & "" _
'                            & "Utilizador:                      " & VarUtilizador & Chr(13) & Chr(13) & "" _
'                            & "Cliente:                         " & VarCliente & " - " & Replace(VarClienteNome, "'", "") & Chr(13) & "" _
'                            & "Palfond:                         " & IIf(VarPlafondCliente = 0, "0,00", Format(VarPlafondCliente, "#,###.00")) & Chr(13) & "" _
'                            & "Documento:                       " & Me.DocumentoVenda.Tipodoc & " " & Format(Me.DocumentoVenda.NumDoc, "#,###") & "/" & Me.DocumentoVenda.Serie & Chr(13) & "" _
'                            & "Valor Documento:                 " & IIf(VarTotalDoc = 0, "0,00", Format(VarTotalDoc, "#,###.00")) & Chr(13) & Chr(13) & "" _
'                            & "Responsabilidade em Letras:      " & IIf(VarTotRespLetras = 0, "0,00", Format(VarTotRespLetras, "#,###.00")) & Chr(13) & "" _
'                            & "Valor em  Conta Corrente:        " & IIf(VarTotValorCC = 0, "0,00", Format(VarTotValorCC, "#,###.00")) & Chr(13) & "" _
'                            & "Guias de Remessa Por Facturar:   " & IIf(VarTotGuias = 0, "0,00", Format(VarTotGuias, "#,###.00")) & Chr(13) & "" _
'                            & "Cheques Pré-Datados:             " & IIf(VarTotCheques = 0, "0,00", Format(VarTotCheques, "#,###.00")) & Chr(13) & Chr(13) & Chr(13) & "" _
'                            & "Totais + Valor Documento:        " & IIf(VarTotParaGuia = 0, "0,00", Format(VarTotParaGuia, "#,###.00")) & Chr(13) & Chr(13) & Chr(13) & "" _
'                            & "Cumprimentos"

'            End If

'            BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL] ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "', '" & VarFrom & "', '" & VarTo & "','','','" & VarAssunto & "','" & VarMensagem & "','',0,'" & VarUtilizador & "' )")


'        End Function



'        Private Function TestaPlafond()

'            If Me.DocumentoVenda.Entidade & "" <> "" Then

'                VarCliente = Me.DocumentoVenda.Entidade
'                VarClienteNome = BSO.Base.Clientes.Edita(VarCliente).Nome
'                If BSO.Base.Clientes.Edita(VarCliente).Limitecredito & "" = "" Then
'                    VarPlafondCred = 0
'                Else
'                    VarPlafondCred = BSO.Base.Clientes.Edita(VarCliente).Limitecredito
'                End If


'                '######  Retirada a soma do VarPlafondExtra no dia 04/11/2016 a pedido da Dona Goretti. #######
'                'If BSO.Comercial.Clientes.Edita(VarCliente).CamposUtil("CDU_PlafondExtra").Valor & "" = "" Then
'                'VarPlafondExtra = 0
'                ' Else
'                'VarPlafondExtra = BSO.Comercial.Clientes.Edita(VarCliente).CamposUtil("CDU_PlafondExtra").Valor
'                ' End If

'                'VarPlafondCliente = VarPlafondCred + VarPlafondExtra

'                VarPlafondCliente = VarPlafondCred

'                '1. Responsabilidade em Letras
'                '2. Valor em  Conta Corrente
'                '3. Guias de Remessa Por Facturar
'                '4. Cheques Pré-Datados
'                '5. Encomendas em Carteira

'                SqlStringTotalDeb = "SELECT TOP 1 (SELECT ISNULL(SUM(CASE Moeda WHEN 'EUR' THEN ValorPendente ELSE ValorPendente / Cambio END), 0) AS ValorContaCorrente " _
'                                & "FROM Pendentes WHERE (TipoEntidade = 'C') AND (TipoConta = 'CCC') AND estado = 'PEN' AND Entidade = '" & VarCliente & "' " _
'                                & "AND DataVenc < '2099-12-31') AS ValorContaCorrente " _
'                                & ", (SELECT ISNULL(SUM(CASE Moeda WHEN 'EUR' THEN ValorPendente ELSE ValorPendente / Cambio END), 0) AS ValorContaCorrente " _
'                                & "FROM Pendentes WHERE (TipoEntidade = 'C') AND (TipoConta = 'CCC') AND estado = 'CPD' AND Entidade = '" & VarCliente & "' " _
'                                & "AND DataVenc < '2099-12-31') AS ValorCPD " _
'                                & ", (SELECT ISNULL(SUM(CASE Moeda WHEN 'EUR' THEN ValorPendente ELSE ValorPendente / Cambio END), 0) AS ValorContaCorrente " _
'                                & "FROM Pendentes WHERE (TipoEntidade = 'C') AND (TipoConta = 'CLR') AND Entidade = '" & VarCliente & "' " _
'                                & "AND DataVenc < '2099-12-31') AS ValorLetras " _
'                                & "FROM Pendentes AS Pendentes_1"

'                ListaTotalDeb = BSO.Consulta(SqlStringTotalDeb)

'                If ListaTotalDeb.Vazia = True Then

'                    VarTotValorCC = 0
'                    VarTotCheques = 0
'                    VarTotRespLetras = 0

'                Else

'                    VarTotValorCC = Math.Round(ListaTotalDeb.Valor("ValorContaCorrente"), 2)
'                    VarTotCheques = Math.Round(ListaTotalDeb.Valor("ValorCPD"), 2)
'                    VarTotRespLetras = Math.Round(ListaTotalDeb.Valor("ValorLetras"), 2)

'                End If

'                SqlStringTotalDeb = "SELECT TOP 1 (SELECT ISNULL(SUM(CASE moeda WHEN 'EUR' THEN ((LinhasDoc.TaxaIva / 100) + 1) * (LinhasDoc.PrecoLiquido - (LinhasDocStatus.QuantTrans * LinhasDoc.PrecUnit)) " _
'                                & "ELSE ((LinhasDoc.TaxaIva / 100) + 1) * ((LinhasDoc.PrecoLiquido - (LinhasDocStatus.QuantTrans * LinhasDoc.PrecUnit))) / cambio END), 0) AS ValorGuiasRemessa " _
'                                & "FROM CabecDoc INNER JOIN LinhasDoc ON CabecDoc.Id = LinhasDoc.IdCabecDoc INNER JOIN LinhasDocStatus ON LinhasDoc.Id = LinhasDocStatus.IdLinhasDoc INNER JOIN DocumentosVenda ON CabecDoc.TipoDoc = DocumentosVenda.Documento INNER JOIN CabecDocStatus ON LinhasDoc.IdCabecDoc = CabecDocStatus.IdCabecDoc " _
'                                & "WHERE (DocumentosVenda.TipoDocumento = 3) AND (CabecDocStatus.Estado = 'P') AND (LinhasDocStatus.EstadoTrans = 'P') AND (CabecDoc.Entidade = '" & VarCliente & "') " _
'                                & "AND CabecDoc.DataVencimento < '2099-12-31' AND (CabecDocStatus.Fechado = 0) AND (LinhasDocStatus.Fechado = 0) AND (CabecDocStatus.Anulado = 0) " _
'                                & "AND cabecdoc.tipodoc = 'GR') AS ValorGuiasRemessa " _
'                                & ", (SELECT ISNULL(SUM(CASE moeda WHEN 'EUR' THEN ((LinhasDoc.TaxaIva / 100) + 1) * (LinhasDoc.PrecoLiquido - (LinhasDocStatus.QuantTrans * LinhasDoc.PrecUnit)) " _
'                                & "ELSE ((LinhasDoc.TaxaIva / 100) + 1) * ((LinhasDoc.PrecoLiquido - (LinhasDocStatus.QuantTrans * LinhasDoc.PrecUnit)) / cambio) END), 0) AS ValorEncomedasCarteira " _
'                                & "FROM CabecDoc INNER JOIN LinhasDoc ON CabecDoc.Id = LinhasDoc.IdCabecDoc INNER JOIN LinhasDocStatus ON LinhasDoc.Id = LinhasDocStatus.IdLinhasDoc INNER JOIN DocumentosVenda ON CabecDoc.TipoDoc = DocumentosVenda.Documento INNER JOIN CabecDocStatus ON LinhasDoc.IdCabecDoc = CabecDocStatus.IdCabecDoc " _
'                                & "WHERE (DocumentosVenda.TipoDocumento = 2) AND (CabecDocStatus.Estado = 'P') AND (LinhasDocStatus.EstadoTrans = 'P') AND (CabecDoc.Entidade = '" & VarCliente & "') " _
'                                & "AND (CabecDocStatus.Fechado = 0) AND (LinhasDocStatus.Fechado = 0) AND (CabecDocStatus.Anulado = 0) " _
'                                & "AND CabecDoc.DataVencimento < '2099-12-31') AS ValorEncomendasCarteira " _
'                                & "FROM CabecDoc AS CabecDoc_1"

'                ListaTotalDeb = BSO.Consulta(SqlStringTotalDeb)

'                If ListaTotalDeb.Vazia = True Then

'                    VarTotGuias = 0
'                    VarTotEncomendas = 0

'                Else

'                    VarTotGuias = Math.Round(ListaTotalDeb.Valor("ValorGuiasRemessa"), 2)
'                    VarTotEncomendas = Math.Round(ListaTotalDeb.Valor("ValorEncomendasCarteira"), 2)

'                End If

'                VarTotalDoc = Me.DocumentoVenda.TotalDocumento

'                VarTotParaEncomenda = VarTotRespLetras + VarTotValorCC + VarTotGuias + VarTotCheques + VarTotEncomendas + VarTotalDoc
'                VarTotParaGuia = VarTotRespLetras + VarTotValorCC + VarTotGuias + VarTotCheques + VarTotalDoc

'                If Me.DocumentoVenda.Tipodoc = "ECL" And VarTotalDoc <> VarTotalDocGravado Then

'                    If VarTotParaEncomenda > VarPlafondCliente Then

'                        If VarLocalTeste = 0 Then
'                            MsgBox("Cliente com plafond ultrapassado!", vbInformation + vbOKOnly)
'                        ElseIf VarLocalTeste = 1 Then
'                            If MsgBox("Cliente com plafond ultrapassado!" & Chr(13) & "Deseja mesmo assim gravar o documento?", vbCritical + vbYesNo) = vbYes Then
'                                EnvioEmailPlafond()
'                            Else
'                                VarCancelaDoc = True
'                            End If
'                        End If

'                    End If

'                ElseIf (Me.DocumentoVenda.Tipodoc = "GC" Or Me.DocumentoVenda.Tipodoc = "GR") And VarTotalDoc <> VarTotalDocGravado Then

'                    If VarTotParaGuia > VarPlafondCliente Then

'                        If VarLocalTeste = 0 Then
'                            MsgBox("Cliente com plafond ultrapassado!", vbInformation + vbOKOnly)
'                        ElseIf VarLocalTeste = 1 Then
'                            If MsgBox("Cliente com plafond ultrapassado!" & Chr(13) & "Deseja mesmo assim gravar o documento?", vbCritical + vbYesNo) = vbYes Then
'                                EnvioEmailPlafond()
'                            Else
'                                VarCancelaDoc = True
'                            End If
'                        End If

'                    End If

'                End If

'                VarTotalDocGravado = 0

'            End If

'        End Function


'    End Class
'End Namespace
