using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using GEJA_KHC_MAUI.Models;
using GEJA_KHC_MAUI.ViewModels;

namespace GEJA_KHC_MAUI.Views;

public partial class EducationAndJobInfoPage : ContentPage
{
    Member member = new Member();
    EducationAndJobInfoViewModel viewModel;

    public EducationAndJobInfoPage(Member _member)
	{
		InitializeComponent();

        member = _member;
        this.BindingContext = viewModel = new EducationAndJobInfoViewModel(member);

        entryMemberId.SetBinding(Entry.TextProperty, "MemberId");

        ddlEducationLevel.SetBinding(Picker.ItemsSourceProperty, "EducationLevelList");
        ddlEducationLevel.SetBinding(Picker.SelectedIndexProperty, "EducationLevelIndex");

        ddlFieldOfStudy.SetBinding(Picker.ItemsSourceProperty, "FieldOfStudyList");
        ddlFieldOfStudy.SetBinding(Picker.SelectedIndexProperty, "FieldOfStudyIndex");

        ddlJob.SetBinding(Picker.ItemsSourceProperty, "JobList");
        ddlJob.SetBinding(Picker.SelectedIndexProperty, "JobIndex");

        ddlJobType.SetBinding(Picker.ItemsSourceProperty, "JobTypeList");
        ddlJobType.SetBinding(Picker.SelectedIndexProperty, "JobTypeIndex");

        EducationAndJobInfo educjobinfo = DbViewModel.GetEducationAndJobInfo(member.MemberId).Result;
        entryMemberId.Text = educjobinfo.MemberId;
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
            await Task.Run(() => DbViewModel.SetEducationAndJobInfo(viewModel.EducationAndJobInfo));
            await Toast.Make("የአባል '" + member.ToString() + "' የሥራና ትምህርት መረጃ ሴቭ ተደርጓል", ToastDuration.Long, 14).Show((new CancellationTokenSource()).Token);
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
    }
}