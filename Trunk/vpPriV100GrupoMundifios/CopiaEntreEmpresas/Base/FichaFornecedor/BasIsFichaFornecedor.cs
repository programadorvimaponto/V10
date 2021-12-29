using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.Base.Editors;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using StdBE100;
using System.Windows.Forms;

namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.CopiaEntreEmpresas.Base.FichaFornecedor
{
    public class BasIsFichaFornecedor : FichaFornecedores
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("CopiaEntreEmpresas") == 1)
            {
                if (!ValidacoesCamposUtilizador())
                    Cancel = true;
            }
        }

        // #edusamp
        public bool ValidacoesCamposUtilizador()
        {
            if (Strings.Len(this.Fornecedor.CamposUtil["CDU_NomeEmpresaGrupo"].Valor + "") == 0)
            {
                // Se n�o tiver empresa definida, garante que o campo de utilizador de fornecedor est� vazio
                this.Fornecedor.CamposUtil["CDU_CodigoFornecedorGrupo"].Valor = "";

                return true;
            }
            else
            {
                // Se a empresa de grupo estiver preenchida..

                // Colocar a empresa em ma�sculas
                this.Fornecedor.CamposUtil["CDU_NomeEmpresaGrupo"].Valor = Strings.UCase(this.Fornecedor.CamposUtil["CDU_NomeEmpresaGrupo"].Valor + "");

                if (!GetTrueIfEmpresaValida(this.Fornecedor.CamposUtil["CDU_NomeEmpresaGrupo"].Valor + ""))
                {
                    // Se a empresa n�o for v�lida, aviso (na outra fun��o) e retorno falso
                    return false;
                }
                else if (GetTrueIfFornecedorValido(this.Fornecedor.CamposUtil["CDU_NomeEmpresaGrupo"].Valor + "", this.Fornecedor.CamposUtil["CDU_CodigoFornecedorGrupo"].Valor + "") == false)
                {
                    return false;
                }
                else
                {
                    // If GetTrueIfArmazemValido(Me.Fornecedor.CamposUtil("CDU_NomeEmpresaGrupo").Valor, Me.Fornecedor.CamposUtil("CDU_ArmazemGrupo").Valor) = False Then
                    // ValidacoesCamposUtilizador = False: Exit Function
                    // Else
                    return true;
                }
            }
        }

        // #edusamp
        public bool GetTrueIfEmpresaValida(string Empresa)
        {
            // Verificar se o Nome de Empresa de Destino pertence � empresa de grupo. N�o basta apenas estar preenchido!
            StdBELista stdBE_ListaEmpresasGrupo;
            stdBE_ListaEmpresasGrupo = BSO.Consulta("SELECT CDU_Empresa, CDU_Nome, CDU_Instancia " + "  FROM PRIEMPRE.dbo.TDU_EmpresasGrupo " + "  WHERE CDU_Empresa = '" + Empresa + "' ");

            if (!stdBE_ListaEmpresasGrupo.Vazia())
                return true;
            else
            {
                MessageBox.Show("A Empresa '" + Empresa + "' n�o pertence �s empresas do grupo." + Strings.Chr(13) + "Isto �, n�o consta na tabela TDU_EmpresasGrupo.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // #edusamp
        public bool GetTrueIfFornecedorValido(string Empresa, string Fornecedor)
        {
            StdBELista stdBE_ListaFornecedor;
            stdBE_ListaFornecedor = BSO.Consulta("SELECT Fornecedor " + "  FROM PRI" + Empresa + ".dbo.Fornecedores " + "  WHERE Fornecedor = '" + Fornecedor + "' and FornecedorAnulado = 'false'");

            if (!stdBE_ListaFornecedor.Vazia())
                return true;
            else
            {
                MessageBox.Show("O Fornecedor '" + Fornecedor + "' n�o � v�lido ou est� anulado.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // #edusamp
        public bool GetTrueIfArmazemValido(string Empresa, string Armazem)
        {
            StdBELista stdBE_ListaArmazem;
            stdBE_ListaArmazem = BSO.Consulta("SELECT Armazem " + "  FROM PRI" + Empresa + ".dbo.Armazens " + "  WHERE Armazem = '" + Armazem + "'");

            if (!stdBE_ListaArmazem.Vazia())
                return true;
            else
            {
                MessageBox.Show("O Armaz�m '" + Armazem + "' n�o existe ou n�o � v�lido.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}