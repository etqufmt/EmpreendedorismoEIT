using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EmpreendedorismoEIT.Utils
{
    public static class MatchManager
    {
        public static List<int> ListarInteiros(string lista)
        {
            //Valores cercados por aspas
            //E separados por vírgula
            var res = new List<int>();
            try
            {
                var listaStr = lista.Split(",");
                for (var i = 0; i < listaStr.Length; i++)
                {
                    if (i == listaStr.Length - 1 && string.IsNullOrWhiteSpace(listaStr[i]))
                    {
                        continue;
                    }
                    var valStr = listaStr[i].Trim('"');
                    var valInt = int.Parse(valStr);
                    res.Add(valInt);
                }
            }
            catch
            {
                res = new List<int>();
            }
            return res;
        }

        public static async Task<Dictionary<int, int>> ObterRecomendacao(
            ApplicationDbContext context,
            ILogger logger,
            List<int> tagsSelecID)
        {
            Dictionary<int, int> res = null;

            //Listar todas as associação entre empresas e tags 
            var listaAssoc = await context.EmpresaTags
                .AsNoTracking()
                .ToListAsync();
            
            //Checar se há associações
            if (listaAssoc.Count == 0)
            {
                logger.LogError("[DEBUG] MatchManager: Nenhuma tag associada");
                return null;
            }

            //Checar se tags selecionadas estão ativas
            foreach (var tagID in tagsSelecID)
            {
                if (listaAssoc.FirstOrDefault(a => a.TagID == tagID) == null)
                {
                    logger.LogError("[DEBUG] MatchManager: Nuvem de tags inválida");
                    return null;
                }
            }

            //Listar todas as tags ativas
            Dictionary<int, double> dicTags = listaAssoc
                .OrderBy(a => a.TagID)
                .GroupBy(a => a.TagID)
                .Select(a => a.First().TagID)
                .ToDictionary(t => t, t => 0.0);

            //Listar tags selecionadas
            List<double> listaTagsSelec = dicTags.Select(t => tagsSelecID.Contains(t.Key) ? 1.0 : 0.0).ToList();

            //Agrupar por empresas
            //TODO: colocar ordem das empresas aleatória
            var grupoEmp = listaAssoc
                //.OrderBy(a => a.TagID)
                //.ThenBy(a => a.TagID)
                .GroupBy(a => a.EmpresaID);
            var listaEmp = new List<ResultadoMatch>();
            foreach (var grupo in grupoEmp)
            {
                var tagsAtivas = new Dictionary<int, double>(dicTags);
                foreach (var assoc in grupo)
                {
                    tagsAtivas[assoc.TagID] = assoc.Grau;
                }
                listaEmp.Add(new ResultadoMatch() {
                    EmpresaID = grupo.Key,
                    Porcentagem = 0,
                    Associacoes = tagsAtivas.Values.ToList()
                });
            }

            //Fazer comparações
            //TODO: colocar peso de acordo com a ordem
            foreach (var emp in listaEmp)
            {
                var match = GetCosineSimilarity(listaTagsSelec, emp.Associacoes);
                emp.Porcentagem = (int)(match * 100);
            }

            //Gerar resposta
            res = listaEmp
                .OrderByDescending(e => e.Porcentagem)
                .Take(3)
                .ToDictionary(e => e.EmpresaID, e => e.Porcentagem);
            //res.Add(1, 85);
            //res.Add(2, 90);
            //res.Add(3, 95);
            return res;
        }

        //Fonte: https://stackoverflow.com/questions/7560760/cosine-similarity-code-non-term-vectors
        private static double GetCosineSimilarity(List<double> V1, List<double> V2)
        {
            int N = 0;
            N = ((V2.Count < V1.Count) ? V2.Count : V1.Count);
            double dot = 0.0d;
            double mag1 = 0.0d;
            double mag2 = 0.0d;
            for (int n = 0; n < N; n++)
            {
                dot += V1[n] * V2[n];
                mag1 += Math.Pow(V1[n], 2);
                mag2 += Math.Pow(V2[n], 2);
            }

            return dot / (Math.Sqrt(mag1) * Math.Sqrt(mag2));
        }
    }

    public class ResultadoMatch
    {
        public int EmpresaID { get; set; }
        public int Porcentagem { get; set; }
        public List<double> Associacoes { get; set; }
    }
}
