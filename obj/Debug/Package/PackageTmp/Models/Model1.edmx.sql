
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 11/03/2012 20:34:38
-- Generated from EDMX file: D:\Site Soft-School\SoftSchool\MvcApplication1\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DBSoftSchool];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dresLycees]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lycees] DROP CONSTRAINT [FK_dresLycees];
GO
IF OBJECT_ID(N'[dbo].[FK_gouvernoratsVilles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Villes] DROP CONSTRAINT [FK_gouvernoratsVilles];
GO
IF OBJECT_ID(N'[dbo].[FK_LogicielsHistorique]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Historiques] DROP CONSTRAINT [FK_LogicielsHistorique];
GO
IF OBJECT_ID(N'[dbo].[FK_Lycees_FK02]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lycees] DROP CONSTRAINT [FK_Lycees_FK02];
GO
IF OBJECT_ID(N'[dbo].[FK_LyceesUtilisateurs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Utilisateurs] DROP CONSTRAINT [FK_LyceesUtilisateurs];
GO
IF OBJECT_ID(N'[dbo].[FK_UtilisateursHistorique]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Historiques] DROP CONSTRAINT [FK_UtilisateursHistorique];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[dres]', 'U') IS NOT NULL
    DROP TABLE [dbo].[dres];
GO
IF OBJECT_ID(N'[dbo].[gouvernorats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[gouvernorats];
GO
IF OBJECT_ID(N'[dbo].[Historiques]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Historiques];
GO
IF OBJECT_ID(N'[dbo].[Logiciels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Logiciels];
GO
IF OBJECT_ID(N'[dbo].[Lycees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Lycees];
GO
IF OBJECT_ID(N'[dbo].[Utilisateurs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Utilisateurs];
GO
IF OBJECT_ID(N'[dbo].[Villes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Villes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'dres'
CREATE TABLE [dbo].[dres] (
    [Nom_Ar] nvarchar(70)  NULL,
    [Nom_Fr] nvarchar(70)  NULL,
    [GovernoratID] int  NOT NULL,
    [DreId] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'gouvernorats'
CREATE TABLE [dbo].[gouvernorats] (
    [Nom_Ar] nvarchar(20)  NULL,
    [Nom_Fr] nvarchar(20)  NULL,
    [GouvernoratID] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Historiques'
CREATE TABLE [dbo].[Historiques] (
    [HistoriqueId] int IDENTITY(1,1) NOT NULL,
    [Date] nvarchar(max)  NOT NULL,
    [LogicielID] int  NOT NULL,
    [UtilisateurID] int  NOT NULL
);
GO

-- Creating table 'Logiciels'
CREATE TABLE [dbo].[Logiciels] (
    [LogicielID] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(max)  NOT NULL,
    [Version] nvarchar(max)  NOT NULL,
    [Image] nvarchar(max)  NOT NULL,
    [Descreption] nvarchar(max)  NOT NULL,
    [Prix] nvarchar(max)  NOT NULL,
    [Lien] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Lycees'
CREATE TABLE [dbo].[Lycees] (
    [Code] nvarchar(6)  NOT NULL,
    [Nom] nvarchar(70)  NULL,
    [GovernoratId] int  NOT NULL,
    [VilleId] int  NOT NULL,
    [DreId] int  NOT NULL,
    [LyceeID] int  NOT NULL
);
GO

-- Creating table 'Utilisateurs'
CREATE TABLE [dbo].[Utilisateurs] (
    [UtilisateurId] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(max)  NOT NULL,
    [Prenom] nvarchar(max)  NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [LyceeID] int  NOT NULL
);
GO

-- Creating table 'Villes'
CREATE TABLE [dbo].[Villes] (
    [Nom_Ar] nvarchar(30)  NOT NULL,
    [Nom_Fr] nvarchar(30)  NULL,
    [DreID] int  NOT NULL,
    [VilleID] int IDENTITY(1,1) NOT NULL
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

-- Creating primary key on [HistoriqueId] in table 'Historiques'
ALTER TABLE [dbo].[Historiques]
ADD CONSTRAINT [PK_Historiques]
    PRIMARY KEY CLUSTERED ([HistoriqueId] ASC);
GO

-- Creating primary key on [LogicielID] in table 'Logiciels'
ALTER TABLE [dbo].[Logiciels]
ADD CONSTRAINT [PK_Logiciels]
    PRIMARY KEY CLUSTERED ([LogicielID] ASC);
GO

-- Creating primary key on [LyceeID] in table 'Lycees'
ALTER TABLE [dbo].[Lycees]
ADD CONSTRAINT [PK_Lycees]
    PRIMARY KEY CLUSTERED ([LyceeID] ASC);
GO

-- Creating primary key on [UtilisateurId] in table 'Utilisateurs'
ALTER TABLE [dbo].[Utilisateurs]
ADD CONSTRAINT [PK_Utilisateurs]
    PRIMARY KEY CLUSTERED ([UtilisateurId] ASC);
GO

-- Creating primary key on [VilleID] in table 'Villes'
ALTER TABLE [dbo].[Villes]
ADD CONSTRAINT [PK_Villes]
    PRIMARY KEY CLUSTERED ([VilleID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LogicielID] in table 'Historiques'
ALTER TABLE [dbo].[Historiques]
ADD CONSTRAINT [FK_LogicielsHistorique]
    FOREIGN KEY ([LogicielID])
    REFERENCES [dbo].[Logiciels]
        ([LogicielID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LogicielsHistorique'
CREATE INDEX [IX_FK_LogicielsHistorique]
ON [dbo].[Historiques]
    ([LogicielID]);
GO

-- Creating foreign key on [UtilisateurID] in table 'Historiques'
ALTER TABLE [dbo].[Historiques]
ADD CONSTRAINT [FK_UtilisateursHistorique]
    FOREIGN KEY ([UtilisateurID])
    REFERENCES [dbo].[Utilisateurs]
        ([UtilisateurId])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UtilisateursHistorique'
CREATE INDEX [IX_FK_UtilisateursHistorique]
ON [dbo].[Historiques]
    ([UtilisateurID]);
GO

-- Creating foreign key on [VilleId] in table 'Lycees'
ALTER TABLE [dbo].[Lycees]
ADD CONSTRAINT [FK_Lycees_FK02]
    FOREIGN KEY ([VilleId])
    REFERENCES [dbo].[Villes]
        ([VilleID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Lycees_FK02'
CREATE INDEX [IX_FK_Lycees_FK02]
ON [dbo].[Lycees]
    ([VilleId]);
GO

-- Creating foreign key on [LyceeID] in table 'Utilisateurs'
ALTER TABLE [dbo].[Utilisateurs]
ADD CONSTRAINT [FK_LyceesUtilisateurs]
    FOREIGN KEY ([LyceeID])
    REFERENCES [dbo].[Lycees]
        ([LyceeID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LyceesUtilisateurs'
CREATE INDEX [IX_FK_LyceesUtilisateurs]
ON [dbo].[Utilisateurs]
    ([LyceeID]);
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

-- Creating foreign key on [DreID] in table 'Villes'
ALTER TABLE [dbo].[Villes]
ADD CONSTRAINT [FK_dresVilles]
    FOREIGN KEY ([DreID])
    REFERENCES [dbo].[dres]
        ([DreId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_dresVilles'
CREATE INDEX [IX_FK_dresVilles]
ON [dbo].[Villes]
    ([DreID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------