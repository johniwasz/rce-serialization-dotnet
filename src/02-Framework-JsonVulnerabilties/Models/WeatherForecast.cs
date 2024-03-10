namespace JsonVulnerabilities.Api.Models
{
    public class WeatherForecast
    {
        private DateTime _date;
        private int _temperatureC;
        private string _summary;

        public WeatherForecast()
        {

        }

        public WeatherForecast(DateTime Date, int TemperatureC, string Summary)
        {

            _date = Date;
            _temperatureC = TemperatureC;
            _summary = Summary;
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }

        public int TemperatureC
        {
            get
            {
                return _temperatureC;
            }
            set
            {
                _temperatureC = value;
            }
        }

        public string Summary
        {
            get
            {
                return _summary;
            }
            set
            {
                _summary = value;
            }
        }

        public int TemperatureF
        {
            get
            {
                return 32 + (int)(_temperatureC / 0.5556);
            }
        }
    
    }
}
