using System;
using Newtonsoft.Json;
namespace Proyecto.Models
{
    public class Tarea
    {
        [JsonProperty]
        public int Id { get; private set;}
        [JsonProperty]
        public string Titulo { get; private set;}
        [JsonProperty]
        public string Descripcion { get; private set;}
        [JsonProperty]
        public DateTime Fecha { get; private set;}
        [JsonProperty]
        public bool Finalizada { get; private set;}
        [JsonProperty]
        public int IdUsuario { get; private set;}

        public Tarea() 
        {

        }

        public Tarea(string Titulo, string Descripcion, DateTime Fecha, bool Finalizada, int IdUsuario)
        {
            this.Titulo = Titulo;
            this.Descripcion = Descripcion;
            this.Fecha = Fecha;
            this.Finalizada = Finalizada;
            this.IdUsuario = IdUsuario;
        }
    }
}