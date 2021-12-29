using Generico;
using Primavera.Extensibility.CustomForm;
using StdBE100;
using Vimaponto.PrimaveraV100;

namespace Inditex
{
    public partial class FrmInditex : CustomForm
    {
        public FrmInditex()
        {
            InitializeComponent();

        }

        private void FrmInditex_Load(object sender, System.EventArgs e)
        {
            string SqlTipoIdentificacao = "Select CDU_TipoIdentificacao FROM prifilopa.dbo.tdu_tipoidentificacaoinditex";
            StdBELista ListaTipoIdentificacao = new StdBELista();
            ListaTipoIdentificacao = PriV100Api.BSO.Consulta(SqlTipoIdentificacao);

            if (ListaTipoIdentificacao.Vazia() == false)
            {
                ListaTipoIdentificacao.Inicio();

                for (int k = 1; k <= ListaTipoIdentificacao.NumLinhas(); k++)
                {
                    //falta linha
                    lookUpEditTipoIdentificacao.Properties.DataSource = ListaTipoIdentificacao;
                    ListaTipoIdentificacao.Seguinte();
                }
            }
            if (Module1.certFiacoes is string)
                TxtCodigoCliente.Text = Module1.certFiacoes;
            else
                TxtCodigoCliente.Text = "";

        }
    }
}