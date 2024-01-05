using System.Collections.ObjectModel;
using GEJA_KHC_MAUI.Models;

namespace GEJA_KHC_MAUI.ViewModels
{
    public class MemberPhotoViewModel : GKHCBaseViewModel
    {
        private Member _Member = new Member();
        private MemberPhoto _MemberPhoto = new MemberPhoto();
        private MemoryStream _PhotoStream = new MemoryStream();

        private Command _SaveCommand;
        private bool _SaveSuccess;

        public MemberPhotoViewModel(Member member)
        {
            _Member = member;
            if (string.IsNullOrEmpty(_Member.MemberId))
                return;

            _MemberPhoto = DbViewModel.GetMemberPhoto(_Member.MemberId).Result;

        }

        public MemberPhoto MemberPhoto
        {
            get { return _MemberPhoto; }
            set
            {
                _MemberPhoto = value;
                OnPropertyChanged(nameof(MemberPhoto));
            }
        }

        public string MemberId
        {
            get { return _MemberPhoto.MemberId; }
            set
            {
                _MemberPhoto.MemberId = value;
                OnPropertyChanged(nameof(MemberId));
            }
        }

        public string PhotoFilePath
        {
            get { return _MemberPhoto.PhotoFilePath; }
            set
            {
                _MemberPhoto.PhotoFilePath = "/photos/" + value;
                OnPropertyChanged(nameof(PhotoFilePath));
            }
        }

        public byte[] Photo
        {
            get { return _MemberPhoto.Photo; }
            set
            {
                _MemberPhoto.Photo = value;
                OnPropertyChanged(nameof(Photo));
                OnPropertyChanged(nameof(PhotoStream));
            }
        }

        public string PhotoUrl
        {
            get
            {
                if (_MemberPhoto.PhotoFilePath != null)
                {
                    if (_MemberPhoto.PhotoFilePath.Trim() != "")
                        return DbViewModel.GetWebApplicationUrl() + _MemberPhoto.PhotoFilePath;
                }

                return "no_photo";
            }
        }

        public ImageSource PhotoStream
        {
            get
            {
                if (_MemberPhoto.Photo != null)
                    _PhotoStream = new MemoryStream(_MemberPhoto.Photo);

                return ImageSource.FromStream(() => _PhotoStream);
            }
            //set
            //{
            //    _PhotoStream = value;
            //    //if (_MemberPhoto.Photo != null)
            //    //    _PhotoStream = new MemoryStream(value);
            //    OnPropertyChanged(nameof(PhotoStream));
            //}
        }

        public string Remark
        {
            get { return _MemberPhoto.Remark; }
            set
            {
                _MemberPhoto.Remark = value;
                OnPropertyChanged(nameof(Remark));
            }
        }

        public bool SaveSuccess
        {
            get
            {
                return _SaveSuccess;
            }
        }

        public Command SaveCommand
        {
            get
            {
                return _SaveCommand ?? (_SaveCommand = new Command(async () => await ExecuteOnMemberPhotoSave()));
            }
        }

        private async Task ExecuteOnMemberPhotoSave()
        {
            try
            {
                // If at least one required field is not entered or selected, return false. Otherwise, return true
                _SaveSuccess = (_MemberPhoto.PhotoFilePath != null && _MemberPhoto.PhotoFilePath.Trim() != "");
            }
            catch (Exception)
            {
                await Task.Run(() => _SaveSuccess = false);
            }
        }
    }
}
