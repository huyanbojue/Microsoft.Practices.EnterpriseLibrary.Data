using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoShop.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Content { get; set; }

        public int ParentId { get; set; }

    }

    public class CategoryManage
    {
        public List<Category> ListCategory { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }

    public class CategoryParent
    {
        public List<Category> ListCategory { get; set; }
        public Category Parent { get; set; }

    }

    public class CategoryParentById
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public List<Category> ListCategory { get; set; }

    }

    public class CategoryPage
    {
        public int TotalCount { get; set; }

        public List<Category> ListCategory { get; set; }

    }

    public class CategoryPage2
    {
        
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }
        public List<Category> ListCategory { get; set; }

    }
}