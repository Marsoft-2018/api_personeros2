using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API_personeros2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradosController : Controller
    {
        private readonly IConfiguration _configuration;
        public GradosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"Call listarGrados()";
            DataTable tabla = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ConexionBdt");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myComand = new MySqlCommand(query, myCon))
                {
                    myReader = myComand.ExecuteReader();
                    tabla.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(tabla);
        }
    }
}
