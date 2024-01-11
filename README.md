# OX.DataAccess 2.0.6 Only for MSSQL



Example
    
    public Class DB
    {
        private readonly string connectionStr;
        private Data data;

        public DB(string Connectionstr) 
        {
            connectionStr = Connectionstr;
            data = new Data();
        }
        
        public void New(Person person)
        {            
            var result = data.ExecSp<Response>("dbo.StoredProcedureName",
            data.ToSqlParameters(person),  //every property is converted to an sql parameter
            connectionStr,
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
            var result = data.ExecSp<Response>("dbo.StoredProcedureName", connectionStr,
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