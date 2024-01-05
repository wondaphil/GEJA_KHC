using GEJA_KHC_MAUI.Models;
using GEJA_KHC_MAUI.ViewModels;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;

namespace GEJA_KHC_MAUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportsPage : ContentPage
    {
        ReportGraphViewModel reportvm;
        double headerFontSize;
        double bodyFontSize;
        private LoadingViewModel viewModel;

        public ReportsPage()
        {
            InitializeComponent();

            this.BindingContext = viewModel = new LoadingViewModel("የሪፖርት መረጃ እየወረደ ነው...");

            // Font size for Phone, Tablet and Desktop
            headerFontSize = DeviceInfo.Current.Idiom == DeviceIdiom.Phone ? 18 :
                                DeviceInfo.Current.Idiom == DeviceIdiom.Tablet ? 22 : 20;
            
            bodyFontSize = DeviceInfo.Current.Idiom == DeviceIdiom.Phone ? 18 :
                                DeviceInfo.Current.Idiom == DeviceIdiom.Tablet ? 22 : 20;
            LoadAll();
        }

        private async void LoadAll()
        {
            ClearReportListView();
            ddlReportType.IsVisible = false;
            viewModel.LoadingInProgress = true;
            
            await Task.Run(() => reportvm = new ReportGraphViewModel());
            //reportvm = new ReportGraphViewModel();

            ddlReportType.Title = "የሪፖርት ዓይነት ምረጥ";
            //ddlReportType.SelectedIndexChanged += OnReportTypeChange;
            ddlReportType.ItemsSource = new List<string>()
                {
                    "የአባላት ብዛት በምድብ", // 0
                    "የአባላት ብዛት በፆታ", // 1
                    "የአባላት ብዛት በትምህርት ደረጃ", // 2
                    "የአባላት ብዛት በጋብቻ ሁኔታ", // 3
                    "የአባላት ብዛት በአባልነት መንገድ", // 4
                    "የአባላት ብዛት በአባልነት ዘመን", // 5
                    "የአባላት ብዛት በክፍለ ከተማ", // 6
                };
            List<int> inspectionYearList = new List<int>();

            viewModel.LoadingInProgress = false;
            ddlReportType.IsVisible = true;
        }

        private void OnReportTypeChange(object sender, EventArgs e)
        {
            ClearReportListView();
            
            switch (ddlReportType.SelectedIndex)
            {
                case -1:
                    break;

                case 0:
                    ShowNoOfMembersByGroupReport();
                    break;

                case 1:
                    ShowNoOfMembersByGenderReport();
                    break;

                case 2:
                    ShowNoOfMembersByEducationLevelReport();
                    break;

                case 3:
                    ShowNoOfMembersByMaritalStatusReport();
                    break;

                case 4:
                    ShowNoOfMembersByMembershipMeansReport();
                    break;

                case 5:
                    ShowNoOfMembersByMembershipYearReport();
                    break;

                case 6:
                    ShowNoOfMembersBySubcityReport();
                    break;

                default:
                    break;
            }
        }

        public void ClearReportListView()
        {
            listviewReport.ItemsSource = null;
            listviewReport.Header = null;
            listviewReport.ItemTemplate = null;
            listviewReport.Footer = null;
            labelTitle.Text = "";
        }

        private void OnRefreshClicked(object sender, EventArgs e)
        {
            OnReportTypeChange(ddlReportType, e);
        }

        private void OnReloadReportDataClicked(object sender, EventArgs e)
        {
            LoadAll();
            //OnReportTypeChange(ddlReportType, e);
        }

        public void ShowNoOfMembersByGroupReport()
        {
            labelTitle.Text = "የአባላት ብዛት በምድብ";

            ColumnDefinition col1 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };
            ColumnDefinition col2 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };

            RowDefinition row = new RowDefinition { Height = GridLength.Auto };

            var headerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

            var lblHeader1 = new Label { Text = "ምድብ", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };
            var lblHeader2 = new Label { Text = "የአባላት ብዛት", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 1, 0);
            
            headerGrid.SetRow(lblHeader1, 0);
            headerGrid.SetColumn(lblHeader1, 0);
            headerGrid.SetRow(lblHeader2, 0);
            headerGrid.SetColumn(lblHeader2, 1);
            
            headerGrid.Children.Add(lblHeader1);
            headerGrid.Children.Add(lblHeader2);
            
            var rptDataTemplate = new DataTemplate(() =>
            {
                var bodyGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

                var lblData1 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };
                var lblData2 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };
                
                lblData1.SetBinding(Label.TextProperty, "Group");
                lblData2.SetBinding(Label.TextProperty, "MemberCount");
                
                bodyGrid.SetRow(lblData1, 0);
                bodyGrid.SetColumn(lblData1, 0);
                bodyGrid.SetRow(lblData2, 0);
                bodyGrid.SetColumn(lblData2, 1);
                
                bodyGrid.Children.Add(lblData1);
                bodyGrid.Children.Add(lblData2);
                
                return new ViewCell { View = bodyGrid };
            });

            int membercount = 0;
            var rptList = reportvm.GetMemberCountByGroup(ref membercount);

            //rptList.Add(new MemberCountViewModel() { Group = "ጠቅላላ", MemberCount = membercount });

            

            //var rptFooterTemplate = new DataTemplate(() =>
            //{
                var footerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 }, RowDefinitions = new RowDefinitionCollection() { row } };

                var lblFooter1 = new Label { Text = "ጠቅላላ", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 0, 2);
                var lblFooter2 = new Label { Text = membercount.ToString(), FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 1, 2);

                footerGrid.SetRow(lblFooter1, 0);
                footerGrid.SetColumn(lblFooter1, 0);
                footerGrid.SetRow(lblFooter2, 0);
                footerGrid.SetColumn(lblFooter2, 1);

                footerGrid.Children.Add(lblFooter1);
                footerGrid.Children.Add(lblFooter2);

            //    return footerGrid;
            //});

            
            listviewReport.ItemsSource = rptList;
            listviewReport.Header = headerGrid;
            listviewReport.ItemTemplate = rptDataTemplate;
            //listviewReport.FooterTemplate = rptFooterTemplate;
            listviewReport.Footer = footerGrid;
        }

        public void ShowNoOfMembersByGenderReport()
        {
            labelTitle.Text = "የአባላት ብዛት በፆታ";

            ColumnDefinition col1 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };
            ColumnDefinition col2 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };
            
            var headerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

            var lblHeader1 = new Label { Text = "ፆታ", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };
            var lblHeader2 = new Label { Text = "የአባላት ብዛት", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 1, 0);
            
            headerGrid.SetRow(lblHeader1, 0);
            headerGrid.SetColumn(lblHeader1, 0);
            headerGrid.SetRow(lblHeader2, 0);
            headerGrid.SetColumn(lblHeader2, 1);
            
            headerGrid.Children.Add(lblHeader1);
            headerGrid.Children.Add(lblHeader2);
            
            var rptDataTemplate = new DataTemplate(() =>
            {
                var bodyGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

                var lblData1 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };
                var lblData2 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };
                
                lblData1.SetBinding(Label.TextProperty, "Gender");
                lblData2.SetBinding(Label.TextProperty, "MemberCount");
                
                bodyGrid.SetRow(lblData1, 0);
                bodyGrid.SetColumn(lblData1, 0);
                bodyGrid.SetRow(lblData2, 0);
                bodyGrid.SetColumn(lblData2, 1);
                
                bodyGrid.Children.Add(lblData1);
                bodyGrid.Children.Add(lblData2);
                
                return new ViewCell { View = bodyGrid };
            });

            int membercount = 0;
            var rptList = reportvm.GetMemberCountByGender(ref membercount);

            var footerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

            var lblFooter1 = new Label { Text = "ጠቅላላ", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 0, 2);
            var lblFooter2 = new Label { Text = membercount.ToString(), FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 1, 2);
            
            footerGrid.SetRow(lblFooter1, 2);
            footerGrid.SetColumn(lblFooter1, 0);
            footerGrid.SetRow(lblFooter2, 2);
            footerGrid.SetColumn(lblFooter2, 1);
            
            footerGrid.Children.Add(lblFooter1);
            footerGrid.Children.Add(lblFooter2);
            
            listviewReport.ItemsSource = rptList;
            listviewReport.Header = headerGrid;
            listviewReport.ItemTemplate = rptDataTemplate;
            listviewReport.Footer = footerGrid;
        }

        public void ShowNoOfMembersByEducationLevelReport()
        {
            labelTitle.Text = "የአባላት ብዛት በትምህርት ደረጃ";

            ColumnDefinition col1 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };
            ColumnDefinition col2 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };

            var headerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

            var lblHeader1 = new Label { Text = "የትምህርት ደረጃ", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };
            var lblHeader2 = new Label { Text = "የአባላት ብዛት", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 1, 0);

            headerGrid.SetRow(lblHeader1, 0);
            headerGrid.SetColumn(lblHeader1, 0);
            headerGrid.SetRow(lblHeader2, 0);
            headerGrid.SetColumn(lblHeader2, 1);

            headerGrid.Children.Add(lblHeader1);
            headerGrid.Children.Add(lblHeader2);

            var rptDataTemplate = new DataTemplate(() =>
            {
                var bodyGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

                var lblData1 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };
                var lblData2 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };

                lblData1.SetBinding(Label.TextProperty, "EducationLevel");
                lblData2.SetBinding(Label.TextProperty, "MemberCount");

                bodyGrid.SetRow(lblData1, 0);
                bodyGrid.SetColumn(lblData1, 0);
                bodyGrid.SetRow(lblData2, 0);
                bodyGrid.SetColumn(lblData2, 1);

                bodyGrid.Children.Add(lblData1);
                bodyGrid.Children.Add(lblData2);

                return new ViewCell { View = bodyGrid };
            });

            int membercount = 0;
            var rptList = reportvm.GetMemberCountByEducationLevel(ref membercount);

            var footerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

            var lblFooter1 = new Label { Text = "ጠቅላላ", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 0, 2);
            var lblFooter2 = new Label { Text = membercount.ToString(), FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 1, 2);

            footerGrid.SetRow(lblFooter1, 2);
            footerGrid.SetColumn(lblFooter1, 0);
            footerGrid.SetRow(lblFooter2, 2);
            footerGrid.SetColumn(lblFooter2, 1);

            footerGrid.Children.Add(lblFooter1);
            footerGrid.Children.Add(lblFooter2);

            listviewReport.ItemsSource = rptList;
            listviewReport.Header = headerGrid;
            listviewReport.ItemTemplate = rptDataTemplate;
            listviewReport.Footer = footerGrid;
        }

        public void ShowNoOfMembersByMaritalStatusReport()
        {
            labelTitle.Text = "የአባላት ብዛት በጋብቻ ሁኔታ";

            ColumnDefinition col1 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };
            ColumnDefinition col2 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };

            var headerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

            var lblHeader1 = new Label { Text = "የጋብቻ ሁኔታ", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };
            var lblHeader2 = new Label { Text = "የአባላት ብዛት", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 1, 0);

            headerGrid.SetRow(lblHeader1, 0);
            headerGrid.SetColumn(lblHeader1, 0);
            headerGrid.SetRow(lblHeader2, 0);
            headerGrid.SetColumn(lblHeader2, 1);

            headerGrid.Children.Add(lblHeader1);
            headerGrid.Children.Add(lblHeader2);

            var rptDataTemplate = new DataTemplate(() =>
            {
                var bodyGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

                var lblData1 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };
                var lblData2 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };

                lblData1.SetBinding(Label.TextProperty, "MaritalStatus");
                lblData2.SetBinding(Label.TextProperty, "MemberCount");

                bodyGrid.SetRow(lblData1, 0);
                bodyGrid.SetColumn(lblData1, 0);
                bodyGrid.SetRow(lblData2, 0);
                bodyGrid.SetColumn(lblData2, 1);

                bodyGrid.Children.Add(lblData1);
                bodyGrid.Children.Add(lblData2);

                return new ViewCell { View = bodyGrid };
            });

            int membercount = 0;
            var rptList = reportvm.GetMemberCountByMaritalStatus(ref membercount);

            var footerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

            var lblFooter1 = new Label { Text = "ጠቅላላ", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 0, 2);
            var lblFooter2 = new Label { Text = membercount.ToString(), FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 1, 2);

            footerGrid.SetRow(lblFooter1, 2);
            footerGrid.SetColumn(lblFooter1, 0);
            footerGrid.SetRow(lblFooter2, 2);
            footerGrid.SetColumn(lblFooter2, 1);

            footerGrid.Children.Add(lblFooter1);
            footerGrid.Children.Add(lblFooter2);

            listviewReport.ItemsSource = rptList;
            listviewReport.Header = headerGrid;
            listviewReport.ItemTemplate = rptDataTemplate;
            listviewReport.Footer = footerGrid;
        }

        public void ShowNoOfMembersByMembershipMeansReport()
        {
            labelTitle.Text = "የአባላት ብዛት በአባልነት መንገድ";

            ColumnDefinition col1 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };
            ColumnDefinition col2 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };

            var headerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

            var lblHeader1 = new Label { Text = "የአባልነት መንገድ", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };
            var lblHeader2 = new Label { Text = "የአባላት ብዛት", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 1, 0);

            headerGrid.SetRow(lblHeader1, 0);
            headerGrid.SetColumn(lblHeader1, 0);
            headerGrid.SetRow(lblHeader2, 0);
            headerGrid.SetColumn(lblHeader2, 1);

            headerGrid.Children.Add(lblHeader1);
            headerGrid.Children.Add(lblHeader2);

            var rptDataTemplate = new DataTemplate(() =>
            {
                var bodyGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

                var lblData1 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };
                var lblData2 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };

                lblData1.SetBinding(Label.TextProperty, "MembershipMeans");
                lblData2.SetBinding(Label.TextProperty, "MemberCount");

                bodyGrid.SetRow(lblData1, 0);
                bodyGrid.SetColumn(lblData1, 0);
                bodyGrid.SetRow(lblData2, 0);
                bodyGrid.SetColumn(lblData2, 1);

                bodyGrid.Children.Add(lblData1);
                bodyGrid.Children.Add(lblData2);

                return new ViewCell { View = bodyGrid };
            });

            int membercount = 0;
            var rptList = reportvm.GetMemberCountByMembershipMeans(ref membercount);

            var footerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

            var lblFooter1 = new Label { Text = "ጠቅላላ", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 0, 2);
            var lblFooter2 = new Label { Text = membercount.ToString(), FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 1, 2);

            footerGrid.SetRow(lblFooter1, 2);
            footerGrid.SetColumn(lblFooter1, 0);
            footerGrid.SetRow(lblFooter2, 2);
            footerGrid.SetColumn(lblFooter2, 1);

            footerGrid.Children.Add(lblFooter1);
            footerGrid.Children.Add(lblFooter2);

            listviewReport.ItemsSource = rptList;
            listviewReport.Header = headerGrid;
            listviewReport.ItemTemplate = rptDataTemplate;
            listviewReport.Footer = footerGrid;
        }

        public void ShowNoOfMembersByMembershipYearReport()
        {
            labelTitle.Text = "የአባላት ብዛት በአባልነት ዘመን";

            ColumnDefinition col1 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };
            ColumnDefinition col2 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };

            var headerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

            var lblHeader1 = new Label { Text = "የአባልነት ዘመን", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };
            var lblHeader2 = new Label { Text = "የአባላት ብዛት", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 1, 0);

            headerGrid.SetRow(lblHeader1, 0);
            headerGrid.SetColumn(lblHeader1, 0);
            headerGrid.SetRow(lblHeader2, 0);
            headerGrid.SetColumn(lblHeader2, 1);

            headerGrid.Children.Add(lblHeader1);
            headerGrid.Children.Add(lblHeader2);

            var rptDataTemplate = new DataTemplate(() =>
            {
                var bodyGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

                var lblData1 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };
                var lblData2 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };

                lblData1.SetBinding(Label.TextProperty, "MembershipYear");
                lblData2.SetBinding(Label.TextProperty, "MemberCount");

                bodyGrid.SetRow(lblData1, 0);
                bodyGrid.SetColumn(lblData1, 0);
                bodyGrid.SetRow(lblData2, 0);
                bodyGrid.SetColumn(lblData2, 1);

                bodyGrid.Children.Add(lblData1);
                bodyGrid.Children.Add(lblData2);

                return new ViewCell { View = bodyGrid };
            });

            int membercount = 0;
            var rptList = reportvm.GetMemberCountByMembershipYear(ref membercount);

            var footerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

            var lblFooter1 = new Label { Text = "ጠቅላላ", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 0, 2);
            var lblFooter2 = new Label { Text = membercount.ToString(), FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 1, 2);

            footerGrid.SetRow(lblFooter1, 2);
            footerGrid.SetColumn(lblFooter1, 0);
            footerGrid.SetRow(lblFooter2, 2);
            footerGrid.SetColumn(lblFooter2, 1);

            footerGrid.Children.Add(lblFooter1);
            footerGrid.Children.Add(lblFooter2);

            listviewReport.ItemsSource = rptList;
            listviewReport.Header = headerGrid;
            listviewReport.ItemTemplate = rptDataTemplate;
            listviewReport.Footer = footerGrid;
        }

        public void ShowNoOfMembersBySubcityReport()
        {
            labelTitle.Text = "የአባላት ብዛት በክፍለ ከተማ";

            ColumnDefinition col1 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };
            ColumnDefinition col2 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };

            var headerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

            var lblHeader1 = new Label { Text = "ክፍለ ከተማ", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };
            var lblHeader2 = new Label { Text = "የአባላት ብዛት", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 1, 0);

            headerGrid.SetRow(lblHeader1, 0);
            headerGrid.SetColumn(lblHeader1, 0);
            headerGrid.SetRow(lblHeader2, 0);
            headerGrid.SetColumn(lblHeader2, 1);

            headerGrid.Children.Add(lblHeader1);
            headerGrid.Children.Add(lblHeader2);

            var rptDataTemplate = new DataTemplate(() =>
            {
                var bodyGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

                var lblData1 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };
                var lblData2 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };

                lblData1.SetBinding(Label.TextProperty, "Subcity");
                lblData2.SetBinding(Label.TextProperty, "MemberCount");

                bodyGrid.SetRow(lblData1, 0);
                bodyGrid.SetColumn(lblData1, 0);
                bodyGrid.SetRow(lblData2, 0);
                bodyGrid.SetColumn(lblData2, 1);

                bodyGrid.Children.Add(lblData1);
                bodyGrid.Children.Add(lblData2);

                return new ViewCell { View = bodyGrid };
            });

            int membercount = 0;
            var rptList = reportvm.GetMemberCountBySubcity(ref membercount);

            var footerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

            var lblFooter1 = new Label { Text = "ጠቅላላ", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 0, 2);
            var lblFooter2 = new Label { Text = membercount.ToString(), FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center }; //, 1, 2);

            footerGrid.SetRow(lblFooter1, 2);
            footerGrid.SetColumn(lblFooter1, 0);
            footerGrid.SetRow(lblFooter2, 2);
            footerGrid.SetColumn(lblFooter2, 1);

            footerGrid.Children.Add(lblFooter1);
            footerGrid.Children.Add(lblFooter2);

            listviewReport.ItemsSource = rptList;
            listviewReport.Header = headerGrid;
            listviewReport.ItemTemplate = rptDataTemplate;
            listviewReport.Footer = footerGrid;
        }

        //public void ShowNoOfMembersByGenderReport()
        //{
        //    labelTitle.Text = "የአባላት ብዛት በBy Construction Year";

        //    ColumnDefinition col1 = new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) };
        //    ColumnDefinition col2 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };
        //    ColumnDefinition col3 = new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) };

        //    var headerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2, col3 } };

        //    var lblHeader1 = new Label { Text = "Id", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15) };
        //    var lblHeader2 = new Label { Text = "Constr. Year Range", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };
        //    var lblHeader3 = new Label { Text = "የአባላት ብዛት", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };

        //    headerGrid.SetRow(lblHeader1, 0);
        //    headerGrid.SetColumn(lblHeader1, 0);
        //    headerGrid.SetRow(lblHeader2, 0);
        //    headerGrid.SetColumn(lblHeader2, 1);
        //    headerGrid.SetRow(lblHeader3, 0);
        //    headerGrid.SetColumn(lblHeader3, 2);

        //    headerGrid.Children.Add(lblHeader1);
        //    headerGrid.Children.Add(lblHeader2);
        //    headerGrid.Children.Add(lblHeader3);

        //    var rptDataTemplate = new DataTemplate(() =>
        //    {
        //        var bodyGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2, col3 } };

        //        var lblData1 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };
        //        var lblData2 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };
        //        var lblData3 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };

        //        lblData1.SetBinding(Label.TextProperty, "Id");
        //        lblData2.SetBinding(Label.TextProperty, "ConstructionYear");
        //        lblData3.SetBinding(Label.TextProperty, "MemberCount");

        //        bodyGrid.SetRow(lblData1, 0);
        //        bodyGrid.SetColumn(lblData1, 0);
        //        bodyGrid.SetRow(lblData2, 0);
        //        bodyGrid.SetColumn(lblData2, 1);
        //        bodyGrid.SetRow(lblData3, 0);
        //        bodyGrid.SetColumn(lblData3, 2);

        //        bodyGrid.Children.Add(lblData1);
        //        bodyGrid.Children.Add(lblData2);
        //        bodyGrid.Children.Add(lblData3);

        //        return new ViewCell { View = bodyGrid };
        //    });

        //    int membercount = 0;
        //    var rptList = reportvm.GetMemberCountByConstructionYear(ref membercount);

        //    var footerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2, col3 } };

        //    var lblFooter1 = new Label { Text = "TOTAL", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };
        //    var lblFooter2 = new Label { Text = membercount.ToString(), FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };

        //    footerGrid.SetRow(lblFooter1, 2);
        //    footerGrid.SetColumn(lblFooter1, 1);
        //    footerGrid.SetRow(lblFooter2, 2);
        //    footerGrid.SetColumn(lblFooter2, 2);

        //    footerGrid.Children.Add(lblFooter1);
        //    footerGrid.Children.Add(lblFooter2);

        //    listviewReport.ItemsSource = rptList;
        //    listviewReport.Header = headerGrid;
        //    listviewReport.ItemTemplate = rptDataTemplate;
        //    listviewReport.Footer = footerGrid;
        //}

        //public void ShowNoOfMembersByEducationLevelReport()
        //{
        //    labelTitle.Text = "የአባላት ብዛት በBy MemberLength";

        //    ColumnDefinition col1 = new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) };
        //    ColumnDefinition col2 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };
        //    ColumnDefinition col3 = new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) };

        //    var headerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2, col3 } };

        //    var lblHeader1 = new Label { Text = "Id", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15) };
        //    var lblHeader2 = new Label { Text = "Member Length (m)", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };
        //    var lblHeader3 = new Label { Text = "የአባላት ብዛት", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };

        //    headerGrid.SetRow(lblHeader1, 0);
        //    headerGrid.SetColumn(lblHeader1, 0);
        //    headerGrid.SetRow(lblHeader2, 0);
        //    headerGrid.SetColumn(lblHeader2, 1);
        //    headerGrid.SetRow(lblHeader3, 0);
        //    headerGrid.SetColumn(lblHeader3, 2);

        //    headerGrid.Children.Add(lblHeader1);
        //    headerGrid.Children.Add(lblHeader2);
        //    headerGrid.Children.Add(lblHeader3);

        //    var rptDataTemplate = new DataTemplate(() =>
        //    {
        //        var bodyGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2, col3 } };

        //        var lblData1 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };
        //        var lblData2 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };
        //        var lblData3 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };

        //        lblData1.SetBinding(Label.TextProperty, "Id");
        //        lblData2.SetBinding(Label.TextProperty, "MemberLength");
        //        lblData3.SetBinding(Label.TextProperty, "MemberCount");

        //        bodyGrid.SetRow(lblData1, 0);
        //        bodyGrid.SetColumn(lblData1, 0);
        //        bodyGrid.SetRow(lblData2, 0);
        //        bodyGrid.SetColumn(lblData2, 1);
        //        bodyGrid.SetRow(lblData3, 0);
        //        bodyGrid.SetColumn(lblData3, 2);

        //        bodyGrid.Children.Add(lblData1);
        //        bodyGrid.Children.Add(lblData2);
        //        bodyGrid.Children.Add(lblData3);

        //        return new ViewCell { View = bodyGrid };
        //    });

        //    int membercount = 0;
        //    var rptList = reportvm.GetMemberCountByMemberLength(ref membercount);

        //    var footerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2, col3 } };

        //    var lblFooter1 = new Label { Text = "TOTAL", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };
        //    var lblFooter2 = new Label { Text = membercount.ToString(), FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };

        //    footerGrid.SetRow(lblFooter1, 2);
        //    footerGrid.SetColumn(lblFooter1, 1);
        //    footerGrid.SetRow(lblFooter2, 2);
        //    footerGrid.SetColumn(lblFooter2, 2);

        //    footerGrid.Children.Add(lblFooter1);
        //    footerGrid.Children.Add(lblFooter2);

        //    listviewReport.ItemsSource = rptList;
        //    listviewReport.Header = headerGrid;
        //    listviewReport.ItemTemplate = rptDataTemplate;
        //    listviewReport.Footer = footerGrid;
        //}

        //public void ShowNoOfMembersByMaritalStatusReport()
        //{
        //    labelTitle.Text = "የአባላት ብዛት በBy Member Type";

        //    ColumnDefinition col1 = new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) };
        //    ColumnDefinition col2 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };
        //    ColumnDefinition col3 = new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) };

        //    var headerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2, col3 } };

        //    var lblHeader1 = new Label { Text = "Id", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15) };
        //    var lblHeader2 = new Label { Text = "Member Type", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15) };
        //    var lblHeader3 = new Label { Text = "የአባላት ብዛት", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15) };

        //    headerGrid.SetRow(lblHeader1, 0);
        //    headerGrid.SetColumn(lblHeader1, 0);
        //    headerGrid.SetRow(lblHeader2, 0);
        //    headerGrid.SetColumn(lblHeader2, 1);
        //    headerGrid.SetRow(lblHeader3, 0);
        //    headerGrid.SetColumn(lblHeader3, 2);

        //    headerGrid.Children.Add(lblHeader1);
        //    headerGrid.Children.Add(lblHeader2);
        //    headerGrid.Children.Add(lblHeader3);

        //    var rptDataTemplate = new DataTemplate(() =>
        //    {
        //        var bodyGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2, col3 } };

        //        var lblData1 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };
        //        var lblData2 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };
        //        var lblData3 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };

        //        lblData1.SetBinding(Label.TextProperty, "TypeId");
        //        lblData2.SetBinding(Label.TextProperty, "TypeName");
        //        lblData3.SetBinding(Label.TextProperty, "Count");

        //        bodyGrid.SetRow(lblData1, 0);
        //        bodyGrid.SetColumn(lblData1, 0);
        //        bodyGrid.SetRow(lblData2, 0);
        //        bodyGrid.SetColumn(lblData2, 1);
        //        bodyGrid.SetRow(lblData3, 0);
        //        bodyGrid.SetColumn(lblData3, 2);

        //        bodyGrid.Children.Add(lblData1);
        //        bodyGrid.Children.Add(lblData2);
        //        bodyGrid.Children.Add(lblData3);

        //        return new ViewCell { View = bodyGrid };
        //    });

        //    int membercount = 0;
        //    var rptList = reportvm.GetMemberCountByMemberType(ref membercount);

        //    var footerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2, col3 } };

        //    var lblFooter1 = new Label { Text = "TOTAL", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };
        //    var lblFooter2 = new Label { Text = membercount.ToString(), FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };

        //    footerGrid.SetRow(lblFooter1, 2);
        //    footerGrid.SetColumn(lblFooter1, 1);
        //    footerGrid.SetRow(lblFooter2, 2);
        //    footerGrid.SetColumn(lblFooter2, 2);

        //    footerGrid.Children.Add(lblFooter1);
        //    footerGrid.Children.Add(lblFooter2);

        //    listviewReport.ItemsSource = rptList;
        //    listviewReport.Header = headerGrid;
        //    listviewReport.ItemTemplate = rptDataTemplate;
        //    listviewReport.Footer = footerGrid;
        //}

        //public void ShowNoOfMembersByMembershipMeansReport()
        //{
        //    labelTitle.Text = "No. of Culverts By Culvert Type";

        //    ColumnDefinition col1 = new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) };
        //    ColumnDefinition col2 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };
        //    ColumnDefinition col3 = new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) };

        //    var headerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2, col3 } };

        //    var lblHeader1 = new Label { Text = "Id", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15) };
        //    var lblHeader2 = new Label { Text = "Culvert Type", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15) };
        //    var lblHeader3 = new Label { Text = "No. of Culverts", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15) };

        //    headerGrid.SetRow(lblHeader1, 0);
        //    headerGrid.SetColumn(lblHeader1, 0);
        //    headerGrid.SetRow(lblHeader2, 0);
        //    headerGrid.SetColumn(lblHeader2, 1);
        //    headerGrid.SetRow(lblHeader3, 0);
        //    headerGrid.SetColumn(lblHeader3, 2);

        //    headerGrid.Children.Add(lblHeader1);
        //    headerGrid.Children.Add(lblHeader2);
        //    headerGrid.Children.Add(lblHeader3);

        //    var rptDataTemplate = new DataTemplate(() =>
        //    {
        //        var bodyGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2, col3 } };

        //        var lblData1 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };
        //        var lblData2 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };
        //        var lblData3 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };

        //        lblData1.SetBinding(Label.TextProperty, "TypeId");
        //        lblData2.SetBinding(Label.TextProperty, "TypeName");
        //        lblData3.SetBinding(Label.TextProperty, "Count");

        //        bodyGrid.SetRow(lblData1, 0);
        //        bodyGrid.SetColumn(lblData1, 0);
        //        bodyGrid.SetRow(lblData2, 0);
        //        bodyGrid.SetColumn(lblData2, 1);
        //        bodyGrid.SetRow(lblData3, 0);
        //        bodyGrid.SetColumn(lblData3, 2);

        //        bodyGrid.Children.Add(lblData1);
        //        bodyGrid.Children.Add(lblData2);
        //        bodyGrid.Children.Add(lblData3);

        //        return new ViewCell { View = bodyGrid };
        //    });

        //    int culvertcount = 0;
        //    var rptList = reportvm.GetCulvertCountByMemberType(ref culvertcount);

        //    var footerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2, col3 } };

        //    var lblFooter1 = new Label { Text = "TOTAL", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15) };
        //    var lblFooter2 = new Label { Text = culvertcount.ToString(), FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15) };

        //    footerGrid.SetRow(lblFooter1, 2);
        //    footerGrid.SetColumn(lblFooter1, 1);
        //    footerGrid.SetRow(lblFooter2, 2);
        //    footerGrid.SetColumn(lblFooter2, 2);

        //    footerGrid.Children.Add(lblFooter1);
        //    footerGrid.Children.Add(lblFooter2);

        //    listviewReport.ItemsSource = rptList;
        //    listviewReport.Header = headerGrid;
        //    listviewReport.ItemTemplate = rptDataTemplate;
        //    listviewReport.Footer = footerGrid;
        //}

        //public void ShowNoOfMembersByMembershipYearReport()
        //{
        //    labelTitle.Text = "No. of Inspected Members By Inspection Year";

        //    ColumnDefinition col1 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };
        //    ColumnDefinition col2 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };

        //    var headerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

        //    var lblHeader1 = new Label { Text = "Inspection Year", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };
        //    var lblHeader2 = new Label { Text = "የአባላት ብዛት", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };

        //    headerGrid.SetRow(lblHeader1, 0);
        //    headerGrid.SetColumn(lblHeader1, 0);
        //    headerGrid.SetRow(lblHeader2, 0);
        //    headerGrid.SetColumn(lblHeader2, 1);

        //    headerGrid.Children.Add(lblHeader1);
        //    headerGrid.Children.Add(lblHeader2);

        //    var rptDataTemplate = new DataTemplate(() =>
        //    {
        //        var bodyGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

        //        var lblData1 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };
        //        var lblData2 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };

        //        lblData1.SetBinding(Label.TextProperty, "InspectionYear");
        //        lblData2.SetBinding(Label.TextProperty, "MemberCount");

        //        bodyGrid.SetRow(lblData1, 0);
        //        bodyGrid.SetColumn(lblData1, 0);
        //        bodyGrid.SetRow(lblData2, 0);
        //        bodyGrid.SetColumn(lblData2, 1);

        //        bodyGrid.Children.Add(lblData1);
        //        bodyGrid.Children.Add(lblData2);

        //        return new ViewCell { View = bodyGrid };
        //    });

        //    var rptList = reportvm.GetInspectedMemberCountByInspectionYear();

        //    listviewReport.ItemsSource = rptList;
        //    listviewReport.Header = headerGrid;
        //    listviewReport.ItemTemplate = rptDataTemplate;
        //}

        //public void ShowNoOfMembersBySubCityReport()
        //{
        //    labelTitle.Text = "No. of Inspected Culverts By Inspection Year";

        //    ColumnDefinition col1 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };
        //    ColumnDefinition col2 = new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) };

        //    var headerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

        //    var lblHeader1 = new Label { Text = "Inspection Year", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };
        //    var lblHeader2 = new Label { Text = "No. of Culverts", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };

        //    headerGrid.SetRow(lblHeader1, 0);
        //    headerGrid.SetColumn(lblHeader1, 0);
        //    headerGrid.SetRow(lblHeader2, 0);
        //    headerGrid.SetColumn(lblHeader2, 1);

        //    headerGrid.Children.Add(lblHeader1);
        //    headerGrid.Children.Add(lblHeader2);

        //    var rptDataTemplate = new DataTemplate(() =>
        //    {
        //        var bodyGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

        //        var lblData1 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };
        //        var lblData2 = new Label() { HorizontalOptions = LayoutOptions.Center, FontSize = bodyFontSize };

        //        lblData1.SetBinding(Label.TextProperty, "InspectionYear");
        //        lblData2.SetBinding(Label.TextProperty, "CulvertCount");

        //        bodyGrid.SetRow(lblData1, 0);
        //        bodyGrid.SetColumn(lblData1, 0);
        //        bodyGrid.SetRow(lblData2, 0);
        //        bodyGrid.SetColumn(lblData2, 1);

        //        bodyGrid.Children.Add(lblData1);
        //        bodyGrid.Children.Add(lblData2);

        //        return new ViewCell { View = bodyGrid };
        //    });

        //    int culvertcount = 0;
        //    var rptList = reportvm.GetInspectedCulvertCountByInspectionYear(ref culvertcount);

        //    var footerGrid = new Grid() { ColumnDefinitions = new ColumnDefinitionCollection() { col1, col2 } };

        //    var lblFooter1 = new Label { Text = "TOTAL", FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };
        //    var lblFooter2 = new Label { Text = culvertcount.ToString(), FontSize = headerFontSize, FontAttributes = FontAttributes.Bold, Padding = new Thickness(0, 5, 5, 15), HorizontalOptions = LayoutOptions.Center };

        //    footerGrid.SetRow(lblFooter1, 2);
        //    footerGrid.SetColumn(lblFooter1, 0);
        //    footerGrid.SetRow(lblFooter2, 2);
        //    footerGrid.SetColumn(lblFooter2, 1);

        //    footerGrid.Children.Add(lblFooter1);
        //    footerGrid.Children.Add(lblFooter2);

        //    listviewReport.ItemsSource = rptList;
        //    listviewReport.Header = headerGrid;
        //    listviewReport.ItemTemplate = rptDataTemplate;
        //    listviewReport.Footer = footerGrid;
        //}
    }
}