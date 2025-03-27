//using ABI.Windows.UI;
using System.ComponentModel;
using SQLite;
using System;
using System.Runtime.CompilerServices;

namespace VDCA.Models
{
    [Serializable]
    [Table("VAQuestions")]
    public partial class Categories : INotifyPropertyChanged
    {
        public Categories()
        {
            // Initialize properties with default values
            SelectedCheckTextColorLocal = Microsoft.Maui.Graphics.Color.FromArgb("#000080"); // Default unselected color
            SelectedBackgroundColorLocal = Microsoft.Maui.Graphics.Color.FromArgb("#ffffff"); // Default unselected background color
        }
        //public string VM_Categories { get; set; }
        [Column("Count")]
        public int CategoryCount { get; set; }

        [Column("Category")]
        public string CategoryName { get; set; }
        private bool IsCheckedLocal { get; set; } = false;
        public bool ItIsChecked
        {
            get
            {
                return IsCheckedLocal;
            }
            set
            {
                if (IsCheckedLocal != value)
                {
                    IsCheckedLocal = value;
                    SetColors();
                    OnPropertyChanged(nameof(ItIsChecked));
                }
            }
        }
        public void SetColors()
        {
            if (IsCheckedLocal)
            {
                SelectedCheckTextColor = Microsoft.Maui.Graphics.Color.FromArgb("#ffffff");
                SelectedBackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#ffc800");
                //CheckImage = "checkmark_square.png";
            }
            else
            {
                SelectedCheckTextColor = Microsoft.Maui.Graphics.Color.FromArgb("#000080");
                SelectedBackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#ffffff");
                //CheckImage = "unchecked_square.png";
            }
        }
        private Microsoft.Maui.Graphics.Color SelectedCheckTextColorLocal { set; get; }
        [Ignore]
        public Microsoft.Maui.Graphics.Color SelectedCheckTextColor
        {
            get
            {
                return SelectedCheckTextColorLocal;
            }
            set
            {
                if (SelectedCheckTextColorLocal != value)
                {

                    SelectedCheckTextColorLocal = value;
                    OnPropertyChanged(nameof(SelectedCheckTextColor));
                }
            }
        }
        private Microsoft.Maui.Graphics.Color SelectedBackgroundColorLocal { set; get; }
        [Ignore]
        public Microsoft.Maui.Graphics.Color SelectedBackgroundColor
        {
            get
            {
                
                return SelectedBackgroundColorLocal;
            }
            set
            {
                if (SelectedBackgroundColorLocal != value)
                {

                    SelectedBackgroundColorLocal = value;
                    OnPropertyChanged(nameof(SelectedBackgroundColor));
                }
            }
        }

        //set the selected var mdail 3-23-23
        public void SetIsSelected(bool _isSelected)
        {
            ItIsChecked = _isSelected;
        }
        //toggles the selected var mdail 3-23-23
        public void ToggleSelected()
        {
            ItIsChecked = !ItIsChecked;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
