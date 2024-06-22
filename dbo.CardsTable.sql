CREATE TABLE [dbo].[CardsTable] (
    [id]           INT        NOT NULL,
    [number]       NCHAR (16) NULL,
    [dateCard]     NCHAR (4)  NULL,
    [cvc]          NCHAR (3)  NULL,
    [passwordCard] NCHAR (4)  NULL,
    [ownerName]    NCHAR (40) NULL,
    [balance] MONEY NULL, 
    CONSTRAINT [PK_CardsTable] PRIMARY KEY CLUSTERED ([id] ASC)
);

