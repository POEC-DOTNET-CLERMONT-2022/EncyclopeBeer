namespace WikiBeer.Models
{
    public enum BeerColor
    {
        white = 1,
        blonde = 2,
        amber = 3,
        brown = 4
    }

    public static class BeerColorExt
    {
        public static string GetString(this BeerColor type)
        {
            switch (type)
            {
                case BeerColor.white: return "White";
                case BeerColor.blonde: return "Blonde";
                case BeerColor.amber: return "Amber";
                case BeerColor.brown: return "Brown";
                default: return "Unknow";
            }
        }
    }
}