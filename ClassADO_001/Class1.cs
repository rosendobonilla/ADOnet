using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ClassADO_001
{
    public class UsaSql
    {
        public string CadenaConexion { get; set; }

        public SqlConnection Conectar(ref string mensaje) {
            SqlConnection carretera = new SqlConnection();
            carretera.ConnectionString = CadenaConexion;

            try {
                carretera.Open();
                mensaje = "Conexión establecida";
            } catch (Exception e) {
                carretera = null;
                mensaje = "ERROR: " + e.Message;
            }
            return carretera;
        }

        public bool modificarBD(SqlConnection con, string operacionSql, ref string mensaje) {
            bool salida = false;
            SqlCommand vocho = new SqlCommand();

            if(con != null) {
                vocho.Connection = con;
                vocho.CommandText = operacionSql;

                try {
                    vocho.ExecuteNonQuery();
                    mensaje = "Modificacion correcta";
                    salida = true;
                }catch(Exception e) {
                    mensaje = "Error: " + e.Message;
                    salida = false;
                } finally {
                    con.Close();
                    con.Dispose();
                }

            } else {
                mensaje = "Conexión cerrada";
            }

            return salida;
        }

        public SqlDataReader ConsultaReader(SqlConnection abierta, string consulta, ref string mensaje) {
            SqlCommand spark = new SqlCommand();
            SqlDataReader contenedor = null;
            if (abierta != null) {
                spark.Connection = abierta;
                spark.CommandText = consulta;
                try {
                    contenedor = spark.ExecuteReader();
                    mensaje = "Consulta correcta";
                } catch (Exception h) {
                    contenedor = null;
                    mensaje = "Error: " + h.Message;
                }
            } else {
                mensaje = "No hay conexión a la base de datos";
            }
            return contenedor;
        }

        public DataSet ConsultaDataset(SqlConnection abierta, string consulta, ref string mensaje) {
            SqlCommand spark = new SqlCommand();
            DataSet datos = new DataSet();
            SqlDataAdapter trailer = new SqlDataAdapter();

            if (abierta != null) {

                spark.Connection = abierta;
                spark.CommandText = consulta;
                trailer.SelectCommand = spark;

                try {
                    trailer.Fill(datos);
                    mensaje = "Consulta correcta";
                } catch (Exception h) {
                    datos = null;
                    mensaje = "Error: " + h.Message;
                }

            } else {
                datos = null;
                mensaje = "No hay conexión a la base de datos";
            }
            return datos;
        }

        public int ConsultaEscalar(SqlConnection abierta, string consulta, SqlTransaction transaction,ref string mensaje) {
            SqlCommand cmd = new SqlCommand();
            int valor = -1;

            if (abierta != null) {

                cmd = new SqlCommand(consulta, abierta, transaction);

                valor = (int)cmd.ExecuteScalar();

            } else {
                valor = -1;
                mensaje = "No hay conexión a la base de datos";
            }

            return valor;
        }



    }
}
