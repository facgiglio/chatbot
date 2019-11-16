CREATE TABLE [dbo].[Log] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [IdUser]    INT            NULL,
    [Date]      DATETIME       NULL,
    [Thread]    VARCHAR (255)  NULL,
    [Level]     VARCHAR (50)   NULL,
    [Logger]    VARCHAR (255)  NULL,
    [Message]   VARCHAR (4000) NULL,
    [Exception] VARCHAR (2000) NULL
);

