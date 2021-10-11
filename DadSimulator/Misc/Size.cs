namespace DadSimulator.Misc
{
    public class Size
    {
        public uint Width;
        public uint Height;

        public Size(uint width, uint height)
        {
            Width = width;
            Height = height;
        }

        public Size(int width, int height)
        {
            Width = (uint)width;
            Height = (uint)height;
        }

        public override string ToString()
        {
            return $"Size({Width}x{Height})";
        }

    }
}
