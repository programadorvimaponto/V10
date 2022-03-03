


-- ------------------------------- 
-- ---    CORRER SO NA FILOPA  ---
-- -------------------------------


/*
-------------------------------------------------------------------------------------------
Data:	    28-05-2019
Autor:	    Gualter Costa
Solução:    VMP Extensibilidade Filopa
Objetivos:  Configura na base de dados de origem (PRIFILOPA) o tipo de documentos de compra que assumem na base de dados de destino após a cópia.
Coment.:    
-------------------------------------------------------------------------------------------
*/


-- Configura a copia documentos de venda da base de dados de Origem do tipo CNT (Contrato)  para documentos de compra na base de dados de destino do tipo CNT (Contrato).
update DocumentosVenda set CDU_TipoDocComprasDestino = 'CNT', CDU_SerieComprasDestino = '2019X'  where Documento like 'CNT'
GO



-- Configura a copia documentos de venda da base de dados de Origem do tipo EMB (Nota de Embarque) para documentos de compra na base de dados de destino do tipo ECF (Encomenda a Fornecedor).
update DocumentosVenda set CDU_TipoDocComprasDestino = 'ECF', CDU_SerieComprasDestino = '2019X'  where Documento like 'EMB'
GO