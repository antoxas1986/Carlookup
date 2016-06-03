using Postal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CarLookUp.Web.ViewModels
{
    public class DetailsEmailVM : Email
    {
        public DetailsEmailVM(string viewName) : base(viewName)
        {
        }

        public int Id { get; set; }

        [DisplayName("Make")]
        public string Maker { get; set; }

        [DisplayName("Model")]
        public string Model { get; set; }

        public string Subject { get; set; }

        public string ToAddress { get; set; }

        [DisplayName("Year")]
        public int Year { get; set; }
    }
}
