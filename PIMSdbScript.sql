USE [PIMSdb]
GO

/****** Object:  Table [dbo].[StudentInformationTable]    Script Date: 6/18/2018 7:40:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[StudentInformationTable](
	[District_Code] [varchar](10) NOT NULL,
	[School_Year_Date] [date] NOT NULL,
	[Student_Id] [varchar](10) NOT NULL,
	[Reporting_Date] [date] NOT NULL,
	[Category_Set_code] [varchar](10) NOT NULL DEFAULT ('ACT16'),
	[Measure_type] [varchar](10) NOT NULL DEFAULT ('COUNT'),
	[Act16_fund_Category] [int] NOT NULL,
 CONSTRAINT [primarykeyconstraint] PRIMARY KEY CLUSTERED 
(
	[District_Code] ASC,
	[School_Year_Date] ASC,
	[Student_Id] ASC,
	[Reporting_Date] ASC,
	[Category_Set_code] ASC,
	[Measure_type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


