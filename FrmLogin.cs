using System;
using Npgsql;
using System.Windows.Forms;

namespace MoneyFlow
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        public void LoginClick(object sender, EventArgs e)
        {
           if(string.IsNullOrEmpty(this.txtUserName.Text) || string.IsNullOrEmpty(this.txtPassword.Text)) 
           {
                if (string.IsNullOrEmpty(this.txtUserName.Text))
                {
                    this.lblUserNameError.Visible = true;
                }

                if (string.IsNullOrEmpty(this.txtPassword.Text))
                {
                    this.lblPasswordError.Visible = true;
                }
           }else
           {

            //else check values
            //accessed con string
            env obj  = new env();
            string ConnectionString = obj.ConnectionString;
            
             //connection 
             NpgsqlConnection cn = new NpgsqlConnection(ConnectionString);
            try
            {
                string query = @"SELECT COUNT(*) FROM t_user WHERE c_user_username = @username AND c_user_password = @password";

                NpgsqlCommand cmd = new NpgsqlCommand(query,cn);

                cmd.Parameters.AddWithValue("username", txtUserName.Text);
                cmd.Parameters.AddWithValue("password", txtPassword.Text);

                cn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Login Successful");
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }

            }catch(Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }finally
            {
                cn.Close();
            }
        }
        }


    public void RegisterClick(object sender, EventArgs e)
        {
            FrmRegistration registerForm = new FrmRegistration();
            registerForm.Show(); 
            //  this.Hide();
        }
}
}