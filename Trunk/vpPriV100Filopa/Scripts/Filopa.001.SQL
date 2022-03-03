/*
-------------------------------------------------------------------------------------------
Data:	    15-05-2018
Autor:	    Andre Sousa
Solução:    VMP Extensibilidade Filopa
Objetivos:  Correções ao script inicial.
Coment.:    
-------------------------------------------------------------------------------------------
*/
BEGIN
	IF EXISTS (
			SELECT 1
			FROM sysobjects
			WHERE NAME = 'CabecDoc_CDU_NCartadeCredito_DF'
			)
	BEGIN
		ALTER TABLE CabecDoc DROP CONSTRAINT CabecDoc_CDU_NCartadeCredito_DF
	END
END

BEGIN
	IF EXISTS (
			SELECT *
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDoc'
				AND COLUMN_NAME = 'CDU_NCartaCredito'
				AND DATA_TYPE = 'INT'
			)
	BEGIN
		ALTER TABLE CabecDoc ALTER COLUMN CDU_NCartaCredito NVARCHAR(50) NULL
	END
END
GO

BEGIN
	IF EXISTS (
			SELECT 1
			FROM sysobjects
			WHERE NAME = 'CabecDocRascunhos_CDU_NCartadeCredito_DF'
			)
	BEGIN
		ALTER TABLE CabecDocRascunhos DROP CONSTRAINT CabecDocRascunhos_CDU_NCartadeCredito_DF
	END
END

BEGIN
	IF EXISTS (
			SELECT *
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'CabecDocRascunhos'
				AND COLUMN_NAME = 'CDU_NCartaCredito'
				AND DATA_TYPE = 'INT'
			)
	BEGIN
		ALTER TABLE CabecDocRascunhos ALTER COLUMN CDU_NCartaCredito NVARCHAR(50) NULL
	END
END
GO

BEGIN 
	UPDATE StdCamposVar SET Descricao = 'Cliente', Texto = 'Cliente', Query = 'SELECT [Fornecedor],[Nome] FROM [Fornecedores] WITH (NOLOCK) ORDER BY [Fornecedor]' WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Fornecedor'
	UPDATE StdCamposVar SET Descricao = 'Cliente', Texto = 'Cliente', Query = 'SELECT [Fornecedor],[Nome] FROM [Fornecedores] WITH (NOLOCK) ORDER BY [Fornecedor]' WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Fornecedor'

	UPDATE StdCamposVar SET  Query = 'SELECT * FROM [TDU_Agentes] WITH (NOLOCK) ORDER BY [CDU_Codigo]' WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Agente'
	UPDATE StdCamposVar SET  Query = 'SELECT * FROM [TDU_Agentes] WITH (NOLOCK) ORDER BY [CDU_Codigo]' WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Agente'

	UPDATE StdCamposVar SET  Query = 'SELECT * FROM [TDU_CompanhiasMaritimas] WITH (NOLOCK) ORDER BY [CDU_Codigo]' WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_CompanhiaMaritima'
	UPDATE StdCamposVar SET  Query = 'SELECT * FROM [TDU_CompanhiasMaritimas] WITH (NOLOCK) ORDER BY [CDU_Codigo]' WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_CompanhiaMaritima'

	UPDATE StdCamposVar SET  Query = 'SELECT [Banco],[Codigo],[Descricao] FROM [Bancos] WITH (NOLOCK) ORDER BY [Banco]' WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Banco'
	UPDATE StdCamposVar SET  Query = 'SELECT [Banco],[Codigo],[Descricao] FROM [Bancos] WITH (NOLOCK) ORDER BY [Banco]' WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Banco'

	UPDATE StdCamposVar SET  Query = 'SELECT * FROM [IntrastatCondEntrega] WITH (NOLOCK) ORDER BY [CondEntrega]' WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_Incoterms'
	UPDATE StdCamposVar SET  Query = 'SELECT * FROM [IntrastatCondEntrega] WITH (NOLOCK) ORDER BY [CondEntrega]' WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_Incoterms'

	UPDATE StdCamposVar SET  Query = 'SELECT [Vendedor], [Nome] FROM [Vendedores] WITH(NOLOCK) ORDER BY [Vendedor]' WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_Vendedor'
	UPDATE StdCamposVar SET  Query = 'SELECT [Vendedor], [Nome] FROM [Vendedores] WITH(NOLOCK) ORDER BY [Vendedor]' WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_Vendedor'

	UPDATE StdCamposVar SET  Query = 'SELECT * FROM [TDU_Certificados] WITH (NOLOCK) ORDER BY [CDU_Codigo]' WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_CertificadoTratado1'
	UPDATE StdCamposVar SET  Query = 'SELECT * FROM [TDU_Certificados] WITH (NOLOCK) ORDER BY [CDU_Codigo]' WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_CertificadoTratado1'
	
	UPDATE StdCamposVar SET  Query = 'SELECT * FROM [TDU_Certificados] WITH (NOLOCK) ORDER BY [CDU_Codigo]' WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_CertificadoTratado2'
	UPDATE StdCamposVar SET  Query = 'SELECT * FROM [TDU_Certificados] WITH (NOLOCK) ORDER BY [CDU_Codigo]' WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_CertificadoTratado2'

	UPDATE StdCamposVar SET  ValorDefeito = NULL WHERE Tabela = 'CabecDoc' AND Campo = 'CDU_NCartaCredito'
	UPDATE StdCamposVar SET  ValorDefeito = NULL WHERE Tabela = 'CabecDocRascunhos' AND Campo = 'CDU_NCartaCredito'

	UPDATE StdCamposVar SET  Query = 'SELECT * FROM [TDU_Certificados] WITH (NOLOCK) ORDER BY [CDU_Codigo]' WHERE Tabela = 'LinhasDoc' AND Campo = 'CDU_CertificadoTratado2'
	UPDATE StdCamposVar SET  Query = 'SELECT * FROM [TDU_Certificados] WITH (NOLOCK) ORDER BY [CDU_Codigo]' WHERE Tabela = 'LinhasDocRascunhos' AND Campo = 'CDU_CertificadoTratado2'
		
END