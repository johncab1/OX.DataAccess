
using Microsoft.Data.SqlClient;
using OX.DataAccess.Utils;
using System.Data;
using System.Reflection;


namespace OX.DataAccess
{
    public class Data
    {
        ///<summary>
        ///Execute an stored procedure.
        ///</summary>
        ///<return>
        ///returns the number of rows affected.
        ///</return>
        ///<param name="ConnectionStr">
        ///connection string.
        ///</param>
        ///<param name="storedProcedure">
        ///Stored procedure name.
        ///</param>
        ///<param name="parameters">
        ///Sql parameters array.
        ///</param>
        public int ExecSp(string ConnectionStr, string storedProcedure, SqlParameter[] parameters = null)
        {
            int affectedRows;
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(storedProcedure, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                affectedRows = command.ExecuteNonQuery();
                connection.Close();
            }
            return affectedRows;
        }


        ///<summary>
        ///Execute an stored procedure.
        ///</summary>
        ///<return>
        ///returns the number of rows affected.
        ///</return>
        ///<param name="ConnectionStr">
        ///connection string.
        ///</param>
        ///<param name="storedProcedure">
        ///Stored procedure name.
        ///</param>
        ///<param name="parameters">
        ///Sql parameters array.
        ///</param>
        ///<param name="dataSet">
        ///DataSet returned ny reference
        ///</param>
        public DataSet ExecSp(string ConnectionStr, string storedProcedure, SqlParameter[] parameters, ref DataSet dataSet)
        {            
            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(storedProcedure, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataSet);
                connection.Close();
            }

            return dataSet;
        }

        ///<summary>
        ///Execute an stored procedure.
        ///</summary>
        ///<return>
        ///returns the specified response entity
        ///</return>
        ///<param name="ConnectionStr">
        ///connection string.
        ///</param>
        ///<param name="storedProcedure">
        ///Stored procedure name.
        ///</param>       
        ///<param name="parameters">
        ///Sql parameters array.
        ///</param>
        ///<param name="body">
        ///function that retrieves the output of the stored procedure.
        ///</param>
        public IEnumerable<T> ExecSp<T>(string ConnectionStr, string storedProcedure, SqlParameter[] parameters, Func<SqlDataReader, T> body)
        {
            List<T> results = new List<T>();

            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(storedProcedure, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    results.Add(body(reader));
                }
                reader.Close();
                connection.Close();
            }
            return results;
        }

        ///<summary>
        ///Execute an stored procedure without parameters.
        ///</summary>
        ///<return>
        ///returns the specified response entity
        ///</return>
        ///<param name="ConnectionStr">
        ///connection string.
        ///</param>
        ///<param name="storedProcedure">
        ///Stored procedure name
        ///</param>
        ///<param name="body">
        ///Function that retrieves the output of the stored procedure.
        ///</param>
        public IEnumerable<T> ExecSp<T>(string ConnectionStr, string storedProcedure, Func<SqlDataReader, T> body)
        {
            List<T> results = new List<T>();

            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(storedProcedure, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    results.Add(body(reader));
                }
                reader.Close();
                connection.Close();
            }
            return results;
        }

        ///<summary>
        ///Convert an entity class to Sql parameters array.
        ///</summary>
        ///<return>
        ///returns the specified response entity
        ///</return>
        ///<param name="entity">
        ///Entity class.
        ///</param>
        public SqlParameter[] ToSqlParameters<T>(T entity) where T : class
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {

                SqlParameter parameter = new()
                {
                    ParameterName = "@" + property.Name,
                    Direction = ParameterDirection.Input,
                    SqlDbType = Util.ConvertToSqlType(property),
                    Value = property.GetValue(entity) == null ? DBNull.Value : property.GetValue(entity)
                };

                parameters.Add(parameter);

            }

            return parameters.ToArray();

        }

    }

}
