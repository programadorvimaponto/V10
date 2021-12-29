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
Imports Primavera.Extensibility.Attributes

Namespace PrintPackingList

    Public Class VndIsEditorVendas
        Inherits EditorVendas
        Public Overrides Sub AntesDeImprimir(ByRef Cancel As Boolean, e As ExtensibilityEventArgs)
            MyBase.AntesDeImprimir(Cancel, e)

            If Module1.VerificaToken("PrintPackingList") = 1 Then

                If Me.DocumentoVenda.Tipodoc = "GC" Then
                    If Right(Me.DocumentoVenda.Serie, 1) = "E" Then
                        ImprimePackingList()

                    Else
                        ImprimeGC()

                    End If

                    Cancel = True

                End If
            End If

        End Sub


        Public Sub ImprimePackingList()

            On Error GoTo Erro
            PSO.Mapas.Inicializar("VND")

            Dim strFormula As String
            strFormula = ""
            '- Fórmula (DadosEmpresa)
            strFormula = "StringVar Nome; StringVar Morada;StringVar Localidade; StringVar CodPostal; StringVar Telefone; StringVar Fax; StringVar Contribuinte; StringVar CapitalSocial; StringVar Conservatoria; StringVar Matricula;StringVar MoedaCapitalSocial;" 'PriGlobal: IGNORE
            strFormula = strFormula & "Nome:=" & "'" & Aplicacao.Empresa.IDNome & "'" 'PriGlobal: IGNORE
            strFormula = strFormula & ";Morada:=" & "'" & Aplicacao.Empresa.IDMorada & "'" 'PriGlobal: IGNORE
            strFormula = strFormula & ";Localidade:=" & "'" & Aplicacao.Empresa.IDLocalidade & "'" 'PriGlobal: IGNORE
            strFormula = strFormula & ";CodPostal:=" & "'" & Aplicacao.Empresa.IDCodPostal & " " & Aplicacao.Empresa.IDCodPostalLocal & "'" 'PriGlobal: IGNORE
            strFormula = strFormula & ";Telefone:=" & "'" & Trim$(Aplicacao.Empresa.IDIndicativoTelefone & " " & Aplicacao.Empresa.IDTelefone) & "'" 'PriGlobal: IGNORE
            strFormula = strFormula & ";Fax:=" & "'" & Trim$(Aplicacao.Empresa.IDIndicativoFax & " " & Aplicacao.Empresa.IDFax) & "'" 'PriGlobal: IGNORE
            strFormula = strFormula & ";Contribuinte:=" & "'" & Aplicacao.Empresa.IFNIF & "'" 'PriGlobal: IGNORE
            strFormula = strFormula & ";CapitalSocial:=" & "'" & Aplicacao.Empresa.ICCapitalSocial & "'" 'PriGlobal: IGNORE
            strFormula = strFormula & ";Conservatoria:=" & "'" & Aplicacao.Empresa.ICConservatoria & "'" 'PriGlobal: IGNORE
            strFormula = strFormula & ";Matricula:=" & "'" & Aplicacao.Empresa.ICMatricula & "'" 'PriGlobal: IGNORE
            strFormula = strFormula & ";MoedaCapitalSocial:=" & "'" & Aplicacao.Empresa.ICMoedaCapSocial & "'" 'PriGlobal: IGNORE
            PSO.Mapas.AddFormula("DadosEmpresa", strFormula)

            Dim SelFormula As String
            SelFormula = "{CabecDoc.Filial}='" & Me.DocumentoVenda.Filial & "' And {CabecDoc.Serie}='" & Me.DocumentoVenda.Serie & "' And {CabecDoc.TipoDoc}='" & Me.DocumentoVenda.Tipodoc & "' and {CabecDoc.NumDoc}= " & Me.DocumentoVenda.NumDoc

            PSO.Mapas.ImprimeListagem(sReport:="GCJFC", sTitulo:="GC " & Me.DocumentoVenda.NumDoc & "/" & Me.DocumentoVenda.Serie, sDestino:="W", iNumCopias:=1, bMapaSistema:=True, strUniqueIdentifier:=Me.DocumentoVenda.ID, sSelFormula:=SelFormula)

            Exit Sub

Erro:
            MsgBox("Erro ao imprimir o mapa seleccionado." & vbCrLf & Err.Number & " - " & Err.Description, vbExclamation)
        End Sub

        Public Sub ImprimeGC()
            On Error GoTo Erro
            PSO.Mapas.Inicializar("VND")
            Dim strFormula As String
            strFormula = ""
            '- Fórmula (DadosEmpresa)
            strFormula = "StringVar Nome; StringVar Morada;StringVar Localidade; StringVar CodPostal; StringVar Telefone; StringVar Fax; StringVar Contribuinte; StringVar CapitalSocial; StringVar Conservatoria; StringVar Matricula;StringVar MoedaCapitalSocial;" 'PriGlobal: IGNORE
            strFormula = strFormula & "Nome:=" & "'" & Aplicacao.Empresa.IDNome & "'" 'PriGlobal: IGNORE
            strFormula = strFormula & ";Morada:=" & "'" & Aplicacao.Empresa.IDMorada & "'" 'PriGlobal: IGNORE
            strFormula = strFormula & ";Localidade:=" & "'" & Aplicacao.Empresa.IDLocalidade & "'" 'PriGlobal: IGNORE
            strFormula = strFormula & ";CodPostal:=" & "'" & Aplicacao.Empresa.IDCodPostal & " " & Aplicacao.Empresa.IDCodPostalLocal & "'" 'PriGlobal: IGNORE
            strFormula = strFormula & ";Telefone:=" & "'" & Trim$(Aplicacao.Empresa.IDIndicativoTelefone & " " & Aplicacao.Empresa.IDTelefone) & "'" 'PriGlobal: IGNORE
            strFormula = strFormula & ";Fax:=" & "'" & Trim$(Aplicacao.Empresa.IDIndicativoFax & " " & Aplicacao.Empresa.IDFax) & "'" 'PriGlobal: IGNORE
            strFormula = strFormula & ";Contribuinte:=" & "'" & Aplicacao.Empresa.IFNIF & "'" 'PriGlobal: IGNORE
            strFormula = strFormula & ";CapitalSocial:=" & "'" & Aplicacao.Empresa.ICCapitalSocial & "'" 'PriGlobal: IGNORE
            strFormula = strFormula & ";Conservatoria:=" & "'" & Aplicacao.Empresa.ICConservatoria & "'" 'PriGlobal: IGNORE
            strFormula = strFormula & ";Matricula:=" & "'" & Aplicacao.Empresa.ICMatricula & "'" 'PriGlobal: IGNORE
            strFormula = strFormula & ";MoedaCapitalSocial:=" & "'" & Aplicacao.Empresa.ICMoedaCapSocial & "'" 'PriGlobal: IGNORE
            PSO.Mapas.AddFormula("DadosEmpresa", strFormula)

            Dim SelFormula As String
            SelFormula = "{CabecDoc.Filial}='" & Me.DocumentoVenda.Filial & "' And {CabecDoc.Serie}='" & Me.DocumentoVenda.Serie & "' And {CabecDoc.TipoDoc}='" & Me.DocumentoVenda.Tipodoc & "' and {CabecDoc.NumDoc}= " & Me.DocumentoVenda.NumDoc

            PSO.Mapas.ImprimeListagem(sReport:="GCARGA", sTitulo:="GC " & Me.DocumentoVenda.NumDoc & "/" & Me.DocumentoVenda.Serie, sDestino:="W", iNumCopias:=1, bMapaSistema:=True, strUniqueIdentifier:=Me.DocumentoVenda.ID, sSelFormula:=SelFormula)


            Exit Sub
Erro:
            MsgBox("Erro ao imprimir o mapa seleccionado." & vbCrLf & Err.Number & " - " & Err.Description, vbExclamation)
        End Sub

    End Class
End Namespace