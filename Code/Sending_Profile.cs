using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender_Man
{
    public class Sending_Profile
    {
        // wszystkie pola do zapisania

        public string Sending_Profile_Name;
        
        
        public List<Directory_filters> Ser_List_of_Dirs;
        public List<string> Ser_List_of_File_Paths;


        public string Ser_Tytle;        
        public string Ser_Txt_converted_name;
        public string Ser_Zip_name;


        public bool Ser_Add_Hour_Bool;
        public bool Ser_Add_Date_Bool;
        public bool Ser_Load_files_and_dirs;
        public bool Ser_Close_after_sending;
        public bool Ser_Pack_to_zip;
    }
}
