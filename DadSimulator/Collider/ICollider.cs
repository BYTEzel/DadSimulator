namespace DadSimulator.Collider
{
    public interface ICollider
    {
        /// <summary>
        /// Get the point cloud which is stored within the implementing class.
        /// </summary>
        /// <returns>Aligned points.</returns>
        AlignedPointCloud GetAlignedPointCloud();
    }
}
