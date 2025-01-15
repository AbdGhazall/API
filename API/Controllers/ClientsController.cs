using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ClientsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public List<Client> GetClients() //(READ) RETURN LIST OF ALL CLIENTS INFO ORDERED FROM LARGE TO SMALL
        {
            return context.Clients.OrderByDescending(c => c.Id).ToList();
        }

        [HttpGet("{id}")]
        public IActionResult GetClient(int id) //(READ) RETURN ALL CLIENT INFO BASED ON SPECIFIC ID
        {
            var client = context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        //TO CREATE CLIENTS WE NEED TO CREATE Dto MODEL
        //WE MUST CHECK EMAIL VALIDATION BEACUSE IT MUST TO BE UNIQUE
        //(CREATE)
        [HttpPost]
        public IActionResult CreateClient(ClientDto clientDto)
        {
            var otherClient = context.Clients.FirstOrDefault(c=>c.Email == clientDto.Email);
            if (otherClient != null)
            {
                ModelState.AddModelError("Email", "The email address is already used");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }

            var client = new Client
            {
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName,
                Email = clientDto.Email,
                Phone = clientDto.Phone,
                Address = clientDto.Address,
                Status = clientDto.Status,
                CreatedAt = DateTime.Now,   
            };
            context.Clients.Add(client);
            context.SaveChanges();

            return Ok(client);
        }

        //UPDATE
        //SAME WITH CREATE WE NEED CHECK EMAIL VALIDATION
        //WE SHOULD CHECK FOR ID IS EXIST OR NOT ALSO
        [HttpPut("{id}")]
        public IActionResult EditClient(int id , ClientDto clientDto)
        {
            var otherClient = context.Clients.FirstOrDefault(c => c.Id!=id && c.Email == clientDto.Email);
            if (otherClient != null)
            {
                ModelState.AddModelError("Email", "The email address is already used");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }

            var client = context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            client.FirstName = clientDto.FirstName;
            client.LastName = clientDto.LastName;
            client.Email = clientDto.Email;
            client.Phone = clientDto.Phone ?? "";
            client.Address = clientDto.Address ?? "";
            client.Status = clientDto.Status;

            context.SaveChanges();

            return Ok(client);
        }

        //DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            var client = context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }
            context.Clients.Remove(client);
            context.SaveChanges();

            return Ok(client);
        }
    }
}
