




--  bu script'e github linkinden ulaşabilirsiniz. Aşağıya bırakacağım linki, master altında çalıştırıp db yi oluşturmaya yarıyor.


USE [master]
GO

SET NOCOUNT ON
GO

IF EXISTS (SELECT 1 FROM sys.databases WHERE [Name] = 'DapperUow')
BEGIN
	ALTER DATABASE DapperUow SET SINGLE_USER
	DROP DATABASE DapperUow
END

CREATE DATABASE DapperUow
GO

IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE [name] = 'TestUser')
BEGIN
	CREATE LOGIN [TestUser] WITH PASSWORD = N'Password123', DEFAULT_DATABASE = [DapperUow],
		DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION = OFF, CHECK_POLICY = OFF
	
	ALTER LOGIN [TestUser] ENABLE
END
GO

USE [DapperUow]
GO

CREATE USER [TestUser] FOR LOGIN [TestUser]
GO

EXEC sp_addrolemember N'db_datareader', N'TestUser'
EXEC sp_addrolemember N'db_datawriter', N'TestUser'
GO

USE [DapperUow]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2/22/2015 10:09:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CategoryId] [int] NULL,
	[Price] [decimal](18, 0) NULL,
	[weight] [float] NULL,
	[height] [float] NULL,
 CONSTRAINT [PK_Id] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UX_Product] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



/****** Object:  Table [dbo].[Category]    Script Date: 2/22/2015 10:09:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL, 
	[Name] [nvarchar](200) NOT NULL, 
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO