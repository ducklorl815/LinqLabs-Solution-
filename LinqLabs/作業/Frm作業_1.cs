using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);

            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },
                                          };
        }

        List<Student> students_scores;

        public class Student
        {
            public string Name { get; set; }
            public string Class { get; set; }
            public int Chi { get; set; }
            public int Eng { get; internal set; }
            public int Math { get; set; }
            public string Gender { get; set; }
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }
        int count;
        private void button13_Click(object sender, EventArgs e)
        {
            count++;
            int num = int.Parse(textBox1.Text);

                var q = from p in nwDataSet1.Products.Take(count * num).Skip((count - 1)* num)
                        orderby p.ProductID
                        select p;
            //MessageBox.Show(nwDataSet1.Products.Count + "");
            if (nwDataSet1.Products.Count < (count-1) * num)
            { count--; return; }
                
            this.dataGridView1.DataSource = q.ToList();

            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10)

            //Distinct()
        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files =  dir.GetFiles();

            this.dataGridView1.DataSource = files;
        }

        private void button36_Click(object sender, EventArgs e)
        {
            #region 搜尋 班級學生成績
            this.dataGridView1.DataSource = students_scores;

            // 共幾個 學員成績 ?

            //this.dataGridView1.DataSource = students_scores;
            //var q = from p in students_scores
            //        select p;

            // 找出 前面三個 的學員所有科目成績		

            //this.dataGridView1.DataSource = students_scores;
            //var q = from p in students_scores.Take(3)
            //        select p;

            // 找出 後面兩個 的學員所有科目成績	

            //var q = from p in students_scores
            //        orderby p.Name descending
            //        select p;
            //dataGridView2.DataSource = q.Take(2).ToList();

            // 找出 Name 'aaa','bbb','ccc' 的學成績	

            //this.dataGridView1.DataSource = students_scores;
            //var q = from p in students_scores
            //        where p.Name == "aaa" || p.Name == "bbb" || p.Name == "ccc"
            //        select p;

            // 找出學員 'bbb' 的成績	                          

            //this.dataGridView1.DataSource = students_scores;
            //var q = from p in students_scores
            //        where p.Name == "bbb"
            //        select p;

            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	

            //this.dataGridView1.DataSource = students_scores;
            //var q = from p in students_scores
            //        where p.Name != "bbb"
            //        select p;

            // 數學不及格 ... 是誰 

            //this.dataGridView1.DataSource = students_scores;
            //var q = from p in students_scores
            //        where p.Math < 60
            //        select p;

            #endregion

            //dataGridView2.DataSource = q.ToList();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");


            System.IO.FileInfo[] files = dir.GetFiles();
            var q = from x in dir.GetFiles()
                    where x.CreationTime.Year == 2019
                    orderby x.CreationTime.Minute ascending
                    select x;
            //this.dataGridView1.DataSource = dir.GetFiles();
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");


            System.IO.FileInfo[] files = dir.GetFiles();
            var q = from x in dir.GetFiles()
                    where x.Length > 102400
                    orderby x.Length descending
                    select x;
            //this.dataGridView1.DataSource = dir.GetFiles();
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            count = 0;
            var p = from q in nwDataSet1.Orders
                    select new
                    {
                        q.CustomerID,
                        q.EmployeeID,
                        q.OrderDate,
                        q.RequiredDate,
                        //q.ShippedDate,
                        q.ShipVia,
                        q.Freight,
                        q.ShipName,
                        q.ShipAddress,
                        q.ShipCity,
                        //q.ShipRegion,
                        //q.ShipPostalCode,
                        q.ShipCountry
                    }
                    ;
            dataGridView1.DataSource= p.ToList();
            var r = from s in nwDataSet1.Orders
                    group s by s.OrderDate.Year into Y
                    select Y.Key;
            //comboBox1.Items.Add(p.Distinct());
            comboBox1.DataSource = r.ToList();
            //comboBox1.Items.Add(p.Distinct());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            count = 0;
            if (comboBox1.SelectedItem == null)
                return;
            var q1 = from p1 in nwDataSet1.Orders
                    join r1 in nwDataSet1.Order_Details
                    on p1.OrderID equals r1.OrderID
                    where p1.OrderDate.Year == (int)comboBox1.SelectedItem
                    select new
                    {
                        p1.CustomerID,
                        p1.EmployeeID,
                        p1.OrderDate,
                        p1.RequiredDate,
                        //p1.ShippedDate,
                        p1.ShipVia,
                        p1.Freight,
                        p1.ShipName,
                        p1.ShipAddress,
                        p1.ShipCity,
                        //p1.ShipRegion,
                        //p1.ShipPostalCode,
                        p1.ShipCountry
                    };
            var q2 = from p2 in nwDataSet1.Orders
                     where p2.OrderDate.Year == (int)comboBox1.SelectedItem
                     select new
                     {
                         p2.CustomerID,
                         p2.EmployeeID,
                         p2.OrderDate,
                         p2.RequiredDate,
                         //p2.ShippedDate,
                         p2.ShipVia,
                         p2.Freight,
                         p2.ShipName,
                         p2.ShipAddress,
                         p2.ShipCity,
                         //p2.ShipRegion,
                         //p2.ShipPostalCode,
                         p2.ShipCountry
                     };


            dataGridView2.DataSource = q1.ToList();
            dataGridView1.DataSource = q2.ToList();

            //dataGridView2.
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10)
            if (count-1 <= 0)
                return; 
            int num = int.Parse(textBox1.Text);
            count--;
            var q = from p in nwDataSet1.Products.Take(count * num).Skip((count - 1) * num)
                    orderby p.ProductID
                    select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            var q1 = from p1 in nwDataSet1.Orders
                     join r1 in nwDataSet1.Order_Details
                     on p1.OrderID equals r1.OrderID
                     where p1.OrderDate.Year == (int)comboBox1.SelectedItem
                     select new
                     {
                         p1.CustomerID,
                         p1.EmployeeID,
                         p1.OrderDate,
                         p1.RequiredDate,
                         //p1.ShippedDate,
                         p1.ShipVia,
                         p1.Freight,
                         p1.ShipName,
                         p1.ShipAddress,
                         p1.ShipCity,
                         //p1.ShipRegion,
                         //p1.ShipPostalCode,
                         p1.ShipCountry
                     };
            var q2 = from p2 in nwDataSet1.Orders
                     where p2.OrderDate.Year == (int)comboBox1.SelectedItem
                     select new
                     {
                         p2.CustomerID,
                         p2.EmployeeID,
                         p2.OrderDate,
                         p2.RequiredDate,
                         //p2.ShippedDate,
                         p2.ShipVia,
                         p2.Freight,
                         p2.ShipName,
                         p2.ShipAddress,
                         p2.ShipCity,
                         //p2.ShipRegion,
                         //p2.ShipPostalCode,
                         p2.ShipCountry
                     };


            dataGridView2.DataSource = q1.ToList();
            dataGridView1.DataSource = q2.ToList();

        }
    }
}
