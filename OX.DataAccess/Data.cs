
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace OX.DataAccess
{
    public static class Data
    {
        public static string OXConnectionStr = "";

        public static IEnumerable<T> ExecSp<T>(string storedProcedure, SqlParameter[] parameters, Func<SqlDataReader, T> body)
        {
            List<T> results = new List<T>();

            using (SqlConnection connection = new SqlConnection(OXConnectionStr))
            {
                connection.Open();
                //SqlCommand command = connection.CreateCommand(storedProcedure, connectionString);
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

        public static SqlParameter[] GetOXParameters<T>(T entity) where T : class
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                SqlDbType DbType;

                SqlParameter parameter = new()
                {
                    ParameterName = "@" + property.Name,
                    Direction = ParameterDirection.Input,
                    Value = property.GetValue(entity)
                };
                if (property.PropertyType.Equals(typeof(int)))
                    parameter.SqlDbType = SqlDbType.Int;
                if (property.PropertyType.Equals(typeof(string)))
                    parameter.SqlDbType = SqlDbType.VarChar;
                if (property.PropertyType.Equals(typeof(bool)))
                    parameter.SqlDbType = SqlDbType.Bit;

                parameters.Add(parameter);

            }

            return parameters.ToArray();

        }
    }

       
    
}
