using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace win64test
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int pageindex = Convert.ToInt32(textBox1.Text.Trim())-1;
            int pageSize = 10;
            int flag = 0;
            List<Student> list1 = new List<Student>();
            for (int i = 0; i<list.Count; i++)
            {
                if (i > pageSize * pageindex)
                {
                    list1.Add(list[i]);
                    flag++;
                }
                if (flag >= pageSize)
                {
                    break;
                }
            }

            int page = list.Count % pageSize>0?list.Count/pageSize+1:list.Count;
            label1.Text = string.Format("共 {0}条  {1}/{2}页",list.Count,pageindex+1,page);

            dataGridView1.DataSource = list1;

        }
        List<Student> list = new List<Student>();
        private void Form2_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 999; i++)
            {
                Student s = new Student() { Address="地址"+i.ToString("000"),Age=i,Name="姓名"+i.ToString("000")};
                list.Add(s);
            }
            label1.Text = "共 " + list.Count.ToString() + " 条";
        }
    }
}
