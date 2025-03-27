using SQLite;
using System;
using System.Threading.Tasks;

namespace VDCA.Data
{
    internal class DbVersion
    {
        private readonly String LOGTAG = "DbVersion";
        private SQLiteConnection db;
        //private readonly string tableName = "version_no";
        private int current_version = -1;

        public DbVersion()
        {
            db = new SQLiteConnection(Constants.DBPath);
        }

        public async Task<int> CheckVersionNo()
        {
            try
            {
                current_version = db.ExecuteScalar<int>("pragma user_version;");
                Console.WriteLine(LOGTAG + " Version Number:  " + current_version.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(LOGTAG + " Error in GetVersionNo error: " + e.Message);
            }
            finally
            {
                db.Close();
                if (current_version != Constants.DATABASE_VERSION)
                {
                    DatabaseManatance dm = new();
                    await dm.CopyDB();
                    try
                    {
                        db = new SQLiteConnection(Constants.DBPath);
                        current_version = db.ExecuteScalar<int>("pragma user_version;");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(LOGTAG + " Error in GetVersionNo error: " + e.Message);
                    }
                    db.Close();

                }
            }
            return current_version;
        }

    }
}
