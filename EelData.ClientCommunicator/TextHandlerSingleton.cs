namespace EelData.ClientCommunicator
{
    public class TextHandlerSingleton
    {
        private static readonly System.Lazy<TextHandler> lazy;

        static TextHandlerSingleton()
        {
            lazy = new System.Lazy<TextHandler>(() => new TextHandler());
        }

        public static TextHandler Instance
        {
            get
            {
                return lazy.Value;
            }
        }
    }
}
