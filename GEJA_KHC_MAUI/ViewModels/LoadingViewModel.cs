
namespace GEJA_KHC_MAUI.ViewModels
{
    public class LoadingViewModel : GKHCBaseViewModel
    {
        private bool _LoadingInProgress;
        private string _LoadingStatusText;
        
        public LoadingViewModel(string LoadingStatusText)
        {
            _LoadingInProgress = false;
            _LoadingStatusText = LoadingStatusText;
        }

        public bool LoadingInProgress
        {
            get { return _LoadingInProgress; }
            set
            {
                _LoadingInProgress = value;
                OnPropertyChanged(nameof(LoadingInProgress));
            }
        }

        public string LoadingStatusText
        {
            get { return _LoadingStatusText; }
        }
    }
}
