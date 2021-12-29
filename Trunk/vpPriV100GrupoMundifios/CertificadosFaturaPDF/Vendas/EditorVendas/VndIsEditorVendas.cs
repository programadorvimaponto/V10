using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using System.IO;
using System.Windows.Forms;
using static StdPlatBS100.StdBSTipos;

namespace CertificadosFaturaPDF
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void AntesDeImprimir(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeImprimir(ref Cancel, e);

            if (Module1.VerificaToken("CertificadosFaturaPDF") == 1)
            {
                // ########################################################################################################################################
                // #######                                    BRUNO PEIXOTO 08/10/2020                                                            #########
                // ####### No caso do utilizador ='ANA' carregar no imprimir e o doc. for financeiro, guarda apenas o PDF para pasta na partilha  #########
                // ########################################################################################################################################
                if (Strings.UCase(Aplicacao.Utilizador.Utilizador) == "ANA")
                {
                    if ((BSO.Vendas.TabVendas.Edita(this.DocumentoVenda.Tipodoc).TipoDocumento == 4 | this.DocumentoVenda.Tipodoc == "GR"))
                    {
                        ImprimePDF();
                        Cancel = true;
                    }
                }
            }
        }

        public void ImprimePDF()
        {
            string CaminhoFicheiro;
            string NomeFicheiro;
            string mapa;

            mapa = BSO.Base.Series.DaValorAtributo("V", DocumentoVenda.Tipodoc, DocumentoVenda.Serie, "Config");

            CaminhoFicheiro = @"\\srvdc\Partilha\Geral\Ana Castro\Docs\";

            if (Directory.Exists(CaminhoFicheiro) == false)
            {
                Directory.CreateDirectory(CaminhoFicheiro);
            }

            NomeFicheiro = this.DocumentoVenda.Tipodoc + "_" + this.DocumentoVenda.Serie + "_" + Strings.Format(this.DocumentoVenda.NumDoc, "00000") + ".pdf";

            if (File.Exists(CaminhoFicheiro + @"\" + NomeFicheiro) == true)
                File.Delete(CaminhoFicheiro + @"\" + NomeFicheiro);

            try
            {
                PSO.Mapas.Inicializar("VND");
                PSO.Mapas.Destino = CRPEExportDestino.edFicheiro;
                PSO.Mapas.SetFileProp(CRPEExportFormat.efPdf, CaminhoFicheiro + NomeFicheiro);

                PSO.Mapas.AddFormula("Nome", "'" + BSO.Contexto.IDNome + "'");
                PSO.Mapas.AddFormula("Contribuinte", "'" + "Contribuinte N.º: " + BSO.Contexto.IFNIF + "'");

                if (BSO.Contexto.IDNumPorta + "" != "")
                    PSO.Mapas.AddFormula("Morada", "'" + BSO.Contexto.IDMorada + ", " + BSO.Contexto.IDNumPorta + "'");
                else
                    PSO.Mapas.AddFormula("Morada", "'" + BSO.Contexto.IDMorada + "'");

                PSO.Mapas.AddFormula("Localidade", "'" + BSO.Contexto.IDLocalidade + "'");
                PSO.Mapas.AddFormula("CodPostal", "'" + BSO.Contexto.IDCodPostal + " " + BSO.Contexto.IDCodPostalLocal + "'");
                PSO.Mapas.AddFormula("Telefone", "'" + "Telef. " + BSO.Contexto.IDIndicativoTelefone + "  " + BSO.Contexto.IDTelefone + "  Fax. " + BSO.Contexto.IDIndicativoFax + "  " + BSO.Contexto.IDFax + "'");

                PSO.Mapas.AddFormula("CapitalSocial", "'" + "Capital Social  " + Strings.Format(BSO.Contexto.ICCapitalSocial, "#,###.00") + " " + BSO.Contexto.ICMoedaCapSocial + "'");
                PSO.Mapas.AddFormula("Conservatoria", "'" + "Cons. Reg. Com. " + BSO.Contexto.ICConservatoria + "'");
                PSO.Mapas.AddFormula("Matricula", "'" + "Matricula N.º " + BSO.Contexto.ICMatricula + "'");
                PSO.Mapas.AddFormula("EMailEmpresa", "'" + BSO.Contexto.IDEmail + "'");
                PSO.Mapas.AddFormula("WebEmpresa", "'" + BSO.Contexto.IDWeb + "'");

                PSO.Mapas.AddFormula("NumVia", "'" + BSO.Base.Series.Edita("V", this.DocumentoVenda.Tipodoc, this.DocumentoVenda.Serie).DescricaoVia01 + "'");

                PSO.Mapas.AddFormula("lbl_Text23", "'" + BSO.Vendas.Documentos.DevolveTextoAssinaturaDoc(this.DocumentoVenda.Tipodoc, this.DocumentoVenda.Serie, this.DocumentoVenda.NumDoc, "000") + "'");

                PSO.Mapas.AddFormula("NomeLicenca", "''");

                PSO.Mapas.AddFormula("Documento", "'" + BSO.Vendas.TabVendas.DaValorAtributo(this.DocumentoVenda.Tipodoc, "Descricao") + " " + this.DocumentoVenda.Tipodoc + " " + this.DocumentoVenda.Serie + "/" + Strings.Format(this.DocumentoVenda.NumDoc, "0") + "'");
                PSO.Mapas.SelectionFormula = "{CabecDoc.Filial} = '000' AND {CabecDoc.TipoDoc} = '" + this.DocumentoVenda.Tipodoc + "' AND {CabecDoc.Serie} = '" + this.DocumentoVenda.Serie + "' AND {CabecDoc.NumDoc} = " + this.DocumentoVenda.NumDoc + "";

                PSO.Mapas.ImprimeListagem(mapa, DocumentoVenda.NumDoc + "/" + DocumentoVenda.Serie, "P", 1, bCategoria: false);
            }
            catch
            {
                MessageBox.Show("Erro ao imprimir o mapa seleccionado.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}