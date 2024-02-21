namespace ThePlayList.Work.Entities
{
    public class Link
    {
        public int FromOperator { get; set; }
        public string FromConnector { get; set; }
        public int FromSubConnector { get; set; }
        public int ToOperator { get; set; }
        public string ToConnector { get; set; }
        public int ToSubConnector { get; set; }
    }
}
