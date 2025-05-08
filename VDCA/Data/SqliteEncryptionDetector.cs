using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDCA.Data
{
    public static class SqliteEncryptionDetector
    {
        /// <summary>
        /// Checks if a SQLite database is encrypted
        /// </summary>
        /// <param name="databasePath">Full path to the SQLite database file</param>
        /// <returns>True if encrypted, false if unencrypted or not a valid database</returns>
        public static async Task<bool> IsDatabaseEncryptedAsync(string databasePath)
        {
            // First check if file exists
            if (!File.Exists(databasePath))
                return false;
            SQLiteConnection connection = null;
            try
            {
                // Try to open without password
                connection = new SQLiteConnection(databasePath);
                // Try to read user_version
                int version = connection.ExecuteScalar<int>("PRAGMA user_version");
                // If we get here, database is not encrypted
                return false;
            }
            catch (SQLiteException ex)
            {
                // Check for typical encryption error messages
                if (ex.Message.Contains("file is not a database") ||
                    ex.Message.Contains("file is encrypted") ||
                    ex.Message.Contains("not a database") ||
                    ex.Message.Contains("unsupported file format"))
                {
                    return true;
                }
                // If it's not a typical encryption error, check the file header
                return await IsLikelyEncryptedAsync(databasePath);
            }
            catch
            {
                // Any other exception, let's check the file header
                return await IsLikelyEncryptedAsync(databasePath);
            }
            finally
            {
                // Ensure the connection is closed and disposed
                if (connection != null)
                {
                    await Task.Run(() =>
                    {
                        connection.Close();
                        connection.Dispose();
                    });
                }
            }
        }
        /// <summary>
        /// Examines SQLite file header to determine if it's likely encrypted
        /// </summary>
        private static async Task<bool> IsLikelyEncryptedAsync(string databasePath)
        {
            try
            {
                // SQLite files start with "SQLite format 3\0"
                // If file doesn't have this header, it's either not SQLite or encrypted
                await using FileStream fileStream = new(databasePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
                if (fileStream.Length < 16)
                    return false; // Too small to be a valid SQLite database
                byte[] header = new byte[16];
                await fileStream.ReadExactlyAsync(header, 0, 16);
                string headerString = Encoding.ASCII.GetString(header);
                return !headerString.StartsWith("SQLite format 3");
            }
            catch
            {
                // If we can't read the file at all, we can't tell
                return false;
            }
        }
    }
}
