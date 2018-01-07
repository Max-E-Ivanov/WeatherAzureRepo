using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WeatherWCFServiceWebRole
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class WeatherService : IWeatherService
    {

        public List<Country> GetCountriesList()
        {
            try
            {
                return WeatherAPI.GetCountriesList();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public WeatherDetails GetCurrentWeather(string sCountry, string sCity, string sLang)
        {
            try
            {
                return WeatherAPI.GetCurrentWeather(sCountry, sCity, sLang);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public AlmanacDetails GetAlmanacWeather(string sCountry, string sCity, string sLang)
        {
            try
            {
                return WeatherAPI.GetAlmanacWeather(sCountry, sCity, sLang);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public LoactionByIP GetCurrentCity()
        {
            try
            {
                return WeatherAPI.GetCurrentCity();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public List<Country> GetCountriesListREST()
        {
            try
            {
                return WeatherAPI.GetCountriesList();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public WeatherDetails GetCurrentWeatherREST(string sCountry, string sCity, string sLang)
        {
            try
            {
                return WeatherAPI.GetCurrentWeather(sCountry, sCity, sLang);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public AlmanacDetails GetAlmanacWeatherREST(string sCountry, string sCity, string sLang)
        {
            try
            {
                return WeatherAPI.GetAlmanacWeather(sCountry, sCity, sLang);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public LoactionByIP GetCurrentCityREST()
        {
            try
            {
                return WeatherAPI.GetCurrentCity();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}
