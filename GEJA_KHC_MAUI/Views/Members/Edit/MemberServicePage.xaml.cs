using GEJA_KHC_MAUI.Models;
using GEJA_KHC_MAUI.ViewModels;

namespace GEJA_KHC_MAUI.Views;

public partial class MemberServicePage : ContentPage
{
    Member member = new Member();
    MemberServiceListViewModel viewModel;

    public MemberServicePage(Member _member)
	{
		InitializeComponent();

        member = _member;

        this.BindingContext = viewModel = new MemberServiceListViewModel(member);
        listViewMemberService.SetBinding(ListView.ItemsSourceProperty, "MemberServiceList");
        
        entryMemberId.Text = member.MemberId;
        lblMemberName.Text = member.FullName + " (" + member.MemberId + ")";
    }

    private async void btnNewMemberService_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MemberServiceNewPage(member));
    }

    private async void listViewMemberService_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        MemberServiceDetailsViewModel selMemberService = (MemberServiceDetailsViewModel)e.SelectedItem;
        MemberService memberservice = DbViewModel.GetMemberServiceById(selMemberService.Id).Result;

        await Navigation.PushAsync(new MemberServiceEditPage(memberservice));
    }

    // Refreshes the list view after editing or inserting new item
    protected async override void OnAppearing()
    {
        List<MemberService> memberServices = new List<MemberService>();

        try
        {
            member = DbViewModel.GetMember(member.MemberId).Result;
            this.BindingContext = viewModel = new MemberServiceListViewModel(member);
        }
        catch (Exception err)
        {
            await DisplayAlert("ስህተት", err.Message, "እሺ");
        }
    }
}