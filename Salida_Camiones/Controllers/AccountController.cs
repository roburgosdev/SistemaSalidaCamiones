using Evaluacion3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Evaluacion3.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public string ValidarLogin(string usuario, string password)
        {
            if (string.IsNullOrWhiteSpace(usuario))
            {
                return "Usuario no puede encontrarse vacío.";
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                return "Contraseña no puede estar vacía.";
            }

            using (EvaluacionContext bd = new EvaluacionContext())
            {
                var user = bd.Usuarios.Where(u => u.Cod_Usuario == usuario).FirstOrDefault();

                if (user == null)
                {
                    return "Usuario no se encuentra en el sistema.";
                }
                if (user.Password != password)
                {
                    return "Contraseña incorrecta.";
                }
                if (user.Id_Estado == 2)
                {
                    return "Usuario deshabilitado";
                }

                HttpContext.Session.SetString("Cod_Usuario", user.Cod_Usuario);
                HttpContext.Session.SetString("Nom_Persona", user.Nom_Persona);
                return "OK";
            }
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}