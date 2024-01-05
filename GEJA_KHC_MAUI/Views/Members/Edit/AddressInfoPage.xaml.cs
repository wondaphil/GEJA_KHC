using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using GEJA_KHC_MAUI.Models;
using GEJA_KHC_MAUI.ViewModels;

namespace GEJA_KHC_MAUI.Views;

public partial class AddressInfoPage : ContentPage
{
    Member member = new Member();
    AddressInfoViewModel viewModel;

    public AddressInfoPage(Member _member)
    {
        InitializeComponent();

        member = _member;
        this.BindingContext = viewModel = new AddressInfoViewModel(member);

        entryMemberId.SetBinding(Entry.TextProperty, "MemberId");

        ddlSubcity.SetBinding(Picker.ItemsSourceProperty, "SubcityList");
        ddlSubcity.SetBinding(Picker.SelectedIndexProperty, "SubcityIndex");

        ddlHouseOwnershipType.SetBinding(Picker.ItemsSourceProperty, "HouseOwnershipTypeList");
        ddlHouseOwnershipType.SetBinding(Picker.SelectedIndexProperty, "HouseOwnershipTypeIndex");

        AddressInfo addressinfo = DbViewModel.GetAddressInfo(member.MemberId).Result;
        entryMemberId.Text = addressinfo.MemberId;
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
            await Task.Run(() => DbViewModel.SetAddressInfo(viewModel.AddressInfo));
            await Toast.Make("የአባል '" + member.ToString() + "' የአድራሻ መረጃ ሴቭ ተደርጓል", ToastDuration.Long, 14).Show((new CancellationTokenSource()).Token);
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