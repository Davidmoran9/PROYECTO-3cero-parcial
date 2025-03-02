using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class EspecialidadesDAL : CadenaDAL
    {
        // Listar todas las especialidades
        public List<EspecialidadesCLS> ListarEspecialidades()
        {
            List<EspecialidadesCLS> lista = new List<EspecialidadesCLS>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarEspecialidades", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                EspecialidadesCLS especialidad = new EspecialidadesCLS
                                {
                                    Id = dr.GetInt32(0),
                                    Nombre = dr.GetString(1)
                                };
                                lista.Add(especialidad);
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

        // Recuperar una especialidad específica
        public EspecialidadesCLS RecuperarEspecialidad(int id)
        {
            EspecialidadesCLS especialidad = null;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarEspecialidad", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (dr.Read())
                            {
                                especialidad = new EspecialidadesCLS
                                {
                                    Id = dr.GetInt32(0),
                                    Nombre = dr.GetString(1)
                                };
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    especialidad = null;
                    throw;
                }
            }
            return especialidad;
        }


        // Insertar una nueva especialidad
        public int GuardarEspecialidad(EspecialidadesCLS obj)
        {
            int idGenerado = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspInsertarEspecialidad", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);

                        SqlParameter outputIdParam = new SqlParameter("@idEspecialidad", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputIdParam);
                        cmd.ExecuteNonQuery();

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

        public int GuardarCambiosEspecialidad(EspecialidadesCLS obj)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspActualizarEspecialidad", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", obj.Id);
                        cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);

                        // 📌 Ver cuántas filas se afectan
                        rpta = cmd.ExecuteNonQuery();
                        if (rpta == 0)
                        {
                            throw new Exception($"No se encontró la especialidad con ID {obj.Id}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en DAL: " + ex.Message);
                }
            }
            return rpta;
        }



        // Eliminar una especialidad
        public int EliminarEspecialidad(int id)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarEspecialidad", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return (filasAfectadas > 0) ? 1 : 0;
                    }
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
    }
}
