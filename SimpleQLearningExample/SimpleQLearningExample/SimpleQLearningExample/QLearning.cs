using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SimpleQLearningExample
{
    public struct actions
    {
        public double n, s, e, w, flag;
    };

    public class QLearning
    {

        Random rnd = new Random();

        int i = 0, j = 0;
        double n_max = 0, s_max = 0, e_max = 0, w_max = 0, maior = -double.MaxValue;
        double r1 = 100, r2 = -100, number_wall = -999;
        double reward = -3;
        int randomico = 0;
        double alfa = 1, gama = 1;
        int count = 0;

        bool condition = false;
        bool walk = true;

        double action_north = 0;
        double action_south = 0;
        double action_east = 0;
        double action_west = 0;

        //Matrizes usadas no desenvolvimento do algoritmo
        double[,] matriz = new double[4, 4];
        actions[,] enviroment = new actions[4, 4];
        actions[,] visitas = new actions[4, 4];
        double[,] matriz_caminho = new double[4, 4];

        Point start = new Point(250, 278);//new Point(442, 278);        
        Map map = new Map();

        Vector2 playerPosition = Vector2.Zero;
        int elipsedTime = 0;
        int totalTime = 1;
        KeyboardState oldState;

        public QLearning()
        {
        }

        public void Initialize()
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    enviroment[i, j].n = 0;
                    enviroment[i, j].s = 0;
                    enviroment[i, j].e = 0;
                    enviroment[i, j].w = 0;
                    enviroment[i, j].flag = 0;
                    if (map.Matriz_tiles[i, j] == 1)
                    {
                        enviroment[i, j].n = number_wall;
                        enviroment[i, j].s = number_wall;
                        enviroment[i, j].e = number_wall;
                        enviroment[i, j].w = number_wall;
                        enviroment[i, j].flag = 0;
                    }
                    else if (map.Matriz_tiles[i, j] == 2)
                    {
                        enviroment[i, j].n = r1;
                        enviroment[i, j].s = r1;
                        enviroment[i, j].e = r1;
                        enviroment[i, j].w = r1;
                        enviroment[i, j].flag = 1;
                    }
                    else if (map.Matriz_tiles[i, j] == 3)
                    {
                        enviroment[i, j].n = r2;
                        enviroment[i, j].s = r2;
                        enviroment[i, j].e = r2;
                        enviroment[i, j].w = r2;
                        enviroment[i, j].flag = 1;
                    }
                }
            }

        }

        private void calculateAIQlearning()
        {


            if (condition)
            {
                walkAI();
            }

            if (!condition)
            {
                randomico = rnd.Next(1, 5);

                if (randomico == 1)//n         
                {

                    if (i - 1 < 0)
                    {
                        walk = false;
                    }
                    else if (enviroment[i - 1, j].s == number_wall)
                    {
                        walk = false;
                    }
                    else
                    {
                        playerPosition -= new Vector2(0, 96);

                        n_max = enviroment[i - 1, j].n;
                        s_max = enviroment[i - 1, j].s;
                        e_max = enviroment[i - 1, j].e;
                        w_max = enviroment[i - 1, j].w;


                        verifyMaiorAction(n_max, s_max, e_max, w_max);


                        enviroment[i, j].n = enviroment[i, j].n + (alfa * (reward + (gama * maior) - enviroment[i, j].n));

                        if (enviroment[i - 1, j].n == 100 || enviroment[i - 1, j].n == -100 && enviroment[i - 1, j].s == 100 || enviroment[i - 1, j].s == -100
                            && enviroment[i - 1, j].e == 100 || enviroment[i - 1, j].e == -100 && enviroment[i - 1, j].w == 100 || enviroment[i - 1, j].w == -100)
                        {
                            count++;
                            walk = false;
                            i = 0;
                            j = 0;
                            playerPosition = Vector2.Zero;
                        }
                    }
                }
                else if (randomico == 2)//s
                {

                    if (i + 1 >= matriz.GetLength(0))
                    {
                        walk = false;
                    }
                    else if (enviroment[i + 1, j].n == number_wall)
                    {
                        walk = false;
                    }
                    else
                    {

                        playerPosition += new Vector2(0, 96);

                        n_max = enviroment[i + 1, j].n;
                        s_max = enviroment[i + 1, j].s;
                        e_max = enviroment[i + 1, j].e;
                        w_max = enviroment[i + 1, j].w;



                        verifyMaiorAction(n_max, s_max, e_max, w_max);

                        enviroment[i, j].s = enviroment[i, j].s + (alfa * (reward + (gama * maior) - enviroment[i, j].s));

                        if (enviroment[i + 1, j].n == 100 || enviroment[i + 1, j].n == -100 && enviroment[i + 1, j].s == 100 || enviroment[i + 1, j].s == -100
                            && enviroment[i + 1, j].e == 100 || enviroment[i + 1, j].e == -100 && enviroment[i + 1, j].w == 100 || enviroment[i + 1, j].w == -100)
                        {
                            count++;
                            walk = false;
                            i = 0;
                            j = 0;
                            playerPosition = Vector2.Zero;
                        }
                    }
                }
                else if (randomico == 3)//e  
                {

                    if (j + 1 >= matriz.GetLength(1))
                    {
                        walk = false;
                    }
                    else if (enviroment[i, j + 1].w == number_wall)
                    {
                        walk = false;
                    }
                    else
                    {
                        playerPosition += new Vector2(96, 0);

                        n_max = enviroment[i, j + 1].n;
                        s_max = enviroment[i, j + 1].s;
                        e_max = enviroment[i, j + 1].e;
                        w_max = enviroment[i, j + 1].w;

                        verifyMaiorAction(n_max, s_max, e_max, w_max);

                        enviroment[i, j].e = enviroment[i, j].e + (alfa * (reward + (gama * maior) - enviroment[i, j].e));

                        if (enviroment[i, j + 1].n == 100 || enviroment[i, j + 1].n == -100 && enviroment[i, j + 1].s == 100 || enviroment[i, j + 1].s == -100
                            && enviroment[i, j + 1].e == 100 || enviroment[i, j + 1].e == -100 && enviroment[i, j + 1].w == 100 || enviroment[i, j + 1].w == -100)
                        {
                            count++;
                            walk = false;
                            i = 0;
                            j = 0;
                            playerPosition = Vector2.Zero;
                        }
                    }

                }
                else if (randomico == 4)//w
                {
                    if (j - 1 < 0)
                    {
                        walk = false;
                    }
                    else if (enviroment[i, j - 1].e == number_wall)
                    {
                        walk = false;
                    }
                    else
                    {

                        playerPosition -= new Vector2(96, 0);

                        n_max = enviroment[i, j - 1].n;
                        s_max = enviroment[i, j - 1].s;
                        e_max = enviroment[i, j - 1].e;
                        w_max = enviroment[i, j - 1].w;


                        verifyMaiorAction(n_max, s_max, e_max, w_max);

                        enviroment[i, j].w = enviroment[i, j].w + (alfa * (reward + (gama * maior) - enviroment[i, j].w));

                        if (enviroment[i, j - 1].n == 100 || enviroment[i, j - 1].n == -100 && enviroment[i, j - 1].s == 100 || enviroment[i, j - 1].s == -100
                            && enviroment[i, j - 1].e == 100 || enviroment[i, j - 1].e == -100 && enviroment[i, j - 1].w == 100 || enviroment[i, j - 1].w == -100)
                        {
                            count++;
                            walk = false;
                            i = 0;
                            j = 0;
                            playerPosition = Vector2.Zero;
                        }
                    }
                }


                if (walk)
                {
                    switch (randomico)
                    {
                        case 1: i--;
                            break;
                        case 2: i++;
                            break;
                        case 3: j++;
                            break;
                        case 4: j--;
                            break;
                    }
                }

                if (!walk)
                    walk = true;

                if (count == 10000)
                    condition = true;
            }
        }

        public void verifyMaiorAction(double n_max, double s_max, double e_max, double w_max)
        {

            double[] max_value = new double[4];
            max_value[0] = n_max;
            max_value[1] = s_max;
            max_value[2] = e_max;
            max_value[3] = w_max;

            maior = -double.MaxValue;

            for(int i = 0; i < 4; i++)
            {
                if (max_value[i] > maior)
                    maior = max_value[i];
            }

            /*if (n_max >= s_max && n_max >= e_max && n_max >= w_max)
                maior = n_max;
            else if (s_max >= n_max && s_max >= e_max && s_max >= w_max)
                maior = s_max;
            else if (e_max >= n_max && e_max >= s_max && e_max >= w_max)
                maior = e_max;
            else if (w_max >= n_max && w_max >= s_max && w_max >= e_max)
                maior = w_max;*/
        }

        public void walkAI()
        {
            //condition = false;

            if (enviroment[i, j].n == 100 && enviroment[i, j].s == 100 && enviroment[i, j].e == 100 && enviroment[i, j].w == 100)
            {
                i = 0;
                j = 0;
                playerPosition = Vector2.Zero;
                //condition = true;
            }
            else if (enviroment[i, j].n > enviroment[i, j].s && enviroment[i, j].n > enviroment[i, j].e && enviroment[i, j].n > enviroment[i, j].w)
            {
                //if (enviroment[i, j].n > 0)
                //{
                    playerPosition -= new Vector2(0, 96);
                    i--;
                    //condition = true;
                //}
            }
            else if (enviroment[i, j].s > enviroment[i, j].n && enviroment[i, j].s > enviroment[i, j].e && enviroment[i, j].s > enviroment[i, j].w)
            {
                //if (enviroment[i, j].s > 0)
                //{
                    playerPosition += new Vector2(0, 96);
                    i++;
                    //condition = true;
                //}
            }
            else if (enviroment[i, j].e > enviroment[i, j].n && enviroment[i, j].e > enviroment[i, j].s && enviroment[i, j].e > enviroment[i, j].w)
            {
                //if (enviroment[i, j].e > 0)
                //{
                    playerPosition += new Vector2(96, 0);
                    j++;
                    //condition = true;
                //}
            }
            else if (enviroment[i, j].w > enviroment[i, j].n && enviroment[i, j].w > enviroment[i, j].s && enviroment[i, j].w > enviroment[i, j].e)
            {
                //if (enviroment[i, j].w > 0)
                //{
                    playerPosition -= new Vector2(96, 0);
                    j--;
                    //condition = true;
                //}
            }
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            this.elipsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
          

            if (elipsedTime > totalTime)
            {
            //if(keyboard.IsKeyDown(Keys.Space) && !oldState.IsKeyDown(Keys.Space))
                calculateAIQlearning();
                elipsedTime = 0;
            }
            oldState = keyboard;
        }

        public void Drawn(SpriteBatch spriteBatch, Texture2D[] texture)
        {
            for (int i = 0; i < map.Matriz_tiles.GetLength(0); i++)
            {
                for (int j = 0; j < map.Matriz_tiles.GetLength(1); j++)
                {
                    spriteBatch.Draw(texture[0], playerPosition, Color.White);
                }
            }
        }

        public void DrawnDebug(SpriteBatch spriteBatch, Texture2D[] texture, SpriteFont font)
        {
            spriteBatch.DrawString(font, " Direcao: " + randomico.ToString(), new Vector2(0, 600), Color.White);
            spriteBatch.DrawString(font, " i: " + this.i.ToString(), new Vector2(0, 620), Color.White);
            spriteBatch.DrawString(font, " j: " + this.j.ToString(), new Vector2(0, 640), Color.White);

            spriteBatch.DrawString(font, " action_north: " + action_north, new Vector2(400, 600), Color.White);
            spriteBatch.DrawString(font, " action_south: " + action_south, new Vector2(400, 620), Color.White);
            spriteBatch.DrawString(font, " action_east: " + action_east, new Vector2(400, 640), Color.White);
            spriteBatch.DrawString(font, " action_west: " + action_west, new Vector2(400, 660), Color.White);
            spriteBatch.DrawString(font, " count: " + count.ToString(), new Vector2(400, 680), Color.White);

            for (int i = 0; i < map.Matriz_tiles.GetLength(0); i++)
            {
                for (int j = 0; j < map.Matriz_tiles.GetLength(1); j++)
                {
                    spriteBatch.DrawString(font, enviroment[i, j].n.ToString(), new Vector2((j * 96) + 46, (i * 96)), Color.Red);
                    spriteBatch.DrawString(font, enviroment[i, j].w.ToString(), new Vector2((j * 96), (i * 96) + 32), Color.Red);
                    spriteBatch.DrawString(font, enviroment[i, j].e.ToString(), new Vector2((j * 96) + 80, (i * 96) + 32), Color.Red);
                    spriteBatch.DrawString(font, enviroment[i, j].s.ToString(), new Vector2((j * 96) + 46, (i * 96) + 70), Color.Red);
                }
            }
        }
    }
}
