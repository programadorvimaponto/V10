Componentes Externos utilizados também no ERP:
- DevExpress 		21.2.3
- CrystalReports	13.0.26
(Como a extensão é uma DLL carregada para o mesmo procesos do ERP, e no mesmo processo não podem haver diferentes versões do mesmo componente, a solução tem sempre que acompanhar a mesma versão utilizada pela Primavera.)

Componentes Externos produzidos na Vimaponto:
- vcSistema			22.002.001		Componente com algumas abstrações e regras VMP. Incluí também mecanismo para migração de schema da base de dados.
- vcSdkUi			22.002.001		Componente que utiliza internamente DevExpress, mas acrescenta mais funcionalidade e facilidade de programação.
- vpPriV100			22.1010.5008	Produto com algumas regras e classes static que garantem um partilha de contexto BSO;PSO de forma global aos projetos.
