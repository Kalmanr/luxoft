using System.Linq.Expressions;
using System.Net;

public static class ValdiateData
{
    public static float[]? isDataValid(string data)
    {
        string[] values = data.Split(',');
        float[] currencies = new float [values.Length];

        try
        {
            for(int i =0; i < values.Length ; i++)
            {
                currencies[i] = float.Parse(values[i]);
            }

            return currencies;
        }
        catch
        {
            throw new Exception("One or more values could not be accepted");
        }
        
    }

    public static Dictionary<float,int> CreateBillsCoinsDictionary(string money)
    {
        string[] moneyCount = money.Split(',');
        Dictionary<float,int> received = new Dictionary<float, int>();
        try{
            for(int i = 0; i< moneyCount.Length; i+=2)
            {
                float value = float.Parse(moneyCount[i]);
                int nItems = int.Parse(moneyCount[i+1]);

                received.Add(value,nItems);
            }
            
            return received;

        }catch
        {
            throw new Exception("Some values are not in the right format");
        }

    }
}