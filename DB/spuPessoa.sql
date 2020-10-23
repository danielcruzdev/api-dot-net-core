ALTER PROCEDURE [dbo].[spuPessoa]
  @TabParametrosJSON   VARCHAR(MAX),
  @Id				   INT OUTPUT
--with encryption                   
AS 
BEGIN
  --DROP TABLE #TabParametros
  SELECT JsonData.Id			AS Id,
		 JsonData.Nome			AS Nome,
         JsonData.Sobrenome		AS Sobrenome
    INTO #TabParametros
    FROM OPENJSON (@TabParametrosJSON, N'$')
    WITH (Id         INT            N'$.Id',
		  Nome       VARCHAR(255)   N'$.Nome',
	      Sobrenome  VARCHAR(255)   N'$.Sobrenome') AS JsonData

  UPDATE a
     SET Nome      = b.Nome,
		 Sobrenome = b.Sobrenome
    FROM Pessoa  a
	     JOIN #TabParametros b ON a.Id = b.Id

  SELECT @Id = a.Id
	 FROM #TabParametros a
END