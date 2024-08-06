
using System.Drawing;
using System.Reflection;
using System.Security.Cryptography;

public class Calculate : ICalculate
{
    private List<float> _products;
    private Dictionary<float,int> _moneyreceived; 
    private string _countryCode;
    public Calculate(List<float> products,Dictionary<float,int> moneyReceived, string countryCode)
    {
        _products = products;
        _moneyreceived = moneyReceived;
        _countryCode = countryCode;
    }    


    public Dictionary<float,int> CalculateChange ()
    {
        LocalCurrency currency = LocalCurrency.Instance;
        float[]? localCurrency =currency.GetCurrency(_countryCode);
        //float [] localCurrency = {0.05F,0.10F,0.20F,0.50F, 1.00F, 2.00F, 5.00F, 1.00f, 20.00F, 50.00F, 100.00F, 200.00F, 500.00F, 1000.00F};
        Dictionary<float,int> result = new Dictionary<float, int>();

        float totalProducts = _products.Sum();
        float totalReceived = 0.00F;
        float change= 0.00F;

        foreach(var item in _moneyreceived)
        {
            totalReceived = totalReceived + item.Key * item.Value;
        }

        change = (float)Math.Round(totalReceived - totalProducts,2);
        Console.WriteLine($"Total Amount: {totalProducts}");
        Console.WriteLine($"Total Received: {totalReceived}");
        Console.WriteLine($"Change: {change}");

        if(localCurrency == null || change < 0)
        {
            throw new Exception("No currency or proces data available");
        }

        float[] optimalChange = localCurrency.Where(x => x < change).ToArray();
        int p1 = 0;
        int countBillsCoins = 0;
        float totalChange = change;

        p1 = optimalChange.Length -1;

        while(p1 >= 0)
        {
            totalChange = (float)Math.Round(totalChange - optimalChange[p1],2);
            if(totalChange < 0 )
            {   totalChange = change;
                p1--;
            }
            else 
            {
                change = totalChange;
                if(result.TryGetValue(optimalChange[p1], out countBillsCoins))
                {
                    countBillsCoins = countBillsCoins + 1;
                    optimalChange[p1] = countBillsCoins;
                }
                else
                {
                    result.Add(optimalChange[p1],1);
                }
                countBillsCoins = 0;
            }


        }


        return result;
    }


}