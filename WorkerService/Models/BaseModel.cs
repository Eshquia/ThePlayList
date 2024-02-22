using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Models
{
    public class BaseModel
    {      
            public class Input
            {
                public string? Label { get; set; }
                public bool Multiple { get; set; }
            }

            public class Output
            {
                public string? Label { get; set; }
            }

            public class Properties
            {
                public string? Title { get; set; }
                public string? Class { get; set; }
                public Dictionary<string, Input>? Inputs { get; set; }
                public Dictionary<string, Output>? Outputs { get; set; }
            }

            public class Operator
            {
                public int Top { get; set; }
                public int Left { get; set; }
                public int Type { get; set; }
                public int Id { get; set; }
                public Properties? Properties { get; set; }
                public Dictionary<string, string>? Entries { get; set; }
        }

            public class Link
            {
                public string? FromOperator { get; set; }
                public string? FromConnector { get; set; }
                public int FromSubConnector { get; set; }
                public int ToOperator { get; set; }
                public string? ToConnector { get; set; }
                public int ToSubConnector { get; set; }
            }

            public class Node
            {
                public Dictionary<string, Operator>? Operators { get; set; }
                public Dictionary<string, Link>? Links { get; set; }
                public Dictionary<string, object>? OperatorTypes { get; set; }
            }
        
    }
}
