

using OX.DataAccess.Enums;
using System.Data;
using System.Reflection;


namespace OX.DataAccess.Utils
{
    internal static class Util
    {
        internal static SqlDbType ConvertToSqlType(PropertyInfo property)
        {
            TypeEnum typeEnum = GetTypeEnum(property);
            
            SqlDbType dbType = typeEnum switch
            {
                TypeEnum.Int => SqlDbType.Int,
                TypeEnum.String => SqlDbType.VarChar,
                TypeEnum.Bool => SqlDbType.Bit,
                TypeEnum.Long =>  SqlDbType.BigInt,
                TypeEnum.Char => SqlDbType.Char,
                TypeEnum.DateTime => SqlDbType.DateTime,
                TypeEnum.Bytes => SqlDbType.VarBinary,
                TypeEnum.Decimal => SqlDbType.Decimal
            };

            return dbType;
        }

        internal static TypeEnum GetTypeEnum(PropertyInfo property)
        {
            int typeId = 0;
            if(property.PropertyType.Equals(typeof(int)))
                typeId = 1;
            if(property.PropertyType.Equals(typeof(string)))
                typeId = 2;
            if(property.PropertyType.Equals(typeof(bool)))
                typeId = 3;
            if(property.PropertyType.Equals(typeof(long)))
                typeId = 4;
            if(property.PropertyType.Equals(typeof(char)))
                typeId = 5;
            if(property.PropertyType.Equals(typeof(DateTime)))
                typeId = 6;
            if(property.PropertyType.Equals(typeof(byte[])))
                typeId = 7;
            if(property.PropertyType.Equals(typeof(decimal)))
                typeId = 8;

            TypeEnum typeEnum = (TypeEnum)typeId;
            return typeEnum;
        }
    }
}
