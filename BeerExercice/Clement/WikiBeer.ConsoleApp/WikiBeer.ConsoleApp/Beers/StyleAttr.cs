namespace WikiBeer.ConsoleApp.Beers
{
    class StyleAttr : Attribute
    {
        // text = Full style name as a string
        internal StyleAttr(string text)
        {
            this.Text = text;
        }

        public string Text { get; private set; }
    }
}