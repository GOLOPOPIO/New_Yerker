using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using New_Yerker.Model.DataModel;

namespace New_Yerker.Model.DataBase
{
    public class PositionDB
    {
        private readonly SQLiteAsyncConnection db;

        public PositionDB(string conectionString)
        {
            db = new SQLiteAsyncConnection(conectionString);

            db.CreateTableAsync<Position>().Wait();
        }

        public Task<List<Position>> GetPosAsync()
        {
            return db.Table<Position>().ToListAsync();
        }

        public Task<Position> GetPosIdAsync(string group)
        {
            return db.Table<Position>()
                .Where(i => i.Group == group)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Position pos)
        {
            if (pos.Group != "")
                return db.UpdateAsync(pos);

            else
                return db.InsertAsync(pos);
        }

        public Task<int> DeletePosAsync(Position pos)
        {
            return db.DeleteAsync(pos);
        }
    }
}
