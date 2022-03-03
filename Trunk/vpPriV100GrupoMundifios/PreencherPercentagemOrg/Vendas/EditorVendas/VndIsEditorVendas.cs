using Generico;
using Microsoft.VisualBasic;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Sales.Editors;
using StdBE100;
using System.Windows.Forms;

namespace PreencherPercentagemOrg
{
    public class VndIsEditorVendas : EditorVendas
    {
        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            base.AntesDeGravar(ref Cancel, e);

            if (Module1.VerificaToken("PreencherPercentagemOrg") == 1)
            {
                if (DocumentoVenda.Tipodoc == "ECL")
                {
                    StdBELista LinhaNaoExiste;
                    bool ReplaceGots;
                    bool ReplaceOCS;
                    bool ReplaceGRS;

                    for (int i = 1; i <= DocumentoVenda.Linhas.NumItens; i++)
                    {
                        ReplaceGots = false;
                        ReplaceOCS = false;
                        ReplaceGRS = false;

                        if (DocumentoVenda.Linhas.GetEdita(i).Armazem != "PLA")
                        {
                            DocumentoVenda.Linhas.GetEdita(i).Descricao = Strings.Replace(DocumentoVenda.Linhas.GetEdita(i).Descricao, "GOTS", "Gots");
                            LinhaNaoExiste = BSO.Consulta("select top 1 ln.CDU_Gots, ln.CDU_OCS, ln.CDU_GRS, ln.Artigo from LinhasDoc ln where ln.Artigo='" + DocumentoVenda.Linhas.GetEdita(i).Artigo + "' and ln.Id='" + DocumentoVenda.Linhas.GetEdita(i).IdLinha + "'");

                            if (LinhaNaoExiste.Vazia() == true)
                            {
                                if (DocumentoVenda.Linhas.GetEdita(i).Artigo + "" != "" && bool.Parse(DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_ReplacePercCert"].Valor.ToString()) == false)
                                {
                                    if (DocumentoVenda.Linhas.GetEdita(i).Descricao.Contains("Gots"))
                                    {
                                        if (DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Gots"].Valor.ToString() == "0" || string.IsNullOrEmpty(DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Gots"].Valor.ToString()))
                                        {
                                            MessageBox.Show("Atenção preencher o campo de utilizador Perc. Gots antes de gravar!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            Cancel = true;
                                            return;
                                        }
                                        else
                                            ReplaceGots = true;
                                    }

                                    if (DocumentoVenda.Linhas.GetEdita(i).Descricao.Contains("OCS"))
                                    {
                                        if (DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_OCS"].Valor.ToString() == "0" || string.IsNullOrEmpty(DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_OCS"].Valor.ToString()))
                                        {
                                            MessageBox.Show("Atenção preencher o campo de utilizador Perc. OCS antes de gravar!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            Cancel = true;
                                            return;
                                        }
                                        else
                                            ReplaceOCS = true;
                                    }

                                    if (DocumentoVenda.Linhas.GetEdita(i).Descricao.Contains("GRS"))
                                    {
                                        if (DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_GRS"].Valor.ToString() == "0" || string.IsNullOrEmpty(DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_GRS"].Valor.ToString()))
                                        {
                                            MessageBox.Show("Atenção preencher o campo de utilizador Perc. GRS antes de gravar!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            Cancel = true;
                                            return;
                                        }
                                        else
                                            ReplaceGRS = true;
                                    }

                                    if (ReplaceGots == true && DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Gots"].Valor.ToString() != "100")
                                    {
                                        this.DocumentoVenda.Linhas.GetEdita(i).Descricao = Strings.Replace(this.DocumentoVenda.Linhas.GetEdita(i).Descricao, "Org. Gots", this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Gots"].Valor + "% Org. Gots");
                                        this.DocumentoVenda.Linhas.GetEdita(i).Descricao = Strings.Replace(this.DocumentoVenda.Linhas.GetEdita(i).Descricao, "Organico Gots", this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Gots"].Valor + "% Org. Gots");
                                        this.DocumentoVenda.Linhas.GetEdita(i).Descricao = Strings.Replace(this.DocumentoVenda.Linhas.GetEdita(i).Descricao, "Org.  Gots", this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Gots"].Valor + "% Org. Gots");
                                        this.DocumentoVenda.Linhas.GetEdita(i).Descricao = Strings.Replace(this.DocumentoVenda.Linhas.GetEdita(i).Descricao, "Org.Gots", this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Gots"].Valor + "% Org. Gots");
                                        this.DocumentoVenda.Linhas.GetEdita(i).Descricao = Strings.Replace(this.DocumentoVenda.Linhas.GetEdita(i).Descricao, "Org Gots", this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Gots"].Valor + "% Org. Gots");
                                        this.DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_ReplacePercCert"].Valor = true;
                                    }

                                    if (ReplaceOCS == true && DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_OCS"].Valor.ToString() != "100")
                                    {
                                        DocumentoVenda.Linhas.GetEdita(i).Descricao = Strings.Replace(DocumentoVenda.Linhas.GetEdita(i).Descricao, "Org. OCS", DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_OCS"].Valor + "% Org. OCS");
                                        DocumentoVenda.Linhas.GetEdita(i).Descricao = Strings.Replace(DocumentoVenda.Linhas.GetEdita(i).Descricao, "Org OCS", DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_OCS"].Valor + "% Org. OCS");
                                        DocumentoVenda.Linhas.GetEdita(i).Descricao = Strings.Replace(DocumentoVenda.Linhas.GetEdita(i).Descricao, "Organico OCS", DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_OCS"].Valor + "% Org. OCS");
                                        DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_ReplacePercCert"].Valor = true;
                                    }

                                    if (ReplaceGRS == true && DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_GRS"].Valor.ToString() != "100")
                                    {
                                        DocumentoVenda.Linhas.GetEdita(i).Descricao = Strings.Replace(DocumentoVenda.Linhas.GetEdita(i).Descricao, "GRS", DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_GRS"].Valor + "% GRS");
                                        DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_ReplacePercCert"].Valor = true;
                                    }
                                }
                                else
                                { 
                                    LinhaNaoExiste.Inicio();

                                    if (LinhaNaoExiste.Valor("CDU_Gots") != DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Gots"])
                                    {
                                        if (LinhaNaoExiste.Valor("CDU_Gots") == 0 || LinhaNaoExiste.Valor("CDU_Gots") == 100)
                                            DocumentoVenda.Linhas.GetEdita(i).Descricao = Strings.Replace(DocumentoVenda.Linhas.GetEdita(i).Descricao, "Org. Gots", DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Gots"].Valor + "% Org. Gots");
                                        else
                                            DocumentoVenda.Linhas.GetEdita(i).Descricao = Strings.Replace(DocumentoVenda.Linhas.GetEdita(i).Descricao, LinhaNaoExiste.Valor("CDU_Gots") + "% Org. Gots", DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_Gots"].Valor + "% Org. Gots");
                                    }

                                    if (LinhaNaoExiste.Valor("CDU_OCS") != DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_OCS"])
                                    {
                                        if (LinhaNaoExiste.Valor("CDU_OCS") == 0 || LinhaNaoExiste.Valor("CDU_OCS") == 100)
                                            DocumentoVenda.Linhas.GetEdita(i).Descricao = Strings.Replace(DocumentoVenda.Linhas.GetEdita(i).Descricao, "Org. OCS", DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_OCS"].Valor + "% Org. OCS");
                                        else
                                            DocumentoVenda.Linhas.GetEdita(i).Descricao = Strings.Replace(DocumentoVenda.Linhas.GetEdita(i).Descricao, LinhaNaoExiste.Valor("CDU_OCS") + "% Org. OCS", DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_OCS"].Valor + "% Org. OCS");
                                    }

                                    if (LinhaNaoExiste.Valor("CDU_GRS") != DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_GRS"])
                                    {
                                        if (LinhaNaoExiste.Valor("CDU_GRS") == 0 || LinhaNaoExiste.Valor("CDU_GRS") == 100)
                                            DocumentoVenda.Linhas.GetEdita(i).Descricao = Strings.Replace(DocumentoVenda.Linhas.GetEdita(i).Descricao, "Org. GRS", DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_GRS"].Valor + "% Org. GRS");
                                        else
                                            DocumentoVenda.Linhas.GetEdita(i).Descricao = Strings.Replace(DocumentoVenda.Linhas.GetEdita(i).Descricao, LinhaNaoExiste.Valor("CDU_GRS") + "% Org. GRS", DocumentoVenda.Linhas.GetEdita(i).CamposUtil["CDU_GRS"].Valor + "% Org. GRS");
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