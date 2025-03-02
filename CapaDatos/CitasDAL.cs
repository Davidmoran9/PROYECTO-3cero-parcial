using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CitasDAL : CadenaDAL
    {
        // Listar todas las citas
        public List<CitasCLS> ListarCitas()
        {
            List<CitasCLS> lista = new List<CitasCLS>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarCitas", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                CitasCLS cita = new CitasCLS
                                {
                                    Id = dr.IsDBNull(0) ? 0 : dr.GetInt32(0),
                                    PacienteId = dr.IsDBNull(1) ? 0 : dr.GetInt32(1),
                                    MedicoId = dr.IsDBNull(2) ? 0 : dr.GetInt32(2),
                                    FechaHora = dr.IsDBNull(3) ? DateTime.MinValue : dr.GetDateTime(3),
                                    Estado = dr.IsDBNull(4) ? "" : dr.GetString(4)
                                };

                                lista.Add(cita);
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

        // Filtrar citas
        public List<CitasCLS> FiltrarCitas(int? pacienteId, int? medicoId, string estado)
        {
            List<CitasCLS> lista = new List<CitasCLS>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarCitas", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PacienteId", (object)pacienteId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MedicoId", (object)medicoId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Estado", (object)estado ?? DBNull.Value);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                CitasCLS cita = new CitasCLS
                                {
                                    Id = dr.GetInt32(0),
                                    PacienteId = dr.GetInt32(1),
                                    MedicoId = dr.GetInt32(2),
                                    FechaHora = dr.GetDateTime(3),
                                    Estado = dr.GetString(4)
                                };
                                lista.Add(cita);
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

        // Insertar nueva cita
        public int GuardarCita(CitasCLS obj)
        {
            int idGenerado = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspInsertarCita", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PacienteId", obj.PacienteId);
                        cmd.Parameters.AddWithValue("@MedicoId", obj.MedicoId);
                        cmd.Parameters.AddWithValue("@FechaHora", obj.FechaHora);
                        cmd.Parameters.AddWithValue("@Estado", obj.Estado);


                        // Parámetro de salida para obtener el ID de la cita generada
                        SqlParameter outputIdParam = new SqlParameter("@idCita", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputIdParam);

                        cmd.ExecuteNonQuery();

                        // Obtener el ID generado
                        idGenerado = Convert.ToInt32(outputIdParam.Value);
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


        // Recuperar una cita específica
        public CitasCLS RecuperarCita(int idCita)
        {
            CitasCLS cita = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarCita", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", idCita);

                        using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (dr.Read())
                            {
                                cita = new CitasCLS
                                {
                                    Id = dr.IsDBNull(0) ? 0 : dr.GetInt32(0),
                                    PacienteId = dr.IsDBNull(1) ? 0 : dr.GetInt32(1),
                                    MedicoId = dr.IsDBNull(2) ? 0 : dr.GetInt32(2),
                                    FechaHora = dr.IsDBNull(3) ? DateTime.MinValue : dr.GetDateTime(3),
                                    Estado = dr.IsDBNull(4) ? "" : dr.GetString(4)
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error en RecuperarCita: " + ex.Message);
                    throw;
                }
            }
            return cita;
        }


        // Editar cita existente
        public int GuardarCambiosCita(CitasCLS obj)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspActualizarCita", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idCita", obj.Id); // CORREGIDO
                        cmd.Parameters.AddWithValue("@PacienteId", obj.PacienteId);
                        cmd.Parameters.AddWithValue("@MedicoId", obj.MedicoId);
                        cmd.Parameters.AddWithValue("@FechaHora", obj.FechaHora);
                        cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                        rpta = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    rpta = 0;
                    throw;
                }
            }
            return rpta;
        }

        // Eliminar cita
        public int EliminarCita(int id)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarCita", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return (filasAfectadas > 0) ? 1 : 0;  // 🔹 Ahora devuelve 1 si eliminó algo
                    }
                }
                catch (Exception)
                {
                    return 0; // 🔹 Si hay error, devuelve 0
                }
            }
        }


    }
}
