using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.Utils
{
    public class FormatTextAttribute : ValidationAttribute
    {
        //Remove espaços em branco no início e fim
        //Capitaliza primeira letra
        protected override ValidationResult IsValid(
            object value,
            ValidationContext ctx)
        {
            var prop = ctx.ObjectType.GetProperty(ctx.MemberName);
            string newVal = null;
            if (prop.GetValue(ctx.ObjectInstance) is string oldVal)
            {
                var trimmedVal = oldVal.Trim();
                newVal = trimmedVal.Substring(0, 1).ToUpper();
                if (trimmedVal.Length > 1)
                {
                    newVal += trimmedVal[1..];
                }
            }

            prop.SetValue(ctx.ObjectInstance, newVal);
            return ValidationResult.Success;
        }
    }
}
