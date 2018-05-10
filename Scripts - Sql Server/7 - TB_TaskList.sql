USE [TaskManager]
GO

/****** Object:  Table [dbo].[TB_TaskList]    Script Date: 02/05/2018 09:05:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TB_TaskList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[mainTaskID] [int] NULL,
	[taskID] [int] NULL,
	[requeriment] [bit] NULL,
	[dependency] [bit] NULL,
 CONSTRAINT [PK_TB_TaskList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TB_TaskList]  WITH CHECK ADD  CONSTRAINT [FK_TB_TaskList_TB_Task] FOREIGN KEY([mainTaskID])
REFERENCES [dbo].[TB_Task] ([ID])
GO

ALTER TABLE [dbo].[TB_TaskList] CHECK CONSTRAINT [FK_TB_TaskList_TB_Task]
GO

ALTER TABLE [dbo].[TB_TaskList]  WITH CHECK ADD  CONSTRAINT [FK_TB_TaskList_TB_Task1] FOREIGN KEY([taskID])
REFERENCES [dbo].[TB_Task] ([ID])
GO

ALTER TABLE [dbo].[TB_TaskList] CHECK CONSTRAINT [FK_TB_TaskList_TB_Task1]
GO

