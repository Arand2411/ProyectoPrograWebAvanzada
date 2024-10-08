USE [master]
GO
/****** Object:  Database [PPAW]    Script Date: 8/26/2024 10:28:54 PM ******/
CREATE DATABASE [PPAW]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PPAW', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\PPAW.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PPAW_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\PPAW_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PPAW] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PPAW].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PPAW] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PPAW] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PPAW] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PPAW] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PPAW] SET ARITHABORT OFF 
GO
ALTER DATABASE [PPAW] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PPAW] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PPAW] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PPAW] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PPAW] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PPAW] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PPAW] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PPAW] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PPAW] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PPAW] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PPAW] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PPAW] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PPAW] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PPAW] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PPAW] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PPAW] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PPAW] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PPAW] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PPAW] SET  MULTI_USER 
GO
ALTER DATABASE [PPAW] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PPAW] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PPAW] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PPAW] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PPAW] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PPAW] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PPAW] SET QUERY_STORE = ON
GO
ALTER DATABASE [PPAW] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [PPAW]
GO
/****** Object:  Table [dbo].[tCategoria]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tCategoria](
	[IdCategoria] [int] IDENTITY(1,1) NOT NULL,
	[NombreCategoria] [varchar](250) NOT NULL,
 CONSTRAINT [PK_tCategoria] PRIMARY KEY CLUSTERED 
(
	[IdCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tProducto]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tProducto](
	[IdProducto] [bigint] IDENTITY(1,1) NOT NULL,
	[DescripcionProducto] [varchar](255) NOT NULL,
	[PrecioUnitario] [decimal](18, 2) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Imagen] [varchar](255) NULL,
	[Estado] [char](1) NULL,
	[IdCategoria] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tRol]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tRol](
	[IdRol] [tinyint] NOT NULL,
	[Descripcion] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tUsuario]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tUsuario](
	[Consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[Identificacion] [varchar](50) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[Contrasenna] [varchar](100) NOT NULL,
	[IdRol] [int] NOT NULL,
	[Estado] [bit] NOT NULL,
	[EsTemporal] [bit] NOT NULL,
	[VigenciaTemporal] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tProducto] ADD  DEFAULT ('A') FOR [Estado]
GO
ALTER TABLE [dbo].[tUsuario] ADD  DEFAULT ((0)) FOR [EsTemporal]
GO
ALTER TABLE [dbo].[tProducto]  WITH CHECK ADD FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[tCategoria] ([IdCategoria])
GO
/****** Object:  StoredProcedure [dbo].[ActualizarContrasenna]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[ActualizarContrasenna]
	@Consecutivo INT, 
	@Contrasenna VARCHAR(100),
	@EsTemporal	 BIT, 
	@VigenciaTemporal DATETIME
AS
BEGIN

	UPDATE tUsuario
	   SET Contrasenna = @Contrasenna,
		   EsTemporal = @EsTemporal,
		   VigenciaTemporal = @VigenciaTemporal
	 WHERE Consecutivo = @Consecutivo

END

GO
/****** Object:  StoredProcedure [dbo].[ActualizarProducto]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[ActualizarProducto]
    @IdProducto int,
    @DescripcionProducto varchar(100),
    @PrecioUnitario DECIMAL(18, 2),
    @Cantidad INT,
	@Imagen varchar(255),
	@Estado char,
    @IdCategoria INT
AS
BEGIN
    UPDATE tProducto
    SET DescripcionProducto = @DescripcionProducto,
        PrecioUnitario = @PrecioUnitario,
        Cantidad = @Cantidad,
        Imagen = @Imagen,
		Estado = @Estado,
		IdCategoria = @IdCategoria
    WHERE IdProducto = @IdProducto
END


GO
/****** Object:  StoredProcedure [dbo].[ActualizarUsuario]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[ActualizarUsuario]
	@Consecutivo	INT,
	@Identificacion VARCHAR(50),
	@Nombre			VARCHAR(100),
	@Correo			VARCHAR(100),
	@IdRol			TINYINT
AS
BEGIN

	IF NOT EXISTS(SELECT 1 FROM dbo.tUsuario WHERE	(Correo = @Correo 
												OR	Identificacion = @Identificacion)
												AND Consecutivo != @Consecutivo)
	BEGIN

		UPDATE tUsuario
		   SET Identificacion = @Identificacion,
			   Nombre = @Nombre,
			   Correo = @Correo,
			   IdRol = @IdRol
		 WHERE Consecutivo = @Consecutivo

	END

END

GO
/****** Object:  StoredProcedure [dbo].[CambiarEstadoUsuario]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[CambiarEstadoUsuario]
	@Consecutivo INT
AS
BEGIN

	UPDATE tUsuario
	   SET Estado = CASE WHEN Estado = 1 THEN 0 ELSE 1 END
	 WHERE Consecutivo = @Consecutivo

END

GO
/****** Object:  StoredProcedure [dbo].[ConsultarAccesorios]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento almacenado para consultar Accesorios (Categoría 3)
CREATE PROCEDURE [dbo].[ConsultarAccesorios]
AS
BEGIN
    SELECT 
        p.IdProducto, 
        p.DescripcionProducto, 
        p.PrecioUnitario, 
        p.Cantidad, 
        p.Imagen, 
        p.Estado
    FROM 
        tProducto p
    WHERE 
        p.IdCategoria = 3; -- Accesorios
END

GO
/****** Object:  StoredProcedure [dbo].[ConsultarComponentes]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Procedimiento almacenado para consultar Componentes (Categoría 1)
CREATE PROCEDURE [dbo].[ConsultarComponentes]
AS
BEGIN
    SELECT 
        p.IdProducto, 
        p.DescripcionProducto, 
        p.PrecioUnitario, 
        p.Cantidad, 
        p.Imagen, 
        p.Estado
    FROM 
        tProducto p
    WHERE 
        p.IdCategoria = 1; -- Componentes
END

GO
/****** Object:  StoredProcedure [dbo].[ConsultarComputadoras]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento almacenado para consultar Computadoras (Categoría 4)
CREATE PROCEDURE [dbo].[ConsultarComputadoras]
AS
BEGIN
    SELECT 
        p.IdProducto, 
        p.DescripcionProducto, 
        p.PrecioUnitario, 
        p.Cantidad, 
        p.Imagen, 
        p.Estado
    FROM 
        tProducto p
    WHERE 
        p.IdCategoria = 4; -- Computadoras
END

GO
/****** Object:  StoredProcedure [dbo].[ConsultarPerifericos]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Procedimiento almacenado para consultar Periféricos (Categoría 2)
CREATE PROCEDURE [dbo].[ConsultarPerifericos]
AS
BEGIN
    SELECT 
        p.IdProducto, 
        p.DescripcionProducto, 
        p.PrecioUnitario, 
        p.Cantidad, 
        p.Imagen, 
        p.Estado
    FROM 
        tProducto p
    WHERE 
        p.IdCategoria = 2; -- Periféricos
END

GO
/****** Object:  StoredProcedure [dbo].[ConsultarProducto]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[ConsultarProducto]
AS
BEGIN
    SELECT IdProducto, DescripcionProducto, PrecioUnitario, Cantidad, Imagen, Estado, IdCategoria
    FROM tProducto;
END;
GO
/****** Object:  StoredProcedure [dbo].[ConsultarRoles]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[ConsultarRoles]

AS
BEGIN

	SELECT IdRol 'value', Descripcion 'text'
	FROM tRol

END

GO
/****** Object:  StoredProcedure [dbo].[ConsultarUsuario]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[ConsultarUsuario]
	@Consecutivo INT
AS
BEGIN

	SELECT	Consecutivo,Identificacion,Nombre,Correo,U.IdRol,
	CASE WHEN Estado = 1 THEN 'Activo' ELSE 'Inactivo' END 'Estado',R.Descripcion
	FROM	dbo.tUsuario U
	INNER JOIN dbo.tRol  R ON U.IdRol = R.IdRol
	WHERE Consecutivo = @Consecutivo

END

GO
/****** Object:  StoredProcedure [dbo].[ConsultarUsuarioIdentificacion]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[ConsultarUsuarioIdentificacion]
	@Identificacion INT
AS
BEGIN

	SELECT	Consecutivo,Identificacion,Nombre,Correo,U.IdRol,
	CASE WHEN Estado = 1 THEN 'Activo' ELSE 'Inactivo' END 'Estado',R.Descripcion
	FROM	dbo.tUsuario U
	INNER JOIN dbo.tRol  R ON U.IdRol = R.IdRol
	WHERE Identificacion = @Identificacion

END

GO
/****** Object:  StoredProcedure [dbo].[ConsultarUsuarios]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[ConsultarUsuarios]
	
AS
BEGIN

	SELECT	Consecutivo,Identificacion,Nombre,Correo,U.IdRol,
	CASE WHEN Estado = 1 THEN 'Activo' ELSE 'Inactivo' END 'Estado',R.Descripcion
	FROM	dbo.tUsuario U
	INNER JOIN dbo.tRol  R ON U.IdRol = R.IdRol

END

GO
/****** Object:  StoredProcedure [dbo].[EliminarProducto]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarProducto]
    @IdProducto INT
AS
BEGIN
    DELETE FROM tProducto
    WHERE IdProducto = @IdProducto;
END



GO
/****** Object:  StoredProcedure [dbo].[IniciarSesion]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[IniciarSesion]
	@Correo			varchar(100),
	@Contrasenna	varchar(100)
AS
BEGIN

	SELECT	Consecutivo,Identificacion,Nombre,Correo,U.IdRol,Estado,R.Descripcion,
			EsTemporal, VigenciaTemporal
	FROM	dbo.tUsuario U
	INNER JOIN dbo.tRol  R ON U.IdRol = R.IdRol
	WHERE	Correo = @Correo
		AND Contrasenna = @Contrasenna
		AND Estado = 1

END

GO
/****** Object:  StoredProcedure [dbo].[ObtenerProductoPorId]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerProductoPorId]
    @IdProducto NVARCHAR(50)
AS
BEGIN
    SELECT IdProducto, DescripcionProducto, PrecioUnitario, Cantidad, Imagen, Estado, IdCategoria
    FROM tProducto
    WHERE IdProducto = @IdProducto
END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarProducto]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarProducto]
	@DescripcionProducto VARCHAR(200),
    @PrecioUnitario DECIMAL(10, 2),
    @Cantidad INT,
    @Imagen VARCHAR(100),
    @Estado CHAR(1) = 'A',
    @IdCategoria INT
AS
BEGIN
	BEGIN
		 INSERT INTO tProducto(DescripcionProducto, PrecioUnitario, Cantidad, Imagen,Estado, IdCategoria)
			VALUES (@DescripcionProducto, @PrecioUnitario, @Cantidad, @Imagen,  @Estado, @IdCategoria);
	END
END


GO
/****** Object:  StoredProcedure [dbo].[RegistrarUsuario]    Script Date: 8/26/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegistrarUsuario]
	@Identificacion varchar(50),
	@Nombre			varchar(100),
	@Correo			varchar(100),
	@Contrasenna	varchar(100)
AS
BEGIN

	DECLARE @Rol		TINYINT = 2,
			@Estado		BIT		= 1,
			@Temporal	BIT		= 0

	IF NOT EXISTS(SELECT 1 FROM dbo.tUsuario WHERE Correo = @Correo OR Identificacion = @Identificacion)
	BEGIN

		INSERT INTO dbo.tUsuario(Identificacion,Nombre,Correo,Contrasenna,IdRol,Estado,EsTemporal,VigenciaTemporal)
		VALUES (@Identificacion,@Nombre,@Correo,@Contrasenna,@Rol,@Estado,@Temporal,GETDATE())

	END

END

GO
USE [master]
GO
ALTER DATABASE [PPAW] SET  READ_WRITE 
GO
