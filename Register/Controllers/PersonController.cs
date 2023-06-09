﻿using AspNetCoreHero.ToastNotification.Abstractions;
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
            if (ModelState.IsValid)
            {
                if (!IdNumberExists(person.IdNumber))
                {

                    person.PersonId = Helper.Utility.GenerateGuid();

                    person.IsActive = true;

                    person.CreatedOn = Helper.Utility.CurrentDatetime();

                    person.Createdby = "Mbuso and Nqobile";

                    await _context.Persons.AddAsync(person);

                    var rc = await _context.SaveChangesAsync();

                    if (rc > 0)
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
                    _notyf.Success("ID number already Exists");
                }

            }

            else
            {
                _notyf.Error("Error: An Error Occurred! ");
            }

            return View();
        }

       

        [HttpGet]
        public async Task<IActionResult>EditPerson(Guid PersonId)
        {
            if(PersonId == Guid.Empty)
            {
                return NotFound();
            }

            var person = await _context.Persons.FirstOrDefaultAsync(x =>x.PersonId == PersonId);
            if(person == null)
            {
                return NotFound();
            }
    
            return View(person);
        }
        
        public async Task<IActionResult> EditPerson(Person person)
        {
            Person modify = new Person();

            try
            {
                modify = await _context.Persons.FirstOrDefaultAsync(x => x.PersonId == person.PersonId);

                if (person != null)
                {
                    modify.Name = person.Name;

                    modify.LastName = person.LastName;

                    modify.IdNumber = person.IdNumber;

                    modify.Gender = person.Gender;

                    modify.Title = person.Title;

                }

                await _context.SaveChangesAsync();

                _notyf.Success("Person has been successfully edited!");

            }
            catch(Exception)
            {
                throw new Exception("Error: Person could not be edited");
            }
            
            return View();
        }

        public async Task<IActionResult> PersonInfo (Guid PersonId)
        {
            if(PersonId == Guid.Empty)
            {
                return NotFound() ;
            }
            var person = await _context.Persons.FirstOrDefaultAsync(x => x.PersonId == PersonId);

            if(person != null)
            {
               person.IdNumber = person.IdNumber;

                person.Gender = person.Gender;

                person.Title = person.Title;

                person.LastName = person.LastName;

                person.Name = person.Name;

                person.CreatedOn = person.CreatedOn;

                person.Createdby = person.Createdby;
            

            }

            ViewData["IdNumber"] = person.IdNumber;

            ViewData["LastName"] = person.LastName;

            ViewData["Name"] = person.Name;

            ViewData["Gender"] = person.Gender;

            ViewData["Title"] = person.Title;

            ViewData["CreatedOn"] = person.CreatedOn;

            ViewData["CreatedBy"] = person.Createdby;

  
            return View(person);
        }

        [HttpGet]
        public async Task<IActionResult> DeletePerson(Guid PersonId)
        {
            if(PersonId == Guid.Empty)
            { 
                return NotFound() ;
            }
            var person = await _context.Persons.FirstOrDefaultAsync(x => x.PersonId == PersonId);
            
            if(person == null) 
            {
                return NotFound(); 
            }
            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult>DeletePerson(Person person)
        {
            Person delete = new Person();
            try
            {
                delete = await _context.Persons.FirstOrDefaultAsync(x => x.PersonId == person.PersonId);

                if(delete != null)
                {
                    _context.Remove(delete);

                    await _context.SaveChangesAsync();
                }

            }catch(Exception)
            {
                throw new Exception("An error occurred");
            }
            return View();
        }


        private bool IdNumberExists(string idNumber)
        {
            return _context.Persons.Any(x => x.IdNumber == idNumber);
        }

    }
}
