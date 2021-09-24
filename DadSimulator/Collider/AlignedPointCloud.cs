namespace DadSimulator.Collider
{
    public class AlignedPointCloud : AlignedObject
    {
        public PointCloud PointCloud;

        public AlignedPointCloud() : base()
        {
            PointCloud = new PointCloud();
        }
    }
}
