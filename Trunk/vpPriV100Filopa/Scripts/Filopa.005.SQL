/*
-------------------------------------------------------------------------------------------
Data:	    22-06-2018
Autor:	    Andre Sousa
Solução:    VMP Extensibilidade Filopa
Objetivos:  Alteração no CDU_Bancos
Coment.:    
-------------------------------------------------------------------------------------------
*/


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.TABLES
			WHERE TABLE_NAME = 'TDU_Bancos'
			)
	BEGIN
		CREATE TABLE TDU_Bancos (
			CDU_Banco NVARCHAR(3) NOT NULL
			,CDU_Descricao NVARCHAR(200) NULL			
			,CONSTRAINT PK_TDU_Bancos PRIMARY KEY NONCLUSTERED (CDU_Banco ASC)
			)

		DELETE
		FROM StdCamposVar
		WHERE Tabela = 'TDU_Bancos'

		DELETE
		FROM StdTabelasVar
		WHERE Tabela = 'TDU_Bancos'

		INSERT INTO StdTabelasVar (
			Tabela
			,Apl
			)
		VALUES (
			'TDU_Bancos'
			,'ERP'
			)

		INSERT INTO StdCamposVar (
			Tabela
			,Campo
			,Descricao
			,Texto
			,Visivel
			,Ordem
			,Pagina
			,ValorDefeito
			,Query
			,ExportarTTE
			)
		VALUES (
			'TDU_Bancos'
			,'CDU_Banco'
			,'Banco'
			,'Banco'
			,1
			,1
			,NULL
			,NULL
			,NULL
			,0
			)

		INSERT INTO StdCamposVar (
			Tabela
			,Campo
			,Descricao
			,Texto
			,Visivel
			,Ordem
			,Pagina
			,ValorDefeito
			,Query
			,ExportarTTE
			)
		VALUES (
			'TDU_Bancos'
			,'CDU_Descricao'
			,'Descrição'
			,'Descrição'
			,1
			,2
			,NULL
			,NULL
			,NULL
			,0
			)
		
	END
END
GO


UPDATE StdCamposVar SET  Query = 'SELECT * FROM [TDU_Bancos] WITH (NOLOCK) ORDER BY [CDU_Banco]' WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Banco'
UPDATE StdCamposVar SET  Query = 'SELECT * FROM [TDU_Bancos] WITH (NOLOCK) ORDER BY [CDU_Banco]' WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Banco'

--delete from StdCamposVar where campo = 'CDU_Vendedor' and Tabela='LinhasDoc'
--delete from StdCamposVar where campo = 'CDU_Vendedor' and Tabela='LinhasDocRascunhos'

--delete from StdCamposVar where campo = 'CDU_ComissaoVendedor' and Tabela='LinhasDoc'
--delete from StdCamposVar where campo = 'CDU_ComissaoVendedor' and Tabela='LinhasDocRascunhos'


--verifcar ordem antes no StdCamposVar

--UPDATE StdCamposVar SET Ordem = 12 WHERE Tabela = 'Fornecedores' and Campo = 'CDU_UsaComissaoVendedor'
--UPDATE StdCamposVar SET Ordem = 13 WHERE Tabela = 'Fornecedores' and Campo = 'CDU_Comissao'
