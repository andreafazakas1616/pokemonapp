using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pokeBbyzApp.Models
{
    public class ExcelViewModel
    {
        public HttpPostedFileBase UploadedFile { get; set; }
        public string Error { get; set; }
    }
}