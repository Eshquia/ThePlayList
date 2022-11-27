namespace ThePlayList.Work.Entities
{
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
