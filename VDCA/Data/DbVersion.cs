using SQLite;
using SQLitePCL;
using System;
using System.Threading.Tasks;
using VDCA.Views;

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
            //db = new SQLiteConnection(Constants.DBPath);
        }
        /// <summary>
        /// Factory method to create and initialize a DbVersion instance asynchronously.
        /// </summary>
        public static async Task<DbVersion> CreateAsync()
        {
            var instance = new DbVersion();
            // Check if the database is encrypted
            bool isEncrypted = await SqliteEncryptionDetector.IsDatabaseEncryptedAsync(Constants.DBPath);
            if (!isEncrypted)
            {
                DatabaseManatance dm = new();
                await dm.CopyDB();
            }
            // Initialize the SQLite connection
            var options = new SQLiteConnectionString(Constants.DBPath, true, key: Constants.COPYRIGHT);
            instance.db = new SQLiteConnection(options);

            return instance;
        }
        public async Task<int> CheckVersionNo()
        {
            try
            {
                //current_version = db.ExecuteScalar<int>("pragma user_version;");
                current_version = db.ExecuteScalar<int>("PRAGMA user_version;");
                if(current_version !=-1)
                {
                    Constants.AppVersionNumberInfo.Version_no = current_version;
                    Constants.AppVersionNumberInfo.VersionString = Constants.VersionLabelStringPreDatabase + current_version.ToString();
                }
                db.Close();
                db.Dispose();
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
                        //db = new SQLiteConnection(Constants.DBPath);
                        //current_version = db.ExecuteScalar<int>("pragma user_version;");
                        var options = new SQLiteConnectionString(Constants.DBPath, true, key: Constants.COPYRIGHT);
                        db = new SQLiteConnection(options);
                        current_version = db.ExecuteScalar<int>("PRAGMA user_version;");
                        Constants.AppVersionNumberInfo.Version_no = current_version;
                        Constants.AppVersionNumberInfo.VersionString = Constants.VersionLabelStringPreDatabase + current_version.ToString();
                        db.Close();
                        db.Dispose();
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
