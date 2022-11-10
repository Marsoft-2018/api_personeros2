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
    public class EstudiantesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EstudiantesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"call listarEstudiantes()";
            DataTable tabla = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ConexionBdt");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(MySqlCommand myComand=new MySqlCommand(query, myCon))
                {
                    myReader = myComand.ExecuteReader();
                    tabla.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(tabla);                
        }

        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            string query = @"call cargarEStudiante(@id)";
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
        public JsonResult Post(Estudiantes est)
        {
            string query = @"CALL agregarEstudiante(@id, @grado, @grupo, @apellido1, @apellido2, @nombre1, @nombre2, @sexo)";
            DataTable tabla = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ConexionBdt");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myComand = new MySqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@id", est.id);
                    myComand.Parameters.AddWithValue("@grado", est.grado);
                    myComand.Parameters.AddWithValue("@grupo", est.grupo);
                    myComand.Parameters.AddWithValue("@apellido1", est.apellido1);
                    myComand.Parameters.AddWithValue("@apellido2", est.apellido2);
                    myComand.Parameters.AddWithValue("@nombre1", est.nombre1);
                    myComand.Parameters.AddWithValue("@nombre2", est.nombre2);
                    myComand.Parameters.AddWithValue("@sexo", est.sexo);
                    myReader = myComand.ExecuteReader();
                    tabla.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Registro agregado con exito");
        }

        [HttpPut]
        public JsonResult Put(Estudiantes est)
        {
            string query = @"Call modificarEstudiante(@grado, @grupo, @apellido1, @apellido2, @nombre1, @nombre2, @sexo, @id)";
            DataTable tabla = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ConexionBdt");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myComand = new MySqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@id", est.id);
                    myComand.Parameters.AddWithValue("@grado", est.grado);
                    myComand.Parameters.AddWithValue("@grupo", est.grupo);
                    myComand.Parameters.AddWithValue("@apellido1", est.apellido1);
                    myComand.Parameters.AddWithValue("@apellido2", est.apellido2);
                    myComand.Parameters.AddWithValue("@nombre1", est.nombre1);
                    myComand.Parameters.AddWithValue("@nombre2", est.nombre2);
                    myComand.Parameters.AddWithValue("@sexo", est.sexo);
                    myReader = myComand.ExecuteReader();
                    tabla.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Registro modificado con exito");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(string id)
        {
            string query = @"Call eliminarEstudiante(@id)";
            DataTable tabla = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ConexionBdt");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myComand = new MySqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@id", id);
                    myReader = myComand.ExecuteReader();
                    tabla.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Registro eliminado con éxito");
        }
    }
}
