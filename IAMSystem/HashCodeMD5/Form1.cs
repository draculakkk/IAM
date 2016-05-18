using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Security.Cryptography;

namespace HashCodeMD5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnMD5_Click(object sender, EventArgs e)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytvalue,bytHash;
            bytvalue = System.Text.Encoding.UTF8.GetBytes(txtvalue.Text.Trim());
            bytHash = md5.ComputeHash(bytvalue);
            md5.Clear();
            for (int i = 0; i < bytHash.Length; i++)
            {
                txtjiami.Text += bytHash[i].ToString("x").PadLeft(2,'0');
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] bytes = System.Text.Encoding.Default.GetBytes(txtvalue.Text.Trim());
            txtjiami.Text = Convert.ToBase64String(bytes);
        }
    }
}
