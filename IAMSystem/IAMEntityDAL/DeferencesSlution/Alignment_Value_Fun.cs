using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;
using System.Reflection;

namespace IAMEntityDAL.DeferencesSlution
{
    public class Alignment_Value_Fun
    {
       
        
        //p2 user标记为账号字段冲突即update sql操作 role 标记为账号角色冲突即insert sql操作 
        List<Sys_UserName_ConflictResolution> ReturnDeferenceValue<T>(T classa, T classb, string usercollname, string uservalue, string Username, Unitity.SystemType SystemType)
        {
            List<Sys_UserName_ConflictResolution> list = new List<Sys_UserName_ConflictResolution>();
            System.Reflection.PropertyInfo[] proper = classa.GetType().GetProperties();
            xml.ReadXml readxml = new xml.ReadXml(SystemType);
          
            for (int i = 0; i < proper.Length; i++)
            {
                Sys_UserName_ConflictResolution module = new Sys_UserName_ConflictResolution();
                
                module.TableName = classa.GetType().Name;
                module.ID = Guid.NewGuid();
                module.STATE = 1;
                module.CreateTime = DateTime.Now;
                module.UserName = Username;
                module.SysType = SystemType.ToString();
                object value1 = proper[i].GetValue(classa, null);
                object value2 = proper[i].GetValue(classb, null);
                string val1, val2;
                if (proper[i].Name.ToUpper() == "isSync".ToUpper() || proper[i].Name.ToUpper() == "syncDate".ToUpper() || proper[i].Name.ToUpper() == "EntityKey".ToUpper() || proper[i].Name.ToUpper() == "EntityState".ToUpper() || proper[i].Name.ToUpper() == "LastLoginTime".ToUpper())
                {
                    continue;
                }

                if (classa.GetType().Name == "AD_Computer")
                {
                    if (proper[i].Name.ToUpper() == "ID".ToUpper() || proper[i].Name.ToUpper() == "ExpiryDate".ToUpper() || proper[i].Name.ToUpper() == "ExpiryDate".ToUpper() || proper[i].Name.ToUpper() == "IsSync".ToUpper() || proper[i].Name.ToUpper() == "SyncDate".ToUpper() || proper[i].Name.ToUpper() == "IsDelete".ToUpper() || proper[i].Name.ToUpper() == "DeleteDatetime".ToUpper() || proper[i].Name.ToUpper() == "Memo".ToUpper() || proper[i].Name.ToUpper() == "p1".ToUpper())
                    {
                        continue;
                    }
                }
                if (classa.GetType().Name == "HRSm_user")
                {
                    if (proper[i].Name.Trim().ToUpper() == "Cuserid".Trim().ToUpper())
                    {
                        continue;
                    }
                    if (proper[i].Name.ToUpper() == "TS".ToUpper() || proper[i].Name.ToUpper() == "Pwdparam".ToUpper() || proper[i].Name.ToUpper() == "p1".ToUpper() || proper[i].Name.Trim().ToUpper() == "Cuserid".Trim().ToUpper() || proper[i].Name.ToUpper() == "user_password".ToUpper() || proper[i].Name.Trim().ToUpper() == "START_DATE".Trim().ToUpper())
                    {
                        continue;
                    }
                }

                if (classa.GetType().Name == "AD_UserInfo")
                {
                    if (proper[i].Name.ToUpper() == "Posts".ToUpper() || proper[i].Name.ToUpper() == "dept".ToUpper() || proper[i].Name.ToUpper() == "parentDept".ToUpper() || proper[i].Name.ToUpper() == "NAME".ToUpper() || proper[i].Name.ToUpper() == "HRMoblePhone".ToUpper() || proper[i].Name.ToUpper() == "ToPostsDate".ToUpper() || proper[i].Name.ToUpper() == "LeavePostsDate".ToUpper() || proper[i].Name.ToUpper() == "IsRevoke".ToUpper() || proper[i].Name.ToUpper() == "RevokeDate".ToUpper() || proper[i].Name.ToUpper() == "PASSWORD".ToUpper() || proper[i].Name.ToUpper() == "EnableDrive".ToUpper() || proper[i].Name.ToUpper() == "IsSealed".ToUpper() || proper[i].Name.ToUpper() == "ADMobile".ToUpper() || proper[i].Name.ToUpper() == "ADTel".ToUpper())
                    {
                        continue;
                    }
                    
                }

                if (classa.GetType().Name == "SAP_Parameters")
                {
                    if (proper[i].Name.ToUpper() == "id".ToUpper()||proper[i].Name.ToUpper()=="p1".ToUpper()||proper[i].Name.ToUpper()=="p2".ToUpper())
                    {
                        continue;
                    }
                }

                if (classa.GetType().Name == "TC_UserInfo")
                {
                    if (proper[i].Name.ToUpper() == "LastLoginTime".ToUpper() || proper[i].Name.ToUpper() == "PASSWORD".ToUpper() || proper[i].Name.ToUpper() == "DefaultRoleID".ToUpper() || proper[i].Name.ToUpper() == "DefaultDisk".ToUpper() || proper[i].Name.ToUpper() == "DefaultLocalDisk".ToUpper() || proper[i].Name.ToUpper() == "GroupAdmin".ToUpper() || proper[i].Name.ToUpper() == "GroupStatus".ToUpper() || proper[i].Name.ToUpper() == "GroupOut".ToUpper() || proper[i].Name.ToUpper() == "DefaultGroup".ToUpper() || proper[i].Name.ToUpper() == "GroupDefaultRole".ToUpper())
                    {
                        continue;
                    }
                }

                if (classa.GetType().Name == "HEC_User")
                {
                  
                    if (proper[i].Name.ToUpper() == "password_lifespan_days".ToUpper() || proper[i].Name.ToUpper() == "password_lifespan_access".ToUpper() || proper[i].Name.ToUpper() == "USER_TYPE".ToUpper() || proper[i].Name.ToUpper() == "p6".ToUpper() || proper[i].Name.ToUpper() == "PASSWORD".ToUpper() || proper[i].Name.ToUpper() == "PASSWORD2".ToUpper() || proper[i].Name.ToUpper() == "DISABLED_DATE".ToUpper() || proper[i].Name.ToUpper() == "ISDISABLED".ToUpper() || proper[i].Name.ToUpper() == "createTime".ToUpper() || proper[i].Name.ToUpper() == "frozen_flag".ToUpper() || proper[i].Name.ToUpper() == "frozen_date".ToUpper())
                    {
                        continue;
                    }
                }

                GetSys_userName(value1, value2, out val1, out val2);
                module.CollSysValue = val2;
                module.CollIAMValue = val1;
                module.CollName = readxml.GetChineseStringByKey(proper[i].Name);
                module.UserCollName = usercollname;
                module.UserValue = uservalue;
                module.P1 = proper[i].Name;
                module.P2 = "user";
                
                if (!val1.Trim().Equals(val2.Trim()))
                {
                    if (classa.GetType().Name == "AD_UserInfo")
                    {
                        if (proper[i].Name.ToUpper() == "expiryDate".ToUpper())
                        {
                            DateTime dateiam=new DateTime();                            
                            DateTime dateyuan=new DateTime();
                            DateTime.TryParse(val1,out dateiam);
                            DateTime.TryParse(val2,out dateyuan);
                            DateTime datenorma = new DateTime(1900,1,1);
                            if ((dateiam - datenorma).Days == 0)
                            {
                                dateiam = DateTime.MinValue;
                            }
                            if ((dateyuan - datenorma).Days == 0)
                            {
                                dateyuan = DateTime.MinValue;
                            }
                            if (dateiam==dateyuan)
                            { continue; }
                            else
                            {
                                list.Add(module);
                                continue;
                            }
                        }
                    }
                    if (classa.GetType().Name == "HEC_User")
                    {
                        
                        if (proper[i].Name.ToUpper() == "END_DATE".ToUpper())
                        {
                            if (val1 == "" && (val2 == "1900-1-1" || val2 == "1900-01-01"||val2=="2099-12-31"))
                            {
                                continue;
                            }
                            else
                            {
                                list.Add(module);
                                continue;
                            }
                        }
                    }

                    if (classa.GetType().Name == "HEC_User_Info")
                    {
                        if (proper[i].Name.ToUpper() == "END_DATE".ToUpper() || proper[i].Name.ToUpper() == "ROLE_END_DATE".ToUpper())
                        {
                            if (val1 == "" && (val2 == "1900-1-1" || val2 == "1900-01-01"||val2=="2099-12-31"))
                            {
                                continue;
                            }
                            else
                            {
                                list.Add(module);
                                continue;
                            }
                        }
                    }
                   
                        list.Add(module);
                    

                    
                }
            }

            return list;
        }

        void GetSys_userName(object value1, object value2, out string val1, out string val2)
        {

            val1 = val2 = string.Empty;
            if (value1 == null && value2 == null)
            {
                return;
            }
            if (value1 != null && value2 != null)
            {
                if (!value2.ToString().Trim().Equals(value1.ToString().Trim()))
                {
                    val1 = value1.ToString();
                    val2 = value2.ToString();
                }
            }

            if (value1 != null && value2 == null)
            {
                val1 = value1.ToString();
                val2 = "";
            }

            if (value2 != null && value1 == null)
            {
                val1 = "";
                val2 = value2.ToString();
            }
        }


        public void AddConflic<T>(T classa, T classb, Unitity.SystemType systemname, string userCollname, string uservalue, string UserName)
        {
            List<Sys_UserName_ConflictResolution> list = ReturnDeferenceValue<T>(classa, classb, userCollname, uservalue, UserName, systemname);
            Sys_UserName_ConflictResolutionDAL db = new Sys_UserName_ConflictResolutionDAL();
            foreach (var item in list)
            {
                db.AddSysUserNameConflicResolution(item);
            }
        }

    }


    //p2 user标记为账号字段冲突 role 标记为账号角色冲突
    public class UserRoleAlignmentValue
    {
        /// <summary>
        /// 账号角色信息，冲突解决记录方案
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="TableName"></param>
        /// <param name="rolename"></param>
        /// <param name="isiam"></param>
        /// <param name="isappliction"></param>
        public void IsAddNewNotInIAM<T>(T entity,string username, Unitity.SystemType systype, string TableName, string rolename, string isiam, string isappliction)
        {
            Sys_UserName_ConflictResolution itme = new Sys_UserName_ConflictResolution()
            {
                P2 = "role",
                TableName = TableName,
                CreateTime = DateTime.Now,
                SysType = systype.ToString(),
                CollIAMValue = isiam,
                CollSysValue = isappliction,
                CollName = rolename ,
                UserName=                     username,
                Remark = sqlString<T>(entity, TableName), ID=Guid.NewGuid(),STATE=1
            };
            Sys_UserName_ConflictResolutionDAL db = new Sys_UserName_ConflictResolutionDAL();
            db.AddSysUserNameConflicResolution(itme);
        }

        public void AddRoleChayi(string username, Unitity.SystemType systype, string TableName, string rolename, string isiam, string isappliction,string sql)
        {
            Sys_UserName_ConflictResolution itme = new Sys_UserName_ConflictResolution()
            {
                P2 = "role",
                TableName = TableName,
                CreateTime = DateTime.Now,
                SysType = systype.ToString(),
                CollIAMValue = isiam,
                CollSysValue = isappliction,
                CollName = rolename,
                UserName = username,
                Remark = sql,
                ID = Guid.NewGuid(),
                STATE = 1
            };
            Sys_UserName_ConflictResolutionDAL db = new Sys_UserName_ConflictResolutionDAL();
            db.AddSysUserNameConflicResolution(itme);
        }

        string sqlString<T>(T entity, string tablename)
        {
            PropertyInfo[] propers = entity.GetType().GetProperties();
            StringBuilder stb1 = new StringBuilder();
            StringBuilder stb2 = new StringBuilder();
            stb1.Append("INSERT INTO " + tablename + "(");
            stb2.Append(" VALUES(");
            for (int i = 0; i < propers.Length; i++)
            {
                string name = propers[i].Name;
                object value = propers[i].GetValue(entity, null);
                if (name == "EntityState" || name == "EntityKey")
                {
                    continue;
                }
                if (tablename.ToUpper().Trim().Equals("AD_UserInfo".ToUpper().Trim()) && name.ToUpper().Equals("expiryDate".ToUpper()))
                {
                    if (value!=null&&!value.ToString().Equals("1900-01-01 00:00:00"))
                    {
                        stb1.Append("[" + name + "] ,");
                        stb2.Append("'" + value + "',");
                    }
                    
                }
                else
                {
                    stb1.Append("[" + name + "] ,");
                    stb2.Append("'" + value + "',");
                }
            }           
            return stb1.ToString().TrimEnd(',') +") " +stb2.ToString().TrimEnd(',')+")";
        }
    }



}
