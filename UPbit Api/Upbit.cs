using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Security.Cryptography;
using static UPbitApi.UpbitClass;
using System.Net;
using Newtonsoft.Json;

namespace UPbitApi
{
    public class Upbit
    {
        private readonly string _publicKey;
        private readonly string _secretKey;
        private static readonly Uri _baseUrl = new("https://api.upbit.com/v1/");
        public Upbit(string publicKey, string secretKey)
        {
            if (string.IsNullOrWhiteSpace(publicKey)) { throw new ArgumentNullException(nameof(publicKey)); }
            if (string.IsNullOrWhiteSpace(secretKey)) { throw new ArgumentNullException(nameof(secretKey)); }
            _publicKey = publicKey;
            _secretKey = secretKey;
        }
        private static Dictionary<string, string> GenerateApiCallArgs(object obj)
        {
            var type = obj.GetType();
            var properties = type.GetProperties();
            var apiCallArgs = new Dictionary<string, string>();
            foreach (var property in properties)
            {
                apiCallArgs.Add(property.Name, (string)property.GetValue(obj));
            }
            return apiCallArgs;
        }
        private static string ToQueryString(Dictionary<string, string> parameters, bool isUrl = false)
        {
            List<string> parameterList = new List<string>();
            parameters.Keys.ToList().ForEach(key =>
            {
                var value = (string)parameters[key];
                if(!string.IsNullOrEmpty(value))
                    parameterList.Add($"{(isUrl ? HttpUtility.UrlEncode(key) : key)}={(isUrl ? HttpUtility.UrlEncode(value) : value)}");
            });
            return string.Join("&", parameterList);
        }

        private static string ToSHA512(string queryString)
        {
            SHA512 sha = SHA512.Create();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(queryString));
            var hexaHash = BitConverter.ToString(hash).Replace("-", "").ToLower();
            return hexaHash;
        }
        private string GenerateAuthenticationToken(Dictionary<string, string> args)
        {
            var queryString = ToQueryString(args);
            var payload = new Dictionary<string, string>
            {
                { "access_key" , _publicKey },
                { "nonce" , Guid.NewGuid().ToString() },
                { "query_hash" , ToSHA512(queryString) },
                { "query_hash_alg" , "SHA512" },
            };
            if (args.Count == 0)
            {
                payload.Remove("query_hash");
                payload.Remove("query_hash_alg");
            }

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var token = encoder.Encode(payload, _secretKey);
            return $"Bearer {token}";
        }
        private async Task<IRestResponse> ApiCall(string endPoint, Method method, Dictionary<string, string> args = null)
        {
            if (args is null)
                args = new Dictionary<string, string>();

            var authenticationToken = GenerateAuthenticationToken(args);
            Uri url = new(_baseUrl, endPoint);
            RestClient client = new(url);
            RestRequest request = new();
            request.Method = method;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", authenticationToken);
            if (method == Method.GET || method == Method.DELETE)
            {
                var urlString = client.BaseUrl.ToString().TrimEnd('/');
                urlString += $"/?{ToQueryString(args)}";
                client.BaseUrl = new Uri(urlString);
            }
            else
                request.AddJsonBody(args);

            if (args.Count == 0)
                request.Body = null;

            return await client.ExecuteAsync(request);
        }
        public async Task<List<Order.Response>> GetOrder(Order.Request args)
        {
            try
            {
                var response = await ApiCall("order", Method.GET, GenerateApiCallArgs(args));
                return JsonConvert.DeserializeObject<List<Order.Response>>(response.Content);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<Orders.Response>> GetOrders(Orders.Request args)
        {
            try
            {
                var response = await ApiCall("orders", Method.GET, GenerateApiCallArgs(args));
                return JsonConvert.DeserializeObject<List<Orders.Response>>(response.Content);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<CoinAddress.Response>> GetCoinAdresses()
        {
            try
            {
                var response = await ApiCall("deposits/coin_addresses", Method.GET);
                return JsonConvert.DeserializeObject<List<CoinAddress.Response>>(response.Content);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<CoinAddress.Response> GetCoinAdress(CoinAddress.Request args)
        {
            try
            {
                var response = await ApiCall("deposits/coin_address", Method.GET, GenerateApiCallArgs(args));
                return JsonConvert.DeserializeObject<CoinAddress.Response>(response.Content);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Chance.Response> GetChance(Chance.Request args)
        {
            try
            {
                var response = await ApiCall("orders/chance", Method.GET, GenerateApiCallArgs(args));
                return JsonConvert.DeserializeObject<Chance.Response>(response.Content);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Withdraw.Response> GetWithdraw(Withdraw.Request args)
        {
            try
            {
                var response = await ApiCall("deposit", Method.GET, GenerateApiCallArgs(args));
                return JsonConvert.DeserializeObject<Withdraw.Response>(response.Content);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<Withdraws.Response>> GetWithdraws(Withdraws.Request args)
        {
            try
            {
                var response = await ApiCall("deposits", Method.GET, GenerateApiCallArgs(args));
                return JsonConvert.DeserializeObject<List<Withdraws.Response>>(response.Content);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> CancelOrder(CancelOrder.Request args)
        {
            try
            {
                var response = await ApiCall("order", Method.DELETE, GenerateApiCallArgs(args));
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<List<WalletStatus.Response>> GetWalletStatus()
        {
            try
            {
                var response = await ApiCall("status/wallet", Method.GET);
                return JsonConvert.DeserializeObject<List<WalletStatus.Response>>(response.Content);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<ApiKeys.Response>> GetApiKeys()
        {
            try
            {
                var response = await ApiCall("api_keys", Method.GET);
                return JsonConvert.DeserializeObject<List<ApiKeys.Response>>(response.Content);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<Accounts.Response>> GetAccounts()
        {
            try
            {
                var response = await ApiCall("accounts", Method.GET);
                return JsonConvert.DeserializeObject<List<Accounts.Response>>(response.Content);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<GenerateCoinAddress.Response> GenerateCoinAddress(GenerateCoinAddress.Request args)
        {
            try
            {
                var response = await ApiCall("deposits/generate_coin_address", Method.POST, GenerateApiCallArgs(args));
                return JsonConvert.DeserializeObject<GenerateCoinAddress.Response>(response.Content);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<DepositKrw.Response> DepositKrw(DepositKrw.Request args)
        {
            try
            {
                var response = await ApiCall("deposits/krw", Method.POST, GenerateApiCallArgs(args));
                return JsonConvert.DeserializeObject<DepositKrw.Response>(response.Content);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> PlaceOrder(PlaceOrder.Request args)
        {
            try
            {
                var response = await ApiCall("orders", Method.POST, GenerateApiCallArgs(args));
                if (response.StatusCode == HttpStatusCode.Created)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static async Task<List<Ticks.Response>> GetTicks(Ticks.Request args)
        {
            try
            {
                var responseString = await new WebClient().DownloadStringTaskAsync(new Uri($"https://api.upbit.com/v1/trades/ticks?{ToQueryString(GenerateApiCallArgs(args), true)}"));
                return JsonConvert.DeserializeObject<List<Ticks.Response>>(responseString);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static async Task<List<Ticker>> GetTicker(string symbol)
        {
            try
            {
                var responseString = await new WebClient().DownloadStringTaskAsync(new Uri($"https://api.upbit.com/v1/ticker/?markets={symbol}"));
                return JsonConvert.DeserializeObject<List<Ticker>>(responseString);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static async Task<List<MarketCodes>> GetMarketCodes()
        {
            try
            {
                var responseString = await new WebClient().DownloadStringTaskAsync(new Uri($"https://api.upbit.com/v1/market/all?isDetails=true"));
                return JsonConvert.DeserializeObject<List<MarketCodes>>(responseString);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static async Task<List<DayCandles.Response>> GetDayCandles(DayCandles.Request args)
        {
            try
            {
                var responseString = await new WebClient().DownloadStringTaskAsync(new Uri($"https://api.upbit.com/v1/candles/days?{ToQueryString(GenerateApiCallArgs(args), true)}"));
                return JsonConvert.DeserializeObject<List<DayCandles.Response>>(responseString);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static async Task<List<Candles.Response>> GetWeekCandles(Candles.Request args)
        {
            try
            {
                var responseString = await new WebClient().DownloadStringTaskAsync(new Uri($"https://api.upbit.com/v1/candles/weeks?{ToQueryString(GenerateApiCallArgs(args), true)}"));
                return JsonConvert.DeserializeObject<List<Candles.Response>>(responseString);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static async Task<List<Candles.Response>> GetMonthCandles(Candles.Request args)
        {
            try
            {
                var responseString = await new WebClient().DownloadStringTaskAsync(new Uri($"https://api.upbit.com/v1/candles/months?{ToQueryString(GenerateApiCallArgs(args), true)}"));
                return JsonConvert.DeserializeObject<List<Candles.Response>>(responseString);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static async Task<List<Candles.Response>> GetMinuteCandles(int unit, Candles.Request args)
        {
            try
            {
                var responseString = await new WebClient().DownloadStringTaskAsync(new Uri($"https://api.upbit.com/v1/candles/minutes/{unit}?{ToQueryString(GenerateApiCallArgs(args), true)}"));
                return JsonConvert.DeserializeObject<List<Candles.Response>>(responseString);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<List<OrderBook.Response>> GetOrderBooks(string symbol)
        {
            try
            {
                var responseString = await (new WebClient()).DownloadStringTaskAsync(new Uri($"https://api.upbit.com/v1/orderbook/?markets={symbol}"));
                return JsonConvert.DeserializeObject<List<OrderBook.Response>>(responseString);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
