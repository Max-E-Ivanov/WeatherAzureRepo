using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Runtime.Serialization;

namespace WeatherWCFServiceWebRole
{
    #region Current weather
    [Serializable]
    [XmlRoot("response")]
    [DataContract]
    public class WeatherData
    {
        public WeatherData()
        { }
        [DataMember]
        public string version { get; set; }
        [DataMember]
        public string termsofService { get; set; }
        [DataMember]
        public WeatherDetails current_observation { get; set; }

    }

    [Serializable]
    [DataContract]
    public class WeatherDetails
    {
        public WeatherDetails()
        { }
        [DataMember]
        public string observation_time { get; set; }
        [DataMember]
        public Location observation_location { get; set; }
        [DataMember]
        public string local_time_rfc822 { get; set; }
        public DateTime local_time_typed
        {
            get
            {
                return DateTime.Parse(local_time_rfc822);
            }
        }
        [DataMember]
        public string observation_time_rfc822 { get; set; }
        public DateTime observation_time_typed
        {
            get
            {
                return DateTime.Parse(observation_time_rfc822);
            }
        }
        [DataMember]
        public string weather { get; set; }
        [DataMember]
        public double temp_f { get; set; }
        [DataMember]
        public double temp_c { get; set; }
        [DataMember]
        public string relative_humidity { get; set; }
        [DataMember]
        public string wind_dir { get; set; }
        [DataMember]
        public double wind_kph { get; set; }
        [DataMember]
        public double pressure_mb { get; set; }
        [DataMember]
        public double feelslike_f { get; set; }
        [DataMember]
        public double feelslike_c { get; set; }
        [DataMember]
        public double visibility_mi { get; set; }
        [DataMember]
        public double visibility_km { get; set; }
        [DataMember]
        public string solarradiation { get; set; }
        [DataMember]
        public double UV { get; set; }
        [DataMember]
        public string icon_url { get; set; }      
    }

    [Serializable]
    [DataContract]
    public class Location
    {
        public Location()
        { }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string state { get; set; }
        [DataMember]
        public double latitude { get; set; }
        [DataMember]
        public double longitude { get; set; }
        [DataMember]
        public string elevation { get; set; }
    }
    #endregion

    #region MinMax weather
    [Serializable]
    [XmlRoot("response")]
    [DataContract]
    public class AlmanacData
    {
        public AlmanacData()
        { }
        [DataMember]
        public AlmanacDetails almanac { get; set; }
    }

    [Serializable]
    [DataContract]
    public class AlmanacDetails
    {
        public AlmanacDetails()
        { }
        [DataMember]
        public string airport_code { get; set; }
        [DataMember]
        public AlmanacRecord temp_high { get; set; }
        [DataMember]
        public AlmanacRecord temp_low { get; set; }
    }

    [Serializable]
    [DataContract]
    public class AlmanacRecord
    {
        public AlmanacRecord()
        { }
        [DataMember]
        public Record normal { get; set; }
        [DataMember]
        public Record record { get; set; }
        [DataMember]
        public int recordyear { get; set; }
    }

    [Serializable]
    [DataContract]
    public class Record
    {
        public Record()
        { }
        [DataMember]
        public double F { get; set; }
        [DataMember]
        public double C { get; set; }
    }
    #endregion
}