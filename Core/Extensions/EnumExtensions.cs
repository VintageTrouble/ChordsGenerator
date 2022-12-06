using Core.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class EnumExtensions
    {
        public static Note AddInterval(this Note tonic, params Interval[] intervals)
        {
            var result = tonic;
            
            foreach (var interval in intervals)
                result = (Note)((int)result + (int)interval);

            if(result > Note.B)
                result -= (int)Note.B;

            return result;
        }
    }
}
