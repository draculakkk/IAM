using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class HECUserGangweiDAL : BaseFind<BaseDataAccess.HEC_User_Gangwei>
    {
        IAMEntities db = null;
        public HECUserGangweiDAL()
        {
            db = new IAMEntities();
        }
        public void NewAdd(BaseDataAccess.HEC_User_Gangwei entity)
        {
            var tmp = db.HEC_User_Gangwei.FirstOrDefault(item => item.EMPLOYEE_CODE == entity.EMPLOYEE_CODE && item.COMPANY_CODE == entity.COMPANY_CODE && item.UNIT_CODE == entity.UNIT_CODE && item.POSITION_CODE == entity.POSITION_CODE);
            if (tmp != null)//加入冲突比对           
            { }
            else
                Add(entity);
        }

        public string NewDelete(Guid id)
        {
            try
            {
                var tmp = db.HEC_User_Gangwei.FirstOrDefault(x => x.ID == id);
                db.HEC_User_Gangwei.DeleteObject(tmp);
                db.ObjectStateManager.ChangeObjectState(tmp, System.Data.EntityState.Deleted);
                db.SaveChanges();
                return string.Format("删除了岗位信息{0}", tmp.POSITION_CODE);
            }
            catch (Exception ex)
            {
                new LogDAL().AddsysErrorLog(ex.ToString());
                throw ex;
            }
        }

        public int NewUpdate(HEC_User_Gangwei entity)
        {
            var tmp = NonExecute<HEC_User_Gangwei>(item =>
            {
                return item.HEC_User_Gangwei.FirstOrDefault(x => x.ID == entity.ID);
            });
            if (tmp == null)
                return 0;
            db.HEC_User_Gangwei.Attach(entity);
            db.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
            return db.SaveChanges();
        }

        /// <summary>
        /// 页面中操作账号岗位信息
        /// </summary>
        /// <param name="ListUserGangwei"></param>
        /// <returns></returns>
        public string PageAddUserGangwei(List<HEC_User_Gangwei> ListUserGangwei)
        {
            var listnew = ListUserGangwei.Where(x => x.isdelete == 2);
            var listdel = ListUserGangwei.Where(x => x.isdelete == 1);
            var listdef = ListUserGangwei.Where(x => x.isdelete == 0);
            StringBuilder stbadd = new StringBuilder();
            StringBuilder stbdel = new StringBuilder();
            stbadd.Append("添加了");
            stbdel.Append("删除了");
            foreach (var x in listnew)
            {
                NewAdd(x);
                stbadd.Append(x.POSITION_CODE + "、");
            }

            foreach (var x in listdef)
            {
                NewUpdate(x);
            }

            foreach (var x in listdel)
            {
                NewUpdate(x);
                stbdel.Append(x.POSITION_CODE + "、");
            }
            if (listnew.Count() > 0 && listdel.Count() > 0)
                return stbadd.ToString() + "<br/>" + stbdel.ToString();
            else if (listnew.Count() > 0 && listdel.Count() <= 0)
                return stbadd.ToString();
            else if (listnew.Count() <= 0 && listdel.Count() > 0)
                return stbdel.ToString();
            else
                return string.Empty;
        }

        /// <summary>
        /// 岗位信息 同步
        /// </summary> 
        /// <param name="SyncList"></param>
        public void SyncUserGangwei(List<HEC_User_Gangwei> SyncList)
        {
            List<HEC_User> listUser = db.HEC_User.ToList();
            List<HEC_User_Gangwei> listg = db.HEC_User_Gangwei.ToList();
            foreach (var x in SyncList)
            {
                var tmp = listg.FirstOrDefault(item => item.EMPLOYEE_CODE == x.EMPLOYEE_CODE && item.COMPANY_CODE == x.COMPANY_CODE && item.UNIT_CODE == x.UNIT_CODE && item.POSITION_CODE == x.POSITION_CODE);
                var tmpuser = listUser.SingleOrDefault(item => item.User_CD.Trim() == x.EMPLOYEE_CODE);
                if (tmp == null)
                {
                    if (base.IsFirstTime)
                        NewAdd(x);
                    else
                    {
                            DeferencesSlution.UserRoleAlignmentValue userroles = new DeferencesSlution.UserRoleAlignmentValue();
                            try
                            {
                                if (x.POSITION_CODE == "HLHJY99"||tmpuser==null)
                                {
                                    NewAdd(x);
                                }
                                else
                                {
                                    userroles.IsAddNewNotInIAM<HEC_User_Gangwei>(x, x.EMPLOYEE_CODE, Unitity.SystemType.HEC, "HEC_User_Gangwei", x.POSITION_NAME, "IAM中无该岗位", "源系统中有该岗位");
                                }
                            }
                            catch (Exception ex)
                            { }
                        
                    }
                }
                else
                {
                    if (tmpuser != null)
                    {                        
                        
                        if (x.ENABLED_FLAG != tmp.ENABLED_FLAG)
                        {
                            Sys_UserName_ConflictResolution module = new Sys_UserName_ConflictResolution();
                            module.TableName = "HEC_User_Gangwei";
                            module.ID = Guid.NewGuid();
                            module.STATE = 1;
                            module.CreateTime = DateTime.Now;
                            module.UserName = x.EMPLOYEE_CODE;
                            module.SysType = Unitity.SystemType.HEC.ToString();
                            module.UserCollName = "ID";
                            module.UserValue = tmp.ID.ToString();
                            module.P1 = "ENABLED_FLAG";
                            module.P2 = "user";
                            module.CollSysValue = x.ENABLED_FLAG;
                            module.CollIAMValue = tmp.ENABLED_FLAG;
                            module.CollName = x.POSITION_NAME + "是否启用";
                            db.Sys_UserName_ConflictResolution.AddObject(module);
                            db.SaveChanges();
                        }
                        if (x.PRIMARY_POSITION_FLAG != tmp.PRIMARY_POSITION_FLAG)
                        {
                            Sys_UserName_ConflictResolution module = new Sys_UserName_ConflictResolution();
                            module.TableName = "HEC_User_Gangwei";
                            module.ID = Guid.NewGuid();
                            module.STATE = 1;
                            module.CreateTime = DateTime.Now;
                            module.UserName = x.EMPLOYEE_CODE;
                            module.SysType = Unitity.SystemType.HEC.ToString();
                            module.UserCollName = "ID";
                            module.UserValue = tmp.ID.ToString();
                            module.P1 = "PRIMARY_POSITION_FLAG";
                            module.P2 = "user";
                            module.CollSysValue = x.PRIMARY_POSITION_FLAG;
                            module.CollIAMValue = tmp.PRIMARY_POSITION_FLAG;
                            module.CollName = x.POSITION_NAME+"是否主岗位";
                            db.Sys_UserName_ConflictResolution.AddObject(module);
                            db.SaveChanges();
                        }
                        
                    }
                    else
                    {
                        tmp.COMPANY_CODE = x.COMPANY_CODE;
                        tmp.COMPANY_NAME = x.COMPANY_NAME;
                        tmp.EMPLOYEE_CODE = x.EMPLOYEE_CODE;
                        tmp.EMPLOYEE_NAME = x.EMPLOYEE_NAME;
                        tmp.ENABLED_FLAG = x.ENABLED_FLAG;
                        tmp.PRIMARY_POSITION_FLAG = x.PRIMARY_POSITION_FLAG;
                        tmp.UNIT_CODE = x.UNIT_CODE;
                        tmp.UNIT_NAME = x.UNIT_NAME;
                        tmp.POSITION_CODE = x.POSITION_CODE;
                        tmp.POSITION_NAME = x.POSITION_NAME;
                        db.ObjectStateManager.ChangeObjectState(tmp,System.Data.EntityState.Modified);
                        db.SaveChanges();
                    }
                }
            }
            SyncListIam(SyncList);
        }
        /// <summary>
        /// 查询iam系统中数据，那些在接口中没有
        /// </summary>
        /// <param name="SyncList"></param>
        void SyncListIam(List<HEC_User_Gangwei> SyncList)
        {
            var listiam = db.HEC_User_Gangwei.ToList();
            foreach (var x in listiam)
            {
                var tmp = SyncList.FirstOrDefault(item => item.EMPLOYEE_CODE == x.EMPLOYEE_CODE && item.COMPANY_CODE == x.COMPANY_CODE && item.UNIT_CODE == x.UNIT_CODE && item.POSITION_CODE == x.POSITION_CODE);
                if (tmp == null)
                {
                    var usercd = db.HEC_User.FirstOrDefault(xx=>xx.User_CD.ToUpper().Trim()==x.EMPLOYEE_CODE.ToUpper().Trim());
                    if (usercd == null)
                        continue;
                    else
                    {
                        DeferencesSlution.UserRoleAlignmentValue userroles = new DeferencesSlution.UserRoleAlignmentValue();
                        userroles.IsAddNewNotInIAM<HEC_User_Gangwei>(x, x.EMPLOYEE_CODE, Unitity.SystemType.HEC, "HEC_User_Gangwei", string.Format("{0}/{1}/{2}", x.COMPANY_NAME, x.UNIT_NAME, x.POSITION_NAME), "IAM中有该岗位", "源系统中无该岗位");
                    }
                }
            }
        }

    }
}
