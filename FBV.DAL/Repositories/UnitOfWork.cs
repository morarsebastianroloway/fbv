﻿using FBV.DAL.Contracts;
using FBV.DAL.Data;
using FBV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBV.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FBVContext _context;

        public UnitOfWork(FBVContext context)
        {
            _context = context;
            PurchaseOrderRepository = new PurchaseOrderRepository(context);
            PurchaseOrderLineRepository = new PurchaseOrderLineRepository(context);
            MembershipRepository = new MembershipRepository(context);
        }

        public IPurchaseOrderRepository PurchaseOrderRepository { get; private set; }
        public IPurchaseOrderLineRepository PurchaseOrderLineRepository { get; private set; }
        public IMembershipRepository MembershipRepository { get; private set; }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
