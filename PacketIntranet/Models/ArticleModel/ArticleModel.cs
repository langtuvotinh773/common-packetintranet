using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PacketIntranet.Models.ArticleModel
{
    public class ArticleModel
    {
        public List<tbl_Articles> List_tbl_Articles { get; set; }
        public List<tbl_Articles> list_Categorys { get; set; }
        public tbl_Articles one_tbl_Articles { get; set; }

        public List<tbl_Relationship> List_tbl_Relationship { get; set; }
        public tbl_Relationship one_tbl_Relationship { get; set; }

    }
}