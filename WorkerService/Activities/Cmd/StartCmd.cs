using System;
using System.Collections.Generic;
using WorkerService.Helpers;
using WorkerService.Interface;
using WorkerService.Models;

namespace WorkerService.Activities.Cmd
{
    public class StartCmd : IExecutable<bool>
    {
        private readonly CmdHelper cmdHelper;  // CmdHelper'ın örneği

        public StartCmd()
        {
            cmdHelper = CmdHelper.Instance;
        }

        public Result<bool> Execute(object input)
        {
            Result<bool> executionResult = new Result<bool>(false);
            try
            {

                CmdHelper cmdHelper = CmdHelper.Instance;
                cmdHelper.ExecuteCommand("echo Merhaba, cmd ekranına ile işlem yapıyorum");
                executionResult.Successful = true;
                return executionResult;
            }
            catch (Exception e)
            {
                executionResult.Successful = false;
                return executionResult;
            }
        }
    }
}