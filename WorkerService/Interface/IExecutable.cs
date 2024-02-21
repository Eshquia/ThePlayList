using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Models;

namespace WorkerService.Interface
{
    public interface IExecutable<T>
    {
        Result<T> Execute(object input);
    }
}
