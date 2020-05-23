﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalAplicada2.Validaciones
{
    public class MontoValidacion : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                decimal cantidad = Convert.ToDecimal(value);

                if (cantidad >= 0.1m)
                    return ValidationResult.Success;
                else
                    return new ValidationResult("El monto debe mayor o igual a 0.1");

            }
            return new ValidationResult("Debes poner un monto");
        }
    }
}
