using Microsoft.Maui.Storage;
using System;
using System.IO;
using System.Threading.Tasks;

namespace VDCA.Data
{
    internal class DatabaseManatance
    {
        public DatabaseManatance() { }

        private String dbInBundleFileName = Constants.NAME_DATABASE;

        private void SetBDBundleFileName()
        {
            dbInBundleFileName += ".db";
        }
        //Check to see if the database exist mdail 4-1-23
        public static bool CheckDBExists()
        {
            //uncoment the below two line and comment the third line if database is updated and needs to be copied during debugging the app 5/10/23
            //File.Delete(Constants.SavedFlashCats);
            //bool yes = false;
            bool yes  = File.Exists(Constants.DBPath);
            return yes;
        }
        //if the databse doesn't exist copy it into place mdail 4-1-23
        public async Task CopyDBIfNotExists()
        {
            SetBDBundleFileName();
            if (!DatabaseManatance.CheckDBExists())
            {
                try
                {
                    // Read the source file                   
                    using Stream fs = await FileSystem.Current.OpenAppPackageFileAsync(dbInBundleFileName);
                    //make the taget file
                    string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, dbInBundleFileName);
                    using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
                    using BinaryWriter writer = new(outputStream);
                    using (BinaryReader reader = new(fs))
                    {
                        var bytesRead = 0;
                        const int bufferSize = 1024;
                        var buffer = new byte[bufferSize];
                        using (fs)
                        {
                            do
                            {
                                buffer = reader.ReadBytes(bufferSize);
                                bytesRead = buffer.Length;
                                writer.Write(buffer);
                            }

                            while (bytesRead > 0);
                        }
                        reader?.Close();
                    }
                    writer?.Close();
                    fs?.Close();
                    outputStream?.Close();
                    try
                    {
                        QuestionsDatabase db = new();
                        _ = db.LoadCountDataAsync();
                        _ = db.LoadCountDataFlashOnlyAsync().ConfigureAwait(false);
                        DbVersion dbv = new();
                        Constants.DB_VERSION_NUMBER = await dbv.CheckVersionNo();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Getting data after db copy failed ex: "  + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Copy database failed ex: " + ex.Message);
                }
            }
        }
        //check to see if database exists, if it does delete it, then either way copy the databse into place mdail 4-1-23
        public async Task CopyDB()
        {
            SetBDBundleFileName();
            if (DatabaseManatance.CheckDBExists())
            {
                try
                {
                    File.Delete(System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, Constants.DB_FILE_NAME));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("CopyDB failed to Delete the file try 1 error = " + ex.Message);
                    // Retry the operation after a delay
                    await Task.Delay(1000);
                    if (DatabaseManatance.CheckDBExists())
                    {
                        try
                        {
                            File.Delete(System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, Constants.DB_FILE_NAME));
                        }
                        catch
                        {
                            Console.WriteLine("CopyDB failed to Delete the file try 2 error = " + ex.Message);
                            // If the delete operation fails again, throw the exception
                            //throw;
                        }
                    }
                }
            }
            try
            {
                await using Stream fs = await FileSystem.Current.OpenAppPackageFileAsync(dbInBundleFileName);
                string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, dbInBundleFileName);
                await using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
                using BinaryWriter writer = new(outputStream);
                using BinaryReader reader = new(fs);
                var bytesRead = 0;
                const int bufferSize = 1024;
                var buffer = new byte[bufferSize];
                do
                {
                    buffer = reader.ReadBytes(bufferSize);
                    bytesRead = buffer.Length;
                    writer.Write(buffer);
                }
                while (bytesRead > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CopyDB failed to Copy the database error = " + ex.Message);
                throw;
            }
        }
    }
 }
