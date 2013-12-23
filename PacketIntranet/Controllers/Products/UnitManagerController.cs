using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PacketIntranet.Models;
using PacketIntranet.Models.ProductModel;

namespace PacketIntranet.Controllers
{ 
    public class UnitManagerController : Controller
    {
        private MvcApplicationDBEntities db = new MvcApplicationDBEntities();

        //
        // GET: /ProductTypesManager/

        public ViewResult Index()
        {
            UnitModel model = new UnitModel()
            {
                List_tbl_Units = db.tbl_Units.ToList(),
            };
            return View(model);
        }

        //
        // GET: /ProductTypesManager/Details/5

        public ViewResult Details(int id)
        {
            tbl_Units tbl_Units = db.tbl_Units.Single(t => t.Unit_Id == id);
            return View(tbl_Units);
        }

        //
        // GET: /ProductTypesManager/Create

        public ActionResult Create()
        {

            UnitModel model = new UnitModel()
            {
                One_tbl_Unit = new tbl_Units()
            };
            return View(model);
        }

        //
        // POST: /ProductTypesManager/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(UnitModel tbl_Units)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Units.AddObject(tbl_Units.One_tbl_Unit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Units);
        }

        //
        // GET: /ProductTypesManager/Edit/5

        public ActionResult Edit(int id)
        {
            UnitModel model = new UnitModel()
            {
                One_tbl_Unit = db.tbl_Units.Single(t => t.Unit_Id == id),
            };
            // tbl_Units tbl_Units = db.tbl_Units.Single(t => t.Unit_Id == id);
            return View(model);
        }

        //
        // POST: /ProductTypesManager/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(UnitModel tbl_Units, int id)
        {
            if (ModelState.IsValid)
            {
                var Update_Unit = db.tbl_Units.Single(a => a.Unit_Id == id);
                Update_Unit.Unit_Name = tbl_Units.One_tbl_Unit.Unit_Name;
                Update_Unit.Description = tbl_Units.One_tbl_Unit.Description;

                UpdateModel(Update_Unit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Units);
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
                    tbl_Units tbl_Units = db.tbl_Units.Single(t => t.Unit_Id == ConvertId);
                    db.tbl_Units.DeleteObject(tbl_Units);
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