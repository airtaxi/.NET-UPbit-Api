using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPbitApi
{
    public class UpbitClass
    {
        public class DayCandles
        {
            public class Response
            {
                public string market { get; set; }
                public DateTime candle_date_time_utc { get; set; }
                public DateTime candle_date_time_kst { get; set; }
                public int opening_price { get; set; }
                public int high_price { get; set; }
                public int low_price { get; set; }
                public int trade_price { get; set; }
                public long timestamp { get; set; }
                public double candle_acc_trade_price { get; set; }
                public double candle_acc_trade_volume { get; set; }
                public int prev_closing_price { get; set; }
                public int change_price { get; set; }
                public double change_rate { get; set; }
            }
            public class Request
            {
                public string market { get; set; }
                public string to { get; set; }
                public string count { get; set; }
                public string convertingPriceUnit { get; set; }
            }
        }
        public class Candles
        {
            public class Request
            {
                public string market { get; set; }
                public string to { get; set; }
                public string count { get; set; }
            }
            public class Response
            {
                public string market { get; set; }
                public DateTime candle_date_time_utc { get; set; }
                public DateTime candle_date_time_kst { get; set; }
                public int opening_price { get; set; }
                public int high_price { get; set; }
                public int low_price { get; set; }
                public int trade_price { get; set; }
                public long timestamp { get; set; }
                public double candle_acc_trade_price { get; set; }
                public double candle_acc_trade_volume { get; set; }
                public int unit { get; set; }
            }
        }
        public class MarketCodes
        {
            public string market { get; set; }
            public string korean_name { get; set; }
            public string english_name { get; set; }
            public string market_warning { get; set; }
        }
        public class ApiKeys
        {
            public class Response
            {
                public string access_key { get; set; }
                public DateTime expire_at { get; set; }
            }
        }
        public class WalletStatus
        {
            public class Response
            {
                public string currency { get; set; }
                public string wallet_state { get; set; }
                public string block_state { get; set; }
                public int block_height { get; set; }
                public DateTime block_updated_at { get; set; }
            }
        }
        public class DepositKrw
        {
            public class Request
            {
                public string amount { get; set; }
            }

            public class Response
            {
                public string type { get; set; }
                public string uuid { get; set; }
                public string currency { get; set; }
                public string txid { get; set; }
                public string state { get; set; }
                public DateTime created_at { get; set; }
                public object done_at { get; set; }
                public string amount { get; set; }
                public string fee { get; set; }
                public string transaction_type { get; set; }
            }
        }
        public class CoinAddress
        {
            public class Request
            {
                public string currency { get; set; }
            }
            public class Response
            {
                public string currency { get; set; }
                public string deposit_address { get; set; }
                public string secondary_address { get; set; }
            }
        }
        public class GenerateCoinAddress
        {
            public class Response
            {
                public bool success { get; set; }
                public string message { get; set; }
            }
            public class Request
            {
                public string currency { get; set; }
            }
        }
        public class Withdraw
        {
            public class Response
            {
                public string type { get; set; }
                public string uuid { get; set; }
                public string currency { get; set; }
                public string txid { get; set; }
                public string state { get; set; }
                public DateTime created_at { get; set; }
                public DateTime done_at { get; set; }
                public string amount { get; set; }
                public string fee { get; set; }
                public string transaction_type { get; set; }
            }
            public class Request
            {
                public string uuid { get; set; }
                public string txid { get; set; }
                public string currency { get; set; }
            }
        }
        public class Withdraws
        {
            public class Response
            {
                public string type { get; set; }
                public string uuid { get; set; }
                public string currency { get; set; }
                public string txid { get; set; }
                public string state { get; set; }
                public DateTime created_at { get; set; }
                public DateTime done_at { get; set; }
                public string amount { get; set; }
                public string fee { get; set; }
                public string transaction_type { get; set; }
            }
            public class Request
            {
                public string currency { get; set; }
                public string state { get; set; }
                public string limit { get; set; }
                public string page { get; set; }
                public string order_by { get; set; }
            }
        }
        public class Order
        {
            public class Response
            {
                public class Trade
                {
                    public string market { get; set; }
                    public string uuid { get; set; }
                    public string price { get; set; }
                    public string volume { get; set; }
                    public string funds { get; set; }
                    public string side { get; set; }
                }
                public string uuid { get; set; }
                public string side { get; set; }
                public string ord_type { get; set; }
                public string price { get; set; }
                public string state { get; set; }
                public string market { get; set; }
                public DateTime created_at { get; set; }
                public string volume { get; set; }
                public string remaining_volume { get; set; }
                public string reserved_fee { get; set; }
                public string remaining_fee { get; set; }
                public string paid_fee { get; set; }
                public string locked { get; set; }
                public string executed_volume { get; set; }
                public int trades_count { get; set; }
                public List<Trade> trades { get; set; }
            }
            public class Request
            {
                public string uuid { get; set; }
                public string identifier { get; set; }
            }
        }
        public class Orders
        {
            public class Request
            {
                public string market { get; set; }
                public string state { get; set; }
                public string page { get; set; }
                public string limit { get; set; }
                public string order_by { get; set; }
            }
            public class Response
            {
                public string uuid { get; set; }
                public string side { get; set; }
                public string ord_type { get; set; }
                public string price { get; set; }
                public string state { get; set; }
                public string market { get; set; }
                public DateTime created_at { get; set; }
                public string volume { get; set; }
                public string remaining_volume { get; set; }
                public string reserved_fee { get; set; }
                public string remaining_fee { get; set; }
                public string paid_fee { get; set; }
                public string locked { get; set; }
                public string executed_volume { get; set; }
                public int trades_count { get; set; }
            }
        }
        public class CancelOrder
        {
            public class Request
            {
                public string uuid { get; set; }
            }
            public class Response
            {
                public string uuid { get; set; }
                public string side { get; set; }
                public string ord_type { get; set; }
                public string price { get; set; }
                public string state { get; set; }
                public string market { get; set; }
                public DateTime created_at { get; set; }
                public string volume { get; set; }
                public string remaining_volume { get; set; }
                public string reserved_fee { get; set; }
                public string remaining_fee { get; set; }
                public string paid_fee { get; set; }
                public string locked { get; set; }
                public string executed_volume { get; set; }
                public int trades_count { get; set; }
            }
        }
        public class Ticks
        {
            public class Response
            {
                public string market { get; set; }
                public string trade_date_utc { get; set; }
                public string trade_time_utc { get; set; }
                public long timestamp { get; set; }
                public int trade_price { get; set; }
                public double trade_volume { get; set; }
                public int prev_closing_price { get; set; }
                public int chane_price { get; set; }
                public string ask_bid { get; set; }
            }
            public class Request
            {
                public string market { get; set; }
                public string to { get; set; }
                public string count { get; set; }
                public string cursor { get; set; }
                public string daysAgo { get; set; }
            }
        }
        public class Ticker
        {
            public string market { get; set; }
            public string trade_date { get; set; }
            public string trade_time { get; set; }
            public string trade_date_kst { get; set; }
            public string trade_time_kst { get; set; }
            public long trade_timestamp { get; set; }
            public double opening_price { get; set; }
            public double high_price { get; set; }
            public double low_price { get; set; }
            public double trade_price { get; set; }
            public double prev_closing_price { get; set; }
            public string change { get; set; }
            public double change_price { get; set; }
            public double change_rate { get; set; }
            public double signed_change_price { get; set; }
            public double signed_change_rate { get; set; }
            public double trade_volume { get; set; }
            public double acc_trade_price { get; set; }
            public double acc_trade_price_24h { get; set; }
            public double acc_trade_volume { get; set; }
            public double acc_trade_volume_24h { get; set; }
            public double highest_52_week_price { get; set; }
            public string highest_52_week_date { get; set; }
            public double lowest_52_week_price { get; set; }
            public string lowest_52_week_date { get; set; }
            public long timestamp { get; set; }
        }
        public class OrderBook
        {
            public class Response
            {
                public class OrderbookUnit
                {
                    public double ask_price { get; set; }
                    public double bid_price { get; set; }
                    public double ask_size { get; set; }
                    public double bid_size { get; set; }
                }

                public string market { get; set; }
                public long timestamp { get; set; }
                public double total_ask_size { get; set; }
                public double total_bid_size { get; set; }
                public List<OrderbookUnit> orderbook_units { get; set; }
            }
        }
        public class PlaceOrder
        {
            public class Request
            {
                public string market { get; set; }
                public string side { get; set; }
                public string volume { get; set; }
                public string price { get; set; }
                public string ord_type { get; set; }
            }
            public class Response
            {
                public string uuid { get; set; }
                public string side { get; set; }
                public string ord_type { get; set; }
                public string price { get; set; }
                public string avg_price { get; set; }
                public string state { get; set; }
                public string market { get; set; }
                public DateTime created_at { get; set; }
                public string volume { get; set; }
                public string remaining_volume { get; set; }
                public string reserved_fee { get; set; }
                public string remaining_fee { get; set; }
                public string paid_fee { get; set; }
                public string locked { get; set; }
                public string executed_volume { get; set; }
                public int trades_count { get; set; }
            }
        }

        public class Chance
        {
            public class Response
            {
                public class Bid
                {
                    public string currency { get; set; }
                    public object price_unit { get; set; }
                    public int min_total { get; set; }
                }

                public class Ask
                {
                    public string currency { get; set; }
                    public object price_unit { get; set; }
                    public int min_total { get; set; }
                }

                public class Market
                {
                    public string id { get; set; }
                    public string name { get; set; }
                    public List<string> order_types { get; set; }
                    public List<string> order_sides { get; set; }
                    public Bid bid { get; set; }
                    public Ask ask { get; set; }
                    public string max_total { get; set; }
                    public string state { get; set; }
                }

                public class BidAccount
                {
                    public string currency { get; set; }
                    public string balance { get; set; }
                    public string locked { get; set; }
                    public string avg_buy_price { get; set; }
                    public bool avg_buy_price_modified { get; set; }
                    public string unit_currency { get; set; }
                }

                public class AskAccount
                {
                    public string currency { get; set; }
                    public string balance { get; set; }
                    public string locked { get; set; }
                    public string avg_buy_price { get; set; }
                    public bool avg_buy_price_modified { get; set; }
                    public string unit_currency { get; set; }
                }

                public string bid_fee { get; set; }
                public string ask_fee { get; set; }
                public Market market { get; set; }
                public BidAccount bid_account { get; set; }
                public AskAccount ask_account { get; set; }
            }
            public class Request
            {
                public string market { get; set; }
            }
        }
        public class Accounts
        {
            public class Response
            {
                public string currency { get; set; }
                public string balance { get; set; }
                public string locked { get; set; }
                public string avg_buy_price { get; set; }
                public bool avg_buy_price_modified { get; set; }
                public string unit_currency { get; set; }
            }
        }
    }
}