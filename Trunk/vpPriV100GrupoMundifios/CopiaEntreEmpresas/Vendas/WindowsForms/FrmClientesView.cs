using Microsoft.VisualBasic;
using Primavera.Extensibility.CustomForm;
using StdBE100;
using System;
using System.Windows.Forms;

namespace CopiaEntreEmpresas
{
    public partial class FrmClientesView : CustomForm
    {
        public FrmClientesView()
        {
            InitializeComponent();
        }
        public string EmpresaDestino { get; set; }
        public string PRIEmpresaDestino { get; set; }
        public string CodigoCliente { get; set; }
        public string DescricaoCliente { get; set; }
        public string CodigoArmazem { get; set; }
        public string CodigoLocalDescarga { get; set; }
        public string CodigoMargem { get; set; }
        public string CodigoIdioma { get; set; }
        public string CodigoMoradaEntrega2 { get; set; }
        public string CodigoLocalidadeEntrega { get; set; }
        public string CodigoPostalEntrega { get; set; }
        public string CodPostalLocalidadeEntrega { get; set; }
        public string DistritoEntrega { get; set; }

        private void FrmClientesView_Load(object sender, EventArgs e)
        {
            if (FindForm() is Form pai)
            {
                pai.MinimumSize = pai.Size;
                pai.MaximumSize = pai.Size;
            }

            TextEditCodigoCliente.EditValue = CodigoCliente;
            TextEditDescricaoCliente.EditValue = DescricaoCliente;
            TextEditCodigoArmazem.EditValue = CodigoArmazem;
            TextEditCodigoLocalDescarga.EditValue = CodigoLocalDescarga;
            TextEditCodigoMargem.EditValue = CodigoMargem;
            TextEditCodigoIdioma.EditValue = CodigoIdioma;
            TextEditCodigoMoradaEntrega2.EditValue = CodigoMoradaEntrega2;
            TextEditCodigoLocalidadeEntrega.EditValue = CodigoLocalidadeEntrega;
            TextEditCodigoPostalEntrega.EditValue = CodigoPostalEntrega;
            TextEditCodPostalLocalidadeEntrega.EditValue = CodPostalLocalidadeEntrega;
            TextEditDistritoEntrega.EditValue=DistritoEntrega;
        }
        private void FrmClientesView_Activated(object sender, EventArgs e)
        {
            PRIEmpresaDestino = "PRI" + EmpresaDestino;
            LabelTitulo.Text = "Clientes da Empresa " + EmpresaDestino;
        }

        public bool ValidarCampos()
        {
            ActualizaDescricaoClientes();
            ActualizaDescricaoArmazem();
            ActualizaDescricaoLocalDescarga();

            if (Strings.Len(this.TextEditDescricaoCliente.EditValue) == 0)
            {
                Interaction.MsgBox("Cliente inválido", Constants.vbCritical, "Atenção");
                return false;
            }

            if (Strings.Len(this.TextEditDescricaoArmazem.EditValue) == 0)
            {
                Interaction.MsgBox("Armazém inválido", Constants.vbCritical, "Atenção");
                return false;
            }

            if (Strings.Len(this.TextEditDescricaoLocalDescarga.EditValue) == 0 & Strings.Len(this.TextEditCodigoLocalDescarga.EditValue) > 0)
            {
                Interaction.MsgBox("Local Descarga inválido", Constants.vbCritical, "Atenção");
                return false;
            }

            return true;
        }

        private void barButtonItemFechar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void barButtonItemConfirmar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!ValidarCampos())
                return;

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void TextEditCodigoCliente_EditValueChanged(object sender, EventArgs e)
        {
            ActualizaDescricaoClientes();
        }

        private void TextEditCodigoArmazem_EditValueChanged(object sender, EventArgs e)
        {
            ActualizaDescricaoArmazem();
        }

        private void TextEditCodigoLocalDescarga_EditValueChanged(object sender, EventArgs e)
        {
            ActualizaDescricaoLocalDescarga();
        }

        private void TextEditCodigoCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
                InvocarListaClientes();
        }

        private void TextEditCodigoArmazem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
                InvocarListaArmazens();
        }

        private void TextEditCodigoLocalDescarga_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
                InvocarListaLocalDescarga();
        }

        // Funções*******************

        // *InvocarListas*
        private void InvocarListaClientes()
        {
            // Não faz validação do cliente
            if (!Validacoes(false))
                return;

            // A abertura da lista pode ser modal ou não modal
            this.TextEditCodigoCliente.EditValue = PSO.Listas.GetF4SQL("Empresa " + EmpresaDestino, "Select * from " + PRIEmpresaDestino + ".dbo.Clientes  where ClienteAnulado = 0", "Cliente");
        }

        private void InvocarListaArmazens()
        {
            // Não faz validação do cliente
            if (!Validacoes(false))
                return;

            // A abertura da lista pode ser modal ou não modal
            this.TextEditCodigoArmazem.EditValue = PSO.Listas.GetF4SQL("Armazéns da Empresa " + EmpresaDestino, "Select * from " + PRIEmpresaDestino + ".dbo.Armazens " + " Where BloqueioEntradas = 0", "Armazem");
        }

        private void InvocarListaLocalDescarga()
        {
            // Faz validação do cliente
            if (!Validacoes(true))
                return;

            // A abertura da lista pode ser modal ou não modal
            string Titulo;
            Titulo = "Locais de Descarga do Cliente " + this.TextEditCodigoCliente.EditValue + " - " + this.TextEditDescricaoCliente.EditValue;

            this.TextEditCodigoLocalDescarga.EditValue = PSO.Listas.GetF4SQL(Titulo, "Select * from " + PRIEmpresaDestino + ".dbo.MoradasAlternativasClientes  where cliente = '" + this.TextEditCodigoCliente.EditValue + "' ", " MoradaAlternativa");
        }

        // *ActualizaDescrições*
        public bool ActualizaDescricaoClientes()
        {
            StdBELista stdBE_Lista;

            if (Strings.Len(this.TextEditCodigoCliente.EditValue) == 0)
            {
                this.TextEditDescricaoCliente.EditValue = "";
                this.TextEditCodigoIdioma.EditValue = "";
                return false;
            }

            stdBE_Lista = BSO.Consulta("SELECT Nome, Idioma FROM " + PRIEmpresaDestino + ".dbo.Clientes WHERE Cliente= '" + this.TextEditCodigoCliente.EditValue + "'");

            if (!stdBE_Lista.Vazia())
            {
                stdBE_Lista.Inicio();
                this.TextEditDescricaoCliente.EditValue = stdBE_Lista.Valor("Nome");
                this.TextEditCodigoIdioma.EditValue = stdBE_Lista.Valor("Idioma");
                return true;
            }
            else
            {
                this.TextEditDescricaoCliente.EditValue = "";
                this.TextEditCodigoIdioma.EditValue = "";
                return false;
            }
        }

        public bool ActualizaDescricaoArmazem()
        {
            StdBELista stdBE_Lista;

            if (Strings.Len(this.TextEditCodigoArmazem.EditValue) == 0)
            {
                return false;
            }

            stdBE_Lista = BSO.Consulta("SELECT Descricao FROM " + PRIEmpresaDestino + ".dbo.Armazens  WHERE Armazem= '" + this.TextEditCodigoArmazem.EditValue + "'");

            if (!stdBE_Lista.Vazia())
            {
                stdBE_Lista.Inicio();
                this.TextEditDescricaoArmazem.EditValue = stdBE_Lista.Valor("Descricao");
                return true;
            }
            else
            {
                this.TextEditDescricaoArmazem.EditValue = "";
                return false;
            }
        }

        public bool ActualizaDescricaoLocalDescarga()
        {
            StdBELista stdBE_Lista;

            if (Strings.Len(this.TextEditCodigoLocalDescarga.EditValue) == 0)
            {
                return false;
            }

            stdBE_Lista = BSO.Consulta(" SELECT * from " + PRIEmpresaDestino + ".dbo.MoradasAlternativasClientes " + " WHERE Cliente= '" + this.TextEditCodigoCliente.EditValue + "'" + " AND MoradaAlternativa = '" + this.TextEditCodigoLocalDescarga.EditValue + "'");

            if (!stdBE_Lista.Vazia())
            {
                stdBE_Lista.Inicio();
                this.TextEditDescricaoLocalDescarga.EditValue = stdBE_Lista.Valor("Morada") + " (" + stdBE_Lista.Valor("Localidade") + ")";
                this.TextEditCodigoMoradaEntrega.EditValue = stdBE_Lista.Valor("Morada");
                this.TextEditCodigoMoradaEntrega2.EditValue = stdBE_Lista.Valor("Morada2");
                this.TextEditCodigoLocalidadeEntrega.EditValue = stdBE_Lista.Valor("Localidade");
                this.TextEditCodigoPostalEntrega.EditValue = stdBE_Lista.Valor("Cp");
                this.TextEditCodPostalLocalidadeEntrega.EditValue = stdBE_Lista.Valor("CpLocalidade");
                this.TextEditDistritoEntrega.EditValue = stdBE_Lista.Valor("Distrito");

                return true;
            }
            else
            {
                this.TextEditDescricaoLocalDescarga.EditValue = "";
                this.TextEditCodigoMoradaEntrega.EditValue = "";
                this.TextEditCodigoMoradaEntrega2.EditValue = "";
                this.TextEditCodigoLocalidadeEntrega.EditValue = "";
                this.TextEditCodigoPostalEntrega.EditValue = "";
                this.TextEditCodPostalLocalidadeEntrega.EditValue = "";
                this.TextEditDistritoEntrega.EditValue = "";
                return false;
            }
        }

        // *ActualizaDescrições*

        // Funções*******************

        // Validações********************
        private bool Validacoes(bool ValidaCodigoCliente)
        {
            if (Strings.Len(PRIEmpresaDestino) <= 3)
            {
                Interaction.MsgBox("A empresa de estido não está preenchida", Constants.vbCritical, "Atenção");
                return false;
            }

            if (ValidaCodigoCliente == true)
            {
                if (Strings.Len(this.TextEditDescricaoCliente.EditValue) == 0)
                {
                    Interaction.MsgBox("O Cliente final não está preenchido", Constants.vbCritical, "Atenção");
                    return false;
                }
            }

            return true;
        }


    }
}