using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PacketIntranet.Models;
using PacketIntranet.Models.ProductModel;
using PacketIntranet.Helpers;
using System.Configuration;
using System.IO;

namespace PacketIntranet.Controllers.Products
{ 
    public class ProductManagerController : Controller
    {
        private MvcApplicationDBEntities db = new MvcApplicationDBEntities();

        //
        // GET: /ProductManager/

        public ViewResult Index()
        {
            ProductsModel model = new ProductsModel()
            {
                list_tbl_Product = db.tbl_Products.OrderByDescending(a=>a.Pro_Id).ToList(),
                list_tbl_Origin = db.tbl_Origins.ToList(),
                list_tbl_ProductType = db.tbl_ProductTypes.ToList(),
                list_tbl_Unit = db.tbl_Units.ToList()
            };
            return View(model);
        }

        //
        // GET: /ProductManager/Details/5

        public ViewResult Details(int id)
        {
            tbl_Products tbl_products = db.tbl_Products.Single(t => t.Pro_Id == id);
            return View(tbl_products);
        }

        public ActionResult Create()
        {
            string pathImage = ConfigurationManager.ConnectionStrings["pathImg_Product"].ToString(); // đường dẫn hình cần lưu
            ProductsModel model = new ProductsModel()
            {
                one_tbl_Product = new tbl_Products(),
                getPath = pathImage,
                list_tbl_Origin = db.tbl_Origins.ToList(),
                list_tbl_ProductType = db.tbl_ProductTypes.ToList(),
                list_tbl_Unit = db.tbl_Units.ToList()
            };
            return View(model);
        }

        //
        // POST: /ProductTypesManager/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ProductsModel tbl_Products, FormCollection Collection)
        {
            if (ModelState.IsValid)
            {
                int CheckInsert =Common.ParseInt(Collection.Get("Check_Insert_PrimaryKey"));
                if (CheckInsert == 1)
                {
                    tbl_Products.one_tbl_Product.ProType_Id = Common.ParseInt(Collection.Get("one_tbl_ProductType.ProType_Id"));
                    tbl_Products.one_tbl_Product.Ori_Id = Common.ParseInt(Collection.Get("one_tbl_Origin.Ori_Id"));
                    tbl_Products.one_tbl_Product.Unit_Id = Common.ParseInt(Collection.Get("one_tbl_Unit.Unit_Id"));
                }
                db.tbl_Products.AddObject(tbl_Products.one_tbl_Product);
                db.SaveChanges();


                for (int item = 0; item < Request.Files.Count; item++)
                {
                    try
                    {
                        string strImageName = "", strImageType = "";
                        // đếm số lượng file 
                        var imageFile = Request.Files[item];
                        strImageName = imageFile.FileName;
                        strImageType = strImageName.Substring(strImageName.LastIndexOf("."));
                        string NameFile = Convert.ToString(DateTime.Now.ToShortDateString()).Replace("/","-") + strImageType;
                        string pathStoredImage = ConfigurationManager.ConnectionStrings["pathImg_Product"].ToString(); // đường dẫn hình cần lưu

                        tbl_files NewFile = new tbl_files();
                        NewFile.Pro_Id = tbl_Products.one_tbl_Product.Pro_Id;
                        db.tbl_files.AddObject(NewFile);
                        db.SaveChanges();

                        var UpdateFile = db.tbl_files.Single(a => a.Fil_Id == NewFile.Fil_Id);
                        UpdateFile.Fil_Name = UpdateFile.Fil_Name + NewFile.Fil_Id + strImageType;
                        UpdateModel(UpdateFile);
                        db.SaveChanges();
                        Common.UploadFile(imageFile, pathStoredImage, UpdateFile.Fil_Name);

                       




                    }
                    catch
                    {
                        throw;
                    }
                }









                return RedirectToAction("Index");
            }

            return View(tbl_Products);
        }

        //
        // GET: /ProductTypesManager/Edit/5

        public ActionResult Edit(int id)
        {
               string pathImage = ConfigurationManager.ConnectionStrings["pathImg_Product"].ToString(); // đường dẫn hình cần lưu
            ProductsModel model = new ProductsModel()
            {
                getPath = pathImage,
                one_tbl_Product = db.tbl_Products.Single(t => t.Pro_Id == id),
                list_tbl_files = db.tbl_files.Where(a=>a.Pro_Id == id).ToList(),
                list_tbl_Origin = db.tbl_Origins.ToList(),
                list_tbl_ProductType = db.tbl_ProductTypes.ToList(),
                list_tbl_Unit = db.tbl_Units.ToList()
            };
            // tbl_Products tbl_Products = db.tbl_Products.Single(t => t.Ori_Id == id);
            return View(model);
        }

        //
        // POST: /ProductTypesManager/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProductsModel tbl_Products, int id, FormCollection Collection)
        {
            if (ModelState.IsValid)
            {
                var Update_Products = db.tbl_Products.Single(a => a.Pro_Id == id);
                Update_Products.ProType_Id = tbl_Products.one_tbl_Product.ProType_Id;
                Update_Products.Ori_Id = tbl_Products.one_tbl_Product.Ori_Id;
                Update_Products.Unit_Id = tbl_Products.one_tbl_Product.Unit_Id;
                Update_Products.Pro_Name = tbl_Products.one_tbl_Product.Pro_Name;
                Update_Products.Pro_Images = tbl_Products.one_tbl_Product.Pro_Images;
                Update_Products.Pro_Price = tbl_Products.one_tbl_Product.Pro_Price;
                Update_Products.Pro_Warranty = tbl_Products.one_tbl_Product.Pro_Warranty;
                Update_Products.Descriptions = tbl_Products.one_tbl_Product.Descriptions;

                UpdateModel(Update_Products);
                db.SaveChanges();



                /////////////////////////////////////////////////////////////////////////////////////////////

                var GroupFile = db.tbl_files.Where(a => a.Pro_Id == id);

                var Deletename = Collection.Get("DelectImageProduct");

                ////// Delete Image ////////////////////////////////////////////////////
                if (Deletename != null)
                {
                    foreach (var iteem in Deletename.Split(','))
                    {
                        string Name = "";
                        Name = iteem;
                        DeleteImage(id, Name);

                    }
                }
                //kiểm tra số phần tử dc ADD hình vào thêm
                int NumberUpload = 0;
                foreach (string itemAddImage in Request.Files.AllKeys)
                {

                    if (itemAddImage == "UploadImage[]")
                    {
                        NumberUpload = NumberUpload + 1;
                    }
                }
                //// dem số file vừa add vào ////////////////
                for (int i = 0; i < NumberUpload; i++)
                {
                    #region
                    var file = Request.Files[i];
                    string strFileName = Path.GetFileName(file.FileName);
                    if (strFileName != "")
                    {

                        tbl_files tbl_file = new tbl_files();
                        tbl_file.Pro_Id = id;
                        db.tbl_files.AddObject(tbl_file);
                        db.SaveChanges();

                        string strImageName = "", strImageType = "";
                        var imageFile = Request.Files[i];
                        strImageName = imageFile.FileName;
                        strImageType = strImageName.Substring(strImageName.LastIndexOf("."));
                        string pathStoredImage = ConfigurationManager.ConnectionStrings["pathImg_Product"].ToString();

                        var UpdateNewFile = db.tbl_files.Single(a => a.Fil_Id == tbl_file.Fil_Id);
                        UpdateNewFile.Fil_Name = UpdateNewFile.Fil_Id + strImageType;
                        UpdateModel(UpdateNewFile);
                        db.SaveChanges();
                        Common.UploadFile(imageFile, pathStoredImage, UpdateNewFile.Fil_Name);

                        #region thumnails Images
                        //System.Drawing.Image image = System.Drawing.Image.FromStream(Request.Files[i].InputStream);
                        //string FilePath = Request.PhysicalApplicationPath + "Content\\RealImages\\";
                        //string LocalFile = "";
                        //int imgWidth = 252, imgHeight = 192;
                        //System.Drawing.Image thumb1 = image.GetThumbnailImage((int)imgWidth, (int)imgHeight, delegate() { return false; }, (IntPtr)0);
                        //LocalFile = FilePath + tbl_file.FileId + "_thumbnail" + strImageType;
                        //thumb1.Save(LocalFile);
                        #endregion


                    }

                    #endregion
                }
                ////////////end dem số file vừa add vào////////////////
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    #region
                    var file = Request.Files[i];
                    string strFileName = Path.GetFileName(file.FileName);

                    if (strFileName != "")
                    {
                        /////????????????????? Kiểm trả value trog bảng và so sánh với <input type="file"> xem có giá trị không ?
                        for (int intCount = 0; intCount < GroupFile.ToArray().Length; intCount++)
                        {
                            try
                            {
                                string updatenamefile = GroupFile.ToArray()[intCount].Fil_Name;
                                string strCheckName = Request.Files[GroupFile.ToArray()[intCount].Fil_Name].FileName;
                                //////11111111111111111111 thay đổi hình ảnh//////////////////////////////////
                                if (Request.Files[GroupFile.ToArray()[intCount].Fil_Name].FileName == strFileName)
                                {
                                    string namemmeme = intCount.ToString();
                                    var imageFile = Request.Files[i];
                                    tbl_files tbl_file = new tbl_files();
                                    tbl_file.Pro_Id = id;
                                    string strImageName = "", strImageType = "";
                                    try
                                    {
                                        strImageName = imageFile.FileName;
                                        if (strImageName != null)
                                        {
                                            strImageType = strImageName.Substring(strImageName.LastIndexOf("."));
                                            string pathStoredImage = ConfigurationManager.ConnectionStrings["pathImg_Product"].ToString();
                                            #region thumnails Images
                                            //System.Drawing.Image image = System.Drawing.Image.FromStream(Request.Files[i].InputStream);
                                            //string FilePath = Request.PhysicalApplicationPath + "Content\\RealImages\\";
                                            //string LocalFile = "";
                                            //int imgWidth = 252, imgHeight = 192;
                                            //System.Drawing.Image thumb1 = image.GetThumbnailImage((int)imgWidth, (int)imgHeight, delegate() { return false; }, (IntPtr)0);
                                            //// LocalFile = FilePath + tbl_file.FileId + "_thumbnail" + strImageType;

                                            //LocalFile = FilePath + ThumbnailOld;
                                            //thumb1.Save(LocalFile);
                                            #endregion
                                            Common.UploadFile(imageFile, pathStoredImage, updatenamefile);
                                        }
                                    }
                                    catch { continue; }
                                }
                            }
                            catch { }
                            /////////////////// Run khi chua có image//////////////

                            #region Chưa có Images
                            if (intCount == GroupFile.ToArray().Length)
                            {
                                for (int i1 = 0; i1 < Request.Files.Count; i1++)
                                {
                                    // var file = Request.Files[i1];
                                    string strFileName1 = Path.GetFileName(file.FileName);
                                    if (strFileName1 != "")
                                    {
                                        tbl_files tbl_file = new tbl_files();
                                        tbl_file.Pro_Id = id;
                                        db.tbl_files.AddObject(tbl_file);
                                        db.SaveChanges();

                                        string strImageName = "", strImageType = "";
                                        var imageFile = Request.Files[i1];
                                        strImageName = imageFile.FileName;
                                        strImageType = strImageName.Substring(strImageName.LastIndexOf("."));
                                        string pathStoredImage = ConfigurationManager.ConnectionStrings["pathImg_Product"].ToString();

                                        var newFile = db.tbl_files.Single(a => a.Fil_Id == tbl_file.Fil_Id);
                                        newFile.Fil_Name = tbl_file.Fil_Id + strImageType;
                                        UpdateModel(newFile);
                                        db.SaveChanges();
                                        Common.UploadFile(imageFile, pathStoredImage, newFile.Fil_Name);
                                        #region thumnails Images
                                        //System.Drawing.Image image = System.Drawing.Image.FromStream(Request.Files[i1].InputStream);
                                        //string FilePath = Request.PhysicalApplicationPath + "Content\\RealImages\\";
                                        //string LocalFile = "";
                                        //int imgWidth = 252, imgHeight = 192;
                                        ////generating the thumbnail
                                        //System.Drawing.Image thumb1 = image.GetThumbnailImage((int)imgWidth, (int)imgHeight, delegate() { return false; }, (IntPtr)0);
                                        //LocalFile = FilePath + tbl_file.FileId + "_thumbnail" + strImageType;
                                        //thumb1.Save(LocalFile);
                                        #endregion
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
                return RedirectToAction("Index");
            }
            return View(tbl_Products);
        }

        //
        // GET: /ProductManager/Delete/5
 
        public ActionResult Delete(int id)
        {
            tbl_Products tbl_products = db.tbl_Products.Single(t => t.Pro_Id == id);
            return View(tbl_products);
        }

        //
        // POST: /ProductManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(FormCollection Collection)
        {
            try
            {

          
            string All_IdProduct = Collection.Get("GetAll_Id");
            foreach (var item in All_IdProduct.Split(','))
            {

                if (item.ToString() != "000")
                {
                    int ConvertId = Common.ParseInt(item);

                    var DeleteAllFile = db.tbl_files.Where(a => a.Pro_Id == ConvertId);
                    foreach (var itemFile in DeleteAllFile)
                    {
                        var deleteItem = db.tbl_files.Single(a => a.Fil_Id == itemFile.Fil_Id);
                        db.DeleteObject(deleteItem);
                    }
                    db.SaveChanges();


                    tbl_Products tbl_Product = db.tbl_Products.Single(t => t.Pro_Id == ConvertId);
                    db.tbl_Products.DeleteObject(tbl_Product);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            catch 
            {

                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult DeleteImage(int idProduct, string strFileName)
        {

            //.Single(t => t.RealEstateId == id);
            try
            {
                tbl_files file = db.tbl_files.Single(f => f.Pro_Id == idProduct && f.Fil_Name == strFileName);
                db.tbl_files.DeleteObject(file);
                db.SaveChanges();
                string Path = System.Configuration.ConfigurationManager.ConnectionStrings["pathImg_Product"].ToString();
                Common.DeleteFile(Path + strFileName);

                //string firstImages = strFileName.Split('.')[0];
                //string lastImages = strFileName.Split('.')[1];
                //JDMFiles.DeleteFile(Path + firstImages + "_thumbnail." + lastImages);
                //"_thumbnail"
            }
            catch { }

            return View();
        }
    }
}