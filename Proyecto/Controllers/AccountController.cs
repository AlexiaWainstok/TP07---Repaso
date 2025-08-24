using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Login (string Username, string Password)
    {
        Usuario usuario = Bd.IniciarSesion(Username, Password);

        if (usuario == null)
        {
            return View("Registrarse"); 
        }
        else
        {
         HttpContext.Session.SetString("Usuario", Objeto.ObjectToString(usuario));
         Bd.ActualizarFechaLogin(usuario.Id);
         return RedirectToAction("CargarTareas", "Home");
        }
    }

    public IActionResult Login ()
    {
        return View ("IniciarSesion");
    }

    [HttpPost]
    public IActionResult Registrar(string Username, string Password, string Nombre, string Apellido, string Foto)
    {
        Usuario nuevoUsuario = new Usuario(Username, Password, Nombre, Apellido, Foto);
        if (!Bd.Registrarse(nuevoUsuario))
        {
            return View("Registrarse");
        }

        else
        {
          HttpContext.Session.SetString("Usuario", nuevoUsuario.Id.ToString());
          return RedirectToAction("CargarTareas", "Home");
        }
    }

    public IActionResult Registrar()
    {
     return View ("Registrarse");
    }
       
}


