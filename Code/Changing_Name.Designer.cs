
namespace Sender_Man
{
    partial class Changing_Name
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_new_name = new System.Windows.Forms.TextBox();
            this.button_save_changes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(32, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Change profile name to";
            // 
            // textBox_new_name
            // 
            this.textBox_new_name.Location = new System.Drawing.Point(35, 58);
            this.textBox_new_name.Name = "textBox_new_name";
            this.textBox_new_name.Size = new System.Drawing.Size(171, 20);
            this.textBox_new_name.TabIndex = 1;
            this.textBox_new_name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_new_name_KeyPress);
            // 
            // button_save_changes
            // 
            this.button_save_changes.Location = new System.Drawing.Point(72, 84);
            this.button_save_changes.Name = "button_save_changes";
            this.button_save_changes.Size = new System.Drawing.Size(96, 26);
            this.button_save_changes.TabIndex = 2;
            this.button_save_changes.Text = "Save Changes";
            this.button_save_changes.UseVisualStyleBackColor = true;
            this.button_save_changes.Click += new System.EventHandler(this.button_save_changes_Click);
            // 
            // Changing_Name
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 128);
            this.Controls.Add(this.button_save_changes);
            this.Controls.Add(this.textBox_new_name);
            this.Controls.Add(this.label1);
            this.Name = "Changing_Name";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_new_name;
        private System.Windows.Forms.Button button_save_changes;
    }
}