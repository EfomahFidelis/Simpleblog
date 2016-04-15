using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleBlog.Models;
using System.ComponentModel.DataAnnotations;

namespace SimpleBlog.Areas.Admin.ViewModels
{
    public class UsersIndex
    {
        public IEnumerable<User> Users { get; set; }
    }

    public class UsersNew
    {
        [Required, StringLength(128)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, StringLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class UsersEdit
    {
        [Required, StringLength(128)]
        public string Username { get; set; }

        [Required, StringLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }


    public class UsersResetPassword
    {
        // No data annotation because this data isn't modified
        // it is just used to send data from the controller to the view
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}