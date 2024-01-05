using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using GEJA_KHC_MAUI.Models;
using GEJA_KHC_MAUI.ViewModels;
namespace GEJA_KHC_MAUI.Views;

public partial class PhotoPage : ContentPage
{
    Member member = new Member();
    MemberPhotoViewModel viewModel;

    private FileResult photoFile;

    public PhotoPage(Member _member)
	{
		InitializeComponent();

        member = _member;
        this.BindingContext = viewModel = new MemberPhotoViewModel(member);

        MemberPhoto photo = DbViewModel.GetMemberPhoto(member.MemberId).Result;
        entryMemberId.Text = member.MemberId;
        lblMemberName.Text = member.FullName + " (" + member.MemberId + ")";
        // Convert byte[] to stream
        //if (viewModel.Photo != null)
        //{
        //    var stream = new MemoryStream(viewModel.Photo);
        //    imgSelectedPhoto.Source = ImageSource.FromStream(() => stream);
        //}
    }

    private async void btnChoosePhotoFile_Clicked(object sender, EventArgs e)
    {
        string photoFileName;
        
        try
        {
            photoFile = await MediaPicker.Default.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "እባክዎን ፎቶ ይምረጡ"
            });
            
            if (photoFile != null)
            {
                // save the file into local storage
                photoFileName = photoFile.FileName;

                Stream sourceStream = await photoFile.OpenReadAsync();
                imgSelectedPhoto.Source = ImageSource.FromStream(() => sourceStream);
                entryPhotoFilePath.Text = photoFileName;

                // Convert the stream data to byte[] to be saved to database
                viewModel.Photo = ConvertImageStreamToByteArray(sourceStream);
            }
            
            else
            {
                await Toast.Make("የፎቶ ፋይል በትክክል አልመረጡም። . \nእባክዎን ከንደገና ይሞክሩ.", ToastDuration.Long, 14).Show((new CancellationTokenSource()).Token);
                return;
            }
        }
        catch (PermissionException pEx)
        {
            await DisplayAlert("ስህተት ተፈጥሯል", pEx.Message, "Ok");
            return;
        }
        catch (Exception ex)
        {
            await DisplayAlert("ስህተት ተፈጥሯል", ex.Message, "Ok");
            return;
        }

        await Toast.Make("የፎቶ ፋይል ተመርጧል", ToastDuration.Long, 14).Show((new CancellationTokenSource()).Token);
    }

    private async void btnCapturePhoto_Clicked(object sender, EventArgs e)
    {
        string photoFileName;
        
        try
        {
            //photo = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions{});
            photoFile = await MediaPicker.Default.CapturePhotoAsync();

            if (photoFile != null)
            {
                photoFileName = photoFile.FileName;

                Stream sourceStream = await photoFile.OpenReadAsync();
                imgSelectedPhoto.Source = ImageSource.FromStream(() => sourceStream);
                entryPhotoFilePath.Text = photoFileName;

                // Convert the stream data to byte[] to be saved to database
                viewModel.Photo = ConvertImageStreamToByteArray(sourceStream);
            }

            else
            {
                await Toast.Make("ፎቶ በትክክል አላነሱም። . \nእባክዎን ከንደገና ይሞክሩ.", ToastDuration.Long, 14).Show((new CancellationTokenSource()).Token);
                return;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("ስህተት ተፈጥሯል", ex.Message, "Ok");
            return;
        }

        await Toast.Make("ያንሱት ፎቶ ተዘጋጅቷል", ToastDuration.Long, 14).Show((new CancellationTokenSource()).Token);
    }

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
        if (!viewModel.SaveSuccess)
        {
            await DisplayAlert("ጌጃ ቃ/ሕ", "እባክዎ የፎቶ ፋይል ይምረጡ። \nየአባሉ ፎቶ ሴቭ አልተደረገም!", "እሺ");
            return;
        }

        try
        {
            // Upload photo file to server
            if (photoFile != null)
            {
                // Display a spinner popup
                var popup = new SpinnerPopup();
                this.ShowPopup(popup);

                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(await photoFile.OpenReadAsync()), "file", photoFile.FileName);

                var httpClient = new HttpClient();
                string webApiServerUrl = Preferences.Get("HttpProtocol", "") + Preferences.Get("ServerAddress", "");
                var response = await httpClient.PostAsync(WebApiURL.Address + "/api/GejaKhcAPI/UploadImage", content);

                popup.Close();
                //lblStatus.Text = response.StatusCode.ToString();

                // Save photo to database
                await Task.Run(() => DbViewModel.SetMemberPhoto(viewModel.MemberPhoto));
                await Toast.Make("የአባል '" + member.ToString() + "' ፎቶ ሴቭ ተደርጓል", ToastDuration.Long, 14).Show((new CancellationTokenSource()).Token);
            }
        }
        catch (Exception err)
        {
            await DisplayAlert("ስህተት", err.Message, "እሺ");

            return;
        }

        // Go back to Member main page
        await Navigation.PopAsync();
    }

    private async void btnCancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    /// <summary>
    /// Convert Stream image data to byte array
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private byte[] ConvertImageStreamToByteArray(Stream input)
    {
        var buffer = new byte[16 * 1024];
        using (MemoryStream ms = new MemoryStream())
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            return ms.ToArray();
        }
    }
}