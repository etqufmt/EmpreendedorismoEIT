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
        private class ResultadoMatch
        {
            public int EmpresaID { get; set; }
            public int Porcentagem { get; set; }
            public List<double> Associacoes { get; set; }
        }

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
            var res = new Dictionary<int, int>();

            //Listar todas as associação entre empresas e tags
            var listaAssoc = await context.EmpresaTags
                .AsNoTracking()
                .ToListAsync();
            if (listaAssoc.Count == 0)
            {
                logger.LogError("[DEBUG] MatchManager: Nenhuma tag associada");
                return res;
            }

            //Listar todas as tags ativas
            //Com um campo zerado para o grau de associação
            Dictionary<int, double> dicTags = listaAssoc
                .GroupBy(a => a.TagID)
                .Select(a => a.First().TagID)
                .ToDictionary(t => t, t => 0.0);

            //Checar se tags selecionadas possuem associações
            //Calcular peso para as tags pela ordem que foram selecionadas na nuvem 
            var dicSelec = new Dictionary<int, double>(dicTags);
            var pesoSelec = 1.0;
            foreach (var tagID in tagsSelecID)
            {
                if (dicSelec.ContainsKey(tagID))
                {
                    dicSelec[tagID] = pesoSelec;
                    pesoSelec = pesoSelec * 0.85;
                }
                else
                {
                    logger.LogError("[DEBUG] MatchManager: Nuvem de tags inválida");
                    return res;
                }
            }
            List<double> listaTagsSelec = dicSelec.Values.ToList();

            //Agrupar por empresas
            var listaEmp = new List<ResultadoMatch>();
            foreach (var grupo in listaAssoc.GroupBy(a => a.EmpresaID))
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

            //Fazer comparações entre os dois vetores
            //Utilizando algoritmo de similaridade (Cosine Similarity)
            foreach (var emp in listaEmp)
            {
                var match = GetCosineSimilarity(listaTagsSelec, emp.Associacoes);
                emp.Porcentagem = (int)(match * 100);
            }

            //Gerar resposta
            //Shuffle necessário para não mostrar sempre as mesmas empresas
            //Em caso de porcentagens iguais
            res = listaEmp
                .Where(e => e.Porcentagem > 0)
                .Shuffle(new Random())
                .OrderByDescending(e => e.Porcentagem)
                .Take(3)
                .ToDictionary(e => e.EmpresaID, e => e.Porcentagem);
            return res;
        }

        //Fonte: https://stackoverflow.com/questions/1287567/is-using-random-and-orderby-a-good-shuffle-algorithm
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            T[] elements = source.ToArray();
            for (int i = elements.Length - 1; i >= 0; i--)
            {
                int swapIndex = rng.Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
        }

        //Fonte: https://stackoverflow.com/questions/7560760/cosine-similarity-code-non-term-vectors
        private static double GetCosineSimilarity(List<double> V1, List<double> V2)
        {
            int N = (V2.Count < V1.Count) ? V2.Count : V1.Count;
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
}
