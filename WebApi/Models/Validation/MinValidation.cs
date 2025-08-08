using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models.Users
{
    public class MinCollectionCountAttribute : ValidationAttribute
    {
        private readonly int _minCount;

        public MinCollectionCountAttribute(int minCount)
        {
            _minCount = minCount;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var collection = value as ICollection;
            if (collection == null || collection.Count < _minCount)
            {
                return new ValidationResult(ErrorMessage ?? $"At least {_minCount} item(s) required.");
            }

            return ValidationResult.Success;
        }
    }
}



