using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace App9
{
    public partial class MainPage : ContentPage
    {
        public List<DayInfo> Days { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnMonthButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            int month = int.Parse(button.CommandParameter.ToString());
            DateTime selectedMonth = new DateTime(2025, month, 1);
            await Navigation.PushAsync(new MonthViewPage(selectedMonth));
        }
    }
}
