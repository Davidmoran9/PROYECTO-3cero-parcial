using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class FacturacionDAL : CadenaDAL
    {
        // Listar todas las facturas
        public List<FacturacionCLS> ListarFacturacion()
        {
            List<FacturacionCLS> lista = new List<FacturacionCLS>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarFacturacion", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                FacturacionCLS factura = new FacturacionCLS
                                {
                                    Id = dr.GetInt32(0),
                                    PacienteId = dr.GetInt32(1),
                                    Monto = dr.GetDecimal(2),
                                    MetodoPago = dr.GetString(3),
                                    FechaPago = dr.GetDateTime(4)
                                };
                                lista.Add(factura);
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

        // Insertar una nueva factura
        public int GuardarFacturacion(FacturacionCLS obj)
        {
            int nuevoId = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspInsertarFacturacion", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@PacienteId", obj.PacienteId);
                        cmd.Parameters.AddWithValue("@Monto", obj.Monto);
                        cmd.Parameters.AddWithValue("@MetodoPago", obj.MetodoPago);
                        cmd.Parameters.AddWithValue("@FechaPago", obj.FechaPago);

                        // Parámetro de salida
                        SqlParameter paramId = new SqlParameter("@Id", SqlDbType.Int);
                        paramId.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(paramId);

                        cmd.ExecuteNonQuery();

                        // Obtener el valor del parámetro de salida
                        nuevoId = Convert.ToInt32(cmd.Parameters["@Id"].Value);
                    }
                }
                catch (Exception)
                {
                    nuevoId = 0;
                    throw;
                }
            }
            return nuevoId;
        }


        // Recuperar una factura específica
        public FacturacionCLS RecuperarFacturacion(int id)
        {
            FacturacionCLS factura = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarFacturacion", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (dr.Read())
                            {
                                factura = new FacturacionCLS
                                {
                                    Id = dr.GetInt32(0),
                                    PacienteId = dr.GetInt32(1),
                                    Monto = dr.GetDecimal(2),
                                    MetodoPago = dr.GetString(3),
                                    FechaPago = dr.GetDateTime(4)
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
            return factura;
        }

        // Editar una factura existente
        public int GuardarCambiosFacturacion(FacturacionCLS obj)
        {
            int filasAfectadas = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspActualizarFacturacion", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", obj.Id);
                        cmd.Parameters.AddWithValue("@PacienteId", obj.PacienteId);
                        cmd.Parameters.AddWithValue("@Monto", obj.Monto);
                        cmd.Parameters.AddWithValue("@MetodoPago", obj.MetodoPago);
                        cmd.Parameters.AddWithValue("@FechaPago", obj.FechaPago);

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

        // Eliminar una factura
        public int EliminarFacturacion(int id)
        {
            int filasAfectadas = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarFacturacion", cn))
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
