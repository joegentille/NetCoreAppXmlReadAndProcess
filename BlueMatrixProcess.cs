using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReadXML
{
    public class BlueMatrixProcess
    {
        
        public void Execute()
        {
            //var startDate = 
            var jsonFile = File.ReadAllText("data.json");
            var jsonFileParse = JObject.Parse(jsonFile);
            var startDate = jsonFileParse.SelectToken("startDate").Value<string>();
            



        }

        private void ValidateDatesValues(string startDate, string endDate)
        {
            if (string.IsNullOrWhiteSpace(startDate))
            {
                
            }
        }


    }
}
