using Dapper;
using Microsoft.Data.SqlClient;
using TFCloud_Blazor_ApiSample.Models;

namespace TFCloud_Blazor_ApiSample.Repos
{
    public class GameRepo
    {
        private readonly SqlConnection _connection;

        public GameRepo(SqlConnection connection)
        {
            _connection = connection;
        }

        public bool AjoutJeux(string Title, int ReleaseYear, string Synopsis)
        {
            string sql = "INSERT INTO Games (Title, ReleaseYear, Synopsis) " +
                "VALUES (@Title, @ReleaseYear, @Synopsis)";

            return _connection.Execute(sql, new { Title, ReleaseYear, Synopsis }) > 0;
        }

        public Game Get(int Id)
        {
            string sql = "SELECT * FROM Games WHERE Id = @Id";
            return _connection.QuerySingle<Game>(sql, new {Id});

        }

        public List<Game> GetAll()
        {
            string sql = "SELECT * FROM Games";

            return _connection.Query<Game>(sql).ToList();

        }

    }
}