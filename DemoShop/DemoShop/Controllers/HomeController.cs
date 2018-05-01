using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoShop.Repository;
using DemoShop.Models;
using System.Diagnostics;

namespace DemoShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IPropertyRepository _propertyRepository;

        public HomeController()
        {
            _categoryRepository = new CategoryRepository("ShopDBConnect");

            _propertyRepository = new PropertyRepository("dinhgianhadat.vn");
        }
        public ActionResult Index()
        {
            Session.Remove("listpage");
            //var model = _categoryRepository.GetCategory();

            //var category = _categoryRepository.GetCategoryById(3);

            //var model2 = _categoryRepository.GetCategoryManage(5,3);

            //var parent = _categoryRepository.GetCategoryParentById(2); ViewBag.parent = parent;

            //var model3 = _categoryRepository.GetAllCategoryParent();

            //var parent = _categoryRepository.GetCategoryParentByIdV2(2); ViewBag.parent = parent;

            //var allparent = _categoryRepository.GetAllCategoryParentV2(); ViewBag.allParent = allparent;

            //var categoryPage = _categoryRepository.GetCategoryPage(2, 2); ViewBag.categoryPage = categoryPage;

            return View();
        }
        public ActionResult Pagelist(int pageindex)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int pagesize = 20;

            var categoryPage = _categoryRepository.GetCategoryPage(pagesize, pageindex);

            if (categoryPage.TotalCount % pagesize == 0)
            {
                ViewBag.totalpage = categoryPage.TotalCount / pagesize;
            }
            else
            {
                ViewBag.totalpage = (categoryPage.TotalCount / pagesize) + 1;
            }

            stopwatch.Stop();
            ViewBag.stopwatch = stopwatch.Elapsed.ToString();

            return View(categoryPage);
        }

        

        public ActionResult Pagelist2(int pageindex)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int pagesize = 20;

            var categoryPage = _categoryRepository.GetCategoryPage3(pagesize, pageindex);

            if (categoryPage.TotalCount % pagesize == 0)
            {
                ViewBag.totalpage = categoryPage.TotalCount / pagesize;
            }
            else
            {
                ViewBag.totalpage = (categoryPage.TotalCount / pagesize) + 1;
            }

            stopwatch.Stop();
            ViewBag.stopwatch = stopwatch.Elapsed.ToString();

            return View(categoryPage);
        }

        public ActionResult PropertyList(int pageindex)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int pagesize = 20;
            var model = _propertyRepository.GetPropertySearch(pagesize, pageindex);

            if (model.TotalCount % pagesize == 0)
            {
                ViewBag.totalpage = model.TotalCount / pagesize;
            }
            else
            {
                ViewBag.totalpage = (model.TotalCount / pagesize) + 1;
            }

            stopwatch.Stop();
            ViewBag.stopwatch = stopwatch.Elapsed.ToString();
            return View(model);
        }

        public ActionResult PropertyList2(int pageindex)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int pagesize = 20;
            var model = _propertyRepository.GetPropertySearch2(pagesize, pageindex);

            if (model.TotalCount % pagesize == 0)
            {
                ViewBag.totalpage = model.TotalCount / pagesize;
            }
            else
            {
                ViewBag.totalpage = (model.TotalCount / pagesize) + 1;
            }

            stopwatch.Stop();
            ViewBag.stopwatch = stopwatch.Elapsed.ToString();
            return View(model);
        }

    }
}