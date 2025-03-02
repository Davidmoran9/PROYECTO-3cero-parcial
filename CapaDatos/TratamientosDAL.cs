using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class TratamientosDAL : CadenaDAL
    {
        // Listar todos los tratamientos
        public List<TratamientosCLS> ListarTratamientos()
        {
            List<TratamientosCLS> lista = new List<TratamientosCLS>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarTratamientos", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                TratamientosCLS tratamiento = new TratamientosCLS
                                {
                                    Id = dr.GetInt32(0),
                                    PacienteId = dr.GetInt32(1),
                                    Descripcion = dr.GetString(2),
                                    Fecha = dr.GetDateTime(3),
                                    Costo = dr.GetDecimal(4)
                                };
                                lista.Add(tratamiento);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    lista = null;
                    throw;
                }
            }
            return lista;
        }

        // Insertar un nuevo tratamiento
        public int GuardarTratamiento(TratamientosCLS obj)
        {
            int idGenerado = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspInsertarTratamiento", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PacienteId", obj.PacienteId);
                        cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                        cmd.Parameters.AddWithValue("@Fecha", obj.Fecha);
                        cmd.Parameters.AddWithValue("@Costo", obj.Costo);

                        // Cambiamos ExecuteNonQuery() por ExecuteScalar() para obtener el ID generado
                        idGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (Exception)
                {
                    idGenerado = 0;
                    throw;
                }
            }
            return idGenerado;
        }


        // Recuperar un tratamiento específico
        public TratamientosCLS RecuperarTratamiento(int idTratamiento)
        {
            TratamientosCLS tratamiento = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarTratamiento", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", idTratamiento);

                        using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (dr.Read())
                            {
                                tratamiento = new TratamientosCLS
                                {
                                    Id = dr.GetInt32(0),
                                    PacienteId = dr.GetInt32(1),
                                    Descripcion = dr.GetString(2),
                                    Fecha = dr.GetDateTime(3),
                                    Costo = dr.GetDecimal(4)
                                };
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return tratamiento;
        }

        // Editar un tratamiento existente
        public int GuardarCambiosTratamiento(TratamientosCLS obj)
        {
            int filasAfectadas = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspActualizarTratamiento", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // 🔹 Verifica que se envía el ID correctamente
                        cmd.Parameters.AddWithValue("@Id", obj.Id);
                        cmd.Parameters.AddWithValue("@PacienteId", obj.PacienteId);
                        cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                        cmd.Parameters.AddWithValue("@Fecha", obj.Fecha);
                        cmd.Parameters.AddWithValue("@Costo", obj.Costo);

                        filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    filasAfectadas = 0;
                    throw;
                }
            }
            return filasAfectadas;
        }

        // Eliminar un tratamiento
        public int EliminarTratamiento(int id)
        {
            int filasAfectadas = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarTratamiento", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);

                        filasAfectadas = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    filasAfectadas = 0;
                    throw;
                }
            }
            return filasAfectadas;
        }

    }
}
