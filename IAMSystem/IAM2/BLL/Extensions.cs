using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace IAM.BLL
{
    /// <summary>
    /// List 完全复制 及复制后的listNew 完全不受Listold操作影响
    /// </summary>
    public class Extensions
    {
        public static T Clone<T>(T
            RealObject)
        {
            using (Stream ObjectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ObjectStream,RealObject);
                ObjectStream.Seek(0,SeekOrigin.Begin);
                return (T)formatter.Deserialize(ObjectStream);
            }
        }
    }
}