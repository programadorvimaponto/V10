using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.Class;

namespace Vimaponto.PrimaveraV100.Clientes.GrupoMundifios.AssistenteArtigos.DataSource
{
    partial class DsEmpresasGrupo
    {
        List<EmpresaGrupo> gLstEmpresasGrupo = new List<EmpresaGrupo>();

        private DsEmpresasGrupoTableAdapters.TDU_EmpresasGrupoTableAdapter AdptEmpresasGrupo { get; set; } = new DsEmpresasGrupoTableAdapters.TDU_EmpresasGrupoTableAdapter();
        private DsEmpresasGrupoTableAdapters.UtilizadoresTableAdapter AdptUtilizadores { get; set; } = new DsEmpresasGrupoTableAdapters.UtilizadoresTableAdapter();

        public bool GetTrueIfAcessoCriacaoArtigo()
        {
            return Convert.ToBoolean(AdptUtilizadores.FillByTrueIfCriacaoArtigosByCodigo(PriV100Api.BSO.Contexto.UtilizadorActual));
        }

        public bool GetTrueIfAcessoGestaoPropriedades()
        {
            return Convert.ToBoolean(AdptUtilizadores.FillByTrueIfGestaoPropriedadesByCodigo(PriV100Api.BSO.Contexto.UtilizadorActual));
        }

        public bool CarregarEmpresasGrupo()
        {
            AdptEmpresasGrupo.Fill_EmpresasCriacaoArtigo(this.TDU_EmpresasGrupo);

            return Convert.ToBoolean(this.TDU_EmpresasGrupo.Count);
        }


        public bool GetEmpresasPermitidas()
        {
            try
            {
                // # Limpar a lista de empresas do grupo.
                VariaveisGlobais.gLstEmpresasGrupo.Clear();
                // If gLstEmpresasGrupo.Count > 0 Then MsgBox(gLstEmpresasGrupo.Item(0).Empresa) ' APAGAR

                // Inserir na posição 0 os dados referentes à empresa atual!!

                //VariaveisGlobais.gLstEmpresasGrupo.Insert(0, new EmpresaGrupo(PriV100Api.VSO.Contexto.Empresa, PriV100Api.BSO.DSO.BDAPL.DataSource, PriV100Api.VSO.Contexto.Utilizador.Utilizador, "PRILEV100", ""));
                VariaveisGlobais.gLstEmpresasGrupo.Insert(0, new EmpresaGrupo(PriV100Api.VSO.Contexto.ConnectionStringInstancia, ""));

                // If gLstEmpresasGrupo.Count > 0 Then MsgBox(gLstEmpresasGrupo.Item(0).Empresa) ' APAGAR

                // Carregar as empresas do Grupo!
                CarregarEmpresasGrupo();

                // Preparar os dados para o Config
                string sPrefixo = @"\Config_";
                string sExtensao = ".ini";
                string sFileName;
                string sCaminho;
                string sCaminhoCompleto;
                bool JaExecutouPriEmpre = false;

                // Para cada empresa de grupo....
                foreach (TDU_EmpresasGrupoRow Empresa in TDU_EmpresasGrupo)
                {

                    // If Empresa.CDU_Nome <> EmpresaExclusica Then Continue For

                    // Se a empresa forigual à inical, continua
                    if (Strings.UCase(Empresa.CDU_Empresa) == Strings.UCase(PriV100Api.BSO.Contexto.CodEmp))
                        continue;

                    // Se a instancia for a mesma que a inicial, uso o que já tenho configurado mas com a empresa do Grupo!
                    if (Strings.UCase(Empresa.CDU_Instancia) == Strings.UCase(VariaveisGlobais.gLstEmpresasGrupo[0].Instancia))
                        gLstEmpresasGrupo.Add(new EmpresaGrupo(Empresa.CDU_Empresa, VariaveisGlobais.gLstEmpresasGrupo[0].Instancia, VariaveisGlobais.gLstEmpresasGrupo[0].User, VariaveisGlobais.gLstEmpresasGrupo[0].Password, ""));
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Empresas Permitidas", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
        }

    }
}
