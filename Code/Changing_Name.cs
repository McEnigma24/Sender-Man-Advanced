using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sender_Man
{
    public partial class Changing_Name : Form
    {
        public string ReturnValue_name { get; set; }

        List<Sending_Profile> lists;
        public Changing_Name(List<Sending_Profile> _lists, int x, int y)
        {
            InitializeComponent();
            lists = _lists;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x + 300, y + 200);
        }

        private void saving_and_closing()
        {
            if (textBox_new_name.Text == "")
            {
                label1.Text = "enter text";
                return;
            }

            if (lists.Find(p => p.Sending_Profile_Name == textBox_new_name.Text) != null)
            {
                label1.Text = "profile name already in use";
                return;
            }

            this.ReturnValue_name = textBox_new_name.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button_save_changes_Click(object sender, EventArgs e)
        {
            saving_and_closing();
        }

        private void textBox_new_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                saving_and_closing();
            }
        }
    }
}
