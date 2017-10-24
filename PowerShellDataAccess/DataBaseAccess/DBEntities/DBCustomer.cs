using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DBEntities
{
    public class DBCustomer : Customer, IDBEntity
    {
        public List<string> CreateRecord()
        {
            throw new NotImplementedException();
        }

        public List<string> DeleteRecord()
        {
            throw new NotImplementedException();
        }

        public List<string> ReadRecord()
        {
            throw new NotImplementedException();
        }

        public List<string> UpdateRecord(string updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
}
