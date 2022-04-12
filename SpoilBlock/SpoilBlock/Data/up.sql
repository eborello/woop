CREATE TABLE [WOOPUser] (
    [ID] int PRIMARY KEY IDENTITY(1,1),

    [ASPNetIdentityId] NVARCHAR(450) NOT NULL,

    [Username] nvarchar(30) NOT NULL
);

CREATE TABLE [Media] (
    [ID] int PRIMARY KEY IDENTITY(1,1),
    [IMDBID] nvarchar(50) NOT NULL,
    [Title] nvarchar(50) NOT NULL,
    [Description] nvarchar(400) NOT NULL,
    [Image] nvarchar(200) NOT NULL
);

CREATE TABLE [WOOPUserMedia] (
    [ID] int PRIMARY KEY IDENTITY(1,1),
    [BlockageLevel] int NOT NULL,
    [UserID] int NOT NULL,
    [MediaID] int NOT NULL
);

ALTER TABLE [Media] ADD CONSTRAINT [Unique_IMDBID]
     UNIQUE (IMDBID);

ALTER TABLE [WOOPUserMedia] ADD CONSTRAINT [Fk_WOOPUserMedia_User_ID]
    FOREIGN KEY ([UserID]) REFERENCES [WOOPUser] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [WOOPUserMedia] ADD CONSTRAINT [Fk_WOOPUserMedia_Media_ID]
    FOREIGN KEY ([MediaID]) REFERENCES [Media] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;