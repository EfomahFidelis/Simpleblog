using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleBlog.Infrastructure;
using SimpleBlog.Models;
using System.ComponentModel.DataAnnotations;



namespace SimpleBlog.Areas.Admin.ViewModels
{


    /// <summary>
    /// 
    /// </summary>
    public class TagCheckbox
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public class PostsIndex
    {
        public PagedData<Post> Posts { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public class PostsForm
    {
        public bool IsNew { get; set; }
        public int? PostId { get; set; }

        [Required, StringLength(128)]
        public string Title { get; set; }

        [Required, StringLength(128)]
        public string Slug { get; set; }

        [Required, DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public IList<TagCheckbox> Tags { get; set; }
    }
}