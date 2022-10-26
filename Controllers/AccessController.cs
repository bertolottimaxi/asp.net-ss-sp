using System;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using System.Data.SqlClient;


namespace WebApplication3.Controllers
{
    public class AccessController : Controller
    {
        static string conexión = "Data Source=DESKTOP-NCHDLHF\\SQLEXPRESS01; Initial Catalog=DB2;" +
            "Integrated Security= true;";
        static string query = "select idUsuario,pass from dbo.usuario";
        // GET: Access
        public ActionResult Login()
        {
            return View();
        }

        
        public static string Cifrar(string pwd)
        {
            StringBuilder aux = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(pwd));

                foreach (byte b in result)
                    aux.Append(b.ToString("x2"));
            }

         return aux.ToString();
        }

        [HttpPost]
        public ActionResult Login(User usuario)
        {
            ViewBag.Message = "Cargada";
            
            //    usuario.Pass = Cifrar(usuario.Pass);
            

            using (SqlConnection cnx = new SqlConnection(conexión))
            {
                SqlCommand cmd = new SqlCommand("ValidarUsuario", cnx);
                cmd.Parameters.AddWithValue("Id", usuario.IdUsuario);
                cmd.Parameters.AddWithValue("Pass", usuario.Pass);
                cmd.CommandType = CommandType.StoredProcedure;

                cnx.Open();
                usuario.IdUsuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            }
            ViewBag.Message = " ";

            if (usuario.IdUsuario != 0)
            {
                Session["usuario"] = usuario;
                ViewBag.Message = "Bien";                
                return RedirectToAction("Menu", "Home");
            }
            else
            {
                ViewBag.Message = "El usuario o la contraseña son incorrectos";
                return View();
            }

          

        }
        
    }
}
