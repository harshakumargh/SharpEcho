CREATE TABLE [dbo].[Team]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL
)

GO

CREATE UNIQUE INDEX [IX_Team_Name] ON [dbo].[Team] ([Name])

GO

CREATE TABLE [dbo].[Matches]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Date] DATE NOT NULL,
    [Team1] BIGINT FOREIGN KEY REFERENCES [dbo].[Team]([Id]),
    [Team2] BIGINT FOREIGN KEY REFERENCES [dbo].[Team]([Id]),
    [Winner] BIGINT FOREIGN KEY REFERENCES [dbo].[Team]([Id]),
    [CreatedBy] NVARCHAR(50) DEFAULT 'System',
    [CreatedOn] Date DEFAULT GETDATE(),
    [ModifiedBy] NVARCHAR(50) DEFAULT 'System',
    [ModifiedOn] Date DEFAULT GETDATE()
)
