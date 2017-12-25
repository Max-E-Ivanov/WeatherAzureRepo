using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Xml.Serialization;

namespace WeatherWCFServiceWebRole
{
    public class CityCountryNameException : ApplicationException
    {
        public CityCountryNameException(string sMessage) : base(sMessage)
        { }
    }

    public static class WeatherAPI
    {
        static string sAPIKey = "5692fbc0aad1c262";

      

        static string MakeRequest(string sParams)
        {
            try
            {
                HttpWebRequest apiRequest = WebRequest.Create("http://api.wunderground.com/api/"+sAPIKey+"/"+sParams + ".xml") as HttpWebRequest;

                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string apiResponse = reader.ReadToEnd();
                    return apiResponse;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while making request to wunderground.com. "+ex.Message, ex);
            }
        }


        static public WeatherDetails GetCurrentWeather(string sCountry, string sCity, string sLang)
        {
            if (string.IsNullOrEmpty(sCountry) || string.IsNullOrEmpty(sCity))
            {
                throw new CityCountryNameException(sCountry + @"/" + sCity);
            }

            try
            {               
                string sParams = @"conditions"+ (string.IsNullOrEmpty(sLang)?"": "/lang:"+sLang)+"/q/"+sCountry+"/"+sCity;
                string sResponse = MakeRequest( sParams);

                XmlSerializer serializer = new XmlSerializer(typeof(WeatherData));
                
                using (TextReader reader = new StringReader(sResponse))
                {
                    WeatherData result = (WeatherData)serializer.Deserialize(reader);

                    return result.current_observation;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting current weather for "+sCountry + "/"+sCity, ex);
            }
        }

        static public AlmanacDetails GetAlmanacWeather(string sCountry, string sCity, string sLang)
        {
            if (string.IsNullOrEmpty(sCountry) || string.IsNullOrEmpty(sCity))
            {
                throw new CityCountryNameException(sCountry + @"/" + sCity);
            }

            try
            {
                string sParams = @"almanac" + (string.IsNullOrEmpty(sLang) ? "" : "/lang:" + sLang) + "/q/" + sCountry + "/" + sCity;
                string sResponse = MakeRequest(sParams);

                XmlSerializer serializer = new XmlSerializer(typeof(AlmanacData));

                using (TextReader reader = new StringReader(sResponse))
                {
                    AlmanacData result = (AlmanacData)serializer.Deserialize(reader);

                    return result.almanac;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting weather almanac for " + sCountry + "/" + sCity, ex);
            }
        }
    }
}