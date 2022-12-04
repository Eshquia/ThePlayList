namespace ThePlayList.Work.Entities
{
    public class Node
    {
        public int id { get; set; }
        public int top { get; set; }
        public int left { get; set; }
        public Properties properties { get; set; }
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

}
