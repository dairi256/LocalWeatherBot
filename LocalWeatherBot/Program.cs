namespace LocalWeatherBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sampleData = new TemperaturePredictor.ModelInput()
            {
                Month_Today = 10F, // Month as float
                MinTemp_Today = 9.7F, // Minimum Temperature today
                MaxTemp_Today = 19.4F, // Maximum Temperature today
            };

            var result = TemperaturePredictor.Predict(sampleData);

        }
    }
}
