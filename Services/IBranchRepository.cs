using System;
using SimpleBank.API.Entities;

namespace SimpleBank.API.Services
{
	public interface IBranchRepository
	{
		Task<IEnumerable<Branch>> GetBranchesAsync(bool includeTellers);

		Task<Branch?> GetBranchAsync(string branchId, bool includeTellers);

		Task<IEnumerable<Teller>> GetTellersAsync(string branchId);

		Task<Teller> GetTellerAsync(string branchId, string tellerId);
    }
}

