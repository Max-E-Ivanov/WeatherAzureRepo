using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WeatherWCFServiceWebRole
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IWeatherServiceWCF
    {
        [OperationContract]
        LoactionByIP GetCurrentCity();

        [OperationContract]
        List<Country> GetCountriesList();

        [OperationContract]
        WeatherDetails GetCurrentWeather(string sCountry, string sCity, string sLang);

        [OperationContract]
        AlmanacDetails GetAlmanacWeather(string sCountry, string sCity, string sLang);
    }

    [ServiceContract]
    public interface IWeatherServiceREST
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Wrapped,
          UriTemplate = "current_city")]
        LoactionByIP GetCurrentCityREST();

        [OperationContract]
        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Wrapped,
          UriTemplate = "countries_list")]
        List<Country> GetCountriesListREST();

        [OperationContract]
        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Wrapped,
          UriTemplate = "current_weather/country/{sCountry}/city/{sCity}/lang/{sLang}")]
        WeatherDetails GetCurrentWeatherREST(string sCountry, string sCity, string sLang);

        [OperationContract]
        [WebInvoke(Method = "GET",
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Wrapped,
          UriTemplate = "almanach_weather/country/{sCountry}/city/{sCity}/lang/{sLang}")]
        AlmanacDetails GetAlmanacWeatherREST(string sCountry, string sCity, string sLang);
    }

    [ServiceContract]
    public interface IWeatherService : IWeatherServiceWCF, IWeatherServiceREST
    {
    }



}
