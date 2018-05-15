ALTER ROLE [db_owner] ADD MEMBER [webadmin]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [webadmin]
GO
ALTER ROLE [db_datareader] ADD MEMBER [webadmin]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [webadmin]