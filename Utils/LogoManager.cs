using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmpreendedorismoEIT.Utils
{
    public static class LogoManager
    {
        public static string SalvarImagem(IWebHostEnvironment webHostEnvironment, IFormFile arquivoForm)
        {
            string uniqueFileName = null;

            if (arquivoForm != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "logo");
                uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(arquivoForm.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    arquivoForm.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        public static void ExcluirImagem(IWebHostEnvironment webHostEnvironment, string arquivo)
        {
            if (String.IsNullOrEmpty(arquivo))
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "logo");
                string filePath = Path.Combine(uploadsFolder, arquivo);
                File.Delete(filePath);
            }
        }

        public static string URLImagem(IUrlHelper UrlHelper, string arquivo)
        {
            string url;
            if (String.IsNullOrEmpty(arquivo))
            {
                url = UrlHelper.Content("~/images/empty_logo.png");
            }
            else
            {
                url = UrlHelper.Content("~/logo/" + arquivo);

            }
            return url;
        }
    }
}
