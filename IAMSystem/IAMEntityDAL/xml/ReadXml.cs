using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace IAMEntityDAL.xml
{
    public class ReadXml
    {
        Dictionary<string, string> _DictionaryZhTC = new Dictionary<string, string>();
        Dictionary<string, string> _DictionaryZhHEC = new Dictionary<string, string>();
        Dictionary<string, string> _DictionaryZhSAP = new Dictionary<string, string>();
        Dictionary<string, string> _DictionaryZhHR = new Dictionary<string, string>();
        Dictionary<string, string> _DictionaryZhAD = new Dictionary<string, string>();
        Dictionary<string, string> _DictionaryZhADC = new Dictionary<string, string>();

        Dictionary<string, string> DictionaryZhTC {
            get { return _DictionaryZhTC; }
            set { _DictionaryZhTC = value; }
        }
        Dictionary<string, string> DictionaryZhHEC {
            get { return _DictionaryZhHEC; }
            set { _DictionaryZhHEC = value; }
        }
        Dictionary<string, string> DictionaryZhSAP {
            get { return _DictionaryZhSAP; }
            set { _DictionaryZhSAP = value; }
        }
        Dictionary<string, string> DictionaryZhHR
        {
            get { return _DictionaryZhHR; }
            set { _DictionaryZhHR = value; }
        }
        Dictionary<string, string> DictionaryZhAD {
            get { return _DictionaryZhAD; }
            set { _DictionaryZhAD = value; }
        }
        Dictionary<string, string> DictionaryZhADC {
            get { return _DictionaryZhADC; }
            set {
                _DictionaryZhADC = value;
            }
        }
        Unitity.SystemType SystemName
        {
            get;
            set;
        }



        public ReadXml(Unitity.SystemType systemName)
        {
            string tmp = systemName.ToString();
            string file = "";
            if (System.Web.HttpContext.Current != null)
            {
                file = System.Web.HttpContext.Current.Server.MapPath("~/xml/" + systemName.ToString() + ".xml");
            }
            else
            {
              file= AppDomain.CurrentDomain.BaseDirectory.ToString()+ "\\"+"xml\\"+tmp+".xml";
            }
            SystemName = systemName;
            string cachename = systemName.ToString();
            if (!System.IO.File.Exists(file))
            { throw new Exception("未找到语言配置文件"); }          
            else
            {
                XmlDocument xmld = new XmlDocument();
                xmld.Load(file);
                var ndlist = xmld.ChildNodes[2];
                foreach (XmlNode x in ndlist)
                {
                    SetValue(x.Attributes["key"].Value, x.Attributes["zh"].Value);
                }
                //switch (SystemName)
                //{
                //    case Unitity.SystemType.AD: System.Web.HttpContext.Current.Cache[cachename] = DictionaryZhAD; break;
                //    case Unitity.SystemType.HR: System.Web.HttpContext.Current.Cache[cachename] = DictionaryZhHR; break;
                //    case Unitity.SystemType.HEC: System.Web.HttpContext.Current.Cache[cachename] = DictionaryZhHEC; break;
                //    case Unitity.SystemType.SAP: System.Web.HttpContext.Current.Cache[cachename] = DictionaryZhSAP; break;
                //    case Unitity.SystemType.TC: System.Web.HttpContext.Current.Cache[cachename] = DictionaryZhTC; break;
                //    case Unitity.SystemType.ADComputer: System.Web.HttpContext.Current.Cache[cachename] = DictionaryZhADC; break;
                //}
            }
        }

        void SetValue(string key, string value)
        {
            switch (SystemName)
            {
                case Unitity.SystemType.AD: DictionaryZhAD[key] = value; break;
                case Unitity.SystemType.HR: DictionaryZhHR[key] = value; break;
                case Unitity.SystemType.HEC: DictionaryZhHEC[key] = value; break;
                case Unitity.SystemType.SAP: DictionaryZhSAP[key] = value; break;
                case Unitity.SystemType.TC: DictionaryZhTC[key] = value; break;
                case Unitity.SystemType.ADComputer: DictionaryZhADC[key] = value; break;
            }
        }


        public string GetChineseStringByKey(string key)
        {
            string output = "";
            switch (SystemName)
            {
                case Unitity.SystemType.AD: DictionaryZhAD.TryGetValue(key,out output); break;
                case Unitity.SystemType.HR: DictionaryZhHR.TryGetValue(key, out output); break;
                case Unitity.SystemType.HEC: DictionaryZhHEC.TryGetValue(key, out output); break;
                case Unitity.SystemType.SAP: DictionaryZhSAP.TryGetValue(key, out output); break;
                case Unitity.SystemType.TC: DictionaryZhTC.TryGetValue(key, out output); break;
                case Unitity.SystemType.ADComputer: DictionaryZhADC.TryGetValue(key, out output); break;
            }
            return output;
        }
    }
}
