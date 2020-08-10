using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace AmvalSystem
{
    public partial class frmExit : Form
    {
        public frmExit()
        {
            InitializeComponent();
        }
        ConnectionDataContext connect = new ConnectionDataContext();
        private void frmExit_Load(object sender, EventArgs e)
        {
            var querymonth = from u in connect.tblMonths select u;
            comboBox1.DataSource = querymonth;
            comboBox1.DisplayMember = "MonthName";
            comboBox1.ValueMember = "MonthID";
            var queryyear = from u in connect.tblYears select u;
            comboBox2.DataSource = queryyear;
            comboBox2.DisplayMember = "YearName";
            comboBox2.ValueMember = "YearID";
        }
        private void FillDate()
        {
            dataGridView1.Rows.Clear();
            var query = from u in connect.tblSannads where u.MonthID == Convert.ToInt32(comboBox1.SelectedValue) && u.YearID == Convert.ToInt32(comboBox2.SelectedValue) group u by new { u.tblEtebar.CodeHazineh , u.momo} into gr select new { CodeHazineh = gr.Key.CodeHazineh, Price = gr.Sum(q => q.Price) , memo = gr.Key.momo };
            foreach (var myread in query)
            {
                dataGridView1.Rows.Add(1, "مرکز ملی پرورش استعدادهای درخشان", myread.CodeHazineh,comboBox1.SelectedValue,comboBox2.SelectedValue,myread.Price,myread.memo );
            }
        }
    
        private void button1_Click(object sender, EventArgs e)
        {
            FillDate();
            var query = (from u in connect.tblSannads where u.MonthID == Convert.ToInt32(comboBox1.SelectedValue) && u.YearID == Convert.ToInt32(comboBox2.SelectedValue) group u by new { u.tblEtebar.CodeHazineh, u.momo } into gr select new { CodeHazineh = gr.Key.CodeHazineh, Price = gr.Sum(q => q.Price), memo = gr.Key.momo }).Count();
            if (query == 0)
            {
                MessageBox.Show("اطلاعاتی جهت ارسال یافت نشد");
                dataGridView1.Rows.Clear();
            }
            else
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                app.Visible = false;
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;
                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {

                    worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;

                }

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Microsoft Office Excel |*.xlsx";
                sfd.Title = "انتخاب مسیر جهت خروجی سیستم مدیریت اسناد";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string filedestination = sfd.FileName;
                    workbook.SaveAs(filedestination, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("اطلاعات در مسیر : " + filedestination + " ذخیره شد");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("ذخیره با خطا مواجه شد");
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }
    }
}
