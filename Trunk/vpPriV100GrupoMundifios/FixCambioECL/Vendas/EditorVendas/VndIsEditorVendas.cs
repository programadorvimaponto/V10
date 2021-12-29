using Generico;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;

namespace FixCambioECL
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            // #################################################################################################
            // ####### Coloca ultimo Cambio na Linha da ECL. Pedido de Goreti - JFC 28-02-2019      ############
            // #################################################################################################

            if (Module1.VerificaToken("FixCambioECL") == 1)
            {
                if (this.DocumentoVenda.Tipodoc == "ECL")
                {
                    StdBELista cambio;
                    StdBELista Moeda;
                    StdBELista loteChange;

                    for (var j = 1; j <= this.DocumentoVenda.Linhas.NumItens; j++)
                    {
                        if (this.DocumentoVenda.Linhas.GetEdita(j).Artigo + "" != "" & this.DocumentoVenda.Linhas.GetEdita(j).Lote + "" != "")
                        {
                            // Identifica a moeda de compra
                            Moeda = BSO.Consulta("select top 1 cc.Moeda from CabecCompras cc inner join LinhasCompras lc on lc.IdCabecCompras=cc.Id inner join Fornecedores f on f.Fornecedor=cc.Entidade where f.CDU_EntidadeInterna not in ('0001','0002','0003','0004','0005','0006') and lc.Armazem!='PLA' and cc.TipoDoc in ('CNT','ECF') and  lc.Artigo='" + this.DocumentoVenda.Linhas.GetEdita(j).Artigo + "' and lc.Lote='" + this.DocumentoVenda.Linhas.GetEdita(j).Lote + "'");
                            Moeda.Inicio();

                            // Se a linha não tiver cambio atribuido, então atribui um consoante a Moeda
                            if (this.DocumentoVenda.Linhas.GetEdita(j).CamposUtil["CDU_Cambio"].Valor == string.Empty | this.DocumentoVenda.Linhas.GetEdita(j).CamposUtil["CDU_Cambio"].Valor.ToString() == "0")
                            {
                                if (Moeda.Vazia() == false)
                                {
                                    if (Moeda.Valor("Moeda") == "EUR")
                                        this.DocumentoVenda.Linhas.GetEdita(j).CamposUtil["CDU_Cambio"].Valor = 1;
                                    else
                                    {
                                        cambio = BSO.Consulta("select top 1 m.Venda from MoedasHistorico m where m.Moeda='" + Moeda.Valor("Moeda") + "' order by m.Data desc");
                                        cambio.Inicio();
                                        this.DocumentoVenda.Linhas.GetEdita(j).CamposUtil["CDU_Cambio"].Valor = cambio.Valor("Venda");
                                    }
                                }
                            }
                            else
                            {
                                // JFC - 25/05/2020
                                // Caso tenha uma cambio atribuido, valida se o lote foi alterado.
                                loteChange = BSO.Consulta("select top 1 ln.Lote from LinhasDoc ln where ln.Id='" + this.DocumentoVenda.Linhas.GetEdita(j).IdLinha + "'");
                                loteChange.Inicio();

                                if (loteChange.Vazia() == false)
                                {
                                    if (loteChange.Valor("Lote") != this.DocumentoVenda.Linhas.GetEdita(j).Lote)
                                    {
                                        // Caso o lote tenha sido alterado é necessário validar se a moeda de commpra se altera, e associar o cambio correto.
                                        // Esta parte do codigo considera apenas trocas de EUR e DOLLARS, caso haja mais moedas envolvida, por exemplo trocas de LIBRAS para DOLLARS, terá que se corrigido.
                                        if (Moeda.Valor("Moeda") == "EUR")
                                            this.DocumentoVenda.Linhas.GetEdita(j).CamposUtil["CDU_Cambio"].Valor = 1;
                                        else if (Moeda.Valor("Moeda") != "EUR" & this.DocumentoVenda.Linhas.GetEdita(j).CamposUtil["CDU_Cambio"].Valor.ToString() == "1")
                                        {
                                            cambio = BSO.Consulta("select top 1 m.Venda from MoedasHistorico m where m.Moeda='" + Moeda.Valor("Moeda") + "' order by m.Data desc");
                                            cambio.Inicio();
                                            this.DocumentoVenda.Linhas.GetEdita(j).CamposUtil["CDU_Cambio"].Valor = cambio.Valor("Venda");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}