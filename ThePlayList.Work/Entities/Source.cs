using System.Collections.Generic;

namespace ThePlayList.Work.Entities
{
    public class Operator
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public int Type { get; set; }
        public int Id { get; set; }
        public Properties Properties { get; set; }
        public Dictionary<string, string> Entries { get; set; }
    }
}
