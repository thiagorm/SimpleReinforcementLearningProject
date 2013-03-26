using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SimpleQLearningExample
{
    public class Map
    {
        /*int[,] matriz_tiles = 
        {
            {0,0,0,0,0,0,0,0,0,3},
            {0,0,1,1,1,0,0,0,0,0},
            {0,0,0,0,1,0,0,1,0,0},
            {0,0,0,0,0,1,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,2},
            {0,0,1,0,0,0,0,0,0,1},
        };*/

        int[,] matriz_tiles = 
        {
            {0,0,0,0},
            {0,1,0,2},
            {0,0,0,0},
            {0,0,0,0}
        };

        public int[,] Matriz_tiles
        {
            get { return matriz_tiles; }
        }

        public void Initialize()
        {
        }

        public void Update()
        {
        }

        public void Drawn(SpriteBatch spriteBatch, Texture2D[] texture)
        {
            for (int i = 0; i < matriz_tiles.GetLength(0); i++)
            {
                for (int j = 0; j < matriz_tiles.GetLength(1); j++)
                {
                    if(matriz_tiles[i,j] == 0)
                        spriteBatch.Draw(texture[1], new Vector2(j * 96, i * 96), Color.White);
                    else if (matriz_tiles[i, j] == 1)
                        spriteBatch.Draw(texture[3], new Vector2(j * 96, i * 96), Color.White);
                    else
                        spriteBatch.Draw(texture[2], new Vector2(j * 96, i * 96), Color.White);

                }
            }
        }
    }
}
