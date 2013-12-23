using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PacketIntranet.Models.ProductModel
{
    public class ProductTypeModel
    {
        public List<tbl_ProductTypes> List_tbl_ProductType { get; set; }
        public tbl_ProductTypes One_tbl_ProductType { get; set; }
    }
}