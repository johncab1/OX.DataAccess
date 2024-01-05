# OX.DataAccess 1.0.4

Implementación de ejemplo

    
    public Class DB
    {
        public DB()
        {
            Data.OXConnectionStr = MyConnectionString;
        }

        public void New(Person person)
        {            
            var result = Data.ExecSp<Response>("dbo.StoredProcedureName",
            Data.GetOXParameters(person),  //los parametros se toman de cada propiedad de la entidad person
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