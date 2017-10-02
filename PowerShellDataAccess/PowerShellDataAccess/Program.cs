using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data;
using System.IO;
using System.Data.SqlClient;

namespace PowerShellDataAccess
{
    class Program
    {
        static Dictionary<string, string> dataBases = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            // args 0=Database name 1=Table name 2=operation 3-... parameters

            var DBFiles = Directory.GetFiles("DB");
            FileInfo fileInfo;

            foreach (var DBFile in DBFiles)
            {
                fileInfo = new FileInfo(DBFile);
                dataBases.Add(fileInfo.Name, fileInfo.FullName);
            }

            int test = createRecord(args);
        }

        static string getCS (string databaseName)
        {
            if (dataBases.TryGetValue(databaseName, out string path))
            {
                var connectionStringBuilder = new SqlConnectionStringBuilder();
                connectionStringBuilder.DataSource = @"(LocalDB)\MSSQLLocalDB";
                connectionStringBuilder.AttachDBFilename = path;
                connectionStringBuilder.IntegratedSecurity = true;

                return connectionStringBuilder.ConnectionString;
            }

            return null;
        }

        static int createRecord (string[] param)
        {
            if (param.Length % 2 == 1)
            {
                using (SqlConnection db = new SqlConnection(getCS(param[1])))
                {
                    using (SqlCommand command = new SqlCommand(param[2], db))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        for (int i = 2; i < param.Length; i++)
                        {
                            command.Parameters.AddWithValue(param[i], param[i++]);
                        }

                        try
                        {
                            db.Open();
                            return command.ExecuteNonQuery();
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }

            else{return 0;}
        }
    }
}