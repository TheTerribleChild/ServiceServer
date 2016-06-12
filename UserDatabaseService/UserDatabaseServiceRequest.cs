using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseService;

namespace UserDatabaseService
{
    public class UserDatabaseServiceRequest : BaseService.IDatabaseServiceRequest
    {
        public DatabaseModificationOperation OperationType { get; set; }
    }
}
