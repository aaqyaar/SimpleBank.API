using System;
using SimpleBank.API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBank.API.Entities
{
	public class Branch
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; } = null!;

        [Required]
        [Phone]
        [MaxLength(50)]
        public string Phone { get; set; } = null!;

        public ICollection<Teller> tellers { get; set; } = new List<Teller>();

        //public Branch(string _address, string _phone) {
        //    Address = _address;
        //    Phone = _phone;
        //}
    }
}

