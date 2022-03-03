using Primavera.Extensibility.CustomForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.Class;
using Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.DataSource;
using static Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.Class.Geral;

namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos
{
    public partial class frmListaPropriedadesMundifiosView : CustomForm
    {
        private DsPropriedadesMundifios Listagem;
        private ListagemPropriedadesMundifios ListagemTipo;
        private ListParameter ListaParametros;
        private string filter;

        public frmListaPropriedadesMundifiosView()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog()
        {
            return base.ShowDialog();
        }
        public Enum Cor { get; set; }

        public ListParameterDataRow Lista { get; set; }




        public new void Show(ListagemPropriedadesMundifios ListagemTipo = 0,  ListParameter ListaParametros = null, string Filtro = "")
        {
            this.ListagemTipo = ListagemTipo;
            this.ListaParametros = ListaParametros;
            this.filter = Filtro;
            // # instanciar dataset geral
            Listagem = new DsPropriedadesMundifios();
            base.Show();
        }

        private void barButtonItemConfirmar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItemFechar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}
