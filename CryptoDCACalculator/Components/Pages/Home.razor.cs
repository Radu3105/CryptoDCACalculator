using CryptoDCACalculator.Business.Services;
using CryptoDCACalculator.Business.Services.Abstractions;
using CryptoDCACalculator.DataAccess.Context;
using CryptoDCACalculator.DataAccess.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System.Diagnostics;

namespace CryptoDCACalculator.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        private CryptoDCACalculatorContext? _context {  get; set; }

        [Inject]
        private IHistoricalPriceService? _historicalPriceService { get; set; }

        [Inject]
        private IJSRuntime JS { get; set; }

        private List<CryptoCurrency> _currencies = new();
        private List<HistoricalPrice> _monthlyPrices = new();
        private string _selectedCryptoCurrencyName = "";
        private bool _isLoading = true;
        private decimal _currentPrice;
        private DateTime _startDate = new DateTime(2024, 1, 1); // Set the start date for the investments (IMPORTANT! Make sure it goes maximum 1 year in the past because the free api tier does not offer historical data > 1 year)
        private decimal _investedAmount = 1000; // Set the invested amount
        private int _enabledInvestmentDay = 15; // Set the enabled day for recurring investments (for example 15th of every month)
        private string[] _xLabels = new string[] { };
        private double[] _chartData;
        private bool _shouldRenderChart = false;

        protected override async Task OnInitializedAsync()
        {
            if (_context != null)
            {
                _currencies = await _context.CryptoCurrencies.ToListAsync();
                _selectedCryptoCurrencyName = _currencies.First().Name;
                await FetchPrices();
                _isLoading = false;

            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender || _shouldRenderChart)
            {
                _shouldRenderChart = false;
                await InitializeChart();
            }
        }

        private async Task InitializeChart()
        {
            if (_xLabels.Length > 0 && _chartData.Length > 0)
            {
                await JS.InvokeVoidAsync("initializeChart", "myChart", _xLabels, _chartData, _selectedCryptoCurrencyName);
            }
        }

        private async Task HandleCurrencyChange(ChangeEventArgs e)
        {
            _isLoading = true;
            _selectedCryptoCurrencyName = e.Value.ToString();
            await FetchPrices();
            _isLoading = false;
        }

        private async Task HandleDateChange(ChangeEventArgs e)
        {
            if (DateTime.TryParse(e.Value.ToString(), out DateTime newDate))
            {
                _startDate = newDate;
                await FetchPrices();
            }
        }

        private async Task HandleInvestmentAmountChange(ChangeEventArgs e)
        {
            if (decimal.TryParse(e.Value.ToString(), out decimal newAmount))
            {
                _investedAmount = newAmount;
                if (_investedAmount <= 0)
                {
                    _investedAmount = 1;
                }
                await FetchPrices();
            }
        }

        private async Task FetchPrices()
        {
            _isLoading = true;
            _monthlyPrices.Clear();
            List<string> labels = new();
            List<double> prices = new();
            DateTime currentDate = _startDate;
            _currentPrice = await _historicalPriceService.GetCachedCurrentPriceAsync(_selectedCryptoCurrencyName.ToLower());
            while (currentDate <= DateTime.Today)
            {
                if (!string.IsNullOrEmpty(_selectedCryptoCurrencyName))
                {
                    var price = await _historicalPriceService.GetPriceForDateByNameAsync(_selectedCryptoCurrencyName.ToLower(), new DateTime(currentDate.Year, currentDate.Month, _enabledInvestmentDay));
                    if (price != null)
                    {
                        _monthlyPrices.Add(price);
                        labels.Add(currentDate.ToString("MMM yyyy"));
                        prices.Add((double)price.Price); 
                    }
                }
                currentDate = currentDate.AddMonths(1);
            }
            _xLabels = labels.ToArray();
            _chartData = prices.ToArray();
            _isLoading = false;
            _shouldRenderChart = true;  
            StateHasChanged();
        }
    }
}
