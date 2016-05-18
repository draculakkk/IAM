using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class BaseQuery
    {
        protected Guid nid = new Guid("00000000-0000-0000-0000-000000000000");
        internal static TResult NonExecute<TResult>(Func<IAMEntities,TResult> action) 
        {
            using (IAMEntities db = new IAMEntities())
            {
                TResult tresult = action(db);
                db.SaveChanges();
                return tresult;
            }
        }

        internal static int NonExecute(Action<IAMEntities> action)
        {
            using (IAMEntities db = new IAMEntities())
            {
                action(db);
                return db.SaveChanges();
            }
        }
    }
}
