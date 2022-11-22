namespace HexViewer
{
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.IO;

    public static class ExtensionMethods
    {
        /// <summary>
        /// Shortens a path to the specified length by placing "..." between path directory seperator.
        /// </summary>
        /// <param name="path">The path to be shortened.</param>
        /// <param name="length">The desired length of the path.</param>
        /// <returns></returns>
        public static string ShortenPath(this string path, int length)
        {
            if (length >= path.Length)
            {
                return path;
            }

            string[] splittedPath = path.Split(Path.DirectorySeparatorChar);

            if (splittedPath.Length < 3)
            {
                return path;
            }

            string newPath = splittedPath[0] + Path.DirectorySeparatorChar;
            string lastElement = Path.DirectorySeparatorChar + splittedPath[splittedPath.Length - 1];

            int currentLength = newPath.Length + lastElement.Length;

            for (int i = 1; i < splittedPath.Length - 1; i++)
            {
                if (splittedPath[i].Length < length - (currentLength + 3))
                {
                    string newElement = splittedPath[i] + Path.DirectorySeparatorChar;
                    newPath += newElement;
                    currentLength += newElement.Length;
                }
                else
                {
                    newPath += "...";
                    break;
                }
            }

            newPath += lastElement;

            return newPath;
        }

        /// <summary>
        /// Produces an escaped format string based on a given string.
        /// Source: https://stackoverflow.com/questions/323640/can-i-convert-a-c-sharp-string-value-to-an-escaped-string-literal
        /// </summary>
        /// <param name="input">The specified string to convert.</param>
        /// <returns>The escaped format string.</returns>
        public static string ToLiteral(this string input)
        {
            using (var writer = new StringWriter())
            {
                using (var provider = CodeDomProvider.CreateProvider("CSharp"))
                {
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);
                    return writer.ToString();
                }
            }
        }
    }
}
