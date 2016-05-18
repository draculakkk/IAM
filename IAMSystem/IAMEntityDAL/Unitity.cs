using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;

namespace IAMEntityDAL
{
    [Serializable]
    public class PageComputerInfo
    {
        public BaseDataAccess.AD_Computer ComputerInfo { get; set; }
        public List<BaseDataAccess.AD_Computer_WorkGroups> ADComputerGroups { get; set; }
    }

    public static class Unitity
    {
        public enum SystemType
        {
            AD = 1,
            HR = 2,
            SAP = 3,
            HEC = 4,
            TC = 5,
            ADComputer = 6
        }

        public enum UserType
        {
            员工 = 0,
            系统 = 1,
            其他 = 2
        }

        

        public static System.Data.DataTable ToDataTable<TSource>(this IEnumerable<TSource> list)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            IEnumerable<TSource> source = list;
            var t = source.GetEnumerator();
            if (t.MoveNext())
            {
                PropertyInfo[] properti = t.Current.GetType().GetProperties();
                for (int i = 0; i < properti.Length; i++)
                {
                    dt.Columns.Add(properti[i].Name);
                }
                object[] array = new object[properti.Length];
                do
                {
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = properti[i].GetValue(t.Current, null);
                    }
                    dt.LoadDataRow(array, true);
                } while (t.MoveNext());
            }

            return dt;
        }
    }
}
