using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class AddUserMail1
    {
        public string Actioner { get; set; }

        public string time { get { return DateTime.Now.ToString(); } }

        public string SystemName { get; set; }

        public string UserName { get; set; }

        public string SystemType { get; set; }

        public string UserInfo { get; set; }

        public string RoleString { get; set; }

        public string gonghao { get; set; }

        public string biangengneirong { get; set; }
    }

    public class HREmployeeDAL : BaseFind<HREmployee>
    {
        public int AddHREmployee(HREmployee employee)
        {
            return NonExecute(db =>
            {

                db.HREmployee.AddObject(employee);
            });
        }

        public int UpdateHREployee(HREmployee employee)
        {
            using (IAMEntities db = new IAMEntities())
            {
                db.HREmployee.Attach(employee);
                db.ObjectStateManager.ChangeObjectState(employee, System.Data.EntityState.Modified);
                return db.SaveChanges();
            }
        }


        /// <summary>
        /// 批量更新
        /// </summary>
        public void UpdateHREployee()
        {
            try
            {
                List<HREmployee> list = NonExecute<List<HREmployee>>(db =>
                {
                    return db.HREmployee.ToList();
                });

                foreach (var tmp in list)
                {
                    tmp.isSync = false;
                    tmp.syncDate = DateTime.Now;
                    UpdateHREployee(tmp);
                }
            }
            catch (Exception ex)
            {
                new LogDAL().AddsysErrorLog(ex.ToString());
                throw ex;
            }
        }

        public bool ExportExcelHrEmployee(string filename, string newfilename, string gonghao, string name, string department, string gangwei, string lizhiriqi, string lizhiriqi2, string shifoulizhi)
        {
            IAMEntities db = new IAMEntities();
            var list = db.HREmployee.Join(db.HRDepartment, item => item.dept, itm => itm.dept, (item, itm) => new
            {
                code = item.code,
                posts = item.posts,
                deptname = itm.name,
                name = item.name,
                moblephone = item.moblePhone,
                topostDate = item.toPostsDate,
                leavePostsDate = item.leavePostsDate,
                userScope = item.userScope,
                isSync = item.isSync,
                syncdate = item.syncDate
            }).ToList();

            if (!string.IsNullOrEmpty(gonghao))
                list = list.Where(item => item.code.Contains(gonghao)).ToList();
            if (!string.IsNullOrEmpty(name))
                list = list.Where(itme => itme.name.Contains(name)).ToList();
            if (!string.IsNullOrEmpty(department))
            {
                list = list.Where(item => item.deptname != null).ToList();
                list = list.Where(item => item.deptname.Contains(department)).ToList();
            }

            if (!string.IsNullOrEmpty(gangwei))
            {
                list = list.Where(item => item.posts != null).ToList();
                list = list.Where(item => item.posts.Contains(gangwei)).ToList();
            }

            if (!string.IsNullOrEmpty(shifoulizhi))
            {
                if (shifoulizhi.Equals("0"))//在职
                    list = list.Where(item => item.leavePostsDate == null).ToList();
                if (shifoulizhi.Equals("1")) //离职
                    list = list.Where(item => item.leavePostsDate != null).ToList();
            }
            if (!string.IsNullOrEmpty(lizhiriqi) && !string.IsNullOrEmpty(lizhiriqi2))
            {
                DateTime d = Convert.ToDateTime(lizhiriqi);
                DateTime d2 = Convert.ToDateTime(lizhiriqi2);
                list = list.Where(item => item.leavePostsDate != null).ToList();
                list = list.Where(item => item.leavePostsDate >= d && item.leavePostsDate <= d2).ToList();
            }

            if (list == null || list.Count <= 0)
            {
                throw new Exception("数据为空");

            }

            System.Data.DataTable dt = list.Select(a => new
            {
                工号 = a.code,
                姓名 = a.name,
                部门 = a.deptname,
                岗位 = a.posts,
                手机 = a.moblephone,
                到职日期 = a.topostDate,
                离职日期 = a.leavePostsDate,
                人员归属范围 = a.userScope
            }).ToDataTable();
            return OLEDBExcelHelper.OLEDBExcelHelper.Write(filename, newfilename, dt);
        }

        public List<HREmployee> HREmployeeList(int PageSize, int PageIndex, string EmployeeNumber, string EmployeeEncode, string EmployeeName, out int count)
        {
            var list = NonExecute<List<HREmployee>>(db =>
            {
                var tmp = db.HREmployee.ToList();
                if (!string.IsNullOrEmpty(EmployeeNumber))
                    tmp = tmp.Where(item => item.code == EmployeeNumber).ToList();
                if (!string.IsNullOrEmpty(EmployeeName))
                    tmp = tmp.Where(item => item.name == EmployeeName).ToList();
                if (!string.IsNullOrEmpty(EmployeeEncode))
                    tmp = tmp.Where(item => item.userScope == EmployeeEncode).ToList();
                return tmp;
            });
            count = list.Count;
            return list.OrderByDescending(item => item.syncDate).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }


        /// <summary>
        /// 同步HREmployee 信息
        /// </summary>
        /// <param name="item"></param>
        public void SyncHrEmployee(HREmployee item)
        {
            try
            {
                HREmployee newentity = NonExecute<HREmployee>(db =>
                {
                    return db.HREmployee.FirstOrDefault(i => i.Pk_psndoc == item.Pk_psndoc);

                });
                if (newentity != null)
                {
                    
                    //检查是否存在于AccountMaping表中
                    var maplist = NonExecute<List<AccountMaping>>(db => { return db.AccountMaping.Where(i => i.gonghao == item.code).ToList(); });
                    if (maplist.Count > 0)
                    {
                        StringBuilder stb = new StringBuilder();
                        //maping中存在数据，检查字段
                        if (newentity.dept != item.dept)
                        {
                            NonExecute(db => db.sys_HRUserResolution.AddObject(new sys_HRUserResolution() { cardNo = item.code, createtime = DateTime.Now, id = Guid.NewGuid(), name = item.name, porpert = "部门", state = "未处理", newvalue = item.dept, oldvalue = newentity.dept }));
                            var oldept = NonExecute(db => { return db.HRDepartment.FirstOrDefault(x => x.dept == item.dept); });
                            var newdept = NonExecute(db => { return db.HRDepartment.FirstOrDefault(x => x.dept == newentity.dept); });
                            if (oldept != null && newdept != null)
                                stb.Append(string.Format("<font style=\"color:blue\">部门由{0}变为{1},</font>", newdept.name, oldept.name));
                            else
                                stb.Append(string.Format("<font style=\"color:blue\">部门由{0}变为{1},</font>", item.dept, newentity.dept));

                        }
                        if (newentity.posts != item.posts)
                        {
                            NonExecute(db => db.sys_HRUserResolution.AddObject(new sys_HRUserResolution() { cardNo = item.code, createtime = DateTime.Now, id = Guid.NewGuid(), name = item.name, porpert = "岗位", state = "未处理", newvalue = item.posts, oldvalue = newentity.posts }));
                            stb.Append(string.Format("<font style=\"color:red\">岗位由{0}变为{1},</font>", newentity.posts, item.posts));
                        }
                        if (!newentity.leavePostsDate.Equals(item.leavePostsDate))
                        {
                            NonExecute(db => db.sys_HRUserResolution.AddObject(new sys_HRUserResolution() { cardNo = item.code, createtime = DateTime.Now, id = Guid.NewGuid(), name = item.name, porpert = "离职日期", state = "未处理", newvalue = item.toPostsDate.HasValue ? item.leavePostsDate.ToString() : "", oldvalue = newentity.leavePostsDate.HasValue ? newentity.leavePostsDate.ToString() : "" }));
                            stb.Append(string.Format("<font style=\"color:red\">离职日期由{0}变为{1},</font>", newentity.leavePostsDate, item.leavePostsDate));
                        }

                        if (!newentity.code.Equals(item.code))
                        {
                            NonExecute(db => db.sys_HRUserResolution.AddObject(new sys_HRUserResolution() { cardNo = item.code, createtime = DateTime.Now, id = Guid.NewGuid(), name = item.name, porpert = "工号", state = "未处理", newvalue = item.code, oldvalue =newentity.code}));
                            stb.Append(string.Format("<font style=\"color:red\">工号由{0}变为{1},</font>", newentity.code, item.code));
                        }

                        if (stb.ToString().Length > 0)
                        {
                            //发送邮件
                            SendMail(maplist, stb.ToString(), item.code);
                        }
                        //更新数据
                        UpdateHREployee(item);
                    }
                    else
                    {
                        UpdateHREployee(item);
                    }

                }
                else
                {
                        AddHREmployee(item);
                        
                }

            }
            catch (Exception ex)
            {
                (new LogDAL()).AddsysErrorLog(string.Format("HR员工同步错误:{0}:{1}",ex.Message,ex.ToString()));
            }
        }

        private void SendMail(List<AccountMaping> list, string biangengneirong,string gonghao)
        {
            if (list == null && list.Count < 0)
            {
                return;
            }

            string body = "";
            if (list.FirstOrDefault(item => item.type == "AD") != null)
            {//ad系统
                MailInfo info = new MailInfo();
                info.Authorizationid = new Guid(System.Web.Configuration.WebConfigurationManager.AppSettings["mailAurthID"]);
                info.SendMode = 1;
                info.SendTime = DateTime.Now;
                string maddress = "";
                maddress = new TaskEmailDAL().GetList().FirstOrDefault(item => item.SystemName.Trim() == "AD").EmailAddress;
                if (!string.IsNullOrEmpty(maddress))
                    info.To = maddress.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                else
                    info.To = new string[] { "yangjian@shac.com.cn" };
                info.Subject = "IAM员工冲突通知-AD账号信息";
                info.URLS = new string[] { };
                StringBuilder stbuser = new StringBuilder();
                stbuser.Append("<table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><tr><th>工号</th><th>系统类型</th><th>账号</th><th>账号类型</th></tr>");
                
                foreach (var user in list.Where(item=>item.type=="AD"))
                {
                    stbuser.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", user.gonghao, user.type, user.zhanghao, user.UserType));
                    
                }
                stbuser.Append("</table>");
                
                AddUserMail1 aa = new AddUserMail1();
                aa.SystemName = "AD";
                aa.RoleString = "";
                aa.SystemType = "";
                aa.UserInfo = stbuser.ToString();
                aa.Actioner = "IAM系统";
                aa.gonghao = gonghao;
                aa.biangengneirong = biangengneirong;
                MailTemplateHelper.RunTemplate<AddUserMail1>(aa, "HREmployee");
                info.Body = MailTemplateHelper.RunTemplate<AddUserMail1>(aa, "HREmployee");
                MySendMail.Send(info);
            }
            if (list.FirstOrDefault(item => item.type == "ADComputer") != null)
            {//Comporty系统
                MailInfo info = new MailInfo();
                info.Authorizationid = new Guid(System.Web.Configuration.WebConfigurationManager.AppSettings["mailAurthID"]);
                info.SendMode = 1;
                info.SendTime = DateTime.Now;
                string maddress = "";
                maddress = new TaskEmailDAL().GetList().FirstOrDefault(item => item.SystemName.Trim() == "AD").EmailAddress;
                if (!string.IsNullOrEmpty(maddress))
                    info.To = maddress.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                else
                    info.To = new string[] { "yangjian@shac.com.cn" };
                info.Subject = "IAM员工冲突通知-ADComputer账号信息";
                info.URLS = new string[] { };
                StringBuilder stbuser = new StringBuilder();
                stbuser.Append("<table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><tr><th>工号</th><th>系统类型</th><th>账号</th><th>账号类型</th></tr>");
               
                foreach (var user in list.Where(item=>item.type=="ADComputer"))
                {
                    stbuser.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", user.gonghao, user.type, user.zhanghao, user.UserType));                    
                }
                stbuser.Append("</table>");

                AddUserMail1 aa = new AddUserMail1();
                aa.SystemName = "ADComputer";
                aa.RoleString = "";
                aa.SystemType = "";
                aa.UserInfo = stbuser.ToString();
                aa.Actioner = "IAM系统";
                aa.biangengneirong = biangengneirong;
                aa.gonghao = gonghao;
                MailTemplateHelper.RunTemplate<AddUserMail1>(aa, "HREmployee");
                info.Body = MailTemplateHelper.RunTemplate<AddUserMail1>(aa, "HREmployee");
                MySendMail.Send(info);
            }
            if (list.FirstOrDefault(item => item.type == "SAP") != null)
            {//SAP系统
                MailInfo info = new MailInfo();
                info.Authorizationid = new Guid(System.Web.Configuration.WebConfigurationManager.AppSettings["mailAurthID"]);
                info.SendMode = 1;
                info.SendTime = DateTime.Now;
                string maddress = "";
                maddress = new TaskEmailDAL().GetList().FirstOrDefault(item => item.SystemName.Trim() == "SAP").EmailAddress;
                if (!string.IsNullOrEmpty(maddress))
                    info.To = maddress.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                else
                    info.To = new string[] { "yangjian@shac.com.cn" };
                info.Subject = "IAM员工冲突通知-SAP账号信息";
                info.URLS = new string[] { };
                StringBuilder stbuser = new StringBuilder();
                stbuser.Append("<table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><tr><th>工号</th><th>系统类型</th><th>账号</th><th>账号类型</th></tr>");
                
                foreach (var user in list.Where(item=>item.type=="SAP"))
                {
                    stbuser.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", user.gonghao, user.type, user.zhanghao, user.UserType));                   
                }
                stbuser.Append("</table>");

                AddUserMail1 aa = new AddUserMail1();
                aa.SystemName = "SAP";
                aa.RoleString = "";
                aa.SystemType = "";
                aa.UserInfo = stbuser.ToString();
                aa.Actioner = "IAM系统";
                aa.biangengneirong = biangengneirong;
                aa.gonghao = gonghao;
                MailTemplateHelper.RunTemplate<AddUserMail1>(aa, "HREmployee");
                info.Body = MailTemplateHelper.RunTemplate<AddUserMail1>(aa, "HREmployee");
                MySendMail.Send(info);
            }
            if (list.FirstOrDefault(item => item.type == "HEC") != null)
            {//HEC系统
                MailInfo info = new MailInfo();
                info.Authorizationid = new Guid(System.Web.Configuration.WebConfigurationManager.AppSettings["mailAurthID"]);
                info.SendMode = 1;
                info.SendTime = DateTime.Now;
                string maddress = "";
                maddress = new TaskEmailDAL().GetList().FirstOrDefault(item => item.SystemName.Trim() == "HEC").EmailAddress;
                if (!string.IsNullOrEmpty(maddress))
                    info.To = maddress.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                else
                    info.To = new string[] { "yangjian@shac.com.cn" };
                info.Subject = "IAM员工冲突通知-HEC账号信息";
                info.URLS = new string[] { };
                StringBuilder stbuser = new StringBuilder();
                stbuser.Append("<table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><tr><th>工号</th><th>系统类型</th><th>账号</th><th>账号类型</th></tr>");
               
                foreach (var user in list.Where(item=>item.type=="HEC"))
                {
                    stbuser.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", user.gonghao, user.type, user.zhanghao, user.UserType));
                    
                }
                stbuser.Append("</table>");

                AddUserMail1 aa = new AddUserMail1();
                aa.SystemName = "HEC";
                aa.RoleString = "";
                aa.SystemType = "";
                aa.UserInfo = stbuser.ToString();
                aa.Actioner = "IAM系统";
                aa.gonghao = gonghao;
                aa.biangengneirong = biangengneirong;
                MailTemplateHelper.RunTemplate<AddUserMail1>(aa, "HREmployee");
                info.Body = MailTemplateHelper.RunTemplate<AddUserMail1>(aa, "HREmployee");
                MySendMail.Send(info);
            }
            if (list.FirstOrDefault(item => item.type == "HR") != null)
            {//EHR系统
                MailInfo info = new MailInfo();
                info.Authorizationid = new Guid(System.Web.Configuration.WebConfigurationManager.AppSettings["mailAurthID"]);
                info.SendMode = 1;
                info.SendTime = DateTime.Now;
                string maddress = "";
                maddress = new TaskEmailDAL().GetList().FirstOrDefault(item => item.SystemName.Trim() == "HR").EmailAddress;
                if (!string.IsNullOrEmpty(maddress))
                    info.To = maddress.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                else
                    info.To = new string[] { "yangjian@shac.com.cn" };
                info.Subject = "IAM员工冲突通知-HR账号信息";
                info.URLS = new string[] { };
                StringBuilder stbuser = new StringBuilder();
                stbuser.Append("<table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><tr><th>工号</th><th>系统类型</th><th>账号</th><th>账号类型</th></tr>");
               
                foreach (var user in list.Where(item=>item.type=="HR"))
                {
                    stbuser.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", user.gonghao, user.type, user.zhanghao, user.UserType));
                   
                }
                stbuser.Append("</table>");

                AddUserMail1 aa = new AddUserMail1();
                aa.SystemName = "HR";
                aa.RoleString = "";
                aa.SystemType = "";
                aa.UserInfo = stbuser.ToString();
                aa.Actioner = "IAM系统";
                aa.gonghao = gonghao;
                aa.biangengneirong = biangengneirong;
                MailTemplateHelper.RunTemplate<AddUserMail1>(aa, "HREmployee");
                info.Body = MailTemplateHelper.RunTemplate<AddUserMail1>(aa, "HREmployee");
                MySendMail.Send(info);
            }
            if (list.FirstOrDefault(item => item.type == "TC") != null)
            {//TC系统
                MailInfo info = new MailInfo();
                info.Authorizationid = new Guid(System.Web.Configuration.WebConfigurationManager.AppSettings["mailAurthID"]);
                info.SendMode = 1;
                info.SendTime = DateTime.Now;
                string maddress = "";
                maddress = new TaskEmailDAL().GetList().FirstOrDefault(item => item.SystemName.Trim() == "TC").EmailAddress;
                if (!string.IsNullOrEmpty(maddress))
                    info.To = maddress.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                else
                    info.To = new string[] { "yangjian@shac.com.cn" };
                info.Subject = "IAM员工冲突通知-TC账号信息";
                info.URLS = new string[] { };
                StringBuilder stbuser = new StringBuilder();
                stbuser.Append("<table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><tr><th>工号</th><th>系统类型</th><th>账号</th><th>账号类型</th></tr>");
               
                foreach (var user in list.Where(item=>item.type=="TC"))
                {
                    stbuser.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", user.gonghao, user.type, user.zhanghao, user.UserType));                   
                }
                stbuser.Append("</table>");

                AddUserMail1 aa = new AddUserMail1();
                aa.SystemName = "TC";
                aa.RoleString = "";
                aa.SystemType = "";
                aa.UserInfo = stbuser.ToString();
                aa.Actioner = "IAM系统";
                aa.gonghao = gonghao;
                aa.biangengneirong = biangengneirong;
                MailTemplateHelper.RunTemplate<AddUserMail1>(aa, "HREmployee");
                info.Body = MailTemplateHelper.RunTemplate<AddUserMail1>(aa, "HREmployee");
                MySendMail.Send(info);
            }
        }


    }
}
