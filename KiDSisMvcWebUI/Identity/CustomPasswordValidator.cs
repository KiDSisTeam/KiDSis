using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace KiDSisMvcWebUI.Identity
{
    public class CustomPasswordValidator : PasswordValidator
    {


        public override async Task<IdentityResult> ValidateAsync(string password)
        {
            var result = await base.ValidateAsync(password);

            if (password.Contains("12345"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Parola ardışık sayısal ifade içeremez");
                result = new IdentityResult(errors);
            }
            if (password.Contains("123"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Parola ardışık sayısal ifade içeremez");
                result = new IdentityResult(errors);
            }
            if (password.Contains("1234"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Parola ardışık sayısal ifade içeremez");
                result = new IdentityResult(errors);
            }
            return result;
        }

    }
}