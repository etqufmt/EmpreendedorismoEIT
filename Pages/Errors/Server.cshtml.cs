using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ServerModel : PageModel
    {
        private readonly ILogger<ServerModel> _logger;
        public string RequestId { get; set; }

        public ServerModel(ILogger<ServerModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature != null)
            {
                var exPath = exceptionHandlerPathFeature.Path;
                var exError = exceptionHandlerPathFeature.Error;

                _logger.LogError(exError, $"[DEBUG] ServerError: Exceção não tratada em '{exPath}'");
            }
        }
    }
}
