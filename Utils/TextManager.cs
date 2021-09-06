using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EmpreendedorismoEIT.Utils
{
    public static class TextManager
    {
        public static string FormatarCNPJ(string CNPJ)
        {
            string res = null;
            if (!String.IsNullOrEmpty(CNPJ) && CNPJ.Length == 14)
            {
                try
                {
                    res = Int64.Parse(CNPJ).ToString(@"##\.###\.###/####-##");
                }
                catch { }
            }
            return res;
        }

        public static string FormatarTelefone(string telefone)
        {
            string res = null;
            if (!String.IsNullOrEmpty(telefone))
            {
                try
                {
                    if (telefone.Length == 10)
                    {
                        res = Int64.Parse(telefone).ToString(@"(##) ####-####");
                    }
                    if (telefone.Length == 11)
                    {
                        res = Int64.Parse(telefone).ToString(@"(##) #####-####");
                    }
                }
                catch { }
            }
            return res;
        }

        public static string CorHTML(int cor)
        {
            try
            {
                //Formato hexadecimal para uso em CSS (#FF32DD)
                return String.Format("{0:X6}", cor).Insert(0, "#");
            }
            catch
            {
                return null;
            }
        }

        public static string WhatsappURL(string num)
        {
            string res = null;
            if (!String.IsNullOrEmpty(num) && num.Length == 11)
            {
                //Código internacional para telefones brasileiros = 55
                const string wppAPI = "https://api.whatsapp.com/send?phone=55";
                return wppAPI + num;
            }
            return res;
        }
    }
}
