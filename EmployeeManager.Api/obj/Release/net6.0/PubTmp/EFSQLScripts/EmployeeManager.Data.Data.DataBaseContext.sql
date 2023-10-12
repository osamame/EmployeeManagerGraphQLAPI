IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230926005218_MigrateandSeedEmployeeManager')
BEGIN
    CREATE TABLE [Departments] (
        [Id] bigint NOT NULL IDENTITY,
        [DepartmentName] nvarchar(max) NOT NULL,
        [DepartmentLocation] nvarchar(max) NOT NULL,
        [IsDeleted] bit NOT NULL,
        [DateCreated] datetime2 NOT NULL,
        CONSTRAINT [PK_Departments] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230926005218_MigrateandSeedEmployeeManager')
BEGIN
    CREATE TABLE [Salaries] (
        [Id] bigint NOT NULL IDENTITY,
        [Grade] int NOT NULL,
        [MinimumSalary] decimal(18,2) NOT NULL,
        [MaximumSalary] decimal(18,2) NOT NULL,
        [IsDeleted] bit NOT NULL,
        [DateCreated] datetime2 NOT NULL,
        CONSTRAINT [PK_Salaries] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230926005218_MigrateandSeedEmployeeManager')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DateCreated', N'DepartmentLocation', N'DepartmentName', N'IsDeleted') AND [object_id] = OBJECT_ID(N'[Departments]'))
        SET IDENTITY_INSERT [Departments] ON;
    EXEC(N'INSERT INTO [Departments] ([Id], [DateCreated], [DepartmentLocation], [DepartmentName], [IsDeleted])
    VALUES (CAST(1 AS bigint), ''2023-09-26T01:52:18.5297932+01:00'', N''Abuja'', N''Human Resource'', CAST(0 AS bit)),
    (CAST(2 AS bigint), ''2023-09-26T01:52:18.5297940+01:00'', N''Abuja'', N''Admin Department'', CAST(0 AS bit)),
    (CAST(3 AS bigint), ''2023-09-26T01:52:18.5297944+01:00'', N''Lagos'', N''Technical Department'', CAST(0 AS bit))');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DateCreated', N'DepartmentLocation', N'DepartmentName', N'IsDeleted') AND [object_id] = OBJECT_ID(N'[Departments]'))
        SET IDENTITY_INSERT [Departments] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230926005218_MigrateandSeedEmployeeManager')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DateCreated', N'Grade', N'IsDeleted', N'MaximumSalary', N'MinimumSalary') AND [object_id] = OBJECT_ID(N'[Salaries]'))
        SET IDENTITY_INSERT [Salaries] ON;
    EXEC(N'INSERT INTO [Salaries] ([Id], [DateCreated], [Grade], [IsDeleted], [MaximumSalary], [MinimumSalary])
    VALUES (CAST(1 AS bigint), ''2023-09-26T01:52:18.5297375+01:00'', 1, CAST(0 AS bit), 2000000000.99, 1000000000.99),
    (CAST(2 AS bigint), ''2023-09-26T01:52:18.5297383+01:00'', 2, CAST(0 AS bit), 3000000000.99, 2000000000.99),
    (CAST(3 AS bigint), ''2023-09-26T01:52:18.5297386+01:00'', 3, CAST(0 AS bit), 4000000000.99, 3000000000.99)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DateCreated', N'Grade', N'IsDeleted', N'MaximumSalary', N'MinimumSalary') AND [object_id] = OBJECT_ID(N'[Salaries]'))
        SET IDENTITY_INSERT [Salaries] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230926005218_MigrateandSeedEmployeeManager')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230926005218_MigrateandSeedEmployeeManager', N'7.0.11');
END;
GO

COMMIT;
GO

