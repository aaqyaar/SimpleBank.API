using Microsoft.AspNetCore.Mvc;
using SimpleBank.API.Models;

namespace SimpleBank.API.Controllers
{
	[ApiController]
	[Route("api/branches")]
	public class BranchController : ControllerBase
	{
 		[HttpGet]
		public ActionResult<IEnumerable<BranchDto>> GetBranches()
		{
			return Ok(BranchDataStore.Current.branches);
		}
		[HttpGet("{id}")]
		public ActionResult<BranchDto> GetBranch(string id)
		{
 			var branch = BranchDataStore.Current.branches.FirstOrDefault((item) => item.id.Equals(id));
			// if the isn't exist return null
			if (branch == null)
			{
                return NotFound();
            }
            return Ok(branch);
		}

		[HttpGet("{id}/tellers")]
		public ActionResult<IEnumerable<TellerDto>> GetTellers(string id)
		{
            var branch = BranchDataStore.Current.branches.FirstOrDefault((item) => item.id.Equals(id));
			if (branch == null)
			{
				return NotFound();
			}
			return Ok(branch.tellers);
        }

        [HttpGet("{id}/tellers/{tellerId}")]
        public ActionResult<IEnumerable<TellerDto>> GetTeller(string id, string tellerId)
        {
            var branch = BranchDataStore.Current.branches.FirstOrDefault((item) => item.id.Equals(id));
            if (branch == null)
            {
                return NotFound();
            }
            var teller = branch.tellers.FirstOrDefault((item) => item.id.Equals(tellerId));
            if (teller == null)
            {
                return NotFound();
            }
            return Ok(teller);
        }

    }
}

