using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VDCA.Models;

namespace VDCA.Services
{
    public class LicenseService
    {
        // Reusable JsonSerializerOptions instance
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        // Read JSON file containing license metadata
        public static async Task<LicenseInfo[]> GetLicenseInfoAsync()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("Licenses/vdca_licenses.json");
                using var reader = new StreamReader(stream);
                var jsonContent = await reader.ReadToEndAsync();
                var jsonlist = JsonSerializer.Deserialize<LicenseInfo[]>(jsonContent, _jsonOptions);
                return jsonlist; // JsonSerializer.Deserialize<LicenseInfo[]>(jsonContent, _jsonOptions);
            }
            catch (Exception ex)
            {
                // Handle file not found or parsing errors
                System.Diagnostics.Debug.WriteLine($"Error loading licenses.json: {ex.Message}");
                return [];
            }
        }

        // Read individual license text file
        public static async Task<string> GetLicenseTextAsync(string fileName)
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync($"Licenses/{fileName}");
                using var reader = new StreamReader(stream);
                return await reader.ReadToEndAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading license file {fileName}: {ex.Message}");
                return "License text not available.";
            }
        }

        // Check if a license file exists
        public static async Task<bool> LicenseFileExistsAsync(string fileName)
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync($"Licenses/{fileName}");
                return stream != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
