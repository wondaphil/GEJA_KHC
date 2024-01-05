using CommunityToolkit.Maui.Views;
using GEJA_KHC_MAUI.Models;
using GEJA_KHC_MAUI.ViewModels;

namespace GEJA_KHC_MAUI.Views;

public partial class MemberListPage : ContentPage
{
    private LoadingViewModel viewModel;

    public MemberListPage()
    {
        InitializeComponent();
        this.BindingContext = viewModel = new LoadingViewModel("እባክዎን ትንሸ ይጠብቁ...");

        try
        {
            LoadGroupList();
            memberListView.ItemSelected += MemberListView_ItemSelected;
            memberListView.SelectedItem = null;
        }
        catch (Exception err)
        {
            DisplayAlert("ስህተት", err.Message, "እሺ");

            return;
        }
    }

    private async void LoadGroupList()
    {
        List<Group> groups = new List<Group>();

        try
        {
            groups = DbViewModel.GetGroupList().Result.ToList();
        }
        catch (Exception err)
        {
            await DisplayAlert("Error", err.Message, "Ok");
        }

        ddlGroup.ItemsSource = groups;
    }

    private async void OnGroupChange(object sender, EventArgs e)
    {
        if (ddlGroup.SelectedIndex != -1)
        {
            Group group = (Group)ddlGroup.SelectedItem;
            List<Member> members = new List<Member>();
            viewModel.LoadingInProgress = true;

            memberListView.ItemsSource = null;
            lblGridTitle.Text = "";

            try
            {
                await Task.Run(() => members = DbViewModel.GetMemberList(group.GroupId).Result.OrderBy(d => d.FullName).ToList());
            }
            catch (Exception err)
            {
                await DisplayAlert("Error", err.Message, "Ok");
            }

            lblGridTitle.Text = "የአባላት ብዛት = " + members.Count;
            memberListView.ItemsSource = members;

            viewModel.LoadingInProgress = false;
        }
    }

    private async void MemberListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        // Display a spinner popup
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);

        Member member = (Member)e.SelectedItem;
        await Navigation.PushAsync(new MemberDetailsPage(member) { Title = "የአባል ዝርዝር መረጃ" });

        popup.Close();
    }

    // Refreshes the page after editing or inserting new item
    protected async override void OnAppearing()
    {
        if (ddlGroup.SelectedIndex != -1)
        {
            Group group = (Group)ddlGroup.SelectedItem;
            List<Member> members = new List<Member>();

            memberListView.ItemsSource = null;

            try
            {
                //members = DbViewModel.GetMemberList(group.GroupId).Result.OrderBy(d => d.FullName).Select(m => m.FullName).ToList();
                await Task.Run(() => members = DbViewModel.GetMemberList(group.GroupId).Result.OrderBy(d => d.FullName).ToList());
            }
            catch (Exception err)
            {
                await DisplayAlert("Error", err.Message, "Ok");
            }

            lblGridTitle.Text = "የአባላት ብዛት = " + members.Count;
            memberListView.ItemsSource = members;
        }
    }
}