using MongoDB.Driver;
using ThePlayList.Work.Entities;

namespace ThePlayList.Work.Data
{
    public interface IThePlayListContext
    {

        IMongoCollection<Work.Entities.Work> Works { get; }
    }
}
