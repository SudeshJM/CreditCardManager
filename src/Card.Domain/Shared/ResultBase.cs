using System;
using System.Collections.Generic;
using System.Text;

namespace Card.Domain.Shared
{
    public class ResultBase
    {
        public bool IsSuccess { get; set; }

        public string Error { get; set; }
    }
}
