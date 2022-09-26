using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using DataTypes.ModelDataTypes;

namespace DataTypes.ModelDataTypes
{
    public class UserProfile 
    {       
         
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }       
        public string Password { get; set; }       
        public string UserStatus { get; set; }
        public Guid UserID { get; set; }
        public string UserWebToken { get; set; }

    }
}
