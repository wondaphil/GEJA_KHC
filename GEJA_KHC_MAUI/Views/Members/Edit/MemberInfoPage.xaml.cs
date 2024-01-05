using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using GEJA_KHC_MAUI.Models;
using GEJA_KHC_MAUI.ViewModels;

namespace GEJA_KHC_MAUI.Views;

public partial class MemberInfoPage : ContentPage
{
    Member member = new Member();
    MemberViewModel viewModel;

    public MemberInfoPage(Member _member)
    {
        InitializeComponent();

        member = _member;
        this.BindingContext = viewModel = new MemberViewModel(_member);

        entryMemberId.SetBinding(Entry.TextProperty, "MemberId");

        ddlGroup.SetBinding(Picker.ItemsSourceProperty, "GroupList");
        ddlGroup.SetBinding(Picker.SelectedIndexProperty, "GroupIndex");

        ddlGender.SetBinding(Picker.ItemsSourceProperty, "GenderList");
        ddlGender.SetBinding(Picker.SelectedIndexProperty, "GenderIndex");

        ddlBirthDate.SetBinding(Picker.ItemsSourceProperty, "BirthDateList");
        ddlBirthDate.SetBinding(Picker.SelectedIndexProperty, "BirthDateIndex");

        ddlBirthMonth.SetBinding(Picker.ItemsSourceProperty, "BirthMonthList");
        ddlBirthMonth.SetBinding(Picker.SelectedIndexProperty, "BirthMonthIndex");

        ddlBirthYear.SetBinding(Picker.ItemsSourceProperty, "BirthYearList");
        ddlBirthYear.SetBinding(Picker.SelectedIndexProperty, "BirthYearIndex");

        ddlMembershipDate.SetBinding(Picker.ItemsSourceProperty, "MembershipDateList");
        ddlMembershipDate.SetBinding(Picker.SelectedIndexProperty, "MembershipDateIndex");

        ddlMembershipMonth.SetBinding(Picker.ItemsSourceProperty, "MembershipMonthList");
        ddlMembershipMonth.SetBinding(Picker.SelectedIndexProperty, "MembershipMonthIndex");

        ddlMembershipYear.SetBinding(Picker.ItemsSourceProperty, "MembershipYearList");
        ddlMembershipYear.SetBinding(Picker.SelectedIndexProperty, "MembershipYearIndex");

        ddlMembershipMeans.SetBinding(Picker.ItemsSourceProperty, "MembershipMeansList");
        ddlMembershipMeans.SetBinding(Picker.SelectedIndexProperty, "MembershipMeansIndex");

        Member basicinfo = DbViewModel.GetMember(member.MemberId).Result;
        entryMemberId.Text = member.MemberId;
        lblMemberName.Text = member.FullName + " (" + member.MemberId + ")";

        SetEntriesOrder();
    }

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
        if (!viewModel.SaveSuccess)
        {
            await DisplayAlert("ጌጃ ቃ/ሕ", "እባክዎ * ያለባቸውን ሳጥኖች ያስገቡ። \nየአባሉ መረጃ ሴቭ አልተደረገም!", "እሺ");
            return;
        }

        try
        {
            // Save to database
            await Task.Run(() => DbViewModel.SetMember(viewModel.Member));
            await Toast.Make("የአባል '" + member.ToString() + "' መሠረታዊ መረጃ ሴቭ ተደርጓል", ToastDuration.Long, 14).Show((new CancellationTokenSource()).Token);
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

    private void SetEntriesOrder()
    {
        //// After typing in one entry or selecting a picker, automatically jump to the next one
        //entryKMFromAddis.Completed += (object sender, EventArgs e) => { entryXCoord.Focus(); };
        //entryXCoord.Completed += (object sender, EventArgs e) => { entryYCoord.Focus(); };
        //entryYCoord.Completed += (object sender, EventArgs e) => { ddlUtmZone.Focus(); };
        //ddlUtmZone.Unfocused += (object sender, FocusEventArgs e) => { entryBridgeLength.Focus(); };
        //entryBridgeLength.Completed += (object sender, EventArgs e) => { entryBridgeWidth.Focus(); };
        //entryBridgeWidth.Completed += (object sender, EventArgs e) => { entryRiverWidth.Focus(); };
        //entryRiverWidth.Completed += (object sender, EventArgs e) => { entryPresentWaterLevel.Focus(); };
        //entryPresentWaterLevel.Completed += (object sender, EventArgs e) => { entryHighestWaterLevel.Focus(); };
        //entryHighestWaterLevel.Completed += (object sender, EventArgs e) => { entryDesignCapacity.Focus(); };
        //entryDesignCapacity.Completed += (object sender, EventArgs e) => { entryTopography.Focus(); };
        //entryTopography.Completed += (object sender, EventArgs e) => { entryAltitude.Focus(); };
        //entryAltitude.Completed += (object sender, EventArgs e) => { switchBefore1935.Focus(); };
        //switchBefore1935.Unfocused += (object sender, FocusEventArgs e) => { entryConstructionYear.Focus(); };
        //entryConstructionYear.Completed += (object sender, EventArgs e) => { entryReplacedYear.Focus(); };
        //entryReplacedYear.Completed += (object sender, EventArgs e) => { entryContractor.Focus(); };
        //entryContractor.Completed += (object sender, EventArgs e) => { entryDesigner.Focus(); };
        //entryDesigner.Completed += (object sender, EventArgs e) => { entryConstructionCost.Focus(); };
        //entryConstructionCost.Completed += (object sender, EventArgs e) => { entryAssetReplacementCost.Focus(); };
        //entryAssetReplacementCost.Completed += (object sender, EventArgs e) => { ddlRoadAlignment.Focus(); };
        //ddlRoadAlignment.Unfocused += (object sender, FocusEventArgs e) => { switchSafetySign.Focus(); };
        //switchSafetySign.Unfocused += (object sender, FocusEventArgs e) => { switchDetourPossible.Focus(); };
        //switchDetourPossible.Unfocused += (object sender, FocusEventArgs e) => { editorRemark.Focus(); };
    }
}