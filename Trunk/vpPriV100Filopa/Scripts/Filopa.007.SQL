
-----------------------------------------------
---- A CORRER SÓ NA BASE DE DADOS DA FILOPA ---
-----------------------------------------------


-- ----------------------------------------------------------------------------------------------------------------------------------------------------
-- --    TABELA : CLIENTES  -------------------------------------------------------------------------------------------------------------------------------------------
-- ----------------------------------------------------------------------------------------------------------------------------------------------------



/*
-------------------------------------------------------------------------------------------
Data:	    06-05-2019
Autor:	    Gualter Costa
Solução:    VMP Extensibilidade Filopa
Objetivos:  Adição do campo CDU_CodigoFornecedorGrupo na tabela Clientes (da base de dados Filopa)
Coment.:    
-------------------------------------------------------------------------------------------
*/


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'Clientes'
				AND COLUMN_NAME = 'CDU_CodigoFornecedorGrupo'
			)
		BEGIN

			ALTER TABLE Clientes ADD CDU_CodigoFornecedorGrupo NVARCHAR(24) NULL

			DELETE StdCamposVar WHERE Tabela = 'Clientes' AND Campo = 'CDU_CodigoFornecedorGrupo'

			DECLARE @Var INT
			SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
			WHERE TABELA = 'Clientes'
			INSERT INTO StdCamposVar (Tabela, 
									  Campo, 
									  Descricao, 
									  Texto, 
									  Visivel, 
									  Ordem, 
									  ValorDefeito, 
									  Query) 
							  VALUES ('Clientes', 
									  'CDU_CodigoFornecedorGrupo', 
									  'CDU_CodFornGrupo', 
									  'CDU_CodFornGrupo',
									  1, 
									  @Var + 1, 
									  '', 
									  '')
		END
END

GO





/*
-------------------------------------------------------------------------------------------
Data:	    07-05-2019
Autor:	    Gualter Costa
Solução:    VMP Extensibilidade Filopa
Objetivos:  Adição do campo CDU_NomeEmpresaGrupo na tabela Clientes (da base de dados Filopa)
Coment.:    
-------------------------------------------------------------------------------------------
*/



BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'Clientes'
				AND COLUMN_NAME = 'CDU_NomeEmpresaGrupo'
			)
		BEGIN

			ALTER TABLE Clientes ADD CDU_NomeEmpresaGrupo NVARCHAR(12) NULL

			DELETE StdCamposVar WHERE Tabela = 'Clientes' AND Campo = 'CDU_NomeEmpresaGrupo'

			DECLARE @Var INT
			SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
			WHERE TABELA = 'Clientes'
			INSERT INTO StdCamposVar (Tabela, 
									  Campo, 
									  Descricao, 
									  Texto, 
									  Visivel, 
									  Ordem, 
									  ValorDefeito, 
									  Query) 
							  VALUES ('Fornecedores', 
									  'CDU_NomeEmpresaGrupo', 
									  'Nome Emp. Destino', 
									  'Nome Emp. Destino',
									  1, 
									  @Var + 1, 
									  '', 
									  '')
		END
END

GO













-- ----------------------------------------------------------------------------------------------------------------------------------------------------
-- --    TABELA : FORNECEDORES  -----------------------------------------------------------------------------------------------------------------------
-- ----------------------------------------------------------------------------------------------------------------------------------------------------





/*
-------------------------------------------------------------------------------------------
Data:	    06-05-2019
Autor:	    Gualter Costa
Solução:    VMP Extensibilidade Filopa
Objetivos:  Adição do campo CDU_CodigoFornecedorGrupo na tabela Fornecedores (da base de dados Filopa)
Coment.:    
-------------------------------------------------------------------------------------------
*/


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'Fornecedores'
				AND COLUMN_NAME = 'CDU_CodigoFornecedorGrupo'
			)
		BEGIN

			ALTER TABLE Fornecedores ADD CDU_CodigoFornecedorGrupo NVARCHAR(24) NULL

			DELETE StdCamposVar WHERE Tabela = 'Fornecedores' AND Campo = 'CDU_CodigoFornecedorGrupo'

			DECLARE @Var INT
			SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
			WHERE TABELA = 'Fornecedores'
			INSERT INTO StdCamposVar (Tabela, 
									  Campo, 
									  Descricao, 
									  Texto, 
									  Visivel, 
									  Ordem, 
									  ValorDefeito, 
									  Query) 
							  VALUES ('Fornecedores', 
									  'CDU_CodigoFornecedorGrupo', 
									  'CDU_CodFornGrupo', 
									  'CDU_CodFornGrupo',
									  1, 
									  @Var + 1, 
									  '', 
									  '')
		END
END

GO








/*
-------------------------------------------------------------------------------------------
Data:	    07-05-2019
Autor:	    Gualter Costa
Solução:    VMP Extensibilidade Filopa
Objetivos:  Adição do campo CDU_NomeEmpresaGrupo na tabela Fornecedores (da base de dados Filopa)
Coment.:    
-------------------------------------------------------------------------------------------
*/



BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'Fornecedores'
				AND COLUMN_NAME = 'CDU_NomeEmpresaGrupo'
			)
		BEGIN

			ALTER TABLE Fornecedores ADD CDU_NomeEmpresaGrupo NVARCHAR(12) NULL

			DELETE StdCamposVar WHERE Tabela = 'Fornecedores' AND Campo = 'CDU_NomeEmpresaGrupo'

			DECLARE @Var INT
			SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
			WHERE TABELA = 'Fornecedores'
			INSERT INTO StdCamposVar (Tabela, 
									  Campo, 
									  Descricao, 
									  Texto, 
									  Visivel, 
									  Ordem, 
									  ValorDefeito, 
									  Query) 
							  VALUES ('Fornecedores', 
									  'CDU_NomeEmpresaGrupo', 
									  'Nome Emp. Destino', 
									  'Nome Emp. Destino',
									  1, 
									  @Var + 1, 
									  '', 
									  '')
		END
END

GO








/*
-------------------------------------------------------------------------------------------
Data:	    18-06-2019
Autor:	    Gualter Costa
Solução:    VMP Extensibilidade Filopa
Objetivos:  Adição do campo CDU_Banco_Destino na tabela TDU_Bancos (da base de dados Filopa)
Coment.:    
-------------------------------------------------------------------------------------------
*/


BEGIN
	IF NOT EXISTS (
			SELECT 1
			FROM INFORMATION_SCHEMA.COLUMNS
			WHERE TABLE_NAME = 'TDU_Bancos'
				AND COLUMN_NAME = 'CDU_Banco_Destino'
			)
		BEGIN

			ALTER TABLE TDU_Bancos ADD CDU_Banco_Destino NVARCHAR(6) NULL

			DELETE StdCamposVar WHERE Tabela = 'TDU_Bancos' AND Campo = 'CDU_Banco_Destino'

			DECLARE @Var INT
			SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
			WHERE TABELA = 'TDU_Bancos'
			INSERT INTO StdCamposVar (Tabela, 
									  Campo, 
									  Descricao, 
									  Texto, 
									  Visivel, 
									  Ordem, 
									  ValorDefeito, 
									  Query) 
							  VALUES ('TDU_Bancos', 
									  'CDU_Banco_Destino', 
									  'Banco Destino', 
									  'Banco Destino',
									  1, 
									  @Var + 1, 
									  null, 
									  '')
		END
END

GO