using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wysylanie_na_maila
{
    class Config_info
    {
        string name;
        string data;

        public Config_info(string n, string d)
        {
            if(n != "" && d != "")
            {
                name = n;
                data = d;
            }
            else
            {
                name = "";
                data = "";
            }
        }
        public Config_info()
        {
            name = "";
            data = "";           
        }


        public string Name() { return name; }
        public void Name(string new_name) { name = new_name; }

        public string Data() { return data; }
        public void Data(string new_data) { data = new_data; }
    }
}
