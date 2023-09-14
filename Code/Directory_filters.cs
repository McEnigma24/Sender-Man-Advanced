using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender_Man
{
    public class Directory_filters
    {
        public string directory_path;
        public List<string> list_filters;
        public bool pack_whole_dir_to_zip;

        public Directory_filters()
        {
        }

        public Directory_filters(string dir_path)
        {
            directory_path = dir_path;
            list_filters = new List<string>();
        }

/*
        public void addFilter(string filter)
        {            
            list_filters.Add(filter);
        }

        public void removeFilter(string filter)
        {
            if(list_filters.Contains(filter))   list_filters.Remove(filter);
        }*/
    }
}
