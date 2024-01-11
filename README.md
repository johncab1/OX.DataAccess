# OX.DataAccess 2.0.8 Only for MSSQL



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
        
        public void New()
        {    
             //getting many rows
             List<Users> users = ExecSp<ResponseUser>(connectionStr,"ope.spNewUser",
            reader =>
            {
                return new ResponseUser
                {
                    Code = reader["Name"].ToString(),
                    Message = reader["LastName"].ToString()
                };

            }).ToList();
            
        }

        public void New(Person person)
        {  
            //getting 1 row
            Response = ExecSp<Response>(connectionStr,"dbo.StoredProcedureName", data.ToSqlParameters(person),
            reader =>
            {
                return new Response
                {
                    Code = reader["Code"].ToString(),
                    Message = reader["Message"].ToString()
                };
            }).FirstOrDefault();
        }

        //execute sotered procedure Without parameters
        public void New(Person person)
        {            
            var result = data.ExecSp<Response>(connectionStr, "dbo.StoredProcedureName",
            reader =>
            {
                return new Response
                {
                    Code = reader["Code"].ToString(),
                    Message = reader["Message"].ToString()
                };
            }).FirstOrDefault();
        }

        //execute stored procedure without data response, Sql parameters are optional
        public void New()
        {                       
            int query = ExecSp(connectionStr, "dbo.StoredProcedureName");  //returns the number of rows affected          
        }

    }

    //Conversion types properties to SqlParameter (C# to SQL)
    class Person
    {
        int var1 {get; set;}        //SQl = int
        string var2 {get; set;}     //SQl = varchar
        bool var3 {get; set;}       //SQl = bit
        long var4 {get; set;}       //SQl = bigint
        char var5 {get; set;}       //SQl = char
        DateTime var6 {get; set;}   //SQl = DateTime
        byte[] var7 {get; set;}     //SQl = varbinary
        decimal var8 {get; set;}    //SQl = decimal
    }