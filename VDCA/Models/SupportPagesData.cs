using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDCA.CustomControl;

namespace VDCA.Models
{
    public partial class SupportPagesData : INotifyPropertyChanged
    {

        private SupportedPages LocalPageName { get; set; }
        public SupportedPages PageName
        {
            get
            {
                return LocalPageName;
            }
            set
            {
                if (LocalPageName != value)
                {
                    LocalPageName = value;
                    OnPropertyChanged(nameof(PageName));
                }
            }
        }
        private string LocalControlExplanationText { get; set; }
        public string ControlExplainationText
        {
            get
            {
                return LocalControlExplanationText;
            }
            set
            {
                if (LocalControlExplanationText != value)
                {
                    LocalControlExplanationText = value;
                    OnPropertyChanged(nameof(ControlExplainationText));
                }
            }
        }
        private int LocalVersionNumber { get; set; }
        public int VersionNumber
        {
            get
            {
                return LocalVersionNumber;
            }
            set
            {
                if (LocalVersionNumber != value)
                {
                    LocalVersionNumber = value;
                    OnPropertyChanged(nameof(VersionNumber));
                }
            }
        }
        private string LocalControlName { get; set; }
        public string ControlName
        {
            get
            {
                return LocalControlName;
            }
            set
            {
                if (LocalControlName != value)
                {
                    LocalControlName = value;
                    OnPropertyChanged(nameof(ControlName));
                }
            }
        }
        private ObservableCollection<ArrowDirection> LocalArrowDirection { get; set; }
        public ObservableCollection<ArrowDirection> ArrowDirection
        {
            get
            {
                return LocalArrowDirection;
            }
            set
            {
                if (LocalArrowDirection != value)
                {
                    LocalArrowDirection = value;
                    OnPropertyChanged(nameof(ArrowDirection));
                }
            }
        }
        private bool LocalSupportRequired { get; set; }
        public bool SupportRequired
        {
            get
            {
                return LocalSupportRequired;
            }
            set
            {
                if (LocalSupportRequired != value)
                {
                    LocalSupportRequired = value;
                    OnPropertyChanged(nameof(SupportRequired));
                }
            }
        }

        private bool LocalDontShowSupport { get; set; }
        public bool DontShowSupport
        {
            get
            {
                return LocalDontShowSupport;
            }
            set
            {
                if (LocalDontShowSupport != value)
                {
                    LocalDontShowSupport = value;
                    OnPropertyChanged(nameof(DontShowSupport));
                }
            }
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
