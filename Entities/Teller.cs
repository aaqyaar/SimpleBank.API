using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBank.API.Entities
{
	public class Teller
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }   

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [Phone]
        [MaxLength(50)]
        public string Phone { get; set; } = null!;

        [ForeignKey("BranchId")]
        public Branch? Branch { get; set; }
         
        public string BranchId { get; set; } = null!;


        //public Teller(string _name, string _phone)
        //{
        //    Name = _name;
        //    Phone = _phone;
        //}
    }
}

