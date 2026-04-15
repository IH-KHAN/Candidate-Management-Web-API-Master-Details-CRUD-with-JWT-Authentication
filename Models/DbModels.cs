using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Prac7.Models
{
    public class DbModels
    {
        public class Candidate
        {
            public int CandidateId { get; set; }
            [Required,StringLength(50)]
            public string? Name { get; set; }
            [Required]

            public DateTime BirthDate { get; set; }
            [Required]

            public bool Fresh { get; set; }
            [Required]            

            public string? Picture { get; set; }
            [Required]
            [Column(TypeName ="decimal(10,2)")]
            public decimal ExpectedSalary { get; set; }
            public ICollection<CandidateSkill> CandidateSkills { get; set; } = new List<CandidateSkill>();
        }
        public class Skill
        {
            public  int SkillId { get; set; }
            public string? SkillName { get; set; }

        }
        public class CandidateSkill

        {
            [JsonIgnore]
            public int CandidateSkillId { get; set; }
            [JsonIgnore]
            public int CandidateId { get; set; }
            [JsonIgnore]
            public int SkillId { get; set; }
            [JsonIgnore]
            public Candidate? Candidate { get; set; }
            
            public Skill? Skill { get; set; }
        }
        public class CandidateDbContext : DbContext
        {
            public CandidateDbContext(DbContextOptions<CandidateDbContext> options) : base(options)
            {

            }
            public DbSet<Candidate> Candidates { get; set; }
            public DbSet<Skill> Skills { get; set; }
            public DbSet<CandidateSkill> CandidateSkills { get; set; }
            protected override void OnModelCreating(ModelBuilder mb)
            {
                mb.Entity<Skill>().HasData(
                    new Skill { SkillId=1,SkillName="C++"},
                    new Skill { SkillId = 2, SkillName = "Php" },
                    new Skill { SkillId = 3, SkillName = "Java" },
                    new Skill { SkillId = 4, SkillName = "C#" }

                    );
            }

        }

    }
}
