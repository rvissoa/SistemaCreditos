using Azure.Core;

namespace SistemaCreditos
{
    public class Util
    {
        public static DateTime convertirFecha(string date) {
            //Zona horaria
            DateTime timeUtc = Convert.ToDateTime(date).ToUniversalTime();
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            DateTime DateLima = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
            return DateLima;
        }
    }
}
