CREATE PROCEDURE [dbo].[CheckDatabase]
AS


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Aprender') BEGIN
	CREATE TABLE [dbo].[Aprender](
		[IdAprender] [int] IDENTITY(1,1) NOT NULL,
		[IdCliente] [int] NOT NULL,
		[Frase] [varchar](1000) NOT NULL,
		[Aprendido] [bit] NOT NULL,
		CONSTRAINT [PK_Aprender] PRIMARY KEY CLUSTERED 
		(	
			[IdAprender] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Log') BEGIN
	CREATE TABLE [dbo].[Log](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Date] [datetime] NULL,
		[Thread] [varchar](255) NULL,
		[Level] [varchar](50) NULL,
		[Logger] [varchar](255) NULL,
		[Message] [varchar](4000) NULL,
		[Exception] [varchar](2000) NULL
	) ON [PRIMARY] 
END