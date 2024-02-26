using WorkerService.Interface;
using WorkerService.Models;

namespace WorkerService.Activities.File
{
    public class CreateNote : IExecutable<bool>
    {

        public Result<bool> Execute(object input)
        {
            if (input is Dictionary<string, string> entries)
            {
                Result<bool> executionResult = new Result<bool>(true);
                executionResult.Successful = true;
                executionResult.ProcessId = "1";
                if ( entries.TryGetValue("Path", out string path) && entries.TryGetValue("FileName", out string fileName))
                {

                    try
                    {
                        using (StreamWriter sw = System.IO.File.CreateText(Path.Combine(path, fileName)))
                        {
                           
                        }
                      executionResult.Successful = true;
                      executionResult.ProcessId = "1";
                    }
                    catch (Exception e)
                    {
                        executionResult.Successful = false;
                    }


                }

               return executionResult;
            }
            else
            {
                // Hatalı giriş parametresi
                Result<bool> executionResult = new Result<bool>(false);
                executionResult.Successful = false;
                return executionResult;
            }
        }
    
    
    }
}
