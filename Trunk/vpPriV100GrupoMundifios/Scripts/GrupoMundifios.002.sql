

-- -----------------------------------------------------------------------------------------------
-- -----           CORRER MANUALMENTE EM TODAS AS BASES DE DADOS DA MUNDIFIOS (N�O FILOPA)   -----
-- -----------------------------------------------------------------------------------------------



/*
-------------------------------------------------------------------------------------------
Data:	    12-06-2019
Autor:	    Gualter Costa
Solu��o:    VMP Extensibilidade Filopa
Objetivos:  Cria��o da tabela TDU_LOCAIS (NAS BASES DE DADOS MUNDIFIOS - EXCEPTO FILOPA)
Coment.:    A valida��o do valores dos bot�es do tipo lista, verifica na bd de destino se o valor seleccionado na lista existe na tabela respectiva na bd destino. 
            (Assim � necess�rio que a tabela TDU_LOCAIS exista na base de dados de destino)
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
			,'Descri��o'
			,'Descri��o'
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



