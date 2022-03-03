
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SecureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        [ResponseCache(NoStore = true, Duration = 0)]
        public async Task<ActionResult<IEnumerable<string>>> GetAsync()
        {
            string[] list = await GetDateFromSQL();

            return list;
        }

        private async Task<string[]> GetDateFromSQL()
        {
            string[] items = new string[200];

            string CS = "<connectionstring>";
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP (100) [FirstName] FROM [SalesLT].[Customer]", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                int i = 0;
                SqlDataReader rdr = await cmd.ExecuteReaderAsync();
                while (rdr.Read())
                {

                    items[i] = rdr["FirstName"].ToString();
                    i++;
                }
            }
            items[199] = "Networking!";

            return items;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
