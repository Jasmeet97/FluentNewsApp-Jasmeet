using FluentNewsApp_Jasmeet.Models;
using System.Collections.ObjectModel;

namespace FluentNewsApp_Jasmeet.ViewModels
{
    public class CategoryViewModel(string categoryName) : ViewModelBase
    {
        private bool _isLoading;
        private bool _isError;

        public string CategoryName { get; } = categoryName;
        public ObservableCollection<Article> Articles { get; } = [];

        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsError
        {
            get => _isError;
            private set
            {
                if (_isError != value)
                {
                    _isError = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task GetArticlesAsync()
        {
            try
            {
                IsError = false;
                IsLoading = true;

                Random rnd = new();
                await Task.Delay(rnd.Next(1, 10) * 1000);

                for (int i = 0; i < 10; i++)
                {
                    Articles.Add(new Article() { Headline = $"Test {i} {CategoryName}", PublishedAt = new DateTime() });
                }

                IsLoading = false;
            }
            catch (Exception ex)
            {
                IsError = true;
            }
        }
    }
}
