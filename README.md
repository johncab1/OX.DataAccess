# OX.DataAccess 2.0.5
Only for MSSQL<br>
Version notes<br>
Added support for Decimal type sql parameter

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

        //Whithout parameters
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