using Com.Zebra.Rfid.Api3;
using ITGBrands.CSFT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ITGBrands.CSFT.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        // INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ZebraRFIDService _zebrasdk;

        //private static Lazy<ReaderModel> lazyRfidModel = new Lazy<ReaderModel>(() => ReaderModel.readerModel);
        //public static ReaderModel rfidModel => lazyRfidModel.Value;

        // public static ReaderModel rfidModel = ReaderModel.readerModel;

        //private static ScannerModel scannerModel = ScannerModel.scannerModel;



        public BaseViewModel()
        {

        }

        //  public bool isConnected { get => rfidModel.isConnected; set => OnPropertyChanged(); }

        public virtual void HHTriggerEvent(bool pressed)
        {

        }

        public virtual void TagReadEvent(TagData[] tags)
        {

        }

        public virtual void StatusEvent(IEvents.StatusEventData statusEvent)
        {

        }

       
        public virtual void ReaderAppearanceEvent(bool appeared)
        {

        }

        //  public bool isConnected { get => rfidModel.isConnected; set => OnPropertyChanged(); }


     


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        // Busy state management
        private bool isBusy = false;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        // Title property for the ViewModel
        private string title = string.Empty;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        // Command example
        public ICommand ExampleCommand { get; protected set; }
    }
}
