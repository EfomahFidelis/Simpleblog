using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleBlog.Models;
using System.ComponentModel.DataAnnotations;

namespace SimpleBlog.Areas.Admin.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleCheckbox
    {
        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>   
    public class UsersIndex
    {
        public IEnumerable<User> Users { get; set; }
    }


    /// <summary>
    /// DTO
    /// </summary>
    public class UsersNew
    {
        public IList<RoleCheckbox> Roles { get; set; }

        [Required, StringLength(128)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, StringLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public class UsersEdit
    {
        public IList<RoleCheckbox> Roles { get; set; }

        [Required, StringLength(128)]
        public string Username { get; set; }

        [Required, StringLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public class UsersResetPassword
    {
        // No data annotation because this data isn't modified
        // it is just used to send data from the controller to the view
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}