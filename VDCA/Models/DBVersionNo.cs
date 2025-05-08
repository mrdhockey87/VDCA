using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VDCA.Models
{
    [Table("version_no")]
    public class DBVersionNo : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        private int VersionNoLocal { get; set; } = 0;
        public int Version_no 
        {
            get
            {
                return VersionNoLocal;
            }
            set
            {
                VersionNoLocal = value;
                OnPropertyChanged(nameof(Version_no));
            }
        } 

        private string VersionStringLocal { get; set; } = "";
        public string VersionString
        {
            get => VersionStringLocal;
            set
            {
                VersionStringLocal = value;
                OnPropertyChanged(nameof(VersionString));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}


/*
 *  v 2.001.0087 move the databse to the raw subdirectory of the resources directory so it copies into the app bundle properly mdail 4-1-23
 *  v 2.001.0086 fix the sql so it works on all platforms by installing the SQLitePCLRaw.bundle_green nuget as it seems that sqlite-net-pcl
 *               nuget is using an older version mdail 4-1-23
 *  v 2.001.0085 fix the file copy so it works properly to copy the database to the local directory on the appropiate systems mdail 4-1-23
 */