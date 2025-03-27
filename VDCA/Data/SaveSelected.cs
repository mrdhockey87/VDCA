using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace VDCA.Data
{
    internal class SaveSelected
    {
        public SaveSelected() { }

        /*
         * To Write to file do (Form a list - List<VM_Categories> VM_Categories)
         * SaveSelected.WriteToXmlFile<List<VM_Categories>>(Constants.SavedCategories, VM_Categories);
         * 
         * To read the data back to a list of categoies do 
         * List<VM_Categories> VM_Categories = SaveSelected.ReadFromXmlFile<List<VM_Categories>>(Constants.SavedCategories);
         */
        public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            //string serializationFile = Path.Combine(Constants.DEVICE_PUBLIC_FOLDER, "savedcategories.xml");
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, append);
                serializer.Serialize(writer, objectToWrite);
            }
            catch (Exception ex)
            {
                Console.Write("Error saving VM_Categories ex: " + ex.Message);
            }
            finally
            {
                writer?.Close();
            }
        }
        public static T ReadFromXmlFile<T>(string filePath) where T : new()
        {
            // Check if the file exists
            if (!File.Exists(filePath))
            {
                WriteToXmlFile(filePath, new List<T>());
            }
            // Deserialize the file
            TextReader reader = null;
            try
            {
                using (reader = new StreamReader(filePath))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new T(); //Return an empty T if deserialization fails
            }
            finally
            {
                reader?.Close();
            }
        }
    }
}
