using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using System;

namespace VerificaTesteQuimico
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
        {
            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

            if (Module1.VerificaToken("VerificaTesteQuimico") == 1)
            {
                // *******************************************************************************************************************************************
                // #### Verificar se TesteQuimico foi realizado - Pedido de Vitor Passos 26/07/2018 (JFC) ####
                // *******************************************************************************************************************************************

                StdBELista lista2;
                bool malha;

                if ((this.DocumentoVenda.Tipodoc == "GR"))
                {
                    malha = false;
                    for (var ln2 = 1; ln2 <= this.DocumentoVenda.Linhas.NumItens; ln2++)
                    {
                        lista2 = BSO.Consulta("select * from TDU_LaboratorioLote lb where lb.CDU_RSSitFinFio='MALHA' and lb.CDU_CodArtigo='" + this.DocumentoVenda.Linhas.GetEdita(ln2).Artigo + "' and lb.CDU_LoteArt='" + this.DocumentoVenda.Linhas.GetEdita(ln2).Lote + "'");

                        if ((lista2.Vazia() == false))
                            malha = true;
                    }

                    if (malha == true)
                        EnvioEmailLab();
                }
            }
        }

        // Variáveis para e-mail
        private string VarFrom;

        private string VarTo;
        private string VarAssunto;
        private string VarTextoInicialMsg;
        private string VarMensagem;
        private string VarLinhas;
        private string VarUtilizador;
        private string VarCliente;

        private void EnvioEmailLab()
        {
            // *******************************************************************************************************************************************
            // #### Enviar Mail para Vitor Passos - Pedido de Vitor Passos 26/07/2018 (JFC) ####
            // *******************************************************************************************************************************************
            VarCliente = this.DocumentoVenda.Entidade;
            int ln;
            VarFrom = "";

            VarTo = "informatica@mundifios.pt; vitorpassos@mundifios.pt";

            if (DateTime.Now.TimeOfDay >= new TimeSpan(7, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(12, 59, 0))
                VarTextoInicialMsg = "Bom dia,";
            else if (DateTime.Now.TimeOfDay >= new TimeSpan(13, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(19, 59, 0))
                VarTextoInicialMsg = "Boa tarde,";
            else
                VarTextoInicialMsg = "Boa noite,";

            VarAssunto = "(Malha) Guia de Remessa: " + Strings.Format(this.DocumentoVenda.NumDoc, "####") + "/" + this.DocumentoVenda.Serie + " (" + Strings.Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") + ")";

            VarUtilizador = Aplicacao.Utilizador.Utilizador;

            VarLinhas = "";
            for (ln = 1; ln <= this.DocumentoVenda.Linhas.NumItens; ln++)

                VarLinhas = VarLinhas + "Linha " + ln + ":                         " + this.DocumentoVenda.Linhas.GetEdita(ln).Artigo + " - Armazem:" + this.DocumentoVenda.Linhas.GetEdita(ln).Armazem + " - Lote:" + this.DocumentoVenda.Linhas.GetEdita(ln).Lote + " - Desc:" + this.DocumentoVenda.Linhas.GetEdita(ln).Descricao + " - Quantidade:" + this.DocumentoVenda.Linhas.GetEdita(ln).Quantidade + this.DocumentoVenda.Linhas.GetEdita(ln).Unidade + Strings.Chr(13) + "";

            VarMensagem = VarTextoInicialMsg + Strings.Chr(13) + Strings.Chr(13) + Strings.Chr(13) + "Foi emitido uma Guia de Remessa no Primavera, pedir malha ao cliente:" + Strings.Chr(13) + Strings.Chr(13) + ""
                    + "Empresa:                         " + BSO.Contexto.CodEmp + " - " + BSO.Contexto.IDNome + Strings.Chr(13) + ""
                    + "Utilizador:                      " + VarUtilizador + Strings.Chr(13) + Strings.Chr(13) + ""
                    + "Cliente:                         " + VarCliente + " - " + Strings.Replace(BSO.Base.Clientes.Edita(VarCliente).Nome, "'", "") + Strings.Chr(13) + ""
                    + "Documento:                       " + this.DocumentoVenda.Tipodoc + " " + Strings.Format(this.DocumentoVenda.NumDoc, "#,###") + "/" + this.DocumentoVenda.Serie + Strings.Chr(13) + Strings.Chr(13) + ""
                    + "Local Descarga:                  " + this.DocumentoVenda.LocalDescarga + Strings.Chr(13) + ""
                    + "Morada Entrega:                  " + Strings.Replace(this.DocumentoVenda.MoradaEntrega, "'", "") + Strings.Chr(13) + Strings.Chr(13) + ""
                    + VarLinhas + Strings.Chr(13) + ""
                    + "Cumprimentos";

            BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL] ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" + Strings.Format(DateTime.Now, "yyyy-MM-dd HH:mm:ss") + "', '" + VarFrom + "', '" + VarTo + "','','','" + VarAssunto + "','" + VarMensagem + "','',0,'" + VarUtilizador + "' )");
        }
    }
}