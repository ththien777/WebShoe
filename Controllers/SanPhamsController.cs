using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebShoe.Models;

namespace WebShoe.Controllers
{
    public class SanPhamsController : Controller
    {
        private CT25Team25Entities db = new CT25Team25Entities();
        private const string CartSession = "cart";
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var links = (from l in db.SanPhams
                         select l).OrderBy(x => x.MaSP);
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(links.ToPagedList(pageNumber, pageSize));

        }
        [HttpPost]
        // GET: SanPhams
        public ActionResult Search(string keyword, int? page)
        {
            db = new CT25Team25Entities();
            ViewBag.keyword = keyword;
            var searchSP = db.SanPhams.Where(x => x.TenSP.ToLower().Contains(keyword.ToLower()) ||
            x.MaSP.ToString().Contains(keyword.ToString()) || keyword == null).ToList().ToPagedList(page ?? 1, 6);
            if (searchSP.Count() == 0)
            {
                return View("Search_NotFound");
            }
            return View(searchSP);
        }
        public ActionResult Search_NotFound()
        {
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }
    }
}