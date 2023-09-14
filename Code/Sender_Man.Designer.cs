namespace Wysylanie_na_maila
{
    partial class Sender_Man
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_wyslij = new System.Windows.Forms.Button();
            this.textBox_tytle = new System.Windows.Forms.TextBox();
            this.textBox_body = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_zip_name = new System.Windows.Forms.TextBox();
            this.button_load_next_file = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label_invalid_data = new System.Windows.Forms.Label();
            this.checkBox_date = new System.Windows.Forms.CheckBox();
            this.checkBox_hour = new System.Windows.Forms.CheckBox();
            this.button_save_to_config = new System.Windows.Forms.Button();
            this.checkBox_load_file_paths = new System.Windows.Forms.CheckBox();
            this.button_open_explorer = new System.Windows.Forms.Button();
            this.checkBox_zip = new System.Windows.Forms.CheckBox();
            this.comboBox_send_mail_to = new System.Windows.Forms.ComboBox();
            this.button_email_delete = new System.Windows.Forms.Button();
            this.button_email_add = new System.Windows.Forms.Button();
            this.checkBox_close_after_sending = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_to_txt_file = new System.Windows.Forms.TextBox();
            this.textBox_txt_converted_name = new System.Windows.Forms.TextBox();
            this.label_how_many_loaded_files = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox_directories = new System.Windows.Forms.ComboBox();
            this.comboBox_extensions_filter = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.button_dir_extensions_add = new System.Windows.Forms.Button();
            this.button_dir_extensions_delete = new System.Windows.Forms.Button();
            this.label_dir_filters_how_many = new System.Windows.Forms.Label();
            this.label_debug = new System.Windows.Forms.Label();
            this.button_clear_directories = new System.Windows.Forms.Button();
            this.button_from_add = new System.Windows.Forms.Button();
            this.button_from_delete = new System.Windows.Forms.Button();
            this.checkBox_whole_dir_to_zip = new System.Windows.Forms.CheckBox();
            this.comboBox_from = new System.Windows.Forms.ComboBox();
            this.comboBox_sending_profiles = new System.Windows.Forms.ComboBox();
            this.button_left_change_sending_profile = new System.Windows.Forms.Button();
            this.button_right_change_sending_profile = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.button_Add_New_sending_profile = new System.Windows.Forms.Button();
            this.button_delete_sending_profile = new System.Windows.Forms.Button();
            this.button_clear_sending_profiles = new System.Windows.Forms.Button();
            this.button_change_sending_profile_name = new System.Windows.Forms.Button();
            this.button_duplicate_sending_profile = new System.Windows.Forms.Button();
            this.comboBox_file_paths = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label_counter_sending_profiles = new System.Windows.Forms.Label();
            this.button_combobox_file_paths_clear = new System.Windows.Forms.Button();
            this.label_counter_dir = new System.Windows.Forms.Label();
            this.label_counter_file_paths = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label_debug_profile_check = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_wyslij
            // 
            this.button_wyslij.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_wyslij.Location = new System.Drawing.Point(328, 480);
            this.button_wyslij.Name = "button_wyslij";
            this.button_wyslij.Size = new System.Drawing.Size(315, 86);
            this.button_wyslij.TabIndex = 0;
            this.button_wyslij.Text = "Send";
            this.button_wyslij.UseVisualStyleBackColor = true;
            this.button_wyslij.Click += new System.EventHandler(this.button_wyslij_Click);
            // 
            // textBox_tytle
            // 
            this.textBox_tytle.Location = new System.Drawing.Point(328, 47);
            this.textBox_tytle.Name = "textBox_tytle";
            this.textBox_tytle.Size = new System.Drawing.Size(201, 20);
            this.textBox_tytle.TabIndex = 1;
            // 
            // textBox_body
            // 
            this.textBox_body.Location = new System.Drawing.Point(20, 105);
            this.textBox_body.MaxLength = 2147483647;
            this.textBox_body.Multiline = true;
            this.textBox_body.Name = "textBox_body";
            this.textBox_body.Size = new System.Drawing.Size(289, 149);
            this.textBox_body.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(324, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Title";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(16, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Body";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(457, 538);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "1 Mail sent";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(649, 530);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Zip name";
            // 
            // textBox_zip_name
            // 
            this.textBox_zip_name.Location = new System.Drawing.Point(652, 546);
            this.textBox_zip_name.Name = "textBox_zip_name";
            this.textBox_zip_name.Size = new System.Drawing.Size(116, 20);
            this.textBox_zip_name.TabIndex = 6;
            // 
            // button_load_next_file
            // 
            this.button_load_next_file.Location = new System.Drawing.Point(217, 444);
            this.button_load_next_file.Name = "button_load_next_file";
            this.button_load_next_file.Size = new System.Drawing.Size(92, 20);
            this.button_load_next_file.TabIndex = 8;
            this.button_load_next_file.Text = "Convert to .txt";
            this.button_load_next_file.UseVisualStyleBackColor = true;
            this.button_load_next_file.Click += new System.EventHandler(this.button_load_next_file_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(16, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 24);
            this.label6.TabIndex = 13;
            this.label6.Text = "To";
            // 
            // label_invalid_data
            // 
            this.label_invalid_data.AutoSize = true;
            this.label_invalid_data.Location = new System.Drawing.Point(416, 491);
            this.label_invalid_data.Name = "label_invalid_data";
            this.label_invalid_data.Size = new System.Drawing.Size(150, 13);
            this.label_invalid_data.TabIndex = 14;
            this.label_invalid_data.Text = "blank data or no files attached";
            // 
            // checkBox_date
            // 
            this.checkBox_date.AutoSize = true;
            this.checkBox_date.Location = new System.Drawing.Point(460, 20);
            this.checkBox_date.Name = "checkBox_date";
            this.checkBox_date.Size = new System.Drawing.Size(69, 17);
            this.checkBox_date.TabIndex = 17;
            this.checkBox_date.Text = "Add date";
            this.checkBox_date.UseVisualStyleBackColor = true;
            // 
            // checkBox_hour
            // 
            this.checkBox_hour.AutoSize = true;
            this.checkBox_hour.Location = new System.Drawing.Point(385, 20);
            this.checkBox_hour.Name = "checkBox_hour";
            this.checkBox_hour.Size = new System.Drawing.Size(69, 17);
            this.checkBox_hour.TabIndex = 18;
            this.checkBox_hour.Text = "Add hour";
            this.checkBox_hour.UseVisualStyleBackColor = true;
            // 
            // button_save_to_config
            // 
            this.button_save_to_config.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.button_save_to_config.Location = new System.Drawing.Point(20, 480);
            this.button_save_to_config.Name = "button_save_to_config";
            this.button_save_to_config.Size = new System.Drawing.Size(289, 86);
            this.button_save_to_config.TabIndex = 19;
            this.button_save_to_config.Text = "Save Preferences";
            this.button_save_to_config.UseVisualStyleBackColor = true;
            this.button_save_to_config.Click += new System.EventHandler(this.button_save_to_config_Click);
            // 
            // checkBox_load_file_paths
            // 
            this.checkBox_load_file_paths.AutoSize = true;
            this.checkBox_load_file_paths.Location = new System.Drawing.Point(390, 457);
            this.checkBox_load_file_paths.Name = "checkBox_load_file_paths";
            this.checkBox_load_file_paths.Size = new System.Drawing.Size(210, 17);
            this.checkBox_load_file_paths.TabIndex = 22;
            this.checkBox_load_file_paths.Text = "Load files and directories while sending";
            this.checkBox_load_file_paths.UseVisualStyleBackColor = true;
            // 
            // button_open_explorer
            // 
            this.button_open_explorer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button_open_explorer.Location = new System.Drawing.Point(647, 364);
            this.button_open_explorer.Name = "button_open_explorer";
            this.button_open_explorer.Size = new System.Drawing.Size(121, 21);
            this.button_open_explorer.TabIndex = 24;
            this.button_open_explorer.Text = "Open file explorer";
            this.button_open_explorer.UseVisualStyleBackColor = true;
            this.button_open_explorer.Click += new System.EventHandler(this.button_open_explorer_Click);
            // 
            // checkBox_zip
            // 
            this.checkBox_zip.AutoSize = true;
            this.checkBox_zip.Location = new System.Drawing.Point(652, 499);
            this.checkBox_zip.Name = "checkBox_zip";
            this.checkBox_zip.Size = new System.Drawing.Size(78, 17);
            this.checkBox_zip.TabIndex = 26;
            this.checkBox_zip.Text = "pack to zip";
            this.checkBox_zip.UseVisualStyleBackColor = true;
            // 
            // comboBox_send_mail_to
            // 
            this.comboBox_send_mail_to.FormattingEnabled = true;
            this.comboBox_send_mail_to.Location = new System.Drawing.Point(20, 46);
            this.comboBox_send_mail_to.Name = "comboBox_send_mail_to";
            this.comboBox_send_mail_to.Size = new System.Drawing.Size(289, 21);
            this.comboBox_send_mail_to.TabIndex = 27;
            this.comboBox_send_mail_to.DropDown += new System.EventHandler(this.comboBox_send_mail_to_DropDownClosed);
            this.comboBox_send_mail_to.DropDownClosed += new System.EventHandler(this.comboBox_send_mail_to_DropDownClosed);
            // 
            // button_email_delete
            // 
            this.button_email_delete.Location = new System.Drawing.Point(259, 18);
            this.button_email_delete.Name = "button_email_delete";
            this.button_email_delete.Size = new System.Drawing.Size(50, 22);
            this.button_email_delete.TabIndex = 28;
            this.button_email_delete.Text = "Delete";
            this.button_email_delete.UseVisualStyleBackColor = true;
            this.button_email_delete.Click += new System.EventHandler(this.button_check_Click);
            // 
            // button_email_add
            // 
            this.button_email_add.Location = new System.Drawing.Point(203, 18);
            this.button_email_add.Name = "button_email_add";
            this.button_email_add.Size = new System.Drawing.Size(50, 22);
            this.button_email_add.TabIndex = 29;
            this.button_email_add.Text = "Add";
            this.button_email_add.UseVisualStyleBackColor = true;
            this.button_email_add.Click += new System.EventHandler(this.button_email_add_Click);
            // 
            // checkBox_close_after_sending
            // 
            this.checkBox_close_after_sending.AutoSize = true;
            this.checkBox_close_after_sending.Location = new System.Drawing.Point(652, 480);
            this.checkBox_close_after_sending.Name = "checkBox_close_after_sending";
            this.checkBox_close_after_sending.Size = new System.Drawing.Size(116, 17);
            this.checkBox_close_after_sending.TabIndex = 31;
            this.checkBox_close_after_sending.Text = "Close after sending";
            this.checkBox_close_after_sending.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(541, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 24);
            this.label8.TabIndex = 33;
            this.label8.Text = "From";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(16, 263);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 24);
            this.label9.TabIndex = 34;
            this.label9.Text = "To .txt";
            // 
            // textBox_to_txt_file
            // 
            this.textBox_to_txt_file.Location = new System.Drawing.Point(20, 290);
            this.textBox_to_txt_file.MaxLength = 2147483647;
            this.textBox_to_txt_file.Multiline = true;
            this.textBox_to_txt_file.Name = "textBox_to_txt_file";
            this.textBox_to_txt_file.Size = new System.Drawing.Size(289, 149);
            this.textBox_to_txt_file.TabIndex = 35;
            // 
            // textBox_txt_converted_name
            // 
            this.textBox_txt_converted_name.Location = new System.Drawing.Point(103, 444);
            this.textBox_txt_converted_name.Name = "textBox_txt_converted_name";
            this.textBox_txt_converted_name.Size = new System.Drawing.Size(88, 20);
            this.textBox_txt_converted_name.TabIndex = 36;
            // 
            // label_how_many_loaded_files
            // 
            this.label_how_many_loaded_files.AutoSize = true;
            this.label_how_many_loaded_files.BackColor = System.Drawing.Color.Transparent;
            this.label_how_many_loaded_files.Location = new System.Drawing.Point(124, 270);
            this.label_how_many_loaded_files.Name = "label_how_many_loaded_files";
            this.label_how_many_loaded_files.Size = new System.Drawing.Size(81, 13);
            this.label_how_many_loaded_files.TabIndex = 40;
            this.label_how_many_loaded_files.Text = "1 Converted file";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(17, 447);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "Txt file name";
            // 
            // comboBox_directories
            // 
            this.comboBox_directories.AllowDrop = true;
            this.comboBox_directories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_directories.FormattingEnabled = true;
            this.comboBox_directories.Location = new System.Drawing.Point(328, 252);
            this.comboBox_directories.Name = "comboBox_directories";
            this.comboBox_directories.Size = new System.Drawing.Size(295, 21);
            this.comboBox_directories.TabIndex = 42;
            this.comboBox_directories.DropDown += new System.EventHandler(this.comboBox_directories_DropDownClosed);
            this.comboBox_directories.SelectedIndexChanged += new System.EventHandler(this.comboBox_directories_SelectedIndexChanged);
            this.comboBox_directories.DropDownClosed += new System.EventHandler(this.comboBox_directories_DropDownClosed);
            this.comboBox_directories.DragDrop += new System.Windows.Forms.DragEventHandler(this.comboBox_directories_DragDrop);
            this.comboBox_directories.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnter);
            // 
            // comboBox_extensions_filter
            // 
            this.comboBox_extensions_filter.FormattingEnabled = true;
            this.comboBox_extensions_filter.Location = new System.Drawing.Point(647, 251);
            this.comboBox_extensions_filter.Name = "comboBox_extensions_filter";
            this.comboBox_extensions_filter.Size = new System.Drawing.Size(121, 21);
            this.comboBox_extensions_filter.TabIndex = 43;
            this.comboBox_extensions_filter.DropDown += new System.EventHandler(this.comboBox_extensions_filter_DropDownClosed);
            this.comboBox_extensions_filter.SelectedIndexChanged += new System.EventHandler(this.comboBox_extensions_filter_SelectedIndexChanged);
            this.comboBox_extensions_filter.DropDownClosed += new System.EventHandler(this.comboBox_extensions_filter_DropDownClosed);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.Location = new System.Drawing.Point(324, 225);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 24);
            this.label10.TabIndex = 44;
            this.label10.Text = "Directories";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(644, 229);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(124, 17);
            this.label11.TabIndex = 45;
            this.label11.Text = "Extension filters";
            // 
            // button_dir_extensions_add
            // 
            this.button_dir_extensions_add.Location = new System.Drawing.Point(647, 275);
            this.button_dir_extensions_add.Name = "button_dir_extensions_add";
            this.button_dir_extensions_add.Size = new System.Drawing.Size(58, 21);
            this.button_dir_extensions_add.TabIndex = 47;
            this.button_dir_extensions_add.Text = "Add";
            this.button_dir_extensions_add.UseVisualStyleBackColor = true;
            this.button_dir_extensions_add.Click += new System.EventHandler(this.button_dir_extensions_add_Click);
            // 
            // button_dir_extensions_delete
            // 
            this.button_dir_extensions_delete.Location = new System.Drawing.Point(711, 276);
            this.button_dir_extensions_delete.Name = "button_dir_extensions_delete";
            this.button_dir_extensions_delete.Size = new System.Drawing.Size(57, 21);
            this.button_dir_extensions_delete.TabIndex = 46;
            this.button_dir_extensions_delete.Text = "Delete";
            this.button_dir_extensions_delete.UseVisualStyleBackColor = true;
            this.button_dir_extensions_delete.Click += new System.EventHandler(this.button_dir_extensions_delete_Click);
            // 
            // label_dir_filters_how_many
            // 
            this.label_dir_filters_how_many.AutoSize = true;
            this.label_dir_filters_how_many.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label_dir_filters_how_many.Location = new System.Drawing.Point(457, 278);
            this.label_dir_filters_how_many.Name = "label_dir_filters_how_many";
            this.label_dir_filters_how_many.Size = new System.Drawing.Size(96, 15);
            this.label_dir_filters_how_many.TabIndex = 49;
            this.label_dir_filters_how_many.Text = "0 active filters";
            // 
            // label_debug
            // 
            this.label_debug.AutoSize = true;
            this.label_debug.Location = new System.Drawing.Point(904, 55);
            this.label_debug.Name = "label_debug";
            this.label_debug.Size = new System.Drawing.Size(41, 13);
            this.label_debug.TabIndex = 50;
            this.label_debug.Text = "label12";
            // 
            // button_clear_directories
            // 
            this.button_clear_directories.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button_clear_directories.Location = new System.Drawing.Point(328, 275);
            this.button_clear_directories.Name = "button_clear_directories";
            this.button_clear_directories.Size = new System.Drawing.Size(44, 21);
            this.button_clear_directories.TabIndex = 52;
            this.button_clear_directories.Text = "Clear";
            this.button_clear_directories.UseVisualStyleBackColor = true;
            this.button_clear_directories.Click += new System.EventHandler(this.button_clear_directories_Click);
            // 
            // button_from_add
            // 
            this.button_from_add.Location = new System.Drawing.Point(662, 20);
            this.button_from_add.Name = "button_from_add";
            this.button_from_add.Size = new System.Drawing.Size(50, 22);
            this.button_from_add.TabIndex = 54;
            this.button_from_add.Text = "Add";
            this.button_from_add.UseVisualStyleBackColor = true;
            this.button_from_add.Click += new System.EventHandler(this.button_from_add_Click);
            // 
            // button_from_delete
            // 
            this.button_from_delete.Location = new System.Drawing.Point(718, 20);
            this.button_from_delete.Name = "button_from_delete";
            this.button_from_delete.Size = new System.Drawing.Size(50, 22);
            this.button_from_delete.TabIndex = 53;
            this.button_from_delete.Text = "Delete";
            this.button_from_delete.UseVisualStyleBackColor = true;
            this.button_from_delete.Click += new System.EventHandler(this.button_from_delete_Click);
            // 
            // checkBox_whole_dir_to_zip
            // 
            this.checkBox_whole_dir_to_zip.AutoSize = true;
            this.checkBox_whole_dir_to_zip.Location = new System.Drawing.Point(647, 302);
            this.checkBox_whole_dir_to_zip.Name = "checkBox_whole_dir_to_zip";
            this.checkBox_whole_dir_to_zip.Size = new System.Drawing.Size(123, 17);
            this.checkBox_whole_dir_to_zip.TabIndex = 56;
            this.checkBox_whole_dir_to_zip.Text = "pack whole dir to zip";
            this.checkBox_whole_dir_to_zip.UseVisualStyleBackColor = true;
            this.checkBox_whole_dir_to_zip.CheckedChanged += new System.EventHandler(this.checkBox_whole_dir_to_zip_CheckedChanged);
            // 
            // comboBox_from
            // 
            this.comboBox_from.FormattingEnabled = true;
            this.comboBox_from.Location = new System.Drawing.Point(545, 47);
            this.comboBox_from.Name = "comboBox_from";
            this.comboBox_from.Size = new System.Drawing.Size(223, 21);
            this.comboBox_from.TabIndex = 57;
            this.comboBox_from.DropDown += new System.EventHandler(this.comboBox_from_DropDown);
            this.comboBox_from.DropDownClosed += new System.EventHandler(this.comboBox_from_DropDown);
            // 
            // comboBox_sending_profiles
            // 
            this.comboBox_sending_profiles.AllowDrop = true;
            this.comboBox_sending_profiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_sending_profiles.FormattingEnabled = true;
            this.comboBox_sending_profiles.Location = new System.Drawing.Point(366, 127);
            this.comboBox_sending_profiles.Name = "comboBox_sending_profiles";
            this.comboBox_sending_profiles.Size = new System.Drawing.Size(158, 21);
            this.comboBox_sending_profiles.TabIndex = 58;
            this.comboBox_sending_profiles.DropDown += new System.EventHandler(this.comboBox_sending_profiles_Combohandler);
            this.comboBox_sending_profiles.SelectedIndexChanged += new System.EventHandler(this.comboBox_sending_profiles_SelectedIndexChanged);
            this.comboBox_sending_profiles.DropDownClosed += new System.EventHandler(this.comboBox_sending_profiles_Combohandler);
            // 
            // button_left_change_sending_profile
            // 
            this.button_left_change_sending_profile.Location = new System.Drawing.Point(328, 126);
            this.button_left_change_sending_profile.Name = "button_left_change_sending_profile";
            this.button_left_change_sending_profile.Size = new System.Drawing.Size(32, 21);
            this.button_left_change_sending_profile.TabIndex = 59;
            this.button_left_change_sending_profile.Text = "<";
            this.button_left_change_sending_profile.UseVisualStyleBackColor = true;
            this.button_left_change_sending_profile.Click += new System.EventHandler(this.button_left_change_sending_profile_Click);
            // 
            // button_right_change_sending_profile
            // 
            this.button_right_change_sending_profile.Location = new System.Drawing.Point(530, 127);
            this.button_right_change_sending_profile.Name = "button_right_change_sending_profile";
            this.button_right_change_sending_profile.Size = new System.Drawing.Size(32, 21);
            this.button_right_change_sending_profile.TabIndex = 60;
            this.button_right_change_sending_profile.Text = ">";
            this.button_right_change_sending_profile.UseVisualStyleBackColor = true;
            this.button_right_change_sending_profile.Click += new System.EventHandler(this.button_right_change_sending_profile_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label12.Location = new System.Drawing.Point(324, 99);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(153, 24);
            this.label12.TabIndex = 61;
            this.label12.Text = "Sending Profile";
            // 
            // button_Add_New_sending_profile
            // 
            this.button_Add_New_sending_profile.Location = new System.Drawing.Point(583, 105);
            this.button_Add_New_sending_profile.Name = "button_Add_New_sending_profile";
            this.button_Add_New_sending_profile.Size = new System.Drawing.Size(84, 25);
            this.button_Add_New_sending_profile.TabIndex = 62;
            this.button_Add_New_sending_profile.Text = "Add new";
            this.button_Add_New_sending_profile.UseVisualStyleBackColor = true;
            this.button_Add_New_sending_profile.Click += new System.EventHandler(this.button_Add_New_sending_profile_Click);
            // 
            // button_delete_sending_profile
            // 
            this.button_delete_sending_profile.Location = new System.Drawing.Point(583, 170);
            this.button_delete_sending_profile.Name = "button_delete_sending_profile";
            this.button_delete_sending_profile.Size = new System.Drawing.Size(84, 25);
            this.button_delete_sending_profile.TabIndex = 63;
            this.button_delete_sending_profile.Text = "Delete";
            this.button_delete_sending_profile.UseVisualStyleBackColor = true;
            this.button_delete_sending_profile.Click += new System.EventHandler(this.button_delete_sending_profile_Click);
            // 
            // button_clear_sending_profiles
            // 
            this.button_clear_sending_profiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button_clear_sending_profiles.Location = new System.Drawing.Point(673, 170);
            this.button_clear_sending_profiles.Name = "button_clear_sending_profiles";
            this.button_clear_sending_profiles.Size = new System.Drawing.Size(95, 25);
            this.button_clear_sending_profiles.TabIndex = 64;
            this.button_clear_sending_profiles.Text = "Clear";
            this.button_clear_sending_profiles.UseVisualStyleBackColor = true;
            this.button_clear_sending_profiles.Click += new System.EventHandler(this.button_clear_sending_profiles_Click);
            // 
            // button_change_sending_profile_name
            // 
            this.button_change_sending_profile_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button_change_sending_profile_name.Location = new System.Drawing.Point(673, 105);
            this.button_change_sending_profile_name.Name = "button_change_sending_profile_name";
            this.button_change_sending_profile_name.Size = new System.Drawing.Size(95, 59);
            this.button_change_sending_profile_name.TabIndex = 65;
            this.button_change_sending_profile_name.Text = "Change name";
            this.button_change_sending_profile_name.UseVisualStyleBackColor = true;
            this.button_change_sending_profile_name.Click += new System.EventHandler(this.button_change_sending_profile_name_Click);
            // 
            // button_duplicate_sending_profile
            // 
            this.button_duplicate_sending_profile.Location = new System.Drawing.Point(583, 136);
            this.button_duplicate_sending_profile.Name = "button_duplicate_sending_profile";
            this.button_duplicate_sending_profile.Size = new System.Drawing.Size(84, 28);
            this.button_duplicate_sending_profile.TabIndex = 66;
            this.button_duplicate_sending_profile.Text = "Duplicate";
            this.button_duplicate_sending_profile.UseVisualStyleBackColor = true;
            this.button_duplicate_sending_profile.Click += new System.EventHandler(this.button_duplicate_sending_profile_Click);
            // 
            // comboBox_file_paths
            // 
            this.comboBox_file_paths.AllowDrop = true;
            this.comboBox_file_paths.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_file_paths.FormattingEnabled = true;
            this.comboBox_file_paths.Location = new System.Drawing.Point(328, 364);
            this.comboBox_file_paths.Name = "comboBox_file_paths";
            this.comboBox_file_paths.Size = new System.Drawing.Size(295, 21);
            this.comboBox_file_paths.TabIndex = 67;
            this.comboBox_file_paths.DragDrop += new System.Windows.Forms.DragEventHandler(this.comboBox_file_paths_DragDrop);
            this.comboBox_file_paths.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnter);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label13.Location = new System.Drawing.Point(328, 337);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 24);
            this.label13.TabIndex = 68;
            this.label13.Text = "File paths";
            // 
            // label_counter_sending_profiles
            // 
            this.label_counter_sending_profiles.AutoSize = true;
            this.label_counter_sending_profiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label_counter_sending_profiles.Location = new System.Drawing.Point(383, 152);
            this.label_counter_sending_profiles.Name = "label_counter_sending_profiles";
            this.label_counter_sending_profiles.Size = new System.Drawing.Size(122, 15);
            this.label_counter_sending_profiles.TabIndex = 79;
            this.label_counter_sending_profiles.Text = "0 sending profiles";
            // 
            // button_combobox_file_paths_clear
            // 
            this.button_combobox_file_paths_clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button_combobox_file_paths_clear.Location = new System.Drawing.Point(328, 387);
            this.button_combobox_file_paths_clear.Name = "button_combobox_file_paths_clear";
            this.button_combobox_file_paths_clear.Size = new System.Drawing.Size(44, 21);
            this.button_combobox_file_paths_clear.TabIndex = 81;
            this.button_combobox_file_paths_clear.Text = "Clear";
            this.button_combobox_file_paths_clear.UseVisualStyleBackColor = true;
            this.button_combobox_file_paths_clear.Click += new System.EventHandler(this.button_combobox_file_paths_clear_Click);
            // 
            // label_counter_dir
            // 
            this.label_counter_dir.AutoSize = true;
            this.label_counter_dir.Location = new System.Drawing.Point(441, 231);
            this.label_counter_dir.Name = "label_counter_dir";
            this.label_counter_dir.Size = new System.Drawing.Size(57, 13);
            this.label_counter_dir.TabIndex = 87;
            this.label_counter_dir.Text = "dir counter";
            // 
            // label_counter_file_paths
            // 
            this.label_counter_file_paths.AutoSize = true;
            this.label_counter_file_paths.Location = new System.Drawing.Point(435, 343);
            this.label_counter_file_paths.Name = "label_counter_file_paths";
            this.label_counter_file_paths.Size = new System.Drawing.Size(72, 13);
            this.label_counter_file_paths.TabIndex = 88;
            this.label_counter_file_paths.Text = "paths counter";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button1.Location = new System.Drawing.Point(907, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 25);
            this.button1.TabIndex = 89;
            this.button1.Text = "clear debug";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_debug_profile_check
            // 
            this.label_debug_profile_check.AutoSize = true;
            this.label_debug_profile_check.Location = new System.Drawing.Point(904, 105);
            this.label_debug_profile_check.Name = "label_debug_profile_check";
            this.label_debug_profile_check.Size = new System.Drawing.Size(41, 13);
            this.label_debug_profile_check.TabIndex = 90;
            this.label_debug_profile_check.Text = "label12";
            // 
            // Sender_Man
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 590);
            this.Controls.Add(this.label_debug_profile_check);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label_counter_file_paths);
            this.Controls.Add(this.label_counter_dir);
            this.Controls.Add(this.button_combobox_file_paths_clear);
            this.Controls.Add(this.label_counter_sending_profiles);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.comboBox_file_paths);
            this.Controls.Add(this.button_duplicate_sending_profile);
            this.Controls.Add(this.button_change_sending_profile_name);
            this.Controls.Add(this.button_clear_sending_profiles);
            this.Controls.Add(this.button_delete_sending_profile);
            this.Controls.Add(this.button_Add_New_sending_profile);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.button_right_change_sending_profile);
            this.Controls.Add(this.button_left_change_sending_profile);
            this.Controls.Add(this.comboBox_sending_profiles);
            this.Controls.Add(this.comboBox_from);
            this.Controls.Add(this.checkBox_whole_dir_to_zip);
            this.Controls.Add(this.button_from_add);
            this.Controls.Add(this.button_from_delete);
            this.Controls.Add(this.button_clear_directories);
            this.Controls.Add(this.label_debug);
            this.Controls.Add(this.label_dir_filters_how_many);
            this.Controls.Add(this.button_dir_extensions_add);
            this.Controls.Add(this.button_dir_extensions_delete);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.comboBox_extensions_filter);
            this.Controls.Add(this.comboBox_directories);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label_how_many_loaded_files);
            this.Controls.Add(this.textBox_txt_converted_name);
            this.Controls.Add(this.textBox_to_txt_file);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.checkBox_close_after_sending);
            this.Controls.Add(this.button_email_add);
            this.Controls.Add(this.button_email_delete);
            this.Controls.Add(this.comboBox_send_mail_to);
            this.Controls.Add(this.checkBox_zip);
            this.Controls.Add(this.button_open_explorer);
            this.Controls.Add(this.checkBox_load_file_paths);
            this.Controls.Add(this.button_save_to_config);
            this.Controls.Add(this.checkBox_hour);
            this.Controls.Add(this.checkBox_date);
            this.Controls.Add(this.label_invalid_data);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button_load_next_file);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_zip_name);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_body);
            this.Controls.Add(this.textBox_tytle);
            this.Controls.Add(this.button_wyslij);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Name = "Sender_Man";
            this.Text = "Sender Man";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.App_FormClosing);
            this.Load += new System.EventHandler(this.App_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_wyslij;
        private System.Windows.Forms.TextBox textBox_tytle;
        private System.Windows.Forms.TextBox textBox_body;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_zip_name;
        private System.Windows.Forms.Button button_load_next_file;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_invalid_data;
        private System.Windows.Forms.CheckBox checkBox_date;
        private System.Windows.Forms.CheckBox checkBox_hour;
        private System.Windows.Forms.Button button_save_to_config;
        private System.Windows.Forms.CheckBox checkBox_load_file_paths;
        private System.Windows.Forms.Button button_open_explorer;
        private System.Windows.Forms.CheckBox checkBox_zip;
        private System.Windows.Forms.ComboBox comboBox_send_mail_to;
        private System.Windows.Forms.Button button_email_delete;
        private System.Windows.Forms.Button button_email_add;
        private System.Windows.Forms.CheckBox checkBox_close_after_sending;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_to_txt_file;
        private System.Windows.Forms.TextBox textBox_txt_converted_name;
        private System.Windows.Forms.Label label_how_many_loaded_files;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox_directories;
        private System.Windows.Forms.ComboBox comboBox_extensions_filter;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button_dir_extensions_add;
        private System.Windows.Forms.Button button_dir_extensions_delete;
        private System.Windows.Forms.Label label_dir_filters_how_many;
        private System.Windows.Forms.Label label_debug;
        private System.Windows.Forms.Button button_clear_directories;
        private System.Windows.Forms.Button button_from_add;
        private System.Windows.Forms.Button button_from_delete;
        private System.Windows.Forms.CheckBox checkBox_whole_dir_to_zip;
        private System.Windows.Forms.ComboBox comboBox_from;
        private System.Windows.Forms.ComboBox comboBox_sending_profiles;
        private System.Windows.Forms.Button button_left_change_sending_profile;
        private System.Windows.Forms.Button button_right_change_sending_profile;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button_Add_New_sending_profile;
        private System.Windows.Forms.Button button_delete_sending_profile;
        private System.Windows.Forms.Button button_clear_sending_profiles;
        private System.Windows.Forms.Button button_change_sending_profile_name;
        private System.Windows.Forms.Button button_duplicate_sending_profile;
        private System.Windows.Forms.ComboBox comboBox_file_paths;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label_counter_sending_profiles;
        private System.Windows.Forms.Button button_combobox_file_paths_clear;
        private System.Windows.Forms.Label label_counter_dir;
        private System.Windows.Forms.Label label_counter_file_paths;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_debug_profile_check;
    }
}

