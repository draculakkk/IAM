using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAMEntityDAL;
using BaseDataAccess;

namespace IAM.BLL
{
    public class SystemModule
    {
        public bool AD { set; get; }

        public bool HEC { get; set; }

        public bool EHR { get; set; }

        public bool SAP { set; get; }

        public bool TC { get; set; }

        public bool Admin { get; set; }

        public bool EndUser { get; set; }

        public bool Leader { get; set; }
    }

    public class UserRoleManager
    {

        public static SystemModule Query(UserRole userentity)
        {
            SystemModule UserRoleModule = new SystemModule();
            if (userentity != null)
            
            {
                string[] roles = userentity.roles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string it in roles)
                {
                    if (it.Equals("AD"))
                        UserRoleModule.AD = true;
                    if (it.Equals("HEC"))
                        UserRoleModule.HEC = true;
                    if (it.Equals("SAP"))
                        UserRoleModule.SAP = true;
                    if (it.Equals("EHR"))
                        UserRoleModule.EHR = true;
                    if (it.Equals("TC"))
                        UserRoleModule.TC = true;
                    if (it.Equals("Admin"))
                    {
                        UserRoleModule.Admin = true;
                        UserRoleModule.AD = true;
                        UserRoleModule.HEC = true;
                        UserRoleModule.SAP = true;
                        UserRoleModule.EHR = true;
                        UserRoleModule.TC = true;
                    }
                    if (it.Equals("Admin") || it.Equals("Leader"))
                    {
                        UserRoleModule.Leader = true;
                        UserRoleModule.AD = true;
                        UserRoleModule.HEC = true;
                        UserRoleModule.SAP = true;
                        UserRoleModule.EHR = true;
                        UserRoleModule.TC = true;
                    }
                    if (it.Equals("EndUser"))
                    {
                        UserRoleModule.EndUser = true;
                        UserRoleModule.AD = false;
                        UserRoleModule.HEC = false;
                        UserRoleModule.SAP = false;
                        UserRoleModule.EHR = false;
                        UserRoleModule.TC = false;
                    }
                }
                
            }
            return UserRoleModule;
        }
    }
}