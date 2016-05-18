using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class SAPUserInfoDAL : BaseFind<SAP_UserInfo>
    {
        public static Dictionary<string, string> dir_SAP_NumberFormat
        {
            get
            {
                Dictionary<string, string> dir = new Dictionary<string, string>();
                dir.Add(string.Empty, "小数点是逗号:N.NNN,NN");
                dir.Add("X", "小数点是句号:N,NNN.NN");
                dir.Add("Y", "小数点是 N NNN NNN,NN");
                return dir;
            }
        }

        public static Dictionary<string, string> dir_DateFormat
        {
            get {
                Dictionary<string, string> dir = new Dictionary<string, string>();
                dir.Add("1","DD.MM.YYYY");
                dir.Add("2", "MM/DD/YYYY");
                dir.Add("4", "YYYY.MM.DD");
                dir.Add("5", "YYYY/MM/DD");
                dir.Add("3", "MM-DD-YYYY");
                dir.Add("6", "YYYY-MM-DD");
                dir.Add("7", "GYY.MM.DD(Japanese Date)");
                dir.Add("8", "GYY/MM/DD(Japanese Date)");
                dir.Add("9", "GYY-MM-DD(Japanese Date)");
                dir.Add("A", "YYYY/MM/DD(Islamic Date 1)");
                dir.Add("B", "YYYY/MM/DD(Islamic Date 2)");
                dir.Add("C", "YYYY/MM/DD(Iranian Date)");
                return dir;
            }
        }

        public static Dictionary<string, string> dir_TimeFormat
        {
            get {
                Dictionary<string, string> dir = new Dictionary<string, string>();
                dir.Add("0","24小时格式(例如:12:08:10)");
                dir.Add("1","12小时格式(例如:12:05:10 PM)");
                dir.Add("2", "12小时格式(例如:12:05:10 pm)");
                dir.Add("3","从0到11的小时(例如:00:05:10 PM)");
                dir.Add("4","从0到11的小时(例如:00:05:10 pm)");
                return dir;
            }
        }

        public static Dictionary<string, string> dri_SAPUserType
        {
            get {
                Dictionary<string, string> dir = new Dictionary<string, string>();
                dir.Add("A","对话");
                dir.Add("B","系统用户(内部RFC和后台处理)");
                dir.Add("C","通讯用户(外部RFC)");
                dir.Add("L","参考用户");
                dir.Add("S","服务用户");
                return dir;
            }
        }

        


        public int AddSapUserInfo(SAP_UserInfo entity)
        {
            return Add(entity);
        }

        public int UpdateUserInfo(SAP_UserInfo entity)
        {
            using (IAMEntities db = new IAMEntities())
            {
                db.SAP_UserInfo.Attach(entity);
                db.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
                return db.SaveChanges();
            }
        }

        public int DeleteUserInfo(string id)
        {
            return NonExecute(db =>
            {
                var entity = db.SAP_UserInfo.FirstOrDefault(item => item.BAPIBNAME == id);
                if (entity != null)
                    db.DeleteObject(entity);
            });
        }

        /// <summary>
        /// 添加或更新 SAP_UserInfo信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int CreateOrUpdate(SAP_UserInfo entity, List<SAP_User_Role> UserRoleList,List<SAP_Parameters> parms)
        {
            try
            {
                SAP_UserInfo module = NonExecute<SAP_UserInfo>(db =>
                {
                    return db.SAP_UserInfo.FirstOrDefault(item => item.BAPIBNAME == entity.BAPIBNAME);
                });
                if (module != null)
                {
                    module.DEPARTMENT_NAME = entity.DEPARTMENT_NAME;
                    
                    module.USERTYPE = entity.USERTYPE;
                   
                    
                    module.LANGUAGE = entity.LANGUAGE;
                    module.MOBLIE_NUMBER = entity.MOBLIE_NUMBER;
                   
                    module.USERTYPE = entity.USERTYPE;
                    module.UCLASSTYPE = entity.UCLASSTYPE;
                    module.PASSWORD = entity.PASSWORD;
                    module.PASSWORD2 = entity.PASSWORD2;
                    module.START_DATE = entity.START_DATE;
                    module.END_DATE = entity.END_DATE;
                    module.LOGIN_LANGUAGE = entity.LOGIN_LANGUAGE;
                    module.p2 = module.p2;
                    module.DECIMAL_POINT_FORMAT = module.DECIMAL_POINT_FORMAT;
                    module.DATE_FORMAT = module.DATE_FORMAT;
                    module.TIME_FORMAT = module.TIME_FORMAT;
                    module.OUTPUT_EQUIMENT = module.OUTPUT_EQUIMENT;
                    module.NOWTIME_EQUIMENT = module.NOWTIME_EQUIMENT;
                    module.OUTPUTED_DELETE = module.OUTPUTED_DELETE;
                    module.USER_TIMEZONE = module.USER_TIMEZONE;
                    module.SYSTEM_TIMEZONE = module.SYSTEM_TIMEZONE;
                    module.PARAMENTERID = module.PARAMENTERID;
                    module.PARAMENTERVALUE = module.PARAMENTERVALUE;
                    module.PARAMETERTEXT = module.PARAMETERTEXT;
                    UpdateUserInfo(module);
                }
                else
                    AddSapUserInfo(entity);
                new SAPUserRoleDAL().CreateOrUpdate(UserRoleList,entity.BAPIBNAME);
                new SAP_ParametersDAL().IamNewAdd(parms.Where(item=>item.isdr==2).ToList());
                new SAP_ParametersDAL().NewUpdate(parms.Where(item=>item.isdr==1).ToList());
                return 1;
            }
            catch (Exception ex)
            {
#if DEBUG
                throw ex;
#else
                return 0;
#endif

            }
        }

        /// <summary>
        /// 添加sap账号信息
        /// 2014-11-12 haiboax 第一次同步时直接添加iam系统没有的账号；以后同步时，以同步冲突方式解决 
        /// </summary>
        /// <param name="entity"></param>
        public void SyncSapUserInfomation(SAP_UserInfo entity)
        {
            SAP_UserInfo module = NonExecute<SAP_UserInfo>(db =>
            {
                return db.SAP_UserInfo.FirstOrDefault(item => item.BAPIBNAME == entity.BAPIBNAME);
            });
            if (module != null)
                AddDeference(module, entity, Unitity.SystemType.SAP, "BAPIBNAME", module.BAPIBNAME, module.BAPIBNAME);
            else
            {
                if (base.IsFirstTime)
                {
                    AddSapUserInfo(entity);
                }
                else
                {
                    if(new Sys_UserName_ConflictResolutionDAL().GetOneUser(entity.BAPIBNAME,"SAP")==null)
                    new DeferencesSlution.UserRoleAlignmentValue().IsAddNewNotInIAM<SAP_UserInfo>(entity, entity.BAPIBNAME, Unitity.SystemType.SAP, "SAP_UserInfo","","IAM系统无该账号","源系统新增账号");  
                }
            }
        }


        public SAP_UserInfo GetOneTCUser(string BAPIBNAME)
        {
            return NonExecute<SAP_UserInfo>(db =>
            {
                return db.SAP_UserInfo.FirstOrDefault(item => item.BAPIBNAME.Trim() == BAPIBNAME);
            });
        }

        /// <summary>
        /// 根据用户工号，姓名，工号进行查询
        /// </summary>
        /// <param name="PageSize">每页的条数</param>
        /// <param name="PageIndex">页数</param>
        /// <param name="departmentname">部门名称</param>
        /// <param name="name">姓，名</param>
        /// <param name="UserCode">工号</param>
        /// <param name="count">查询的集合数</param>
        /// <returns></returns>
        public List<SAP_UserInfo> GetSapUserInfo(int PageSize, int PageIndex, string UserName, string name, string UserCode, out int count)
        {
            List<SAP_UserInfo> listuserinfo = NonExecute<List<SAP_UserInfo>>(db =>
            {
                return db.SAP_UserInfo.ToList();
            });
          
            if (!string.IsNullOrEmpty(UserName))
              listuserinfo=  listuserinfo.Where(item => item.BAPIBNAME.Contains(UserName)).ToList();
            if (!string.IsNullOrEmpty(name))
             listuserinfo=   listuserinfo.Where(item => item.LASTNAME.Contains(name) || item.FIRSTNAME.Contains(name)).ToList();
            if (!string.IsNullOrEmpty(UserCode))
              listuserinfo=  listuserinfo.Where(item => item.BAPIBNAME == UserCode).ToList();
            count = listuserinfo.Count;
            return listuserinfo.OrderByDescending(ite => ite.BAPIBNAME).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

    }
}
