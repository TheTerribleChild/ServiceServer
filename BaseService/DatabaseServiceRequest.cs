using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseService
{
    public enum DatabaseModificationOperation { Add, Update, Delete };

    public interface DatabaseServiceRequest
    {
        DatabaseModificationOperation OperationType { get; set; }
    }
}
