using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using GEJA_KHC_MAUI.Models;
using GEJA_KHC_MAUI.ViewModels;

namespace GEJA_KHC_MAUI.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MemberNewPage : ContentPage
{
    MemberViewModel viewModel;

    public MemberNewPage()
	{
		InitializeComponent();

        try
        { 
            this.BindingContext = viewModel = new MemberViewModel(new Member());

            entryMemberId.SetBinding(Entry.TextProperty, "MemberId");
            entryFullName.SetBinding(Entry.TextProperty, "FullName");

            ddlGroup.SetBinding(Picker.ItemsSourceProperty, "GroupList");
            ddlGroup.SetBinding(Picker.SelectedIndexProperty, "GroupIndex");
        }
        catch (Exception ex)
        {
            DisplayAlert("ስህተት", ex.InnerException.Message, "እሺ");
        }
        // After typing in one entry, automatically jump to the next one

    }

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
        if (!viewModel.SaveSuccess)
        {
            await DisplayAlert("ጌጃ ቃ/ሕ", "እባክዎ ሁሉንም ሳጥኖች ያስገቡ። \nየአባሉ መረጃ ሴቭ አልተደረገም!", "እሺ");
            return;
        }

        try
        {
            // Save to database
            await Task.Run(() => DbViewModel.SetMember(viewModel.Member));
            await Toast.Make("አባሉ '" + viewModel.Member.ToString() + "' በትክክል ተመዝግቧል", ToastDuration.Long, 14).Show((new CancellationTokenSource()).Token);
        }
        catch (Exception err)
        {
            await DisplayAlert("ስህተት", err.Message, "እሺ");

            return;
        }

        // Go back to Member main page
        await Navigation.PopAsync();
    }

    private async void btnCancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnGroupChange(object sender, EventArgs e)
    {
        if (ddlGroup.SelectedIndex != -1)
        {
            Group group = new Group();
            await Task.Run(() => group = (Group)ddlGroup.SelectedItem);

            entryMemberId.Text = SuggestedMemberId(group.GroupId);
        }
    }

    public string SuggestedMemberId(int groupid)
    {
        // MemberId is in the format "01-0000" or "02-0000" or "03-0000" or "04-0000" where "01", "02", "03" and "04" are group ids
        // If there is no member in the group, suggest 0001 (e.g. 01-0001 or 04-0001"
        // Otherwise, find the member no. of the lastly added member in the group, extract the last four digits and increment it by 1

        // Get the lastly added member Id in the given group
        var memberList = DbViewModel.GetMemberList(groupid).Result.Select(s => s.MemberId).ToList();

        // If no member so far registered on the given group 
        if (memberList.Count == 0)
        {
            // suggest -0001 as member no (because it is the first member for the group)
            var grid = DbViewModel.GetGroupList().Result.Where(g => g.GroupId == groupid).FirstOrDefault().GroupId.ToString();
            string firstMemberId = "0" + grid + "-0001";
            return "[{\"MemberId\": " + "\"" + firstMemberId + "\"}]";
        }

        // The lastly added member
        string lastMemberId = memberList.Max().ToString();

        string lastDigits = lastMemberId.Split('-').Last();

        int indexOfLastDigit = lastMemberId.IndexOf(lastDigits);
        string withoutlastDigits = lastMemberId.Remove(indexOfLastDigit, lastDigits.Length);

        string suggestedMemberId;
        int i = 1;

        // If the suggested member id already exists, keep on incrementing it 
        do
        {
            suggestedMemberId = withoutlastDigits + (Int32.Parse(lastDigits) + i).ToString().PadLeft(4, '0');
            i++;
        } while (memberList.Any(br => br == suggestedMemberId)); // repeat if member id exists

        // A json string of the form [{"MemberId": "01-0455"}]
        return suggestedMemberId;
    }
}