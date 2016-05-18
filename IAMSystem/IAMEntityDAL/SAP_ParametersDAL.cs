using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class SAP_ParametersDAL : BaseFind<SAP_Parameters>
    {
        public int NewUpdate(SAP_Parameters entity)
        {
            try
            {
                var e = NonExecute<SAP_Parameters>(db =>
                {
                    return db.SAP_Parameters.FirstOrDefault(item => item.PARAMENTERID == entity.PARAMENTERID && item.PARAMENTERVALUE == entity.PARAMENTERVALUE && item.PARAMETERTEXT == item.PARAMETERTEXT && item.BAPIBNAME == entity.BAPIBNAME);
                });
                if (e == null)
                {
                    return 0;
                }
                entity.id = e.id;
                using (IAMEntities db = new IAMEntities())
                {
                    db.SAP_Parameters.Attach(entity);
                    db.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
                    return db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Guid id = new LogDAL().AddsysErrorLog(ex.ToString());
                throw new Exception("错误id:" + id.ToString());
            }
        }

        public int NewUpdate1(SAP_Parameters entity)
        {
            try
            {
                using (IAMEntities db = new IAMEntities())
                {
                    db.SAP_Parameters.Attach(entity);
                    db.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
                    return db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Guid id = new LogDAL().AddsysErrorLog(ex.ToString());
                throw new Exception("错误id:" + id.ToString());
            }
        }
        public void NewUpdate(List<SAP_Parameters> entity)
        {
            foreach (var ti in entity)
            {
                NewUpdate(ti);
            }
        }

        public int NewAdd(SAP_Parameters entity)
        {
            if (entity != null)
            {
                var e = NonExecute<SAP_Parameters>(db =>
                {
                    return db.SAP_Parameters.FirstOrDefault(item => item.PARAMENTERID == entity.PARAMENTERID && item.PARAMENTERVALUE == entity.PARAMENTERVALUE && item.PARAMETERTEXT == item.PARAMETERTEXT && item.BAPIBNAME == entity.BAPIBNAME);
                });
                if (e == null)
                {
                    if (base.IsFirstTime)
                        Add(entity);
                    else
                        new DeferencesSlution.UserRoleAlignmentValue().IsAddNewNotInIAM<SAP_Parameters>(entity, entity.BAPIBNAME, Unitity.SystemType.SAP, "SAP_Parameters", "", "IAM系统无该参数", "源系统新增参数");
                }
                else
                {
                    new DeferencesSlution.Alignment_Value_Fun().AddConflic<SAP_Parameters>(entity, e, Unitity.SystemType.ADComputer, "id", e.id.ToString(), e.BAPIBNAME);
                }

            }
            return 1;
        }

        public void NewAdd(List<SAP_Parameters> entity)
        {
            foreach (var ti in entity)
            {
                NewAdd(ti);
            }
        }

        public void IamNewAdd(List<SAP_Parameters> entity)
        {
            foreach (var ti in entity)
            {
                Guid id = ti.id;
                string user = ti.BAPIBNAME;
                var mm = NonExecute<SAP_Parameters>(db =>
                {
                    return db.SAP_Parameters.FirstOrDefault(item => item.id == id);
                });
                if (mm == null)
                    Add(ti);
            }
        }

        public void IamNewAdd(List<SAP_Parameters> entity, string bapname)
        {
            List<SAP_Parameters> listnew = entity.Where(item => item.isdr == 2).ToList();
            List<SAP_Parameters> listdel = entity.Where(item => item.isdr == 1).ToList();
            
            foreach (var ti in listnew)
            {
                System.Text.StringBuilder stb = new StringBuilder();
                
                var mm = NonExecute<SAP_Parameters>(db =>
                {
                    return db.SAP_Parameters.FirstOrDefault(item => item.BAPIBNAME == bapname && item.PARAMENTERID.Trim() == ti.PARAMENTERID.Trim());
                });

                if (mm == null)
                {
                    stb.Append(@"INSERT INTO dbo.SAP_Parameters
        ( id ,BAPIBNAME ,PARAMENTERID ,PARAMENTERVALUE ,PARAMETERTEXT ,isdr)VALUES  ( NEWID() ,@u,@paid ,@pavalue ,@patext ,0)");
                    List<System.Data.SqlClient.SqlParameter> parms = new List<System.Data.SqlClient.SqlParameter>();
                    parms.Add(new System.Data.SqlClient.SqlParameter("@u",bapname));
                    parms.Add(new System.Data.SqlClient.SqlParameter("@paid", ti.PARAMENTERID));
                    parms.Add(new System.Data.SqlClient.SqlParameter("@pavalue", ti.PARAMENTERVALUE));
                    parms.Add(new System.Data.SqlClient.SqlParameter("@patext", ti.PARAMETERTEXT));
                    using (IAMEntities db = new IAMEntities())
                    {
                        db.ExecuteStoreCommand(stb.ToString(),parms.ToArray());
                        db.SaveChanges();
                    }
                }
                else
                {
                    stb.Append(@"update dbo.SAP_Parameters set PARAMENTERID=@pid,PARAMENTERVALUE=@pvalue,PARAMETERTEXT=@parex where id=@id ");
                    List<System.Data.SqlClient.SqlParameter> parms = new List<System.Data.SqlClient.SqlParameter>();
                    parms.Add(new System.Data.SqlClient.SqlParameter("@id", mm.id));
                    parms.Add(new System.Data.SqlClient.SqlParameter("@pid", ti.PARAMENTERID));
                    parms.Add(new System.Data.SqlClient.SqlParameter("@pvalue", ti.PARAMENTERVALUE));
                    parms.Add(new System.Data.SqlClient.SqlParameter("@parex", ti.PARAMETERTEXT));
                    using (IAMEntities db = new IAMEntities())
                    {
                        db.ExecuteStoreCommand(stb.ToString(), parms.ToArray());
                        db.SaveChanges();
                    }
                }

            }
            foreach (var x in listdel)
            {
                delete(bapname,x.PARAMENTERID);
            }
        }

        public void delete(string bapname, string parmentid)
        {
            var enti = NonExecute<SAP_Parameters>(db => { return db.SAP_Parameters.FirstOrDefault(item => item.BAPIBNAME == bapname && item.PARAMENTERID == parmentid); });
            if (enti != null)
            {
                using (IAMEntities db = new IAMEntities())
                {
                    db.SAP_Parameters.Attach(enti);
                    db.ObjectStateManager.ChangeObjectState(enti, System.Data.EntityState.Deleted);
                    db.SaveChanges();
                }
            }
        }

        public List<SAP_Parameters> list(string abiname)
        {
            return NonExecute<List<SAP_Parameters>>(db =>
            {
                return db.SAP_Parameters.Where(item => item.BAPIBNAME == abiname).ToList();
            });
        }

        public void SyncUserParameters(List<SAP_Parameters> prams)
        {
            foreach (var item in prams)
            {
                NewAdd(item);
            }
        }

        public List<SAP_Parameters> list()
        {
            return NonExecute<List<SAP_Parameters>>(db =>
            {
                return db.SAP_Parameters.ToList();
            });
        }

    }
}
