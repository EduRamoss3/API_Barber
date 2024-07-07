
using System.ComponentModel.DataAnnotations;

namespace Barber.Application.DefaultValues
{
    public static class HourServiceTimeDefault
    {
        [Required]
        [MinLength(10,ErrorMessage =" Min 10 minutes!")]
        [MaxLength(60, ErrorMessage =" Max 60 minutes!")]
        public static int DefaultMinutes = 30;

        public static void SetDefaultMinutes(int defaultMinutes)
        {
            DefaultMinutes = defaultMinutes;
        }
       
    }
}
