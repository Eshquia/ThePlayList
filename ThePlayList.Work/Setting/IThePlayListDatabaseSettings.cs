namespace ThePlayList.Work.Setting
{
    public interface IThePlayListDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DataBaseName { get; set; }
    }
}
