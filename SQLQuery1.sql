/****** Скрипт для команды SelectTopNRows из среды SSMS  ******/
SELECT TOP (1000) [id]
      ,[seksia]
      ,[name_trener]
      ,[kabinet]
      ,[price]
  FROM [sportik].[dbo].[dss_db]
  select*from dss_db