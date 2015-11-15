using System;

namespace ImgHoster.Static_Classes
{
    
    public static class Base62
    {
        private static string ALPHANUMERIC_ALT =
         "0123456789" +
         "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
         "abcdefghijklmnopqrstuvwxyz";

        public static string ToBase(this long input)
        {
            string r = string.Empty;
            int targetBase = ALPHANUMERIC_ALT.Length;
            do
            {
                r = string.Format("{0}{1}",
                    ALPHANUMERIC_ALT[(int)(input % targetBase)],
                    r);
                input /= targetBase;
            } while (input > 0);

            return r;
        }

        public static long FromBase(this string input)
        {
            //we need to reverse input
            char[] arr = input.ToCharArray();
            Array.Reverse(arr);
            string r = new string(arr);

            int srcBase = ALPHANUMERIC_ALT.Length;
            long id = 0;

            for (int i = 0; i < r.Length; i++)
            {
                int charIndex = ALPHANUMERIC_ALT.IndexOf(r[i]);
                id += charIndex * (long)Math.Pow(srcBase, i);
            }

            return id;
        }
    }

}