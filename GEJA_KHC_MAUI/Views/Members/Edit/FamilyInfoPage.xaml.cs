using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using GEJA_KHC_MAUI.Models;
using GEJA_KHC_MAUI.ViewModels;

namespace GEJA_KHC_MAUI.Views;

public partial class FamilyInfoPage : ContentPage
{
    Member member = new Member();
    FamilyInfoViewModel viewModel;

    public FamilyInfoPage(Member _member)
	{
		InitializeComponent();

        member = _member;
        this.BindingContext = viewModel = new FamilyInfoViewModel(member);

        entryMemberId.SetBinding(Entry.TextProperty, "MemberId");

        ddlMaritalStatus.SetBinding(Picker.ItemsSourceProperty, "MaritalStatusList");
        ddlMaritalStatus.SetBinding(Picker.SelectedIndexProperty, "MaritalStatusIndex");

        ddlMarriageYear.SetBinding(Picker.ItemsSourceProperty, "MarriageYearList");
        ddlMarriageYear.SetBinding(Picker.SelectedIndexProperty, "MarriageYearIndex");

        FamilyInfo familyinfo = DbViewModel.GetFamilyInfo(member.MemberId).Result;
        entryMemberId.Text = familyinfo.MemberId;
        lblMemberName.Text = member.FullName + " (" + member.MemberId + ")";

        SetEntriesOrder();
    }
    private async void btnSave_Clicked(object sender, EventArgs e)
    {
        if (!viewModel.SaveSuccess)
        {
            await DisplayAlert("ጌጃ ቃ/ሕ", "እባክዎ * ያለባቸውን ሳጥኖች ያስገቡ። \nየአባሉ መረጃ ሴቭ አልተደረገም!", "እሺ");
            return;
        }

        try
        {
            // Save to database
            await Task.Run(() => DbViewModel.SetFamilyInfo(viewModel.FamilyInfo));
            await Toast.Make("የአባል '" + member.ToString() + "' የቤተሰብ መረጃ ሴቭ ተደርጓል", ToastDuration.Long, 14).Show((new CancellationTokenSource()).Token);
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

    private void SetEntriesOrder()
    {
    }
}