using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmpreendedorismoEIT.Areas.Admin.Pages
{
    public static class SectionManager
    {
        public static string Inicio => "Inicio";

        public static string Juniores => "Juniores";

        public static string Incubadas => "Incubadas";

        public static string Tags => "Tags";

        public static string CheckInicio(ViewContext viewContext) => PageNavClass(viewContext, Inicio);

        public static string CheckJuniores(ViewContext viewContext) => PageNavClass(viewContext, Juniores);

        public static string CheckIncubadas(ViewContext viewContext) => PageNavClass(viewContext, Incubadas);

        public static string CheckTags(ViewContext viewContext) => PageNavClass(viewContext, Tags);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["Section"] as string ?? "";
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
