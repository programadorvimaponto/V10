
-- ---------------------------------------------
-- -- A CORRER SÓ NA BASE DE DADOS DA FILOPA ---
-- ---------------------------------------------



-- ----------------------------------------------------------------------------------------
-- NOTA MUITO IMPORTANTE :                                                               --
-- ----------------------------------------------------------------------------------------
-- DEPOIS DE CORRER MANUALMENTE ESTA SCRIPT                                              --
-- É NECESSÁRIO NO ADMINISTRADOR PRIMAVERA DE AMBAS AS BASES DE DADOS CORRER             --
-- A OPÇÃO DE RECONSTRUÇÃO DAS TABELAS DE UTILIZADOR PARA QUE OS NOVOS CAMPOS SEJAM      --
-- BEM RECONHECIDOS PELO PRIMAVERA E PELOS MOTORES DO PRIMAVERA                          --
-- ----------------------------------------------------------------------------------------    






/*
-------------------------------------------------------------------------------------------
Data:	    16-05-2019
Autor:	    Gualter Costa
Solução:    VMP Extensibilidade Filopa
Objetivos:  Adição de Campos de Utilizador para definicao do tipo de documento e serie a gerar na cópia do documento na base de dados de destino nas tabelas (DocumentosVenda e DocumentosCompra) 
Coment.:    
-------------------------------------------------------------------------------------------
*/



       

-- --------------------------------
-- -- TABELA : DocumentosVenda ----
-- --------------------------------

-- campo : CDU_TipoDocVendasDestino

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'DocumentosVenda' AND COLUMN_NAME = 'CDU_TipoDocVendasDestino')
BEGIN

       ALTER TABLE DocumentosVenda ADD CDU_TipoDocVendasDestino NVARCHAR(5) 
    
	   DECLARE @Var INT                
       SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
       WHERE TABELA = 'DocumentosVenda'

       INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query) 
       VALUES ('DocumentosVenda', 'CDU_TipoDocVendasDestino', 'Doc Vendas Destino ', 'Doc Vendas Destino', 1, @Var + 1, '', NULL)
END
GO


-- campo : CDU_SerieVendasDestino

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'DocumentosVenda' AND COLUMN_NAME = 'CDU_SerieVendasDestino')
BEGIN

       ALTER TABLE DocumentosVenda ADD CDU_SerieVendasDestino NVARCHAR(5) 
    
	   DECLARE @Var INT                
       SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
       WHERE TABELA = 'DocumentosVenda'

       INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query) 
       VALUES ('DocumentosVenda', 'CDU_SerieVendasDestino', 'Serie Vendas Destino', 'Serie Vendas Destino', 1, @Var + 1, '', NULL)
END
GO
         

-- campo : CDU_TipoDocComprasDestino

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'DocumentosVenda' AND COLUMN_NAME = 'CDU_TipoDocComprasDestino')
BEGIN

       ALTER TABLE DocumentosVenda ADD CDU_TipoDocComprasDestino NVARCHAR(5) 
    
	   DECLARE @Var INT                
       SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
       WHERE TABELA = 'DocumentosVenda'

       INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query) 
       VALUES ('DocumentosVenda', 'CDU_TipoDocComprasDestino', 'Doc Compras Destino', 'Doc Compras Destino', 1, @Var + 1, '', NULL)
END
GO



-- campo : CDU_SerieComprasDestino

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'DocumentosVenda' AND COLUMN_NAME = 'CDU_SerieComprasDestino')
BEGIN

       ALTER TABLE DocumentosVenda ADD CDU_SerieComprasDestino NVARCHAR(5) 
    
	   DECLARE @Var INT                
       SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
       WHERE TABELA = 'DocumentosVenda'

       INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query) 
       VALUES ('DocumentosVenda', 'CDU_SerieComprasDestino', 'Serie Compras Destin', 'Serie Compras Destin', 1, @Var + 1, '', NULL)
END
GO



-- ---------------------------------
-- -- TABELA : DocumentosCompra ----
-- ---------------------------------


-- campo : CDU_TipoDocVendasDestino

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'DocumentosCompra' AND COLUMN_NAME = 'CDU_TipoDocVendasDestino')
BEGIN

       ALTER TABLE DocumentosCompra ADD CDU_TipoDocVendasDestino NVARCHAR(5) 
    
	   DECLARE @Var INT                
       SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
       WHERE TABELA = 'DocumentosCompra'

       INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query) 
       VALUES ('DocumentosCompra', 'CDU_TipoDocVendasDestino', 'Doc Vendas Destino', 'Doc Vendas Destino', 1, @Var + 1, '', NULL)
END
GO


-- campo : CDU_SerieVendasDestino

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'DocumentosCompra' AND COLUMN_NAME = 'CDU_SerieVendasDestino')
BEGIN

       ALTER TABLE DocumentosCompra ADD CDU_SerieVendasDestino NVARCHAR(5) 
    
	   DECLARE @Var INT                
       SELECT @Var = COALESCE(MAX(Ordem), 0) FROM [STDCAMPOSVAR]
       WHERE TABELA = 'DocumentosCompra'

       INSERT INTO StdCamposVar (Tabela, Campo, Descricao, Texto, Visivel, Ordem, ValorDefeito, Query) 
       VALUES ('DocumentosCompra', 'CDU_SerieVendasDestino', 'Serie Vendas Destino', 'Serie Vendas Destino', 1, @Var + 1, '', NULL)
END
GO