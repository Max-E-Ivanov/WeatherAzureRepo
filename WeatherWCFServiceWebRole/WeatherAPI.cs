using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Channels;

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

        static string MakeWebRequest(string sRequest)
        {
            try
            {
                HttpWebRequest apiRequest = WebRequest.Create(sRequest) as HttpWebRequest;

                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string apiResponse = reader.ReadToEnd();
                    return apiResponse;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while making request " + ex.Message, ex);
            }
        }


        static string MakeWeatherRequest(string sParams)
        {
            try
            {
                return MakeWebRequest("http://api.wunderground.com/api/" + sAPIKey + "/" + sParams + ".xml");
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
                string sResponse = MakeWeatherRequest( sParams);

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
                string sResponse = MakeWeatherRequest(sParams);

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

        static public List<Tuple<string, string, string>> GetCountriesList()
        {
            try
            {

                List<Tuple<string, string, string>> lsCountries = new List<Tuple<string, string, string>>();

                CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
                foreach (CultureInfo culture in cultures)
                {
                    RegionInfo region = new RegionInfo(culture.Name);

                    if (!lsCountries.Exists( t=> t.Item1 == region.EnglishName))
                    {
                        lsCountries.Add( new Tuple<string, string, string>(region.EnglishName, region.TwoLetterISORegionName, region.NativeName));
                    }
                }

                return new List<Tuple<string, string, string>>(lsCountries.OrderBy(t=>t.Item3));
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting countries list", ex);
            }
        }

        private static string GetClientIP()
        {
            OperationContext context = OperationContext.Current;
            MessageProperties prop = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint =
                   prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            string ip = endpoint.Address;
            return ip;
        }

        static public KeyValuePair<string, string> GetCurrentCity()
        {
            try
            {
                
                string sClientIP = GetClientIP();               
                if (string.IsNullOrEmpty(sClientIP))
                    return new KeyValuePair<string, string>();

                string sResponse = MakeWebRequest(@"http://ip-api.com/xml/" + sClientIP);

                XmlSerializer serializer = new XmlSerializer(typeof(LoactionByIP));

                using (TextReader reader = new StringReader(sResponse))
                {
                    LoactionByIP result = (LoactionByIP)serializer.Deserialize(reader);

                    return new KeyValuePair<string, string>(result.country, result.city);
                }

                
            }
            catch (Exception ex)
            {
                throw new Exception("Error while getting calling user city", ex);
            }
        }
    }
}