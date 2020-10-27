USE [API_TESTE]
GO
/****** Object:  StoredProcedure [dbo].[spsPessoa]    Script Date: 27/10/2020 11:25:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spsPessoa]
  @Id	INT
--with encryption
AS
BEGIN

   SELECT @Id AS Id
   INTO #TabParametros

   SELECT *
   FROM Pessoa a
		JOIN #TabParametros b ON a.Id = b.Id
   WHERE a.Id = b.Id

END