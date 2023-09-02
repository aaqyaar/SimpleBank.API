using System.ComponentModel.DataAnnotations;

namespace SimpleBank.API.Models
{
	public class BranchDto
	{
		public string id { get; set; } = string.Empty;
		[Required]
		public string address { get; set; } = string.Empty;
		[Required]
		[Phone]
		public string phone { get; set; } = string.Empty;

		public int numberOfTellers
		{
			get { return tellers.Count; }
		}
		public ICollection<TellerDto> tellers { get; set; } = new List<TellerDto>();
    }

    public class BranchWithoutTellerDto
    {
        public string id { get; set; } = string.Empty;
        [Required]
        public string address { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string phone { get; set; } = string.Empty;       
    }
}

