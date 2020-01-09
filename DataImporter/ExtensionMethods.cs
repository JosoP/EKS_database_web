namespace DataImporter
{
    /// <summary>
    ///     Class in which all extension methods of program are stored.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        ///     Extension method that adds to string class functionality to remove all occurrences of text between two
        ///     specified characters, including specified characters.
        /// </summary>
        /// <param name="str">String on which this method is called.</param>
        /// <param name="startCharacter">Starting character which starts text that will be removed.</param>
        /// <param name="endCharacter">Ending character which ends text that will be removed.</param>
        /// <returns>String in which text between specified characters is removed.</returns>
        public static string RemoveBetweenIncluding(this string str, char startCharacter, char endCharacter)
        {
            var isDeletingActive = false;
            var editedStr = "";

            for (int i = 0; i < str.Length; i++)
            {
                var currentChar = str[i];

                if (currentChar == startCharacter)
                {
                    isDeletingActive = true;
                }

                if (isDeletingActive == false)
                {
                    editedStr += str[i]; //copy character
                }

                if (currentChar == endCharacter)
                {
                    isDeletingActive = false;
                }
            }

            return editedStr;
        }
    }
}