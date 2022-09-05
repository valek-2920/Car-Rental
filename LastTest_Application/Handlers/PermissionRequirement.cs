using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastTest.Application.Handlers
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionTypes _permissionType { get; private set; }

        public PermissionRequirement(PermissionTypes permissionType)
        {
            _permissionType = permissionType;
        }


    }
}
