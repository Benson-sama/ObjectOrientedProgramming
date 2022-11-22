namespace ClassLibrary2
{
    using LP4;

    public class Class1
    {
        [OneParameter]
        public static int Convert(string s)
        {
            int result = 0;

            foreach (var character in s)
            {
                result += (System.Convert.ToInt32(character)) % 3;
            }

            return result;
        }
    }
}
