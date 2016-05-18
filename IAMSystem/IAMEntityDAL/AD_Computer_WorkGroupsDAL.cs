using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class AD_Computer_WorkGroupsDAL : BaseFind<AD_Computer_WorkGroups>
    {
        public int Add_AdComputerWorkGroup(AD_Computer_WorkGroups entity)
        {
            if (entity == null)
                return 1;

            var mo = NonExecute<AD_Computer_WorkGroups>(db =>
            {
                return db.AD_Computer_WorkGroups.FirstOrDefault(item => item.ComputerName == entity.ComputerName && item.WorkGroup == entity.WorkGroup);
            });
            if (mo == null)
                return Add(entity);
            else
                return 1;
        }

        public List<AD_Computer_WorkGroups> GetList()
        {
            return NonExecute<List<AD_Computer_WorkGroups>>(db =>
            {
                return db.AD_Computer_WorkGroups.ToList();
            });
        }

        public void CreateOrUpdate(List<AD_Computer_WorkGroups> list)
        {
            var oldlist = GetList();
            foreach (var item in list)
            {
                if (oldlist.FirstOrDefault(i => i.Id == item.Id) == null)
                    Add_AdComputerWorkGroup(item);
            }
        }


        public void SyncComputerWorkGroup(List<AD_Computer_WorkGroups> groups)
        {
            if (groups == null || groups.Count <= 0)
            {
                return;
            }
            string computername = groups[0].ComputerName;
            var listold = NonExecute<List<AD_Computer_WorkGroups>>(db =>
            {
                return db.AD_Computer_WorkGroups.Where(item => item.ComputerName == computername).ToList();
            });
            DeferencesSlution.UserRoleAlignmentValue userrrols = new DeferencesSlution.UserRoleAlignmentValue();
            int count;
            var kekoinglist = new ADWorkGroupDAL().GetADWorkGroupList(out count);
            foreach (var item in groups)
            {
                if (base.IsFirstTime)
                    Add_AdComputerWorkGroup(item);
                else
                {
                    var enti = listold.FirstOrDefault(it => it.ComputerName == item.ComputerName && it.WorkGroup == item.WorkGroup);
                    if (enti == null)
                    {
                        var tmpkekong = kekoinglist.FirstOrDefault(x => x.NAME == item.WorkGroup);
                        if (tmpkekong == null)//查询对比冲突的工作组是否在可控组范围内
                        {
                            Add_AdComputerWorkGroup(item);
                        }
                        else
                        {
                            if (new Sys_UserName_ConflictResolutionDAL().GetOne(item.ComputerName, "源系统中存在该组", "iam无该组", "ADComputer", item.WorkGroup) == null)
                            {
                                userrrols.IsAddNewNotInIAM<AD_Computer_WorkGroups>(item, item.ComputerName, Unitity.SystemType.ADComputer, "AD_Computer_WorkGroups", item.WorkGroup, "IAM无该组", "源系统中存在该组");
                            }
                        }
                    }
                }
            }

            foreach (var item in listold)
            {
                var enti = groups.FirstOrDefault(ite => ite.ComputerName == item.ComputerName && ite.WorkGroup == item.WorkGroup);
                if (enti == null)
                    userrrols.IsAddNewNotInIAM<AD_Computer_WorkGroups>(item, item.ComputerName, Unitity.SystemType.ADComputer, "", item.WorkGroup, "IAM有该组", "源系统中无该组");
            }
        }
    }
}
