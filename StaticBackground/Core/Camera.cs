using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticBackground.Core
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Vector2 target)
        {
            var offset = Matrix.CreateTranslation(    //to center it around the center of the window not the top left
                            400,
                            240,
                            0);

            var position = Matrix.CreateTranslation(
                            -target.X,
                            -target.Y,
                            0);

            Transform = position * offset;
        }
    }
}
