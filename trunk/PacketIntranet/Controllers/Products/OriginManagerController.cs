using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PacketIntranet.Models;
using PacketIntranet.Models.ProductModel;

namespace PacketIntranet.Controllers.Products
{ 
    public class OriginManagerController : Controller
    {
        private MvcApplicationDBEntities db = new MvcApplicationDBEntities();

        //
        // GET: /ProductTypesManager/

        public ViewResult Index()
        {
            OriginModel model = new OriginModel()
            {
                List_tbl_Origins = db.tbl_Origins.ToList(),
            };
            return View(model);
        }

        //
        // GET: /ProductTypesManager/Details/5

        public ViewResult Details(int id)
        {
            tbl_Origins tbl_Origins = db.tbl_Origins.Single(t => t.Ori_Id == id);
            return View(tbl_Origins);
        }

        //
        // GET: /ProductTypesManager/Create

        public ActionResult Create()
        {

            OriginModel model = new OriginModel()
            {
                One_tbl_Origins = new tbl_Origins()
            };
            return View(model);
        }

        //
        // POST: /ProductTypesManager/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(OriginModel tbl_Origins)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Origins.AddObject(tbl_Origins.One_tbl_Origins);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Origins);
        }

        //
        // GET: /ProductTypesManager/Edit/5

        public ActionResult Edit(int id)
        {
            OriginModel model = new OriginModel()
            {
                One_tbl_Origins = db.tbl_Origins.Single(t => t.Ori_Id == id),
            };
            // tbl_Origins tbl_Origins = db.tbl_Origins.Single(t => t.Ori_Id == id);
            return View(model);
        }

        //
        // POST: /ProductTypesManager/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(OriginModel tbl_Origins, int id)
        {
            if (ModelState.IsValid)
            {
                var Update_Origin = db.tbl_Origins.Single(a => a.Ori_Id == id);
                Update_Origin.Ori_Conutry = tbl_Origins.One_tbl_Origins.Ori_Conutry;
                Update_Origin.Descriptions = tbl_Origins.One_tbl_Origins.Descriptions;

                UpdateModel(Update_Origin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Origins);
        }



        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(FormCollection Collection)
        {
            string All_IdProductType = Collection.Get("GetAll_Id");
            foreach (var item in All_IdProductType.Split(','))
            {

                if (item.ToString() != "000")
                {
                    int ConvertId = Convert.ToInt32(item);
                    tbl_Origins tbl_Origins = db.tbl_Origins.Single(t => t.Ori_Id == ConvertId);
                    db.tbl_Origins.DeleteObject(tbl_Origins);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}