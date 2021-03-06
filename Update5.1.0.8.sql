USE [Db_Asnad]
GO
/****** Object:  Table [dbo].[tblRequest]    Script Date: 2018/02/22 03:12:24 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblRequest](
	[RequestID] [int] IDENTITY(1,1) NOT NULL,
	[ReuestName] [nvarchar](max) NULL,
	[RequestDate] [nvarchar](max) NULL,
	[Request] [nvarchar](max) NULL,
	[AdminID] [int] NULL,
 CONSTRAINT [PK_tblRequest] PRIMARY KEY CLUSTERED 
(
	[RequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[tblRequest]  WITH CHECK ADD  CONSTRAINT [FK_tblRequest_tblAdmin] FOREIGN KEY([AdminID])
REFERENCES [dbo].[tblAdmin] ([AdminID])
GO
ALTER TABLE [dbo].[tblRequest] CHECK CONSTRAINT [FK_tblRequest_tblAdmin]
GO
