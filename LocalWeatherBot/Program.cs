namespace LocalWeatherBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Keep this code if you want to regenerate the training data from raw data
            // string inputFile = "rawData.csv";
            // string outputFile = "MLNET_Weather_Training.csv";
            // LagShiftLogic.CreateMLNETTrainingData(inputFile, outputFile);
            // The csv file that you use for training saves in the 'bin/Debug/net10.0' folder of the project.

            //Load sample data
            var sampleData = new TemperaturePredictor.ModelInput()
            {
                Month_Today = 10F, // The month that we are in currently (1-12)
                MinTemp_Today = 9.7F, // The minimum temperature recorded today
                MaxTemp_Today = 19.4F, // The maximum temperature recorded today
            };

            //Load model and predict output
            var result = TemperaturePredictor.Predict(sampleData);

        }
    }
}
