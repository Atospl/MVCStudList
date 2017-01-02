using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace MVCStudList.Models
{
    public class MainPageModel
    {
        public StudentListModel studListModel;
        public int maxLen = int.Parse(WebConfigurationManager.AppSettings["StudListLen"]);
    }
}