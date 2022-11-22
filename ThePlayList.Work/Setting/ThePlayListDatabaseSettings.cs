namespace ThePlayList.Work.Setting
{
    public class ThePlayListDatabaseSettings : IThePlayListDatabaseSettings
    {
        public string ConnectionString { get ; set ; }
        public string DataBaseName { get; set ; }
    }
}
