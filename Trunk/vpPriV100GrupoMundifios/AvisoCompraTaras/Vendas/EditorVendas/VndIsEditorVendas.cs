using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using static BasBE100.BasBETiposGcp;

namespace AvisoCompraTaras
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("AvisoCompraTaras") == 1)
            {
                // JFC a pedido do Jafernandes. Comprar as taras aos clientes. Sair nas Guias.
                if (this.DocumentoVenda.Tipodoc == "GR" & this.DocumentoVenda.Pais == "PT" & this.DocumentoVenda.Entidade != "1207" & this.DocumentoVenda.Entidade != "0580" & this.DocumentoVenda.Entidade != "0707")
                {
                    StdBELista lista;
                    bool Escreve;
                    lista = BSO.Consulta("SELECT Entidade FROM CabecDoc Where Tipodoc='GR' and Serie=" + "'" + this.DocumentoVenda.Serie + "' and Numdoc=" + "'" + this.DocumentoVenda.NumDoc + "'");
                    Escreve = false;

                    if ((lista.Vazia() == true))
                    {
                        for (var i = 1; i <= this.DocumentoVenda.Linhas.NumItens; i++)
                        {
                            if (this.DocumentoVenda.Linhas.GetEdita(i).Quantidade > 10000)
                            {
                                // Este if tem que ser igual ao do Corpo
                                if (Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "0817" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "1132" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "1387" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "1338" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "1560" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "0218" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "0331" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "0922" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "0262" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "0459" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "1865" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "1317" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "1219" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "1069")
                                    Escreve = true;
                            }
                        }
                        // CABECALHO
                        if (Escreve)
                        {
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(this.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "");
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(this.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "");
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(this.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "INFORMAMOS QUE A MUNDIFIOS COMPRA A 0.05€ CADA CARTÃO E 0.50€ CADA PALETE. ");
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(this.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "DO(s) SEGUINTE(s) ARTIGO(s)/LOTE(s):");
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(this.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "");
                        }

                        for (var i = 1; i <= this.DocumentoVenda.Linhas.NumItens; i++)
                        {
                            // CORPO
                            if (this.DocumentoVenda.Linhas.GetEdita(i).Quantidade > 10000)
                            {
                                if (Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "0817" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "1132" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "1387" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "1338" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "1560" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "0218" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "0331" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "0922" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "0262" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "0459" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "1865" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "1317" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "1219" | Strings.Left(this.DocumentoVenda.Linhas.GetEdita(i).Lote, 4) == "1069")
                                    BSO.Vendas.Documentos.AdicionaLinhaEspecial(this.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, this.DocumentoVenda.Linhas.GetEdita(i).Descricao + "/" + this.DocumentoVenda.Linhas.GetEdita(i).Lote);
                            }
                        }

                        // RODAPE
                        if (Escreve)
                        {
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(this.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "");
                            BSO.Vendas.Documentos.AdicionaLinhaEspecial(this.DocumentoVenda, vdTipoLinhaEspecial.vdLinha_Comentario, 0, "NA RECOLHA AS TARAS DEVERÃO ESTAR DEVIDAMENTE SEPARADAS E ORGANIZADAS.");
                        }
                    }
                }
            }
        }
    }
}