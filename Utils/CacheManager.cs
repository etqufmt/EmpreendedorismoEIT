using EmpreendedorismoEIT.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.Utils
{
    public class CacheManager
    {
        public static async Task<SelectList> RamosAtuacaoSL(
            IMemoryCache memoryCache,
            ApplicationDbContext context,
            ILogger logger)
        {
            string cacheKey = "RamosAtuacaoSL";
            SelectList RamosAtuacaoSL = null;
            if (!memoryCache.TryGetValue(cacheKey, out RamosAtuacaoSL))
            {
                //Obtém dados do banco
                var ra = await context.RamosAtuacao.OrderBy(r => r.CNAE).AsNoTracking().ToListAsync();
                RamosAtuacaoSL = new SelectList(ra, "ID", "CNAE");

                //Armazena no cache
                memoryCache.Set(cacheKey, RamosAtuacaoSL,
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(30)));
                logger.LogInformation($"{cacheKey} atualizado no cache");
            }
            else
            {
                logger.LogInformation($"{cacheKey} lido do cache");
            }

            return RamosAtuacaoSL;
        }
    }
}
