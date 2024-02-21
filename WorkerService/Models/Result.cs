using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Models
{
    public class Result<T>
    {
        public T Value { get; private set; }
        public bool Successful {get;set;}
        public Result(T value)
        {
            Value = value;
        }
    }
}
