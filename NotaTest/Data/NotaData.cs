using NotaTest.Models;
using System.Data.SqlClient;
using System.Data;

namespace NotaTest.Datos
{
    public class NotaData
    {

        public List<NotaModel> List()
        {
            var oList = new List<NotaModel>();

            var con = new Conexion();

            using (var conexion = new SqlConnection(con.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_list", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oList.Add(new NotaModel()
                        {

                            IdNota = Convert.ToInt32(dr["IdNota"]),
                            Titulo = dr["Titulo"].ToString(),
                            Cuerpo = dr["Cuerpo"].ToString(),
                            Fecha = Convert.ToDateTime(dr["Fecha"]),
                        });
                    }
                }
            }

            return oList;
        }



        public NotaModel ObtenerPorId(int IdNota)
        {
            var oNota = new NotaModel();

            var con = new Conexion();

            using (var conexion = new SqlConnection(con.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerPorId", conexion);
                cmd.Parameters.AddWithValue("IdNota", IdNota);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        oNota.IdNota = Convert.ToInt32(dr["IdNota"]);
                        oNota.Titulo = dr["Titulo"].ToString();
                        oNota.Cuerpo = dr["Cuerpo"].ToString();
                        oNota.Fecha = Convert.ToDateTime(dr["Fecha"]);
                    }
                }
            }

            return oNota;
        }

        public NotaModel ObtenerPorTitulo(string titulo)
        {
            var oNota = new NotaModel();

            var con = new Conexion();

            using (var conexion = new SqlConnection(con.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerPorId", conexion);
                cmd.Parameters.AddWithValue("Titulo", titulo);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        oNota.IdNota = Convert.ToInt32(dr["IdNota"]);
                        oNota.Titulo = dr["Titulo"].ToString();
                        oNota.Cuerpo = dr["Cuerpo"].ToString();
                        oNota.Fecha = Convert.ToDateTime(dr["Fecha"]);
                    }
                }
            }

            return oNota;
        }


        public bool Guardar(NotaModel oNota)
        {
            bool respuesta;

            try
            {
                var con = new Conexion();

                using (var conexion = new SqlConnection(con.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Guardar", conexion);
                    cmd.Parameters.AddWithValue("Titulo", oNota.Titulo);
                    cmd.Parameters.AddWithValue("Cuerpo", oNota.Cuerpo);
                    cmd.Parameters.AddWithValue("Fecha", oNota.Fecha);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                respuesta = false;
            }

            return respuesta;
        }


        public bool Editar(NotaModel oNota)
        {
            bool respuesta;

            try
            {
                var con = new Conexion();

                using (var conexion = new SqlConnection(con.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Editar", conexion);
                    cmd.Parameters.AddWithValue("IdNota", oNota.IdNota);
                    cmd.Parameters.AddWithValue("Titulo", oNota.Titulo);
                    cmd.Parameters.AddWithValue("Cuerpo", oNota.Cuerpo);
                    cmd.Parameters.AddWithValue("Fecha", oNota.Fecha);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                respuesta = false;
            }

            return respuesta;
        }

        public bool Eliminar(int IdNota)
        {
            bool respuesta;

            try
            {
                var con = new Conexion();

                using (var conexion = new SqlConnection(con.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Eliminar", conexion);
                    cmd.Parameters.AddWithValue("IdNota", IdNota);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                respuesta = false;
            }

            return respuesta;
        }
    }
}
