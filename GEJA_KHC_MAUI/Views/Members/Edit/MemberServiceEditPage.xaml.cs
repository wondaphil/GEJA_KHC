using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using GEJA_KHC_MAUI.Models;
using GEJA_KHC_MAUI.ViewModels;

namespace GEJA_KHC_MAUI.Views;

public partial class MemberServiceEditPage : ContentPage
{
    MemberService memberservice = new MemberService();
    MemberServiceViewModel viewModel;

    public MemberServiceEditPage(MemberService _memberservice)
	{
		InitializeComponent();

        memberservice = _memberservice;

        try
        {
            this.BindingContext = viewModel = new MemberServiceViewModel(_memberservice);

            ddlServiceType.SetBinding(Picker.ItemsSourceProperty, "ServiceTypeList");
            ddlServiceType.SetBinding(Picker.SelectedIndexProperty, "ServiceTypeIndex");

            ddlService.SetBinding(Picker.ItemsSourceProperty, "ServiceList");
            ddlService.SetBinding(Picker.SelectedIndexProperty, "ServiceIndex");
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
            Member member = DbViewModel.GetMember(viewModel.MemberService.MemberId).Result;
            await Task.Run(() => DbViewModel.SetMemberService(viewModel.MemberService));
            await Toast.Make("የአባል '" + member.ToString() + "' የአገልግሎት መረጃ ተስተካክሏል", ToastDuration.Long, 14).Show((new CancellationTokenSource()).Token);
        }
        catch (Exception err)
        {
            await DisplayAlert("Error", err.Message, "Ok");

            return;
        }

        await Navigation.PopAsync();
    }

    private async void btnDelete_Clicked(object sender, EventArgs e)
    {
        Member member = DbViewModel.GetMember(viewModel.MemberService.MemberId).Result;
        bool action = await DisplayAlert("ጌጃ ቃ/ሕ", "'ይህንን የአገልግሎት መረጃ ለመሰረዝ እርግጠኛ ነህ?", "አዎን", "አይደለሁም");

        if (action)
        {
            // Delete the current MemberService
            await Task.Run(() => DbViewModel.DeleteMemberService(memberservice.Id));
            await Toast.Make("የአገልግሎት መረጃው በሚገባ ተሰርዟል", ToastDuration.Long, 14).Show((new CancellationTokenSource()).Token);

            // Go back to Pier and Foundation list page
            await Navigation.PopAsync();
        }
    }
}