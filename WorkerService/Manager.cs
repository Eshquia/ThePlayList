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
            var sortedLinks = SortLinks(node.Links);

            foreach (var linkKey in sortedLinks)
            {

                Link link = node.Links[linkKey];
                Operator newoperator = node.Operators[link.ToOperator.ToString()];
                var activiteTypeFullName = "WorkerService.Activities."+newoperator.Properties.Class +'.'+ newoperator.Properties.Title.Replace(" ", "");
                Type activiteType = Type.GetType(activiteTypeFullName);

                if (activiteType != null && typeof(IExecutable<bool>).IsAssignableFrom(activiteType))
                {
                    Task.Delay(1000);
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
        private static List<string> SortLinks(Dictionary<string, Link> links)
        {
            var sortedLinks = new List<string>();
            var currentOperatorId = "1000000";

            while (true)
            {
                var nextLink = links.FirstOrDefault(link => link.Value.FromOperator == currentOperatorId);
                if (nextLink.Value != null)
                {
                    sortedLinks.Add(nextLink.Key);
                    currentOperatorId = nextLink.Value.ToOperator.ToString();
                }
                else
                {
                    break;
                }
            }

            return sortedLinks;
        }
    }
   
}
