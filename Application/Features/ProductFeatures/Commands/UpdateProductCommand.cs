﻿using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{
    public class UpdateProductCommand : IRequest<int> 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public UpdateProductCommandHandler(IApplicationDbContext context) =>  _context = context;
            public async Task<int> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var product = await _context.Products.Where(p => p.Id == command.Id).FirstOrDefaultAsync();
                if (product == null)
                {
                    return default;
                } else
                {
                    product.Barcode = command.Barcode;
                    product.Description = command.Description;
                    product.Rate = command.Rate;
                    product.Name = command.Name;
                    await _context.SaveChangesAsync();
                    return product.Id;
                }
            }
        }
    }
}
