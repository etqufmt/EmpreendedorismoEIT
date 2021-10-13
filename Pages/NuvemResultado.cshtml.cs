using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmpreendedorismoEIT.Pages
{
    public class NuvemResultadoModel : PageModel
    {
        [BindProperty]
        public string[] list { get; set; }

        public void OnGet()
        {
        }

        public void OnPost(string[] tags)
        {
            list = tags;
        }
    }
}
