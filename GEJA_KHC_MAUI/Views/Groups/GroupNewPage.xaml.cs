using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using GEJA_KHC_MAUI.Models;
using GEJA_KHC_MAUI.ViewModels;

namespace GEJA_KHC_MAUI.Views;

public partial class GroupNewPage : ContentPage
{
    GroupViewModel viewModel;
    
	public GroupNewPage()
	{
		InitializeComponent();

        this.BindingContext = viewModel = new GroupViewModel(new Group());

        // Get the last group and calculate the new group id
        var lastGroup = DbViewModel.GetGroupList().Result.OrderByDescending(d => d.GroupId).FirstOrDefault();
        entryGroupId.Text = (lastGroup == null) ? "1" : (lastGroup.GroupId + 1).ToString();

        // After typing in one entry, automatically jump to the next one
        entryGroupName.Completed += (object sender, EventArgs e) => { entryPastor.Focus(); };
        entryPastor.Completed += (object sender, EventArgs e) => { entryRemark.Focus(); };
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
            await Task.Run(() => DbViewModel.SetGroup(viewModel.Group));
            await Toast.Make("ምድብ '" + viewModel.Group.ToString() + "' በትክክል ተመዝግቧል", ToastDuration.Long, 14).Show((new CancellationTokenSource()).Token);
        }
        catch (Exception err)
        {
            await DisplayAlert("ስህተት", err.Message, "እሺ");

            return;
        }

        // Go back to group main page
        await Navigation.PopAsync();
    }

    private async void btnCancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}