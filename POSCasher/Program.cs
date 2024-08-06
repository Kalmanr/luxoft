
LocalCurrency localCurrency = LocalCurrency.Instance;

//Currency is configured once
Console.WriteLine("Let's introduce your currency and values, please type values as required:");
Console.WriteLine("CountryCode: "); 
string countryCode = Console.ReadLine() ?? "";
if(String.IsNullOrEmpty(countryCode)) return;
if(localCurrency.GetCurrency(countryCode) == null)
{
    Console.WriteLine("Provide currency values for your region as a list separated by comma ',' For Example: 0.05,0.10:");
    string currencyValues = Console.ReadLine() ?? "";
    float[]? currencyLoad = ValdiateData.isDataValid(currencyValues);
    if(currencyLoad !=null) 
    {
        bool wasSaved = localCurrency.AddCurrencyValues(countryCode,currencyLoad);
        if(wasSaved)
            Console.WriteLine("Currency and values successfully added");
    }
}
Console.WriteLine("Type the prices of purchased products, separated by comma:");
string prices = Console.ReadLine() ?? "";
Console.WriteLine("Type the value of bill/coin received, followed by a comma and the number of bills/coins:");
string moneyReceived = Console.ReadLine() ?? "";

if(String.IsNullOrEmpty(countryCode)  || String.IsNullOrEmpty(prices) || String.IsNullOrEmpty(moneyReceived))
{
    Console.WriteLine("One or more requested values are empty");
    throw new Exception("Starting values in blank");
}


float[]? pricesLoad = ValdiateData.isDataValid(prices) ;
Dictionary<float,int> countedMoney = ValdiateData.CreateBillsCoinsDictionary(moneyReceived);

if(pricesLoad == null) {Console.WriteLine("One or more products have a wrong format"); return;}

Calculate calculate = new Calculate(pricesLoad.ToList(),countedMoney, countryCode);

var response =calculate.CalculateChange();
Console.WriteLine("*************************");
Console.WriteLine("Value   |   Quantity");
if(response != null)
{
    foreach(var item in response)
    {
        Console.WriteLine($"{item.Key}   |   {item.Value}");
    }
}
else{
    throw new Exception("An error occured at calculate change by missing data");
}



