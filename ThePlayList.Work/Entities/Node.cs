using System.Collections.Generic;

namespace ThePlayList.Work.Entities
{
    public class Node
    {
        public int id { get; set; }
        public int top { get; set; }
        public int left { get; set; }
        public Properties properties { get; set; }
    }


    public class Input
    {
        public string Label { get; set; }
        public bool Multiple { get; set; }
    }

    public class Output
    {
        public string Label { get; set; }
    }

    public class Properties
    {
        public string Title { get; set; }
        public string Class { get; set; }
        public Dictionary<string, Input> Inputs { get; set; }
        public Dictionary<string, Output> Outputs { get; set; }
    }

}
