using EventBusRabbitMQ.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusRabbitMQ.Events
{
    public class PlayWork:IEvent
    {

        public string Id { get; set; }
        public List<Node>? Nodes { get; set; }
        public List<Connect>? Connects { get; set; }
        public Status Status { get; set; }
        public DateTime LastWorkDate { get; set; }
    }
    public class Node
    {
        public int id { get; set; }
        public int top { get; set; }
        public int left { get; set; }
        public Properties? properties { get; set; }
    }
    public class Input1
    {
        public string label { get; set; }
    }

    public class Inputs
    {
        public Ins ins { get; set; }
        public Input1 input_1 { get; set; }
    }

    public class Ins
    {
        public string label { get; set; }
        public bool multiple { get; set; }
    }

    public class Output1
    {
        public string label { get; set; }
    }

    public class Outputs
    {
        public Output1 output_1 { get; set; }
    }

    public class Properties
    {
        public string title { get; set; }
        public string @class { get; set; }
        public Inputs inputs { get; set; }
        public Outputs outputs { get; set; }
        public string data { get; set; }
    }
    public enum Status
    {
        NotWorking = 0,
        Working = 1,
        Stopped = 2
    }
    public class Connect
    {
        public int fromOperator { get; set; }
        public string fromConnector { get; set; }
        public int fromSubConnector { get; set; }
        public string toOperator { get; set; }
        public string toConnector { get; set; }
        public int toSubConnector { get; set; }
    }
}
