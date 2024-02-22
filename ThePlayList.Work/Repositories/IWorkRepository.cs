using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThePlayList.Work.Repositories
{
    public interface IWorkRepository
    {
        public Task<Entities.Work> GetWork(string id);
        public Task<IEnumerable<Entities.Work>> GetAllWorks();
        public Task<string> Create(Entities.Work work);
        public Task<bool> Delete(string id);
        public Task<bool> Update(Entities.Work work);
    }
}
