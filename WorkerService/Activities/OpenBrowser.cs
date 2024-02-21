using System.Diagnostics;
using WorkerService.Interface;

namespace WorkerService.Models
{
    public class OpenBrowser : IExecutable<bool>
    {
        public Result<bool> Execute(object input)
        {
            if (input is string url)
            {

                Result<bool> executionResult = new Result<bool>(true);
                executionResult.Successful = true;
                executionResult.ProcessId = "1111231231231";
                Process.Start(new ProcessStartInfo
                {
                    FileName = input.ToString(),
                    UseShellExecute = true
                });
                return executionResult;
            }
            else
            {
                // Hatalı giriş parametresi
                Result<bool> executionResult = new Result<bool>(false);
                executionResult.Successful=true;
                return executionResult;
            }
        }

    }
}