﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DadSimulator.Collider
{
    public class TextureCollider : ICollider
    {
        private PointCloud m_pc;
        public TextureCollider(Color[,] texture)
        {
            var m_pc = new PointCloud();
            LocateNonTransparentPixel(texture);
        }

        private void LocateNonTransparentPixel(Color[,] texture)
        {
            for (int x = 0; x < texture.GetLength(0); x++)
            {
                for (int y = 0; y < texture.GetLength(1); y++)
                {
                    if (texture[x, y].A != 0)
                    {
                        m_pc.PointsInOrigin.Add(new Point(x, y));
                    }
                }
            }
        }

        public PointCloud GetPointCloud()
        {
            return m_pc;
        }
    }
}
