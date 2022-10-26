using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Autorizaciones;
using WebApplication3.Models;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication3.Controllers
{
    [ValidateSession]
    public class HomeController : Controller
    {
        static string conexión = "Data Source=DESKTOP-NCHDLHF\\SQLEXPRESS01; Initial Catalog=DB2;" +
            "Integrated Security= true;";
        
        public ActionResult Index()
        {
            
            return View();
        }

        
        public ActionResult Ingresar()
        {
            return View();
        }
        public ActionResult Menu()
        {            
            return View();
        }

        public ActionResult CloseSession()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login", "Access");
        }

        [HttpPost]
        public ActionResult Ingresar(Agente agente)
        {
            using (SqlConnection cnx = new SqlConnection(conexión))
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand("AgregarAgente", cnx);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Dni", agente.Dni);
                cmd.Parameters.AddWithValue("NombreyApellido", agente.NombreyApellido);
                ViewBag.message = String.Format("{0}-{1}", cmd.Parameters[0], cmd.Parameters[1]);
                cmd.ExecuteNonQuery();

                

            }


            return View(agente);
        }

        public ActionResult Buscar(string criterio,string buscando)
        {
            List<Agente> ListaAgentes = new List<Agente>();
            string query = "select * from dbo.personal_info";

            if (!((buscando is null)||(buscando==" ")) )
            {

                if (criterio == "DNI")
                    query = String.Format("select * from dbo.personal_info where dni LIKE '%{0}%'", buscando);
                else if (criterio == "Nombre y Apellido")
                    query = String.Format("select * from dbo.personal_info where nombreYapellido LIKE '%{0}%'", buscando);
            }

            using (SqlConnection cn = new SqlConnection(conexión))
            {
                SqlCommand cmd = new SqlCommand(query, cn);

                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ListaAgentes.Add(
                            new Agente(Convert.ToInt32(reader[0]), reader[1].ToString()));

                    }
                    ViewBag.Lista = ListaAgentes;
                }
            }

            return View();
        }
    }
}