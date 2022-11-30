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

INSERT INTO[dbo].[Matches](Date,Team1,Team2,Winner) VALUES ('09/20/2022',1,2,1)

DECLARE @Team1 INT = 1
DECLARE @Team2 INT = 2

select sum(team1wins) as Team1, sum(team2wins) as Team2
from ((select team1, team2,
               (case when winner = @Team1 then 1 else 0 end) as team1wins,
			   (case when winner = @Team2 then 1 else 0 end) as team2wins
        from Matches where (team1 =@Team1 and team2 = @Team2 ) OR(team1 =@Team2 and team2 = @Team1)
       )
      ) t