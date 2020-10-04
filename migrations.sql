IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE TABLE [Bank] (
        [BankId] bigint NOT NULL IDENTITY,
        [AccountName] nvarchar(60) NOT NULL,
        [AccountNumber] nvarchar(18) NULL,
        [Description] nvarchar(220) NULL,
        [BankAccountType] int NOT NULL,
        [InitialAmount] decimal(18,2) NOT NULL,
        [Date] datetime2 NOT NULL,
        CONSTRAINT [PK_Bank] PRIMARY KEY ([BankId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE TABLE [Country] (
        [CountryId] int NOT NULL IDENTITY,
        [Name] nvarchar(120) NOT NULL,
        [IconImage] nvarchar(800) NULL,
        CONSTRAINT [PK_Country] PRIMARY KEY ([CountryId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE TABLE [Currency] (
        [CurrencyId] int NOT NULL IDENTITY,
        [Name] nvarchar(120) NOT NULL,
        CONSTRAINT [PK_Currency] PRIMARY KEY ([CurrencyId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE TABLE [DocumentNumberSequence] (
        [DocumentNumberSequenceId] bigint NOT NULL IDENTITY,
        [DocumentType] int NOT NULL,
        [Name] nvarchar(60) NOT NULL,
        [Automatic] bit NOT NULL,
        [Prefix] nvarchar(20) NOT NULL,
        [Sequence] bigint NULL,
        [FinalSequence] bigint NOT NULL,
        [ValidFrom] datetime2 NOT NULL,
        [ValidTo] datetime2 NOT NULL,
        [IsDefault] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_DocumentNumberSequence] PRIMARY KEY ([DocumentNumberSequenceId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE TABLE [PaymentDeadline] (
        [PaymentDeadlineId] bigint NOT NULL IDENTITY,
        [Description] nvarchar(120) NOT NULL,
        [DayValue] int NOT NULL,
        CONSTRAINT [PK_PaymentDeadline] PRIMARY KEY ([PaymentDeadlineId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE TABLE [Tax] (
        [TaxId] bigint NOT NULL IDENTITY,
        [Name] nvarchar(60) NOT NULL,
        [PercentValue] decimal(12,2) NOT NULL,
        CONSTRAINT [PK_Tax] PRIMARY KEY ([TaxId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE TABLE [TaxRegimeType] (
        [TaxRegimeTypeId] bigint NOT NULL IDENTITY,
        [Name] nvarchar(60) NOT NULL,
        CONSTRAINT [PK_TaxRegimeType] PRIMARY KEY ([TaxRegimeTypeId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE TABLE [Werehouse] (
        [WerehouseId] bigint NOT NULL IDENTITY,
        [Code] nvarchar(20) NOT NULL,
        [Name] nvarchar(60) NOT NULL,
        [Location] nvarchar(60) NOT NULL,
        CONSTRAINT [PK_Werehouse] PRIMARY KEY ([WerehouseId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE TABLE [Client] (
        [ClientId] bigint NOT NULL IDENTITY,
        [Code] nvarchar(20) NOT NULL,
        [Name] nvarchar(60) NOT NULL,
        [Email] nvarchar(60) NOT NULL,
        [Phone] nvarchar(10) NOT NULL,
        [Rnc] nvarchar(15) NULL,
        [HomeOrApartment] nvarchar(200) NULL,
        [City] nvarchar(60) NULL,
        [Street] nvarchar(60) NULL,
        [CountryId] int NULL,
        CONSTRAINT [PK_Client] PRIMARY KEY ([ClientId]),
        CONSTRAINT [FK_Client_Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Country] ([CountryId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE TABLE [Supplier] (
        [SupplierId] bigint NOT NULL IDENTITY,
        [Code] nvarchar(20) NOT NULL,
        [Name] nvarchar(60) NOT NULL,
        [Email] nvarchar(60) NOT NULL,
        [Phone] nvarchar(10) NOT NULL,
        [Rnc] nvarchar(15) NULL,
        [HomeOrApartment] nvarchar(200) NULL,
        [City] nvarchar(60) NULL,
        [Street] nvarchar(60) NULL,
        [CountryId] int NULL,
        [IsIndependent] bit NOT NULL DEFAULT (default(0)),
        CONSTRAINT [PK_Supplier] PRIMARY KEY ([SupplierId]),
        CONSTRAINT [FK_Supplier_Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Country] ([CountryId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE TABLE [User] (
        [UserId] bigint NOT NULL IDENTITY,
        [Fullname] nvarchar(200) NOT NULL,
        [Email] nvarchar(30) NULL,
        [PasswordHash] varbinary(max) NULL,
        [PasswordSalt] varbinary(max) NULL,
        [CountryId] int NULL,
        CONSTRAINT [PK_User] PRIMARY KEY ([UserId]),
        CONSTRAINT [FK_User_Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Country] ([CountryId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE TABLE [Company] (
        [CompanyId] bigint NOT NULL IDENTITY,
        [Name] nvarchar(60) NOT NULL,
        [Rnc] nvarchar(18) NULL,
        [HomeOrApartment] nvarchar(50) NULL,
        [City] nvarchar(30) NULL,
        [Street] nvarchar(220) NULL,
        [CountryId] int NULL,
        [Phone] nvarchar(11) NULL,
        [Website] nvarchar(max) NULL,
        [RegimeTypeId] bigint NULL,
        [CurrencyId] int NULL,
        CONSTRAINT [PK_Company] PRIMARY KEY ([CompanyId]),
        CONSTRAINT [FK_Company_Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Country] ([CountryId]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Company_Currency_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [Currency] ([CurrencyId]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Company_TaxRegimeType_RegimeTypeId] FOREIGN KEY ([RegimeTypeId]) REFERENCES [TaxRegimeType] ([TaxRegimeTypeId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE TABLE [Product] (
        [ProductId] bigint NOT NULL IDENTITY,
        [Code] nvarchar(20) NOT NULL,
        [Name] nvarchar(60) NOT NULL,
        [Description] nvarchar(60) NULL,
        [Reference] nvarchar(60) NULL,
        [WerehouseId] bigint NULL,
        [Cost] decimal(16,2) NOT NULL,
        [Price] decimal(16,2) NOT NULL,
        [Price2] decimal(16,2) NOT NULL,
        [Price3] decimal(16,2) NOT NULL,
        [Price4] decimal(16,2) NOT NULL,
        CONSTRAINT [PK_Product] PRIMARY KEY ([ProductId]),
        CONSTRAINT [FK_Product_Werehouse_WerehouseId] FOREIGN KEY ([WerehouseId]) REFERENCES [Werehouse] ([WerehouseId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE TABLE [InvoiceHeader] (
        [InvoiceHeaderId] bigint NOT NULL IDENTITY,
        [Code] nvarchar(20) NOT NULL,
        [Ncf] bigint NOT NULL,
        [Rnc] nvarchar(15) NULL,
        [ClientId] bigint NULL,
        [CompanyId] bigint NULL,
        [Created] datetime2 NOT NULL,
        [DueDate] datetime2 NOT NULL,
        [PaymentTermId] bigint NULL,
        [Notes] nvarchar(220) NULL,
        [TermAndConditions] nvarchar(220) NULL,
        [Footer] nvarchar(220) NULL,
        [SystemCreated] datetime2 NOT NULL,
        [SystemModified] datetime2 NULL,
        CONSTRAINT [PK_InvoiceHeader] PRIMARY KEY ([InvoiceHeaderId]),
        CONSTRAINT [FK_InvoiceHeader_Client_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Client] ([ClientId]) ON DELETE NO ACTION,
        CONSTRAINT [FK_InvoiceHeader_Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company] ([CompanyId]) ON DELETE NO ACTION,
        CONSTRAINT [FK_InvoiceHeader_PaymentDeadline_PaymentTermId] FOREIGN KEY ([PaymentTermId]) REFERENCES [PaymentDeadline] ([PaymentDeadlineId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE TABLE [Category] (
        [CategoryId] bigint NOT NULL IDENTITY,
        [Code] nvarchar(20) NOT NULL,
        [Name] nvarchar(60) NOT NULL,
        [ShowOn] bit NOT NULL,
        [ProductId] bigint NOT NULL,
        CONSTRAINT [PK_Category] PRIMARY KEY ([CategoryId]),
        CONSTRAINT [FK_Category_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([ProductId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE TABLE [InvoiceDetail] (
        [InvoiceDetailId] bigint NOT NULL IDENTITY,
        [ProductId] bigint NULL,
        [Discount] decimal(4,2) NOT NULL,
        [TaxAmount] decimal(12,2) NOT NULL,
        [Qty] (integer) NOT NULL,
        [Subtotal] decimal(16,2) NOT NULL,
        [Total] decimal(16,2) NOT NULL,
        [InvoiceHeaderId] bigint NULL,
        CONSTRAINT [PK_InvoiceDetail] PRIMARY KEY ([InvoiceDetailId]),
        CONSTRAINT [FK_InvoiceDetail_InvoiceHeader_InvoiceHeaderId] FOREIGN KEY ([InvoiceHeaderId]) REFERENCES [InvoiceHeader] ([InvoiceHeaderId]) ON DELETE NO ACTION,
        CONSTRAINT [FK_InvoiceDetail_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([ProductId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CountryId', N'IconImage', N'Name') AND [object_id] = OBJECT_ID(N'[Country]'))
        SET IDENTITY_INSERT [Country] ON;
    INSERT INTO [Country] ([CountryId], [IconImage], [Name])
    VALUES (1, N'', N'Dominican Republic'),
    (2, N'', N'Puerto Rico'),
    (3, N'', N'Panama'),
    (4, N'', N'Colombia');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CountryId', N'IconImage', N'Name') AND [object_id] = OBJECT_ID(N'[Country]'))
        SET IDENTITY_INSERT [Country] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CurrencyId', N'Name') AND [object_id] = OBJECT_ID(N'[Currency]'))
        SET IDENTITY_INSERT [Currency] ON;
    INSERT INTO [Currency] ([CurrencyId], [Name])
    VALUES (1, N'DOP Peso Dominicano'),
    (2, N'US Dolar USA');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CurrencyId', N'Name') AND [object_id] = OBJECT_ID(N'[Currency]'))
        SET IDENTITY_INSERT [Currency] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE INDEX [IX_Category_ProductId] ON [Category] ([ProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE INDEX [IX_Client_CountryId] ON [Client] ([CountryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE INDEX [IX_Company_CountryId] ON [Company] ([CountryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE INDEX [IX_Company_CurrencyId] ON [Company] ([CurrencyId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE INDEX [IX_Company_RegimeTypeId] ON [Company] ([RegimeTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE INDEX [IX_InvoiceDetail_InvoiceHeaderId] ON [InvoiceDetail] ([InvoiceHeaderId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE INDEX [IX_InvoiceDetail_ProductId] ON [InvoiceDetail] ([ProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE INDEX [IX_InvoiceHeader_ClientId] ON [InvoiceHeader] ([ClientId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE INDEX [IX_InvoiceHeader_CompanyId] ON [InvoiceHeader] ([CompanyId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE INDEX [IX_InvoiceHeader_PaymentTermId] ON [InvoiceHeader] ([PaymentTermId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE INDEX [IX_Product_WerehouseId] ON [Product] ([WerehouseId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE INDEX [IX_Supplier_CountryId] ON [Supplier] ([CountryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    CREATE INDEX [IX_User_CountryId] ON [User] ([CountryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200628232756_initialModel')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200628232756_initialModel', N'3.1.3');
END;

GO

