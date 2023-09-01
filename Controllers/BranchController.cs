using Microsoft.AspNetCore.Mvc;
using SimpleBank.API.Models;
using SimpleBank.API.Services;

namespace SimpleBank.API.Controllers
{
	[ApiController]
	[Route("api/branches")]
	public class BranchController : ControllerBase
	{
        private readonly ILogger<BranchController> _logger;
        private readonly IMailService _mailService;
        private readonly BranchDataStore _branchStore;

         public BranchController(ILogger<BranchController> logger, IMailService mailService, BranchDataStore branchStore)
        {
            _logger = logger;
            _mailService = mailService;
            _branchStore = branchStore ?? throw new ArgumentException(nameof(BranchController));
        }

         [HttpGet]
		public ActionResult<IEnumerable<BranchDto>> GetBranches()
		{
			return Ok(_branchStore.branches);
		}
		[HttpGet("{id}", Name = "GetBranch")]
		public ActionResult<BranchDto> GetBranch(string id)
		{
 			var branch = _branchStore.branches.FirstOrDefault((item) => item.id.Equals(id));
			// if the isn't exist return null
			if (branch == null)
			{
                return NotFound();
            }
            return Ok(branch);
		}

        [HttpPost]
        public ActionResult<BranchDto> CreateBranch([FromBody] BranchCreationDto branch)
        {
            var _store = _branchStore.branches;
            var __maxBranchId = _branchStore.branches.Select(item => int.Parse(item.id.Substring(5))).Max(); // Extract the numeric part and parse as int

            var _incrementedId = $"BRSOM{(__maxBranchId + 1):D3}";
            var _data = new BranchDto()
            {
                id = _incrementedId,
                address = branch.address,
                phone = branch.phone,
                tellers = branch.tellers
            };
            _store.Add(_data);
            return CreatedAtRoute("GetBranch", new { id = _data.id }, _data);
        }
        [HttpPut("{id}")]
        public ActionResult<BranchDto> UpdateBranch([FromRoute] string id,[FromBody] BranchCreationDto branch)
        {
            var _store = _branchStore.branches.FirstOrDefault((item) => item.id == id);
            if (_store == null)
            {
                return NotFound();
            }
            _store.address = branch.address;
            _store.phone = branch.phone;
            return Ok(_store);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBranch([FromRoute] string id)
        {           
            var _findBranch = _branchStore.branches.FirstOrDefault((item) => item.id == id);
            if (_findBranch == null)
            {
                return NotFound();
            }
            var removedBranch = _branchStore.branches.Remove(_findBranch);
            _mailService.Send("Brand Deletion", $"Hey Sir we deleted this branch number {_findBranch.phone}.");
            return NoContent();  
        }
        
        [HttpGet("{id}/tellers")]
		public ActionResult<IEnumerable<TellerDto>> GetTellers(string id)
		{
            var branch = _branchStore.branches.FirstOrDefault((item) => item.id.Equals(id));
			if (branch == null)
			{
				return NotFound();
			}
			return Ok(branch.tellers);
        }

        [HttpGet("{id}/tellers/{tellerId}", Name = "GetTeller")]
        public ActionResult<IEnumerable<TellerDto>> GetTeller(string id, string tellerId)
        {
            var branch = _branchStore.branches.FirstOrDefault((item) => item.id.Equals(id));
            if (branch == null)
            {
                return NotFound();
            }
            var teller = branch.tellers.FirstOrDefault((item) => item.id.Equals(tellerId));
            if (teller == null)
            {
                _logger.LogWarning($"Branch with teller {tellerId} not found");

                return NotFound();
            }
            return Ok(teller);
        }
        [HttpPost("{id}/tellers")]
        public ActionResult<TellerDto> CreateTeller([FromRoute] string id, [FromBody] TellerCreationDto teller)        
        {
            var branch = _branchStore.branches.FirstOrDefault((item) => item.id.Equals(id));
            if (branch == null)
            {
                return NotFound();
            }
            var __maxTellerId = _branchStore.branches
                .SelectMany(c => c.tellers)
                .Max(t => int.Parse(t.id.Substring(3))); // Extract the numeric part and parse as int

            // Increment the numeric part and format it back to "TEL00X" format
            var incrementedId = $"TEL{(__maxTellerId + 1):D3}";

            var _data = new TellerDto()
            {
                id = incrementedId,
                name = teller.name,
                phone = teller.phone
            };

            branch.tellers.Add(_data);

            return CreatedAtRoute("GetTeller",  new
            {
             id = id,
             tellerId = _data.id
            }, _data);
        }
        [HttpPut("{id}/tellers/{tellerId}")]
        public ActionResult<BranchDto> UpdateTeller([FromRoute] string id, [FromRoute] string tellerId,[FromBody] TellerCreationDto teller)
        {
            var _store = _branchStore.branches.FirstOrDefault((item) => item.id == id);
            if (_store == null)
            {
                return NotFound();
            }
            var _teller = _store.tellers.FirstOrDefault((item) => item.id == tellerId);
            if (_teller == null)
            {
                return NotFound();
            }
            _teller.name = teller.name;
            _teller.phone = teller.phone;
            return Ok(_store);
        }
        [HttpDelete("{id}/tellers/{tellerId}")]
        public IActionResult DeleteTeller([FromRoute] string id, [FromRoute] string tellerId)
        {
            var _findBranch = _branchStore.branches.FirstOrDefault((item) => item.id == id);
            if (_findBranch == null)
            {
                return NotFound();
            }
            var _findTeller = _findBranch.tellers.FirstOrDefault((item) => item.id == tellerId);
            if (_findTeller == null)
            {
                return NotFound();
            }
            var removedBranch = _findBranch.tellers.Remove(_findTeller);

            return NoContent();
        }
    }
}

