using DadSimulator.Collider;
using NUnit.Framework;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DadSimulator.Tests
{
    class CollisionCheckerTest
    {
        [Test]
        public void CreateMap()
        {
            var size = new Size() { Height = 10, Width = 10 };
            CollidableMap map = new CollidableMap(size);

            var nonColliding = new AlignedPointCloud()
            {
                PointCloud = new PointCloud() { PointsInOrigin = { new Point(1, 1) } },
                Shift = Vector2.Zero
            };

            Assert.IsFalse(map.IsColliding(nonColliding));
        }

        [Test]
        public void AddRectangleExceptions()
        {
            CollidableMap map = new CollidableMap(new Size() { Height = 10, Width = 10 });
            Assert.DoesNotThrow(()=> map.AddRectangle(new Rectangle(0, 0, 5, 5)));
            Assert.Throws<System.ArgumentOutOfRangeException>(() => map.AddRectangle(new Rectangle(0, 0, 100, 100)));
        }

        [Test]
        public void AddRectangle()
        {
            CollidableMap map = new CollidableMap(new Size() { Height = 10, Width = 10 });
            map.AddRectangle(new Rectangle(0, 0, 5, 5));

            var pointCloud = new PointCloud
            {
                PointsInOrigin = { new Point(1, 1), new Point(2, 2) }
            };

            var noCollision = new AlignedPointCloud
            {
                PointCloud = pointCloud,
                Shift = new Vector2(7, 7)
            };

            var partialCollision = new AlignedPointCloud
            {
                PointCloud = pointCloud,
                Shift = new Vector2(3, 3)
            };

            var collision = new AlignedPointCloud
            {
                PointCloud = pointCloud,
                Shift = Vector2.Zero
            };

            Assert.IsFalse(map.IsColliding(noCollision));
            Assert.IsTrue(map.IsColliding(partialCollision));
            Assert.IsTrue(map.IsColliding(collision));

        }


        [Test]
        public void AddPointCloud()
        {

        }

        [Test]
        public void AddTexture()
        {

        }
    }
}
