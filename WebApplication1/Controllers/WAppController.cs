using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO;
using WebApplication1.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WAppController : ControllerBase
    {
        private readonly SqliteQueries _sql = new SqliteQueries();

        public WAppController(SqliteQueries sql)
        {
            _sql = sql;
        }

        // GET: api/<WApp_controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _sql.GetAll();
        }

        // GET api/<WApp_controller>/5
        [HttpGet("{id}")]
        public string Get([FromRoute] int id)
        {
            if(_sql.GetAll().Count < id || id == 0)
            {
                return "The list we have is not long enough for the id you provided";
            }

            return _sql.GetSingle(id);
        }

        // POST api/<WApp_controller>
        [HttpPost]
        public bool Post([FromBody] ExampleDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.value))
            {
                return false;
            }
            _sql.AddValues(dto.value);

            return true;
        }

        //PUT api/<WApp_controller>/5
        [HttpPut("{id}")]
        public string Put([FromRoute] int id, [FromBody] ExampleDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.value) || id > _sql.GetAll().Count)
            {
                throw new Exception("Value cannot be null or empty, or your ID is higher than what we have available.");
            }

            _sql.UpdateValue(id, dto.value);

            return dto.value;

        }

        // DELETE api/<WApp_controller>/5
        [HttpDelete("{id}")]
        public bool Delete([FromRoute] int id)
        {
            if (id > _sql.GetAll().Count || id == 0)
            {
                return false;
            }

            _sql.DeleteValue(id);

            return true;
        }
    }
}
