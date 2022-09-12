
CREATE DATABASE [BancoEjercicio]
GO

USE [BancoEjercicio]
GO

/****** Object:  Table [dbo].[Persona]    Script Date: 19-Aug-22 2:26:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Persona](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](255) NULL,
	[Genero] [nvarchar](20) NULL,
	[Edad] [int] NULL,
	[Identificacion] [nvarchar](20) NULL,
	[Direccion] [nvarchar](max) NULL,
	[Telefono] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NULL,
	[Estado] [bit] NULL,
	[Discriminator] [nvarchar](max) NULL,
 CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



CREATE TABLE [dbo].[Cuenta](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Numero] [nvarchar](50) NOT NULL,
	[Tipo] [nvarchar](50) NOT NULL,
	[SaldoInicial] [numeric](18, 2) NULL,
	[Estado] [bit] NOT NULL,
	[ClienteId] [int] NOT NULL,
	[Saldo] [numeric](18, 2) NULL,
 CONSTRAINT [PK_Cuenta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Cuenta]  WITH CHECK ADD  CONSTRAINT [FK_Cuenta_Persona] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Persona] ([Id])
GO

ALTER TABLE [dbo].[Cuenta] CHECK CONSTRAINT [FK_Cuenta_Persona]
GO


CREATE TABLE [dbo].[Movimiento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CuentaId] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Tipo] [nvarchar](50) NOT NULL,
	[SaldoInicial] [numeric](18, 2) NOT NULL,
	[Valor] [numeric](18, 2) NOT NULL,
	[SaldoDisponible] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_Movimiento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Movimiento]  WITH CHECK ADD  CONSTRAINT [FK_Movimiento_Cuenta] FOREIGN KEY([CuentaId])
REFERENCES [dbo].[Cuenta] ([Id])
GO

ALTER TABLE [dbo].[Movimiento] CHECK CONSTRAINT [FK_Movimiento_Cuenta]
GO

CREATE OR ALTER PROCEDURE [dbo].[spReporteMovimiento]
@ClienteId INT = NULL,
@FechaDesde DATETIME = NULL,
@FechaHasta DATETIME = NULL
AS
BEGIN

	SET NOCOUNT ON;

	SELECT m.Fecha, p.Nombre, c.Numero, 
	c.Tipo, m.SaldoInicial, c.Estado, m.Valor as Movimiento, m.SaldoDisponible
	FROM Movimiento m
	JOIN Cuenta c On c.Id = m.CuentaId
	JOIN Persona p On p.Id = c.ClienteId
	WHERE 1 = 1 
	And (@FechaDesde Is Null Or m.Fecha >= @FechaDesde)
	And (@FechaHasta Is Null Or m.Fecha <= @FechaHasta)
	And (@ClienteId Is Null Or c.ClienteId = @ClienteId)
	ORDER BY c.ClienteId, m.Fecha Desc, m.Id Desc

END
GO
