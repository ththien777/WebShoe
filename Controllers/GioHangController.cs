using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebShoe.Models;

namespace WebShoe.Controllers
{
    public class GioHangController : Controller
    {
        CT25Team25Entities db = new CT25Team25Entities();
        private List<ChiTietDonHang> cart = null;
        public GioHangController()
        {
            var session = System.Web.HttpContext.Current.Session;
            if (session["cart"] != null)
                cart = session["cart"] as List<ChiTietDonHang>;
            else
            {
                cart = new List<ChiTietDonHang>();
                session["cart"] = cart;
            }
        }
        // GET: GioHang
        public ActionResult Index()
        {
            var hashtable = new Hashtable();
            foreach (var CTDH in cart)
            {
                if (hashtable[CTDH.SanPham_ID] !=null)
                {
                    (hashtable[CTDH.SanPham_ID] as ChiTietDonHang).SoLuong += CTDH.SoLuong;
                }
                else
                {
                    hashtable[CTDH.SanPham_ID] = CTDH;
                }
            }
            cart.Clear();
            foreach (ChiTietDonHang CTDH in hashtable.Values)
            {
                cart.Add(CTDH);
            }
            ViewBag.Success = "Không có sản phẩm trong giỏ hàng";
            return View(cart);
        }
        [HttpPost]
        public ActionResult AddToCart(int sanPhamid, int soluong=1)
        {
            var sanpham = db.SanPhams.Find(sanPhamid);
            cart.Add(new ChiTietDonHang
            {
                SanPham_ID = sanPhamid,
                SoLuong = soluong,
            });
            return RedirectToAction("Index");
        }
        public ActionResult UpdateCart(int sanPhamid, int soluong)
        {
            cart = (List<ChiTietDonHang>)Session["cart"];
            var product = cart.Find(p => p.SanPham_ID == sanPhamid);
            if (product != null)
            {
                product. SoLuong = soluong;
            }
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
        public ActionResult xoaSP(int id)
        {
            cart = (List<ChiTietDonHang>)Session["cart"];
            var sanpham = cart.Find(p => p.SanPham_ID == id);
            if (sanpham != null)
            {
                cart.Remove(sanpham);
            }
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
        public ActionResult checkOut()
        {      
                return View();
        }

    }
}

