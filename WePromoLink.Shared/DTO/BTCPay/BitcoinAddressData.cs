namespace WePromoLink.DTO.BTCPay;

public class BitcoinAddressData
{
    public string hash160 { get; set; }
    public string address { get; set; }
    public int n_tx { get; set; }
    public int n_unredeemed { get; set; }
    public long total_received { get; set; }
    public long total_sent { get; set; }
    public long final_balance { get; set; }
    public List<object> txs { get; set; }
}