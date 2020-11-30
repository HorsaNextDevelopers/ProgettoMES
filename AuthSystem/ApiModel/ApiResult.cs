using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSystem.ApiModel
{
    public class ApiResult<T>
    {

        public bool Ok { get; set; }

        public string Message { get; set; }

        public T DataResult { get; set; }

    }
}
