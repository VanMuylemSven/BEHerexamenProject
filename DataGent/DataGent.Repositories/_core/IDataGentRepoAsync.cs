using System.Collections.Generic;
using System.Threading.Tasks;
using DataGent.Models;

namespace DataGent.Repositories
{
    public interface IDataGentRepoAsync
    {
        IEnumerable<Stad> GetDataGentFromURL();
        Task<Commentaar> PostCommentaar(Commentaar commentaar);
        Task<Commentaar> UpdateCommentaar(Commentaar commentaar);
        IEnumerable<Commentaar> GetCommentaar();
        IEnumerable<Commentaar> GetCommentaarByUserId(string id);
        Stad GetStadFromId(int id);
        Task<Commentaar> DeleteCommentaar(Commentaar commentaar);
    }
}