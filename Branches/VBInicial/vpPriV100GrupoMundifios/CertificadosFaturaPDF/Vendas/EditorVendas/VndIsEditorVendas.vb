Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Primavera.Extensibility.Sales.Editors
Imports Primavera.Extensibility.BusinessEntities
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgsPublic
Imports Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs
Imports StdBE100
Imports Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.Generico
Imports StdPlatBS100.StdBSTipos
Imports System.IO
Imports System.Windows.Forms

Namespace CertificadosFaturaPDF
    Public Class VndIsEditorVendas
        Inherits EditorVendas

        Public Overrides Sub AntesDeImprimir(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeImprimir(Cancel, e)

            If Module1.VerificaToken("CertificadosFaturaPDF") = 1 Then
                '########################################################################################################################################
                '#######                                    BRUNO PEIXOTO 08/10/2020                                                            #########
                '####### No caso do utilizador ='ANA' carregar no imprimir e o doc. for financeiro, guarda apenas o PDF para pasta na partilha  #########
                '########################################################################################################################################
                If UCase(Aplicacao.Utilizador.Utilizador) = "ANA" Then
                    If (BSO.Vendas.TabVendas.Edita(Me.DocumentoVenda.Tipodoc).TipoDocumento = 4 Or Me.DocumentoVenda.Tipodoc = "GR") Then

                        ImprimePDF()
                        Cancel = True

                    End If
                End If
                '########################################################################################################################################
                '#######                                    BRUNO PEIXOTO 08/10/2020                                                            #########
                '####### No caso do utilizador ='ANA' carregar no imprimir e o doc. for financeiro, guarda apenas o PDF para pasta na partilha  #########
                '########################################################################################################################################

            End If

        End Sub


        Public Sub ImprimePDF()
            Dim CaminhoFicheiro As String
            Dim NomeFicheiro As String
            Dim mapa As String

            mapa = BSO.Base.Series.DaValorAtributo("V", Me.DocumentoVenda.Tipodoc, Me.DocumentoVenda.Serie, "Config")

            CaminhoFicheiro = "\\srvdc\Partilha\Geral\Ana Castro\Docs\"
            'Verifica se a pasta existe.
            If Directory.Exists(CaminhoFicheiro) = False Then
                'caso não exista, cria a pasta
                Directory.CreateDirectory(CaminhoFicheiro)
            End If

            NomeFicheiro = Me.DocumentoVenda.Tipodoc & "_" & Me.DocumentoVenda.Serie & "_" & Format(Me.DocumentoVenda.NumDoc, "00000") & ".pdf"

            If File.Exists(CaminhoFicheiro & "\" & NomeFicheiro) = True Then
                'caso o ficheiro já exista, elimina o mesmo para poder criar um novo.
                File.Delete(CaminhoFicheiro & "\" & NomeFicheiro)
            End If

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
            PSO.Mapas.ImprimeListagem(mapa, Me.DocumentoVenda.NumDoc & "/" & Me.DocumentoVenda.Serie, "P", 1, , , , , False)
            Exit Sub

Erro:
            MsgBox("Erro ao imprimir o mapa seleccionado." & vbCrLf & Err.Number & " - " & Err.Description, vbExclamation)



        End Sub

    End Class
End Namespace
