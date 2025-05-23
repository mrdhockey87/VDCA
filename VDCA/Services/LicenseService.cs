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
        // Read JSON file containing license metadata
        public async Task<LicenseInfo[]> GetLicenseInfoAsync()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("Licenses/vdca_licenses.json");
                using var reader = new StreamReader(stream);
                var jsonContent = await reader.ReadToEndAsync();

                return JsonSerializer.Deserialize<LicenseInfo[]>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                // Handle file not found or parsing errors
                System.Diagnostics.Debug.WriteLine($"Error loading licenses.json: {ex.Message}");
                return Array.Empty<LicenseInfo>();
            }
        }

        // Read individual license text file
        public async Task<string> GetLicenseTextAsync(string fileName)
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
        public async Task<bool> LicenseFileExistsAsync(string fileName)
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
