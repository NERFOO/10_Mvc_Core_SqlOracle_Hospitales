using _10_Mvc_Core_SqlOracle_Hospitales.Models;
using System.Data;
using System.Data.SqlClient;

#region
/*
 CREATE PROCEDURE SP_CREATE_HOSPITAL
(@HOSPITALCOD INT, @NOMBRE NVARCHAR(50), @DIRECCION NVARCHAR(50), @TELEFONO NVARCHAR(50), @NUM_CAMA INT)
AS
	INSERT INTO HOSPITAL VALUES(@HOSPITALCOD, @NOMBRE, @DIRECCION, @TELEFONO, @NUM_CAMA)
GO


CREATE PROCEDURE SP_DELETE_HOSPITAL
(@HOSPITALCOD INT)
AS
	DELETE FROM HOSPITAL WHERE HOSPITAL_COD = @HOSPITALCOD
GO

CREATE PROCEDURE SP_UPDATE_HOSPITAL
(@HOSPITALCOD INT, @NOMBRE NVARCHAR(50), @DIRECCION NVARCHAR(50), @TELEFONO NVARCHAR(50), @NUM_CAMA INT)
AS
	UPDATE HOSPITAL SET NOMBRE = @NOMBRE, DIRECCION = @DIRECCION, TELEFONO = @TELEFONO, NUM_CAMA = @NUM_CAMA WHERE HOSPITAL_COD = @HOSPITALCOD
GO
 */
#endregion

namespace _10_Mvc_Core_SqlOracle_Hospitales.Repositories
{
    public class RepositoryHospitalesSql : IRepositoryHospitales
    {
        //Sql
        SqlConnection connection;
        SqlCommand command;

        //Linq
        DataTable tablaHospitales;

        public RepositoryHospitalesSql()
        {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=sa;Password=MCSD2022";

            //Sql
            this.connection = new SqlConnection(connectionString);
            this.command = new SqlCommand();
            this.command.Connection = this.connection;

            //Linq
            string consulta = "SELECT * FROM HOSPITAL";
            SqlDataAdapter adapter = new SqlDataAdapter(consulta, connectionString);
            this.tablaHospitales = new DataTable();
            adapter.Fill(tablaHospitales);
        }

        public List<Hospital> GetHospitales()
        {
            var consulta = from datos in this.tablaHospitales.AsEnumerable()
                           select datos;

            List<Hospital> hospitales = new List<Hospital>();

            foreach(var row in consulta)
            {
                Hospital hospital = new Hospital
                {
                    HospitalCod = row.Field<int>("HOSPITAL_COD"),
                    Nombre = row.Field<string>("NOMBRE"),
                    Direccion = row.Field<string>("DIRECCION"),
                    Telefono = row.Field<string>("TELEFONO"),
                    NumCama = row.Field<int>("NUM_CAMA")
                };
                hospitales.Add(hospital);
            }

            return hospitales;
        }

        //public List<string> GetNombreHospital()
        //{
        //    var consulta = (from datos in this.tablaHospitales.AsEnumerable()
        //                      select datos.Field<string>("NOMBRE")).Distinct();

        //    List<string> hospitales = new List<string>();

        //    foreach(string row in consulta)
        //    {
        //        hospitales.Add(row);
        //    }

        //    return hospitales;
        //}

        public Hospital FindHospital(int hospitalCod)
        {
            var consulta = from datos in this.tablaHospitales.AsEnumerable()
                           where datos.Field<int>("HOSPITAL_COD") == hospitalCod
                           select datos;

            var row = consulta.First();

            Hospital hospital = new Hospital
            {
                HospitalCod = row.Field<int>("HOSPITAL_COD"),
                Nombre = row.Field<string>("NOMBRE"),
                Direccion = row.Field<string>("DIRECCION"),
                Telefono = row.Field<string>("TELEFONO"),
                NumCama = row.Field<int>("NUM_CAMA")
            };

            return hospital;
        }

        public int GetMaximoHospitalCod()
        {
            var consulta = (from datos in this.tablaHospitales.AsEnumerable()
                            select datos).Max(x => x.Field<int>("HOSPITAL_COD") + 1 );

            return consulta;
        }

        public void CreateHospital(string nombre, string direccion, string telefono, int numCama)
        {
            int maximo = this.GetMaximoHospitalCod();

            SqlParameter paramHospitalCod = new SqlParameter("@HOSPITALCOD", maximo);
            SqlParameter paramNombre = new SqlParameter("@NOMBRE", nombre);
            SqlParameter paramDireccion = new SqlParameter("@DIRECCION", direccion);
            SqlParameter paramTelefono = new SqlParameter("@TELEFONO", telefono);
            SqlParameter paramNumCama = new SqlParameter("@NUM_CAMA", numCama);
            this.command.Parameters.Add(paramHospitalCod);
            this.command.Parameters.Add(paramNombre);
            this.command.Parameters.Add(paramDireccion);
            this.command.Parameters.Add(paramTelefono);
            this.command.Parameters.Add(paramNumCama);

            this.command.CommandType = CommandType.StoredProcedure;
            this.command.CommandText = "SP_CREATE_HOSPITAL";

            this.connection.Open();
            this.command.ExecuteNonQuery();

            this.connection.Close();
            this.command.Parameters.Clear();
        }

        public void DeleteHospital(int hospitalCod)
        {
            SqlParameter paramHospitalCod = new SqlParameter("@HOSPITALCOD", hospitalCod);
            this.command.Parameters.Add(paramHospitalCod);

            this.command.CommandType = CommandType.StoredProcedure;
            this.command.CommandText = "SP_DELETE_HOSPITAL";

            this.connection.Open();
            this.command.ExecuteNonQuery();

            this.connection.Close();
            this.command.Parameters.Clear();
        }

        public void UpdateHospital(int hospitalCod, string nombre, string direccion, string telefono, int numCama)
        {
            SqlParameter paramHospitalCod = new SqlParameter("@HOSPITALCOD", hospitalCod);
            SqlParameter paramNombre = new SqlParameter("@NOMBRE", nombre);
            SqlParameter paramDireccion = new SqlParameter("@DIRECCION", direccion);
            SqlParameter paramTelefono = new SqlParameter("@TELEFONO", telefono);
            SqlParameter paramNumCama = new SqlParameter("@NUM_CAMA", numCama);
            this.command.Parameters.Add(paramHospitalCod);
            this.command.Parameters.Add(paramNombre);
            this.command.Parameters.Add(paramDireccion);
            this.command.Parameters.Add(paramTelefono);
            this.command.Parameters.Add(paramNumCama);

            this.command.CommandType = CommandType.StoredProcedure;
            this.command.CommandText = "SP_UPDATE_HOSPITAL";

            this.connection.Open();
            this.command.ExecuteNonQuery();

            this.connection.Close();
            this.command.Parameters.Clear();            
        }
    }
}
