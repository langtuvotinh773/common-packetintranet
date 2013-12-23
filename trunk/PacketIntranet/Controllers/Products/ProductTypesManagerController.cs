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
    public class ProductTypesManagerController : Controller
    {
        private MvcApplicationDBEntities db = new MvcApplicationDBEntities();

        //
        // GET: /ProductTypesManager/

        public ViewResult Index()
        {
            ProductTypeModel model = new ProductTypeModel()
            {
                List_tbl_ProductType = db.tbl_ProductTypes.ToList(),
            };
            return View(model);
        }

        //
        // GET: /ProductTypesManager/Details/5

        public ViewResult Details(int id)
        {
            tbl_ProductTypes tbl_producttypes = db.tbl_ProductTypes.Single(t => t.ProType_Id == id);
            return View(tbl_producttypes);
        }

        //
        // GET: /ProductTypesManager/Create

        public ActionResult Create()
        {
           
            ProductTypeModel model = new ProductTypeModel()
            {
                One_tbl_ProductType = new tbl_ProductTypes()
            };
            return View(model);
        } 

        //
        // POST: /ProductTypesManager/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ProductTypeModel tbl_producttypes)
        {
            if (ModelState.IsValid)
            {
                db.tbl_ProductTypes.AddObject(tbl_producttypes.One_tbl_ProductType);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(tbl_producttypes);
        }
        
        //
        // GET: /ProductTypesManager/Edit/5
 
        public ActionResult Edit(int id)
        {
            ProductTypeModel model = new ProductTypeModel()
            {
                One_tbl_ProductType = db.tbl_ProductTypes.Single(t => t.ProType_Id == id),
            };
           // tbl_ProductTypes tbl_producttypes = db.tbl_ProductTypes.Single(t => t.ProType_Id == id);
            return View(model);
        }

        //
        // POST: /ProductTypesManager/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProductTypeModel tbl_producttypes, int id)
        {
            if (ModelState.IsValid)
            {
                var Update_ProductType = db.tbl_ProductTypes.Single(a => a.ProType_Id == id);
                Update_ProductType.ProType_Name = tbl_producttypes.One_tbl_ProductType.ProType_Name;
                Update_ProductType.Descriptions = tbl_producttypes.One_tbl_ProductType.Descriptions;

                UpdateModel(Update_ProductType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_producttypes);
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
                    tbl_ProductTypes tbl_producttypes = db.tbl_ProductTypes.Single(t => t.ProType_Id == ConvertId);
                    db.tbl_ProductTypes.DeleteObject(tbl_producttypes);
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