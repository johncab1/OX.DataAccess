
using Microsoft.Data.SqlClient;

namespace OX.Data
{
    public class Data
    {
        public IEnumerable<T> ExecSp<T>(string storedProcedure, string connectionString, SqlParameter[] parameters, Func<SqlDataReader, T> body)
        {
            List<T> results = new List<T>();

            using (SqlConnection connection = new SqlConnection())
            {
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
            }
            return results;
        }
    }
}
