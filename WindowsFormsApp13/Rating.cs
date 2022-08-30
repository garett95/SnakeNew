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
    public partial class Rating : Form
    {
        public Rating()
        {
            InitializeComponent();
        }

        private void Rating_Load(object sender, EventArgs e)
        {
            using (UserContext db = new UserContext())
            {
                db.Users.OrderByDescending(r=>r.Record).Load();
                dataGridView.DataSource = db.Users.Local.ToBindingList();
            }
        }
    }
}
