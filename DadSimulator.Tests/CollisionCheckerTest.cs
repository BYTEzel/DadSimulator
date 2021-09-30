using DadSimulator.Collider;
using NUnit.Framework;
using Microsoft.Xna.Framework;

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

            var outOfBounds = new AlignedPointCloud
            {
                PointCloud = pointCloud,
                Shift = new Vector2(100, 100)
            };

            Assert.IsFalse(map.IsColliding(noCollision));
            Assert.IsTrue(map.IsColliding(partialCollision));
            Assert.IsTrue(map.IsColliding(collision));
            Assert.IsFalse(map.IsColliding(outOfBounds));
        }


        [Test]
        public void AddPointCloud()
        {
            var map = new CollidableMap(new Size() { Height = 10, Width = 10 });
            var apc = new AlignedPointCloud
            {
                PointCloud = new PointCloud() { PointsInOrigin = { new Point(1, 1), new Point(2, 2) } },
                Shift = new Vector2(7, 7)
            };

            map.AddAlignedPointCloud(apc);
            Assert.IsTrue(map.IsColliding(apc));

            apc.Shift = new Vector2(100, 100);
            Assert.IsFalse(map.IsColliding(apc));
        }

        [TestCase(0, 0, false)]
        [TestCase(1, 0, false)]
        [TestCase(12, 39, false)]
        [TestCase(33, 33, false)]
        [TestCase(43, 10, false)]
        [TestCase(17, 17, true)]
        [TestCase(21, 25, true)]
        [TestCase(32, 31, true)]
        [TestCase(24, 28, true)]
        [TestCase(29, 18, true)]
        public void AddTexture(int shiftX, int shiftY, bool isCollding)
        {
            var texture = GraphicsLoader.LoadTemplateContent(IO.Templates.TestTextureTransparency);
            var map = new CollidableMap(texture);
            var apc = new AlignedPointCloud
            {
                PointCloud = new PointCloud() { PointsInOrigin = { new Point(0, 0) } },
                Shift = new Vector2(shiftX, shiftY)
            };

            Assert.AreEqual(isCollding, map.IsColliding(apc));
        }
    }
}
