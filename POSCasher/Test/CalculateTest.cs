using System.Diagnostics;
using Xunit;


public class CalculateTest
{
    [Fact]
    public void CalculateHappy()
    {
        //Arrange
        LocalCurrency currencyM = LocalCurrency.Instance;
        float[] currency = [0.05f,0.1f,0.2f,0.5f,1.00f,2.00f];
        currencyM.AddCurrencyValues("MX",currency);
        float[] prices = [0.05f,0.25f];
        Dictionary<float,int> dict = new Dictionary<float, int>();
        dict.Add(1.00f,1);
        string coutryCode = "MX";

        //Act
        var calculate = new Calculate(prices.ToList(),dict,coutryCode);


        //Assert
        var result = calculate.CalculateChange();
        float sum = 0.00f;
        foreach(var item in result )
        {
            sum = (float)Math.Round(sum + item.Key,2);
        }
        Assert.Equal(0.70f,sum);
    }

        [Fact]
        public void CalculateWrong()
    {
        //Arrange
        LocalCurrency currencyM =  new LocalCurrency();
        float[] prices = [0.05f,0.25f];
        Dictionary<float,int> dict = new Dictionary<float, int>();
        dict.Add(1.00f,1);
        string coutryCode = "MX";

        //Act
        var calculate = new Calculate(prices.ToList(),dict,coutryCode);


        //Assert
        var exception = Assert.Throws<Exception>(() => calculate.CalculateChange());
        Assert.Equal("No currency or proces data available",exception.Message);
    }


}