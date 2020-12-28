using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QnA.Models;

namespace QnA.ViewModels
{
    public struct UserType
    {
        public string Value { get;  set; }
        public string Name { get; set; }
    }

    public class UserViewModel
    {
        public User user { get; set; }
        public List<User> userList { get; set; }
        public String type { get; set; }
        public List<UserType> userTypes = new List<UserType>
            {
                new UserType { Value = "Admin" , Name="Admin" },
                new UserType { Value = "User" , Name="User" },
            };
    }
}