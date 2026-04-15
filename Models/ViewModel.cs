using System.ComponentModel.DataAnnotations;

namespace Prac7.Models
{
    public class CandidateVM
    {
        public int candidateId { get; set; }
        [Required,StringLength(50)]
        public string? name { get; set; }
        [Required]

        public DateTime birthDate { get; set; }
        [Required]

        public bool  fresher { get; set; }
        public decimal expectedSalary { get; set; }
        //[Required]

        public string? picture { get; set; }
        [Required]

        public IFormFile? PictureFile { get; set; }
        [Required]

        public string? SkillsStringify { get; set; }
    }
}
