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
Imports StdPlatBS100.StdBSTipos
Imports BasBE100.BasBETiposGcp
Imports System.IO
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService
Imports Primavera.Extensibility.Constants.ExtensibilityService

Namespace Filopa
    Public Class VndIsEditorVendas
        Inherits EditorVendas

        Public Overrides Sub AntesDeImprimir(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeImprimir(Cancel, e)

            If Module1.VerificaToken("Filopa") = 1 Then


                If Me.DocumentoVenda.Tipodoc = "NEC" Then

                    ImprimeNEC
                    Cancel = True


                End If

                If Me.DocumentoVenda.Tipodoc = "CNT" Then

                    ImprimeCNT
                    Cancel = True


                End If

            End If

        End Sub

        Public Overrides Sub ArtigoIdentificado(Artigo As String, NumLinha As Integer, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.ArtigoIdentificado(Artigo, NumLinha, Cancel, e)
            If Module1.VerificaToken("Filopa") = 1 Then

                '#################################################################################################################################
                '####### Verifica se o artigo existe na TDU_ArtigosNoSplice, se existir aparece comentario na linha abaixo - BRUNO 27/11/2020   ##
                '#################################################################################################################################

                If Me.DocumentoVenda.Tipodoc = "NEC" Then

                    Dim sqlArtigoNoSplice As StdBELista

                    sqlArtigoNoSplice = BSO.Consulta("select CDU_Artigo from TDU_ArtigosNoSplice where CDU_Artigo='" & Artigo & "'")
                    If sqlArtigoNoSplice.Vazia = False Then
                        BSO.Vendas.Documentos.AdicionaLinhaEspecial(Me.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "TFO Knotted Yarn: No Splice should be used in the double yarn")

                    End If
                End If


                '#################################################################################################################################
                '####### Verifica se o artigo existe na TDU_ArtigosNoSplice, se existir aparece comentario na linha abaixo - BRUNO 27/11/2020   ##
                '#################################################################################################################################


                '########################################################################################################################################
                '####### Verifica se o artigo existe na pesquisa pedida pelo Jorge, se existir aparece comentario na linha abaixo - BRUNO 09/04/2021   ##
                '########################################################################################################################################
                If Me.DocumentoVenda.Tipodoc = "NEC" Then

                    Dim sqlArtigosDescExtraDY As StdBELista

                    sqlArtigosDescExtraDY = BSO.Consulta("select distinct a.Artigo, a.Descricao, a.CDU_DescricaoExtra from Artigo a inner join VMP_ART_TipoArtigo on VMP_ART_TipoArtigo.CodigoArtigo=a.Artigo inner join VMP_ART_Tipo on VMP_ART_Tipo.Id=VMP_ART_TipoArtigo.IdTipo inner join VMP_ART_NE ne on ne.Codigo=a.CDU_NE where a.ArtigoAnulado='0'and a.Familia='F01'and a.SubFamilia='01' and VMP_ART_Tipo.Codigo='006'and  (a.CDU_OrdenacaoDescricaoExtra not like '%C018%' and  a.CDU_OrdenacaoDescricaoExtra not like '%C011%' )and a.CDU_DescricaoTorcao2='L'and a.CDU_DescricaoComponentes='CO' and a.CDU_DescricaoComponentesPerc='100'and ne.Cabos='2'and ne.NE between '20' and '40'  and a.Artigo='" & Artigo & "'")
                    If sqlArtigosDescExtraDY.Vazia = False Then
                        BSO.Vendas.Documentos.AdicionaLinhaEspecial(Me.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "The Double Yarn TPI should be Half of the Single Yarn TPI")

                    End If
                End If


                '########################################################################################################################################
                '####### Verifica se o artigo existe na pesquisa pedida pelo Jorge, se existir aparece comentario na linha abaixo - BRUNO 09/04/2021   ##
                '########################################################################################################################################

                'JFC Copiar Vendedor e Comissão do Cabeçalho do Documento para a Linha assim que o artigo é identificado 24/01/2019
                Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Vendedor = Me.DocumentoVenda.CamposUtil("CDU_Vendedor").Valor
                Me.DocumentoVenda.Linhas.GetEdita(NumLinha).Comissao = Me.DocumentoVenda.CamposUtil("CDU_ComissaoVendedor").Valor


            End If
        End Sub


        Public Overrides Sub ClienteIdentificado(Cliente As String, ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.ClienteIdentificado(Cliente, Cancel, e)
            If Module1.VerificaToken("Filopa") = 1 Then

                If Me.DocumentoVenda.CamposUtil("CDU_Remarks").Valor & "" = "" Then

                    Me.DocumentoVenda.CamposUtil("CDU_Remarks").Valor = "- 'A' GRADE QUALITY WITH ALL DYEING GUARANTEES" & Chr(13) &
                "- CONES WITH YARN RESERVE (TAIL)" & Chr(13) &
                "- PACKING IN NEUTRAL PALLETS/CONES / LENGTH MEASURED CONES" & Chr(13) &
                "- COVERING LETTER MUST MENTION CLEARLY AGENT COMMISSION." & Chr(13) &
                "- SPECS SHOULD BE SENT FOR APPROVAL BEFORE SHIPMENT" & Chr(13) &
                "- NO HUMIDITY ADJUSTMENTS ARE ALLOWED" & Chr(13) &
                "- THE PRODUCT IN THIS ORDER CONFIRMATION MUST BE IN ACCORDANCE WITH THE RESPECTIVE OEKO TEX STANDARD 100 CERTIFICATES"

                End If

            End If

        End Sub

        Public Overrides Sub AntesDeGravar(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeGravar(Cancel, e)
            If Module1.VerificaToken("Filopa") = 1 Then

                '#################################################################
                '####### Verifica se CDU_Fornecedor é válido - JFC 24/01/2019   ##
                '#################################################################
                If Me.DocumentoVenda.Tipodoc = "NEC" Or Me.DocumentoVenda.Tipodoc = "CNT" Or Me.DocumentoVenda.Tipodoc = "EMB" Then
                    If BSO.Base.Fornecedores.Existe(Me.DocumentoVenda.CamposUtil("CDU_Fornecedor").Valor) = False Then
                        MsgBox("O numero de Cliente não é válido: " & Me.DocumentoVenda.CamposUtil("CDU_Fornecedor").Valor & "" & vbCritical)
                        Cancel = True

                    End If
                End If
                '#################################################################
                '####### Verifica se CDU_Fornecedor é válido - JFC 24/01/2019   ##
                '#################################################################

                'JFC 08/10/2018
                'Guardar a data prevista de embarque original. Pedido de Pedro Lobato
                If Me.DocumentoVenda.Tipodoc = "CNT" Then

                    Dim j As Long
                    For j = 1 To Me.DocumentoVenda.Linhas.NumItens
                        If Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_DataEntregaOriginal").Valor & "" = "" And Me.DocumentoVenda.Linhas.GetEdita(j).Artigo & "" <> "" Then

                            Me.DocumentoVenda.Linhas.GetEdita(j).CamposUtil("CDU_DataEntregaOriginal").Valor = Me.DocumentoVenda.Linhas.GetEdita(j).DataEntrega

                        End If
                    Next j

                End If


                'JFC Gravar o nome do Cliente (Ficha Fornecedor) no local de descarga. Para aparecer nas diversas listas e editor de transformação.
                If Me.DocumentoVenda.CamposUtil("CDU_Fornecedor").Valor & "" <> "" Then
                    Me.DocumentoVenda.LocalDescarga = BSO.Base.Fornecedores.DaNome(Me.DocumentoVenda.CamposUtil("CDU_Fornecedor").Valor)
                End If

                'JFC Validar as comissões
                If Me.DocumentoVenda.Tipodoc <> "NEC" Then

                    Dim TransValida As StdBELista
                    Dim TransDiff As Boolean
                    Dim TransString As String
                    Dim TransMsg As Boolean

                    TransMsg = False
                    TransString = "Atenção, valores diferentes da linha original!"

                    For i = 1 To Me.DocumentoVenda.Linhas.NumItens

                        TransDiff = False

                        If Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & "" <> "" And Me.DocumentoVenda.Linhas.GetEdita(i).IDLinhaOriginal & "" <> "" Then

                            TransValida = BSO.Consulta("select concat(cd.TipoDoc, ' ', cd.NumDoc,'/', cd.Serie) as 'Doc',  ln.Artigo, ln.CDU_Comissao, ln.Comissao, ln.Vendedor from linhasdoc ln inner join cabecdoc cd on cd.id=ln.idcabecdoc where ln.Id='" & Me.DocumentoVenda.Linhas.GetEdita(i).IDLinhaOriginal & "'")

                            TransValida.Inicio()

                            If Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_Comissao") <> TransValida.Valor("CDU_Comissao") Then
                                TransDiff = True
                            ElseIf Me.DocumentoVenda.Linhas.GetEdita(i).Comissao <> TransValida.Valor("Comissao") Then
                                TransDiff = True
                            ElseIf Me.DocumentoVenda.Linhas.GetEdita(i).Vendedor <> TransValida.Valor("Vendedor") Then
                                TransDiff = True
                            End If

                        End If

                        If TransDiff Then

                            TransString = "" & TransString & "" & Chr(13) & Chr(13) & TransValida.Valor("Doc") & "    -->  " & " Linha: " & i & " - Artigo: " & Me.DocumentoVenda.Linhas.GetEdita(i).Artigo & Chr(13) & "Comissao: " & TransValida.Valor("CDU_Comissao") & "% --> " & Me.DocumentoVenda.Linhas.GetEdita(i).CamposUtil("CDU_Comissao") & "%" & Chr(13) & "Vendedor: " & TransValida.Valor("Vendedor") & " --> " & Me.DocumentoVenda.Linhas.GetEdita(i).Vendedor & Chr(13) & "Com.Vend: " & TransValida.Valor("Comissao") & "% --> " & Me.DocumentoVenda.Linhas.GetEdita(i).Comissao & "%"
                            TransMsg = True
                        End If

                    Next i
                    If TransMsg Then
                        If vbNo = MsgBox("" & TransString & Chr(13) & Chr(13) & "Deseja continuar com a gravação?", vbYesNo, "Validação Comissões") Then
                            Cancel = True
                            Exit Sub
                        End If
                    End If

                End If


                '    '*******************************************************************************************************************************************
                '    ''#### Bruno Peixoto 18/02/2020 ### Validar se em NEC/CNT/EMB tem produtos certificados e se o fornecedor é Certificado ####
                '    '*******************************************************************************************************************************************

                If Me.DocumentoVenda.Tipodoc = "CNT" Or Me.DocumentoVenda.Tipodoc = "EMB" Or Me.DocumentoVenda.Tipodoc = "NEC" Then
                    Dim l As Long
                    Dim listFornCert As StdBELista

                    listFornCert = BSO.Consulta("select *, getdate() as 'Hoje' from Clientes where Cliente='" & Me.DocumentoVenda.Entidade & "'")
                    listFornCert.Inicio()

                    For l = 1 To Me.DocumentoVenda.Linhas.NumItens
                        If Me.DocumentoVenda.Linhas.GetEdita(l).Artigo & "" <> "" Then
                            If UCase(BSO.Base.Artigos.Edita(Me.DocumentoVenda.Linhas.GetEdita(l).Artigo).CamposUtil("CDU_DescricaoExtra").Valor & BSO.Base.Artigos.Edita(Me.DocumentoVenda.Linhas.GetEdita(l).Artigo).Descricao) Like "*GOTS*" Then

                                If listFornCert.Valor("CDU_Gots") <> True Then
                                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor não está identificado como certificado Gots.", vbCritical + vbOKOnly)
                                ElseIf listFornCert.Valor("CDU_GotsData") < listFornCert.Valor("Hoje") Then
                                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor tem o certificado Gots expirado.", vbCritical + vbOKOnly)
                                End If
                            End If

                            If UCase(BSO.Base.Artigos.Edita(Me.DocumentoVenda.Linhas.GetEdita(l).Artigo).CamposUtil("CDU_DescricaoExtra").Valor & BSO.Base.Artigos.Edita(Me.DocumentoVenda.Linhas.GetEdita(l).Artigo).Descricao) Like "*GRS*" Then
                                If listFornCert.Valor("CDU_Grs") <> True Then
                                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor não está identificado como certificado GRS.", vbCritical + vbOKOnly)
                                ElseIf listFornCert.Valor("CDU_GrsData") < listFornCert.Valor("Hoje") Then
                                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor tem o certificado GRS expirado.", vbCritical + vbOKOnly)
                                End If
                            End If

                            If UCase(BSO.Base.Artigos.Edita(Me.DocumentoVenda.Linhas.GetEdita(l).Artigo).CamposUtil("CDU_DescricaoExtra").Valor & BSO.Base.Artigos.Edita(Me.DocumentoVenda.Linhas.GetEdita(l).Artigo).Descricao) Like "*OCS*" Then
                                If listFornCert.Valor("CDU_Ocs") <> True Then
                                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor não está identificado como certificado OCS.", vbCritical + vbOKOnly)
                                ElseIf listFornCert.Valor("CDU_OcsData") < listFornCert.Valor("Hoje") Then
                                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor tem o certificado OCS expirado.", vbCritical + vbOKOnly)
                                End If
                            End If

                            If UCase(Me.DocumentoVenda.Linhas.GetEdita(l).CamposUtil("CDU_ObsLote").Valor) Like "*BCI*" Then
                                If listFornCert.Valor("CDU_Bci") <> True Then
                                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor não está identificado como certificado BCI.", vbCritical + vbOKOnly)
                                ElseIf listFornCert.Valor("CDU_BciData") < listFornCert.Valor("Hoje") Then
                                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor tem o certificado BCI expirado.", vbCritical + vbOKOnly)
                                End If
                            End If

                            If UCase(Me.DocumentoVenda.Linhas.GetEdita(l).Descricao) Like "*BCI*" Then
                                If listFornCert.Valor("CDU_Bci") <> True Then
                                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor não está identificado como certificado BCI.", vbCritical + vbOKOnly)
                                ElseIf listFornCert.Valor("CDU_BciData") < listFornCert.Valor("Hoje") Then
                                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor tem o certificado BCI expirado.", vbCritical + vbOKOnly)
                                End If
                            End If

                            If UCase(BSO.Base.Artigos.Edita(Me.DocumentoVenda.Linhas.GetEdita(l).Artigo).CamposUtil("CDU_DescricaoExtra").Valor & BSO.Base.Artigos.Edita(Me.DocumentoVenda.Linhas.GetEdita(l).Artigo).Descricao) Like "*FSC*" Then
                                If listFornCert.Valor("CDU_Fsc") <> True Then
                                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor não está identificado como certificado FSC.", vbCritical + vbOKOnly)
                                ElseIf listFornCert.Valor("CDU_FscData") < listFornCert.Valor("Hoje") Then
                                    MsgBox("Atenção:" & Chr(13) & "O Fornecedor tem o certificado FSC expirado.", vbCritical + vbOKOnly)
                                End If
                            End If
                        End If
                    Next l


                End If
            End If

        End Sub

        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

            If Module1.VerificaToken("Filopa") = 1 Then

                '###################################################################################################################################################################################################
                '###### Copiar o CDU_NFatura, CDU_NContentor, CDU_CNT_DataEmbarque do Embarque para o respetivo Contrato transformado. JFC - 24/01/2019 - Pedido de Pedro Lobrato para aparecer na lista Contratos##
                '###################################################################################################################################################################################################
                If Me.DocumentoVenda.Tipodoc = "EMB" Then

                    BSO.DSO.ExecuteSQL("update ln2 set ln2.CDU_NFatura=ln.CDU_NFatura, ln2.CDU_NContentor=ln.CDU_NContentor, ln2.CDU_CNT_DataEmbarque=cd.Data from CabecDoc cd inner join LinhasDoc ln on ln.IdCabecDoc=cd.Id inner join LinhasDocTrans lt on lt.IdLinhasDoc=ln.Id inner join LinhasDoc ln2 on ln2.Id=lt.IdLinhasDocOrigem inner join CabecDoc cd2 on cd2.Id=ln2.IdCabecDoc where cd.TipoDoc='EMB' and cd2.TipoDoc='CNT' and cd.Id='" & Me.DocumentoVenda.ID & "'")


                End If

                '###################################################################################################################################################################################################
                '###### Copiar o CDU_NFatura, CDU_NContentor, CDU_CNT_DataEmbarque do Embarque para o respetivo Contrato transformado. JFC - 24/01/2019 - Pedido de Pedro Lobrato para aparecer na lista Contratos##
                '###################################################################################################################################################################################################

            End If

        End Sub

        Public Overrides Sub DepoisDeTransformar(e As ExtensibilityEventArgs)
            MyBase.DepoisDeTransformar(e)

            If Module1.VerificaToken("Filopa") = 1 Then

                'JFC Quando documento é Pre-Invoice, Condição de Pagamento deixa de ser a que estava no Embarque
                'e passa a ser a que está definida na ficha do Cliente.
                If Me.DocumentoVenda.Tipodoc = "PF" Then
                    'Me.DocumentoVenda.CondPag = BSO.Comercial.Clientes.Consulta(Me.DocumentoVenda.Entidade).CondPag
                    If BSO.Base.Clientes.DaValorAtributo(Me.DocumentoVenda.Entidade, "CondPag") <> "" Then

                        Me.DocumentoVenda.CondPag = BSO.Base.Clientes.DaValorAtributo(Me.DocumentoVenda.Entidade, "CondPag")
                        BSO.Vendas.Documentos.PreencheDadosRelacionados(Me.DocumentoVenda, PreencheDados.enuDadosCondPag)

                    End If

                End If

            End If

        End Sub


        Public Overrides Sub TeclaPressionada(KeyCode As Integer, Shift As Integer, e As ExtensibilityEventArgs)
            MyBase.TeclaPressionada(KeyCode, Shift, e)

            If Module1.VerificaToken("Filopa") = 1 Then
                '#################################################################################################
                '# Inserir Certificados de Transação, Formulário FrmAlteraCertificadoTransacao2 (BRUNO 15/10/2020) #
                '#################################################################################################
                'Crtl+R- AlteraCertificadoTransacao




                If Me.DocumentoVenda.Tipodoc = "EMB" Then
                    If Me.LinhaActual > 0 Then
                        If KeyCode = 82 Then

                            Module1.certArtigo = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).Artigo
                            Module1.certDocumento = Me.DocumentoVenda.Tipodoc & " " & Me.DocumentoVenda.NumDoc & "/" & Me.DocumentoVenda.Serie
                            'certLote = Me.DocumentoVenda.Linhas(Me.LinhaActual).lote
                            Module1.certIDlinha = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).IdLinha
                            Module1.certEmitido = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).CamposUtil("CDU_CertificadoRecebido").Valor
                            Module1.certDescricao = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).Descricao

                            'Acrescentado dia 27/01/2021 - Bruno
                            Module1.certCancelado = Me.DocumentoVenda.Linhas.GetEdita(Me.LinhaActual).CamposUtil("CDU_CertificadoCancelado").Valor


                            Dim result As ExtensibilityResult = PriV100Api.BSO.Extensibility.CreateCustomFormInstance(GetType(FrmAlteraCertificadoTransacaoFilopaView))

                            If result.ResultCode = ExtensibilityResultCode.Ok Then

                                Dim frm As FrmAlteraCertificadoTransacaoFilopaView = result.Result
                                frm.ShowDialog()

                            End If

                        End If
                    End If
                End If



                '#################################################################################################
                '# Inserir Certificados de Transação, Formulário FrmAlteraCertificadoTransacao2 (BRUNO 15/10/2020) #
                '#################################################################################################

            End If
        End Sub

        Public Sub ImprimeNEC()
            Dim CaminhoFicheiro As String
            Dim NomeFicheiro As String

            CaminhoFicheiro = "C:\NEC\"
            'Verifica se a pasta existe.
            If Directory.Exists(CaminhoFicheiro) = False Then
                'caso não exista, cria a pasta
                Directory.CreateDirectory(CaminhoFicheiro)
            End If
            NomeFicheiro = Me.DocumentoVenda.Serie & "_" & Me.DocumentoVenda.NumDoc & "_" & Me.DocumentoVenda.Tipodoc & ".pdf"

            If File.Exists(CaminhoFicheiro & "\" & NomeFicheiro) = True Then
                'caso o ficheiro já exista, elimina o mesmo para poder criar um novo.
                File.Delete(CaminhoFicheiro & "\" & NomeFicheiro)
            End If

            'ImprimeEnc Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor, Me.DocumentoVenda.CamposUtil("CDU_SerieDocRastreabilidade").Valor, Me.DocumentoVenda.CamposUtil("CDU_NumDocRastreabilidade").Valor, BSO.Comercial.TabVendas.DaValorAtributo(Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor, "Descricao") & "Nº " & Me.DocumentoVenda.CamposUtil("CDU_NumDocRastreabilidade").Valor & " (" & BSO.Comercial.Series.DaValorAtributo("V", Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor, Me.DocumentoVenda.CamposUtil("CDU_SerieDocRastreabilidade").Valor, "DescricaoVia01") & ")", BSO.Comercial.Series.DaValorAtributo("V", Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor, Me.DocumentoVenda.CamposUtil("CDU_SerieDocRastreabilidade").Valor, "Config"), False, 1, CaminhoFicheiro
            On Error GoTo Erro
            PSO.Mapas.Inicializar("VND")
            PSO.Mapas.Destino = CRPEExportDestino.edFicheiro
            PSO.Mapas.SetFileProp(CRPEExportFormat.efPdf, CaminhoFicheiro & NomeFicheiro)


            PSO.Mapas.AddFormula("Nome", "'" + BSO.Contexto.IDNome + "'")
            PSO.Mapas.AddFormula("Contribuinte", "'" + "Contribuinte N.º: " + BSO.Contexto.IFNIF + "'")
            If BSO.Contexto.IDNumPorta & "" <> "" Then
                PSO.Mapas.AddFormula("Morada", "'" + BSO.Contexto.IDMorada + ", " + BSO.Contexto.IDNumPorta + "'")
            Else
                PSO.Mapas.AddFormula("Morada", "'" + BSO.Contexto.IDMorada + "'")
            End If
            PSO.Mapas.AddFormula("Localidade", "'" + BSO.Contexto.IDLocalidade + "'")
            PSO.Mapas.AddFormula("CodPostal", "'" + BSO.Contexto.IDCodPostal & " " & BSO.Contexto.IDCodPostalLocal + "'")
            PSO.Mapas.AddFormula("Telefone", "'" + "Telef. " + BSO.Contexto.IDIndicativoTelefone + "  " + BSO.Contexto.IDTelefone + "  Fax. " + BSO.Contexto.IDIndicativoFax + "  " + BSO.Contexto.IDFax + "'")

            PSO.Mapas.AddFormula("CapitalSocial", "'" + "Capital Social  " + Format(BSO.Contexto.ICCapitalSocial, "#,###.00") + " " + BSO.Contexto.ICMoedaCapSocial + "'")
            PSO.Mapas.AddFormula("Conservatoria", "'" + "Cons. Reg. Com. " + BSO.Contexto.ICConservatoria + "'")
            PSO.Mapas.AddFormula("Matricula", "'" + "Matricula N.º " + BSO.Contexto.ICMatricula + "'")
            PSO.Mapas.AddFormula("EMailEmpresa", "'" + BSO.Contexto.IDEmail + "'")
            PSO.Mapas.AddFormula("WebEmpresa", "'" + BSO.Contexto.IDWeb + "'")

            PSO.Mapas.AddFormula("NumVia", "'" + BSO.Base.Series.Edita("V", Me.DocumentoVenda.Tipodoc, Me.DocumentoVenda.Serie).DescricaoVia01 + "'")

            PSO.Mapas.AddFormula("lbl_Text23", "'" + BSO.Vendas.Documentos.DevolveTextoAssinaturaDoc(Me.DocumentoVenda.Tipodoc, Me.DocumentoVenda.Serie, Me.DocumentoVenda.NumDoc, "000") + "'")

            PSO.Mapas.AddFormula("NomeLicenca", "''")

            PSO.Mapas.AddFormula("Documento", "'" + BSO.Vendas.TabVendas.DaValorAtributo(Me.DocumentoVenda.Tipodoc, "Descricao") + " " + Me.DocumentoVenda.Tipodoc + " " + Me.DocumentoVenda.Serie + "/" + Format(Me.DocumentoVenda.NumDoc, 0) + "'")
            PSO.Mapas.SelectionFormula = "{CabecDoc.Filial} = '000' AND {CabecDoc.TipoDoc} = '" & Me.DocumentoVenda.Tipodoc & "' AND {CabecDoc.Serie} = '" & Me.DocumentoVenda.Serie & "' AND {CabecDoc.NumDoc} = " & Me.DocumentoVenda.NumDoc & ""
            PSO.Mapas.ImprimeListagem("NEC", "Order Confirmation", "P", 1, , , , , False)
            Exit Sub

Erro:
            MsgBox("Erro ao imprimir o mapa seleccionado." & vbCrLf & Err.Number & " - " & Err.Description, vbExclamation)

            'On Error GoTo Erro
            'PSO.Mapas.Inicializar "GCP"
            'Dim SelFormula As String
            'SelFormula = "{CabecDoc.Filial}='" & Me.DocumentoVenda.Filial & "' And {CabecDoc.Serie}='" & Me.DocumentoVenda.Serie & "' And {CabecDoc.TipoDoc}='" & Me.DocumentoVenda.TipoDoc & "' and {CabecDoc.NumDoc}= " & Me.DocumentoVenda.NumDoc
            '
            'PSO.Mapas.ImprimeListagem sReport:="NEC", sTitulo:="NEC " & Me.DocumentoVenda.NumDoc & "/" & Me.DocumentoVenda.Serie, sDestino:="W", iNumCopias:=1, bMapaSistema:=True, strUniqueIdentifier:=Me.DocumentoVenda.ID, sSelFormula:=SelFormula
            '
            'Exit Sub
            '
            'Erro:
            'MsgBox "Erro ao imprimir o mapa seleccionado." & vbCrLf & Err.Number & " - " & Err.Description, vbExclamation

        End Sub


        Public Sub ImprimeCNT()
            Dim CaminhoFicheiro As String
            Dim NomeFicheiro As String


            CaminhoFicheiro = "C:\CNT\"
            'Verifica se a pasta existe.
            If Directory.Exists(CaminhoFicheiro) = False Then
                'caso não exista, cria a pasta
                Directory.CreateDirectory(CaminhoFicheiro)
            End If
            NomeFicheiro = Me.DocumentoVenda.Serie & "_" & Me.DocumentoVenda.NumDoc & "_" & Me.DocumentoVenda.Tipodoc & ".pdf"

            If File.Exists(CaminhoFicheiro & "\" & NomeFicheiro) = True Then
                'caso o ficheiro já exista, elimina o mesmo para poder criar um novo.
                File.Delete(CaminhoFicheiro & "\" & NomeFicheiro)
            End If

            'ImprimeEnc Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor, Me.DocumentoVenda.CamposUtil("CDU_SerieDocRastreabilidade").Valor, Me.DocumentoVenda.CamposUtil("CDU_NumDocRastreabilidade").Valor, BSO.Comercial.TabVendas.DaValorAtributo(Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor, "Descricao") & "Nº " & Me.DocumentoVenda.CamposUtil("CDU_NumDocRastreabilidade").Valor & " (" & BSO.Comercial.Series.DaValorAtributo("V", Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor, Me.DocumentoVenda.CamposUtil("CDU_SerieDocRastreabilidade").Valor, "DescricaoVia01") & ")", BSO.Comercial.Series.DaValorAtributo("V", Me.DocumentoVenda.CamposUtil("CDU_TipoDocRastreabilidade").Valor, Me.DocumentoVenda.CamposUtil("CDU_SerieDocRastreabilidade").Valor, "Config"), False, 1, CaminhoFicheiro
            On Error GoTo Erro
            PSO.Mapas.Inicializar("VND")
            PSO.Mapas.Destino = CRPEExportDestino.edFicheiro
            PSO.Mapas.SetFileProp(CRPEExportFormat.efPdf, CaminhoFicheiro & NomeFicheiro)


            PSO.Mapas.AddFormula("Nome", "'" + BSO.Contexto.IDNome + "'")
            PSO.Mapas.AddFormula("Contribuinte", "'" + "Contribuinte N.º: " + BSO.Contexto.IFNIF + "'")
            If BSO.Contexto.IDNumPorta & "" <> "" Then
                PSO.Mapas.AddFormula("Morada", "'" + BSO.Contexto.IDMorada + ", " + BSO.Contexto.IDNumPorta + "'")
            Else
                PSO.Mapas.AddFormula("Morada", "'" + BSO.Contexto.IDMorada + "'")
            End If
            PSO.Mapas.AddFormula("Localidade", "'" + BSO.Contexto.IDLocalidade + "'")
            PSO.Mapas.AddFormula("CodPostal", "'" + BSO.Contexto.IDCodPostal & " " & BSO.Contexto.IDCodPostalLocal + "'")
            PSO.Mapas.AddFormula("Telefone", "'" + "Telef. " + BSO.Contexto.IDIndicativoTelefone + "  " + BSO.Contexto.IDTelefone + "  Fax. " + BSO.Contexto.IDIndicativoFax + "  " + BSO.Contexto.IDFax + "'")

            PSO.Mapas.AddFormula("CapitalSocial", "'" + "Capital Social  " + Format(BSO.Contexto.ICCapitalSocial, "#,###.00") + " " + BSO.Contexto.ICMoedaCapSocial + "'")
            PSO.Mapas.AddFormula("Conservatoria", "'" + "Cons. Reg. Com. " + BSO.Contexto.ICConservatoria + "'")
            PSO.Mapas.AddFormula("Matricula", "'" + "Matricula N.º " + BSO.Contexto.ICMatricula + "'")
            PSO.Mapas.AddFormula("EMailEmpresa", "'" + BSO.Contexto.IDEmail + "'")
            PSO.Mapas.AddFormula("WebEmpresa", "'" + BSO.Contexto.IDWeb + "'")

            PSO.Mapas.AddFormula("NumVia", "'" + BSO.Base.Series.Edita("V", Me.DocumentoVenda.Tipodoc, Me.DocumentoVenda.Serie).DescricaoVia01 + "'")

            PSO.Mapas.AddFormula("lbl_Text23", "'" + BSO.Vendas.Documentos.DevolveTextoAssinaturaDoc(Me.DocumentoVenda.Tipodoc, Me.DocumentoVenda.Serie, Me.DocumentoVenda.NumDoc, "000") + "'")

            PSO.Mapas.AddFormula("NomeLicenca", "''")

            PSO.Mapas.AddFormula("Documento", "'" + BSO.Vendas.TabVendas.DaValorAtributo(Me.DocumentoVenda.Tipodoc, "Descricao") + " " + Me.DocumentoVenda.Tipodoc + " " + Me.DocumentoVenda.Serie + "/" + Format(Me.DocumentoVenda.NumDoc, 0) + "'")
            PSO.Mapas.SelectionFormula = "{CabecDoc.Filial} = '000' AND {CabecDoc.TipoDoc} = '" & Me.DocumentoVenda.Tipodoc & "' AND {CabecDoc.Serie} = '" & Me.DocumentoVenda.Serie & "' AND {CabecDoc.NumDoc} = " & Me.DocumentoVenda.NumDoc & ""
            PSO.Mapas.ImprimeListagem("NEC", "Order Confirmation", "P", 1, , , , , False)
            Exit Sub

Erro:
            MsgBox("Erro ao imprimir o mapa seleccionado." & vbCrLf & Err.Number & " - " & Err.Description, vbExclamation)


        End Sub



    End Class
End Namespace
