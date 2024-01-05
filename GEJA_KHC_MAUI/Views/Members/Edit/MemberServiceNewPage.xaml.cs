using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using GEJA_KHC_MAUI.Models;
using GEJA_KHC_MAUI.ViewModels;

namespace GEJA_KHC_MAUI.Views;


public partial class MemberServiceNewPage : ContentPage
{
    MemberServiceViewModel viewModel;

    public MemberServiceNewPage(Member _member)
	{
		InitializeComponent();

        try
        {
            this.BindingContext = viewModel = new MemberServiceViewModel(new MemberService() { MemberId = _member.MemberId });

            ddlServiceType.SetBinding(Picker.ItemsSourceProperty, "ServiceTypeList");
            ddlServiceType.SetBinding(Picker.SelectedIndexProperty, "ServiceTypeIndex");

            ddlService.SetBinding(Picker.ItemsSourceProperty, "ServiceList");
            ddlService.SetBinding(Picker.SelectedIndexProperty, "ServiceIndex");

            entryMemberId.Text = _member.MemberId;
            //entryId.Text = newId;
        }
        catch (Exception ex)
        {
            DisplayAlert("ስህተት", ex.InnerException.Message, "እሺ");
        }
    }

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
        if (!viewModel.SaveSuccess)
        {
            await DisplayAlert("ጌጃ ቃ/ሕ", "እባክዎ ሁሉንም ሳጥኖች ይምረጡ። \nየአባሉ የአገልግሎት መረጃ ሴቭ አልተደረገም!", "እሺ");
            return;
        }

        try
        {
            // Save to database
            Member member = DbViewModel.GetMember(viewModel.MemberId).Result;
            await Task.Run(() => DbViewModel.SetMemberService(viewModel.MemberService));
            await Toast.Make("የአባል '" + member.ToString() + "' የአገልግሎት መረጃ በትክክል ተመዝግቧል", ToastDuration.Long, 14).Show((new CancellationTokenSource()).Token);
        }
        catch (Exception err)
        {
            await DisplayAlert("ስህተት", err.Message, "እሺ");

            return;
        }

        await Navigation.PopAsync();
    }

    private async void btnCancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}