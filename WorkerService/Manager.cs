using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Interface;
using static WorkerService.Models.BaseModel;

namespace WorkerService
{
    public static class Manager
    {

        public static void Process(Node node)
        {
            foreach (var linkPair in node.Links)
            {
                string linkKey = linkPair.Key;
                Link link = linkPair.Value;
                Operator newoperator = node.Operators[link.ToOperator.ToString()];
                var activiteTypeFullName = "WorkerService.Models." + newoperator.Properties.Class;
                Type activiteType = Type.GetType(activiteTypeFullName);

                if (activiteType != null && typeof(IExecutable<bool>).IsAssignableFrom(activiteType))
                {
                    // Sınıf IExecutable<bool> arayüzünü uyguluyorsa
                    var executableInstance = (IExecutable<bool>)Activator.CreateInstance(activiteType);

                    var result = executableInstance.Execute(newoperator.Entries); // Gerekli parametreleri object içinde verirsiniz

                    // Result'u kullanabilirsiniz
                    if (result.Successful)
                    {
                        Console.WriteLine("Operation successful with result: " + result.Value);
                    }
                    else
                    {
                        // İşlem başarısızsa
                        Console.WriteLine("Operation failed");
                    }
                }
                else
                {
                    // Belirtilen tür IExecutable<bool> arayüzünü uygulamıyorsa veya tür bulunamazsa
                }
            }
        }

    }
   
}
