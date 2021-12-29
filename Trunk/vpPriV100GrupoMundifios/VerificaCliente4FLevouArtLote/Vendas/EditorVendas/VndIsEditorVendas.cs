using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using System;

namespace VerificaCliente4FLevouArtLote
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            if (Module1.VerificaToken("VerificaCliente4FLevouArtLote") == 1)
            {
                if (this.DocumentoVenda.Tipodoc == "GR" & this.DocumentoVenda.Entidade == "0958")
                    VerificaCliente4FLevouArtLote();
            }
        }

        private StdBELista ListaCliLevouArtLote;
        private string SqlStringCliLevouArtLote;

        // Variáveis para e-mail
        private string VarFrom;

        private string VarTo;
        private string VarAssunto;
        private string VarTextoInicialMsg;
        private string VarMensagem;
        private string VarArmazem;
        private string VarLinhas;
        private string VarUtilizador;
        private int VarLocalTeste; // 0 - ao seleccionar o cliente; 1 - antes de gravar o documento(ECL ou GR)
        private bool VarCancelaDoc;
        private bool VarNetTrans;

        // ###JFC pedido de Carina. Verificar se cliente 4F já levou o lote. Envia email a lembrar necessidade de enviar Caracteristicas Tecnicas.
        private void VerificaCliente4FLevouArtLote()
        {
            for (var i = 1; i <= this.DocumentoVenda.Linhas.NumItens; i++)
            {
                if (this.DocumentoVenda.Linhas.GetEdita(i).Lote + "" != "" & this.DocumentoVenda.Linhas.GetEdita(i).Lote + "" != "<L01>")
                {
                    SqlStringCliLevouArtLote = "SELECT dbo.CabecDoc.Entidade, dbo.LinhasDoc.Artigo, dbo.LinhasDoc.Lote "
                                            + "FROM dbo.CabecDoc INNER JOIN dbo.LinhasDoc ON dbo.CabecDoc.Id = dbo.LinhasDoc.IdCabecDoc "
                                            + "WHERE (dbo.CabecDoc.Tipodoc in ('FI', 'FA', 'FO', 'FIT')) and (dbo.LinhasDoc.Artigo = '" + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + "') AND (dbo.LinhasDoc.Lote = '" + this.DocumentoVenda.Linhas.GetEdita(i).Lote + "') AND (dbo.CabecDoc.Entidade = '" + this.DocumentoVenda.Entidade + "')";

                    ListaCliLevouArtLote = BSO.Consulta(SqlStringCliLevouArtLote);

                    if (ListaCliLevouArtLote.Vazia() == true)
                    {
                        VarFrom = "";
                        VarTo = "marketing@mundifios.pt;";

                        if (DateTime.Now.TimeOfDay >= new TimeSpan(7, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(12, 59, 0))
                            VarTextoInicialMsg = "Bom dia,";
                        else if (DateTime.Now.TimeOfDay >= new TimeSpan(13, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(19, 59, 0))
                            VarTextoInicialMsg = "Boa tarde,";
                        else
                            VarTextoInicialMsg = "Boa noite,";

                        VarAssunto = "Novo lote: (" + this.DocumentoVenda.Entidade + ") - " + Strings.Replace(BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).Nome, "'", "");

                        VarUtilizador = Aplicacao.Utilizador.Utilizador;

                        VarMensagem = VarTextoInicialMsg + Strings.Chr(13) + Strings.Chr(13) + Strings.Chr(13) + "Foi emitido uma Guia com um lote novo para o cliente, pfv enviar caracteristicas tecnicas:" + Strings.Chr(13) + Strings.Chr(13) + ""
                                    + "Empresa:                         " + BSO.Contexto.CodEmp + " - " + BSO.Contexto.IDNome + Strings.Chr(13) + ""
                                    + "Utilizador:                      " + VarUtilizador + Strings.Chr(13) + Strings.Chr(13) + ""
                                    + "Cliente:                         " + this.DocumentoVenda.Entidade + " - " + Strings.Replace(BSO.Base.Clientes.Edita(this.DocumentoVenda.Entidade).Nome, "'", "") + Strings.Chr(13) + ""
                                    + "Documento:                       " + this.DocumentoVenda.Tipodoc + " " + Strings.Format(this.DocumentoVenda.NumDoc, "#,###") + "/" + this.DocumentoVenda.Serie + Strings.Chr(13) + ""
                                    + "Artigo:                           " + this.DocumentoVenda.Linhas.GetEdita(i).Artigo + Strings.Chr(13) + ""
                                    + "Desc:                             " + this.DocumentoVenda.Linhas.GetEdita(i).Descricao + Strings.Chr(13) + ""
                                    + "Lote:                             " + this.DocumentoVenda.Linhas.GetEdita(i).Lote + Strings.Chr(13) + ""
                                    + "Cumprimentos";

                        BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL]  ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" + Strings.Format(DateTime.Now, "yyyy-MM-dd HH:mm:ss") + "', '" + VarFrom + "', '" + VarTo + "','','','" + VarAssunto + "','" + VarMensagem + "','',0,'" + VarUtilizador + "' )");
                    }
                }
            }
        }
    }
}