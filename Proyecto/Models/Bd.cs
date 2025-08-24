using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace Proyecto.Models
{
    public static class Bd
    {
    private static string _connectionString = @"Server=A-PHZ2-CEO-13; Integrated Security=True;TrustServerCertificate=True";
   
    public static Usuario IniciarSesion (string Username, string Password)
    {
        Usuario miUsuario = null;
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE Username = @pUsername AND Password = @pPassword";
            miUsuario = connection.QueryFirstOrDefault<Usuario>(query, new { pUsername= Username, pPassword = Password });
        }
        return miUsuario;
    }

    public static bool Registrarse (Usuario usuario)
    //Me devuelve si pudo agregar al usuario o no
    {
        bool pudeAgregar = false;

        using(SqlConnection connection = new SqlConnection(_connectionString))
        {

            string query = "SELECT * FROM Usuarios WHERE Username = @pUsername";
            usuario = connection.QueryFirstOrDefault<Usuario>(query, new { pUsername = usuario.Username });

            if (usuario == null)
            {
                string otro = "INSERT INTO Usuarios (Username, Password, Nombre, Apellido, Foto, UltimoLogin) VALUES (@pUsername, @pPassword, @pNombre, @pApellido, @pFoto, @pUltimoLogin)";
                connection.Execute(query, new { pUsername = usuario.Username, pPassword = usuario.Password, pNombre = usuario.Nombre, pApellido = usuario.Apellido, pFoto = usuario.Foto, pUltimoLogin = usuario.UltimoLogin });
                pudeAgregar = true;
            }
        }
        
        return pudeAgregar;
    }

    public static List<Tarea> TraerTarea (int IdUsuario)
    {
    //Devuelve la lista de las tareas de ese usuario 
        List<Tarea> ListaTareas = null;
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Tareas WHERE IdUsuario = @pIdUsuarioTarea";
            ListaTareas = connection.Query<Tarea>(query, new { pIdUsuarioTarea = IdUsuario}).ToList();
        }
        return ListaTareas;
    }

    public static void CrearTarea (Tarea tareaCrear)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "INSERT INTO Tareas (Titulo, Descripcion, Fecha, Finalizada, IdUsuario) VALUES (@pTitulo, @pDescripcion, @pFecha, @pFinalizada, @pIdUsuario)";
            connection.Execute(query, new { pTitulo = tareaCrear.Titulo, pDescripcion = tareaCrear.Descripcion, pFecha = tareaCrear.Fecha, pFinalizada = tareaCrear.Finalizada, pIdUsuario = tareaCrear.IdUsuario});
        }
    }

    public static void EliminarTarea (int IdTareaEliminar)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "DELETE FROM Tareas WHERE Id = @IdTareaEliminar";
            connection.Execute(query, new { IdTareaEliminar });
        }
    }

    public static Tarea TraerTareaAEditar (int IdTareaEditar)
    {
        Tarea tareaEditar = null;
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Tareas WHERE Id = @IdTareaEditar";
            tareaEditar = connection.QueryFirstOrDefault<Tarea>(query, new { Id = IdTareaEditar});
        }
        return tareaEditar;
    }

    public static void ActualizarTarea (Tarea TareaActualizar)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE Tareas SET (Titulo, Descripcion, Fecha, Finalizada, IdUsuario) = (@pTitulo, @pDescripcion, @pFecha, @pFinalizada, @pIdUsuario) WHERE Tarea = @pTareaActualizar";
            connection.Execute(query, new {pTitulo = TareaActualizar.Titulo, pDescripcion = TareaActualizar.Descripcion, pFecha = TareaActualizar.Fecha, pFinalizada = TareaActualizar.Finalizada, pIdUsuario = TareaActualizar.IdUsuario });
        }
    }

    public static void FinalizarTarea (int IdTareaFinalizada)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE Tareas SET Finalizada = 1 @ WHERE Id = @IdTareaFinalizada";
            connection.Execute(query, new { Id = IdTareaFinalizada});
        }
    }

    public static void ActualizarFechaLogin (int IdUsuarioActualizarFecha)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE Usuarios SET UltimoLogin = GETDATE() WHERE Id = @IdUsuarioActualizarFecha";
            connection.Execute(query, new { Id = IdUsuarioActualizarFecha });
        }
    }
}
    
}