IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180731202031_initial')
BEGIN
    CREATE TABLE [ContactMe] (
        [ContactId] int NOT NULL IDENTITY,
        [AboutMe1] nvarchar(max) NULL,
        [AboutMe2] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [Facebook] nvarchar(max) NULL,
        [GitHub] nvarchar(max) NULL,
        [LinkedIn] nvarchar(max) NULL,
        [Phone] nvarchar(max) NULL,
        [PictureUrl] nvarchar(max) NULL,
        CONSTRAINT [PK_ContactMe] PRIMARY KEY ([ContactId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180731202031_initial')
BEGIN
    CREATE TABLE [Literatures] (
        [LiteratureID] int NOT NULL IDENTITY,
        [Authors] nvarchar(max) NULL,
        [Title] nvarchar(max) NULL,
        [Url] nvarchar(max) NULL,
        CONSTRAINT [PK_Literatures] PRIMARY KEY ([LiteratureID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180731202031_initial')
BEGIN
    CREATE TABLE [Projects] (
        [ProjectID] int NOT NULL IDENTITY,
        [BackColor] nvarchar(max) NULL,
        [Comments] nvarchar(max) NULL,
        [CompletionDate] datetime2 NOT NULL,
        [GitHubUrl] nvarchar(max) NULL,
        [Name] nvarchar(max) NULL,
        [PictureUrl] nvarchar(max) NULL,
        [WebUrl] nvarchar(max) NULL,
        [WorkLogUrl] nvarchar(max) NULL,
        [YouTubeUrl] nvarchar(max) NULL,
        CONSTRAINT [PK_Projects] PRIMARY KEY ([ProjectID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180731202031_initial')
BEGIN
    CREATE TABLE [Technologies] (
        [TechnologyID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [PictureLink] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Technologies] PRIMARY KEY ([TechnologyID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180731202031_initial')
BEGIN
    CREATE TABLE [LiteraturesTech] (
        [LiteratureID] int NOT NULL,
        [TechnologyID] int NOT NULL,
        CONSTRAINT [PK_LiteraturesTech] PRIMARY KEY ([LiteratureID], [TechnologyID]),
        CONSTRAINT [FK_LiteraturesTech_Literatures_LiteratureID] FOREIGN KEY ([LiteratureID]) REFERENCES [Literatures] ([LiteratureID]) ON DELETE CASCADE,
        CONSTRAINT [FK_LiteraturesTech_Technologies_TechnologyID] FOREIGN KEY ([TechnologyID]) REFERENCES [Technologies] ([TechnologyID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180731202031_initial')
BEGIN
    CREATE TABLE [TechProjects] (
        [TechnologyID] int NOT NULL,
        [ProjectID] int NOT NULL,
        CONSTRAINT [PK_TechProjects] PRIMARY KEY ([TechnologyID], [ProjectID]),
        CONSTRAINT [FK_TechProjects_Projects_ProjectID] FOREIGN KEY ([ProjectID]) REFERENCES [Projects] ([ProjectID]) ON DELETE CASCADE,
        CONSTRAINT [FK_TechProjects_Technologies_TechnologyID] FOREIGN KEY ([TechnologyID]) REFERENCES [Technologies] ([TechnologyID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180731202031_initial')
BEGIN
    CREATE INDEX [IX_LiteraturesTech_TechnologyID] ON [LiteraturesTech] ([TechnologyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180731202031_initial')
BEGIN
    CREATE INDEX [IX_TechProjects_ProjectID] ON [TechProjects] ([ProjectID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180731202031_initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180731202031_initial', N'2.0.3-rtm-10026');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180817080426_Surveys')
BEGIN
    CREATE TABLE [Surveys] (
        [SurveyID] int NOT NULL IDENTITY,
        [Change] nvarchar(max) NULL,
        [Name] nvarchar(max) NULL,
        [Opinion] nvarchar(max) NULL,
        [Time] datetime2 NOT NULL,
        CONSTRAINT [PK_Surveys] PRIMARY KEY ([SurveyID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180817080426_Surveys')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180817080426_Surveys', N'2.0.3-rtm-10026');
END;

GO

