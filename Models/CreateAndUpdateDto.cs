using System;
namespace SimpleBank.API.Models
{
    public class BranchCreationDto
    {
        public string address { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public ICollection<TellerDto> tellers { get; set; } = new List<TellerDto>();
    }
    public class TellerCreationDto
	{
        public string name { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
    }
}

