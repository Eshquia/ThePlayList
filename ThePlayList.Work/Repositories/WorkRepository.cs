using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThePlayList.Work.Data;

namespace ThePlayList.Work.Repositories
{
    public class WorkRepository : IWorkRepository
    {
        private readonly IThePlayListContext _context;
        public WorkRepository(IThePlayListContext context)
        {
            _context = context;
        }

        public async Task<string> Create(Entities.Work work)
        {
            await _context.Works.InsertOneAsync(work);
            return work.Id.ToString();
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Entities.Work> filter = Builders<Entities.Work>.Filter.Eq(x => x.Id ,id);
            DeleteResult result = await _context.Works.DeleteOneAsync(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<IEnumerable<Entities.Work>> GetAllWorks()
        {
            return await _context.Works.Find(x=>true).ToListAsync();
        }

        public async Task<Entities.Work> GetWork(string id)
        {
            return await _context.Works.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Entities.Work work)
        {
            var UpdateResult = await _context.Works.ReplaceOneAsync(x => x.Id.Equals(work.Id), work);
            return UpdateResult.IsAcknowledged && UpdateResult.ModifiedCount>0;
        }
    }
}
