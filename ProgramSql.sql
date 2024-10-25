USE [master]
GO
/****** Object:  Database [Avtosalon]    Script Date: 23.06.2024 19:03:28 ******/
CREATE DATABASE [Avtosalon]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Avtosalon', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQL\MSSQL\DATA\Avtosalon.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Avtosalon_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQL\MSSQL\DATA\Avtosalon_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Avtosalon] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Avtosalon].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Avtosalon] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Avtosalon] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Avtosalon] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Avtosalon] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Avtosalon] SET ARITHABORT OFF 
GO
ALTER DATABASE [Avtosalon] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Avtosalon] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Avtosalon] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Avtosalon] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Avtosalon] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Avtosalon] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Avtosalon] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Avtosalon] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Avtosalon] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Avtosalon] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Avtosalon] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Avtosalon] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Avtosalon] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Avtosalon] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Avtosalon] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Avtosalon] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Avtosalon] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Avtosalon] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Avtosalon] SET  MULTI_USER 
GO
ALTER DATABASE [Avtosalon] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Avtosalon] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Avtosalon] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Avtosalon] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Avtosalon] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Avtosalon] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Avtosalon', N'ON'
GO
ALTER DATABASE [Avtosalon] SET QUERY_STORE = OFF
GO
USE [Avtosalon]
GO
/****** Object:  Table [dbo].[Автомобили]    Script Date: 23.06.2024 19:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Автомобили](
	[Код_авто] [int] IDENTITY(1,1) NOT NULL,
	[Код_модели] [int] NULL,
	[Код_ТО] [int] NULL,
	[Описание] [nvarchar](300) NULL,
	[Цена] [nvarchar](7) NULL,
	[Код_доп_услуг] [int] NULL,
	[Код_характеристик] [int] NULL,
	[Фото] [nvarchar](255) NULL,
 CONSTRAINT [PK_Автомобили] PRIMARY KEY CLUSTERED 
(
	[Код_авто] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Договор]    Script Date: 23.06.2024 19:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Договор](
	[Код_договора] [int] IDENTITY(1,1) NOT NULL,
	[Код_заявки] [int] NULL,
	[Дата] [date] NULL,
 CONSTRAINT [PK_Договор] PRIMARY KEY CLUSTERED 
(
	[Код_договора] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Доп_услуги]    Script Date: 23.06.2024 19:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Доп_услуги](
	[Код_доп_услуг] [int] IDENTITY(1,1) NOT NULL,
	[Наименование] [nvarchar](100) NULL,
	[Стоимость] [nvarchar](7) NULL,
 CONSTRAINT [PK_Доп_услуги] PRIMARY KEY CLUSTERED 
(
	[Код_доп_услуг] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Доп_услуги_авто]    Script Date: 23.06.2024 19:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Доп_услуги_авто](
	[Код_доп_услуг_авто] [int] IDENTITY(1,1) NOT NULL,
	[Количество] [nvarchar](2) NULL,
	[Код_доп_услуг] [int] NULL,
	[Код_авто] [int] NULL,
 CONSTRAINT [PK_Доп_услуги_авто] PRIMARY KEY CLUSTERED 
(
	[Код_доп_услуг_авто] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Заявка]    Script Date: 23.06.2024 19:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Заявка](
	[Код_заявки] [int] IDENTITY(1,1) NOT NULL,
	[Код_клиента] [int] NULL,
	[Код_авто] [int] NULL,
	[Дата] [date] NULL,
	[Код_сотрудника] [int] NULL,
	[Итоговая_цена] [nvarchar](50) NULL,
 CONSTRAINT [PK_Заявка] PRIMARY KEY CLUSTERED 
(
	[Код_заявки] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Клиенты]    Script Date: 23.06.2024 19:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Клиенты](
	[Код_клиента] [int] IDENTITY(1,1) NOT NULL,
	[ФИО] [nvarchar](200) NULL,
	[Код_паспорта] [int] NULL,
	[Телефон] [nvarchar](50) NULL,
 CONSTRAINT [PK_Клиенты] PRIMARY KEY CLUSTERED 
(
	[Код_клиента] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Коробка_передач]    Script Date: 23.06.2024 19:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Коробка_передач](
	[Код_типа_коробки передач] [int] IDENTITY(1,1) NOT NULL,
	[Наименование] [nvarchar](100) NULL,
 CONSTRAINT [PK_Коробка_передач] PRIMARY KEY CLUSTERED 
(
	[Код_типа_коробки передач] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Кузов]    Script Date: 23.06.2024 19:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Кузов](
	[Код_типа_кузова] [int] IDENTITY(1,1) NOT NULL,
	[Наименование] [nvarchar](100) NULL,
 CONSTRAINT [PK_Тип_кузова] PRIMARY KEY CLUSTERED 
(
	[Код_типа_кузова] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Паспорт]    Script Date: 23.06.2024 19:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Паспорт](
	[Код_паспорта] [int] IDENTITY(1,1) NOT NULL,
	[Серия] [nvarchar](4) NULL,
	[Номер] [nvarchar](6) NULL,
	[Дата_выдачи] [date] NULL,
 CONSTRAINT [PK_Паспорт] PRIMARY KEY CLUSTERED 
(
	[Код_паспорта] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Сотрудники]    Script Date: 23.06.2024 19:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Сотрудники](
	[Код_сотрудника] [int] IDENTITY(1,1) NOT NULL,
	[ФИО] [nvarchar](200) NULL,
	[email] [nvarchar](50) NULL,
	[Логин] [nvarchar](50) NULL,
	[Пароль] [nvarchar](50) NULL,
	[Код_типа_пользователя] [int] NULL,
	[Фото] [nvarchar](255) NULL,
	[Статус] [bit] NULL,
 CONSTRAINT [PK_Сотрудники] PRIMARY KEY CLUSTERED 
(
	[Код_сотрудника] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Список_моделей]    Script Date: 23.06.2024 19:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Список_моделей](
	[Код_модели] [int] IDENTITY(1,1) NOT NULL,
	[Наименование] [nvarchar](50) NULL,
 CONSTRAINT [PK_Список_моделей] PRIMARY KEY CLUSTERED 
(
	[Код_модели] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Тип_пользователя]    Script Date: 23.06.2024 19:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Тип_пользователя](
	[Код_типа_пользователя] [int] IDENTITY(1,1) NOT NULL,
	[Наименование] [nvarchar](50) NULL,
 CONSTRAINT [PK_Тип_пользователя] PRIMARY KEY CLUSTERED 
(
	[Код_типа_пользователя] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ТО]    Script Date: 23.06.2024 19:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ТО](
	[Код_ТО] [int] IDENTITY(1,1) NOT NULL,
	[Статус] [nvarchar](50) NULL,
 CONSTRAINT [PK_ТО] PRIMARY KEY CLUSTERED 
(
	[Код_ТО] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Характеристики]    Script Date: 23.06.2024 19:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Характеристики](
	[Код_характеристик] [int] IDENTITY(1,1) NOT NULL,
	[Скорость] [nvarchar](50) NULL,
	[Масса] [nvarchar](50) NULL,
	[Код_типа_коробки_передач] [int] NULL,
	[Код_типа_кузова] [int] NULL,
	[Номер] [nvarchar](3) NULL,
 CONSTRAINT [PK_Характеристики] PRIMARY KEY CLUSTERED 
(
	[Код_характеристик] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Автомобили] ON 
GO
INSERT [dbo].[Автомобили] ([Код_авто], [Код_модели], [Код_ТО], [Описание], [Цена], [Код_доп_услуг], [Код_характеристик], [Фото]) VALUES (6, 1, 1, N'Nissan X-Trail - это популярный компактный кроссовер, который отличается стильным дизайном, просторным салоном, удобством использования и отличной проходимостью. ', N'3396000', NULL, 1, N'X-trail.png')
GO
INSERT [dbo].[Автомобили] ([Код_авто], [Код_модели], [Код_ТО], [Описание], [Цена], [Код_доп_услуг], [Код_характеристик], [Фото]) VALUES (7, 2, 2, N'Nissan Juke - компактный городской кроссовер с ярким дизайном и неповторимым стилем. Представляет собой эффектный автомобиль с оригинальной внешностью, привлекательной для молодежной аудитории.', N'655000', NULL, 2, N'Juke.png')
GO
INSERT [dbo].[Автомобили] ([Код_авто], [Код_модели], [Код_ТО], [Описание], [Цена], [Код_доп_услуг], [Код_характеристик], [Фото]) VALUES (8, 3, 1, N'Nissan Terrano - это внедорожник компании Nissan, который имеет надежную репутацию и хорошую проходимость. Этот автомобиль предлагает комфортабельный салон, просторный багажник и различные версии двигателей, мощность которых может варьироваться.', N'1329000', NULL, 4, N'Nissan-Terrano.jpg')
GO
INSERT [dbo].[Автомобили] ([Код_авто], [Код_модели], [Код_ТО], [Описание], [Цена], [Код_доп_услуг], [Код_характеристик], [Фото]) VALUES (9, 4, 1, N'Nissan Almera N16 - это компактный седан, выпускавшийся японским автопроизводителем Nissan с 2000 по 2006 год. Автомобиль был доступен по всему миру, включая рынки в Европе, Азии и других регионах.', N'315000', NULL, 5, N'almera.jpg')
GO
INSERT [dbo].[Автомобили] ([Код_авто], [Код_модели], [Код_ТО], [Описание], [Цена], [Код_доп_услуг], [Код_характеристик], [Фото]) VALUES (10, 5, NULL, N'Nissan Teana – это среднеразмерный седан, выпускаемый японским автопроизводителем Nissan с 2003 года. Автомобиль предлагает комфортную и просторную кабину, а также надежное и экономичное двигатели, которые обеспечивают плавное и комфортное движение.', N'820000', NULL, 6, N'Teana.jpg')
GO
SET IDENTITY_INSERT [dbo].[Автомобили] OFF
GO
SET IDENTITY_INSERT [dbo].[Договор] ON 
GO
INSERT [dbo].[Договор] ([Код_договора], [Код_заявки], [Дата]) VALUES (1, 1, CAST(N'2024-06-15' AS Date))
GO
INSERT [dbo].[Договор] ([Код_договора], [Код_заявки], [Дата]) VALUES (2, 2, CAST(N'2024-06-15' AS Date))
GO
SET IDENTITY_INSERT [dbo].[Договор] OFF
GO
SET IDENTITY_INSERT [dbo].[Доп_услуги] ON 
GO
INSERT [dbo].[Доп_услуги] ([Код_доп_услуг], [Наименование], [Стоимость]) VALUES (1, N'Химчистка салона', N'1500')
GO
INSERT [dbo].[Доп_услуги] ([Код_доп_услуг], [Наименование], [Стоимость]) VALUES (2, N'Ароматизатор', N'500')
GO
SET IDENTITY_INSERT [dbo].[Доп_услуги] OFF
GO
SET IDENTITY_INSERT [dbo].[Доп_услуги_авто] ON 
GO
INSERT [dbo].[Доп_услуги_авто] ([Код_доп_услуг_авто], [Количество], [Код_доп_услуг], [Код_авто]) VALUES (1, N'4', NULL, 6)
GO
INSERT [dbo].[Доп_услуги_авто] ([Код_доп_услуг_авто], [Количество], [Код_доп_услуг], [Код_авто]) VALUES (2, N'2', NULL, 7)
GO
INSERT [dbo].[Доп_услуги_авто] ([Код_доп_услуг_авто], [Количество], [Код_доп_услуг], [Код_авто]) VALUES (1002, N'', NULL, 6)
GO
INSERT [dbo].[Доп_услуги_авто] ([Код_доп_услуг_авто], [Количество], [Код_доп_услуг], [Код_авто]) VALUES (1003, N'', NULL, 7)
GO
INSERT [dbo].[Доп_услуги_авто] ([Код_доп_услуг_авто], [Количество], [Код_доп_услуг], [Код_авто]) VALUES (1004, N'', NULL, 6)
GO
INSERT [dbo].[Доп_услуги_авто] ([Код_доп_услуг_авто], [Количество], [Код_доп_услуг], [Код_авто]) VALUES (1005, N'1', NULL, 6)
GO
INSERT [dbo].[Доп_услуги_авто] ([Код_доп_услуг_авто], [Количество], [Код_доп_услуг], [Код_авто]) VALUES (1006, N'3', NULL, 8)
GO
SET IDENTITY_INSERT [dbo].[Доп_услуги_авто] OFF
GO
SET IDENTITY_INSERT [dbo].[Заявка] ON 
GO
INSERT [dbo].[Заявка] ([Код_заявки], [Код_клиента], [Код_авто], [Дата], [Код_сотрудника], [Итоговая_цена]) VALUES (1, 1, 6, CAST(N'2024-06-05' AS Date), 2, N'3402000')
GO
INSERT [dbo].[Заявка] ([Код_заявки], [Код_клиента], [Код_авто], [Дата], [Код_сотрудника], [Итоговая_цена]) VALUES (2, 1, 7, CAST(N'2024-06-15' AS Date), 2, N'656000')
GO
INSERT [dbo].[Заявка] ([Код_заявки], [Код_клиента], [Код_авто], [Дата], [Код_сотрудника], [Итоговая_цена]) VALUES (1002, 1, 6, CAST(N'2024-06-16' AS Date), 2, N'3396000')
GO
INSERT [dbo].[Заявка] ([Код_заявки], [Код_клиента], [Код_авто], [Дата], [Код_сотрудника], [Итоговая_цена]) VALUES (1003, 1, 7, CAST(N'2024-06-16' AS Date), 2, N'655000')
GO
INSERT [dbo].[Заявка] ([Код_заявки], [Код_клиента], [Код_авто], [Дата], [Код_сотрудника], [Итоговая_цена]) VALUES (1004, 1, 6, CAST(N'2024-06-16' AS Date), 2, N'3396000')
GO
INSERT [dbo].[Заявка] ([Код_заявки], [Код_клиента], [Код_авто], [Дата], [Код_сотрудника], [Итоговая_цена]) VALUES (1005, 1, 6, CAST(N'2024-06-16' AS Date), 2, N'3397500')
GO
INSERT [dbo].[Заявка] ([Код_заявки], [Код_клиента], [Код_авто], [Дата], [Код_сотрудника], [Итоговая_цена]) VALUES (1006, 2, 8, CAST(N'2024-06-18' AS Date), 2, N'1333500')
GO
SET IDENTITY_INSERT [dbo].[Заявка] OFF
GO
SET IDENTITY_INSERT [dbo].[Клиенты] ON 
GO
INSERT [dbo].[Клиенты] ([Код_клиента], [ФИО], [Код_паспорта], [Телефон]) VALUES (1, N'Бекманбетов Александров Владимирович', 1, N'89645763476')
GO
INSERT [dbo].[Клиенты] ([Код_клиента], [ФИО], [Код_паспорта], [Телефон]) VALUES (2, N'Сакольников Дмитрий Михайлович', 2, N'89048367346')
GO
SET IDENTITY_INSERT [dbo].[Клиенты] OFF
GO
SET IDENTITY_INSERT [dbo].[Коробка_передач] ON 
GO
INSERT [dbo].[Коробка_передач] ([Код_типа_коробки передач], [Наименование]) VALUES (1, N'Механическая')
GO
INSERT [dbo].[Коробка_передач] ([Код_типа_коробки передач], [Наименование]) VALUES (2, N'Автоматическая')
GO
SET IDENTITY_INSERT [dbo].[Коробка_передач] OFF
GO
SET IDENTITY_INSERT [dbo].[Кузов] ON 
GO
INSERT [dbo].[Кузов] ([Код_типа_кузова], [Наименование]) VALUES (1, N'Седан')
GO
INSERT [dbo].[Кузов] ([Код_типа_кузова], [Наименование]) VALUES (2, N'Купе')
GO
INSERT [dbo].[Кузов] ([Код_типа_кузова], [Наименование]) VALUES (3, N'Хетчбек')
GO
INSERT [dbo].[Кузов] ([Код_типа_кузова], [Наименование]) VALUES (4, N'Лифтбек')
GO
INSERT [dbo].[Кузов] ([Код_типа_кузова], [Наименование]) VALUES (5, N'Фастбке')
GO
INSERT [dbo].[Кузов] ([Код_типа_кузова], [Наименование]) VALUES (6, N'Универсал')
GO
INSERT [dbo].[Кузов] ([Код_типа_кузова], [Наименование]) VALUES (7, N'Кроссовер')
GO
INSERT [dbo].[Кузов] ([Код_типа_кузова], [Наименование]) VALUES (8, N'Внедорожник')
GO
INSERT [dbo].[Кузов] ([Код_типа_кузова], [Наименование]) VALUES (9, N'SUV')
GO
SET IDENTITY_INSERT [dbo].[Кузов] OFF
GO
SET IDENTITY_INSERT [dbo].[Паспорт] ON 
GO
INSERT [dbo].[Паспорт] ([Код_паспорта], [Серия], [Номер], [Дата_выдачи]) VALUES (1, N'5719', N'976349', CAST(N'1993-09-25' AS Date))
GO
INSERT [dbo].[Паспорт] ([Код_паспорта], [Серия], [Номер], [Дата_выдачи]) VALUES (2, N'4325', N'453354', CAST(N'1969-08-18' AS Date))
GO
SET IDENTITY_INSERT [dbo].[Паспорт] OFF
GO
SET IDENTITY_INSERT [dbo].[Сотрудники] ON 
GO
INSERT [dbo].[Сотрудники] ([Код_сотрудника], [ФИО], [email], [Логин], [Пароль], [Код_типа_пользователя], [Фото], [Статус]) VALUES (1, N'Никонов Никита Дмитриевич', N'nikonovnikita0748@gmail.com', N'!Nik12', N'!Nik1212', 2, N'manager.png', 1)
GO
INSERT [dbo].[Сотрудники] ([Код_сотрудника], [ФИО], [email], [Логин], [Пароль], [Код_типа_пользователя], [Фото], [Статус]) VALUES (2, N'Ануфриев Кирилл Александрович', N'Anuf23@mail.ru', N'!ANu37', N'AnUfri37', 1, N'consul.jpg', 1)
GO
INSERT [dbo].[Сотрудники] ([Код_сотрудника], [ФИО], [email], [Логин], [Пароль], [Код_типа_пользователя], [Фото], [Статус]) VALUES (3, N'Приданников Максим Андреевич', N'pdan21@yandex.ru', N'!Pridan21', N'!Pridan21', 3, N'mex.png', 1)
GO
INSERT [dbo].[Сотрудники] ([Код_сотрудника], [ФИО], [email], [Логин], [Пароль], [Код_типа_пользователя], [Фото], [Статус]) VALUES (4, N'Палюхов Олег Дмитриевич', N'Paluh@mail.ru', N'Paluhov_37', N'Paluhov_37', 4, N'admin.jpg', 1)
GO
INSERT [dbo].[Сотрудники] ([Код_сотрудника], [ФИО], [email], [Логин], [Пароль], [Код_типа_пользователя], [Фото], [Статус]) VALUES (5, N'Зарубин Андрей Викторович', N'and23@gmail.com', N'!Admin11', N'!Admin11', 4, N'admin.jpg', 1)
GO
SET IDENTITY_INSERT [dbo].[Сотрудники] OFF
GO
SET IDENTITY_INSERT [dbo].[Список_моделей] ON 
GO
INSERT [dbo].[Список_моделей] ([Код_модели], [Наименование]) VALUES (1, N'X-trail')
GO
INSERT [dbo].[Список_моделей] ([Код_модели], [Наименование]) VALUES (2, N'Juke')
GO
INSERT [dbo].[Список_моделей] ([Код_модели], [Наименование]) VALUES (3, N'Terrano')
GO
INSERT [dbo].[Список_моделей] ([Код_модели], [Наименование]) VALUES (4, N'Almera')
GO
INSERT [dbo].[Список_моделей] ([Код_модели], [Наименование]) VALUES (5, N'Teana')
GO
SET IDENTITY_INSERT [dbo].[Список_моделей] OFF
GO
SET IDENTITY_INSERT [dbo].[Тип_пользователя] ON 
GO
INSERT [dbo].[Тип_пользователя] ([Код_типа_пользователя], [Наименование]) VALUES (1, N'Продавец-консультант')
GO
INSERT [dbo].[Тип_пользователя] ([Код_типа_пользователя], [Наименование]) VALUES (2, N'Финансовый менеджер')
GO
INSERT [dbo].[Тип_пользователя] ([Код_типа_пользователя], [Наименование]) VALUES (3, N'Автомеханик')
GO
INSERT [dbo].[Тип_пользователя] ([Код_типа_пользователя], [Наименование]) VALUES (4, N'Системный администратор')
GO
SET IDENTITY_INSERT [dbo].[Тип_пользователя] OFF
GO
SET IDENTITY_INSERT [dbo].[ТО] ON 
GO
INSERT [dbo].[ТО] ([Код_ТО], [Статус]) VALUES (1, N'Пройдено')
GO
INSERT [dbo].[ТО] ([Код_ТО], [Статус]) VALUES (2, N'В процессе')
GO
SET IDENTITY_INSERT [dbo].[ТО] OFF
GO
SET IDENTITY_INSERT [dbo].[Характеристики] ON 
GO
INSERT [dbo].[Характеристики] ([Код_характеристик], [Скорость], [Масса], [Код_типа_коробки_передач], [Код_типа_кузова], [Номер]) VALUES (1, N'186', N'1908', 2, 8, N'1')
GO
INSERT [dbo].[Характеристики] ([Код_характеристик], [Скорость], [Масса], [Код_типа_коробки_передач], [Код_типа_кузова], [Номер]) VALUES (2, N'215', N'1449', 1, 8, N'2')
GO
INSERT [dbo].[Характеристики] ([Код_характеристик], [Скорость], [Масса], [Код_типа_коробки_передач], [Код_типа_кузова], [Номер]) VALUES (3, N'231', N'1245', 2, 4, N'3')
GO
INSERT [dbo].[Характеристики] ([Код_характеристик], [Скорость], [Масса], [Код_типа_коробки_передач], [Код_типа_кузова], [Номер]) VALUES (4, N'180', N'1650', 1, 9, N'4')
GO
INSERT [dbo].[Характеристики] ([Код_характеристик], [Скорость], [Масса], [Код_типа_коробки_передач], [Код_типа_кузова], [Номер]) VALUES (5, N'176', N'1690', 1, 1, N'5')
GO
INSERT [dbo].[Характеристики] ([Код_характеристик], [Скорость], [Масса], [Код_типа_коробки_передач], [Код_типа_кузова], [Номер]) VALUES (6, N'220', N'1500', 2, 1, N'6')
GO
SET IDENTITY_INSERT [dbo].[Характеристики] OFF
GO
ALTER TABLE [dbo].[Автомобили]  WITH CHECK ADD  CONSTRAINT [FK_Автомобили_Доп_услуги] FOREIGN KEY([Код_доп_услуг])
REFERENCES [dbo].[Доп_услуги] ([Код_доп_услуг])
GO
ALTER TABLE [dbo].[Автомобили] CHECK CONSTRAINT [FK_Автомобили_Доп_услуги]
GO
ALTER TABLE [dbo].[Автомобили]  WITH CHECK ADD  CONSTRAINT [FK_Автомобили_Список_моделей] FOREIGN KEY([Код_модели])
REFERENCES [dbo].[Список_моделей] ([Код_модели])
GO
ALTER TABLE [dbo].[Автомобили] CHECK CONSTRAINT [FK_Автомобили_Список_моделей]
GO
ALTER TABLE [dbo].[Автомобили]  WITH CHECK ADD  CONSTRAINT [FK_Автомобили_ТО] FOREIGN KEY([Код_ТО])
REFERENCES [dbo].[ТО] ([Код_ТО])
GO
ALTER TABLE [dbo].[Автомобили] CHECK CONSTRAINT [FK_Автомобили_ТО]
GO
ALTER TABLE [dbo].[Автомобили]  WITH CHECK ADD  CONSTRAINT [FK_Автомобили_Характеристики] FOREIGN KEY([Код_характеристик])
REFERENCES [dbo].[Характеристики] ([Код_характеристик])
GO
ALTER TABLE [dbo].[Автомобили] CHECK CONSTRAINT [FK_Автомобили_Характеристики]
GO
ALTER TABLE [dbo].[Договор]  WITH CHECK ADD  CONSTRAINT [FK_Договор_Заявка1] FOREIGN KEY([Код_заявки])
REFERENCES [dbo].[Заявка] ([Код_заявки])
GO
ALTER TABLE [dbo].[Договор] CHECK CONSTRAINT [FK_Договор_Заявка1]
GO
ALTER TABLE [dbo].[Доп_услуги_авто]  WITH CHECK ADD  CONSTRAINT [FK_Доп_услуги_авто_Автомобили] FOREIGN KEY([Код_авто])
REFERENCES [dbo].[Автомобили] ([Код_авто])
GO
ALTER TABLE [dbo].[Доп_услуги_авто] CHECK CONSTRAINT [FK_Доп_услуги_авто_Автомобили]
GO
ALTER TABLE [dbo].[Доп_услуги_авто]  WITH CHECK ADD  CONSTRAINT [FK_Доп_услуги_авто_Доп_услуги] FOREIGN KEY([Код_доп_услуг])
REFERENCES [dbo].[Доп_услуги] ([Код_доп_услуг])
GO
ALTER TABLE [dbo].[Доп_услуги_авто] CHECK CONSTRAINT [FK_Доп_услуги_авто_Доп_услуги]
GO
ALTER TABLE [dbo].[Заявка]  WITH CHECK ADD  CONSTRAINT [FK_Заявка_Автомобили] FOREIGN KEY([Код_авто])
REFERENCES [dbo].[Автомобили] ([Код_авто])
GO
ALTER TABLE [dbo].[Заявка] CHECK CONSTRAINT [FK_Заявка_Автомобили]
GO
ALTER TABLE [dbo].[Заявка]  WITH CHECK ADD  CONSTRAINT [FK_Заявка_Клиенты] FOREIGN KEY([Код_клиента])
REFERENCES [dbo].[Клиенты] ([Код_клиента])
GO
ALTER TABLE [dbo].[Заявка] CHECK CONSTRAINT [FK_Заявка_Клиенты]
GO
ALTER TABLE [dbo].[Заявка]  WITH CHECK ADD  CONSTRAINT [FK_Заявка_Сотрудники] FOREIGN KEY([Код_сотрудника])
REFERENCES [dbo].[Сотрудники] ([Код_сотрудника])
GO
ALTER TABLE [dbo].[Заявка] CHECK CONSTRAINT [FK_Заявка_Сотрудники]
GO
ALTER TABLE [dbo].[Клиенты]  WITH CHECK ADD  CONSTRAINT [FK_Клиенты_Паспорт] FOREIGN KEY([Код_паспорта])
REFERENCES [dbo].[Паспорт] ([Код_паспорта])
GO
ALTER TABLE [dbo].[Клиенты] CHECK CONSTRAINT [FK_Клиенты_Паспорт]
GO
ALTER TABLE [dbo].[Сотрудники]  WITH CHECK ADD  CONSTRAINT [FK_Сотрудники_Тип_пользователя] FOREIGN KEY([Код_типа_пользователя])
REFERENCES [dbo].[Тип_пользователя] ([Код_типа_пользователя])
GO
ALTER TABLE [dbo].[Сотрудники] CHECK CONSTRAINT [FK_Сотрудники_Тип_пользователя]
GO
ALTER TABLE [dbo].[Характеристики]  WITH CHECK ADD  CONSTRAINT [FK_Характеристики_Коробка_передач] FOREIGN KEY([Код_типа_коробки_передач])
REFERENCES [dbo].[Коробка_передач] ([Код_типа_коробки передач])
GO
ALTER TABLE [dbo].[Характеристики] CHECK CONSTRAINT [FK_Характеристики_Коробка_передач]
GO
ALTER TABLE [dbo].[Характеристики]  WITH CHECK ADD  CONSTRAINT [FK_Характеристики_Кузов] FOREIGN KEY([Код_типа_кузова])
REFERENCES [dbo].[Кузов] ([Код_типа_кузова])
GO
ALTER TABLE [dbo].[Характеристики] CHECK CONSTRAINT [FK_Характеристики_Кузов]
GO
USE [master]
GO
ALTER DATABASE [Avtosalon] SET  READ_WRITE 
GO
