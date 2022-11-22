namespace ClassLibrary3
{
    using LP4;

    public class Class1
    {
        [OneParameter]
        public Rectangle MakeSquare(int length)
        {
            return new Rectangle(length);
        }
    }
}
