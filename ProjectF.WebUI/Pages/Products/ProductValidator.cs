﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Pages.Products
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty()
                .WithMessage("Nombre es requerido")
                .MaximumLength(60)
                .WithMessage("Máximo Carateres (60) excedido");


            RuleFor(p => p.CategoryId).NotEmpty()
                .WithMessage("Categoria es requerida");

            RuleFor(p => p.WarehouseId).NotEmpty()
                .WithMessage("Almacen es requerida");

            RuleFor(p => p.TaxId).NotEmpty()
                .WithMessage("Impuesto es requerida");

            RuleFor(p => p.UnitOfMeasureId).NotEmpty()
                .WithMessage("Unidad es requerida");

        }
    }
}
