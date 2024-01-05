using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using GEJA_KHC_MAUI.Models;
using GEJA_KHC_MAUI.ViewModels;

namespace GEJA_KHC_MAUI.Views;

public partial class MemberDetailsPage : ContentPage
{
    Member member = new Member();
    MemberViewModel viewModel;

    public MemberDetailsPage(Member _member)
	{
		InitializeComponent();

        member = _member;
        try
        {
            this.BindingContext = viewModel = new MemberViewModel(_member);
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.InnerException.Message, "Ok");
        }

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
        try
        {
            //await Shell.Current.GoToAsync("//MemberEditPage");

            string memberid = member.MemberId;
            //await Shell.Current.GoToAsync($"//MemberEditPage?_memberId={memberid}");
            //await Shell.Current.GoToAsync($"//MemberEditPage?_memberId={memberid}", new Dictionary { { "data", new MyData(...) } });
        }
        catch (Exception err)
        {
            await DisplayAlert("ስህተት", err.Message, "Ok");

            return;
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        bool action = await DisplayAlert("ጌጃ ቃ/ሕ", "'" + member.ToString() + "'ን ለመሰረዝ እርግጠኛ ነህ?", "አዎን", "አይደለሁም");

        if (action)
        {
            try
            {
                // Delete the current member
                await DbViewModel.DeleteMember(member.MemberId);
            }
            catch (Exception err)
            {
                await DisplayAlert("ስህተት", err.Message, "Ok");

                return;
            }

            await Toast.Make("የአባሉ መረጃ በሚገባ ተሰርዟል", ToastDuration.Long, 14).Show((new CancellationTokenSource()).Token);

            // Display a spinner popup
            var popup = new SpinnerPopup();
            this.ShowPopup(popup);

            // Go back to member detail page
            await Navigation.PopAsync();

            popup.Close();
        }
    }

    // Refreshes the list view after editing or inserting new item
    protected override void OnAppearing()
    {
        member = DbViewModel.GetMember(member.MemberId).Result;
        this.BindingContext = viewModel = new MemberViewModel(member);
    }
}