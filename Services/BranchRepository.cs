using System;
using Microsoft.EntityFrameworkCore;
using SimpleBank.API.DbContexts;
using SimpleBank.API.Entities;

namespace SimpleBank.API.Services
{
	public class BranchRepository : IBranchRepository
	{
        private readonly BranchContext context;

        public BranchRepository(BranchContext _context)
		{
            context = _context ?? throw new ArgumentNullException(nameof(_context));
		}

        public async Task<Branch?> GetBranchAsync(string branchId, bool includeTellers)
        {
            if (includeTellers)
            {
                return await context.Branches.Include(b => b.tellers).Where(b => b.Id == branchId).FirstOrDefaultAsync();
            }
            return await context.Branches.Where(b => b.Id == branchId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Branch>> GetBranchesAsync(bool includeTellers)
        {
            if (includeTellers)
            {
                return await context.Branches.Include(b => b.tellers).OrderBy(b => b.Id).ToListAsync();
            }
            return await context.Branches.OrderBy(b => b.Id).ToListAsync();
        }

        public async Task<bool> isBranchExistAsync(string branchId)
        {
            return await context.Branches.AnyAsync(b => b.Id == branchId);
        }

        public async Task<Teller> GetTellerAsync(string branchId, string tellerId)
        {
            return await context.Tellers.Where(t => t.BranchId == branchId && t.Id == tellerId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Teller>> GetTellersAsync(string branchId)
        {
            return await context.Tellers.Where(t => t.BranchId == branchId).ToListAsync();
        }
    }
}

