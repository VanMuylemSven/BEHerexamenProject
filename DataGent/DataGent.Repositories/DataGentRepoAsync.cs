using DataGent.Models;
using DataGent.Models.Data;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataGent.Repositories
{
    public class DataGentRepoAsync : IDataGentRepoAsync
    {

        private string _connectionstring;
        /*https://datatank.stad.gent/4/milieuennatuur/ecoplan.json*/

        private readonly StedenDbContext _context;

        public DataGentRepoAsync(StedenDbContext context, IConfiguration configuration)
        {
            this._context = context;
            this._connectionstring = ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection");
        }

        public IEnumerable<Stad> GetDataGentFromURL()
        {
            //Get data from URL
            const string URL = "https://datatank.stad.gent/4/milieuennatuur/ecoplan.json";

            var data = new WebClient().DownloadString(URL);
            var stedenList = JsonConvert.DeserializeObject<List<Stad>>(data);

            //Give Id's to objects because why the hell doesn't this API use ID's?
            int teller = 0;
            foreach (var item in stedenList)
            {
                item.Id = teller;
                teller++;
            }

            return stedenList;
        }

        public Stad GetStadFromId(int id)
        {
            //Get stedenlist;
            List<Stad> stedenList = GetDataGentFromURL().ToList();
            return stedenList[id];
        }

        public async Task<Commentaar> PostCommentaar(Commentaar commentaar)
        {
            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                try
                {
                    string sql = "INSERT INTO Commentaar(UserId, StadId, CommentaarText, Tijdstip)";
                    sql += "VALUES(@UserId, @StadId, @commentaar, @tijdstip)";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    //74381bf2-ca0f-4444-a316-f567ca374674 //sven userId , for testing

                    cmd.Parameters.AddWithValue("@UserId", commentaar.UserId);
                    cmd.Parameters.AddWithValue("@StadId", commentaar.StadId);
                    //cmd.Parameters.AddWithValue("@Email", commentaar.Email);
                    //cmd.Parameters.AddWithValue("@naam", "Roomer"/*commentaar.Naam*/);
                    cmd.Parameters.AddWithValue("@commentaar", commentaar.CommentaarText);
                    cmd.Parameters.AddWithValue("@tijdstip", commentaar.Tijdstip);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return commentaar;
            }
        }

        public async Task<Commentaar> UpdateCommentaar(Commentaar commentaar)
        {
            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                try
                {
                    string sql = "UPDATE Commentaar ";
                    sql += "SET CommentaarText = @CommentaarText, Tijdstip = @Tijdstip ";
                    sql += "WHERE StadId = @StadId AND UserId = @UserId; ";

                    SqlCommand cmd = new SqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@UserId", commentaar.UserId);
                    cmd.Parameters.AddWithValue("@StadId", commentaar.StadId);
                    cmd.Parameters.AddWithValue("@CommentaarText", commentaar.CommentaarText);
                    cmd.Parameters.AddWithValue("@Tijdstip", commentaar.Tijdstip);

                    con.Open();
                    cmd.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return commentaar;
            }
        }

        public IEnumerable<Commentaar> GetCommentaar()
        {
            List<Commentaar> commentaarList = new List<Commentaar>();

            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                try
                {
                    string sql = "SELECT* FROM dbo.Commentaar";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    con.Open();

                    //await
                    var result = cmd.ExecuteReader();
                    while (result.Read())
                    {
                        Commentaar comment = new Commentaar
                        {
                            CommentaarId = Int32.Parse(result["CommentaarId"].ToString()),
                            UserId = result["UserId"].ToString(),
                            StadId = Int32.Parse(result["StadId"].ToString()),
                            CommentaarText = result["CommentaarText"].ToString(),
                            Tijdstip = DateTime.Parse(result["Tijdstip"].ToString()),
                        };
                        commentaarList.Add(comment);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
            return commentaarList;
        }

        public IEnumerable<Commentaar> GetCommentaarByUserId(string id)
        {
            List<Commentaar> commentaarList = new List<Commentaar>();

            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                try
                {
                    string sql = "SELECT* FROM dbo.Commentaar WHERE UserId = @userId";

                    SqlCommand cmd = new SqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@userId", id);
                    con.Open();

                    //await
                    var result = cmd.ExecuteReader();
                    while (result.Read())
                    {
                        Commentaar comment = new Commentaar
                        {
                            CommentaarId = Int32.Parse(result["CommentaarId"].ToString()),
                            UserId = result["UserId"].ToString(),
                            StadId = Int32.Parse( result["StadId"].ToString()),
                            CommentaarText = result["CommentaarText"].ToString(),
                            Tijdstip = DateTime.Parse(result["Tijdstip"].ToString()),
                        };
                        commentaarList.Add(comment);
                    }
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
            return commentaarList;
        }

        public async Task<Commentaar> DeleteCommentaar(Commentaar commentaar)
        {
            using (SqlConnection con = new SqlConnection(_connectionstring))
            {
                try
                {
                    string sql = "DELETE FROM Commentaar ";
                    sql += "WHERE UserId = @UserId AND CommentaarId = @CommentaarId";

                    SqlCommand cmd = new SqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@UserId", commentaar.UserId);
                    cmd.Parameters.AddWithValue("@CommentaarId", commentaar.CommentaarId);

                    con.Open();
                    cmd.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return commentaar;
            }
        }
    }
}
