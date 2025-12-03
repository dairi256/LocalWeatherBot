using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace LocalWeatherBot
{
    // This Lag Shift logic class was responsible for transforming the old dataset into the one that we can use now.
    // This file is not needed for the actual bot to run, but kept here for reference.
    internal class LagShiftLogic
    {
        public class RawWeather
        {
            public int Month { get; set; }
            public float Min_Temp { get; set; }
            public float Max_Temp { get; set; }
        }

        public class WeatherTrainingData
        {
            // Features
            public int Month_Today { get; set; }
            public float MinTemp_Today { get; set; }
            public float MaxTemp_Today { get; set; }

            // Label - the outcome that we want to shift and predict
            public float MaxTemp_Tomorrow { get; set; }
        }


        public static void CreateMLNETTrainingData(string inputFilePath, string outputFilePath)
        {
            var rawData = new List<RawWeather>();

            using (TextFieldParser parser = new TextFieldParser(inputFilePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                // Skip the header row (assuming one line header)
                if (!parser.EndOfData) parser.ReadLine();

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    const int MonthIndex = 0;
                    const int MaxTempIndex = 2;
                    const int MinTempIndex = 1;

                    if (fields.Length > MinTempIndex &&
                        int.TryParse(fields[MonthIndex], out int month) &&
                        float.TryParse(fields[MaxTempIndex], NumberStyles.Any, CultureInfo.InvariantCulture, out float maxTemp) &&
                        float.TryParse(fields[MinTempIndex], NumberStyles.Any, CultureInfo.InvariantCulture, out float minTemp))
                    {
                        rawData.Add(new RawWeather
                        {
                            Month = month,
                            Min_Temp = minTemp,
                            Max_Temp = maxTemp
                        });
                    }
                }
            }

            var sb = new StringBuilder();

            sb.AppendLine("Month_Today,MinTemp_Today,MaxTemp_Today,MaxTemp_Tomorrow");

            for (int i = 0; i < rawData.Count - 1; i++)
            {
                var currentDay = rawData[i];
                var nextDay = rawData[i + 1];

                // Formatting for the new .csv
                sb.AppendLine(
                    $"{currentDay.Month}," +
                    $"{currentDay.Min_Temp.ToString(CultureInfo.InvariantCulture)}," +
                    $"{currentDay.Max_Temp.ToString(CultureInfo.InvariantCulture)}," +
                    $"{nextDay.Max_Temp.ToString(CultureInfo.InvariantCulture)}"
                );
            }

            File.WriteAllText(outputFilePath, sb.ToString());
            Console.WriteLine($"Successfully created ML.NET training data file: {outputFilePath}");
        }

    }
}

