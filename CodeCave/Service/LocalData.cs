using CodeCave.Model;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CodeCave.Service
{
    public static class LocalData
    {
        private static SQLiteAsyncConnection db;
        private static async Task InitDb()
        {
            if (db != null) return;
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyDB.db");
            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<Note>();
        }

        #region Note DB
        public static async Task InsertNewNoteAsync(Note note)
        {
            await InitDb();
            await db.InsertAsync(note);
        }
        public static async Task DeleteNoteAsync(string id)
        {
            await InitDb();
            await db.DeleteAsync<Note>(id);
        }
        public static async Task<List<Note>> GetNotesAsync()
        {
            await InitDb();
            List<Note> notes = await db.Table<Note>().ToListAsync();
            var n = notes.OrderByDescending(a => a.ID).ToList();
            return n;
        }
        public static async Task UpdateNoteAsync(Note note)
        {
            await InitDb();
            await db.UpdateAsync(note);
        }
        #endregion
    }
}
