using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleNina
{
    public static class DBcreate
    {
        /// <summary>
        /// 标签表
        /// </summary>
        public static void Label()
        {
            string strsql = "";
            string sql = "";
            for (int i = 0; i < 17; i++)
            {
                sql += $@"
USE MASTER 
CREATE DATABASE bookmark_no_{i}
ON  PRIMARY ( NAME='bookmark_no_{i}', FILENAME='D:\Data\bookmark_no_{i}.mdf' ) 
LOG ON( NAME='bookmark_no_{i}log', FileName='D:\Data\bookmark_no_{i}.ldf' ) 
GO
;
                GO
";
                strsql += $@"
USE MASTER 
drop DATABASE bookmark_no_{i};
                GO";

                Console.WriteLine("创建库：bookmark_no_" + i);
                for (int j = 0; j < 100; j++)
                {
                    sql += $@"

    USE bookmark_no_{i}
    create table[bookmark_{j}](
    [Bid][int] identity(1,1) not null,
	[BUid] [int] not null,
	[BCid] [int] not null,
	[BTid] [int] not null,
	[Bpage] [varchar] (150) not null DEFAULT ((0)),
	[Btime] [datetime] not null DEFAULT (getdate()),
	[Bstatus] [int] not null,
 CONSTRAINT[pk_bookmark_{j}] PRIMARY KEY CLUSTERED
( [Bid] ASC )
WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] ) ON[PRIMARY]
GO

USE [bookmark_no_{i}]
CREATE NONCLUSTERED INDEX [bookmark_no_index1] ON [dbo].[bookmark_{j}]
(
	[BUid] ASC,
	[BCid] ASC,
	[BTid] ASC,
	[Btime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

";
                    Console.WriteLine("库：bookmark_no_" + i + ",表：bookmark_" + j);
                }
            }

            File.WriteAllText(@"d:\sql_lab.txt", sql);

            Console.WriteLine("d:\\sql_lab.txt   完成");
            Console.ReadKey();


        }

        /// <summary>
        /// 影响力
        /// </summary>
        public static void Effect()
        {
            string delsql = "";
            string sql = "";
            for (int i = 0; i < 17; i++)
            {
                sql += $@"
USE MASTER 
CREATE DATABASE effct_no_{i}
ON  PRIMARY ( NAME='effct_no_{i}', FILENAME='D:\Data\effct_no_{i}.mdf' ) 
LOG ON( NAME='effct_no_{i}log', FileName='D:\Data\effct_no_{i}.ldf' ) ;
                GO
";
                delsql += $@"
USE MASTER 
drop DATABASE effct_no_{i};
                GO";

                Console.WriteLine("创建库：effct_no_" + i);
                for (int j = 0; j < 100; j++)
                {
                    sql += $@"

    USE effct_no_{i}
    create table[effect_{j}](
   [Eid] [int] IDENTITY(1,1) NOT NULL,
	[ECid] [int] NOT NULL,
	[EUid] [int] NOT NULL,
	[Eamount] [int] NOT NULL CONSTRAINT [DF_effect_{j}_Eamount]  DEFAULT ((0)),
	[Eshare] [int] NOT NULL CONSTRAINT [DF_effect_{j}_Eshare]  DEFAULT ((0)),
	[Ereward] [int] NOT NULL CONSTRAINT [DF_effect_{j}_Ereward]  DEFAULT ((0)),
	[Erecommand] [int] NOT NULL CONSTRAINT [DF_effect_{j}_Erecommand]  DEFAULT ((0)),
	[Egift] [int] NOT NULL CONSTRAINT [DF_effect_{j}_Egift]  DEFAULT ((0)),
	[Egift_gold] [int] NOT NULL CONSTRAINT [DF_effect_{j}_Egift_gold]  DEFAULT ((0)),
 CONSTRAINT [effect_index_{j}] PRIMARY KEY CLUSTERED 
(
	[Eid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

";
                    Console.WriteLine("库：effct_no_" + i + ",表：effect_" + j);
                }
            }

            File.WriteAllText(@"d:\sql_eff.txt", "   " + sql);

            Console.WriteLine("d:\\sql_eff.txt   完成");
            Console.ReadKey();

        }

        /// <summary>
        /// 增加列
        /// </summary>
        /// <param name="db">数据库，不带下标  effct_no_</param>
        /// <param name="tb">表名，不带下标 effect_</param>
        /// <param name="columnName">要增加的列  Ceffect</param>
        /// <param name="type">要增加的列的类型  int,varchar(200)</param>
        /// <param name="defaults">默认值</param>
        public static string AddClumnWithTableName(string db, string tb, string columnName, string type = "int", string defaults = "")
        {
            string sql = "";
            for (int i = 0; i < 17; i++)
            {
                //                sql += $@" 
                //USE MASTER 
                //CREATE DATABASE {db}{i};
                //                GO 
                //";
                Console.WriteLine($"创建库：{db}" + i);
                for (int j = 0; j < 100; j++)
                {
                    sql += $@"
USE {db}{i}
alter table {tb}{j} add {columnName}  {type}  {(defaults.Length > 0 ? " NOT NULL default " + defaults : " ")} ;
";
                    Console.WriteLine($"库：{db}" + i + $",表：{tb}" + j);
                }
            }
            return sql;
        }

        /// <summary>
        /// 影响力 17-26
        /// </summary>
        public static void UserLog()
        {
            string delsql = "";
            string sql = "";
            for (int i = 17; i < 27; i++)
            {
                sql += $@"
USE MASTER 
CREATE DATABASE user_log_no_{i}
--ON  PRIMARY ( NAME='user_log_no_{i}', FILENAME='D:\Data\user_log_no_{i}.mdf' ) 
--LOG ON( NAME='user_log_no_{i}log', FileName='D:\Data\user_log_no_{i}.ldf' ) ;
                GO
";
                delsql += $@"
USE MASTER 
drop DATABASE user_log_no_{i};
                GO";

                Console.WriteLine("创建库：user_log_no_" + i);
                for (int j = 0; j < 100; j++)
                {
                    sql += $@"

    USE user_log_no_{i}
    CREATE TABLE [dbo].[user_log{j}](
	[Lid] [bigint] NOT NULL CONSTRAINT [DF_user_log{j}_Lid]  DEFAULT (0),
	[LUid] [int] NOT NULL CONSTRAINT [DF_user_log{j}_LUid]  DEFAULT (0),
	[Ltarget] [int] NOT NULL CONSTRAINT [DF_user_log{j}_Ltarget]  DEFAULT (0),
	[Lcontent] [nvarchar](50) NOT NULL CONSTRAINT [DF_user_log{j}_Lcontent]  DEFAULT (''),
	[Ltime] [datetime] NOT NULL CONSTRAINT [DF_user_log{j}_Ltime]  DEFAULT (getdate()),
	[Laction] [tinyint] NOT NULL CONSTRAINT [DF_user_log{j}_Laction]  DEFAULT (0),
	[Lstatus] [tinyint] NOT NULL CONSTRAINT [DF_user_log{j}_Lstatus]  DEFAULT (1),
	[Ltid] [int] NOT NULL CONSTRAINT [DF_user_log{j}_Ltid]  DEFAULT (0),
 CONSTRAINT [PK_user_log{j}] PRIMARY KEY CLUSTERED 
(
	[Lid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

";
                    Console.WriteLine("库：user_log_no_" + i + ",表：user_log" + j);
                }
            }

            File.WriteAllText(@"d:\sql_uselog.txt", "   " + sql);

            Console.WriteLine("d:\\sql_uselog.txt   完成");
            Console.ReadKey();

        }

        /// <summary>
        /// 金币
        /// </summary>
        public static void Coin()
        {
            string delsql = "";
            string sql = "";
            for (int i = 0; i < 17; i++)
            {
                sql += $@"
USE MASTER
CREATE DATABASE coin_{i}
ON  PRIMARY ( NAME='coin_{i}', FILENAME='D:\Data\coin_{i}.mdf' ) 
LOG ON( NAME='coin_{i}_log', FileName='D:\Data\coin_{i}.ldf' ) ;
GO
";
                delsql += $@"
USE MASTER 
drop DATABASE coin_{i};
                GO";

                Console.WriteLine("创建库：coin_" + i);
                for (int j = 0; j < 100; j++)
                {
                    sql += $@"
USE [coin_{i}]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[coin_user_{j}](
	[Cid] [int] IDENTITY(1,1) NOT NULL,
	[CUid] [int] NOT NULL,
	[Cgottime] [datetime] NOT NULL CONSTRAINT [DF_coin_user_{j}_Cgottime]  DEFAULT (getdate()),
	[Cgotcoin] [int] NOT NULL CONSTRAINT [DF_coin_user_{j}_Cgotcoin]  DEFAULT ((0)),
	[Cremaincoin] [int] NOT NULL CONSTRAINT [DF_coin_user_{j}_Cremaincoin]  DEFAULT ((0)),
	[Coverduetime] [datetime] NOT NULL CONSTRAINT [DF_coin_user_{j}_Coverduetime]  DEFAULT (getdate()+(30)),
	[Cgotmode] [int] NOT NULL CONSTRAINT [DF_coin_user_{j}_Cgotmode]  DEFAULT (({j})),
	[Cgotdes] [nvarchar](50) NOT NULL CONSTRAINT [DF_coin_user_{j}_Cgotdes]  DEFAULT (''),
 CONSTRAINT [PK_coin_user_{j}] PRIMARY KEY CLUSTERED 
(
	[Cid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'coin_user_{j}', @level2type=N'COLUMN',@level2name=N'Cid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'coin_user_{j}', @level2type=N'COLUMN',@level2name=N'CUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'获得时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'coin_user_{j}', @level2type=N'COLUMN',@level2name=N'Cgottime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'获取的金币数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'coin_user_{j}', @level2type=N'COLUMN',@level2name=N'Cgotcoin'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'剩余金币' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'coin_user_{j}', @level2type=N'COLUMN',@level2name=N'Cremaincoin'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'过期时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'coin_user_{j}', @level2type=N'COLUMN',@level2name=N'Coverduetime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'获取方式 1,任务获得   2,每日登陆   3,运营活动   4,充值赠送' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'coin_user_{j}', @level2type=N'COLUMN',@level2name=N'Cgotmode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'获得具体描述（具体任务之类的）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'coin_user_{j}', @level2type=N'COLUMN',@level2name=N'Cgotdes'
GO
";
                    Console.WriteLine("库：coin_" + i + ",表：coin_user_0" + j);
                }
            }

            File.WriteAllText(@"d:\\sql_coin.txt", "   " + sql);

            Console.WriteLine("d:\\sql_coin.txt   完成");
            Console.ReadKey();

        }

        public static void Lnumber2()
        {
            //alter table [dbo].[comic_log0] add Lnumber2 int not null default 0
            string sql = "";
            for (int i = 0; i < 17; i++)
            {
                sql += $@"
USE comic_log_no_{i}
";
                Console.WriteLine("增加字段：Lnumber2");
                for (int j = 0; j < 100; j++)
                {
                    sql += $@"
alter table [dbo].[comic_log{j}] add Lnumber2 int not null default 0
";
                    Console.WriteLine("库：comic_log_no_" + i + ",表：comic_log" + j);
                }
            }

            File.WriteAllText(@"d:\\sql_lnumber2.txt", "   " + sql);

            Console.WriteLine("d:\\sql_lnumber2.txt   完成");
            Console.ReadKey();
        }
    }
}
