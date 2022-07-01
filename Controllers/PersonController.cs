using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using enovation.Models;

namespace enovation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
      
        private readonly DataContext _context;

        public PersonController(DataContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Person>>> Get()
        {
            return Ok(await _context.Persons.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult <Person>>  Get(int id)
        {
            var hero = await _context.Persons.FindAsync(id);

            if(hero == null)
                return BadRequest("Not found");
            
            return Ok(hero);
        }


        [HttpPost]
        public async Task<ActionResult<List<Person>>> AddPerson(Person Person)
        {
            _context.Persons.Add(Person);
            await _context.SaveChangesAsync();

            return Ok(await _context.Persons.ToArrayAsync());

        }

        [HttpPut]
        public async Task<ActionResult<List<Person>>> UpdatePerson(Person request)
        {
            var dbperson = await _context.Persons.FindAsync(request.Id);

            dbperson.Name = request.Name;
            dbperson.FirstName = request.FirstName; 
            dbperson.LastName = request.LastName;
            dbperson.Place = request.Place;

            await _context.SaveChangesAsync();
            return Ok(await _context.Persons.ToArrayAsync());

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Person>>> Delete(int id)
        {
            var dbperson = await _context.Persons.FindAsync(id);

            _context.Persons.Remove(dbperson);
            await _context.SaveChangesAsync();
            return Ok(await _context.Persons.ToArrayAsync());
        }


    }
}
