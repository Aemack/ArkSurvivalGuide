using ArkGuide.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArkGuide.ViewModels
{
    public class LoadingPageViewModel : BaseViewModel
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public LoadingPageViewModel()
        {
            
        }


    }
}
