using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender_Man
{
    public class One_Big_Serializer
    {
        public string last_used_profile;
        public List<string> sending_to;
        public List<string> sending_from;
        public List<Sending_Profile> all_profiles;

        public One_Big_Serializer() { }
    }
}
