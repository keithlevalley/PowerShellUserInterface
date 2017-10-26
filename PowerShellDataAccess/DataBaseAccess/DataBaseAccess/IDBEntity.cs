using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataBaseAccess
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