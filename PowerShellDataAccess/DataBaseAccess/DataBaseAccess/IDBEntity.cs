using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataBaseAccess
{
    // When adding new IEntity objects they also must be added to the DBSET in the UserModel class

    public interface IDBEntity
    {
        List<IDBEntity> CreateRecord();
        List<IDBEntity> ReadRecord();
        List<IDBEntity> UpdateRecord(string updatedEntity);
        List<IDBEntity> DeleteRecord();
    }
}