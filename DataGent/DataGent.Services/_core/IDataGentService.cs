using System.Collections.Generic;
using System.Threading.Tasks;
using DataGent.Models;

namespace DataGent.Services
{
    public interface IDataGentService
    {
        IEnumerable<Stad> GetDataGentFromURL();
        Stad GetStadFromId(int id);
        Task<Commentaar> PostCommentaar(Commentaar commentaar);
        Task<Commentaar> UpdateCommentaar(Commentaar commentaar);
        IEnumerable<Commentaar> GetCommentaar();
        IEnumerable<Commentaar> GetCommentaarByUserId(string id);
        Task<Commentaar> DeleteCommentaar(Commentaar commentaar);
        //void SendMail(string email, string subject, string body);
    }
}