using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit;
using System.Threading;
using System.IO.Compression;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Xml.Serialization;
using Sender_Man;
using System.Diagnostics;

namespace Wysylanie_na_maila
{
    public partial class Sender_Man : Form
    {
        XmlSerializer serializer = new XmlSerializer(typeof(One_Big_Serializer));
        One_Big_Serializer BIG_SER; // trzyma też informację, który ostatnio był używany
        List<Sending_Profile> List_Sending_Profile;     // wiele profili        
        string combobox_before_change;

        int counter;
        int file_counter;
        List<string> List_txt_files;                            // .add przy każdym "Convert to txt"                                        .delete w clean_up()
        List<string> List_file_paths;                           // .add dopiero w momencie gdzie sprawdzane są ścieżki przed wysłaniem      .delete tuż przed dodawaniem i w clean_up()
        List<Directory_filters> List_directory_with_filters;    // 


        // DIRECTORIES
        const string dir_path_app_files = @"Application_Files";
        const string dir_path_config = dir_path_app_files + @"\Config";
        const string dir_path_sending_folder = dir_path_app_files + @"\sending_folder";
        const string dir_path_for_converted_to_txt = dir_path_app_files + @"\converted_to_txt";
        const string dir_path_tmp_zip = dir_path_app_files + @"\tmp_for_zip";
        const string dir_path_serialized_profiles = dir_path_app_files + @"\serialized";
        
        // bez dir path do profiles -> bo będzie tylko jeden obiekt z zaserializowaną listą profili, bez wielu plików na każdy profil

        // zrobić osobny folder serializer_properties ----> a kiedy wszystko będzie działać z tym nowym to usunąc stary Config/ -Properties.txt- -File paths.txt- -Emails_List-
        //                                                                                                       zostawić tylko -Email_Send_Authorization.txt- -Authorization_Check-

        // FILES
        const string path_serialized_profiles = dir_path_app_files + @"\serialized\profiles.txt";
        const string path_authorization = dir_path_app_files + @"\Config\Email_Send_Authorization.txt";
        const string path_authorization_check = dir_path_app_files + @"\Config\Authorization_Check.txt";


        List<Config_info> config;        
        List<Config_info> authorization;
        List<Config_info> authorization_check;

        string authorization_gmail;
        string authorization_password;
        bool authorization_pass;
        bool app_startup;
        bool locked_combohandler;
        bool locked_selected_index_changed;
        string mouse_change_sending_profile;
        int label_x, label_y;
        int label3_change_configuration_x, label3_change_configuration_y;

        //                     24 900 000
        int EMAIL_SIZE_LIMIT = 24900000;

        bool debug = false;



        private void change_focus()
        {
            label1.Focus();
        }

        public void Debug_add(string line)
        {
            if(debug) label_debug.Text += line + "\r";
        }
        public void Debug_reset(string line)
        {
            if (debug) label_debug.Text = line + "\r";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            label_debug.Text = "";
        }
        private void Debug_profile_check(Sending_Profile prof)
        {
            if (debug)
            {
                //label_debug_profile_check.Text = "";

                label_debug_profile_check.Text += prof.Sending_Profile_Name + "\r";


                label_debug_profile_check.Text += "prof.Ser_List_of_Dirs -> " + prof.Ser_List_of_Dirs.Count + "\r";
                label_debug_profile_check.Text += "prof.Ser_List_of_File_Paths -> " + prof.Ser_List_of_File_Paths.Count + "\r";
            }
        }
        private void Debug_with_file(in string name)
        {
            if (debug)
            {
                using (StreamWriter sw = File.CreateText(name + ".txt"))
                {
                    sw.WriteLine("random:");
                    sw.Write("shit:");
                }
            }
        }



        public void Resetting_app_directories()
        {
            if (Directory.Exists(dir_path_tmp_zip)) Directory.Delete(dir_path_tmp_zip, true); Directory.CreateDirectory(dir_path_tmp_zip);
            if (Directory.Exists(dir_path_for_converted_to_txt)) Directory.Delete(dir_path_for_converted_to_txt, true); Directory.CreateDirectory(dir_path_for_converted_to_txt);
            if (Directory.Exists(dir_path_sending_folder)) Directory.Delete(dir_path_sending_folder, true); Directory.CreateDirectory(dir_path_sending_folder);
        }
        public void Creating_app_directories_if_not_existant()
        {
            if (!Directory.Exists(dir_path_app_files))
            {
                Directory.CreateDirectory(dir_path_app_files);
                MessageBox.Show("[Sender Man.exe] saves everything to files in (Application_Files)\rAlways have the [exe] and (directory) in the same folder\r\r" +
                    " you can always hide them somewhere and create a shortcut to the [exe]", "App needs a place to store data");
            }

            if (!Directory.Exists(dir_path_config)) Directory.CreateDirectory(dir_path_config);
            if (!Directory.Exists(dir_path_serialized_profiles)) Directory.CreateDirectory(dir_path_serialized_profiles);
            if (!File.Exists(path_serialized_profiles)) File.Create(path_serialized_profiles);

            if (!Directory.Exists(dir_path_tmp_zip)) Directory.CreateDirectory(dir_path_tmp_zip);
            if (!Directory.Exists(dir_path_for_converted_to_txt)) Directory.CreateDirectory(dir_path_for_converted_to_txt);
            if (!Directory.Exists(dir_path_sending_folder)) Directory.CreateDirectory(dir_path_sending_folder);

        }
        public void file_for_authorization_if_not_existant()
        {
            if (!File.Exists(path_authorization))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path_authorization))
                {
                    sw.WriteLine("gmail:");
                    sw.Write("app_password:");
                }
            }
        }
        public void Setup_Folder_Architecture()
        {
            Creating_app_directories_if_not_existant();

            file_for_authorization_if_not_existant();
        }
        

        // BUTTONS Sending Profiles
        int current_index_of_Sending_Profile()
        {
            string current_profile = comboBox_sending_profiles.Text;

            int index = 0;
            foreach(Sending_Profile prof in List_Sending_Profile)
            {
                if (prof.Sending_Profile_Name == current_profile) return index;
                index++;
            }

            return -1;
        }
        int current_index_of_Sending_Profile(string looking_for)
        {
            int index = 0;
            foreach (Sending_Profile prof in List_Sending_Profile)
            {
                if (prof.Sending_Profile_Name == looking_for) return index;
                index++;
            }

            return -1;
        }

        private void button_left_change_sending_profile_Click(object sender, EventArgs e)
        {
            int index = current_index_of_Sending_Profile();
            if(index != -1 && index != 0)
            {
                Debug_reset("STRZAŁKA W LEWO"); Debug_add(" "); Debug_add(" ");

                Debug_add("Update_Data_in_Current_Profile - starting....");
                Update_Data_in_Current_Profile();
                Debug_add("Update_Data_in_Current_Profile - DONE");

                Debug_add(" ");

                Debug_add("Load_Chosen_Profile - starting....");
                // załadowanie tego nowego                
                Load_Chosen_Profile(List_Sending_Profile[--index].Sending_Profile_Name.ToString());
                Debug_add("Load_Chosen_Profile - DONE");

                //combobox_change_selected_item(index);

                Debug_profile_check(get_information_of_profile(comboBox_sending_profiles.Items[1].ToString()));
            }
        }
        private void button_right_change_sending_profile_Click(object sender, EventArgs e)
        {
            int index = current_index_of_Sending_Profile();
            if (index != -1 && index != (List_Sending_Profile.Count() - 1))
            {
                Debug_reset("STRZAŁKA W PRAWO"); Debug_add(" "); Debug_add(" ");

                Debug_add("Update_Data_in_Current_Profile - starting....");
                Update_Data_in_Current_Profile();
                Debug_add("Update_Data_in_Current_Profile - DONE");

                Debug_add(" ");

                Debug_add("Load_Chosen_Profile - starting....");
                // załadowanie tego nowego                
                Load_Chosen_Profile(List_Sending_Profile[++index].Sending_Profile_Name.ToString());
                Debug_add("Load_Chosen_Profile - DONE");

                //combobox_change_selected_item(index);


                // po Load_Chosen_Profile jeszcze raz sprawdzamy zawartośc New Sending Profile

                Debug_profile_check(get_information_of_profile(comboBox_sending_profiles.Items[0].ToString()));                
            }
        }
        private void button_Add_New_sending_profile_Click(object sender, EventArgs e)
        {
            Sending_Profile New = new_sending_profile();


            if (comboBox_sending_profiles.Items.Count > 0)
            {
                Debug_add("Add New -> if (comboBox_sending_profiles.Items.Count > 0)");

                Update_Data_in_Current_Profile();                

                Add_New_adding_after_or_inserting_in_middle(New);
                Reset_Everything_Sending_Profile__from_new_profile();
            }
            else
            {
                Debug_add("Add New -> else");

                Add_New_adding_after_or_inserting_in_middle(New);
                Reset_Everything_Sending_Profile__from_new_profile();
            }

            update_sending_profile_counter();            
        }
        private void button_delete_sending_profile_Click(object sender, EventArgs e)
        {
            // usuwanie akutalnie wybranego

            // i ładowanie jednego na lewo od niego
            // a jeśli nic nie ma to Reset_Everythig_Sending_Profile();

            if (comboBox_sending_profiles.Items.Count == 0) return;

            string profile_to_delete = comboBox_sending_profiles.Text;
            int index = comboBox_sending_profiles.Items.IndexOf(profile_to_delete);
            Debug_reset("index - usuwanego profilu -> " + index);
            Debug_add("");

            // deleting
            foreach (var prof in List_Sending_Profile)
            {
                if(prof.Sending_Profile_Name == profile_to_delete)
                {
                    Debug_add("if(prof.Sending_Profile_Name == profile_to_delete)   ");
                    Debug_add(prof.Sending_Profile_Name + "==" + profile_to_delete);
                    List_Sending_Profile.Remove(prof);
                    comboBox_sending_profiles.Items.Remove(prof.Sending_Profile_Name.ToString());

                    // setting up remainnig index on top
                    if (comboBox_sending_profiles.Items.Count > 0)
                    {
                        if (comboBox_sending_profiles.Items.Count > index)
                        {
                            Debug_add("if (comboBox_sending_profiles.Items[index] != null)");

                            Load_Chosen_Profile(comboBox_sending_profiles.Items[index].ToString());
                            combobox_change_selected_item(index);                            
                        }
                        else
                        {
                            --index;
                            Debug_add("if (comboBox_sending_profiles.Items[index] == null)  " + index);

                            Load_Chosen_Profile(comboBox_sending_profiles.Items[index].ToString());
                            combobox_change_selected_item(index);                            
                        }
                    }

                    update_sending_profile_counter();
                    return;
                }
            }            
        }
        private void button_clear_sending_profiles_Click(object sender, EventArgs e)
        {
            Debug_reset("");

            List_Sending_Profile.Clear();
            comboBox_sending_profiles.Items.Clear();
            Reset_Everything_Sending_Profile__from_new_profile();


            update_dir_filter_counter_and_dir_and_file_counters();
            update_sending_profile_counter();
            /*
            // saving To && From

            List<string> too = to_List_from_combobox(comboBox_send_mail_to);        comboBox_send_mail_to.Items.Clear();
            List<string> fromm = to_List_from_combobox(comboBox_from);              comboBox_from.Items.Clear();


            if (File.Exists(path_serialized_profiles)) File.Delete(path_serialized_profiles); File.Create(path_serialized_profiles);


            // to
            foreach (var to in too) comboBox_send_mail_to.Items.Add(to);
            if (comboBox_send_mail_to.Items.Count > 0) comboBox_send_mail_to.SelectedIndex = 0;
            // from
            foreach (var from in fromm) comboBox_from.Items.Add(from);
            if (comboBox_from.Items.Count > 0) comboBox_from.SelectedIndex = 0;
            */
        }
        private void button_change_sending_profile_name_Click(object sender, EventArgs e)
        {
            string before = comboBox_sending_profiles.Text;      if (before == "") return;
            int index = current_index_of_Sending_Profile(before);

            Debug_reset("name changed");

            int x = 0, y = 0;

            if (WindowState == FormWindowState.Maximized)
            {
                x = RestoreBounds.Location.X;
                y = RestoreBounds.Location.Y;
            }
            else if (WindowState == FormWindowState.Normal)
            {
                x = Location.X;
                y = Location.Y;
            }
            else
            {
                x = RestoreBounds.Location.X;
                y = RestoreBounds.Location.Y;
            }

            using (var form = new Changing_Name(List_Sending_Profile, x, y))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string new_profiles_name = form.ReturnValue_name;
                    

                    Sending_Profile previous_profile_information = get_information_of_profile(before); if (previous_profile_information == null) return;
                    previous_profile_information.Sending_Profile_Name = new_profiles_name; 
                    modyfie_profile_on_the_list(before, previous_profile_information);                    

                    comboBox_sending_profiles.Items.Remove(before);
                    Change_name_same_place_on_list(new_profiles_name, index);

                    combobox_change_selected_item(new_profiles_name);


                    //Debug_add("new profiles name " + prof.Sending_Profile_Name);
                    // old
                    {
                        /*// old
                        {
                            foreach (Sending_Profile prof in List_Sending_Profile)
                            {
                                // CHANGE NAME
                                if (prof.Sending_Profile_Name == before)
                                {
                                    int index = current_index_of_Sending_Profile(before);

                                    prof.Sending_Profile_Name = new_profiles_name;

                                    comboBox_sending_profiles.Items.Remove(before);
                                    Change_name_same_place_on_list(new_profiles_name, index);



                                    combobox_change_selected_item(new_profiles_name);



                                    Debug_add("new profiles name " + prof.Sending_Profile_Name);

                                    return;
                                }
                            }
                        }*/
                    }
                }
            }
        }
        private void button_duplicate_sending_profile_Click(object sender, EventArgs e)
        {
            if (comboBox_sending_profiles.Items.Count == 0) return;

            string current_profile_for_duplication = comboBox_sending_profiles.Text;
            Update_Data_in_Current_Profile();


            // 1. funkcja zapisuje aktualny stan
            Sending_Profile status_right_now = status_right_NOW___adding_numbers_to_profile_name_if_needed(current_profile_for_duplication + " - duplication");

            // 2. funkcja wsadzająca, go tuż po tym który jest duplikowany
            Duplicating_adding_after_or_inserting_in_middle(status_right_now, current_profile_for_duplication);

            // 3. załadowanie tego zduplikowanego
            Load_Chosen_Profile(current_profile_for_duplication);

            combobox_change_selected_item(status_right_now.Sending_Profile_Name.ToString());
            update_sending_profile_counter();
        }


        // METHODS Sending Profiles
        private void Change_name_same_place_on_list(string adding, int index_at)
        {
            // the last element
            if (index_at == List_Sending_Profile.Count - 1)
            {
                Debug_add("the last element");
                
                comboBox_sending_profiles.Items.Add(adding);
            }
            // in the middle or first
            else
            {
                Debug_add("in the middle or first");
                
                comboBox_sending_profiles.Items.Insert(index_at, adding);
            }
        }
        private void Add_New_adding_after_or_inserting_in_middle(Sending_Profile status_saved)
        {
            Debug_add("Add_New_adding_after_or_inserting_in_middle -> Add New -> ''  " + status_saved.Sending_Profile_Name);


            if(comboBox_sending_profiles.Items.Count == 0)
            {
                List_Sending_Profile.Add(status_saved);
                comboBox_sending_profiles.Items.Add(status_saved.Sending_Profile_Name.ToString());

                combobox_change_selected_item(0);
            }
            else
            {
                int index = List_Sending_Profile.FindIndex(p => p.Sending_Profile_Name == comboBox_sending_profiles.Text) + 1;

                List_Sending_Profile.Insert(index, status_saved);
                comboBox_sending_profiles.Items.Insert(index, status_saved.Sending_Profile_Name.ToString());

                combobox_change_selected_item(status_saved.Sending_Profile_Name.ToString());
            }
        }
        private void Duplicating_adding_after_or_inserting_in_middle(Sending_Profile status_saved, string duplicated_item)
        {
            int index = List_Sending_Profile.FindIndex(p => p.Sending_Profile_Name == duplicated_item);

            // the last element
            if (index == List_Sending_Profile.Count - 1)
            {
                Debug_add("the last element");

                List_Sending_Profile.Add(status_saved);
                comboBox_sending_profiles.Items.Add(status_saved.Sending_Profile_Name.ToString());
            }
            // in the middle or first
            else
            {
                Debug_add("in the middle or first");

                index++;
                List_Sending_Profile.Insert(index, status_saved);
                comboBox_sending_profiles.Items.Insert(index, status_saved.Sending_Profile_Name.ToString());
            }            
        }

        private bool modyfie_profile_on_the_list(string profile_on_the_list_name, Sending_Profile information)
        {
            foreach (Sending_Profile prof in List_Sending_Profile)
            {
                if (prof.Sending_Profile_Name.ToString() == profile_on_the_list_name)
                {
                    Debug_add("chosen one" + prof.Sending_Profile_Name);
                    // Profile Name
                    //prof.Sending_Profile_Name = information.Sending_Profile_Name;         nazwa jest ta sama

                    // Lists

                    Debug_add("przed przypisaniem"); Debug_add("");

                    if (prof.Ser_List_of_Dirs.Count > 0) Debug_add("Ser_List_of_Dirs    got something " + prof.Ser_List_of_Dirs.Count);
                    else Debug_add("Ser_List_of_Dirs    nothing here");

                    if (prof.Ser_List_of_File_Paths.Count > 0) Debug_add("Ser_List_of_File_Paths    got something " + prof.Ser_List_of_File_Paths.Count);
                    else Debug_add("Ser_List_of_File_Paths    nothing here");


                    prof.Ser_List_of_Dirs = information.Ser_List_of_Dirs;
                    prof.Ser_List_of_File_Paths = information.Ser_List_of_File_Paths;


                    Debug_add("po przypisaniu"); Debug_add("");

                    // tutaj jest zapisywane poprawnie
                    if (prof.Ser_List_of_Dirs.Count > 0) Debug_add("Ser_List_of_Dirs    got something " + prof.Ser_List_of_Dirs.Count);
                    else Debug_add("Ser_List_of_Dirs    nothing here");

                    if (prof.Ser_List_of_File_Paths.Count > 0) Debug_add("Ser_List_of_File_Paths    got something " + prof.Ser_List_of_File_Paths.Count);
                    else Debug_add("Ser_List_of_File_Paths    nothing here");

                    // Bools
                    prof.Ser_Add_Date_Bool = information.Ser_Add_Date_Bool;
                    prof.Ser_Add_Hour_Bool = information.Ser_Add_Hour_Bool;
                    prof.Ser_Load_files_and_dirs = information.Ser_Load_files_and_dirs;
                    prof.Ser_Close_after_sending = information.Ser_Close_after_sending;
                    prof.Ser_Pack_to_zip = information.Ser_Pack_to_zip;

                    // Names
                    prof.Ser_Txt_converted_name = information.Ser_Txt_converted_name;
                    prof.Ser_Tytle = information.Ser_Tytle;
                    prof.Ser_Zip_name = information.Ser_Zip_name;

                    return true;
                }
            }

            return false;
        }
        private Sending_Profile get_information_of_profile(string profile_on_the_list_name)
        {
            foreach (Sending_Profile prof in List_Sending_Profile) if (prof.Sending_Profile_Name.ToString() == profile_on_the_list_name)
            {
                return prof;
            }

            return null;
        }
        

        private string get_unused_name(in string original)
        {            
            string name = original;
            int index = 1;
            while (List_Sending_Profile.Find(p => p.Sending_Profile_Name == name) != null)
            {
                index++;
                name = original + " - " + index.ToString();
            }

            return name;
        }
        private Sending_Profile status_right_NOW___adding_numbers_to_profile_name_if_needed(in string original)
        {
            Sending_Profile res = new Sending_Profile();

            // res.Sending_Profile_Name = get_unused_name(comboBox_sending_profiles.Text + " - duplication");
            res.Sending_Profile_Name = get_unused_name(original);

            res.Ser_List_of_Dirs = List_directory_with_filters;
            res.Ser_List_of_File_Paths = List_file_paths;

            res.Ser_Tytle = textBox_tytle.Text;            
            res.Ser_Txt_converted_name = textBox_txt_converted_name.Text;
            res.Ser_Zip_name = textBox_zip_name.Text;

            res.Ser_Add_Hour_Bool = checkBox_hour.Checked;
            res.Ser_Add_Date_Bool = checkBox_date.Checked;
            res.Ser_Load_files_and_dirs = checkBox_load_file_paths.Checked;
            res.Ser_Close_after_sending = checkBox_close_after_sending.Checked;
            res.Ser_Pack_to_zip = checkBox_zip.Checked;

            return res;
        }
        private Sending_Profile status_right_NOW___profile_name_SAME_AS_PARAMETER(in string original)
        {
            Sending_Profile res = new Sending_Profile();

            if (original != "" && comboBox_sending_profiles.Items.Contains(original)) res.Sending_Profile_Name = original;
            else res.Sending_Profile_Name = get_unused_name("New Sending Profile");

            res.Ser_List_of_Dirs = List_directory_with_filters;
            res.Ser_List_of_File_Paths = List_file_paths;

            res.Ser_Tytle = textBox_tytle.Text;
            res.Ser_Txt_converted_name = textBox_txt_converted_name.Text;
            res.Ser_Zip_name = textBox_zip_name.Text;

            res.Ser_Add_Hour_Bool = checkBox_hour.Checked;
            res.Ser_Add_Date_Bool = checkBox_date.Checked;
            res.Ser_Load_files_and_dirs = checkBox_load_file_paths.Checked;
            res.Ser_Close_after_sending = checkBox_close_after_sending.Checked;
            res.Ser_Pack_to_zip = checkBox_zip.Checked;

            return res;
        }

        private Sending_Profile new_sending_profile()
        {
            Sending_Profile res = new Sending_Profile();

            res.Sending_Profile_Name = get_unused_name("New Sending Profile");

            res.Ser_List_of_Dirs = new List<Directory_filters>();
            res.Ser_List_of_File_Paths = new List<string>();

            res.Ser_Tytle = "";
            res.Ser_Txt_converted_name = "";
            res.Ser_Zip_name = "";

            res.Ser_Add_Hour_Bool = false;
            res.Ser_Add_Date_Bool = false;
            res.Ser_Load_files_and_dirs = false;
            res.Ser_Close_after_sending = false;
            res.Ser_Pack_to_zip = false;

            return res;
        }
        private void Update_Data_in_Current_Profile()
        {
            Debug_add("Update_Data_in_Current_Profile -> starting...");

            Sending_Profile current_state = status_right_NOW___profile_name_SAME_AS_PARAMETER(comboBox_sending_profiles.Text);
            

            // if change is successfull
            if (comboBox_sending_profiles.Items.Contains(current_state.Sending_Profile_Name.ToString()))
            {
                modyfie_profile_on_the_list(comboBox_sending_profiles.Text, current_state);
                Debug_add("- UPDATEDED SUCCESSFULL -");
            }
            else
            {
                Debug_add("NEW PROFILE");

                List_Sending_Profile.Add(current_state);
                comboBox_sending_profiles.Items.Add(current_state.Sending_Profile_Name.ToString());

                combobox_change_selected_item(0);
                Debug_add("NEW PROFILE ---> completed all procedures");
            }

            Debug_add("Update_Data_in_Current_Profile -> DONE");


            // old
            {
                /*string current_profile = comboBox_sending_profiles.Text;
                foreach (Sending_Profile prof in List_Sending_Profile)
                {
                    // UPDATE
                    if (prof.Sending_Profile_Name == current_profile)
                    {
                        Debug_add("UPDATE");


                        prof.Ser_List_of_Dirs = List_directory_with_filters;
                        prof.Ser_List_of_File_Paths = List_file_paths;

                        prof.Ser_Tytle = textBox_tytle.Text;                    
                        prof.Ser_Txt_converted_name = textBox_txt_converted_name.Text;
                        prof.Ser_Zip_name = textBox_zip_name.Text;

                        prof.Ser_Add_Hour_Bool = checkBox_hour.Checked;
                        prof.Ser_Add_Date_Bool = checkBox_date.Checked;
                        prof.Ser_Load_files_and_dirs = checkBox_load_file_paths.Checked;
                        prof.Ser_Close_after_sending = checkBox_close_after_sending.Checked;
                        prof.Ser_Pack_to_zip = checkBox_zip.Checked;

                        return;
                    }
                }


                // NEW PROFILE            
                {
                    Debug_add("NEW PROFILE");
                    Sending_Profile new_profile = status_right_NOW();                

                    List_Sending_Profile.Add(new_profile);                
                    comboBox_sending_profiles.Items.Add(new_profile.Sending_Profile_Name.ToString());


                    combobox_change_selected_item(comboBox_sending_profiles, 0);
                    // comboBox_sending_profiles.SelectedIndex = 0;
                }*/
            }
        }
        private void Update_Data_in_Chosen_Profile(string profile_to_update)
        {
            Debug_add("Update_Data_in_Chosen_Profile");
            Debug_add("Update_Data_in_Chosen_Profile");
            Debug_add("Update_Data_in_Chosen_Profile");

            Sending_Profile current_state = status_right_NOW___profile_name_SAME_AS_PARAMETER(comboBox_sending_profiles.Text);
            

            // if change is successfull
            if (modyfie_profile_on_the_list(profile_to_update, current_state))
            {
                Debug_add("UPDATEDED SUCCESSFULL");
            }
            else
            {
                Debug_add("NEW PROFILE");

                List_Sending_Profile.Add(current_state);
                comboBox_sending_profiles.Items.Add(current_state.Sending_Profile_Name.ToString());


                combobox_change_selected_item(0);
            }


            // old
            {
                /*string current_profile = comboBox_sending_profiles.Text;
                foreach (Sending_Profile prof in List_Sending_Profile)
                {
                    // UPDATE
                    if (prof.Sending_Profile_Name == current_profile)
                    {
                        Debug_add("UPDATE");


                        prof.Ser_List_of_Dirs = List_directory_with_filters;
                        prof.Ser_List_of_File_Paths = List_file_paths;

                        prof.Ser_Tytle = textBox_tytle.Text;                    
                        prof.Ser_Txt_converted_name = textBox_txt_converted_name.Text;
                        prof.Ser_Zip_name = textBox_zip_name.Text;

                        prof.Ser_Add_Hour_Bool = checkBox_hour.Checked;
                        prof.Ser_Add_Date_Bool = checkBox_date.Checked;
                        prof.Ser_Load_files_and_dirs = checkBox_load_file_paths.Checked;
                        prof.Ser_Close_after_sending = checkBox_close_after_sending.Checked;
                        prof.Ser_Pack_to_zip = checkBox_zip.Checked;

                        return;
                    }
                }


                // NEW PROFILE            
                {
                    Debug_add("NEW PROFILE");
                    Sending_Profile new_profile = status_right_NOW();                

                    List_Sending_Profile.Add(new_profile);                
                    comboBox_sending_profiles.Items.Add(new_profile.Sending_Profile_Name.ToString());


                    combobox_change_selected_item(comboBox_sending_profiles, 0);
                    // comboBox_sending_profiles.SelectedIndex = 0;
                }*/
            }
        }

        private void Load_Chosen_Profile(string prof_name)
        {
            Debug_add("Load_Chosen_Profile(string prof_name)      " + prof_name);

            // if (List_Sending_Profile.Count == 0) return;

            Sending_Profile profile_info = get_information_of_profile(prof_name);               Debug_add("     złapałem " + profile_info.Sending_Profile_Name + " ");
            if (profile_info == null) Debug_reset("get_information_of_profile -> returnd NULL, unable to load profile");

            Debug_profile_check(profile_info);
            Reset_Everything_Sending_Profile__from_new_profile();
            Debug_profile_check(profile_info);
            

            apply_information(profile_info);
            update_dir_filter_counter_and_dir_and_file_counters();
            combobox_change_selected_item(profile_info.Sending_Profile_Name.ToString());
        }
        private void apply_information(Sending_Profile profile_info)
        {
            Debug_add("applying information ....................");

            // Dirs
            if (profile_info.Ser_List_of_Dirs.Count > 0) Debug_add("profile_info.Ser_List_of_Dirs - extracted something");
            else Debug_add("profile_info.Ser_List_of_Dirs - extracted nothing");

            if (profile_info.Ser_List_of_File_Paths.Count > 0) Debug_add("profile_info.Ser_List_of_File_Paths - extracted something");
            else Debug_add("profile_info.Ser_List_of_File_Paths - extracted nothing");



            List_directory_with_filters = profile_info.Ser_List_of_Dirs;
            if (List_directory_with_filters.Count > 0)
            {
                foreach (var recever in List_directory_with_filters) comboBox_directories.Items.Add(recever.directory_path);

                comboBox_directories.SelectedIndex = 0;
                apply_filters(List_directory_with_filters[0]);

                Debug_add("dirs - extracted something");
            }
            else Debug_add("dirs - extracted nothing");

            // Files
            List_file_paths = profile_info.Ser_List_of_File_Paths;
            if (List_file_paths.Count > 0)
            {
                foreach (var recever in List_file_paths) comboBox_file_paths.Items.Add(recever);
                comboBox_file_paths.SelectedIndex = 0;

                Debug_add("file paths - extracted something");
            }
            else Debug_add("file paths - extracted nothing");


            textBox_tytle.Text = profile_info.Ser_Tytle;
            textBox_txt_converted_name.Text = profile_info.Ser_Txt_converted_name;
            textBox_zip_name.Text = profile_info.Ser_Zip_name;

            checkBox_hour.Checked = profile_info.Ser_Add_Hour_Bool;
            checkBox_date.Checked = profile_info.Ser_Add_Date_Bool;
            checkBox_load_file_paths.Checked = profile_info.Ser_Load_files_and_dirs;
            checkBox_close_after_sending.Checked = profile_info.Ser_Close_after_sending;
            checkBox_zip.Checked = profile_info.Ser_Pack_to_zip;

            Debug_add("applying information .........DONE.........");
        }

        private void Reset_comboboxes_and_dir_extensions_list()
        {
            // dirs
            comboBox_directories.Items.Clear();
            comboBox_extensions_filter.Items.Clear();

            // file paths
            comboBox_file_paths.Items.Clear();


            //List_directory_with_filters.Clear();
            //List_file_paths.Clear();
        }
        private void Reset_Everything_Sending_Profile__from_new_profile()
        {
            Reset_comboboxes_and_dir_extensions_list();

            textBox_tytle.Text = "";            
            textBox_txt_converted_name.Text = "";
            textBox_zip_name.Text = "";

            checkBox_hour.Checked = false;
            checkBox_date.Checked = false;
            checkBox_load_file_paths.Checked = false;
            checkBox_close_after_sending.Checked = false;
            checkBox_zip.Checked = false;
        }


        // MOŻE POWODOWAĆ PROBLEMY //                                                                   !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        private void combobox_change_selected_item_generic(ComboBox comboBox, int index)
        {
            combobox_before_change = comboBox.Text;
            comboBox.SelectedIndex = index;
        }


        // Function Wrappers //
        private void combobox_change_selected_item(int index)
        {
            locked_selected_index_changed = true;

            combobox_before_change = comboBox_sending_profiles.Text;
            comboBox_sending_profiles.SelectedIndex = index;

            locked_selected_index_changed = false;
        }
        private void combobox_change_selected_item(string item)
        {
            locked_selected_index_changed = true;

            combobox_before_change = comboBox_sending_profiles.Text;
            comboBox_sending_profiles.SelectedItem = item;

            locked_selected_index_changed = false;
        }
        private void set_label_text__move_relative_to_original_position_generic(Label label, string text, int x, int y)
        {
            label.Location = new Point(label_x + x, label_y + y);
            label.Text = text;
        }
        private void label3_set_label_text__move_relative_to_original_position(string text, int x, int y)
        {
            button_Add_New_sending_profile.Enabled = false;
            button_change_sending_profile_name.Enabled = false;
            button_delete_sending_profile.Enabled = false;
            button_dir_extensions_add.Enabled = false;
            button_save_to_config.Enabled = false;
            button_duplicate_sending_profile.Enabled = false;
            button_clear_directories.Enabled = false;


            button_dir_extensions_delete.Enabled = false;
            button_load_next_file.Enabled = false;
            button_right_change_sending_profile.Enabled = false;
            button_left_change_sending_profile.Enabled = false;
            button_clear_sending_profiles.Enabled = false;
            button_wyslij.Enabled = false;
            button_combobox_file_paths_clear.Enabled = false;
            button_open_explorer.Enabled = false;



            set_label_text__move_relative_to_original_position_generic(label3, text, x, y);
        }



        private void comboBox_sending_profiles_Combohandler(object sender, EventArgs e)
        {
            change_focus();
            this.BeginInvoke(new Action(() => { comboBox_sending_profiles.Select(0, 0); }));

            if(!locked_combohandler)
            {
                Debug_add("Combohandler (" + comboBox_sending_profiles.Text + ")");
                locked_combohandler = true;

                mouse_change_sending_profile = comboBox_sending_profiles.Text;
            }
            else locked_combohandler = false;
        }
        private void comboBox_sending_profiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            // może trzeba będzie blokować, żeby nie psuły się pozostałe części
            
            if (app_startup)
            {
                app_startup = false;                
                return;
            }

            if(!locked_selected_index_changed)
            {
                Debug_add("SelectedIndexChanged (" + comboBox_sending_profiles.Text + ")");

                Update_Data_in_Chosen_Profile(mouse_change_sending_profile);
                Load_Chosen_Profile(comboBox_sending_profiles.Text.ToString());
            }

            // ALL //
            {
                /*Debug_add("comboBox_sending_profiles_SelectedIndexChanged");
                Debug_add("comboBox_sending_profiles_SelectedIndexChanged");
                Debug_add("comboBox_sending_profiles_SelectedIndexChanged");



                //if(List_Sending_Profile.Count > 1)
                if (app_startup)
                {
                    app_startup = false;
                    Debug_add("");
                    Debug_add("ENDING quick");
                    Debug_add("");
                    return;
                }


                // łapie na który został zmieniony
                if (!changing_name)
                {
                    Debug_add("NOW UPDATING          " + combobox_before_change);
                    Update_Data_in_Chosen_Profile(combobox_before_change);

                    Load_Chosen_Profile(comboBox_sending_profiles.Text);
                }
                else
                {
                    Update_Data_in_Chosen_Profile(comboBox_sending_profiles.Text);
                }*/
            }
        }        


        
        List<string> to_List_from_combobox(ComboBox box)
        {            
            List<string> res = new List<string>();

            for (int i = 0; i < box.Items.Count; i++) res.Add(box.Items[i].ToString());

            return res;
        }

        // Save Preferences - SERIALIZATION
        private void button_save_to_config_Click(object sender, EventArgs e)
        {   
            Update_Data_in_Current_Profile();

            Debug_add("BIG_SER - set up");
            // set up 
            BIG_SER.all_profiles = List_Sending_Profile;
            BIG_SER.last_used_profile = comboBox_sending_profiles.Text;
            BIG_SER.sending_to = to_List_from_combobox(comboBox_send_mail_to);
            BIG_SER.sending_from = to_List_from_combobox(comboBox_from);


            Debug_add("BIG_SER - check what's been asigned");
            // checking whats inside
            {
                Debug_add("List is ");
                if (List_Sending_Profile.Count == 0) Debug_add("EMPTY");
                else Debug_add("GOT SOMETHING TO SAVE");

                Debug_add("Last used profile ->" + comboBox_sending_profiles.Text);
            }            

            using (FileStream fileStream = new FileStream(path_serialized_profiles, FileMode.Create))
                serializer.Serialize(fileStream, BIG_SER);

            update_sending_profile_counter();
        }









        // to change the name of profile        -> wyświetla się nowy form, główny jest disabled i po zamknięciu przekazuję wpisaną informację
        //                                          (do niej jest podana lista, żeby na bierząco sprawdzać czy już jest taki profil)

        /*public void ShowMyDialogBox()
        {
            Form testDialog = new Form();

            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                this.txtResult.Text = testDialog.TextBox1.Text;
            }
            else
            {
                this.txtResult.Text = "Cancelled";
            }
            testDialog.Dispose();
        }*/




        




        // CONSTRUCTOR
        public Sender_Man()
        {
            app_startup = true;

            InitializeComponent();
            //ShowMyDialogBox();
            change_focus();
            counter = 1;
            file_counter = 1;
            label_x = label3.Location.X;    label_y = label3.Location.Y;
            label3_change_configuration_x = -50; label3_change_configuration_y = 5;
            label_debug.Text = "";
            label_debug_profile_check.Text = "";
            label3.Text = "";
            label_invalid_data.Text = "";
            label_how_many_loaded_files.Text = "";
            label_dir_filters_how_many.Text = "";
            label_counter_dir.Text = "";            label_counter_file_paths.Text = "";
            combobox_before_change = "";
            locked_combohandler = false;
            locked_selected_index_changed = false;
            List_txt_files = new List<string>();
            List_file_paths = new List<string>();
            List_directory_with_filters = new List<Directory_filters>();

            ///////////////////////////////////////////////////////////////////////////////

            config = new List<Config_info>();            
            authorization = new List<Config_info>();
            authorization_check = new List<Config_info>();            

            // gmail authorization
            authorization_gmail = "";
            authorization_password = "";
            authorization_pass = true;

            
            // Directories
            Setup_Folder_Architecture();
            Resetting_app_directories();

            

            // załadowanie authorization data
            {
                // Z pliku do listy
                {
                    bool info_part = true;
                    string name;
                    string data;
                    foreach (string line in System.IO.File.ReadLines(path_authorization))
                    {
                        // setup
                        info_part = true;
                        name = "";
                        data = "";

                        foreach (char c in line)
                        {
                            if (c == ':')
                            {
                                info_part = false;
                                continue;
                            }

                            if (info_part) name += c;
                            else data += c;
                        }

                        // if nothing 
                        if (data == "")
                        {
                            if (name == "gmail") data = "nothing";
                            else if (name == "app_password") data = "nothing";
                        }

                        authorization.Add(new Config_info(name, data));
                    }
                }

                // Z listy do string
                {
                    // gmail
                    authorization_gmail = (authorization.Find(x => x.Name() == "gmail")).Data();

                    // password
                    authorization_password = (authorization.Find(x => x.Name() == "app_password")).Data();



                    Debug_with_file("-authorization_gmail-" + authorization_gmail + "-");
                    Debug_with_file("-authorization_password-" + authorization_password + "-");
                }
            }
            
            if (authorization_gmail == "nothing" || authorization_password == "nothing")
            {
                Debug_with_file("if (authorization_gmail == 'nothing' || authorization_password == 'nothing')");


                label3_set_label_text__move_relative_to_original_position("Change configuration and restart", label3_change_configuration_x, label3_change_configuration_y);                
                button_wyslij.Text = "Wrong authorization";
                return;
            }

            // authorization check
            {
                bool need_for_check = true;
                if (File.Exists(path_authorization_check))
                {
                    // loading data from \Authorization_Check.txt
                    {
                        bool info_part = true;
                        string name;
                        string data;
                        foreach (string line in System.IO.File.ReadLines(path_authorization_check))
                        {
                            // setup
                            info_part = true;
                            name = "";
                            data = "";

                            foreach (char c in line)
                            {
                                if (c == ':')
                                {
                                    info_part = false;
                                    continue;
                                }

                                if (info_part) name += c;
                                else data += c;
                            }

                            authorization_check.Add(new Config_info(name, data));
                        }
                    }

                    // odczytywanie wartości
                    {
                        // gmail
                        string gmail_from_config = (authorization_check.Find(x => x.Name() == "gmail")).Data();

                        // tytle
                        string password_from_config = (authorization_check.Find(x => x.Name() == "app_password")).Data();

                        if (authorization_gmail == gmail_from_config && authorization_password == password_from_config) need_for_check = false;                        
                    }
                }

                // nie ma pliku "Authorization_Check.txt"
                if (need_for_check)
                {
                    Debug_with_file("1");

                    Authorization_Check();

                    Debug_with_file("2");

                    // creating Authorization_Check.txt or removing old one
                    if (authorization_pass)
                    {
                        if (File.Exists(path_authorization_check)) File.Delete(path_authorization_check);

                        using (StreamWriter sw = File.CreateText(path_authorization_check))
                        {
                            sw.WriteLine("gmail:" + authorization_gmail);
                            sw.Write("app_password:" + authorization_password);
                        }
                    }

                    else
                    {
                        label3_set_label_text__move_relative_to_original_position("Change configuration and restart", label3_change_configuration_x, label3_change_configuration_y);
                        button_wyslij.Text = "Wrong authorization";                        
                    }
                }
            }

            

            // Nowa DeSerializacja
            List_Sending_Profile = new List<Sending_Profile>();
            BIG_SER = new One_Big_Serializer();
            

            // aplikowanie zapisanych ustawień
            {
                if (File.Exists(path_serialized_profiles) && new FileInfo(path_serialized_profiles).Length != 0)
                {
                    using (FileStream fileStream = new FileStream(path_serialized_profiles, FileMode.Open))
                        BIG_SER = (One_Big_Serializer)serializer.Deserialize(fileStream);

                    // APLIKOWANIE zapisanych ustawień
                    {
                        List_Sending_Profile = BIG_SER.all_profiles;
                        string latest_used_profile = BIG_SER.last_used_profile;

                        // to
                        foreach (var to in BIG_SER.sending_to) comboBox_send_mail_to.Items.Add(to);
                        if (comboBox_send_mail_to.Items.Count > 0) comboBox_send_mail_to.SelectedIndex = 0;

                        // from
                        foreach (var from in BIG_SER.sending_from) comboBox_from.Items.Add(from);
                        if (comboBox_from.Items.Count > 0) comboBox_from.SelectedIndex = 0;


                        if (List_Sending_Profile.Count == 0) Debug_add("dodana lista jest pusta");

                        if (latest_used_profile == "")
                        {
                            Debug_add("latest_used_profile == ''");
                            Reset_Everything_Sending_Profile__from_new_profile();
                        }

                        else
                        {
                            Debug_add("latest_used_profile != ''");
                            //foreach (var prof in List_Sending_Profile) comboBox_sending_profiles.Items.Add(prof.Sending_Profile_Name.ToString());


                            if (List_Sending_Profile.Count() > 0)
                            {
                                foreach (Sending_Profile prof in List_Sending_Profile) comboBox_sending_profiles.Items.Add(prof.Sending_Profile_Name.ToString());

                                combobox_change_selected_item(latest_used_profile);
                                // comboBox_sending_profiles.SelectedItem = latest_used_profile;

                                Debug_add("about to load chosen profile");
                                Load_Chosen_Profile(latest_used_profile);
                            }
                        }
                    }
                }
                else
                {
                    Reset_Everything_Sending_Profile__from_new_profile();
                }
            }


            change_focus();
            update_dir_filter_counter_and_dir_and_file_counters();
            update_sending_profile_counter();
        }










        // SEND BUTTON
        private async void button_wyslij_Click(object sender, EventArgs e)
        {
            change_focus();

            if (!authorization_pass)
            {
                label3_set_label_text__move_relative_to_original_position("Change configuration and restart", label3_change_configuration_x, label3_change_configuration_y);
                return;
            }

            if (!(textBox_tytle.Text != "" && comboBox_send_mail_to.Text != "" &&
                    (List_txt_files.Count() > 0 || (checkBox_load_file_paths.Checked && ( (List_file_paths.Count() > 0) || (List_directory_with_filters.Count() > 0))))
                    && is_there_internet_connection()))
            {
                label3.Text = "";
                label_invalid_data.Text = "blank data or no files attached";
                return;
            }

            if (!permission_to_continue())
            {
                //label_invalid_data.Text += " -> no permission";
                return;
            }

            button_wyslij.Enabled = false;
            Application.UseWaitCursor = true;

            try
            {
                Debug_reset("starting to schedule_mails");
                await schedule_mails();
                Debug_add("finished"); Debug_add(" ");


                Debug_add("starting to send_mails");
                await send_mails();
                Debug_add("finished"); Debug_add(" ");

                // opcja żeby załadować mail body tylko w jednym z maili jeśli będzie więcej albo doczepić do wszystkich
                // dlatego pierwszy mail jaki zostanie wysłany nie będzie w parallel for żeby nie było konfliktów między wątkowych                
            }
            catch
            {
                Application.UseWaitCursor = false;
                label3_set_label_text__move_relative_to_original_position("something went wrong", 0, 0);
                return;
            }
            Application.UseWaitCursor = false;

            // info            
            label3_set_label_text__move_relative_to_original_position(counter.ToString() + " Mail sent", 0, 0);

            // czyszczenie
            clean_up();
            button_wyslij.Enabled = true;

            // are we closing
            if(checkBox_close_after_sending.Checked) System.Windows.Forms.Application.Exit();
        }

        // SEND BUTTON - helpers        
        private bool permission_to_continue()
        {
            // gmail check
            if (!IsValid(comboBox_send_mail_to.Text))
            {
                label_invalid_data.Text = "invalid gmail";
                return false;
            }

            // checking if the files exist
            if (checkBox_load_file_paths.Checked)
            {
                // sprawdzenie, czy git pliki i directories
                foreach (string file in List_file_paths)
                {
                    if (!File.Exists(file))
                    {
                        MessageBox.Show(file + "<- path doesn't exist");
                        //label_file_path_valid.Text = "path doesn't exist";
                        return false;
                    }
                }
            }

            return true;
        }

        // CLEAN UP
        private void clean_up()
        {
            List_txt_files.Clear();
            Resetting_app_directories();

            counter++;
            file_counter = 1;
            label_how_many_loaded_files.Text = "";            
            label_invalid_data.Text = "";
        }


        // SCHEDULE MAILS                           // RETURN JAK PLIK JEST WIĘKSZY NIŻ 25 MB   MOŻE DA SIĘ JAKOŚ PRZEKAZAĆ
        private async Task schedule_mails()
        {
            await Task.Run(async () => {

                // creating -> Sending Folder
                if (Directory.Exists(dir_path_sending_folder)) Directory.Delete(dir_path_sending_folder, true);
                Directory.CreateDirectory(dir_path_sending_folder);

                long current_load_in_bytes = 0;
                int folder_index = 1;
                string current_dir_name = dir_path_sending_folder + @"\email_" + folder_index.ToString();
                DirectoryInfo scheduled_directory = new DirectoryInfo(current_dir_name);
                Directory.CreateDirectory(scheduled_directory.FullName);

                // MessageBox.Show("scheduling dir");

                // przejście przez dir                
                foreach (var dir in List_directory_with_filters)
                {
                    DirectoryInfo current_directory = new DirectoryInfo(dir.directory_path);

                    // zaciągamy osobno pliki
                    if (!dir.pack_whole_dir_to_zip)
                    {
                        // bierzemy wszystkie pliki
                        if (dir.list_filters.Count == 0)
                        {
                            //MessageBox.Show("bierzemy wszystkie pliki");

                            foreach (var file in current_directory.GetFiles("*"))
                            {
                                //MessageBox.Show("file in current_directory");

                                if (can_we_put_another_file_in_this_directory(file, ref current_load_in_bytes))
                                {
                                    System.IO.File.Copy(file.FullName, scheduled_directory.FullName + @"\" + file.Name, true);                                    
                                }
                                else
                                {
                                    create_new_directory(ref folder_index, ref current_dir_name, ref scheduled_directory, ref current_load_in_bytes);

                                    if (can_we_put_another_file_in_this_directory(file, ref current_load_in_bytes))
                                    {
                                        System.IO.File.Copy(file.FullName, scheduled_directory.FullName + @"\" + file.Name, true);
                                    }
                                    else
                                    {
                                        // plik jest za duży
                                        clean_up();
                                        MessageBox.Show(file + "    file is too big\r<every google mail has to be less than 25MB>", "Unable to send");
                                        return;
                                    }
                                }
                            }
                        }

                        // tylko rozszerzenia
                        else
                        {
                            foreach (string extension in dir.list_filters)
                            {
                                foreach (var file in current_directory.GetFiles("*." + extension))
                                {
                                    if (can_we_put_another_file_in_this_directory(file, ref current_load_in_bytes))
                                    {   
                                        System.IO.File.Copy(file.FullName, scheduled_directory.FullName + @"\" + file.Name, true);
                                    }
                                    else
                                    {
                                        create_new_directory(ref folder_index, ref current_dir_name, ref scheduled_directory, ref current_load_in_bytes);

                                        if (can_we_put_another_file_in_this_directory(file, ref current_load_in_bytes))
                                        {
                                            System.IO.File.Copy(file.FullName, scheduled_directory.FullName + @"\" + file.Name, true);
                                        }
                                        else
                                        {
                                            // plik jest za duży
                                            clean_up();
                                            MessageBox.Show(file + "    file is too big\r<every google mail has to be less than 25MB>", "Unable to send");
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // pakujemy wszystko do zip
                    else
                    {
                        // bez kopiowania tylko pakowanie do właściwego pliku
                        // sprawszamy ile jest MB po zapakowaniu
                        
                        if (Directory.Exists(dir_path_tmp_zip)) Directory.Delete(dir_path_tmp_zip, true);
                        Directory.CreateDirectory(dir_path_tmp_zip);

                        string final = dir_path_tmp_zip + @"\" + current_directory.Name + ".zip";
                        ZipFile.CreateFromDirectory(current_directory.FullName, final);

                        FileInfo zip = new FileInfo(final);
                        if(zip.Length > EMAIL_SIZE_LIMIT)
                        {
                            clean_up();
                            MessageBox.Show(scheduled_directory.FullName + final + "    packed directory is too big\r<every google mail has to be less than 25MB>", "Unable to send");                            
                            return;
                        }
                        else
                        {
                            if (can_we_put_another_file_in_this_directory(zip, ref current_load_in_bytes))
                            {
                                System.IO.File.Move(zip.FullName, scheduled_directory.FullName + @"\" + zip.Name);                                
                            }
                            else
                            {
                                create_new_directory(ref folder_index, ref current_dir_name, ref scheduled_directory, ref current_load_in_bytes);

                                if (can_we_put_another_file_in_this_directory(zip, ref current_load_in_bytes))
                                {
                                    System.IO.File.Move(zip.FullName, scheduled_directory.FullName + @"\" + zip.Name);                                    
                                }
                                else
                                {
                                    // plik jest za duży
                                    clean_up();
                                    MessageBox.Show(zip.FullName + "    LOGICALY NEVER SHOULD HAPPEN\r<every google mail has to be less than 25MB>", "Unable to send");
                                    return;
                                }
                            }
                        }
                    }
                }

                // MessageBox.Show("scheduling paths and txt");

                // przejście przez path i txt
                List<string> combined_path_and_txt_files = new List<string>();
                combined_path_and_txt_files.AddRange(List_file_paths);
                combined_path_and_txt_files.AddRange(List_txt_files);
                
                foreach (var path in combined_path_and_txt_files)
                {
                    FileInfo file = new FileInfo(path);

                    if (can_we_put_another_file_in_this_directory(file, ref current_load_in_bytes))
                    {
                        System.IO.File.Copy(file.FullName, scheduled_directory.FullName + @"\" + file.Name, true);
                    }
                    else
                    {
                        create_new_directory(ref folder_index, ref current_dir_name, ref scheduled_directory, ref current_load_in_bytes);                        

                        if (can_we_put_another_file_in_this_directory(file, ref current_load_in_bytes))
                        {
                            System.IO.File.Copy(file.FullName, scheduled_directory.FullName + @"\" + file.Name, true);
                        }
                        else
                        {
                            // plik jest za duży
                            clean_up();
                            MessageBox.Show(file + "    file is too big\r<every google mail has to be less than 25MB>", "Unable to send");
                            return;
                        }
                    }
                }
            });
        }
        
        // SCHEDULE MAILS - helpers
        private bool can_we_put_another_file_in_this_directory(FileInfo file, ref long current)
        {
            if (file.Length + current < EMAIL_SIZE_LIMIT)
            {
                current += file.Length;
                return true;
            }
            else return false;
        }
        private void create_new_directory(ref int folder_index, ref string current_dir_name, ref DirectoryInfo scheduled_directory, ref long current_load_in_bytes)
        {
            folder_index++;
            current_dir_name = dir_path_sending_folder + @"\" + "email_" + folder_index.ToString();
            scheduled_directory = new DirectoryInfo(current_dir_name);
            Directory.CreateDirectory(scheduled_directory.FullName);
            current_load_in_bytes = 0;
        }


        // SEND MAILS
        private async Task send_mails()
        {
            //await Task.Run(async () => {

            DirectoryInfo d = new DirectoryInfo(dir_path_sending_folder);

            string from = comboBox_from.Text;
            string send_mail_to = comboBox_send_mail_to.Text;
            string tytle = textBox_tytle.Text;
            string body = textBox_body.Text;
            string zip_name = textBox_zip_name.Text;

/*            Parallel.ForEach(d.GetDirectories(), dir =>
            {   
                sending_procedure(dir, from, send_mail_to, tytle, body, zip_name);
            });*/


            // chyba nie ma różnicy w prędkości
            foreach(var dir in d.GetDirectories())
            {
                sending_procedure(dir, from, send_mail_to, tytle, body, zip_name);
            }

            //});
        }


        // SENDING PROCEDURE
        private void sending_procedure(DirectoryInfo directoryInfo, string from, string send_mail_to, string tytle, string body, string zip_name)
        {
            //await Task.Run(async () => {

            var email = new MimeMessage();

            // From
            //email.From.Add(new MailboxAddress(textBox_from.Text, "secret@gmail.com"));
            email.From.Add(new MailboxAddress(from, "secret@gmail.com"));
                
            // To
            //email.To.Add(new MailboxAddress("Receiver Name", comboBox_send_mail_to.Text));
            email.To.Add(new MailboxAddress("Receiver Name", send_mail_to));
                
            // Tytle
            {
                //string tytle = textBox_tytle.Text;
                string added = "";
                DateTime date = DateTime.Now;

                // date + hours
                if (checkBox_hour.Checked && checkBox_date.Checked)
                {
                    added += " " + date.ToString();
                    added = added.Remove(17);

                    // jak z sekundami to bez usinania
                }
                // only date
                else if (checkBox_date.Checked)
                {
                    added += " " + date.ToString();
                    added = added.Remove(11);
                }
                // only hours
                else if (checkBox_hour.Checked)
                {
                    added += " " + date.ToString();
                    added = added.Remove(0, 11);
                    added = added.Remove(6);

                    // jak z sekundami to bez usinania
                }

                tytle += added;
                email.Subject = tytle;
            }

            var builder = new BodyBuilder();

            // Body
            //builder.TextBody = textBox_body.Text;
            builder.TextBody = body;



            // wysyłam wszystko co było w folderze
            {
                // same pliki
                if (!checkBox_zip.Checked)
                {
                    foreach (var file in directoryInfo.GetFiles("*"))
                    {
                        builder.Attachments.Add(file.FullName);
                    }
                }

                // spakowane do zip
                else
                {
                    string final = @"";
                    //if (textBox_zip_name.Text != "") final += textBox_zip_name.Text + ".zip";
                    if (zip_name != "") final += zip_name + ".zip";
                    else final += "zip container.zip";


                    ZipFile.CreateFromDirectory(directoryInfo.FullName, final);
                    builder.Attachments.Add(final);
                    File.Delete(final);
                }
            }
            


            // authentication and sending
            email.Body = builder.ToMessageBody();
            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                // nowy stuff
                {
                    smtp.SslProtocols = System.Security.Authentication.SslProtocols.Tls11;
                    smtp.ServerCertificateValidationCallback = (mysender, certificate, chain, sslPolicyErrors) => { return true; };
                    smtp.CheckCertificateRevocation = false;
                }

                smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);


                smtp.Authenticate(authorization_gmail, authorization_password);

                smtp.Send(email);
                smtp.Disconnect(true);
            }            
        }       






















        // load next txt file
        private void button_load_next_file_Click(object sender, EventArgs e)
        {
            if (textBox_to_txt_file.Text != "" && textBox_txt_converted_name.Text != "")
            {
                if (!Directory.Exists(dir_path_sending_folder)) Directory.CreateDirectory(dir_path_sending_folder);

                // dodaje 1 plik .txt
                string file_name = dir_path_for_converted_to_txt + @"\" + textBox_txt_converted_name.Text + ".txt";

                // sprawdzenie czy trzeba dodać numerację do nazwy pliku //
                if (List_txt_files.Count() > 0 && List_txt_files.Contains(file_name))
                {                    
                    int c = 1;
                    file_name = dir_path_for_converted_to_txt + @"\" + textBox_txt_converted_name.Text + "_" + c.ToString() + ".txt";
                    while (List_txt_files.Contains(file_name))
                    {
                        c++;
                        file_name = dir_path_for_converted_to_txt + @"\" + textBox_txt_converted_name.Text + "_" + c.ToString() + ".txt";
                    }
                }
                List_txt_files.Add(file_name);


                File.Create(file_name).Close(); // Create file
                using (StreamWriter sw = File.AppendText(file_name))
                {
                    sw.WriteLine(textBox_to_txt_file.Text); // Write text to .txt file
                }

                
                label_how_many_loaded_files.Text = file_counter + " Converted file";
                if (file_counter > 1) label_how_many_loaded_files.Text += "s";

                file_counter++;
            }
        }

        // file explorer
        private void button_open_explorer_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = true;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {                
                string[] arrAllFiles = choofdlog.FileNames; //used when Multiselect = true           
                foreach(string p in arrAllFiles)
                {
                    if (File.Exists(p))
                    {
                        List_file_paths.Add(p);
                        comboBox_file_paths.Items.Add(p);
                    }
                }
            }
        }

        
        private void deleting_from_combobox_replacement(ComboBox combo)
        {
            string input = combo.Text;

            if (combo.Items.Contains(input))
            {
                int index = combo.Items.IndexOf(input);
                combo.Items.Remove(input);

                if (combo.Items.Count > 0)
                {
                    if (index <= combo.Items.Count - 1) combo.SelectedIndex = index;
                    else
                    {
                        --index;
                        combo.SelectedIndex = index;
                    }
                }
                else combo.Text = "";
            }
        }

        private void button_check_Click(object sender, EventArgs e)
        {
            deleting_from_combobox_replacement(comboBox_send_mail_to);
        }
        private void button_email_add_Click(object sender, EventArgs e)
        {
            string input = comboBox_send_mail_to.Text;
            if (input != "" && !comboBox_send_mail_to.Items.Contains(input))
            {
                comboBox_send_mail_to.Items.Add(input);                
            }
        }

        // from list
        private void button_from_add_Click(object sender, EventArgs e)
        {
            string input = comboBox_from.Text;

            if (!comboBox_from.Items.Contains(input)) comboBox_from.Items.Add(input);
        }
        private void button_from_delete_Click(object sender, EventArgs e)
        {
            deleting_from_combobox_replacement(comboBox_from);
        }

        // authorization
        private void Authorization_Check()
        {
            Debug_with_file("Authorization_Check");

            // different exception
            {/*
                
                catch (SmtpCommandException e)
                {
                    authorization_pass = false;
                    button_wyslij.Text = "1";
                }
                catch (SmtpFailedRecipientsException e)
                {
                    authorization_pass = false;
                    button_wyslij.Text = "2";
                }
                catch (SmtpFailedRecipientException e)
                {
                    authorization_pass = false;
                    button_wyslij.Text = "3";
                }                
                catch (SmtpProtocolException e)
                {
                    authorization_pass = false;
                    button_wyslij.Text = "4";
                }
                catch (SmtpException e)
                {
                    authorization_pass = false;
                    button_wyslij.Text = "SMPT Exception No internet Connection";
                }
                */
            }

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                // Internet
                if (!is_there_internet_connection())
                {
                    button_wyslij.Text = "No internet connection";
                    authorization_pass = false; 
                    return;
                }

                // nowy stuff
                {
                    smtp.SslProtocols = System.Security.Authentication.SslProtocols.Tls11;
                    smtp.ServerCertificateValidationCallback = (mysender, certificate, chain, sslPolicyErrors) => { return true; };
                    smtp.CheckCertificateRevocation = false;
                }

                // Ability to connect to SMTP
                try
                {                    
                    smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                }
                catch
                {
                    authorization_pass = false;                    
                    label3_set_label_text__move_relative_to_original_position("SMTP server is unreachable", 0, 0);
                    button_wyslij.Text = "Unable to authorize";
                    smtp.Disconnect(true); return;
                }                

                // Is authorization right
                try 
                {
                    smtp.Authenticate(authorization_gmail, authorization_password);
                }
                catch
                {
                    authorization_pass = false;
                    label3_set_label_text__move_relative_to_original_position("Change configuration and restart", label3_change_configuration_x, label3_change_configuration_y);
                    button_wyslij.Text = "Wrong authorization";
                }

                smtp.Disconnect(true);
            }
        }
        private bool is_there_internet_connection()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }
        private static bool IsValid(string email)
        {
            var valid = true;

            try { var emailAddress = new MailAddress(email); }
            catch { valid = false; }

            return valid;
        }

        // drag and drop
        private void DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void comboBox_file_paths_DragDrop(object sender, DragEventArgs e)
        {
            add_to_textbox_and_Combobox_after_DragAndDrop(e);
        }
        private void comboBox_directories_DragDrop(object sender, DragEventArgs e)
        {
            add_to_textbox_and_Combobox_after_DragAndDrop(e);
        }
        private void add_to_textbox_and_Combobox_after_DragAndDrop(DragEventArgs e)
        {
            string[] path_list_of_files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string p in path_list_of_files)
            {
                if (File.Exists(p))
                {
                    List_file_paths.Add(p);
                    comboBox_file_paths.Items.Add(p);
                }
                else if (Directory.Exists(p) && !comboBox_directories.Items.Contains(p))
                {
                    List_directory_with_filters.Add(new Directory_filters(p));
                    comboBox_directories.Items.Add(p);
                }
            }

            if(comboBox_directories.Items.Count > 0 && List_directory_with_filters.Count > 0) comboBox_directories.SelectedIndex = 0;
            if(comboBox_file_paths.Items.Count > 0 && List_file_paths.Count > 0) comboBox_file_paths.SelectedIndex = 0;

            update_dir_and_path_counters();
        }

        // comboBox not highlighted
        void comboBox_send_mail_to_DropDownClosed(object sender, EventArgs e)
        {
            change_focus();
            this.BeginInvoke(new Action(() => { comboBox_send_mail_to.Select(0, 0); }));
        }
        private void comboBox_from_DropDown(object sender, EventArgs e)
        {
            change_focus();
            this.BeginInvoke(new Action(() => { comboBox_from.Select(0, 0); }));
        }
        void comboBox_directories_DropDownClosed(object sender, EventArgs e)
        {
            //Debug_add("comboBox_directories_DropDownClosed");

            change_focus();
            this.BeginInvoke(new Action(() => { comboBox_directories.Select(0, 0); }));
        }
        private void comboBox_directories_SelectedIndexChanged(object sender, EventArgs e)
        {
            change_focus();     //Debug_add("comboBox_directories_SelectedIndexChanged");

            if (comboBox_directories.SelectedItem != null)
            {
                //Debug_add(comboBox_directories.SelectedItem.ToString());

                foreach (var obj in List_directory_with_filters)
                {
                    if (comboBox_directories.Text == obj.directory_path)
                    {
                        apply_filters(obj);
                        checkBox_whole_dir_to_zip.Checked = obj.pack_whole_dir_to_zip;
                    }
                }

                label_dir_filters_how_many.Text = "";
                update_dir_filter_counter_and_dir_and_file_counters();
            }
            else
            {
                label_dir_filters_how_many.Text = "";
                update_dir_filter_counter_and_dir_and_file_counters();
            }
        }
        void comboBox_extensions_filter_DropDownClosed(object sender, EventArgs e)
        {
            change_focus();
            this.BeginInvoke(new Action(() => { comboBox_extensions_filter.Select(0, 0); }));
        }
        void apply_filters(Directory_filters all_extension)
        {
            comboBox_extensions_filter.Items.Clear();
            comboBox_extensions_filter.Text = "";

            // label_dir_filters_how_many <---> tutaj to chce mieć modyfikowane -- jednak nie

            if (all_extension.list_filters.Count == 0) return;

            //Debug_reset("applying filters");
            foreach (string filter in all_extension.list_filters)
            {
                //Debug_add(filter);
                comboBox_extensions_filter.Items.Add(filter);
                update_dir_filter_counter_and_dir_and_file_counters();
            }            
        }

        // BUTTONS directories extensions -> add or delete
        private void button_dir_extensions_add_Click(object sender, EventArgs e)
        {
            change_focus();

            // to że jest jakiś wybrany - jakieś directory
            if (comboBox_directories.Text == "") return;

            if (comboBox_extensions_filter.Text != "" && !comboBox_extensions_filter.Items.Contains(comboBox_extensions_filter.Text))
            {
                comboBox_extensions_filter.Items.Add(comboBox_extensions_filter.Text);
                change_focus();
                update_dir_filter_counter_and_dir_and_file_counters();
                add_extension_to_List_directories_with_extensions();

                comboBox_extensions_filter.Text = "";
            }
        }
        private void button_dir_extensions_delete_Click(object sender, EventArgs e)
        {
            change_focus();

            if (comboBox_extensions_filter.Text != "" && comboBox_extensions_filter.Items.Contains(comboBox_extensions_filter.Text))
            {
                delete_extension_from_List_directories_with_extensions();
                comboBox_extensions_filter.Items.Remove(comboBox_extensions_filter.Text);
                update_dir_filter_counter_and_dir_and_file_counters();
                if (comboBox_extensions_filter.Items.Count > 0) comboBox_extensions_filter.Text = comboBox_extensions_filter.Items[0].ToString();
            }

            if (comboBox_extensions_filter.Items.Count == 0) comboBox_extensions_filter.Text = "";
        }

        // METHODS directories extensions -> add or delete
        private void add_extension_to_List_directories_with_extensions()
        {   
            //Debug_reset("adding to right element");
            //Debug_add(comboBox_directories.SelectedItem.ToString());

            foreach (var dir in List_directory_with_filters)
            {
                //Debug_add(dir.directory_path);

                if (dir.directory_path == comboBox_directories.SelectedItem.ToString())
                {
                    dir.list_filters.Add(comboBox_extensions_filter.Text);
                }
            }
        }
        private void delete_extension_from_List_directories_with_extensions()
        {
            foreach (var dir in List_directory_with_filters)
            {
                if (dir.directory_path == comboBox_directories.SelectedItem.ToString())
                {
                    //Debug_reset("deleting wright element");
                    dir.list_filters.Remove(comboBox_extensions_filter.Text);
                }
            }
        }
        private void comboBox_extensions_filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            change_focus();
        }
        private void update_dir_filter_counter_and_dir_and_file_counters()
        {
            update_dir_and_path_counters();

            if (comboBox_directories.Items.Count == 0 || comboBox_directories.Text == "")
            {
                label_dir_filters_how_many.Text = "";
                return;
            }

            foreach (var dir in List_directory_with_filters)
            {
                if (dir.directory_path == comboBox_directories.SelectedItem.ToString())
                {
                    if (dir.pack_whole_dir_to_zip)
                    {
                        label_dir_filters_how_many.Text = "all files to zip";
                        return;
                    }
                    else
                    {


                        if (comboBox_extensions_filter.Items.Count == 0) label_dir_filters_how_many.Text = "loading all files";                        
                        if (comboBox_extensions_filter.Items.Count == 1) label_dir_filters_how_many.Text = comboBox_extensions_filter.Items.Count.ToString() + " active filter";
                        if (comboBox_extensions_filter.Items.Count > 1)  label_dir_filters_how_many.Text = comboBox_extensions_filter.Items.Count.ToString() + " active filters";
                    }
                }
            }            

            //if (comboBox_extensions_filter.Items.Count == 0) label_dir_filters_how_many.Text = "loading all files";
            //if (comboBox_extensions_filter.Items.Count == 1) label_dir_filters_how_many.Text = comboBox_extensions_filter.Items.Count.ToString() + " active filter";
            //if (comboBox_extensions_filter.Items.Count > 1)  label_dir_filters_how_many.Text = comboBox_extensions_filter.Items.Count.ToString() + " active filters";
        }
        private void update_dir_and_path_counters()
        {
            label_counter_dir.Text = "";
            label_counter_file_paths.Text = "";

            int dir_count = comboBox_directories.Items.Count;
            int path_count = comboBox_file_paths.Items.Count;

            if (dir_count > 0) label_counter_dir.Text = dir_count.ToString();
            if (path_count > 0) label_counter_file_paths.Text = path_count.ToString();
        }
        private void update_sending_profile_counter()
        {
            int count = comboBox_sending_profiles.Items.Count;

            if (count > 1 || count == 0) label_counter_sending_profiles.Text = count.ToString() + " sending profiles";
            else label_counter_sending_profiles.Text = count.ToString() + " sending profile";
        }


        // BUTTONS clear
        private void button_clear_directories_Click(object sender, EventArgs e)
        {
            List_directory_with_filters.Clear();
            comboBox_directories.Items.Clear();             comboBox_directories.Text = "";
            comboBox_extensions_filter.Items.Clear();       comboBox_extensions_filter.Text = "";
            label_dir_filters_how_many.Text = "";

            update_dir_filter_counter_and_dir_and_file_counters();
        }        
        private void button_combobox_file_paths_clear_Click(object sender, EventArgs e)
        {
            List_file_paths.Clear();
            comboBox_file_paths.Items.Clear();

            update_dir_filter_counter_and_dir_and_file_counters();
        }

        // CHECKBOX pack whole dir to zip
        private void checkBox_whole_dir_to_zip_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox_directories.Items.Count == 0) return;
            if (comboBox_directories.SelectedItem.ToString() == "") return;

            if (checkBox_whole_dir_to_zip.Checked == true)
            {
                comboBox_extensions_filter.Enabled = false;
                button_dir_extensions_add.Enabled = false;
                button_dir_extensions_delete.Enabled = false;
                label11.Font = new Font(label11.Font, FontStyle.Regular);

                foreach (var obj in List_directory_with_filters)
                {
                    if (comboBox_directories.Text == obj.directory_path)
                    {
                        obj.pack_whole_dir_to_zip = true;
                        update_dir_filter_counter_and_dir_and_file_counters();
                    }
                }
            }
            else
            {
                comboBox_extensions_filter.Enabled = true;
                button_dir_extensions_add.Enabled = true;
                button_dir_extensions_delete.Enabled = true;
                label11.Font = new Font(label11.Font, FontStyle.Bold);

                foreach (var obj in List_directory_with_filters)
                {
                    if (comboBox_directories.Text == obj.directory_path)
                    {
                        obj.pack_whole_dir_to_zip = false;
                        update_dir_filter_counter_and_dir_and_file_counters();
                    }
                }
            }
        }









        // Saving window location //
        private void App_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                Properties.Settings.Default.Form_Location = RestoreBounds.Location;
                Properties.Settings.Default.Form_Size = RestoreBounds.Size;
                Properties.Settings.Default.Form_Maximised = true;
                Properties.Settings.Default.Form_Minimised = false;
            }
            else if (WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.Form_Location = Location;
                Properties.Settings.Default.Form_Size = Size;
                Properties.Settings.Default.Form_Maximised = false;
                Properties.Settings.Default.Form_Minimised = false;
            }
            else
            {
                Properties.Settings.Default.Form_Location = RestoreBounds.Location;
                Properties.Settings.Default.Form_Size = RestoreBounds.Size;
                Properties.Settings.Default.Form_Maximised = false;
                Properties.Settings.Default.Form_Minimised = true;
            }
            Properties.Settings.Default.Save();
        }
        private void App_Load(object sender, EventArgs e)
        {
            //Properties.Settings.Default.Reset();

            if (Properties.Settings.Default.Form_Maximised)
            {
                Location = Properties.Settings.Default.Form_Location;
                WindowState = FormWindowState.Maximized;
                Size = Properties.Settings.Default.Form_Size;
            }
            else if (Properties.Settings.Default.Form_Minimised)
            {
                Location = Properties.Settings.Default.Form_Location;
                WindowState = FormWindowState.Minimized;
                Size = Properties.Settings.Default.Form_Size;
            }
            else
            {
                Location = Properties.Settings.Default.Form_Location;
                Size = Properties.Settings.Default.Form_Size;
            }
        }        
    }
}