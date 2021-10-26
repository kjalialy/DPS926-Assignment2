using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public class DatabaseManager
    {
        readonly SQLiteAsyncConnection database;

        public DatabaseManager(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<PlayerDB>().Wait();
            database.CreateTableAsync<TeamObj>().Wait();
        }

        public Task<List<PlayerDB>> GetPlayersAsync()
        {
            //Get all notes.
            return database.Table<PlayerDB>().ToListAsync();
        }

        public Task<PlayerDB> GetPlayerAsync(string id)
        {
            // Get a specific note.
            return database.Table<PlayerDB>()
                            .Where(i => i.personId == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> AddPlayerAsync(PlayerDB player)
        {
            // Save a new note.
            return database.InsertAsync(player);
        }

        public Task<int> RemovePlayerAsync(PlayerDB player)
        {
            // remove a note.
            return database.DeleteAsync(player);
        }

        public Task<List<TeamObj>> GetTeamsAsync()
        {
            //Get all notes.
            return database.Table<TeamObj>().ToListAsync();
        }

        public Task<TeamObj> GetTeamAsync(string id)
        {
            // Get a specific note.
            return database.Table<TeamObj>()
                            .Where(i => i.teamId == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> AddTeamAsync(TeamObj team)
        {
            // Save a new note.
            return database.InsertAsync(team);
        }

        public Task<int> RemoveTeamAsync(TeamObj team)
        {
            // remove a note.
            return database.DeleteAsync(team);
        }
    }
}
