﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectF.Application.Suppliers;
using static ProjectF.Data.Entities.Suppliers.SupplierMapper;
using Microsoft.AspNetCore.Mvc;

namespace ProjectF.Api.Features.Supplier
{
    [Route("api/inventory/[controller]")]
    [ApiController]
    public class SupplierController : Controller
    {
        private readonly SupplierCrudHandler _supplierOperation;

        public SupplierController(SupplierCrudHandler supplierOperation)
        {
            _supplierOperation = supplierOperation;
        }

        [HttpPost]
        public ActionResult CreateSupplier(SupplierViewModel viewModel)
            => _supplierOperation
                .Create(viewModel.ToDto())
                .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: s => Ok(FromEntity(s)));

        [HttpPut("{id}")]
        public ActionResult UpdateSupplier(long id, SupplierViewModel viewModel)
            => _supplierOperation
                .Update(id, viewModel.ToDto())
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: c => Ok(SupplierViewModel.FromDto(c)));

        [HttpGet("{id}")]
        public IActionResult GetSupplier(long id)
            => _supplierOperation
                .Find(id)
                .Match<ActionResult>(
                    Left: err => NotFound(err.Message),
                    Right: cat
                            => Ok(SupplierViewModel.FromDto(FromEntity(cat))));

        [HttpGet]
        public ActionResult GetSupplier()
        {
            var result = _supplierOperation.FindAll()
                .Select(c => SupplierViewModel.FromDto(c));
            if (result.Any()) return Ok(result);

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
            => _supplierOperation.Delete(id)
            .Match<ActionResult>(
                Left: err => BadRequest(err.Message),
                Right: c => NoContent());
    }
}
