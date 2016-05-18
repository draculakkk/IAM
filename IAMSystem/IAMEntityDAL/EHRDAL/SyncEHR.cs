using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL.EHRDAL
{
    public class SyncEHR
    {


        static view_bd_psndocDAL _EHRServices = new view_bd_psndocDAL();

        public static void SyncHRCompanyInfo(out int Allcount, out int Okcount)
        {
            try
            {
                System.Data.DataTable dt = new SQLHelper().ExcutDataSet(new SQLHelper().ConnectionString, System.Data.CommandType.Text, "select * from shacehr.view_bd_corp").Tables[0];
                Allcount = dt.Rows.Count;
                Okcount = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    BaseDataAccess.HRCompany entity = new BaseDataAccess.HRCompany();
                    entity.Pk_CORP = dr["PK_CORP"].ToString();
                    entity.UNTTCODE = dr["UNITCODE"].ToString();
                    entity.UNTTNAME = dr["UNITNAME"].ToString();
                    entity.FATHERCORP = dr["FATHERCORP"].ToString();
                    try
                    {
                        new HRCompanyDAL().AddHRCompany(entity);
                        Okcount++;
                    }
                    catch (Exception ex)
                    {
                        new LogDAL().AddsysErrorLog(ex.ToString());
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                new LogDAL().AddsysErrorLog(ex.ToString());
                Allcount = Okcount = 0;
            }
        }

        /// <summary>
        /// HR员工基本信息同步任务
        /// </summary>
        /// <param name="Allcount">总行数</param>
        /// <param name="Okcount">成功行数</param>
        public static void SyncHREmployee(out int Allcount, out int Okcount)
        {
            LogDAL _logServices = new LogDAL();
            HREmployeeDAL _employee = new HREmployeeDAL();

            _employee.UpdateHREployee();
            var list = _EHRServices.ReturnView_bdpsndocList();
            list = list.OrderByDescending(item => item.Outdutydate).ToList();

            

            Allcount = list.Count;
            Okcount = 0;
            foreach (var item in list)
            {
                try
                {

                    BaseDataAccess.HREmployee Entity = new BaseDataAccess.HREmployee();
                    Entity.Pk_psndoc = item.Pk_psndoc;
                    Entity.isSync = true;
                    Entity.moblePhone = item.Mobile;
                    Entity.posts = item.Jobname;
                    Entity.name = item.Psnname;
                    Entity.userScope = item.psnlscope.ToString();
                    Entity.syncDate = DateTime.Now;
                    Entity.p1 = item.Psnclasscode;
                    if (item.Outdutydate!=null)
                    {
                        Entity.leavePostsDate = item.Outdutydate;

                    }
                    if (!string.IsNullOrEmpty(item.Indutydate))
                    {
                        Entity.toPostsDate = Convert.ToDateTime(item.Indutydate);
                    }
                    Entity.code = item.Psncode;
                    Entity.dept = item.Pk_deptdoc;
                    _employee.SyncHrEmployee(Entity);
                    Okcount++;
                }
                catch (Exception ex)
                {
                    (new LogDAL()).AddsysErrorLog(string.Format("HR员工同步错误:{0}:{1}", ex.Message, ex.ToString()));
                    continue;
                }

            }
        }


        /// <summary>
        /// HR操作员信息同步任务
        /// 2014-11-12 haiboax 同步完检查iam系统的账号是否都存在接口中
        /// </summary>
        /// <param name="Allcount">总行数</param>
        /// <param name="Okcount">成功行数</param>
        public static void SyncSmUser(out int Allcount, out int Okcount)
        {
            IAMEntityDAL.HRSm_userDAL _hrsmuserservices = new IAMEntityDAL.HRSm_userDAL();
            List<BaseDataAccess.EHREntities.view_sm_user> listsmuser = _EHRServices.Returnview_sm_userList();
            _hrsmuserservices.UpdateHRsmUser();
            Allcount = listsmuser.Count;
            Okcount = 0;
            
            foreach (var item in listsmuser)
            {
                BaseDataAccess.HRSm_user entity = new BaseDataAccess.HRSm_user()
                {
                    Able_time = item.Able_time,
                    Authen_type = item.Authen_type,
                    isSync = true,
                    Cuserid = item.Cuserid,
                    Disable_time = item.Disable_time,
                    Dr = short.Parse(item.Dr.ToString()),
                    Isca = item.Isca,
                    keyuser = item.KeyUser,
                    Langcode = item.Langcode,
                    Locked_tag = item.Locked_tag,
                    Pk_corp = item.Pk_corp,
                    Pwdlevelcode = item.PwdLevelCode,
                    Pwdparam = item.Pwdparam,
                    Pwdtype = short.Parse(item.Pwdtype.ToString()),
                    TS = item.Ts,
                    User_code = item.User_code,
                    USER_name = item.User_name,
                    User_note = item.User_note,
                    user_password = item.User_pagessword,
                    syncDate = DateTime.Now,p1=item.Cuserid

                };
                try
                {
                    _hrsmuserservices.SyncHRsmUser(entity);
                  
                    Okcount++;

                }
                catch (Exception ex)
                {
                    new LogDAL().AddsysErrorLog("hr操作员同步错误："+ex.ToString());
                    continue;
                }
            }

            List<BaseDataAccess.HRSm_user> listold = new List<BaseDataAccess.HRSm_user>();
            listold = new BaseDataAccess.IAMEntities().HRSm_user.ToList();
            foreach (var item in listold)
            {
                var tpm = listsmuser.FirstOrDefault(itm => itm.User_code.Trim() == item.User_code.Trim());
                if (tpm == null)
                    new DeferencesSlution.UserRoleAlignmentValue().IsAddNewNotInIAM<BaseDataAccess.HRSm_user>(item, item.User_code, Unitity.SystemType.HR, "HRSm_User", "", "IAM系统中有该账号", "源系统中无该账号");
            }
        }


        /// <summary>
        /// 导入角色 同步任务
        /// </summary>
        /// <param name="allcount"></param>
        /// <param name="Okcount"></param>
        public static void SyncHzRole(out int allcount, out int Okcount)
        {
            List<BaseDataAccess.EHREntities.hz_Roledel> listHzrole = _EHRServices.ReturnHzRoleDel();
            HRsm_roleDAL hrsmrole = new HRsm_roleDAL();
            hrsmrole.UpdateHRsm_Role();
            allcount = listHzrole.Count;
            Okcount = 0;
            foreach (var item in listHzrole)
            {
                BaseDataAccess.HRsm_role module = new BaseDataAccess.HRsm_role()
                {
                    Pk_role = item.Pk_role,
                    Role_code = item.Role_code,
                    role_name = item.Role_name,
                    Pk_corp = item.Pk_corp,
                    isSync = true,
                    Dr = item.Dr.ToString(),
                    Resource_type = item.Resource_type,
                    syncDate = DateTime.Now,
                    TS = item.Ts
                };
                try { hrsmrole.SyncHRsmRole(module); Okcount++; }
                catch
                {
                    continue;
                }

            }
        }


        /// <summary>
        /// 导入公司角色关系 同步任务
        /// </summary>
        /// <param name="allcount"></param>
        /// <param name="Okcount"></param>
        public static void SyncHrsmRoleCorpAlloc(out int allcount, out int Okcount)
        {
            List<BaseDataAccess.EHREntities.hz_r_c_allocdel> listalloc = _EHRServices.ReturnHzRoleCorpAlloc();
            allcount = listalloc.Count;
            Okcount = 0;
            foreach (var item in listalloc)
            {
                BaseDataAccess.HRSm_role_corp_alloc entity = new BaseDataAccess.HRSm_role_corp_alloc();
                entity.Pk_role_corp_alloc = item.Pk_role_corp_alloc;
                entity.Pk_corp = item.Pk_corp;
                entity.Pk_role = item.Pk_role;
                entity.TS = item.Ts;
                entity.Dr = short.Parse(item.Dr.ToString());
                entity.isSync = true;
                entity.syncDate = DateTime.Now;
                try
                {
                    new HRSm_role_corp_allocDAL().SyncHRsmRoleCorpAlloc(entity);
                    Okcount++;
                }
                catch
                {

                    continue;
                }
            }
        }

        /// <summary>
        /// 导入用户角色关系 同步任务
        /// </summary>
        /// <param name="allcount"></param>
        /// <param name="Okcount"></param>
        public static void SyncHrsmUserRole(out int allcount, out int Okcount)
        {
            var listUserrole = _EHRServices.ReturnHrsmUserRole();
            //System.Data.DataTable dt = listUserrole.ToDataTable();
            //using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection("data Source=10.124.87.172;Initial Catalog=IAM;User ID=sa;Password=Iam12345"))
            //{
            //    conn.Open();
            //    System.Data.SqlClient.SqlBulkCopy sqlb = new System.Data.SqlClient.SqlBulkCopy(conn);
            //    sqlb.DestinationTableName = "hrsm_userRoleYuan";
            //    sqlb.WriteToServer(dt);
            //}
            //Okcount = 0; allcount = 0;
            //return;
            List<BaseDataAccess.EHREntities.view_sm_user> listsmuser = _EHRServices.Returnview_sm_userList();
            allcount = listUserrole.Count;
            Okcount = 0;
            foreach (var tmp in listUserrole.Where(item=>item.Dr==0))
            {
                BaseDataAccess.HRsm_user_role UserRoleModel = new BaseDataAccess.HRsm_user_role();
                UserRoleModel.Pk_user_role = tmp.Pk_user_role;
                UserRoleModel.Cuserid = tmp.CuserId;
                UserRoleModel.Pk_corp = tmp.Pk_corp;
                UserRoleModel.Pk_role = tmp.Pk_role;
                UserRoleModel.Dr = short.Parse(tmp.Dr.ToString());
                UserRoleModel.TS = tmp.Ts;
                UserRoleModel.isSync = true;
                UserRoleModel.syncDate = DateTime.Now;
                try
                {
                    new HRsm_user_roleDAL().SyncHRsmUserRole(UserRoleModel,listsmuser);
                    Okcount++;
                }
                catch(Exception ex)
                {
                    new LogDAL().AddsysErrorLog("hr角色同步"+ex.ToString());
                    continue;
                }
            }
            int count;
            var listiam = new HRsm_user_roleDAL().HrsmUserRole();
            var listiamuser = new HRSm_userDAL().HrSmUserList();
            var listrole = new HRsm_roleDAL().HRsmRoleList(out count);
            foreach (var x in listiam)
            {
                var tmp = listUserrole.FirstOrDefault(item=>item.Pk_corp==x.Pk_corp&&item.Pk_role==x.Pk_role&&item.CuserId==x.Cuserid);
                var tmpuser = listiamuser.FirstOrDefault(item=>item.Cuserid==x.Cuserid);
                var tmprole = listrole.FirstOrDefault(item=>item.Pk_role==x.Pk_role);
                if (tmp == null&&tmpuser!=null)
                {
                    new DeferencesSlution.UserRoleAlignmentValue().IsAddNewNotInIAM<HRsm_user_role>(x, tmpuser.User_code, Unitity.SystemType.HR, "HRsm_user_role", tmprole.role_name+"/"+x.Pk_corp, "IAM系统有该组权限", "源系统中无该组权限"); 
                }
            }

            foreach (var x in listiam.Where(item => item.Dr == 0))
            {
                var tmpisdr = listUserrole.FirstOrDefault(item=>item.CuserId==x.Cuserid&&item.Pk_role==x.Pk_role&&item.Pk_corp==x.Pk_corp&&item.Dr==1);//查找已删除

                var tmpisnotdr = listUserrole.FirstOrDefault(item => item.CuserId == x.Cuserid && item.Pk_role == x.Pk_role && item.Pk_corp == x.Pk_corp && item.Dr == 0);//查找未删除

                var tmpuser = listiamuser.FirstOrDefault(item=>item.Cuserid==x.Cuserid);

                var tmprole = listrole.FirstOrDefault(item=>item.Pk_role==x.Pk_role);
                if (tmpisdr != null && tmpuser != null&&tmpisnotdr==null)
                {
                    new DeferencesSlution.UserRoleAlignmentValue().IsAddNewNotInIAM<HRsm_user_role>(x, tmpuser.User_code, Unitity.SystemType.HR, "HRsm_user_role", tmprole.role_name + "/" + x.Pk_corp, "IAM系统该组权限不为删除", "源系统中该组权限为删除");
                }

            }


        }


        /// <summary>
        /// 部门信息  同步任务
        /// </summary>
        /// <param name="countall"></param>
        /// <param name="Okcount"></param>
        public static void SyncHrDepartMent(out int countall, out int Okcount)
        {
            var listdepartment = _EHRServices.ReturnViewbdDeptdoc();
            countall = listdepartment.Count;
            Okcount = 0;
            foreach (var tmp in listdepartment)
            {
                BaseDataAccess.HRDepartment tmpdepart = new BaseDataAccess.HRDepartment();
                tmpdepart.dept = tmp.Pk_deptdoc;
                tmpdepart.name = tmp.Deptname;
                tmpdepart.parentDept = tmp.pk_fathedept;
                if (!string.IsNullOrEmpty(tmp.Canceldate))
                {
                    tmpdepart.revokeDate = Convert.ToDateTime(tmp.Canceldate);
                }
                tmpdepart.isSealed = tmp.canceled == "Y" ? true : tmp.canceled == "N" ? false : false;
                tmpdepart.isRevoke = tmp.hrcanceled == "Y" ? true : tmp.canceled == "N" ? false : false;
                tmpdepart.isSync = true;
                tmpdepart.syncDate = DateTime.Now;
                try
                {
                    new HRDepartmentDAL().SyncHrDepartMent(tmpdepart);
                    Okcount++;
                }
                catch
                {
                    continue;
                }
            }
        }

    }
}
