using FluentNewsApp_Jasmeet.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FluentNewsApp_Jasmeet.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly List<string> PredefinedCategories = ["Technology", "Business", "Sports", "Family"];
        public ObservableCollection<CategoryViewModel> Categories { get; } = [];
        public ICommand RefreshCommand { get; }
        public ICommand SimulateErrorCommand { get; }
        public ICommand SimulateEmptyCommand { get; }

        public MainViewModel()
        {
            // Initialize categories
            foreach (var category in PredefinedCategories)
            {
                Categories.Add(new CategoryViewModel(category));
            }

            // Load of categories on startup
            _ = LoadCategoriesAsync();

            // Initialize Refresh Command
            RefreshCommand = new RelayCommand(async _ => await LoadCategoriesAsync());
            SimulateErrorCommand = new RelayCommand(async _ => await LoadCategoriesAsync(true));
            SimulateEmptyCommand = new RelayCommand(async _ => await LoadCategoriesAsync(false, true));
        }
        private async Task LoadCategoriesAsync(bool simulateError = false, bool simulateEmpty = false)
        {
            // Get all category results in parallel
            await Task.WhenAll(Categories.Select(category => category.GetArticlesAsync(simulateError, simulateEmpty)));
        }
    }
}
