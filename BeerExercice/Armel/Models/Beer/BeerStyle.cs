namespace WikiBeer.Models
{
    public enum BeerStyle
    {
        ipa = 1, 
        session_ipa = 2,
        pale_ale = 3
    }

    public static class BeerStylesExt
    {
        public static string GetString(this BeerStyle type)
        {
            switch (type)
            {
                case BeerStyle.ipa: return "IPA";
                case BeerStyle.session_ipa: return "Session IPA";
                case BeerStyle.pale_ale: return "Pale Ale";
                default: return "Inconnue";
            }
        }
    }

}