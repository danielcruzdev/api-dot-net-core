USE [API_TESTE]
GO
/****** Object:  StoredProcedure [dbo].[spiPessoa]    Script Date: 23/10/2020 14:23:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spiPessoa]
  @TabParametrosJSON   VARCHAR(MAX),  
  @Id INT OUTPUT
--with encryption                   
AS 
BEGIN
  --DROP TABLE #TabParametros
  SELECT 
		(SELECT ISNULL(MAX(a.Id),0) + 1 FROM Pessoa a) AS Id,
		 JsonData.Nome								   AS Nome,
         JsonData.Sobrenome							   AS Sobrenome
    INTO #TabParametros
    FROM OPENJSON (@TabParametrosJSON, N'$')
    WITH (Nome       VARCHAR(255)   N'$.Nome',
	      Sobrenome  VARCHAR(255)   N'$.Sobrenome') AS JsonData

  INSERT INTO Pessoa(Id, Nome, Sobrenome)
  SELECT a.Id, a.Nome, a.Sobrenome
    FROM #TabParametros a

  SELECT @Id = a.Id
	 FROM #TabParametros a
END