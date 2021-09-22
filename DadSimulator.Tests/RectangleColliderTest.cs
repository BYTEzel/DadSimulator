using DadSimulator.Collider;
using NUnit.Framework;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DadSimulator.Tests
{
    public class RectangleColliderTest
    {
        [Test]
        public void NoIntersection()
        {
            var pc = new PointCloud() { Id="comparison", Shift=new Vector2(0,0), PointsInOrigin=new List<Point>() { new Point(10,10), new Point(20,40) } };

        }
    }
}
