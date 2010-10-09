namespace KataFizzBuzz
{
    public class FizzBuzz
    {
        public static string FizzOrBuzz(int number)
        {
            string toReturn = number.ToString();
            if (number%3 == 0) toReturn = "Fizz";
            if (number%5 == 0) toReturn = "Buzz";
            if (number%3 == 0 && number%5 == 0) toReturn = "FizzBuzz";
            return toReturn;
        }
    }
}