using GEJA_KHC_MAUI.Models;
using GEJA_KHC_MAUI.ViewModels;

namespace GEJA_KHC_MAUI.Views;

public partial class GroupListPage : ContentPage
{
    public GroupListPage()
    {
        InitializeComponent();

        try
        {
            var groups = DbViewModel.GetGroupList();
            lblGridTitle.Text = "የምድቦች ብዛት " + groups.Result.Count();
            groupListView.ItemsSource = groups.Result;

            groupListView.ItemSelected += GroupListView_ItemSelected;
            groupListView.SelectedItem = null;
        }
        catch (Exception err)
        {
            DisplayAlert("ስህተት", err.Message, "እሺ");

            return;
        }
    }

    private async void GroupListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        Group group = (Group)e.SelectedItem;
        await Navigation.PushAsync(new GroupEditPage(group));
    }

    // Refreshes the list view after editing or inserting new item
    protected override void OnAppearing()
    {
        try
        {
            var groups = DbViewModel.GetGroupList().Result.OrderBy(d => d.GroupId).ToList();
            lblGridTitle.Text = "የምድቦች ብዛት " + groups.Count;
            groupListView.ItemsSource = groups;
        }
        catch (Exception err)
        {
            DisplayAlert("Error", err.Message, "Ok");

            return;
        }
    }
}