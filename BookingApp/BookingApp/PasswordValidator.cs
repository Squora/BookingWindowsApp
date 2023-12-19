using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BookingApp
{
    public static class PasswordValidator
    {
        public static Strength PasswordStrength = Strength.Weak;

        public enum Strength
        {
            Weak,
            Medium,
            Strong
        }
        public static Strength CheckStrength(string password)
        {
            Strength strength = Strength.Weak;

            if (IsStrong(password))
            {
                strength = Strength.Strong;
            }
            else if (IsMedium(password))
            {
                strength = Strength.Medium;
            }
            else if (IsWeak(password))
            {
                strength = Strength.Weak;
            }

            return strength;
        }

        private static bool IsWeak(string password)
        {
            return password.Length < 6;
        }

        private static bool IsMedium(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*[a-zA-Z])(?=.*\d).{6,}$");
        }

        private static bool IsStrong(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");
        }
    }
}
