namespace ThePlayList.Work.Entities
{
    public class Node
    {
        public Node()
        {
            width = 120;
            height = 60;
        }
        public string type { get; set; }
        public string title { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int id { get; set; }
        public int payload { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}
