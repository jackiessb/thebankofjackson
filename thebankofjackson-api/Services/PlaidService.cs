using Going.Plaid;
using Going.Plaid.Entity;
using Going.Plaid.Item;
using Going.Plaid.Link;
using System.Text.Json;
using thebankofjackson_api.Controllers;
using thebankofjackson_api.Credentials;
using Environment = Going.Plaid.Environment;

namespace thebankofjackson_api.Services {
    public interface IPlaidService {
        public Task<LinkTokenCreateResponse> GetLinkToken();
        public void ExchangeLinkToken();
    }

    public class PlaidService : IPlaidService {
        private readonly IConfiguration _config;
        private readonly IPlaidCredentials _credentials;
        private readonly ILogger<PlaidService> _logger;

        private PlaidClient _client;
        public PlaidService(ILogger<PlaidService> logger, IPlaidCredentials credentials, IConfiguration config)
        {
            _logger = logger;
            _credentials = credentials;
            _config = config;
            _client = new PlaidClient(Environment.Sandbox);

            // Change to "PlaidProd" when it is time to hit real endpoint
            _config.Bind("PlaidSandbox", _credentials);
        }

        public async Task<LinkTokenCreateResponse> GetLinkToken() {
            var request = new LinkTokenCreateRequest
            {
                ClientId = _credentials.ClientID,
                Secret = _credentials.Secret,
                User = new LinkTokenCreateRequestUser { ClientUserId = "Jackson" },
                ClientName = "HappyDad",
                Products = GetProducts(),
                Language = Language.English,
                CountryCodes = GetCountryCodes()
            };

            _logger.LogInformation("Request created. Calling async...");

            try
            {
                var response = await _client.LinkTokenCreateAsync(request);

                if (response.LinkToken != null) {
                    _credentials.LinkToken = response.LinkToken;
                }

                _logger.LogInformation("LinkTokenCreateAsync response StatusCode: " + JsonSerializer.Serialize(response.StatusCode));
                _logger.LogInformation("LinkTokenCreateAsync response LinkToken: " + JsonSerializer.Serialize(response.LinkToken));

                return response;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async void ExchangeLinkToken() {
            
        }

        private Products[] GetProducts()
        {
            var products = new List<Products>();

            foreach (var product in _credentials.Products.ToList())
            {
                var productToAdd = Enum.Parse<Products>(product, true);

                products.Add(productToAdd);
            }

            if (products.Count <= 0)
            {
                _logger.LogInformation("No products found. Creating an empty list of them");

                return Array.Empty<Products>();
            }
            else
            {
                var productArray = products.ToArray();

                _logger.LogInformation("Number of products found: " + productArray.Length);

                return productArray;
            }
        }

        private CountryCode[] GetCountryCodes()
        {
            var codes = new List<CountryCode>();

            foreach (var code in _credentials.CountryCodes.ToList())
            {
                var codeToAdd = Enum.Parse<CountryCode>(code, true);

                codes.Add(codeToAdd);
            }

            if (codes.Count <= 0)
            {
                _logger.LogInformation("No codes found. Creating an empty list of them");

                return Array.Empty<CountryCode>();
            }
            else
            {
                var codeArray = codes.ToArray();

                _logger.LogInformation("Number of codes found: " + codeArray.Length);

                return codeArray;
            }
        }
    }
}
