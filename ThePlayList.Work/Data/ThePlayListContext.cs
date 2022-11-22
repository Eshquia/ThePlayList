using MongoDB.Driver;
using ThePlayList.Work.Setting;

namespace ThePlayList.Work.Data
{
    public class ThePlayListContext : IThePlayListContext
    {
        public ThePlayListContext(IThePlayListDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DataBaseName);

            Works = database.GetCollection<Entities.Work>("Work");
        }
        public IMongoCollection<Entities.Work> Works { get; }
    }
}
