using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class ValidationHelper
    {
        public static void ModelValidation(object obj)
        {
            //if(requestDTO==null)
            //{
            //    throw new ArgumentNullException(nameof(requestDTO));
            //}

            ArgumentNullException.ThrowIfNull(nameof(obj));
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, validationContext, results, true);
            if (!isValid)
            {
                throw new ArgumentException(results.FirstOrDefault()?.ErrorMessage);

            }
        }
    }
}
