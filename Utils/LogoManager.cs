using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace EmpreendedorismoEIT.Utils
{
    public static class LogoManager
    {
        public static string SalvarImagem(IWebHostEnvironment webHostEnvironment, IFormFile arquivo)
        {
            string uniqueFileName = null;

            if (arquivo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "logo");
                uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(arquivo.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    arquivo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        public static void ExcluirImagem(IWebHostEnvironment webHostEnvironment, string arquivo)
        {
            if (arquivo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "logo");
                string filePath = Path.Combine(uploadsFolder, arquivo);
                File.Delete(filePath);
            }
        }
    }
}
