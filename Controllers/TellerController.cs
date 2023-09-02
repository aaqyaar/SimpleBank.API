using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleBank.API.Models;
using SimpleBank.API.Services;

namespace SimpleBank.API.Controllers
{
	[ApiController]
    [Route("api/branches")]
    public class TellerController : ControllerBase
	{
        private readonly ILogger<TellerController> _logger;
        private readonly IMailService _mailService;
        private readonly IBranchRepository _repository;
        private readonly IMapper _mapper;

        public TellerController(ILogger<TellerController> logger, IMailService mailService, BranchDataStore branchStore, IBranchRepository repository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _mailService = mailService;
            _repository = repository ?? throw new ArgumentException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        [HttpGet("{id}/tellers")]
        public async Task<ActionResult<IEnumerable<TellerDto>>> GetTellers(string id)
        {
            if (!await _repository.isBranchExistAsync(id))
            {
                _logger.LogInformation($"Branch with id {id} not exists");
                return NotFound();
            }
            var data = await _repository.GetTellersAsync(id);
            return Ok(_mapper.Map<IEnumerable<TellerDto>>(data));
        }

        //ActionResult<IEnumerable<TellerDto>
        [HttpGet("{id}/tellers/{tellerId}", Name = "GetTeller")]
        public async Task<ActionResult<TellerDto>> GetTeller(string id, string tellerId)
        {
            if (!await _repository.isBranchExistAsync(id))
            {
                _logger.LogInformation($"Branch with id {id} not exists");
                return NotFound();
            }
            var data = await _repository.GetTellerAsync(id, tellerId);
            if (data == null)
            {
                _logger.LogWarning($"Branch with teller {tellerId} not found");

                return NotFound();
            }
            return Ok(_mapper.Map<TellerDto>(data));
        }

        //[HttpPost("{id}/tellers")]
        //public ActionResult<TellerDto> CreateTeller([FromRoute] string id, [FromBody] TellerCreationDto teller)
        //{
        //    var branch = _branchStore.branches.FirstOrDefault((item) => item.id.Equals(id));
        //    if (branch == null)
        //    {
        //        return NotFound();
        //    }
        //    var __maxTellerId = _branchStore.branches
        //        .SelectMany(c => c.tellers)
        //        .Max(t => int.Parse(t.id.Substring(3))); // Extract the numeric part and parse as int

        //    // Increment the numeric part and format it back to "TEL00X" format
        //    var incrementedId = $"TEL{(__maxTellerId + 1):D3}";

        //    var _data = new TellerDto()
        //    {
        //        id = incrementedId,
        //        name = teller.name,
        //        phone = teller.phone
        //    };

        //    branch.tellers.Add(_data);

        //    return CreatedAtRoute("GetTeller", new
        //    {
        //        id = id,
        //        tellerId = _data.id
        //    }, _data);
        //}
        //[HttpPut("{id}/tellers/{tellerId}")]
        //public ActionResult<BranchDto> UpdateTeller([FromRoute] string id, [FromRoute] string tellerId, [FromBody] TellerCreationDto teller)
        //{
        //    var _store = _branchStore.branches.FirstOrDefault((item) => item.id == id);
        //    if (_store == null)
        //    {
        //        return NotFound();
        //    }
        //    var _teller = _store.tellers.FirstOrDefault((item) => item.id == tellerId);
        //    if (_teller == null)
        //    {
        //        return NotFound();
        //    }
        //    _teller.name = teller.name;
        //    _teller.phone = teller.phone;
        //    return Ok(_store);
        //}
        //[HttpDelete("{id}/tellers/{tellerId}")]
        //public IActionResult DeleteTeller([FromRoute] string id, [FromRoute] string tellerId)
        //{
        //    var _findBranch = _branchStore.branches.FirstOrDefault((item) => item.id == id);
        //    if (_findBranch == null)
        //    {
        //        return NotFound();
        //    }
        //    var _findTeller = _findBranch.tellers.FirstOrDefault((item) => item.id == tellerId);
        //    if (_findTeller == null)
        //    {
        //        return NotFound();
        //    }
        //    var removedBranch = _findBranch.tellers.Remove(_findTeller);

        //    return NoContent();
        //}
    }
}

