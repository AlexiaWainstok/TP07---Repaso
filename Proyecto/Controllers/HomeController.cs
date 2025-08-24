using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
       return View ("Index");

    }

    public IActionResult CargarTareas()
    {
        int IdUsuario = int.Parse(HttpContext.Session.GetString("Usuario"));
        ViewBag.ListaDeTareas = Bd.TraerTarea(IdUsuario);
        return View("VerTareas");
    }

    [HttpPost]
    public IActionResult CrearTarea(string Titulo, string Descripcion, DateTime Fecha, bool Finalizada)
    {
        int IdUsuario = int.Parse(HttpContext.Session.GetString("Usuario"));
        Tarea nuevaTarea = new Tarea(Titulo, Descripcion, DateTime.Now, Finalizada, IdUsuario);
        Bd.CrearTarea(nuevaTarea);
        return RedirectToAction("CargarTareas");
    }

    [HttpPost]
    public IActionResult CrearTareaGuardar()
    {
        return View("CrearTarea");
    }

    public IActionResult FinalizarTarea(int IdTareaFinalizada)
    {
        Bd.FinalizarTarea(IdTareaFinalizada);
        return RedirectToAction("CargarTarea");
    }

    public IActionResult EliminarTarea(int IdTareaEliminar)
    {
        Bd.EliminarTarea(IdTareaEliminar);
        return RedirectToAction("CargarTarea");
    }

  [HttpPost]
    public IActionResult EditarTarea(string Titulo, string Descripcion, DateTime Fecha, bool Finalizada)
    {
        int IdUsuario = int.Parse(HttpContext.Session.GetString("Usuario"));
        Tarea tareaNueva = new Tarea(Titulo, Descripcion, Fecha, Finalizada, IdUsuario);
        Bd.ActualizarTarea(tareaNueva);
        return RedirectToAction("CargarTarea");
    }

    public IActionResult EditarTareaGuardar(int id)
    {
        ViewBag.Tarea = Bd.TraerTarea(id);
        return View("ModificarTarea");
    }


}
