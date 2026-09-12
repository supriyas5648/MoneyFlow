using System;
using Npgsql;
using System.Windows.Forms;
using MoneyFlow.Service;
using MoneyFlow.Model;

namespace MoneyFlow
{
    public partial class FrmLogin : Form
    {

        public FrmLogin()
        {
            InitializeComponent();
        }

        public void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtUserName.Text) || string.IsNullOrEmpty(this.txtPassword.Text))
            {
                if (string.IsNullOrEmpty(this.txtUserName.Text))
                {
                    this.lblUserNameError.Visible = true;
                }

                if (string.IsNullOrEmpty(this.txtPassword.Text))
                {
                    this.lblPasswordError.Visible = true;
                }
            }
            else
            {

                //else check values

                //connection 
                NpgsqlConnection cn = new NpgsqlConnection(env.ConnectionString);
                try
                {
                    string query = @"SELECT c_user_id, c_user_username
                                     FROM t_user
                                     WHERE c_user_username = @username
                                     AND c_user_password = @password;";

                    NpgsqlCommand cmd = new NpgsqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("username", txtUserName.Text);
                    cmd.Parameters.AddWithValue("password", txtPassword.Text);

                    cn.Open();

                    using NpgsqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Get user information
                        User currentUser = new User
                        {
                            UserId = Convert.ToInt32(reader["c_user_id"]),
                            UserUsername = reader["c_user_username"].ToString()
                        };

                        // Pass logged-in user to FrmMain
                        FrmMain2 frmMain = new FrmMain2(currentUser);
                        frmMain.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message);
                }
                finally
                {
                    cn.Close();
                }
            }
        }


        public void btnRegisterClick(object sender, EventArgs e)
        {
            FrmRegistration registerForm = new FrmRegistration();
            registerForm.Show();
            this.Hide();
        }
    }
}