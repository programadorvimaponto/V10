//using Primavera.Extensibility.Purchases.Editors;
//using Primavera.Extensibility.BusinessEntities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
//using StdBE100;
//using Microsoft.VisualBasic;
//using static BasBE100.BasBETiposGcp;

//namespace Inovafil
//{
//    public class CmpIsEditorCompras : EditorCompras
//    {

//        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
//        {
//            base.AntesDeGravar(ref Cancel, e);

//            // ##############################################################################################################
//            // #Envia e-mail para Joaquim António com Encomendas a Fornecedor que contenham o artigo Rama. JFC - 31/01/2019 #
//            // ##############################################################################################################
//            if (this.DocumentoCompra.Tipodoc == "ECF")
//            {
//                bool EnviaEmailRamaBolean;
//                bool EnviaEmailCertificadoBolean;
//                EnviaEmailRamaBolean = false;
//                EnviaEmailCertificadoBolean = false;
//                StdBELista listValidaCabec;

//                listValidaCabec = BSO.Consulta("select * from CabecCompras cc where cc.Id='" + this.DocumentoCompra.ID + "'");


//                if(listValidaCabec.Vazia()==true)
//                {

//                    for(int i=1;i<=DocumentoCompra.Linhas.NumItens;i++)
//                    {

//                        if (BSO.Base.Artigos.DaValorAtributo(DocumentoCompra.Linhas.GetEdita(i).Artigo, "Descricao").contains("Rama"))
//                            EnviaEmailRamaBolean = true;

//                        if(DocumentoCompra.Linhas.GetEdita(i).CamposUtil["CDU_Artigo"].Valor + "" == "" && DocumentoCompra.Linhas.GetEdita(i).Artigo + "" != "")
//                        {

//                            if (Strings.UCase(DocumentoCompra.Linhas.GetEdita(i).Descricao).Contains("RECYCLED") || Strings.UCase(DocumentoCompra.Linhas.GetEdita(i).Descricao).Contains("RECICLADO") || Strings.UCase(DocumentoCompra.Linhas.GetEdita(i).Descricao).Contains("REPREVE") || Strings.UCase(DocumentoCompra.Linhas.GetEdita(i).Descricao).Contains("GRS") || Strings.UCase(DocumentoCompra.Linhas.GetEdita(i).Descricao).Contains("GOTS") || Strings.UCase(DocumentoCompra.Linhas.GetEdita(i).Descricao).Contains("OCS") || Strings.UCase(DocumentoCompra.Linhas.GetEdita(i).Descricao).Contains("BCI") || Strings.UCase(DocumentoCompra.Linhas.GetEdita(i).Descricao).Contains("SUPIMA"))
//                                EnviaEmailCertificadoBolean = true;


//                        }

//                    }
//                }

//                if (EnviaEmailRamaBolean == true)
//                    EnvioEmailRama();


//            }

//        }


//        public override void ArtigoIdentificado(string Artigo, int NumLinha, ref bool Cancel, ExtensibilityEventArgs e)
//        {
//            base.ArtigoIdentificado(Artigo, NumLinha, ref Cancel, e);

//            if(DocumentoCompra.Tipodoc=="ECF")
//            {

//                if(DocumentoCompra.Linhas.GetEdita(NumLinha).Descricao.Contains("Lã"))
//                {

//                    if (DocumentoCompra.Pais == "PT")
//                        BSO.Compras.Documentos.AdicionaLinhaEspecial(DocumentoCompra, compTipoLinhaEspecial.compLinha_Comentario, 0, "Artigo composto por Lã. Por favor envie o Relatório de Qualidade e a Declaração origem e de Non Mulesing.");
//                    else
//                        BSO.Compras.Documentos.AdicionaLinhaEspecial(DocumentoCompra, compTipoLinhaEspecial.compLinha_Comentario, 0, "This Product is composed by WO. Please send Quality Report and Declaration of Origin and Not Mulesing.");

//                }

//                if (DocumentoCompra.Linhas.GetEdita(NumLinha).Descricao.Contains("Cashemira"))
//                {

//                    if (DocumentoCompra.Pais == "PT")
//                        BSO.Compras.Documentos.AdicionaLinhaEspecial(DocumentoCompra, compTipoLinhaEspecial.compLinha_Comentario, 0, "Artigo composto por Cashemira. Por favor envie o Relatório de Qualidade e Certificado Laboratorial de composição.");
//                    else
//                        BSO.Compras.Documentos.AdicionaLinhaEspecial(DocumentoCompra, compTipoLinhaEspecial.compLinha_Comentario, 0, "This Product is composed by Cashemira. Please send Quality Report and Laboratory Certificate of composition.");

//                }

//            }

//        }


//        public override void DepoisDeGravar(string Filial, string Tipo, string Serie, int NumDoc, ExtensibilityEventArgs e)
//        {
//            base.DepoisDeGravar(Filial, Tipo, Serie, NumDoc, e);

//            bool AtualizaSS = false;

//            for(int i=1;i<=DocumentoCompra.Linhas.NumItens;i++)
//            {

//                if (DocumentoCompra.Linhas.GetEdita(i).Armazem == "INO")
//                    AtualizaSS = true;

//            }

//            if (AtualizaSS == true)
//                BSO.DSO.ExecuteSQL("exec priinovafil.dbo.spAtualizaSS");

//        }


//        string VarFrom;
//        string VarTo;
//        string VarAssunto;
//        string VarTextoInicialMsg;
//        string VarMensagem;
//        string VarUtilizador;
//        string VarLinhas;

//        private void EnvioEmailRama()
//        {
//            int ln;
//            VarFrom = "";

//            VarTo = "informatica@mundifios.pt;jafernandes@mundifios.pt";

//            if (DateTime.Now.TimeOfDay >= new TimeSpan(7, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(12, 59, 0))
//                VarTextoInicialMsg = "Bom dia,";
//            else if (DateTime.Now.TimeOfDay >= new TimeSpan(13, 0, 0) && DateTime.Now.TimeOfDay <= new TimeSpan(19, 59, 0))
//                VarTextoInicialMsg = "Boa tarde,";
//            else
//                VarTextoInicialMsg = "Boa noite,";

//            VarAssunto = "(Rama Inovafil) Encomenda a Fornecedor: " + Strings.Format(this.DocumentoCompra.NumDoc, "####") + "/" + this.DocumentoCompra.Serie;

//            VarUtilizador = Aplicacao.Utilizador.Utilizador;

//            VarLinhas = "";

//            for (ln = 1; ln <= this.DocumentoCompra.Linhas.NumItens; ln++)

//                VarLinhas = VarLinhas + "Linha " + ln + ":                         " + this.DocumentoCompra.Linhas.GetEdita(ln).Artigo + " - Armazem:" + this.DocumentoCompra.Linhas.GetEdita(ln).Armazem + " - Lote:" + this.DocumentoCompra.Linhas.GetEdita(ln).Lote + " - Desc:" + this.DocumentoCompra.Linhas.GetEdita(ln).Descricao + " - Quantidade:" + this.DocumentoCompra.Linhas.GetEdita(ln).Quantidade + this.DocumentoCompra.Linhas.GetEdita(ln).Unidade + " - Prec.Unit:" + this.DocumentoCompra.Linhas.GetEdita(ln).PrecUnit + this.DocumentoCompra.Moeda + " - Data Entrega:" + this.DocumentoCompra.Linhas.GetEdita(ln).DataEntrega + Strings.Chr(13) + "";


//            VarMensagem = VarTextoInicialMsg + Strings.Chr(13) + Strings.Chr(13) + Strings.Chr(13) + "Foi emitido uma Encomenda a Fornecedor no Primavera:" + Strings.Chr(13) + Strings.Chr(13) + ""
//                        + "Empresa:                         " + BSO.Contexto.CodEmp + " - " + BSO.Contexto.IDNome + Strings.Chr(13) + ""
//                        + "Utilizador:                      " + VarUtilizador + Strings.Chr(13) + Strings.Chr(13) + ""
//                        + "Fornecedor:                      " + this.DocumentoCompra.Entidade + " - " + Strings.Replace(BSO.Base.Fornecedores.Edita(this.DocumentoCompra.Entidade).Nome, "'", "") + Strings.Chr(13) + ""
//                        + "Documento:                       " + this.DocumentoCompra.Tipodoc + " " + Strings.Format(this.DocumentoCompra.NumDoc, "#,###") + "/" + this.DocumentoCompra.Serie + Strings.Chr(13) + Strings.Chr(13) + Strings.Chr(13) + ""
//                        + VarLinhas + Strings.Chr(13) + ""
//                        + "Cumprimentos";




//            BSO.DSO.ExecuteSQL("INSERT INTO [PRIEMPRE].[DBO].[MENSAGENSEMAIL]  ([Data], [From], [To], [CC], [BCC], [Assunto], [Mensagem], [Anexos], [Formato], [Utilizador]) VALUES('" + Strings.Format(DateTime.Now, "yyyy-MM-dd HH:mm:ss") + "', '" + VarFrom + "', '" + VarTo + "','','','" + VarAssunto + "','" + VarMensagem + "','',0,'" + VarUtilizador + "' )");
//        }


//    }
//}
