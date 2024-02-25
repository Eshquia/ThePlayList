using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Helpers;
using WorkerService.Interface;
using WorkerService.Models;

namespace WorkerService.Activities.Cmd
{
    public class SendCommand : IExecutable<bool>
    {
        public Result<bool> Execute(object input)
        {

            if (input is Dictionary<string, string> entries)
            {

                Result<bool> executionResult = new Result<bool>(true);
                executionResult.Successful = true;
                executionResult.ProcessId = "1";

                if (entries.TryGetValue("Command", out string command))
                {
                    CmdHelper cmdHelper = CmdHelper.Instance;
                    cmdHelper.ExecuteCommand(command);
                }
                return executionResult;
            }
            else
            {
                // Hatalı giriş parametresi
                Result<bool> executionResult = new Result<bool>(false);
                executionResult.Successful = true;
                return executionResult;
            }
        }

    }
}
