using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using AmvalSystem.Forms;

namespace AmvalSystem
{
    public partial class frmLogin : Form
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        public frmLogin()
        {
            InitializeComponent();
            lbltime.Text = DateTime.Now.ToString("HH:mm:ss tt");
            timer.Tick += new EventHandler(timer_tick);
            timer.Interval = 800;
            timer.Start();
        }
        private void timer_tick(object sender, EventArgs e)
        {
            lbltime.Text = DateTime.Now.ToString("HH:mm:ss tt");
        }
        ConnectionDataContext connect = new ConnectionDataContext();
        private void frmLogin_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
            lblDate.Text = pc.GetYear(DateTime.Now) + "/" + pc.GetMonth(DateTime.Now) + "/" + pc.GetDayOfMonth(DateTime.Now);
            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            lblversion.Text = version;
            txtuser.Focus();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {

        }

        private void radButton2_Click(object sender, EventArgs e)
        {
          
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (txtuser.Text == string.Empty)
            {
                errorProvider1.SetError(txtuser, "لطفا نام  کاربری را وارد کنید");
                txtuser.Focus();
                return;
            }
            else if (txtpass.Text == string.Empty)
            {
                errorProvider1.SetError(txtpass, "لطفا رمز عبور را وارد کنید");
                txtpass.Focus();
                return;
            }
            else
            {
                errorProvider1.Clear();

                var query = from u in connect.tblUsers select u;
                foreach (var myread in query)
                {
                    if (myread.Username == txtuser.Text & myread.Password == txtpass.Text)
                    {
                        this.WindowState = FormWindowState.Minimized;
                        this.ShowInTaskbar = false;
                        //MessageBox.Show("شما با موفقیت وارد شدید", "نتیجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        (new frmMain()).ShowDialog();
                    }
                    else
                    {
                        BehComponents.MessageBoxFarsi.Show("نام کاربری و یا رمز عبور اشتباه است آن را اصلاح کنید", "خطا", BehComponents.MessageBoxFarsiButtons.OK, BehComponents.MessageBoxFarsiIcon.Error);
                        //MessageBox.Show("نام کاربری و رمز عبور اشتباه است لطفا آن را اصلاح کنید");
                    }
                }
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}