using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp13
{
    public partial class Reg : Form
    {
        public Reg()
        {
            InitializeComponent();
        }

   

        private void BtnSignUp_Click(object sender, EventArgs e)
        {

            try
            {
                AddUser();
                
            }
            catch (Exception)
            {
               
                MessageBox.Show("Registration error. Please, try again!", "", MessageBoxButtons.OK);
            }
          

        }
        async void AddUser()
        {
            await Task.Run(() => {
                using (UserContext db = new UserContext())
                {
                 
                    db.Users.Add(
                    new User 
                    { 
                        Login = TbLogin.Text,
                        Password  = TbPassword.Text,
                        Email = TbEmail.Text
                    });
                    db.SaveChanges();

                    MessageBox.Show("User successfully added!", "", MessageBoxButtons.OK);
                }
            });
        }

        private void CbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (CbShowPassword.Checked)
            {
                TbPassword.PasswordChar = '*';
            }
            else
            {
                TbPassword.PasswordChar = '\0';
            }
        }
    }
}
