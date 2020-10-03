using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WAppController : ControllerBase
    {

        public List<string> stringVal = new List<string>();
        public WAppController()
        {
            stringVal.Add("stuck was here");
        }

        // GET: api/<WApp_controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return stringVal;
        }

        // GET api/<WApp_controller>/5
        [HttpGet("{id}")]
        public string Get([FromRoute] int id)
        {
            if(stringVal.Count < id || id == 0)
            {
                return "The list we have is not long enough for the id you provided";
            }

            return stringVal[id - 1];
        }

        // POST api/<WApp_controller>
        [HttpPost]
        public bool Post([FromBody] ExampleDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.value))
            {
                return false;
            }
            stringVal.Add(dto.value);

            return true;
        }

        // PUT api/<WApp_controller>/5
        [HttpPut("{id}")]
        public string Put([FromRoute] int id, [FromBody] ExampleDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.value) || id > stringVal.Count)
            {
                throw new Exception("Value cannot be null or empty, or your ID is higher than what we have available.");
            }

            stringVal[id - 1] = dto.value;

            return dto.value;

        }

        // DELETE api/<WApp_controller>/5
        [HttpDelete("{id}")]
        public bool Delete([FromRoute] int id)
        {
            if(id > stringVal.Count || id == 0)
            {
                return false;
            }

            stringVal.RemoveAt(id - 1);

            return true;
        }
    }
}
