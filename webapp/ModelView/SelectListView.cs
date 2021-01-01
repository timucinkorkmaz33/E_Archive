using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.ModelView
{
    public class SelectListView:SelectListItem
    {
        public string _Text { get; set; }
        public int _Value { get; set; }
        public bool _sValue { get; set; }
        public string _strValue { get; set; }

    }
}