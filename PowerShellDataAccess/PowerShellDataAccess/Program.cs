using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data;
using System.IO;

namespace PowerShellDataAccess
{

    class Program
    {
        public string ConnectionString { get; set; }
        public Dictionary<string, string> DataBases { get; private set; }

        Program()
        {
            var DBFiles = Directory.GetFiles("DB");
            FileInfo fileInfo;

            foreach (var DBFile in DBFiles)
            {
                fileInfo = new FileInfo(DBFile);

                DataBases.Add(fileInfo.Name ,fileInfo.FullName);
            }
        }     
    }
}
