using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAMEntityDAL;
using BaseDataAccess;
using System.Data.SqlClient;

namespace IAM.BLL
{
    public class Temp : IEqualityComparer<HEC_User>
    {
        public Temp() { }
        public bool Equals(HEC_User x, HEC_User y)
        {
            return x.USER_NAME == y.USER_NAME;
        }

        public int GetHashCode(HEC_User x)
        {
            return 0;
        }
    }

    public class HECSyncServices
    {
        HECRoleDAL _hecIAMRoles = new HECRoleDAL();
        HECCompanyInfoDAL _hecIAMCompanys = new HECCompanyInfoDAL();
        HECUserInfoDAL _hecIAMUserRoles = new HECUserInfoDAL();
        /// <summary>
        /// HEC ��ɫͬ������
        /// </summary>
        string hecName = "shacHec";
        string hecPassword = "shacHec";
        public void HECSyncRole(out int allcount, out int Okcount)
        {

            WebReference.auto_service _servcices = new WebReference.auto_service();

            var x = _servcices.execute(new WebReference.parameter() { ws_user_name = hecName, ws_password = hecPassword }).role_records.ToList();
            allcount = x.Count;
            Okcount = 0;
            foreach (var item in x)
            {
                HEC_Role entity = new HEC_Role()
                {
                    ROLE_CODE = item.role_code,
                    ROLE_NAME = item.role_name,
                    DESCRIPTION = item.description,
                    START_DATE = item.start_date,
                    END_DATE = ""

                };
                try
                {
                    _hecIAMRoles.SyncHECRole(entity); Okcount++;
                }
                catch
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// HEC ��˾��Ϣͬ������
        /// </summary>
        public void HECSyncCoompany(out int allcount, out int OKcount)
        {
            WebReference1.auto_service _autoservices = new WebReference1.auto_service();
            var x = _autoservices.execute(new WebReference1.parameter() { ws_user_name = hecName, ws_password = hecPassword }).company_records.ToList();
            allcount = x.Count;
            OKcount = 0;
            foreach (var item in x)
            {
                HEC_Company_Info entity = new HEC_Company_Info()
                {
                    COMPANY_CODE = item.company_code,
                    COMPANY_FULL_NAME = item.company_full_name,
                    COMPANY_SHORT_NAME = item.company_short_name,
                    END_DATE_ACTIVE = "",
                    START_DATE_ACRIVE = "",
                    START_DATE = item.start_date_active,
                    End_DATE = ""

                };
                try
                {
                    _hecIAMCompanys.SyncHECCompany(entity);
                    OKcount++;
                }
                catch
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// ͬ��hec�û���Ϣ
        /// </summary>
        public void HECSyncUserInfo(List<HEC_User> HecUserList)
        {
            using (IAMEntities db = new IAMEntities())
            {
                db.ExecuteStoreCommand("UPDATE dbo.HEC_User SET END_DATE='2099-12-31' WHERE END_DATE IS NULL UPDATE dbo.HEC_User_Info SET END_DATE='2099-12-31' WHERE END_DATE IS NULL OR END_DATE=''");
                db.SaveChanges();
            }

            var HecUserList1 = new List<HEC_User>();//HecUserList.Distinct(new Temp()).ToList();
            int flag = 0;
            foreach (var x in HecUserList)
            {
                if (flag == 0)
                    HecUserList1.Add(x);
                else
                {
                    var tmp = HecUserList1.FirstOrDefault(item => item.User_CD == x.User_CD);
                    if (tmp == null)
                        HecUserList1.Add(x);
                }
                flag++;

            }
            int count;
            var listold = new HECUserDAL().GetHECUser(null, null, int.MaxValue, 1, out count);
            System.Text.StringBuilder stbsql = new System.Text.StringBuilder();

            foreach (var item in HecUserList1)
            {
                
                stbsql.Append(string.Format("update HEC_User set START_DATE='{0}' where User_CD='{1}' ", item.START_DATE, item.User_CD));
            }

            if (!string.IsNullOrEmpty(stbsql.ToString()))
            {
                try
                {
                    using (IAMEntities db = new IAMEntities())
                    {
                        db.ExecuteStoreCommand(stbsql.ToString());
                        db.SaveChanges();
                    }
                }
                catch
                {

                }
            }

            foreach (var item in HecUserList1)
            {
                new HECUserDAL().SyncUser(item);                
            }
            int count1;
            var listiamuserrole = new HECUserInfoDAL().GetHecUserInfo(int.MaxValue, 1, string.Empty, string.Empty, out count1);
            var listiamrole = new HECRoleDAL().GetHECRole(int.MaxValue, 1, out count1);
            foreach (var i in listold)
            {
                
                var tmp = HecUserList1.FirstOrDefault(it => it.User_CD.Trim() == i.User_CD.Trim());
                if (tmp == null)
                {
                    var tmprole = listiamuserrole.Where(item => item.USER_NAME == i.User_CD);

                    new IAMEntityDAL.DeferencesSlution.UserRoleAlignmentValue().IsAddNewNotInIAM<HEC_User>(i, i.User_CD, Unitity.SystemType.HEC, "HEC_User", "", "IAM系统有该账号", "源系统中无该账号");

                    foreach (var x in tmprole)
                    {
                        var role = listiamrole.FirstOrDefault(item => item.ROLE_CODE == x.ROLE_CODE);
                        if (new IAMEntityDAL.Sys_UserName_ConflictResolutionDAL().GetOne(x.USER_NAME, "源系统中无该组权限", "IAM系统有该组权限", "HEC", x.ROLE_CODE) == null)
                            new IAMEntityDAL.DeferencesSlution.UserRoleAlignmentValue().IsAddNewNotInIAM<HEC_User_Info>(x, x.USER_NAME, Unitity.SystemType.AD, "HEC_User_Info", role.ROLE_NAME, "IAM系统有该组权限", "源系统中无该组权限");
                    }
                }
            }

            
        }



        /// <summary>
        /// 2014-11-12 haiboax 
        /// </summary>
        /// <param name="allcount"></param>
        /// <param name="OKcount"></param>
        public void HECSyncUserRole(out int allcount, out int OKcount)
        {
            try
            {
                WebReference2.auto_service _autoservices = new WebReference2.auto_service();
                _autoservices.Timeout = 2000;
                var x = _autoservices.execute(new WebReference2.parameter() { ws_user_name = hecName, ws_password = hecPassword }).user_records.ToList();
                allcount = x.Count;
                OKcount = 0;
                List<HEC_User> listuser = new List<HEC_User>();
                List<HEC_User_Info> listrole = new List<HEC_User_Info>();
                foreach (var item in x)
                {
                        HEC_User_Info entity = new HEC_User_Info()
                        {
                            COMPANY_CODE = item.company_code,
                            DESCRIPTION = item.description,
                            EMPLOYEE_CODE = item.employee_code,
                            EMPLOYEE_NAME = item.employee_name,
                            END_DATE = string.IsNullOrEmpty(item.end_date)==false ? item.end_date : "2099-12-31",
                            ROLE_CODE = item.role_code,
                            ROLE_END_DATE = item.role_end_date,
                            ROLE_START_DATE = item.role_start_date,
                            ID = Guid.NewGuid(),
                            IsSync = 1,
                            SyncDate = DateTime.Now,
                            ERROR_MSG = "",
                            USER_NAME = item.user_name,
                            START_DATE = string.IsNullOrEmpty(item.start_date)==false ? item.start_date : "1900-1-1",
                            password_lifespan_access = item.password_lifespan_access,
                            password_lifespan_days = item.password_lifespan_days,
                            frozen_date = item.frozen_date,
                            frozen_flag = item.frozen_flag,
                            isdr = 0

                        };
                    

                    HEC_User UserEntity = new HEC_User()
                    {
                        User_CD = item.user_name,
                        USER_CODE = item.employee_code,
                        START_DATE = string.IsNullOrEmpty(item.start_date) ? Convert.ToDateTime("1900-1-1") : Convert.ToDateTime(item.start_date),
                        END_DATE = string.IsNullOrEmpty(item.end_date) ? Convert.ToDateTime("2099-12-31") : Convert.ToDateTime(item.end_date),
                        DESCRIPTION = item.description,
                        frozen_date = item.frozen_date,
                        frozen_flag = item.frozen_flag,
                        Memo = "",
                        password_lifespan_access = item.password_lifespan_access,
                        password_lifespan_days = item.password_lifespan_days,
                        USER_NAME = item.employee_name,
                        ISDISABLED = 0
                    };
                    if (entity.ROLE_CODE != "RPL_PRESENTER")
                    {
                        listuser.Add(UserEntity);
                    }

                    listrole.Add(entity);

                }
                
                    _hecIAMUserRoles.SyncHECUserInfo(listrole);//同步用户角色
                    OKcount++;
                    HECSyncUserInfo(listuser);                               //同步账号信息
            }
            catch(Exception ex)
            {
                new LogDAL().AddsysErrorLog("hec同步接口-" + ex.ToString());
                int countall, ok;
                HECSyncUserRole(out countall, out ok);
                allcount = countall;
                OKcount = ok;
            }

        }


        /// <summary>
        /// hec 部门信息同步 by xhb 2015-3-9
        /// </summary>
        /// <param name="allcount"></param>
        /// <param name="Okcount"></param>
        public void HECSyncDepartMent(out int allcount, out int Okcount)
        {


            IAM2.WebReference_HEC_bumen.auto_service _services = new IAM2.WebReference_HEC_bumen.auto_service();
            var list = _services.execute(new IAM2.WebReference_HEC_bumen.parameter() { ws_user_name = hecName, ws_password = hecPassword }).unit_records.ToList();
            allcount = list.Count;
            Okcount = 0;
            List<HEC_DepartMent_Info> ListIAM_DepartMent = new List<HEC_DepartMent_Info>();
            foreach (var x in list)
            {
                HEC_DepartMent_Info tmp = new HEC_DepartMent_Info()
                {
                    ID = Guid.NewGuid(),
                    UNIT_CODE = x.unit_code,
                    UNIT_NAME = x.unit_name,
                    PARENT_UNIT_CODE = x.parent_unit_code,
                    PARENT_UNIT_NAME = x.parent_unit_name,
                    COMPANY_CODE = x.company_code,
                    COMPANY_NAME = x.company_name,
                    ENABLED_FLAG = x.enabled_flag
                };
                Okcount++;
                ListIAM_DepartMent.Add(tmp);
            }

            try
            {
                new HECDepartMentDAL().SyncDepartMent(ListIAM_DepartMent);
            }
            catch (Exception ex)
            {
                allcount = Okcount = 0;
                new LogDAL().AddsysErrorLog(ex.ToString());
            }
        }


        /// <summary>
        /// hec 岗位信息同步 by xhb 2015-3-9
        /// </summary>
        /// <param name="allcount"></param>
        /// <param name="Okcount"></param>
        public void HECSyncGangwei(out int allcount, out int Okcount)
        {
            IAM2.WebReference_HEC_Gangwei.auto_service GangWeiServices = new IAM2.WebReference_HEC_Gangwei.auto_service();
            var list = GangWeiServices.execute(new IAM2.WebReference_HEC_Gangwei.parameter() { ws_password = hecPassword, ws_user_name = hecName }).position_records.ToList();
            allcount = list.Count;
            Okcount = 0;
            List<HEC_Gangwei_Info> ListGangwei = new List<HEC_Gangwei_Info>();
            foreach (var x in list)
            {
                HEC_Gangwei_Info tmp = new HEC_Gangwei_Info()
                {
                    ID = Guid.NewGuid(),
                    POSITION_NAME = x.position_name,
                    POSTITION_CODE = x.position_code,
                    UNIT_CODE = x.unit_code,
                    UNIT_NAME = x.unit_name,
                    ENABLED_FLAG = x.enabled_flag,
                    Company_Code=x.company_code
                };
                Okcount++;
                ListGangwei.Add(tmp);
            }
            new IAMEntityDAL.HECGangweiDAL().SyncGangwei(ListGangwei);
        }

        public void HECSyncUserGangwei(out int allcount, out int OKcount)
        {
            try
            {
                IAM2.WebReference_HEC_UserGangwei.auto_service UserGangweiServices = new IAM2.WebReference_HEC_UserGangwei.auto_service();
                var list = UserGangweiServices.execute(new IAM2.WebReference_HEC_UserGangwei.parameter() { ws_password = hecPassword, ws_user_name = hecName }).employee_position_records.ToList();
                allcount = list.Count;
                OKcount = 0;
                List<HEC_User_Gangwei> ListUserGangwei = new List<HEC_User_Gangwei>();
                //list = list.Where(x => x.position_code != "HLHJY99").ToList();
                foreach (var x in list)
                {

                    HEC_User_Gangwei tmp = new HEC_User_Gangwei()
                    {
                        ID = Guid.NewGuid(),
                        EMPLOYEE_CODE = x.employee_code,
                        EMPLOYEE_NAME = x.employee_name,
                        COMPANY_CODE = x.company_code,
                        COMPANY_NAME = x.company_name,
                        UNIT_CODE = x.unit_code,
                        UNIT_NAME = x.unit_name,
                        POSITION_CODE = x.position_code,
                        POSITION_NAME = x.position_name,
                        PRIMARY_POSITION_FLAG = x.primary_position_flag,
                        ENABLED_FLAG = x.enabled_flag,
                        isdelete = 0
                    };
                    OKcount++;
                    ListUserGangwei.Add(tmp);
                }

                new HECUserGangweiDAL().SyncUserGangwei(ListUserGangwei);
                //发送账号冲突邮件
                //BLL.Sys_UserName_ConflictResolutionMail.SendMail(IAMEntityDAL.Unitity.SystemType.HEC);
            }
            catch (Exception ex)
            {
                OKcount = allcount = 0;
                BLL.Sys_UserName_ConflictResolutionMail.SendMail(IAMEntityDAL.Unitity.SystemType.HEC);
                new LogDAL().AddsysErrorLog("HEC岗位冲突接口报错："+ex.ToString());
            }
        }

    }
}