using CommunityToolkit.Maui.Views;
using GEJA_KHC_MAUI.Models;
using GEJA_KHC_MAUI.ViewModels;

namespace GEJA_KHC_MAUI.Views;

public partial class MemberPage : ContentPage
{
    public MemberPage()
    {
        this.Title = "ምድቦች";

        InitializeComponent();
    }

    private async void OnNewMember(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MemberNewPage());
    }

    private async void OnMemberList(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MemberListPage());
    }

    private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {
        SearchBar search = (SearchBar)sender;
        var members = DbViewModel.GetAllMembers().Result;
        Member mem = members.Where(d => d.MemberId.ToString().Equals(search.Text, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

        if (mem != null)
            await Navigation.PushAsync(new MemberDetailsPage(mem) { Title = "Member Detail" });
    }

    private void searchMember_TextChanged(object sender, TextChangedEventArgs e)
    {
        List<string> notfound = new List<string>() { "<ምንም መረጃ አልተገኘም>" };
        if (searchMember.Text.Length >= 3)
        {
            SearchBar search = (SearchBar)sender;
            List<Member> memTable = new List<Member>();
            try
            {
                memTable = DbViewModel.GetAllMembers().Result.ToList();
            }
            catch (Exception err)
            {
                //ToastViewModel.FailureToast(err.Message);
                DisplayAlert("ስህተት ተፈጥሯል", err.Message, "እሺ");
            }

            // If member's name or id contains the entered text
            var mem = (from d in memTable
                       where d.MemberId.ToString().Contains(search.Text, StringComparison.OrdinalIgnoreCase)
                                || d.FullName.Contains(search.Text, StringComparison.OrdinalIgnoreCase)
                       select d).ToList();
            
            if (mem.Count > 0)
                listviewMemberResult.ItemsSource = mem;
            else
                listviewMemberResult.ItemsSource = notfound;
            listviewMemberResult.IsVisible = true;
        }
        else
        {
            listviewMemberResult.ItemsSource = new List<string>();
            listviewMemberResult.IsVisible = true;
        }
    }

    private void searchMember_Focused(object sender, FocusEventArgs e)
    {
        List<string> notfound = new List<string>() { "<ምንም መረጃ አልተገኘም>" };
        if (searchMember.Text.Length >= 3)
        {
            SearchBar search = (SearchBar)sender;
            List<Member> memTable = new List<Member>();
            try
            {
                memTable = DbViewModel.GetAllMembers().Result.ToList();
            }
            catch (Exception err)
            {
                //ToastViewModel.FailureToast(err.Message);
                DisplayAlert("ስህተት ተፈጥሯል", err.Message, "እሺ");
            }

            var mem = (from d in memTable
                       where d.MemberId.ToString().StartsWith(search.Text, StringComparison.OrdinalIgnoreCase)
                                || d.FullName.StartsWith(search.Text, StringComparison.OrdinalIgnoreCase)
                       select d).ToList();

            if (mem.Count > 0)
                listviewMemberResult.ItemsSource = mem;
            else
                listviewMemberResult.ItemsSource = notfound;
            listviewMemberResult.IsVisible = true;
        }
        else
        {
            listviewMemberResult.ItemsSource = new List<string>();
            listviewMemberResult.IsVisible = true;
        }
    }

    private void listviewMemberResult_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (listviewMemberResult.SelectedItem is string)
            if ((string)listviewMemberResult.SelectedItem == "<ምንም መረጃ አልተገኘም>")
                return;

        Member searchMember = (Member)listviewMemberResult.SelectedItem;
        Member mem = new Member();

        List<Member> memTable = new List<Member>();
        try
        {
            var members = DbViewModel.GetAllMembers().Result;
            mem = members.Where(d => d.MemberId == searchMember.MemberId).FirstOrDefault();
        }
        catch (Exception err)
        {
            //ToastViewModel.FailureToast(err.Message);
            DisplayAlert("ስህተት ተፈጥሯል", err.Message, "እሺ");
        }

        if (mem != null)
            Navigation.PushAsync(new MemberDetailsPage(mem) { Title = "የአባል ዝርዝር መረጃ" });
    }

    private void searchMember_Unfocused(object sender, FocusEventArgs e)
    {
        listviewMemberResult.IsVisible = false;
    }
}