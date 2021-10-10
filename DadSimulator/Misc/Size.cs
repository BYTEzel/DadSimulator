namespace DadSimulator.Misc
{
    public class Size
    {
        public uint Width;
        public uint Height;

        public Size(uint Width, uint Height)
        {
            this.Width = Width;
            this.Height = Height;
        }

        public override string ToString()
        {
            return $"Size({Width}x{Height})";
        }

    }
}
