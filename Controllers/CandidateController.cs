using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prac7.Models;
using System.Runtime.InteropServices;
using System.Text.Json;
using static Prac7.Models.DbModels;

namespace Prac7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CandidateController : ControllerBase
    {
        private readonly CandidateDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CandidateController(CandidateDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [HttpGet]
        public async Task<IActionResult> GetCandidates()
        {
            var candidates = await _context.Candidates.Include(c => c.CandidateSkills).ThenInclude(cs => cs.Skill).ToListAsync();

            return Ok(candidates);
        }
        [HttpPost]
        public async Task<IActionResult> PostCandidate([FromForm] CandidateVM vm)
        {
            var candidate = new Candidate
            {
                Name = vm.name,
                BirthDate = vm.birthDate,
                Fresh = vm.fresher,
                ExpectedSalary = vm.expectedSalary
            };
            if (vm.PictureFile != null)
            {
                var webRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var folder = Path.Combine(webRoot, "Images");
                Directory.CreateDirectory(folder);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.PictureFile.FileName);
                var filePath = Path.Combine(folder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.PictureFile.CopyToAsync(stream);
                }
                candidate.Picture = fileName;
                if (!string.IsNullOrEmpty(vm.SkillsStringify))
                {
                    var skillIds = JsonSerializer.Deserialize<int[]>(vm.SkillsStringify);
                    if (skillIds != null)
                    {
                        foreach (var id in skillIds)
                        {
                            candidate.CandidateSkills.Add(new CandidateSkill { SkillId = id });
                        }
                    }

                }

            }
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
            return Ok("Data Saved Successfully!");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidates(int id, [FromForm] CandidateVM vm)
        {
            var candidate = await _context.Candidates.Include(c => c.CandidateSkills).FirstOrDefaultAsync(c => c.CandidateId == id);
            if (candidate == null)
                return NotFound();
            candidate.Name = vm.name;
            candidate.BirthDate = vm.birthDate;
            candidate.Fresh = vm.fresher;
            candidate.ExpectedSalary = vm.expectedSalary;
            if (vm.PictureFile != null)
            {
                var webRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var folder = Path.Combine(webRoot, "Images");
                Directory.CreateDirectory(folder);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.PictureFile.FileName);
                var filePath = Path.Combine(folder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.PictureFile.CopyToAsync(stream);
                }
                _context.CandidateSkills.RemoveRange(candidate.CandidateSkills);
                if (!string.IsNullOrEmpty(vm.SkillsStringify))
                {
                    var skillIds = JsonSerializer.Deserialize<int[]>(vm.SkillsStringify);
                    if (skillIds != null)
                    {
                        foreach (var s in skillIds)
                        {
                            candidate.CandidateSkills.Add(new CandidateSkill { SkillId = s });
                        }
                    }

                }


            }




            await _context.SaveChangesAsync();

            return Ok("Data Updated Successfully!");

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var candidate= await _context.Candidates.FindAsync(id);
            if (candidate==null)
                return NotFound();
            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
