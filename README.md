# OX.DataAccess 1.0.4

Version notes
-new method is added to execute sp without parameters
-Conversión types supported in parameters sql

C#                  SQL
int         =>      int
string      =>      varchar
bool        =>      bit
long        =>      bigint
DateTime    =>      DateTime
byte[]      =>      varbinary





Example
    
    public Class DB
    {
        public DB()
        {
            Data.ConnectionStr = MyConnectionString;
        }
        
        public void New(Person person)
        {            
            var result = Data.ExecSp<Response>("dbo.StoredProcedureName",
            Data.ToSqlParameters(person),  //every property is converted to an sql parameter
            reader =>
            {
                return new Response
                {
                    Code = reader["Code"].ToString(),
                    Message = reader["Message"].ToString()
                };
            });
        }

        Whitout parameters
        public void New(Person person)
        {            
            var result = Data.ExecSp<Response>("dbo.StoredProcedureName",
            reader =>
            {
                return new Response
                {
                    Code = reader["Code"].ToString(),
                    Message = reader["Message"].ToString()
                };
            });
        }

    }