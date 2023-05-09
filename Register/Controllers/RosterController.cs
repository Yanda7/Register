using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Register.Data;
using Register.Models;

namespace Register.Controllers
{
    public class RosterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INotyfService _notyf;

        public RosterController(ApplicationDbContext context, INotyfService notyf)
        {
            _context = context;

            _notyf = notyf;
        }


        [HttpGet]
        public async Task<IActionResult> AddRoster()
        {  
            var person = await _context.Persons.ToListAsync();

            IEnumerable<SelectListItem> GetPerson = from x in person

                                                    where x.IsActive == true

                                                    select new SelectListItem
                                                    {
                                                        Value = x.PersonId.ToString(),

                                                        Text = $"{x.Name},{x.LastName}", 
            
                                                    };
            ViewBag.PersonId = new SelectList(GetPerson, "Value", "Text");

            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoster(Roster roster)
        {
            if (ModelState.IsValid)
            {
                roster.RosterId = Helper.Utility.GenerateGuid();

                roster.IsActive = true;

                roster.CreatedOn = Helper.Utility.CurrentDatetime();

                roster.Createdby = "Nqobile and Mbuso";
                
                await _context.AddAsync(roster);

                var rc = await _context.SaveChangesAsync();

                if(rc > 0)
                {
                    _notyf.Success("Roster has been successfully saved");
                }else
                {
                    _notyf.Error("Roster couldn't be saved");
                }
            }else
            {
                _notyf.Error("An Error occurred");
            }

            return View();
        }
    }
}
