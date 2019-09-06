using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarbageStreamAPI.Model
{
    public struct StreamScoreResponse
    {
        public bool Success { get; set; }
        public int? Score { get; set; }
        public string Error { get; set; }
    }
}
