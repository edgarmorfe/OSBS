using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DL
{
    public static class DataAccessHelper
    {
        public static string GetConnectionString(string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static List<T> LoadData<T>(string sql, string connectionName = "OSBSDB")
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString(connectionName)))
            {
                return conn.Query<T>(sql, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public static List<T> LoadDataText<T>(string sql, string connectionName = "OSBSDB")
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString(connectionName)))
            {
                return conn.Query<T>(sql, commandType: CommandType.Text).ToList();
            }
        }

        public static List<T> LoadData<T>(string sql, DynamicParameters dParams, string connectionName = "OSBSDB")
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString(connectionName)))
            {
                return conn.Query<T>(sql,param: dParams, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public static List<T> LoadDataText<T>(string sql, DynamicParameters dParams, string connectionName = "OSBSDB")
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString(connectionName)))
            {
                return conn.Query<T>(sql, param: dParams, commandType: CommandType.Text).ToList();
            }
        }

        public static DynamicParameters ReturnInt(string sql, DynamicParameters dParams, string connectionName = "OSBSDB")
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString(connectionName)))
            {
               conn.Execute(sql, param:dParams, commandType: CommandType.StoredProcedure);
            }
            return dParams;
        }

        public static int SaveData<T>(string sql, DynamicParameters dParams, string connectionName = "OSBSDB")
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString(connectionName)))
            {
                return conn.Execute(sql, param: dParams, commandType: CommandType.StoredProcedure);
            }
        }

        public static int SaveData(string sql, DynamicParameters dParams, string connectionName = "OSBSDB")
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString(connectionName)))
            {
                return conn.Execute(sql, param: dParams, commandType: CommandType.StoredProcedure);
            }
        }

        public static List<T> LoadData<T>(string sql, string dbName, string connectionName = "OSBSDB")
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString(dbName)))
            {
                return conn.Query<T>(sql).ToList();
            }
        }
  
        public static int SaveDataWithReturn(string sql, DynamicParameters dParams, string connectionName = "OSBSDB")
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString(connectionName)))
            {
                return conn.Query<int>(sql, param: dParams, commandType: CommandType.StoredProcedure).Single();
            }
        }

    }
}
