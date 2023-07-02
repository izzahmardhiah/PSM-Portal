using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using psmportal.Models;

namespace psmportal.Models.ViewModels
{
    public class RequestCreateViewModel
    {
        public tb_request Request { get; set; }
        public List<tb_lecturer> Lecturers { get; set; }
    }
}