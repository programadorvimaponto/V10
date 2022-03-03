using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using System.Windows.Forms;

namespace PrintPackingList
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void AntesDeImprimir(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeImprimir(ref Cancel, e);

            if (Module1.VerificaToken("PrintPackingList") == 1)
            {
                if (this.DocumentoVenda.Tipodoc == "GC")
                {
                    if (Strings.Right(this.DocumentoVenda.Serie, 1) == "E")
                        ImprimePackingList();
                    else
                        ImprimeGC();

                    Cancel = true;
                }
            }
        }

        public void ImprimePackingList()
        {
            try
            {
                    PSO.Mapas.Inicializar("VND");

                    string strFormula;
                    strFormula = "";
                    // - Fórmula (DadosEmpresa)
                    strFormula = "StringVar Nome; StringVar Morada;StringVar Localidade; StringVar CodPostal; StringVar Telefone; StringVar Fax; StringVar Contribuinte; StringVar CapitalSocial; StringVar Conservatoria; StringVar Matricula;StringVar MoedaCapitalSocial;"; // PriGlobal: IGNORE
                    strFormula = strFormula + "Nome:=" + "'" + Aplicacao.Empresa.IDNome + "'"; // PriGlobal: IGNORE
                    strFormula = strFormula + ";Morada:=" + "'" + Aplicacao.Empresa.IDMorada + "'"; // PriGlobal: IGNORE
                    strFormula = strFormula + ";Localidade:=" + "'" + Aplicacao.Empresa.IDLocalidade + "'"; // PriGlobal: IGNORE
                    strFormula = strFormula + ";CodPostal:=" + "'" + Aplicacao.Empresa.IDCodPostal + " " + Aplicacao.Empresa.IDCodPostalLocal + "'"; // PriGlobal: IGNORE
                    strFormula = strFormula + ";Telefone:=" + "'" + Strings.Trim(Aplicacao.Empresa.IDIndicativoTelefone + " " + Aplicacao.Empresa.IDTelefone) + "'"; // PriGlobal: IGNORE
                    strFormula = strFormula + ";Fax:=" + "'" + Strings.Trim(Aplicacao.Empresa.IDIndicativoFax + " " + Aplicacao.Empresa.IDFax) + "'"; // PriGlobal: IGNORE
                    strFormula = strFormula + ";Contribuinte:=" + "'" + Aplicacao.Empresa.IFNIF + "'"; // PriGlobal: IGNORE
                    strFormula = strFormula + ";CapitalSocial:=" + "'" + Aplicacao.Empresa.ICCapitalSocial + "'"; // PriGlobal: IGNORE
                    strFormula = strFormula + ";Conservatoria:=" + "'" + Aplicacao.Empresa.ICConservatoria + "'"; // PriGlobal: IGNORE
                    strFormula = strFormula + ";Matricula:=" + "'" + Aplicacao.Empresa.ICMatricula + "'"; // PriGlobal: IGNORE
                    strFormula = strFormula + ";MoedaCapitalSocial:=" + "'" + Aplicacao.Empresa.ICMoedaCapSocial + "'"; // PriGlobal: IGNORE
                    PSO.Mapas.AddFormula("DadosEmpresa", strFormula);

                    string SelFormula;

         
                SelFormula = "{CabecDoc.Filial}='" + this.DocumentoVenda.Filial + "' And {CabecDoc.Serie}='" + this.DocumentoVenda.Serie + "' And {CabecDoc.TipoDoc}='" + this.DocumentoVenda.Tipodoc + "' and {CabecDoc.NumDoc}= " + this.DocumentoVenda.NumDoc;
            
                PSO.Mapas.ImprimeListagem("GCJFC", "GC " + this.DocumentoVenda.NumDoc + "/" + this.DocumentoVenda.Serie,  "W",  1, sSelFormula: SelFormula, bMapaSistema: false, strUniqueIdentifier: this.DocumentoVenda.ID);
            }
            catch
            {
                MessageBox.Show("Erro ao imprimir o mapa seleccionado.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
         
        public void ImprimeGC()
        {
            try
            {
                {
                    PSO.Mapas.Inicializar("VND");
                    string strFormula;
                    strFormula = "";
                    // - Fórmula (DadosEmpresa)
                    strFormula = "StringVar Nome; StringVar Morada;StringVar Localidade; StringVar CodPostal; StringVar Telefone; StringVar Fax; StringVar Contribuinte; StringVar CapitalSocial; StringVar Conservatoria; StringVar Matricula;StringVar MoedaCapitalSocial;"; // PriGlobal: IGNORE
                    strFormula = strFormula + "Nome:=" + "'" + Aplicacao.Empresa.IDNome + "'"; // PriGlobal: IGNORE
                    strFormula = strFormula + ";Morada:=" + "'" + Aplicacao.Empresa.IDMorada + "'"; // PriGlobal: IGNORE
                    strFormula = strFormula + ";Localidade:=" + "'" + Aplicacao.Empresa.IDLocalidade + "'"; // PriGlobal: IGNORE
                    strFormula = strFormula + ";CodPostal:=" + "'" + Aplicacao.Empresa.IDCodPostal + " " + Aplicacao.Empresa.IDCodPostalLocal + "'"; // PriGlobal: IGNORE
                    strFormula = strFormula + ";Telefone:=" + "'" + Strings.Trim(Aplicacao.Empresa.IDIndicativoTelefone + " " + Aplicacao.Empresa.IDTelefone) + "'"; // PriGlobal: IGNORE
                    strFormula = strFormula + ";Fax:=" + "'" + Strings.Trim(Aplicacao.Empresa.IDIndicativoFax + " " + Aplicacao.Empresa.IDFax) + "'"; // PriGlobal: IGNORE
                    strFormula = strFormula + ";Contribuinte:=" + "'" + Aplicacao.Empresa.IFNIF + "'"; // PriGlobal: IGNORE
                    strFormula = strFormula + ";CapitalSocial:=" + "'" + Aplicacao.Empresa.ICCapitalSocial + "'"; // PriGlobal: IGNORE
                    strFormula = strFormula + ";Conservatoria:=" + "'" + Aplicacao.Empresa.ICConservatoria + "'"; // PriGlobal: IGNORE
                    strFormula = strFormula + ";Matricula:=" + "'" + Aplicacao.Empresa.ICMatricula + "'"; // PriGlobal: IGNORE
                    strFormula = strFormula + ";MoedaCapitalSocial:=" + "'" + Aplicacao.Empresa.ICMoedaCapSocial + "'"; // PriGlobal: IGNORE
                    PSO.Mapas.AddFormula("DadosEmpresa", strFormula);

                    string SelFormula;
                    SelFormula = "{CabecDoc.Filial}='" + this.DocumentoVenda.Filial + "' And {CabecDoc.Serie}='" + this.DocumentoVenda.Serie + "' And {CabecDoc.TipoDoc}='" + this.DocumentoVenda.Tipodoc + "' and {CabecDoc.NumDoc}= " + this.DocumentoVenda.NumDoc;

                    PSO.Mapas.ImprimeListagem(sReport: "GCARGA", sTitulo: "GC " + this.DocumentoVenda.NumDoc + "/" + this.DocumentoVenda.Serie, sDestino: "W", iNumCopias: 1, bMapaSistema: false, strUniqueIdentifier: this.DocumentoVenda.ID, sSelFormula: SelFormula);
                }
            }
            catch
            {
                MessageBox.Show("Erro ao imprimir o mapa seleccionado.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}