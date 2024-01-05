using GEJA_KHC_MAUI.Models;
using GEJA_KHC_MAUI.ViewModels;

namespace GEJA_KHC_MAUI.Views;

public partial class GroupPage : ContentPage
{
	public GroupPage()
	{
        this.Title = "ምድቦች";

        InitializeComponent();
	}

    private async void OnNewGroup(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GroupNewPage());
    }

    private async void OnGroupList(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GroupListPage());
    }

    private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {
        SearchBar search = (SearchBar)sender;
        var groups = DbViewModel.GetGroupList().Result;
        Group grp = groups.Where(d => d.GroupId.ToString().Equals(search.Text, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

        if (grp != null)
            await Navigation.PushAsync(new GroupEditPage(grp));
    }

    private void searchGroup_TextChanged(object sender, TextChangedEventArgs e)
    {
        List<string> notfound = new List<string>() { "<ምንም መረጃ አልተገኘም>" };
        if (searchGroup.Text.Length >= 0)
        {
            SearchBar search = (SearchBar)sender;
            List<Group> grpTable = new List<Group>();
            try
            {
                grpTable = DbViewModel.GetGroupList().Result.ToList();
            }
            catch (Exception err)
            {
                //ToastViewModel.FailureToast(err.Message);
                DisplayAlert("ስህተት ተፈጥሯል", err.Message, "እሺ");
            }

            var grp = (from d in grpTable
                       where d.GroupId.ToString().StartsWith(search.Text, StringComparison.OrdinalIgnoreCase)
                                || d.GroupName.StartsWith(search.Text, StringComparison.OrdinalIgnoreCase)
                       select d).ToList();

            if (grp.Count > 0)
                listviewGroupResult.ItemsSource = grp;
            else
                listviewGroupResult.ItemsSource = notfound;
            listviewGroupResult.IsVisible = true;
        }
        else
        {
            listviewGroupResult.ItemsSource = new List<string>();
            listviewGroupResult.IsVisible = true;
        }
    }

    private void searchGroup_Focused(object sender, FocusEventArgs e)
    {
        List<string> notfound = new List<string>() { "<ምንም መረጃ አልተገኘም>" };
        if (searchGroup.Text.Length >= 0)
        {
            SearchBar search = (SearchBar)sender;
            List<Group> grpTable = new List<Group>();
            try
            {
                grpTable = DbViewModel.GetGroupList().Result.ToList();
            }
            catch (Exception err)
            {
                //ToastViewModel.FailureToast(err.Message);
                DisplayAlert("ስህተት ተፈጥሯል", err.Message, "እሺ");
            }

            var grp = (from d in grpTable
                       where d.GroupId.ToString().StartsWith(search.Text, StringComparison.OrdinalIgnoreCase)
                                || d.GroupName.StartsWith(search.Text, StringComparison.OrdinalIgnoreCase)
                       select d).ToList();

            if (grp.Count > 0)
                listviewGroupResult.ItemsSource = grp;
            else
                listviewGroupResult.ItemsSource = notfound;
            listviewGroupResult.IsVisible = true;
        }
        else
        {
            listviewGroupResult.ItemsSource = new List<string>();
            listviewGroupResult.IsVisible = true;
        }
    }

    private void listviewGroupResult_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (listviewGroupResult.SelectedItem is string)
            if ((string)listviewGroupResult.SelectedItem == "<ምንም መረጃ አልተገኘም>")
                return;

        Group searchGroup = (Group)listviewGroupResult.SelectedItem;
        Group grp = new Group();

        List<Group> grpTable = new List<Group>();
        try
        {
            var groups = DbViewModel.GetGroupList().Result;
            grp = groups.Where(d => d.GroupId == searchGroup.GroupId).FirstOrDefault();
        }
        catch (Exception err)
        {
            //ToastViewModel.FailureToast(err.Message);
            DisplayAlert("ስህተት ተፈጥሯል", err.Message, "እሺ");
        }

        if (grp != null)
            Navigation.PushAsync(new GroupEditPage(grp));
    }

    private void searchGroup_Unfocused(object sender, FocusEventArgs e)
    {
        listviewGroupResult.IsVisible = false;
    }
}