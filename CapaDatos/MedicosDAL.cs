using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class MedicosDAL : CadenaDAL
    {
        // Listar todos los médicos
        public List<MedicosCLS> ListarMedicos()
        {
            List<MedicosCLS> lista = new List<MedicosCLS>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarMedicos", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                MedicosCLS medico = new MedicosCLS
                                {
                                    Id = dr.GetInt32(0),
                                    Nombre = dr.GetString(1),
                                    Apellido = dr.GetString(2),
                                    EspecialidadId = dr.GetInt32(3),
                                    Telefono = dr.GetString(4),
                                    Email = dr.GetString(5)
                                };
                                lista.Add(medico);
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
        // Filtrar médicos por nombre y especialidad
        public List<MedicosCLS> FiltrarMedicos(string nombre, string especialidad)
        {
            List<MedicosCLS> lista = new List<MedicosCLS>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarMedicos", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", (object)nombre ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Especialidad", (object)especialidad ?? DBNull.Value);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                MedicosCLS medico = new MedicosCLS
                                {
                                    Id = dr.GetInt32(0),
                                    Nombre = dr.GetString(1),
                                    Apellido = dr.GetString(2),
                                    EspecialidadId = dr.GetInt32(3),
                                    Telefono = dr.GetString(4),
                                    Email = dr.GetString(5)
                                };
                                lista.Add(medico);
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

        // Recuperar un médico específico
        public MedicosCLS RecuperarMedico(int idMedico)
        {
            MedicosCLS medico = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarMedico", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", idMedico);

                        using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (dr.Read())
                            {
                                medico = new MedicosCLS
                                {
                                    Id = dr.GetInt32(0),
                                    Nombre = dr.GetString(1),
                                    Apellido = dr.GetString(2),
                                    EspecialidadId = dr.GetInt32(3),
                                    Telefono = dr.GetString(4),
                                    Email = dr.GetString(5)
                                };
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    medico = null;
                    throw;
                }
            }
            return medico;
        }

        // Insertar un nuevo médico
        public int GuardarMedico(MedicosCLS obj)
        {
            int idGenerado = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspInsertarMedico", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", obj.Apellido);
                        cmd.Parameters.AddWithValue("@EspecialidadId", obj.EspecialidadId);
                        cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
                        cmd.Parameters.AddWithValue("@Email", obj.Email);

                        SqlParameter outputIdParam = new SqlParameter("@idMedico", SqlDbType.Int)
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

        // Editar un médico existente
        public int GuardarCambiosMedico(MedicosCLS obj)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspActualizarMedico", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", obj.Id);
                        cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", obj.Apellido);
                        cmd.Parameters.AddWithValue("@EspecialidadId", obj.EspecialidadId);
                        cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
                        cmd.Parameters.AddWithValue("@Email", obj.Email);
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

        // Eliminar un médico
        public int EliminarMedico(int id)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarMedico", cn))
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
