using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_6._2D
{
    public class BoxOfCoins
    {

        public static int Solve(int[] boxes)
        {
            if (boxes == null)
            {
                throw new ArgumentNullException(); 
            }

            if (boxes.Length == 1)
            {
                return boxes[0];
            }


            int N = boxes.Length;
            int i, j, x, y, z;
            int[,] outcome = new int[N, N];


            for (int gap = 0; gap < N; gap++)
            {
                for ( i = 0, j = gap; j < N; i++, j++)
                {
                    if ((i + 2) <= j)
                    {
                        x = outcome[i + 2, j]; 
                    }
                    else x = 0;

                    if ((i + 1) <= (j - 1))
                    {
                        y = outcome[i + 1, j - 1];
                    }
                    else y = 0;

                    if (i <= (j - 2))
                    {
                        z = outcome[i, j - 2];
                    }
                    else z = 0;
                      

                    outcome[i, j] = Math.Max(boxes[i] + Math.Min(x, y),
                                        boxes[j] + Math.Min(y, z));
                }
            }

            int result = Math.Max(outcome[0, N - 1] - outcome[1, N - 1], outcome[0, N - 1] - outcome[0, N - 2]);
            return result; 


        }

    }
}
