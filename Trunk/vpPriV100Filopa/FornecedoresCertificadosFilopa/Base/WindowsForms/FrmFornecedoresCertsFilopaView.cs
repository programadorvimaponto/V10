using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.CustomForm;
using StdBE100;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FornecedoresCertificadosFilopa
{
    public partial class FrmFornecedoresCertsFilopaView : CustomForm
    {
       public FrmFornecedoresCertsFilopaView()
        {
            InitializeComponent();
        }

        private void barButtonItemGravar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (TextEditCodigoCliente.EditValue.ToString() != "")
                AlteraCertsClientes();
            else
                MessageBox.Show("O cliente não está identificado.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }


        private void CarregaDados()
        {
            if (this.TextEditCliente.EditValue == string.Empty)
            {
                this.TextEditCodigoCliente.EditValue = string.Empty;
                return;
            }

            StdBELista listCerts;
            string sql;
            sql = "SELECT cliente, Nome, CDU_Oekotex ,CDU_OekotexNum , CDU_OekotexData ,CDU_OekotexClasse ,  CDU_Oekotex2,    CDU_OekotexNum2, CDU_OekotexData2 ,    CDU_OekotexClasse2,  CDU_Gots ,   CDU_GotsData  ,  CDU_Ocs, CDU_OcsData ,CDU_Grs ,CDU_GrsData, CDU_Rcs, CDU_RcsData, CDU_Bci, CDU_BciData, CDU_Fsc, CDU_FscData, CDU_EgyptCotton, CDU_Supima, CDU_SupimaData, CDU_SupimaNum, CDU_EgyptCottonData, CDU_EuropeanFlax ,CDU_EuropeanFlaxData, CDU_Iso9001, CDU_Iso9001Data, CDU_Iso14001,    CDU_Iso14001Data ,   CDU_Sa8000,   CDU_Sa8000Data, CDU_Fairtrade, CDU_FairtradeData, CDU_FscNum, CDU_EuropeanflaxNum FROM " + "PRIFilopa" + ".dbo.Clientes WHERE Cliente= '" + this.TextEditCodigoCliente.EditValue+ "'";

            listCerts = BSO.Consulta(sql);
            listCerts.Inicio();
            // 
            // CheckBoxs
            this.CheckEditOekotex.EditValue = listCerts.Valor("CDU_Oekotex");
            this.CheckEditOekotex2.EditValue = listCerts.Valor("CDU_Oekotex2");
            this.CheckEditGots.EditValue = listCerts.Valor("CDU_Gots");
            this.CheckEditOcs.EditValue = listCerts.Valor("CDU_Ocs");
            this.CheckEditBci.EditValue = listCerts.Valor("CDU_BCI");
            this.CheckEditEgypt.EditValue = listCerts.Valor("CDU_EgyptCotton");
            this.CheckEditSupima.EditValue = listCerts.Valor("CDU_Supima");
            this.CheckEditGrs.EditValue = listCerts.Valor("CDU_Grs");
            this.CheckEditRcs.EditValue = listCerts.Valor("CDU_Rcs");
            this.CheckEditFsc.EditValue = listCerts.Valor("CDU_Fsc");
            this.CheckEditFlax.EditValue = listCerts.Valor("CDU_EuropeanFlax");
            this.CheckEdit9001.EditValue = listCerts.Valor("CDU_Iso9001");
            this.CheckEdit14001.EditValue = listCerts.Valor("CDU_Iso14001");
            this.CheckEditSa.EditValue = listCerts.Valor("CDU_Sa8000");
            this.CheckEditFairTrade.EditValue = listCerts.Valor("CDU_Fairtrade");

            // Datas
            this.DateEditOekotex.EditValue = listCerts.Valor("CDU_OekotexData");
            this.DateEditOekotex2.EditValue = listCerts.Valor("CDU_OekotexData2");
            this.DateEditGots.EditValue = listCerts.Valor("CDU_GotsData");
            this.DateEditOcs.EditValue = listCerts.Valor("CDU_OcsData");
            this.DateEditBci.EditValue = listCerts.Valor("CDU_BCIData");
            this.DateEditEgypt.EditValue = listCerts.Valor("CDU_EgyptCottonData");
            this.DateEditSupima.EditValue = listCerts.Valor("CDU_SupimaData");
            this.DateEditGrs.EditValue = listCerts.Valor("CDU_GrsData");
            this.DateEditRcs.EditValue = listCerts.Valor("CDU_RcsData");
            this.DateEditFsc.EditValue = listCerts.Valor("CDU_FscData");
            this.DateEditFlax.EditValue = listCerts.Valor("CDU_EuropeanFlaxData");
            this.DateEdit9001.EditValue = listCerts.Valor("CDU_Iso9001Data");
            this.DateEdit14001.EditValue = listCerts.Valor("CDU_Iso14001Data");
            this.DateEditSa.EditValue = listCerts.Valor("CDU_Sa8000Data");
            this.DateEditFairTrade.EditValue = listCerts.Valor("CDU_FairtradeData");

            // NumeroCertificados
            this.TextEditOekotex.EditValue = listCerts.Valor("CDU_OekotexNum");
            this.TextEditOekotex2.EditValue = listCerts.Valor("CDU_OekotexNum2");
            this.TextEditFsc.EditValue = listCerts.Valor("CDU_FscNum");
            this.TextEditEuropeanFlax.EditValue = listCerts.Valor("CDU_EuropeanflaxNum");
            this.TextEditSupima.EditValue = listCerts.Valor("CDU_SupimaNum");

            // Classes Oekotex
            this.LookUpEditClasse.EditValue = listCerts.Valor("CDU_OekotexClasse");
            this.LookUpEditClasse2.EditValue = listCerts.Valor("CDU_OekotexClasse2");
        }


        private void AlteraCertsClientes()
        {

            try
            {
                {
                    StdBECampos Campos = new StdBECampos();

                    Campos = BSO.Base.Clientes.DaValorAtributos(this.TextEditCodigoCliente.EditValue.ToString(), "CDU_Oekotex", "CDU_Oekotex2", "CDU_Gots", "CDU_Ocs", "CDU_Bci", "CDU_EgyptCotton", "CDU_Supima", "CDU_SupimaData", "CDU_SupimaNum", "CDU_Grs", "CDU_Rcs", "CDU_Fsc", "CDU_EuropeanFlax", "CDU_Iso9001", "CDU_Iso14001", "CDU_Sa8000", "CDU_Fairtrade", "CDU_OekotexData", "CDU_OekotexData2", "CDU_GotsData", "CDU_OcsData", "CDU_BciData", "CDU_EgyptCottonData", "CDU_GrsData", "CDU_RcsData", "CDU_FscData", "CDU_EuropeanFlaxData", "CDU_Iso9001Data", "CDU_Iso14001Data", "CDU_Sa8000Data", "CDU_FairtradeData", "CDU_OekotexNum", "CDU_OekotexNum2", "CDU_FscNum", "CDU_EuropeanflaxNum", "CDU_OekotexClasse", "CDU_OekotexClasse2");

                    // CheckBoxs
                    Campos["CDU_Oekotex"].Valor = this.CheckEditOekotex.EditValue;
                    Campos["CDU_Oekotex2"].Valor = this.CheckEditOekotex2.EditValue;
                    Campos["CDU_Gots"].Valor = this.CheckEditGots.EditValue;
                    Campos["CDU_Ocs"].Valor = this.CheckEditOcs.EditValue;
                    Campos["CDU_Bci"].Valor = this.CheckEditBci.EditValue;
                    Campos["CDU_EgyptCotton"].Valor = this.CheckEditEgypt.EditValue;
                    Campos["CDU_Supima"].Valor = this.CheckEditSupima.EditValue;
                    Campos["CDU_Grs"].Valor = this.CheckEditGrs.EditValue;
                    Campos["CDU_Rcs"].Valor = this.CheckEditRcs.EditValue;
                    Campos["CDU_Fsc"].Valor = this.CheckEditFsc.EditValue;
                    Campos["CDU_EuropeanFlax"].Valor = this.CheckEditFlax.EditValue;
                    Campos["CDU_Iso9001"].Valor = this.CheckEdit9001.EditValue;
                    Campos["CDU_Iso14001"].Valor = this.CheckEdit14001.EditValue;
                    Campos["CDU_Sa8000"].Valor = this.CheckEditSa.EditValue;
                    Campos["CDU_Fairtrade"].Valor = this.CheckEditFairTrade.EditValue;

                    // Datas
                    Campos["CDU_OekotexData"].Valor = Strings.Format(this.DateEditOekotex.EditValue, "yyyy-MM-dd");
                    Campos["CDU_OekotexData2"].Valor = Strings.Format(this.DateEditOekotex.EditValue, "yyyy-MM-dd");
                    Campos["CDU_GotsData"].Valor = Strings.Format(this.DateEditGots.EditValue, "yyyy-MM-dd");
                    Campos["CDU_OcsData"].Valor = Strings.Format(this.DateEditOcs.EditValue, "yyyy-MM-dd");
                    Campos["CDU_BciData"].Valor = Strings.Format(this.DateEditBci.EditValue, "yyyy-MM-dd");
                    Campos["CDU_EgyptCottonData"].Valor = Strings.Format(this.DateEditEgypt.EditValue, "yyyy-MM-dd");
                    Campos["CDU_SupimaData"].Valor = Strings.Format(this.DateEditSupima.EditValue, "yyyy-MM-dd");
                    Campos["CDU_GrsData"].Valor = Strings.Format(this.DateEditGrs.EditValue, "yyyy-MM-dd");
                    Campos["CDU_RcsData"].Valor = Strings.Format(this.DateEditRcs.EditValue, "yyyy-MM-dd");
                    Campos["CDU_FscData"].Valor = Strings.Format(this.DateEditFsc.EditValue, "yyyy-MM-dd");
                    Campos["CDU_EuropeanFlaxData"].Valor = Strings.Format(this.DateEditFlax.EditValue, "yyyy-MM-dd");
                    Campos["CDU_Iso9001Data"].Valor = Strings.Format(this.DateEdit9001.EditValue, "yyyy-MM-dd");
                    Campos["CDU_Iso14001Data"].Valor = Strings.Format(this.DateEdit14001.EditValue, "yyyy-MM-dd");
                    Campos["CDU_Sa8000Data"].Valor = Strings.Format(this.DateEditSa.EditValue, "yyyy-MM-dd");
                    Campos["CDU_FairtradeData"].Valor = Strings.Format(this.DateEditFairTrade.EditValue, "yyyy-MM-dd");

                    // NumeroCertificados
                    Campos["CDU_OekotexNum"].Valor = this.TextEditOekotex.EditValue;
                    Campos["CDU_OekotexNum2"].Valor = this.TextEditOekotex2.EditValue;
                    Campos["CDU_FscNum"].Valor = this.TextEditFsc.EditValue;
                    Campos["CDU_EuropeanflaxNum"].Valor = this.TextEditEuropeanFlax.EditValue;
                    Campos["CDU_SupimaNum"].Valor = this.TextEditSupima.EditValue;
                    // Classes
                    Campos["CDU_OekotexClasse"].Valor = this.LookUpEditClasse.EditValue;
                    Campos["CDU_OekotexClasse2"].Valor = this.LookUpEditClasse2.EditValue;

                    BSO.Base.Clientes.ActualizaValorAtributos(this.TextEditCodigoCliente.EditValue.ToString(), Campos);
                    CopiaMundifios();
                }

            }
            catch
            {

                MessageBox.Show("Mundifios - Erro ao gravar: ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }


        }


        private void CopiaMundifios()
        {
            try
            {
                string ent = BSO.Base.Clientes.DaValorAtributo(TextEditCodigoCliente.EditValue.ToString(), "CDU_EntidadeInterna");

                if(Module1.AbreEmpresa("Mundifios"))
                {
                    StdBELista Forn = new StdBELista();
                    Forn = BSO.Consulta("select f.Fornecedor from Fornecedores f where f.FornecedorAnulado='0' and f.CDU_EntidadeInterna='" + ent + "'");
                    Forn.Inicio();


                    if(Forn.Vazia()==false)
                    {

                        StdBECampos Campos = new StdBECampos();
                        Campos = BSO.Base.Clientes.DaValorAtributos(this.TextEditCodigoCliente.EditValue.ToString(), "CDU_Oekotex", "CDU_Oekotex2", "CDU_Gots", "CDU_Ocs", "CDU_Bci", "CDU_EgyptCotton", "CDU_Supima", "CDU_SupimaData", "CDU_SupimaNum", "CDU_Grs", "CDU_Rcs", "CDU_Fsc", "CDU_EuropeanFlax", "CDU_Iso9001", "CDU_Iso14001", "CDU_Sa8000", "CDU_Fairtrade", "CDU_OekotexData", "CDU_OekotexData2", "CDU_GotsData", "CDU_OcsData", "CDU_BciData", "CDU_EgyptCottonData", "CDU_GrsData", "CDU_RcsData", "CDU_FscData", "CDU_EuropeanFlaxData", "CDU_Iso9001Data", "CDU_Iso14001Data", "CDU_Sa8000Data", "CDU_FairtradeData", "CDU_OekotexNum", "CDU_OekotexNum2", "CDU_FscNum", "CDU_EuropeanflaxNum", "CDU_OekotexClasse", "CDU_OekotexClasse2");
                        BSO.Base.Fornecedores.ActualizaValorAtributos(Forn.Valor("Fornecedor"), Campos);
                        MessageBox.Show("Filopa - Dados gravados com sucesso!" + Strings.Chr(13) + Strings.Chr(13) + "Mundifios - Dados gravados com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                        MessageBox.Show("Filopa - Dados gravados com sucesso!" + Strings.Chr(13) + Strings.Chr(13) + "Mundifios - Erro:Fornecedor inexistente(EntidadeInterna " & BSO.Base.Clientes.DaValorAtributo(TextEditCodigoCliente.EditValue.ToString(), "CDU_EntidadeInterna") & ")", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Module1.FechaEmpresa();
                }
            }
            catch
            {
                MessageBox.Show("Filopa - Dados gravados com sucesso!" + Strings.Chr(13) + Strings.Chr(13) + "Mundifios - Erro:", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TextEditCliente_EditValueChanged(object sender, EventArgs e)
        {

            CarregaDados();

        }

        private void TextEditCodigoCliente_EditValueChanged(object sender, EventArgs e)
        {

            TextEditCliente.EditValue = BSO.Base.Clientes.DaValorAtributo(TextEditCodigoCliente.EditValue.ToString(), "Nome");


        }

        private void TextEditCodigoCliente_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F4)
                PSO.AbreLista(0, "Clientes", "Cliente",this.FindForm(), this.TextEditCodigoCliente, "mnuTabCliente", blnModal: true);

        }
        StdBELista ListaClasse;
        private void FrmFornecedoresCertsFilopaView_Load(object sender, EventArgs e)
        {

            this.TextEditCodigoCliente.EditValue = Module1.certEntidade;


            DataTable dtclasse = new DataTable();
            dtclasse = BSO.DSO.ConsultaDataTable("SELECT CDU_Classe FROM PRIMundifios.dbo.TDU_ClassesCertificadoOKOTEX");


            this.LookUpEditClasse.Properties.DataSource = dtclasse;
            LookUpEditClasse.Properties.DisplayMember = "CDU_Classe";
            LookUpEditClasse.Properties.ValueMember = "CDU_Classe";

            this.LookUpEditClasse2.Properties.DataSource = dtclasse;
            LookUpEditClasse2.Properties.DisplayMember = "CDU_Classe";
            LookUpEditClasse2.Properties.ValueMember = "CDU_Classe";


        }
    }
}
