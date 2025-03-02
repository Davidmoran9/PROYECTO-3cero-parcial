using CapaEntidad;
using System;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class UsuarioDAL : CadenaDAL
    {
        // Método para verificar las credenciales del login
        public UsuarioCLS VerificarLogin(string correo, string contrasena)
        {
            UsuarioCLS usuario = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    string query = "SELECT Id, Correo, Contrasena, Rol FROM Usuarios WHERE Correo = @Correo AND Contrasena = @Contrasena";

                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        // Parámetros para evitar inyecciones SQL
                        cmd.Parameters.AddWithValue("@Correo", correo);
                        cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read()) // Si se encuentra el usuario
                            {
                                usuario = new UsuarioCLS
                                {
                                    Id = dr.GetInt32(0),
                                    Correo = dr.GetString(1),
                                    Contrasena = dr.GetString(2),
                                    Rol = dr.GetString(3)
                                };
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    usuario = null;
                    throw;
                }
            }

            return usuario;
        }
    }
}
