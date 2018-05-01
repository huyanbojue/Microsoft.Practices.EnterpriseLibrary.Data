
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoShop.Models;
using System.Data;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;
using DemoShop.DataAccess;

using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DemoShop.Repository
{
    public interface ICategoryRepository
    {
        List<Category> GetCategory();

        Category GetCategoryById(int id);

        CategoryManage GetCategoryManage(int pageSize, int pageIndex);

        CategoryParent GetCategoryParentById(int id);

        List<CategoryParent> GetAllCategoryParent();

        CategoryParentById GetCategoryParentByIdV2(int id);

        List<CategoryParentById> GetAllCategoryParentV2();

        CategoryPage GetCategoryPage(int pagesize, int pageindex);
        List<CategoryPage2> GetCategoryPage2(int size);
        CategoryPage GetCategoryPage3(int pageSize, int pageIndex);

        void AddCategory(Category category);
    }
    public class CategoryRepository : BaseCmdExec, ICategoryRepository
    {
        public CategoryRepository(string strConnName) : base("Data Source=.;Initial Catalog=ShopDB;Integrated Security=True;MultipleActiveResultSets=True;")
        {

        }
        public List<Category> GetCategory()
        {
            object[] param = new object[] { };
            return base.ExecStoredProc<Category>("SP_GetAllCategory", param).ToList();
        }

        public Category GetCategoryById(int id)
        {
            object[] param = new object[] { id };
            return base.ExecStoredProc<Category>("SP_GetCategoryById", param).SingleOrDefault();
        }

        public CategoryManage GetCategoryManage(int pageSize, int pageIndex)
        {
            var model = new CategoryManage();

            model.PageSize = pageSize;
            model.PageIndex = pageIndex;

            object[] param = new object[] { };
            model.ListCategory = base.ExecStoredProc<Category>("SP_GetAllCategory", param).Take(pageSize).ToList();

            model.TotalCount = model.ListCategory.Count();

            return model;
        }

        public CategoryParent GetCategoryParentById(int id)
        {
            object[] param = new object[] { id };

            var rowMapper = MapBuilder<CategoryParent>.MapAllProperties()
                                .Map(r => r.Parent)
                                .WithFunc(n=> XMLToObject<Category>(n, "Category"))
                                .Map(r =>r.ListCategory)
                                .WithFunc(n => XMLToObject<List<Category>>(n, "ListCategory"))
                                .Build();

            return base.ExecStoredProc<CategoryParent>("SP_GetCategoryParentById", param , rowMapper).SingleOrDefault(); 
        }

        public List<CategoryParent> GetAllCategoryParent()
        {
            var model = new List<CategoryParent>();

            var paren = new CategoryParent();

            foreach(var item in GetCategory())
            {
                paren = GetCategoryParentById(item.Id);
                model.Add(paren);
            }

            return model;
        }

        public CategoryParentById GetCategoryParentByIdV2(int id)
        {
            object[] param = new object[] { id };

            var rowMapper = MapBuilder<CategoryParentById>.MapAllProperties()
                                .Map(r => r.ListCategory)
                                .WithFunc(n => XMLToObject<List<Category>>(n, "ListCategory"))
                                .Build();

            return base.ExecStoredProc<CategoryParentById>("SP_GetCategoryParentByIdV2", param, rowMapper).SingleOrDefault(); 
        }

        public List<CategoryParentById> GetAllCategoryParentV2()
        {
            object[] param = new object[] { };

            var rowMapper = GenerateRowMapper<CategoryParentById,List<Category>>("ListCategory", base.GetPropertyInfo<CategoryParentById>(x => x.ListCategory));

            //var rowMapper = MapBuilder<CategoryParentById>.MapAllProperties()
            //                    .Map(r => r.ListCategory)
            //                    .WithFunc(n => XMLToObject<List<Category>>(n, "ListCategory"))
            //                    .Build();

            return base.ExecStoredProc<CategoryParentById>("SP_GetAllCategoryParentV2", param, rowMapper).ToList();
        }

        public CategoryPage GetCategoryPage(int pagesize , int pageindex)
        {
            object[] param = new object[] { pagesize, pageindex };

            var rowMapper = GenerateRowMapper<CategoryPage, 
                List<Category>>("ListCategory", base.GetPropertyInfo<CategoryPage>(x => x.ListCategory));

            return base.ExecStoredProc<CategoryPage>("SP_GetPageList", param, rowMapper).SingleOrDefault();
        }

        public List<CategoryPage2> GetCategoryPage2(int size)
        {
            object[] param = new object[] { size };

            var rowMapper = GenerateRowMapper<CategoryPage2, List<Category>>("ListCategory", base.GetPropertyInfo<CategoryPage2>(x => x.ListCategory));

            return base.ExecStoredProc<CategoryPage2>("SP_GetCategoryPageList", param, rowMapper).ToList();
        }

        
        public void AddCategory(Category category)
        {
            object[] param = new object[] { category.Name, category.Alias,category.Content, category.ParentId };
            base.ExecStoredNonQuery<Category>("SP_InsertCategory", param);
        }

        public CategoryPage GetCategoryPage3(int pageSize, int pageIndex)
        {
            object[] param = new object[] { pageSize, pageIndex };

            var rowMapper = GenerateRowMapper<CategoryPage,
                List<Category>>("ListCategory", base.GetPropertyInfo<CategoryPage>(x => x.ListCategory));

            return base.ExecStoredProc<CategoryPage>("SP_GetPageList3", param, rowMapper).SingleOrDefault();
        }
    }
}