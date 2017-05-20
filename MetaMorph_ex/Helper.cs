using System;
using System.Linq;

namespace MetaMorph_ex
{
    public class Helper
    {
        /// <summary>
        /// Return a random aschii char
        /// </summary>
        /// <returns></returns>
        public static string GenerateString(int index)
        {
            Random rnd = new Random();
            string txtRand = string.Empty;
            for (int i = 0; i < index; i++) txtRand += ((char)rnd.Next(int.MinValue, int.MaxValue - 20)).ToString();
            return txtRand;
        }
        public static string RandomAZString(int length)
        {
            Random rnd = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray()).ToLower();
        }
    }
}
