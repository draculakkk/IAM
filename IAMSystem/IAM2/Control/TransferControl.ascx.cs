using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAM.Control
{
    public partial class TransferControl : System.Web.UI.UserControl
    {
        string _systemtype;
        public string SystemType { get { return _systemtype; } set { _systemtype = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}