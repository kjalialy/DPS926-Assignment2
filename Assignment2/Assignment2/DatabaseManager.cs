using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Assignment2
{
    public class DatabaseManager
    {
        readonly SQLiteAsyncConnection database;

        public DatabaseManager()
        {
            database = DependencyService.Get<SQLiteDBInterface>().createSQLiteDB();
            database.CreateTableAsync<PlayerDB>().Wait();
            database.CreateTableAsync<TeamObj>().Wait();
        }

        public Task<List<PlayerDB>> GetPlayersAsync()
        {
            //Get all players.
            return database.Table<PlayerDB>().ToListAsync();
        }

        public Task<PlayerDB> GetPlayerAsync(string id)
        {
            // Get a specific player.
            return database.Table<PlayerDB>()
                            .Where(i => i.personId == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> AddPlayerAsync(PlayerDB player)
        {
            // Save a new player in DB.
            return database.InsertAsync(player);
        }

        public Task<int> RemovePlayerAsync(PlayerDB player)
        {
            // remove a player.
            return database.DeleteAsync(player);
        }

        public Task<List<TeamObj>> GetTeamsAsync()
        {
            //Get all teams.
            return database.Table<TeamObj>().ToListAsync();
        }

        public Task<TeamObj> GetTeamAsync(string id)
        {
            // Get a specific team.
            return database.Table<TeamObj>()
                            .Where(i => i.teamId == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> AddTeamAsync(TeamObj team)
        {
            // Save a new team in DB.
            return database.InsertAsync(team);
        }

        public Task<int> RemoveTeamAsync(TeamObj team)
        {
            // remove a team.
            return database.DeleteAsync(team);
        }
    }
}
