using AutoMapper;
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
        private readonly IBranchRepository _repository;
        private readonly IMapper _mapper;
        
         public BranchController(ILogger<BranchController> logger, IMailService mailService, BranchDataStore branchStore, IBranchRepository repository, IMapper mapper)
        {
            _logger = logger;
            _mailService = mailService;
            _branchStore = branchStore ?? throw new ArgumentException(nameof(BranchController));
            _repository = repository ?? throw new ArgumentException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

         [HttpGet]
		public async Task<IActionResult> GetBranches(bool includeTeller = true)
		{
            var data = await _repository.GetBranchesAsync(includeTeller);
            if (includeTeller)
            {
                 return Ok(_mapper.Map<IEnumerable<BranchDto>>(data));
            }
            return Ok(_mapper.Map<IEnumerable<BranchWithoutTellerDto>>(data));
        }

        [HttpGet("{id}", Name = "GetBranch")]
		public async Task<IActionResult> GetBranch(string id, bool includeTeller = false)
		{
            var branch = await _repository.GetBranchAsync(id, includeTeller);
 			// if the isn't exist return null
			if (branch == null)
			{
                return NotFound();
            }

            if (includeTeller)
            {
                return Ok(_mapper.Map<BranchDto>(branch));
            }
            return Ok(_mapper.Map<BranchWithoutTellerDto>(branch));

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
        
  
       
    }
}

