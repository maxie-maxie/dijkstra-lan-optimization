namespace FastInputOutput
{
    public class Scanner
    {
        private string[] tokens;
        private int pointer;
        public Scanner()
        {
            tokens = new string[0];
            pointer = 0;
        }
        public string Next()
        {
            while (pointer >= tokens.Length)
            {
                string line = Console.ReadLine();
                if (line == null) return null;
                tokens = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                pointer = 0;
            }
            return tokens[pointer++];
        }
        public int NextInt32()
        {
            return int.Parse(Next());
        }
        public long NextInt64()
        {
            return long.Parse(Next());
        }
    }
}
