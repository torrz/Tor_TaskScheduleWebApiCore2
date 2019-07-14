using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tor_TaskScheduleWebApiCore2.Classes
{
    [Serializable]
    public class Account
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string Phone { get; set; }
        public short? Role { get; set; }
        public DateTime Update_time { get; set; }
        public DateTime Created_time { get; set; }

    }
}
