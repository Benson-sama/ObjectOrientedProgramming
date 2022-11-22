namespace ClassLibrary4
{
    using LP4;

    public class Class1
    {
        [OneParameter]
        public void PrintObject(object obj)
        {
            System.Console.WriteLine(obj);
        }
    }
}
