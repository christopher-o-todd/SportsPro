using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsPro.Models;
using SportsPro.DataLayer;

namespace SportsPro.ViewComponents
{
    // a ViewComponent model to show the copyright information on each page via the layout.cshtml file
    public class CopyrightViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var timeNow = DateTime.Now;

            return View(timeNow);       
        }
    } 
} 
