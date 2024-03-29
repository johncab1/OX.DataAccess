# OX.DataAccess 2.1.3 Only for MSSQL



Example
    
    public Class DB
    {        
    
        public void getAll()
        {    
             //getting many rows
             List<Users> users = ExecSp<ResponseUser>(connectionStr,"ope.StoredProcedureName",
            reader =>
            {
                return new ResponseUser
                {
                    Name = reader["Name"].ToString(),
                    LastName = reader["LastName"].ToString()
                    age = Convert.ToInt32(reader["age"].ToString())
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

        //When stored procedure returns 2 or more data sets
        public void GetData()
        {
            DataSet ds;
            ExecSp(_connectionStr, "dbo.StoredProcedureName", data.ToSqlParameters(entity), ref ds);


            //
            employes  = data.ToEntity<Employe>(ds,0,
             reader =>
             {
                 return new Employe
                 {                     
                     Name = reader["Name"].ToString(),
                     LastName = reader["LastName"].ToString(),
                     Department = reader["Department"].ToString(),

                 };
             }).ToList();
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