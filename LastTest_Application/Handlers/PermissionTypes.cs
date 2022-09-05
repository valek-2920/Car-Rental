using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastTest.Application.Handlers
{
    public enum PermissionTypes
    {
        [Description(PermissionTypeNames.CLIENTE)]
         CLIENTE,

        [Description(PermissionTypeNames.ADMINISTRADOR)]
        ADMINISTRADOR
    }
}
