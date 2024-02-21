using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            }
      }

    }
   
}
