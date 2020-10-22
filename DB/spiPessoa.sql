CREATE PROCEDURE [dbo].[spiPessoa]
  @Nome			VARCHAR(255),
  @Sobrenome    VARCHAR(255)
--with encryption
AS
BEGIN
   SELECT @Nome		 AS Nome,
		  @Sobrenome AS Sobrenome
     INTO #TabParametros
 
  INSERT INTO Pessoa(Nome, Sobrenome)
  SELECT a.Nome, a.Sobrenome
    FROM #TabParametros a
END
