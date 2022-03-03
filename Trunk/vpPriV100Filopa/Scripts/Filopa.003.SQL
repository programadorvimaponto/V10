/*
-------------------------------------------------------------------------------------------
Data:	    12-06-2018
Autor:	    Andre Sousa
Solução:    VMP Extensibilidade Filopa
Objetivos:  Novos campos / remover campos nao necessários
Coment.:    
-------------------------------------------------------------------------------------------
*/

BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.TABLES
			WHERE TABLE_NAME = 'TDU_Locais'
			)
	BEGIN
		CREATE TABLE TDU_Locais (
			CDU_Local NVARCHAR(25) NOT NULL
			,CDU_Descricao NVARCHAR(200) NULL			
			,CONSTRAINT PK_TDU_Locais PRIMARY KEY NONCLUSTERED (CDU_Local ASC)
			)

		DELETE
		FROM StdCamposVar
		WHERE Tabela = 'TDU_Locais'

		DELETE
		FROM StdTabelasVar
		WHERE Tabela = 'TDU_Locais'

		INSERT INTO StdTabelasVar (
			Tabela
			,Apl
			)
		VALUES (
			'TDU_Locais'
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
			'TDU_Locais'
			,'CDU_Local'
			,'Local'
			,'Local'
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
			'TDU_Locais'
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



--mudar queries dos campos porto , destino , localidades

UPDATE StdCamposVar SET  Query = 'SELECT * FROM [TDU_Locais] WITH (NOLOCK) ORDER BY [CDU_Local]' WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Porto'
UPDATE StdCamposVar SET  Query = 'SELECT * FROM [TDU_Locais] WITH (NOLOCK) ORDER BY [CDU_Local]' WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Porto'

UPDATE StdCamposVar SET  Query = 'SELECT * FROM [TDU_Locais] WITH (NOLOCK) ORDER BY [CDU_Local]' WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Destino'
UPDATE StdCamposVar SET  Query = 'SELECT * FROM [TDU_Locais] WITH (NOLOCK) ORDER BY [CDU_Local]' WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Destino'

UPDATE StdCamposVar SET  Query = 'SELECT * FROM [TDU_Locais] WITH (NOLOCK) ORDER BY [CDU_Local]' WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Localidade'
UPDATE StdCamposVar SET  Query = 'SELECT * FROM [TDU_Locais] WITH (NOLOCK) ORDER BY [CDU_Local]' WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Localidade'


--retirar constrains para a alteração da length

--BEGIN
--	IF EXISTS (
--			SELECT 1
--			FROM sysobjects
--			WHERE NAME = 'CabecDoc_CDU_Porto_DF'
--			)
--	BEGIN
--		ALTER TABLE CabecDoc DROP CONSTRAINT CabecDoc_CDU_Porto_DF
--	END
--END

--BEGIN
--	IF EXISTS (
--			SELECT 1
--			FROM sysobjects
--			WHERE NAME = 'CabecDoc_CDU_Destino_DF'
--			)
--	BEGIN
--		ALTER TABLE CabecDoc DROP CONSTRAINT CabecDoc_CDU_Destino_DF
--	END
--END

--BEGIN
--	IF EXISTS (
--			SELECT 1
--			FROM sysobjects
--			WHERE NAME = 'CabecDoc_CDU_Localidade_DF'
--			)
--	BEGIN
--		ALTER TABLE CabecDoc DROP CONSTRAINT CabecDoc_CDU_Localidade_DF
--	END
--END

----alterar a length

--ALTER TABLE CabecDoc ALTER COLUMN CDU_Porto  NVARCHAR(25) NULL
--ALTER TABLE CabecDoc ALTER COLUMN CDU_Destino  NVARCHAR(25) NULL
--ALTER TABLE CabecDoc ALTER COLUMN CDU_Localidade  NVARCHAR(25) NULL


