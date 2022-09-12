namespace RadioArchive
{
    public static class IconTypeExtenstions
    {
        /// <summary>
        /// Converts <see cref="IconType"/> to FontAwesome string
        /// </summary>
        /// <param name="type">the type to convert to</param>
        /// <returns></returns>
        public static string ToFontAwesome(this IconType type)
        {
            //return the FontAwsome string base on given type
            switch (type)
            {
                case IconType.Picture:
                    return "\uf0f6";
                case IconType.File:
                    return "\uf1c5";
                    //if none found return null
                default:
                    return null;
            }
        }
    }
}
