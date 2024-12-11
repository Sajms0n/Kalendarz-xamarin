using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace App9
{
    public partial class MonthViewPage : ContentPage
    {
        public List<DayInfo> Days { get; set; }
        public DateTime SelectedMonth { get; set; }
        private DayInfo selectedDay;

        public MonthViewPage(DateTime month)
        {
            InitializeComponent();
            SelectedMonth = month;
            MonthLabel.Text = month.ToString("MMMM yyyy");
            GenerateDays();
            DaysCollectionView.ItemsSource = Days;
        }

        private void GenerateDays()
        {
            Days = new List<DayInfo>();
            int daysInMonth = DateTime.DaysInMonth(SelectedMonth.Year, SelectedMonth.Month);
            DateTime firstDayOfMonth = new DateTime(SelectedMonth.Year, SelectedMonth.Month, 1);
            int offset = (int)firstDayOfMonth.DayOfWeek - 1;
            if (offset < 0) offset = 6; // Adjust for Monday start

            for (int i = 0; i < offset; i++)
            {
                Days.Add(new DayInfo { Date = DateTime.MinValue, DayNumber = null }); // Empty days for alignment
            }

            for (int day = 1; day <= daysInMonth; day++)
            {
                Days.Add(new DayInfo { Date = new DateTime(SelectedMonth.Year, SelectedMonth.Month, day), DayNumber = day });
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDay = e.CurrentSelection[0] as DayInfo;
            if (selectedDay != null && selectedDay.Date != DateTime.MinValue && !string.IsNullOrWhiteSpace(selectedDay.EventDescription))
            {
                DisplayAlert("Day Selected", selectedDay.EventDescription, "OK");
            }
        }

        private void OnAddEventClicked(object sender, EventArgs e)
        {
            if (selectedDay != null && selectedDay.Date != DateTime.MinValue)
            {
                EventEntry.IsVisible = true;
                SaveEventButton.IsVisible = true;
            }
            else
            {
                DisplayAlert("Error", "Please select a valid day first.", "OK");
            }
        }

        private void OnSaveEventClicked(object sender, EventArgs e)
        {
            if (selectedDay != null && selectedDay.Date != DateTime.MinValue && !string.IsNullOrWhiteSpace(EventEntry.Text))
            {
                // Add your logic to save the event to the selected day
                selectedDay.EventDescription = EventEntry.Text;
                DisplayAlert("Event Saved", $"Event for {selectedDay.Date.ToString("MMMM dd, yyyy")}: {selectedDay.EventDescription}", "OK");

                // Hide the entry and button after saving
                EventEntry.IsVisible = false;
                SaveEventButton.IsVisible = false;
                EventEntry.Text = string.Empty;
            }
            else
            {
                DisplayAlert("Error", "Please enter event details.", "OK");
            }
        }
    }

    public class DayInfo
    {
        public DateTime Date { get; set; }
        public int? DayNumber { get; set; }
        public string EventDescription { get; set; }
    }
}
