
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/03/2012 10:14:08
-- Generated from EDMX file: D:\Site Soft-School\SoftSchool\MvcApplication1\Models\Model4.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'dres'
CREATE TABLE [dbo].[dres] (
    [Nom_Ar] longtext  NULL,
    [Nom_Fr] longtext  NULL,
    [GovernoratID] int  NOT NULL,
    [DreId] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'gouvernorats'
CREATE TABLE [dbo].[gouvernorats] (
    [Nom_Ar] longtext  NULL,
    [Nom_Fr] longtext  NULL,
    [GouvernoratID] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'historiques'
CREATE TABLE [dbo].[historiques] (
    [HistoriqueId] int IDENTITY(1,1) NOT NULL,
    [Date] longtext  NOT NULL,
    [LogicielID] int  NOT NULL,
    [UtilisateurID] int  NOT NULL,
    [date_demande] datetime  NOT NULL,
    [date_reponse] datetime  NOT NULL
);
GO

-- Creating table 'logiciels'
CREATE TABLE [dbo].[logiciels] (
    [LogicielID] int IDENTITY(1,1) NOT NULL,
    [Nom] longtext  NOT NULL,
    [Version] longtext  NOT NULL,
    [Image] longtext  NOT NULL,
    [Descreption] longtext  NOT NULL,
    [Prix] longtext  NOT NULL,
    [Lien] longtext  NOT NULL
);
GO

-- Creating table 'lycees'
CREATE TABLE [dbo].[lycees] (
    [Code] longtext  NOT NULL,
    [Nom] longtext  NULL,
    [VilleId] int  NOT NULL,
    [LyceeID] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'my_aspnet_applications'
CREATE TABLE [dbo].[my_aspnet_applications] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] longtext  NULL,
    [description] longtext  NULL
);
GO

-- Creating table 'my_aspnet_membership'
CREATE TABLE [dbo].[my_aspnet_membership] (
    [userId] int  NOT NULL,
    [Email] longtext  NULL,
    [Comment] longtext  NULL,
    [Password] longtext  NOT NULL,
    [PasswordKey] longtext  NULL,
    [PasswordFormat] tinyint  NULL,
    [PasswordQuestion] longtext  NULL,
    [PasswordAnswer] longtext  NULL,
    [IsApproved] bool  NULL,
    [LastActivityDate] datetime  NULL,
    [LastLoginDate] datetime  NULL,
    [LastPasswordChangedDate] datetime  NULL,
    [CreationDate] datetime  NULL,
    [IsLockedOut] bool  NULL,
    [LastLockedOutDate] datetime  NULL,
    [FailedPasswordAttemptCount] bigint  NULL,
    [FailedPasswordAttemptWindowStart] datetime  NULL,
    [FailedPasswordAnswerAttemptCount] bigint  NULL,
    [FailedPasswordAnswerAttemptWindowStart] datetime  NULL
);
GO

-- Creating table 'my_aspnet_profiles'
CREATE TABLE [dbo].[my_aspnet_profiles] (
    [userId] int  NOT NULL,
    [valueindex] longtext  NULL,
    [stringdata] longtext  NULL,
    [binarydata] varbinary(100)  NULL,
    [lastUpdatedDate] datetime  NOT NULL
);
GO

-- Creating table 'my_aspnet_roles'
CREATE TABLE [dbo].[my_aspnet_roles] (
    [id] int IDENTITY(1,1) NOT NULL,
    [applicationId] int  NOT NULL,
    [name] longtext  NOT NULL
);
GO

-- Creating table 'my_aspnet_sessioncleanup'
CREATE TABLE [dbo].[my_aspnet_sessioncleanup] (
    [LastRun] datetime  NOT NULL,
    [IntervalMinutes] int  NOT NULL
);
GO

-- Creating table 'my_aspnet_sessions'
CREATE TABLE [dbo].[my_aspnet_sessions] (
    [SessionId] longtext  NOT NULL,
    [ApplicationId] int  NOT NULL,
    [Created] datetime  NOT NULL,
    [Expires] datetime  NOT NULL,
    [LockDate] datetime  NOT NULL,
    [LockId] int  NOT NULL,
    [Timeout] int  NOT NULL,
    [Locked] bool  NOT NULL,
    [SessionItems] varbinary(100)  NULL,
    [Flags] int  NOT NULL
);
GO

-- Creating table 'my_aspnet_users'
CREATE TABLE [dbo].[my_aspnet_users] (
    [id] int IDENTITY(1,1) NOT NULL,
    [applicationId] int  NOT NULL,
    [name] longtext  NOT NULL,
    [isAnonymous] bool  NOT NULL,
    [lastActivityDate] datetime  NULL
);
GO

-- Creating table 'my_aspnet_usersinroles'
CREATE TABLE [dbo].[my_aspnet_usersinroles] (
    [userId] int  NOT NULL,
    [roleId] int  NOT NULL
);
GO

-- Creating table 'utilisateur'
CREATE TABLE [dbo].[utilisateur] (
    [IDuser] bigint IDENTITY(1,1) NOT NULL,
    [aspnet_user] bigint  NOT NULL,
    [Nom] longtext  NOT NULL,
    [Prenom] longtext  NOT NULL,
    [IDLycees] int  NOT NULL
);
GO

-- Creating table 'villes'
CREATE TABLE [dbo].[villes] (
    [Nom_Ar] longtext  NOT NULL,
    [Nom_Fr] longtext  NULL,
    [DreID] int  NOT NULL,
    [VilleID] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'activation'
CREATE TABLE [dbo].[activation] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [LogicelId] int  NOT NULL,
    [Cle_Demander] longtext  NULL,
    [Cle_Reponse] longtext  NULL,
    [date_demande] datetime  NULL,
    [date_reponse] datetime  NULL,
    [IDuser] bigint  NOT NULL
);
GO

-- Creating table 'inbox'
CREATE TABLE [dbo].[inbox] (
    [ID] bigint IDENTITY(1,1) NOT NULL,
    [Contact] bigint  NULL,
    [subject] longtext  NULL,
    [objet] longtext  NULL,
    [LogicielID] bigint  NULL,
    [cle] longtext  NULL,
    [Reponse] bool  NULL,
    [UserId] bigint  NOT NULL,
    [file] longtext  NULL,
    [date_envoie] datetime  NULL
);
GO

-- Creating table 'outbox'
CREATE TABLE [dbo].[outbox] (
    [id] bigint IDENTITY(1,1) NOT NULL,
    [contact] bigint  NULL,
    [subject] longtext  NULL,
    [objet] longtext  NULL,
    [cle] longtext  NULL,
    [file] longtext  NULL,
    [UserId] bigint  NOT NULL,
    [date_envoie] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [DreId] in table 'dres'
ALTER TABLE [dbo].[dres]
ADD CONSTRAINT [PK_dres]
    PRIMARY KEY CLUSTERED ([DreId] ASC);
GO

-- Creating primary key on [GouvernoratID] in table 'gouvernorats'
ALTER TABLE [dbo].[gouvernorats]
ADD CONSTRAINT [PK_gouvernorats]
    PRIMARY KEY CLUSTERED ([GouvernoratID] ASC);
GO

-- Creating primary key on [HistoriqueId] in table 'historiques'
ALTER TABLE [dbo].[historiques]
ADD CONSTRAINT [PK_historiques]
    PRIMARY KEY CLUSTERED ([HistoriqueId] ASC);
GO

-- Creating primary key on [LogicielID] in table 'logiciels'
ALTER TABLE [dbo].[logiciels]
ADD CONSTRAINT [PK_logiciels]
    PRIMARY KEY CLUSTERED ([LogicielID] ASC);
GO

-- Creating primary key on [LyceeID] in table 'lycees'
ALTER TABLE [dbo].[lycees]
ADD CONSTRAINT [PK_lycees]
    PRIMARY KEY CLUSTERED ([LyceeID] ASC);
GO

-- Creating primary key on [id] in table 'my_aspnet_applications'
ALTER TABLE [dbo].[my_aspnet_applications]
ADD CONSTRAINT [PK_my_aspnet_applications]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [userId] in table 'my_aspnet_membership'
ALTER TABLE [dbo].[my_aspnet_membership]
ADD CONSTRAINT [PK_my_aspnet_membership]
    PRIMARY KEY CLUSTERED ([userId] ASC);
GO

-- Creating primary key on [userId] in table 'my_aspnet_profiles'
ALTER TABLE [dbo].[my_aspnet_profiles]
ADD CONSTRAINT [PK_my_aspnet_profiles]
    PRIMARY KEY CLUSTERED ([userId] ASC);
GO

-- Creating primary key on [id] in table 'my_aspnet_roles'
ALTER TABLE [dbo].[my_aspnet_roles]
ADD CONSTRAINT [PK_my_aspnet_roles]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [LastRun], [IntervalMinutes] in table 'my_aspnet_sessioncleanup'
ALTER TABLE [dbo].[my_aspnet_sessioncleanup]
ADD CONSTRAINT [PK_my_aspnet_sessioncleanup]
    PRIMARY KEY CLUSTERED ([LastRun], [IntervalMinutes] ASC);
GO

-- Creating primary key on [SessionId], [ApplicationId] in table 'my_aspnet_sessions'
ALTER TABLE [dbo].[my_aspnet_sessions]
ADD CONSTRAINT [PK_my_aspnet_sessions]
    PRIMARY KEY CLUSTERED ([SessionId], [ApplicationId] ASC);
GO

-- Creating primary key on [id] in table 'my_aspnet_users'
ALTER TABLE [dbo].[my_aspnet_users]
ADD CONSTRAINT [PK_my_aspnet_users]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [userId], [roleId] in table 'my_aspnet_usersinroles'
ALTER TABLE [dbo].[my_aspnet_usersinroles]
ADD CONSTRAINT [PK_my_aspnet_usersinroles]
    PRIMARY KEY CLUSTERED ([userId], [roleId] ASC);
GO

-- Creating primary key on [IDuser] in table 'utilisateur'
ALTER TABLE [dbo].[utilisateur]
ADD CONSTRAINT [PK_utilisateur]
    PRIMARY KEY CLUSTERED ([IDuser] ASC);
GO

-- Creating primary key on [VilleID] in table 'villes'
ALTER TABLE [dbo].[villes]
ADD CONSTRAINT [PK_villes]
    PRIMARY KEY CLUSTERED ([VilleID] ASC);
GO

-- Creating primary key on [Id] in table 'activation'
ALTER TABLE [dbo].[activation]
ADD CONSTRAINT [PK_activation]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'inbox'
ALTER TABLE [dbo].[inbox]
ADD CONSTRAINT [PK_inbox]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [id] in table 'outbox'
ALTER TABLE [dbo].[outbox]
ADD CONSTRAINT [PK_outbox]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DreID] in table 'villes'
ALTER TABLE [dbo].[villes]
ADD CONSTRAINT [FK_dresVilles]
    FOREIGN KEY ([DreID])
    REFERENCES [dbo].[dres]
        ([DreId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_dresVilles'
CREATE INDEX [IX_FK_dresVilles]
ON [dbo].[villes]
    ([DreID]);
GO

-- Creating foreign key on [GovernoratID] in table 'dres'
ALTER TABLE [dbo].[dres]
ADD CONSTRAINT [FK_gouvernoratsdres]
    FOREIGN KEY ([GovernoratID])
    REFERENCES [dbo].[gouvernorats]
        ([GouvernoratID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_gouvernoratsdres'
CREATE INDEX [IX_FK_gouvernoratsdres]
ON [dbo].[dres]
    ([GovernoratID]);
GO

-- Creating foreign key on [VilleId] in table 'lycees'
ALTER TABLE [dbo].[lycees]
ADD CONSTRAINT [FK_Lycees_FK02]
    FOREIGN KEY ([VilleId])
    REFERENCES [dbo].[villes]
        ([VilleID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Lycees_FK02'
CREATE INDEX [IX_FK_Lycees_FK02]
ON [dbo].[lycees]
    ([VilleId]);
GO

-- Creating foreign key on [LogicelId] in table 'activation'
ALTER TABLE [dbo].[activation]
ADD CONSTRAINT [FK_logicielsactivation]
    FOREIGN KEY ([LogicelId])
    REFERENCES [dbo].[logiciels]
        ([LogicielID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_logicielsactivation'
CREATE INDEX [IX_FK_logicielsactivation]
ON [dbo].[activation]
    ([LogicelId]);
GO

-- Creating foreign key on [UserId] in table 'inbox'
ALTER TABLE [dbo].[inbox]
ADD CONSTRAINT [FK_utilisateurinbox]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[utilisateur]
        ([IDuser])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_utilisateurinbox'
CREATE INDEX [IX_FK_utilisateurinbox]
ON [dbo].[inbox]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'outbox'
ALTER TABLE [dbo].[outbox]
ADD CONSTRAINT [FK_utilisateuroutbox]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[utilisateur]
        ([IDuser])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_utilisateuroutbox'
CREATE INDEX [IX_FK_utilisateuroutbox]
ON [dbo].[outbox]
    ([UserId]);
GO

-- Creating foreign key on [IDuser] in table 'activation'
ALTER TABLE [dbo].[activation]
ADD CONSTRAINT [FK_utilisateuractivation]
    FOREIGN KEY ([IDuser])
    REFERENCES [dbo].[utilisateur]
        ([IDuser])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_utilisateuractivation'
CREATE INDEX [IX_FK_utilisateuractivation]
ON [dbo].[activation]
    ([IDuser]);
GO

-- Creating foreign key on [IDLycees] in table 'utilisateur'
ALTER TABLE [dbo].[utilisateur]
ADD CONSTRAINT [FK_lyceesutilisateur]
    FOREIGN KEY ([IDLycees])
    REFERENCES [dbo].[lycees]
        ([LyceeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_lyceesutilisateur'
CREATE INDEX [IX_FK_lyceesutilisateur]
ON [dbo].[utilisateur]
    ([IDLycees]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------