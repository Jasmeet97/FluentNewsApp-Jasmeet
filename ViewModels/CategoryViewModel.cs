using FluentNewsApp_Jasmeet.Models;
using FluentNewsApp_Jasmeet.Services;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace FluentNewsApp_Jasmeet.ViewModels
{
    public class CategoryViewModel(string categoryName) : ViewModelBase
    {
        private bool _isLoading;
        private bool _isError;
        private bool _isEmpty;
        private string _message = string.Empty;
        private readonly NewsApiService _newsApiService = new();

        public string CategoryName { get; } = categoryName;
        public ObservableCollection<Article> Articles { get; set; } = [];

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
                    OnPropertyChanged(nameof(ShowMessage));
                }
            }
        }

        public bool IsEmpty
        {
            get => _isEmpty;
            private set
            {
                if (_isEmpty != value)
                {
                    _isEmpty = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ShowMessage));
                }
            }
        }

        public string Message
        {
            get => _message;
            private set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ShowMessage => IsError || IsEmpty;

        public static void SimulateError()
        {
            List<Exception> errors = [
                new HttpRequestException($"Request failed with status 400"),
                new Exception("Failed to display articles."),
                new Exception("Something went wrong. Try again later.")
            ];
            Random rnd = new();
            int index = rnd.Next(0, errors.Count);
            throw errors[index];
        }

        public async Task GetArticlesAsync(bool simulateError = false, bool simulateEmpty = false)
        {
            try
            {
                IsError = false;
                IsLoading = true;
                IsEmpty = false;
                Message = string.Empty;

                // Clearing the list before loading new
                Articles.Clear();

                // Keeping the delay to simulate a longer wait time
                Random rnd = new();
                await Task.Delay(rnd.Next(1, 10) * 1000);

                if (simulateError)
                {
                    SimulateError();
                }

                if (simulateEmpty)
                {
                    IsEmpty = true;
                    Message = "No News Articles Found.";
                    return;
                }

                var articles = await _newsApiService.GetNewsAsync(CategoryName);
                if (articles.Count == 0)
                {
                    IsEmpty = true;
                    Message = "No News Articles Found.";
                    return;
                }

                foreach (var article in articles)
                {
                    Articles.Add(article);
                }
            }
            catch (HttpRequestException exception)
            {
                // For Logging Purposes
                Console.Error.WriteLine(exception.Message);
                IsError = true;
                // Assuming user wouldn't understand http request exception states
                Message = "Failed to fetch news articles.";
            }
            catch (Exception exception)
            {
                // For Logging Purposes
                Console.Error.WriteLine(exception.Message);
                IsError = true;
                Message = exception.Message;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
