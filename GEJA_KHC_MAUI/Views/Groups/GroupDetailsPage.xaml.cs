using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using GEJA_KHC_MAUI.Models;
using GEJA_KHC_MAUI.ViewModels;

namespace GEJA_KHC_MAUI.Views;

public partial class GroupDetailsPage : ContentPage
{
    Group group = new Group();
    GroupViewModel viewModel;

    public GroupDetailsPage(Group _group)
    {
        this.Title = "የምድብ ዝርዝር መረጃ";
        InitializeComponent();

        group = _group;
        this.BindingContext = viewModel = new GroupViewModel(_group);

        ToolbarItem edit = new ToolbarItem
        {
            Text = "አስተካክል",
            IconImageSource = ImageSource.FromFile("icon_edit_white.png"),
            Order = ToolbarItemOrder.Primary,
            Priority = 0
        };

        ToolbarItem delete = new ToolbarItem
        {
            Text = "ሰርዝ",
            IconImageSource = ImageSource.FromFile("icon_delete_white.png"),
            Order = ToolbarItemOrder.Primary,
            Priority = 0
        };

        edit.Clicked += OnEditClicked;
        delete.Clicked += OnDeleteClicked;

        // "this" refers to a Page object
        this.ToolbarItems.Add(edit);
        this.ToolbarItems.Add(delete);
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GroupEditPage(group));
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        bool action = await DisplayAlert("ጌጃ ቃ/ሕ", "'" + group.ToString() + "'ን ለመሰረዝ እርግጠኛ ነህ?", "አዎን", "አይደለሁም");

        if (action)
        {
            try
            {
                // Delete the current group
                await DbViewModel.DeleteGroup(group.GroupId);
            }
            catch (Exception err)
            {
                await DisplayAlert("ስህተት", err.Message, "Ok");

                return;
            }

            await Toast.Make("የምድቡ መረጃ በሚገባ ተሰርዟል", ToastDuration.Long, 14).Show((new CancellationTokenSource()).Token);

            // Go back to group detail page
            await Navigation.PopAsync();
        }
    }

    // Refreshes the list view after editing or inserting new item
    protected override void OnAppearing()
    {
        group = DbViewModel.GetGroupList().Result.Where(d => d.GroupId == group.GroupId).FirstOrDefault();
        this.BindingContext = viewModel = new GroupViewModel(group);
    }
}