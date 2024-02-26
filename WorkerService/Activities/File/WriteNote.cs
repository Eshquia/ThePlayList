using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Helpers;
using WorkerService.Interface;
using WorkerService.Models;

namespace WorkerService.Activities.File
{
    public class WriteNote : IExecutable<bool>
    {

        public Result<bool> Execute(object input)
        {
            if (input is Dictionary<string, string> entries)
            {
                Result<bool> executionResult = new Result<bool>(true);
                executionResult.Successful = true;
                executionResult.ProcessId = "1";
                if (entries.TryGetValue("Path", out string path) && entries.TryGetValue("TextValue", out string value))
                {

                    try
                    {
                        if (value.Contains("{{") && value.Contains("}}"))
                        {
                            // String içinde {{ ve }} karakterleri var. Redisten alınacak
                            int baslangicIndex = value.IndexOf("{{");
                            int bitisIndex = value.IndexOf("}}");

                            // Stringi parçala
                            string parcalananDeger = value.Substring(baslangicIndex + 2, bitisIndex - baslangicIndex - 2);

                            Console.WriteLine("Parçalanan Değer: " + parcalananDeger);
                            value = RedisHelper.Get(parcalananDeger);
                        }
                        using (StreamWriter sw = System.IO.File.CreateText(path))
                        {
                            sw.Write(value);
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
