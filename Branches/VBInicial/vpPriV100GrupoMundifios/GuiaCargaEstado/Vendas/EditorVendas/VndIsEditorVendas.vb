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

Namespace GuiaCargaEstado
    Public Class VndNsVendas
        Inherits EditorVendas

        Public Overrides Sub DepoisDeGravar(Filial As String, Tipo As String, Serie As String, NumDoc As Integer, e As ExtensibilityEventArgs)
            MyBase.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e)

            '*******************************************************************************************************************************************
            '#### Ao gravar GR coloca o estado da Guia de Carga como Expedida 01/10/2018 (JFC)                                                      ####
            '*******************************************************************************************************************************************
            Dim ln2 As Long

            If Module1.VerificaToken("GuiaCargaEstado") = 1 Then

                If (Me.DocumentoVenda.Tipodoc = "GR") Then


                    For ln2 = 1 To Me.DocumentoVenda.Linhas.NumItens

                        If Me.DocumentoVenda.Linhas.GetEdita(ln2).Artigo & "" <> "" Then

                            BSO.DSO.ExecuteSQL("UPDATE ln SET ln.CDU_EstadoGC = '04' from linhasdoc ln inner join cabecdoc cd on cd.id=ln.idcabecdoc WHERE cd.tipodoc='GC' and cd.entidade = '" & Me.DocumentoVenda.Entidade & "' and ln.artigo = '" & Me.DocumentoVenda.Linhas.GetEdita(ln2).Artigo & "' and ln.lote = '" & Me.DocumentoVenda.Linhas.GetEdita(ln2).Lote & "'")

                        End If
                    Next ln2

                End If

                '*******************************************************************************************************************************************
                '#### Ao gravar GR coloca o estado da Guia de Carga como Expedida 01/10/2018 (JFC)                                                      ####
                '*******************************************************************************************************************************************
            End If

        End Sub


    End Class
End Namespace