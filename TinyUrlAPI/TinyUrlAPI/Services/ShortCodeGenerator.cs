namespace TinyUrlAPI.Services
{
    public class ShortCodeGenerator: IShortCodeGenerator
    {
        private const string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const int CodeLength = 6;
        private readonly Random _random = new();


        public ShortCodeGenerator() { }

        public string Generate()
        {
            var code = new char[CodeLength];
            for (int i = 0; i < CodeLength; i++)
            {
                code[i] = Characters[_random.Next(Characters.Length)];
            }
            return new string(code);
        }

    }
}
