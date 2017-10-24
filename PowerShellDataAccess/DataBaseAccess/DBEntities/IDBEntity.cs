using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security;

namespace DBEntities
{
    // When adding new IEntity objects they also must be added to the DBSET in the UserModel class

    public interface IDBEntity
    {
        List<string> CreateRecord();
        List<string> ReadRecord();
        List<string> UpdateRecord(string updatedEntity);
        List<string> DeleteRecord();
    }
}
