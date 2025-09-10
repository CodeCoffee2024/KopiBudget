using KopiBudget.Application.Dtos;
using KopiBudget.Application.Interfaces.Services;
using KopiBudget.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace KopiBudget.Infrastructure.Services
{
    public class ExchangeRateService : BackgroundService, IExchangeRateProviderService
    {
        #region Fields

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ExchangeRateService> _logger;
        private readonly string _apiKey;
        private string _defaultCurrency;
        private readonly object _lock = new();

        private ExchangeRate? _latestRates;
        private ExchangeRate? _previousRates;

        #endregion Fields

        #region Public Constructors

        public ExchangeRateService(
            IHttpClientFactory httpClientFactory,
            ILogger<ExchangeRateService> logger,
            IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _apiKey = config["ExchangeRateApi:ApiKey"]!;
            _defaultCurrency = config["ExchangeRateApi:DefaultCurrency"]!;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ExchangeRateDto?> GetLatestRatesAsync(string? currency)
        {
            if (_defaultCurrency != currency && currency != null)
            {
                _previousRates = null;
                _latestRates = null;
            }
            _defaultCurrency = currency!;
            await FetchRates();

            lock (_lock)
            {
                if (_latestRates == null) return null; // still null if fetch failed

                var dto = new ExchangeRateDto
                {
                    BaseCurrency = _latestRates.BaseCurrency,
                    ConversionRates = _latestRates.ConversionRates.Select(kvp =>
                    {
                        decimal? previousRate = null;
                        decimal? change = null;

                        if (_previousRates?.ConversionRates.TryGetValue(kvp.Key, out var prevValue) == true)
                        {
                            previousRate = prevValue;
                            if (prevValue != 0)
                            {
                                change = (kvp.Value - prevValue) / prevValue;
                            }
                        }

                        return new ExchangeRateRowDto
                        {
                            Currency = kvp.Key,
                            Rate = kvp.Value,
                            PreviousRate = previousRate,
                            Change = change
                        };
                    }).ToList()
                };

                return dto;
            }
        }

        #endregion Public Methods

        #region Protected Methods

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await FetchRates();
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private async Task FetchRates()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetFromJsonAsync<ExchangeRateApiResponse>(
                    $"https://v6.exchangerate-api.com/v6/{_apiKey}/latest/{_defaultCurrency}");

                if (response != null)
                {
                    var newRates = new ExchangeRate
                    {
                        BaseCurrency = response.Base,
                        RetrievedAt = DateTime.UtcNow,
                        ConversionRates = response.ConversionRates
                    };

                    lock (_lock)
                    {
                        // move latest into previous before updating
                        _previousRates = _latestRates;
                        _latestRates = newRates;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching exchange rates");
            }
        }

        #endregion Private Methods

        #region Inner Classes

        private class ExchangeRateApiResponse
        {
            #region Properties

            [JsonPropertyName("base_code")]
            public string Base { get; set; } = default!;
            [JsonPropertyName("conversion_rates")]
            public Dictionary<string, decimal> ConversionRates { get; set; } = new();

            #endregion Properties
        }

        #endregion Inner Classes
    }
}