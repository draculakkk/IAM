using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace IAMEntityDAL
{
    /// <summary>
    /// 同步任务管理器
    /// </summary>
    public static class SyncTask
    {
        public class taskModule {
            public string name { get; set; }
            public DateTime bgtime { get; set; }
            public bool isrun { get; set; }
            public DateTime edtime { get; set; }
            public int AllCount { get; set; }
            public int OkCount { get; set; }
            public DateTime runTime { get; set; }
            public Action<taskModule> func { get; set; }
            public Thread thread { get; set; }
            public DateTime actionRunTime { get; set; }
        }
       public static List<taskModule> tasklist = new List<taskModule>();
        /// <summary>
        /// 计时器自动调用到期task
        /// </summary>
        static Timer timer = new Timer((x) => {
            DateTime t=DateTime.Now;
            foreach (var item in tasklist)
            {
                var rdata = DateTime.Now.AddHours( item.runTime.Hour-t.Hour).AddMinutes( item.runTime.Minute-t.Minute ).AddMilliseconds( item.runTime.Millisecond-t.Millisecond);
                if (rdata > t.AddMinutes(-1) && rdata < t.AddMinutes(1) 
                    && item.actionRunTime.AddMinutes(2)< t)
                {
                    item.actionRunTime = DateTime.Now;
                    //到运行时了
                    RunTask(item);
                }
            }
        },null,1000,10000);
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="name"></param>
        /// <param name="runTime"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static taskModule AddTask(string name,DateTime runTime,Action<taskModule> func)
        {
            if (tasklist.Where(item => item.name == name).FirstOrDefault() != null) return null;
            var x = new taskModule() { name = name, isrun = false, runTime = runTime, func = (m) => {
                if (m.isrun) return;
                m.isrun = true;
                m.bgtime = DateTime.Now;
                func(m);
                m.edtime = DateTime.Now;
                LogDAL log = new LogDAL();
                log.AddSyncLog(m.name, m.OkCount, m.AllCount - m.OkCount, m.edtime - m.bgtime);
                m.isrun = false;
            } };
            tasklist.Add(x);
            return x;
        }
        /// <summary>
        /// 添加任务 从数据库初始化运行时间
        /// </summary>
        /// <param name="name"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static taskModule AddTask(string name, Action<taskModule> func)
        {
            try { 
            IAMEntityDAL.SyncConfigDAL da = new SyncConfigDAL();
            var list=da.ReturnSyncConfigList();
            var one=list.Where(item => item.asyncName == name).FirstOrDefault();
            var date = new DateTime(1989, 11, 28, 23, 55, 55);
            if(one!=null )
            {
                date = one.datetime;
            }
            return AddTask(name, date, func);
            }
            catch (Exception e)
            {
                try
                {
                    IAMEntityDAL.LogDAL log = new LogDAL();
                    log.AddsysErrorLog(e.ToString());
                }
                catch { }
                return null;
            }
        }
        /// <summary>
        /// 开始任务
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static taskModule RunTask(string name)
        {
            var x = tasklist.Where(item => item.name == name).FirstOrDefault();
            if (x == null) return null;
            return RunTask(x);
        }
        /// <summary>
        /// 开始任务
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static taskModule RunTask(taskModule m)
        {
            try
            {
                if (m.isrun) return m;
                System.Threading.Thread t = new System.Threading.Thread(() =>
                {
                    try
                    {
                        m.func(m);
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            IAMEntityDAL.LogDAL log = new LogDAL();
                            log.AddsysErrorLog(e.ToString());
                        }
                        catch { }
                    }
                });
                m.thread = t;
                if (m.thread.ThreadState == ThreadState.Running) return m;
                m.thread.Start();
                return m;
            }
            catch (Exception ex)
            {
                IAMEntityDAL.LogDAL log = new LogDAL();
                log.AddsysErrorLog(ex.ToString());
                return m;
            }
        }
        /// <summary>
        /// 
        /// 停止任务
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static taskModule StopTask(taskModule m){
            if (!m.isrun) return m;
            if(m.thread!=null) m.thread.Abort();
            return m;
        }
        /// <summary>
        /// 停止任务
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static taskModule StopTask(string name)
        {
            var x = tasklist.Where(item => item.name == name).FirstOrDefault();
            if (x == null) return null;
            return StopTask(x);
        }
    }
}
