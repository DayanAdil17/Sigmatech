using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sigmatech.Entities.Base;

namespace src.Entities.User
{
    public class UserEntity : BaseEntity
    {
        public string userName { get; set; }
        public string userRole { get; set; }
        public string password { get; set; }
    }
}