



















// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `Core`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=.;Initial Catalog=WeiXinYiSheng;User ID=mm;Password=mm`
//     Schema:                 ``
//     Include Views:          `False`



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace Module.Models
{

	public partial class CoreDB : Database
	{
		public CoreDB() 
			: base("Core")
		{
			CommonConstruct();
		}

		public CoreDB(string connectionStringName) 
			: base(connectionStringName)
		{
			CommonConstruct();
		}
		
		partial void CommonConstruct();
		
		public interface IFactory
		{
			CoreDB GetInstance();
		}
		
		public static IFactory Factory { get; set; }
        public static CoreDB GetInstance()
        {
			//http://stackoverflow.com/questions/7052350/how-to-create-a-dal-using-petapoco/9995413#9995413
            //If you are using this in a web application then you should instantiate one PetaPoco database per request.

			/*if (_instance!=null)
				return _instance;
				
			if (Factory!=null)
				return Factory.GetInstance();
			else*/
				return new CoreDB();
        }

		[ThreadStatic] static CoreDB _instance;
		
		public override void OnBeginTransaction()
		{
			if (_instance==null)
				_instance=this;
		}
		
		public override void OnEndTransaction()
		{
			if (_instance==this)
				_instance=null;
		}
        

		public class Record<T> where T:new()
		{
			public static CoreDB repo { get { return CoreDB.GetInstance(); } }
			public bool IsNew() { return repo.IsNew(this); }
			public object Insert() { return repo.Insert(this); }

			public void Save() { repo.Save(this); }
			public int Update() { return repo.Update(this); }

			public int Update(IEnumerable<string> columns) { return repo.Update(this, columns); }
			public static int Update(string sql, params object[] args) { return repo.Update<T>(sql, args); }
			public static int Update(Sql sql) { return repo.Update<T>(sql); }
			public int Delete() { return repo.Delete(this); }
			public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
			public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
			public static int Delete(object primaryKey) { return repo.Delete<T>(primaryKey); }
			public static bool Exists(object primaryKey) { return repo.Exists<T>(primaryKey); }
			public static bool Exists(string sql, params object[] args) { return repo.Exists<T>(sql, args); }
			public static T SingleOrDefault(object primaryKey) { return repo.SingleOrDefault<T>(primaryKey); }
			public static T SingleOrDefault(string sql, params object[] args) { return repo.SingleOrDefault<T>(sql, args); }
			public static T SingleOrDefault(Sql sql) { return repo.SingleOrDefault<T>(sql); }
			public static T FirstOrDefault(string sql, params object[] args) { return repo.FirstOrDefault<T>(sql, args); }
			public static T FirstOrDefault(Sql sql) { return repo.FirstOrDefault<T>(sql); }
			public static T Single(object primaryKey) { return repo.Single<T>(primaryKey); }
			public static T Single(string sql, params object[] args) { return repo.Single<T>(sql, args); }
			public static T Single(Sql sql) { return repo.Single<T>(sql); }
			public static T First(string sql, params object[] args) { return repo.First<T>(sql, args); }
			public static T First(Sql sql) { return repo.First<T>(sql); }
			public static List<T> Fetch(string sql, params object[] args) { return repo.Fetch<T>(sql, args); }
			public static List<T> Fetch(Sql sql) { return repo.Fetch<T>(sql); }
			public static List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args) { return repo.Fetch<T>(page, itemsPerPage, sql, args); }
			public static List<T> Fetch(long page, long itemsPerPage, Sql sql) { return repo.Fetch<T>(page, itemsPerPage, sql); }
			public static List<T> SkipTake(long skip, long take, string sql, params object[] args) { return repo.SkipTake<T>(skip, take, sql, args); }
			public static List<T> SkipTake(long skip, long take, Sql sql) { return repo.SkipTake<T>(skip, take, sql); }
			public static Page<T> Page(long page, long itemsPerPage, string sql, params object[] args) { return repo.Page<T>(page, itemsPerPage, sql, args); }
			public static Page<T> Page(long page, long itemsPerPage, Sql sql) { return repo.Page<T>(page, itemsPerPage, sql); }
			public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
			public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }

		}

	}
	



    
	[TableName("Sys_AdminUser")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class Sys_AdminUser : CoreDB.Record<Sys_AdminUser>  
    {



		[Column] public int Id { get; set; }





		[Column] public string LoginUserName { get; set; }





		[Column] public string NickName { get; set; }





		[Column] public string PassWord { get; set; }





		[Column] public DateTime CreateDateTime { get; set; }



	}

    
	[TableName("Log")]


	[PrimaryKey("log_id")]



	[ExplicitColumns]
    public partial class Log : CoreDB.Record<Log>  
    {



		[Column] public int log_id { get; set; }





		[Column] public DateTime LogDate { get; set; }





		[Column] public string descript { get; set; }





		[Column] public short logtype { get; set; }





		[Column] public string log_info { get; set; }





		[Column] public string Module_Name { get; set; }



	}

    
	[TableName("ExchangeLog")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class ExchangeLog : CoreDB.Record<ExchangeLog>  
    {



		[Column] public int Id { get; set; }





		[Column] public string OpenId { get; set; }





		[Column] public long? Mobile { get; set; }





		[Column] public string NickName { get; set; }





		[Column] public decimal Score { get; set; }





		[Column] public decimal Money { get; set; }





		[Column] public int PayStatus { get; set; }





		[Column] public DateTime CreateDatetime { get; set; }





		[Column] public DateTime? PayDateTime { get; set; }





		[Column] public string PayUser { get; set; }



	}

    
	[TableName("AutoReplyContent")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class AutoReplyContent : CoreDB.Record<AutoReplyContent>  
    {



		[Column] public int Id { get; set; }





		[Column] public string UpKey { get; set; }





		[Column] public string ReplyContent { get; set; }





		[Column] public string ResponseMsgType { get; set; }





		[Column] public DateTime CreateDatetime { get; set; }





		[Column] public int IsDelete { get; set; }





		[Column] public string Remark { get; set; }



	}

    
	[TableName("DoctorInfo")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class DoctorInfo : CoreDB.Record<DoctorInfo>  
    {



		[Column] public int Id { get; set; }





		[Column] public string DoctorName { get; set; }





		[Column] public string Remark { get; set; }





		[Column] public DateTime CreateDatetime { get; set; }





		[Column] public string CreatorOpenId { get; set; }



	}

    
	[TableName("DoctorWorkSchedule")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class DoctorWorkSchedule : CoreDB.Record<DoctorWorkSchedule>  
    {



		[Column] public int Id { get; set; }





		[Column] public int DoctorId { get; set; }





		[Column] public DateTime WorkDateTime { get; set; }





		[Column] public string DoctorName { get; set; }





		[Column] public int WorkShortDate { get; set; }





		[Column] public int DayTime { get; set; }





		[Column] public DateTime CreateDateTime { get; set; }





		[Column] public string CreatorOpenId { get; set; }



	}

    
	[TableName("OrderInfo")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class OrderInfo : CoreDB.Record<OrderInfo>  
    {



		[Column] public int Id { get; set; }





		[Column] public string BuyerOpenId { get; set; }





		[Column] public string OrderInfoJson { get; set; }





		[Column] public DateTime CreateDateTime { get; set; }





		[Column] public string OrderId { get; set; }





		[Column] public int OrderStatus { get; set; }





		[Column] public int OrderTotalPrice { get; set; }





		[Column] public int OrderCreateTime { get; set; }





		[Column] public DateTime OrderCreateDateTime { get; set; }





		[Column] public string BuyerNickName { get; set; }





		[Column] public string ProductId { get; set; }





		[Column] public string ProductName { get; set; }





		[Column] public int ProductPrice { get; set; }



	}

    
	[TableName("ExportsLiShi")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class ExportsLiShi : CoreDB.Record<ExportsLiShi>  
    {



		[Column] public int Id { get; set; }





		[Column] public int ExpertsSysUserId { get; set; }





		[Column] public int LiShiSysUserId { get; set; }





		[Column] public DateTime CreateDateTime { get; set; }



	}

    
	[TableName("Sys_Module")]


	[PrimaryKey("MODULE_ID")]



	[ExplicitColumns]
    public partial class Sys_Module : CoreDB.Record<Sys_Module>  
    {



		[Column] public int MODULE_ID { get; set; }





		[Column] public string MODULE_NAME { get; set; }





		[Column] public int? PARENT_MODULE_ID { get; set; }





		[Column] public byte IS_DISPLAY { get; set; }





		[Column] public short? ORDER_ID { get; set; }





		[Column] public short? Banner_Button_Width { get; set; }





		[Column] public string Link_Path { get; set; }





		[Column] public byte Link_Type { get; set; }





		[Column] public string Link_Target { get; set; }





		[Column] public byte Create_Table { get; set; }





		[Column] public int? Template_ID { get; set; }





		[Column] public string Description { get; set; }





		[Column] public string Business_Table { get; set; }





		[Column] public bool Is_Sys_File { get; set; }





		[Column] public bool Is_Original_Data { get; set; }





		[Column] public string Creator_Area_Code { get; set; }



	}

    
	[TableName("UserExceptionsRecord")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class UserExceptionsRecord : CoreDB.Record<UserExceptionsRecord>  
    {



		[Column] public int Id { get; set; }





		[Column] public int UserId { get; set; }





		[Column] public int ExceptionsType { get; set; }





		[Column] public DateTime CreateDateTime { get; set; }



	}

    
	[TableName("ScoreMoneyConfig")]


	[ExplicitColumns]
    public partial class ScoreMoneyConfig : CoreDB.Record<ScoreMoneyConfig>  
    {



		[Column] public int UserType { get; set; }





		[Column] public int UserLevel { get; set; }





		[Column] public int Score { get; set; }





		[Column] public int Money { get; set; }





		[Column] public string Remark { get; set; }



	}

    
	[TableName("MyCollectMedicine")]


	[ExplicitColumns]
    public partial class MyCollectMedicine : CoreDB.Record<MyCollectMedicine>  
    {



		[Column] public int Id { get; set; }





		[Column] public int UserId { get; set; }





		[Column] public int PointId { get; set; }





		[Column] public DateTime CreateDateTime { get; set; }



	}

    
	[TableName("AddScoreLog")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class AddScoreLog : CoreDB.Record<AddScoreLog>  
    {



		[Column] public int Id { get; set; }





		[Column] public string OpenId { get; set; }





		[Column] public string OrderId { get; set; }





		[Column] public int OrderTotalPrice { get; set; }





		[Column] public decimal Score { get; set; }





		[Column] public int ExchangeStatus { get; set; }





		[Column] public DateTime CreateDateTime { get; set; }





		[Column] public string FromOrderId { get; set; }



	}

    
	[TableName("Sys_Point")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class Sys_Point : CoreDB.Record<Sys_Point>  
    {



		[Column] public int Id { get; set; }





		[Column] public int ModuleId { get; set; }





		[Column] public string Title { get; set; }





		[Column] public string Content { get; set; }





		[Column] public DateTime CreateDateTime { get; set; }





		[Column] public DateTime? UpdateDateTime { get; set; }





		[Column] public int ZanCount { get; set; }





		[Column] public int SeeCount { get; set; }



	}

    
	[TableName("UserOpLog")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class UserOpLog : CoreDB.Record<UserOpLog>  
    {



		[Column] public int Id { get; set; }





		[Column] public int UserId { get; set; }





		[Column] public DateTime? CreateDateTime { get; set; }





		[Column] public int PointId { get; set; }





		[Column] public int OptionType { get; set; }



	}

    
	[TableName("CustomerManager")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class CustomerManager : CoreDB.Record<CustomerManager>  
    {



		[Column] public int Id { get; set; }





		[Column] public string Name { get; set; }





		[Column] public long? Mobile { get; set; }



	}

    
	[TableName("WhiteList")]


	[PrimaryKey("Mobile", autoIncrement=false)]

	[ExplicitColumns]
    public partial class WhiteList : CoreDB.Record<WhiteList>  
    {



		[Column] public long Mobile { get; set; }





		[Column] public string OpenId { get; set; }





		[Column] public string NickName { get; set; }



	}

    
	[TableName("MaterialNews")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class MaterialNews : CoreDB.Record<MaterialNews>  
    {



		[Column] public int Id { get; set; }





		[Column] public string Media_Id { get; set; }





		[Column] public string MediaJson { get; set; }





		[Column] public DateTime CreateDateTime { get; set; }





		[Column] public DateTime? UpdateDateTime { get; set; }



	}

    
	[TableName("Area")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class Area : CoreDB.Record<Area>  
    {



		[Column] public int Id { get; set; }





		[Column] public string AreaName { get; set; }





		[Column] public int ParentId { get; set; }





		[Column] public int? AreaLevel { get; set; }



	}

    
	[TableName("Sys_User")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class Sys_User : CoreDB.Record<Sys_User>  
    {



		[Column] public int Id { get; set; }





		[Column] public string OpenId { get; set; }





		[Column] public long Mobile { get; set; }





		[Column] public string NickName { get; set; }





		[Column] public string HeadImgUrl { get; set; }





		[Column] public string PassWord { get; set; }





		[Column] public string Email { get; set; }





		[Column] public string CompanyName { get; set; }





		[Column] public int UserType { get; set; }





		[Column] public int? UserLevel { get; set; }





		[Column] public string Remark { get; set; }





		[Column] public int ParentId { get; set; }





		[Column] public int ApproveFlag { get; set; }





		[Column] public decimal Score { get; set; }





		[Column] public decimal LastScore { get; set; }





		[Column] public int? Province { get; set; }





		[Column] public int? City { get; set; }





		[Column] public string UserInfoJson { get; set; }





		[Column] public int QrCodeScene_id { get; set; }





		[Column] public int IsDelete { get; set; }





		[Column] public DateTime CreateDateTime { get; set; }





		[Column] public int? CustomerManagerId { get; set; }





		[Column] public DateTime? UpdateDateTime { get; set; }



	}


}



