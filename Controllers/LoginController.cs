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

namespace API_personeros2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT e.id AS nombre_usuario,CONCAT(nombre1,' ',nombre2,' ',apellido1,' ',apellido2) AS nombre,e.rol AS rol,e.estado AS estado FROM estudiantes e";
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
        public JsonResult Post(Login datos)
        {
            string query = @"call cargarUsuario(@usuario,@clave)";
            DataTable tabla = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ConexionBdt");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myComand = new MySqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@usuario", datos.id);
                    myComand.Parameters.AddWithValue("@clave", datos.contrasena);
                    myReader = myComand.ExecuteReader();
                    tabla.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(tabla);
        }

        [HttpPut]
        public JsonResult Put(Candidato cand)
        {
            string query = @"UPDATE candidatos SET FOTO=@FOTO, alumnos_CODEST=@alumnos_CODEST, color=@color, partido=@partido WHERE Id=@Id";
            DataTable tabla = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ConexionBdt");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myComand = new MySqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@Id", cand.Id);
                    myComand.Parameters.AddWithValue("@FOTO", cand.FOTO);
                    myComand.Parameters.AddWithValue("@alumnos_CODEST", cand.alumnos_CODEST);
                    myComand.Parameters.AddWithValue("@color", cand.color);
                    myComand.Parameters.AddWithValue("@partido", cand.partido);
                    myReader = myComand.ExecuteReader();
                    tabla.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Login ingresado con ex");
        }

        [HttpDelete("{Id}")]
        public JsonResult Delete(string Id)
        {
            string query = @"DELETE FROM candidatos WHERE Id=@Id";
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
            return new JsonResult("Registro eliminado con éxito");
        }
    }
}