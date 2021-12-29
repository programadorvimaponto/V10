using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.Base.Editors;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using System;
using System.Windows.Forms;

namespace ArtigosNovos
{
    public class BasIsFichaArtigo : FichaArtigos
    {
        private string ArtigoCopiar;
        private bool ArtigoNovo;

        public override void AntesDeCriar(ExtensibilityEventArgs e)
        {
            base.AntesDeCriar(e);

            if (Module1.VerificaToken("ArtigosNovos") == 1)
                ArtigoNovo = true;
        }

        public override void AntesDeEditar(string Artigo, ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeEditar(Artigo, ref Cancel, e);

            if (Module1.VerificaToken("ArtigosNovos") == 1)
            {
                ArtigoCopiar = Artigo;

                ArtigoNovo = false;
            }
        }

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("ArtigosNovos") == 1)
            {
                if (this.Artigo.CamposUtil["CDU_DescricaoExtra"].Valor + "" == "")
                    this.Artigo.CamposUtil["CDU_DescricaoExtra"].Valor = " ";

                if (this.Artigo.Artigo != ArtigoCopiar)
                {
                    if (ArtigoNovo == false)
                        this.ArtigoIdiomas.RemoveTodos();
                    ArtigoCopiar = "";
                }

                if (ArtigoNovo == true)
                    this.Artigo.CamposUtil["CDU_DataCriacao"].Valor = DateTime.Now;

                if (this.Artigo.TipoArtigo == "3" & (this.Artigo.IntrastatCodigoPautal + "" == "" | this.Artigo.IntrastatPesoLiquido + "" == ""))
                    MessageBox.Show("Atenção:" + Strings.Chr(13) + "É obrigatório o preenchimento do código pautal (intrastat) e do respetivo peso líquido (1) no caso dos artigos do tipo mercadoria", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (this.Artigo.CamposUtil["CDU_DescricaoInterna"].Valor + "" == "" | ArtigoNovo == true)
                    this.Artigo.CamposUtil["CDU_DescricaoInterna"].Valor = this.Artigo.Descricao;
            }
        }
    }
}