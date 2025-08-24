using System;
using Newtonsoft.Json;
namespace Proyecto.Models
{
    public class Usuario
    {
        [JsonProperty]
        public int Id { get; private set;}
        [JsonProperty]
        public string Username { get; private set;}
        [JsonProperty]
        public string Password { get; private set;}
        [JsonProperty]
        public string Nombre { get; private set;}
        [JsonProperty]
        public string Apellido { get; private set;}
        [JsonProperty]
        public string Foto { get; private set;}
        [JsonProperty]
        public DateTime UltimoLogin { get; private set;}

        public Usuario() 
        {

        }

        public Usuario(string Username, string Password, string Nombre, string Apellido, string Foto)
        {
            this.Username = Username;
            this.Password = Password;
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.Foto = Foto;
        }
    }
}