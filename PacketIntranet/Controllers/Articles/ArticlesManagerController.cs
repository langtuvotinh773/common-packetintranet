using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PacketIntranet.Models;
using PacketIntranet.Models.ArticleModel;
using PacketIntranet.Helpers;
using System.Configuration;

namespace PacketIntranet.Controllers.Articles
{ 
    public class ArticlesManagerController : Controller
    {
        private MvcApplicationDBEntities db = new MvcApplicationDBEntities();

        //
        // GET: /ArticlesManager/

        public ViewResult Index()
        {
            var listCategory = db.tbl_Articles.Where(a => a.Type == 1);
            ArticleModel model = new ArticleModel()
            {
                List_tbl_Articles = db.tbl_Articles.ToList(),
                list_Categorys = listCategory.ToList()
            };
            return View(model);
        }

        //
        // GET: /ArticlesManager/Details/5

        public ViewResult Details(int id)
        {
            tbl_Articles tbl_articles = db.tbl_Articles.Single(t => t.ArticleId == id);
            return View(tbl_articles);
        }

        //
        // GET: /ArticlesManager/Create

        public ActionResult Create()
        {
            var listCategory = db.tbl_Articles.Where(a => a.Type == 1);
            ArticleModel model = new ArticleModel()
            {
                List_tbl_Articles = db.tbl_Articles.ToList(),
                list_Categorys = db.tbl_Articles.Where(a => a.Type == 1).ToList(),
                one_tbl_Articles = new tbl_Articles()
            };
            return View(model);
        } 

        //
        // POST: /ArticlesManager/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ArticleModel tbl_articles, FormCollection Collection)
        {

            //if (ModelState.IsValid)
            //{
                int CheckInsert = Common.ParseInt(Collection.Get("Check_Insert_PrimaryKey"));
                if (CheckInsert == 1)
                {
                    //tbl_Products.one_tbl_Product.ProType_Id = Common.ParseInt(Collection.Get("one_tbl_ProductType.ProType_Id"));
                    //tbl_Products.one_tbl_Product.Ori_Id = Common.ParseInt(Collection.Get("one_tbl_Origin.Ori_Id"));
                    //tbl_Products.one_tbl_Product.Unit_Id = Common.ParseInt(Collection.Get("one_tbl_Unit.Unit_Id"));
                   // tbl_articles.one_tbl_Articles.
                }
                tbl_articles.one_tbl_Articles.PostDate = DateTime.Now;
                tbl_articles.one_tbl_Articles.Type = Common.ParseInt(Collection.Get("Type"));
                
                db.tbl_Articles.AddObject(tbl_articles.one_tbl_Articles);
                db.SaveChanges();

                if (Request.Files.Count > 0)
                {
                    for (int item = 0; item < Request.Files.Count; item++)
                    {
                        try
                        {
                            string strImageName = "", strImageType = "";
                            // đếm số lượng file 
                            var imageFile = Request.Files[item];
                            strImageName = imageFile.FileName;
                            strImageType = strImageName.Substring(strImageName.LastIndexOf("."));
                            string pathStoredImage = ConfigurationManager.ConnectionStrings["pathImg_Article"].ToString(); // đường dẫn hình cần lưu
                            var updateFeatureImage = db.tbl_Articles.Single(a => a.ArticleId == tbl_articles.one_tbl_Articles.ArticleId).ArticleId;
                            string fileName = Common.ParseString(updateFeatureImage) + strImageType;

                            Common.UploadFile(imageFile, pathStoredImage, fileName);
                        }
                        catch
                        {
                        }
                    }
                }
                return RedirectToAction("Index");
               
            //}
            //return View(tbl_articles);
        }
        
        //
        // GET: /ArticlesManager/Edit/5
 
        public ActionResult Edit(int id)
        {
            tbl_Articles tbl_articles = db.tbl_Articles.Single(t => t.ArticleId == id);
            return View(tbl_articles);
        }

        //
        // POST: /ArticlesManager/Edit/5

        [HttpPost]
        public ActionResult Edit(tbl_Articles tbl_articles)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Articles.Attach(tbl_articles);
                db.ObjectStateManager.ChangeObjectState(tbl_articles, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_articles);
        }

        //
        // GET: /ArticlesManager/Delete/5
 
        public ActionResult Delete(int id)
        {
            tbl_Articles tbl_articles = db.tbl_Articles.Single(t => t.ArticleId == id);
            return View(tbl_articles);
        }

        //
        // POST: /ArticlesManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            tbl_Articles tbl_articles = db.tbl_Articles.Single(t => t.ArticleId == id);
            db.tbl_Articles.DeleteObject(tbl_articles);
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