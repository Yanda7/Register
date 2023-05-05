using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Register.Data;
using Register.Models;

namespace Register.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INotyfService _notyf;
        public PersonController(ApplicationDbContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        public async Task<IActionResult> PersonList()
        {
            var person = await _context.Persons.Where(x => x.IsActive == true).ToListAsync();
            return View(person);
        }

        [HttpGet]
        public async Task<IActionResult> AddPerson()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson(Person person)
        {
            if(ModelState.IsValid)
            {
                person.PersonId = Helper.Utility.GenerateGuid();

                person.IsActive = true;

                person.CreatedOn = Helper.Utility.CurrentDatetime();

                person.Createdby = "Mbuso and Nqobile";

                await _context.Persons.AddAsync(person);

                var rc = await _context.SaveChangesAsync();

                if(rc > 0)
                {
                    _notyf.Success("Person Successfully saved");
                }
                else
                {
                    _notyf.Error("Person couldn't be saved!");
                }
            }
            else
            {
                _notyf.Error("Error: An Error Occurred! ");
            }

            return View();
        }
        
    }
}
