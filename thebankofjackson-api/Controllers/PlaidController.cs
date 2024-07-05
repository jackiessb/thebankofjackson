using Going.Plaid;
using Going.Plaid.Entity;
using Going.Plaid.Link;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using thebankofjackson_api.Credentials;
using thebankofjackson_api.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Environment = Going.Plaid.Environment;

namespace thebankofjackson_api.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class PlaidController : ControllerBase {
        private readonly ILogger<PlaidController> _logger;
        private readonly IPlaidService _plaidService;

        public PlaidController(ILogger<PlaidController> logger, IPlaidService plaidService)
        {
            _logger = logger;
            _plaidService = plaidService;
        }

        [HttpGet(Name = "GetLinkToken")]
        public async Task<LinkTokenCreateResponse> GetLinkToken()
        {
            _logger.LogInformation("Starting call to Plaid for Link Token");

            try
            {
                var response = await _plaidService.GetLinkToken();

                _logger.LogInformation("GetLinkToken response StatusCode: " + JsonSerializer.Serialize(response.StatusCode));
                _logger.LogInformation("GetLinkToken response LinkToken: " + JsonSerializer.Serialize(response.LinkToken));

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetLinkToken failed: " + ex.Message);

                throw new Exception();
            }
        }

        [HttpGet(Name = "ExchangeLinkToken")]
        public async void ExchangeLinkToken() {
            _logger.LogInformation("Starting call to Plaid to exchange link token");
        }
    }
}
