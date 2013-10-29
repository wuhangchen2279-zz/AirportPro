using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ServiceLibrary
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int UserId { set; get; }

        [DataMember]
        public string UserName { set; get; }

        [DataMember]
        public string Role { set; get; }

        [DataMember]
        public string Password { set; get; }

        [DataMember]
        public string Email { set; get; }
    }
}