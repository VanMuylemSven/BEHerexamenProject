using DataGent.Models;
using DataGent.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataGent.Services
{
    public class DataGentService : IDataGentService
    {
        //Ctor
        private readonly IDataGentRepoAsync _dataGentRepoAsync;
        public DataGentService(IDataGentRepoAsync dataGentRepoAsync)
        {
            _dataGentRepoAsync = dataGentRepoAsync;
        }

        //methods

        public IEnumerable<Stad> GetDataGentFromURL()
        {
           
            return _dataGentRepoAsync.GetDataGentFromURL();
        }
        public Stad GetStadFromId(int id)
        {

            return _dataGentRepoAsync.GetStadFromId(id);
        }

        public async Task<Commentaar> PostCommentaar(Commentaar commentaar)
        {
            return await _dataGentRepoAsync.PostCommentaar(commentaar);

        }

        public IEnumerable<Commentaar> GetCommentaar()
        {
            return _dataGentRepoAsync.GetCommentaar();
        }

        public IEnumerable<Commentaar> GetCommentaarByUserId(string id)
        {
            return _dataGentRepoAsync.GetCommentaarByUserId(id);
        }
        public async Task<Commentaar> UpdateCommentaar(Commentaar commentaar)
        {
            return await _dataGentRepoAsync.UpdateCommentaar(commentaar);
        }

        public async Task<Commentaar> DeleteCommentaar(Commentaar commentaar)
        {
            return await _dataGentRepoAsync.DeleteCommentaar(commentaar);
        }

        //1e idee voor extraatje, maar failt.
        /*public void SendMail(string email, string subject, string body)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("mail.MyWebsiteDomainName.com", 25);

                smtpClient.Credentials = new System.Net.NetworkCredential("info@MyWebsiteDomainName.com", "myIDPassword");
                smtpClient.UseDefaultCredentials = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("info@MyWebsiteDomainName", "MyWeb Site");
                mail.To.Add(new MailAddress("sven0567@hotmail.com"));
                mail.Subject = subject;
                mail.Body = body;

                smtpClient.Send(mail);
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }*/
    }
}
