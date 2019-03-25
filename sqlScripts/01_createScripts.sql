IF db_id('BankingReportingSystem') IS NULL 
    CREATE DATABASE BankingReportingSystem

GO

CREATE TABLE [BankingReportingSystem].[dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Customers] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [BankingReportingSystem].[dbo].[CreditCards](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[CardNumber] [nvarchar](50) NOT NULL,
	[CardHolderName] [nvarchar](250) NOT NULL,
	[AvailableCash] [smallmoney] NOT NULL,
	[ExpirationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_CreditCards] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CreditCards] UNIQUE NONCLUSTERED 
(
	[CardNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [BankingReportingSystem].[dbo].[CreditCards]  WITH CHECK ADD  CONSTRAINT [FK_CreditCards_Customers] FOREIGN KEY([CustomerId])
REFERENCES [BankingReportingSystem].[dbo].[Customers] ([Id])
ON DELETE CASCADE

ALTER TABLE [BankingReportingSystem].[dbo].[CreditCards] CHECK CONSTRAINT [FK_CreditCards_Customers]

CREATE TABLE [BankingReportingSystem].[dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[CardId] [int] NOT NULL,
	[Amount] [smallmoney] NOT NULL,
	[Status] [int] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [BankingReportingSystem].[dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Customers] FOREIGN KEY([CardId])
REFERENCES [BankingReportingSystem].[dbo].[CreditCards] ([Id])
ON DELETE CASCADE

ALTER TABLE [BankingReportingSystem].[dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Customers]