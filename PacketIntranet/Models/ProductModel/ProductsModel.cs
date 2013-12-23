using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PacketIntranet.Models.ProductModel
{
    public class ProductsModel
    {
        public List<tbl_Products> list_tbl_Product { get; set; }
        public tbl_Products one_tbl_Product { get; set; }

        public List<tbl_Units> list_tbl_Unit { get; set; }
        public List<tbl_ProductTypes> list_tbl_ProductType { get; set; }
        public List<tbl_Origins> list_tbl_Origin { get; set; }

        public tbl_Units one_tbl_Unit { get; set; }
        public tbl_ProductTypes one_tbl_ProductType { get; set; }
        public tbl_Origins one_tbl_Origin { get; set; }

        public List<tbl_files> list_tbl_files { get; set; }
        public tbl_files one_tbl_files { get; set; }

        public string getPath { get; set; }
    }

   
}