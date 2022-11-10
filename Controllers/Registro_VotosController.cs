using API_personeros2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace API_personeros2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Registro_VotosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public Registro_VotosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{Id}")]
        public JsonResult Get(int Id)
        {
            string query = @"CALL conteoVotosCandidato(@Id);";
            DataTable tabla = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ConexionBdt");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myComand = new MySqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@Id", Id);
                    myReader = myComand.ExecuteReader();
                    tabla.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(tabla);
        }
        [HttpGet()]
        public JsonResult Get()
        {
            string query = @"CALL listarConteo();";
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

        [HttpPost]
        public JsonResult Post(Registro_Votos voto)
        {
            Console.WriteLine("@id, @numero, @anho");
            string query = @"Call registrar_votos(@idEstudiante, @numero, @Anio)";
            DataTable tabla = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ConexionBdt");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myComand = new MySqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@idEstudiante", voto.idEstudiante);
                    myComand.Parameters.AddWithValue("@numero", voto.numero);
                    myComand.Parameters.AddWithValue("@Anio", voto.Anio);
                    myReader = myComand.ExecuteReader();

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Registro agregado con exito");
        }

    }
}
