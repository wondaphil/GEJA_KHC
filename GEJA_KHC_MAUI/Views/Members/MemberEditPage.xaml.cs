using GEJA_KHC_MAUI.ViewModels;
using GEJA_KHC_MAUI.Models;
using CommunityToolkit.Maui.Views;

namespace GEJA_KHC_MAUI.Views;

public partial class MemberEditPage : ContentPage
{
    Member member = new Member();
    string memberId;
    //MemberViewModel viewModel;

    //public MemberEditPage(Member _member)
    public MemberEditPage()
    {
        InitializeComponent();
        LoadGroupList();
        //PreLoadAllDDL(new Member() { MemberId = "005786", GroupId = 1 });
    }

    //public MemberEditPage(Member _member)
    public MemberEditPage(string _memberId)
    {
        InitializeComponent();
        //LoadGroupList();
        PreLoadAllDDL(new Member() { MemberId = _memberId, GroupId = 1 });

        memberId = _memberId;
        if (memberId == null)
            return;
        
        member = DbViewModel.GetMember(memberId).Result;

        if (member == null)
            return;

        try
        {
            PreLoadAllDDL(member);
        }
        catch (Exception err)
        {
            DisplayAlert("ስህተት ተፈጥሯል", err.Message, "Ok");
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
            await DisplayAlert("ስህተት ተፈጥሯል", err.Message, "Ok");
        }

        ddlGroup.ItemsSource = groups;
    }

    private void PreLoadAllDDL(Member _member)
    {
        if (_member != null)
        {
            Group group = DbViewModel.GetGroup((int) _member.GroupId).Result;
            
            List<Group> groupList = DbViewModel.GetGroupList().Result.OrderBy(s => s.GroupId).ToList();
            List<Member> memberList = DbViewModel.GetMemberList(group.GroupId).Result.OrderBy(s => s.FullName).ToList();

            int index = -1;
            
            foreach (var grp in groupList)
            {
                if (grp.GroupId == group.GroupId)
                {
                    index = groupList.IndexOf(grp);
                    break;
                }
            }
            ddlGroup.SelectedIndex = index;

            index = -1;
            foreach (var mem in memberList)
            {
                if (mem.MemberId == _member.MemberId)
                {
                    index = memberList.IndexOf(mem);
                    break;
                }
            }
            ddlMember.SelectedIndex = index;
        }
    }

    private async void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGroup.SelectedIndex != -1)
        {
            Group group = (Group)ddlGroup.SelectedItem;
            List<Member> members = new List<Member>();
            
            try
            {
                members = DbViewModel.GetMemberList(group.GroupId).Result.OrderBy(s => s.FullName).ToList();
            }
            catch (Exception err)
            {
                await DisplayAlert("Error", err.Message, "Ok");
            }

            ddlMember.ItemsSource = members;

            grdButtons.IsVisible = false;
        }
    }

    private void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMember.SelectedIndex != -1)
        {
            member = (Member)ddlMember.SelectedItem;

            grdButtons.IsVisible = true;
        }
    }

    private async void btnMemberInfo_Clicked(object sender, EventArgs e)
    {
        // Display a spinner popup
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);

        await Navigation.PushAsync(new MemberInfoPage(member));

        popup.Close();
    }

    private async void btnMemberPhoto_Clicked(object sender, EventArgs e)
    {
        // Display a spinner popup
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);

        await Navigation.PushAsync(new PhotoPage(member));

        popup.Close();
    }

    private async void btnAddressInfo_Clicked(object sender, EventArgs e)
    {
        // Display a spinner popup
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);

        await Navigation.PushAsync(new AddressInfoPage(member));

        popup.Close();
    }

    private async void btnFamilyInfo_Clicked(object sender, EventArgs e)
    {
        // Display a spinner popup
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);

        await Navigation.PushAsync(new FamilyInfoPage(member));

        popup.Close();
    }

    private async void btnEducationAndJobInfo_Clicked(object sender, EventArgs e)
    {
        // Display a spinner popup
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);

        await Navigation.PushAsync(new EducationAndJobInfoPage(member));

        popup.Close();
    }

    private async void btnServiceInfo_Clicked(object sender, EventArgs e)
    {
        // Display a spinner popup
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);

        await Navigation.PushAsync(new MemberServicePage(member));

        popup.Close();
    }

    // Refreshes the page after editing/deleting an item
    protected override void OnAppearing()
    {
        if (member != null && member.MemberId != null)
            member = DbViewModel.GetMember(member.MemberId).Result;
        //this.BindingContext = viewModel = new MemberViewModel(member);
    }
}