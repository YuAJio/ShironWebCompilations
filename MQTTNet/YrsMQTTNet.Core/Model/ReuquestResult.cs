using System;
using System.Collections.Generic;
using System.Text;

namespace YrsMQTTNet.Core.Model
{
    public class ReuquestResult
    {
        public bool Successful { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
