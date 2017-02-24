namespace Tools
{
    /// <summary>
    /// This class contains members to help convert numbers to strings.
    /// </summary>
    public class Convert
    {
        /// <summary>
        /// Takes a double value-type input and returns a string.
        /// </summary>
        /// <param name="number">the numerical value to be converted</param>
        /// <returns></returns>
        public static string FromDouble(double number)
        {
            return number.ToString();
        }

        /// <summary>
        /// Takes an integer value-type input and returns a string.
        /// </summary>
        /// <param name="number">the numerical value to be converted</param>
        /// <returns></returns>
        public static string FromInteger(int number)
        {
            return number.ToString();
        }

        /// <summary>
        /// Takes two string and concatenates them together into one
        /// </summary>
        /// <param name="string01">The first string</param>
        /// <param name="string02">The second string</param>
        /// <returns>The joined string</returns>
        public static string Concat(string string01, string string02)
        {
            return string01 + string02;
        }

        /// <summary>
        /// Takes two string and concatenates them together into one with a user defined separator
        /// </summary>
        /// <param name="string01"></param>
        /// <param name="string02"></param>
        /// <param name="separator"></param>
        /// <returns>The joined string</returns>
        public static string Join(string string01, string string02, string separator)
        {
            return string01 + separator + string02;
        }

        /// <summary>
        /// Default constructor is not accessible from other assemblies.
        /// </summary>
        internal Convert()
        {

        }
    }
}
