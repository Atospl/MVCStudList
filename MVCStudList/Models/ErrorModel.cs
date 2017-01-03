using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCStudList.Models
{
    public class ErrorModel
    {
        public string ErrorMessage;

        public ErrorModel(string message)
        {
            ErrorMessage = message;
        }
    }
}