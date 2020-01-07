namespace DataImporter
{
    public static class ExtensionMethods
    {
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

        //public static string RemoveDuplicities(this string str, string )
    }
}