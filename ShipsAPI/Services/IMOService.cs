namespace ShipsAPI.Services
{
    public static class IMOService
    {
        public static bool IsCorrect(string imo)
        {
            if (string.IsNullOrEmpty(imo) || imo.Length != 7 || !imo.All(char.IsDigit))
                return false;

            int controlSum = 0;
            int multiply = 7;

            // Tylko pierwsze 6 cyfr bierzemy do kontroli
            for (int i = 0; i < 6; i++)
            {
                controlSum += (imo[i] - '0') * multiply;
                multiply--;
            }

            int checkDigit = controlSum % 10;
            int actualDigit = imo[6] - '0';

            return checkDigit == actualDigit;
        }
    }
}
