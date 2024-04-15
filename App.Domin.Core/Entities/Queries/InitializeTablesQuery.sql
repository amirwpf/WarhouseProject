CREATE DATABASE IF NOT EXISTS [warehouse];
GO
USE [warehouse];
GO

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'item')
BEGIN
    CREATE TABLE [dbo].[item](
        [Id] [int] NOT NULL,
        [Code] [int] NULL,
          NOT NULL,
        [Version] [int] NOT NULL,
    PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    
    ALTER TABLE [dbo].[item] ADD  DEFAULT ((0)) FOR [Version];
END
GO

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'stock')
BEGIN
    CREATE TABLE [dbo].[stock](
        [Id] [int] NOT NULL,
        [Code] [int] NULL,
          NOT NULL,
        [Version] [int] NOT NULL,
    PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    
    ALTER TABLE [dbo].[stock] ADD  DEFAULT ((0)) FOR [Version];
END
GO

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'delivery')
BEGIN
    CREATE TABLE [dbo].[delivery](
        [Id] [int] NOT NULL,
        [Number] [int] NULL,
        [Date] [datetime] NOT NULL,
        [stockId] [int] NULL,
        [Version] [int] NOT NULL,
    PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    
    ALTER TABLE [dbo].[delivery] ADD  DEFAULT ((0)) FOR [Version];
    
    ALTER TABLE [dbo].[delivery]  WITH CHECK ADD FOREIGN KEY([stockId])
    REFERENCES [dbo].[stock] ([Id]);
END
GO

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'deliveryItems')
BEGIN
    CREATE TABLE [dbo].[deliveryItems](
        [Id] [int] NOT NULL,
        [itemId] [int] NULL,
        [DeliveryId] [int] NULL,
        [Quantity] [int] NOT NULL,
    PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    
    ALTER TABLE [dbo].[deliveryItems]  WITH CHECK ADD FOREIGN KEY([DeliveryId])
    REFERENCES [dbo].[delivery] ([Id])
    ON DELETE CASCADE;
    
    ALTER TABLE [dbo].[deliveryItems]  WITH CHECK ADD FOREIGN KEY([itemId])
    REFERENCES [dbo].[item] ([Id]);
END
GO

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'receipt')
BEGIN
    CREATE TABLE [dbo].[receipt](
        [Id] [int] NOT NULL,
        [Number] [int] NULL,
        [Date] [datetime] NOT NULL,
        [StockId] [int] NULL,
        [Version] [int] NOT NULL,
    PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    
    ALTER TABLE [dbo].[receipt] ADD  DEFAULT ((0)) FOR [Version];
    
    ALTER TABLE [dbo].[receipt]  WITH CHECK ADD FOREIGN KEY([StockId])
    REFERENCES [dbo].[stock] ([Id]);
END
GO

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'receiptItem')
BEGIN
    CREATE TABLE [dbo].[receiptItem](
        [Id] [int] NOT NULL,
        [ItemId] [int] NULL,
        [ReceiptId] [int] NULL,
        [Quantity] [int] NOT NULL,
    PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
    
    ALTER TABLE [dbo].[receiptItem]  WITH CHECK ADD FOREIGN KEY([ItemId])
    REFERENCES [dbo].[item] ([Id]);
    
    ALTER TABLE [dbo].[receiptItem]  WITH CHECK ADD FOREIGN KEY([ReceiptId])
    REFERENCES [dbo].[receipt] ([Id])
    ON DELETE CASCADE;
END
GO

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tableIds')
BEGIN
    CREATE TABLE [dbo].[tableIds](
        [Id] [int] NOT NULL,
          NULL,
        [IdNumber] [int] NULL,
    PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY];
END
GO