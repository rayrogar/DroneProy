using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatch_Controller_RESTapi.Enums
{
    public enum StateEnum
    {
        IDLE = 1,
        LOADING = 2,
        LOADED = 3,
        DELIVERING = 4, 
        DELIVERED = 5, 
        RETURNING = 6
    }
}