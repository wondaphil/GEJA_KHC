using GEJA_KHC_MAUI.Models;
using GEJA_KHC_MAUI.ViewModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace GEJA_KHC_MAUI.Views;

public partial class GroupEditPage : ContentPage
{
    Group group = new Group();
    GroupViewModel viewModel;

    public GroupEditPage(Group _group)
	{
        this.Title = "Edit Group";
        InitializeComponent();

        group = _group;
        this.BindingContext = viewModel = new GroupViewModel(_group);
    }

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
        if (!viewModel.SaveSuccess)
        {
            await DisplayAlert("ጌጃ ቃ/ሕ", "እባክዎ ሁሉንም ሳጥኖች ያስገቡ። \nየምድብ መረጃ ሴቭ አልተደረገም!", "እሺ");
            return;
        }

        try
        {
            // Save to database
            group = viewModel.Group;
            await DbViewModel.SetGroup(group);
        }
        catch (Exception err)
        {
            await DisplayAlert("Error", err.Message, "Ok");

            return;
        }

        //await DisplayAlert("ጌጃ ቃ/ሕ", "ምድብ '" + group.Totring() + "' በትክክል ተስተካክሏል", "እሺ");
        await Toast.Make("ምድብ '" + group.ToString() + "' በትክክል ተስተካክሏል", ToastDuration.Long, 14).Show((new CancellationTokenSource()).Token);

        // Go back to group detail page
        await Navigation.PopAsync();
    }

    private async void btnCancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}