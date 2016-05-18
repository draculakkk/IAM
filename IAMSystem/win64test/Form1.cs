using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IAMEntityDAL;

namespace win64test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IAMEntityDAL.AD_Department_WorkGroupDAL kk = new IAMEntityDAL.AD_Department_WorkGroupDAL();
                var items = kk.GetList();
                int count = 0;
                foreach (var item in items)
                {
                    item.p2 = string.Format("{0} {1} {2}", item.Center, item.Department, item.KeShi).Trim();
                    kk.UpdateAd_Department_WorkGroup(item);
                    count++;
                }
                MessageBox.Show("成功更新了" + count.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectedText != "")
            {
                Clipboard.SetDataObject(textBox1.SelectedText);
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bijiao f = new bijiao();
            f.Show();
        }


    }
}
