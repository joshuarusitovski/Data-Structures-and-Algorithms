using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_8._2HD
{
    class CoinRepresentation
    {
        private static Hashtable _memory = new Hashtable();

        /** ~~~ REFERENCES ~~~
         * [1]      TutorialsEU, “Hash Tables in C# - How to use and when to use?,” 
         *         www.youtube.com, Apr. 23, 2021, https://www.youtube.com/watch?v=f2qQ5hEfaLo&t=750s 
         *         (accessed May 14, 2024).
         * 
         * [2]      D. Badiya, “Minimum number of coins having value equal to powers of 2 required to obtain N,” 
         *         GeeksforGeeks, Jan. 04, 2021. 
         *         https://www.geeksforgeeks.org/minimum-number-of-coins-having-value-equal-to-powers-of-2-required-to-obtain-n/
         *         (accessed May 11, 2024).
         * 
         * [3]      Korsch, J., LaFollette, P., Lipschutz, S. (2003); Loopless Algorithms and
         *         Schroder Trees; <i>International Journal of Computer Mathematics; Vol. 80,
         *         June 2003, pp 709-725; Taylor & Francis
         * /

        /** 
         *  <summary>
         *  Recursively solves the total number of combinations possible for
         *  a limited set of coins (two each) of 2^k denominations and returns
         *  the sum.
         *  
         *  Counts all duplicate solutions as a single solution, so that the total
         *  returned is representative of  the total number of unique combinations to the problem.
         *  
         *  For example, for the sum 6, the set of coins includes:
         *  2^0, 2^1, 2^2, 2^3, 2^4, 2^5 and 2^6, or a set containing
         *  (1, 1, 2, 2, 4, 4, 8, 8, 16, 16, 32, 32, 64, 64)
         *  
         *  The solution optimizes the problem space to ignore coins
         *  that exceed the total. This is achieved by caching of results.
         *  
         *  For the example total of 7, 1 is returned, for
         *  the only combination of:
         *  
         *  4 + 2 + 1. 
         *  </summary>
         *  
         *  <param name="sum">The sum as a long, to find combinations for</param>
         *  <returns>The total number of unique combinations that add up to sum</returns>
         */
        public static long Solve(long sum)
        {
            
            if(sum < 0) return 0;

            /*This acts as the base case identifier; once the recursion allows the input parameter to be equivalent
             * to 0, a single solution has been found, therefor 1 is added to the solution tally, updating the hashtable.          
             */
            if (sum == 0 ) return 1;

            if (!_memory.ContainsKey(sum)) 
            {
                if (sum % 2 == 0 )
                {
                    // Consider this the calaculation of both the floor and ceiling functions given the parameter (sum / 2).  
                    _memory.Add(sum, Solve(sum / 2) + Solve(sum/2 - 1) );
                }
                else
                {
                    /*Should sum mod 2 be equivalent to 1, the recursive function will disregard all even solutions. 
                     * For this to be possible, a single coin of value 2^0 = 1 must be included, as this is the only 
                     * way to ensure that the overall sum can be odd. 
                     */
                    _memory.Add(sum, Solve((sum - 1) / 2)); 
                }
            }

            return Convert.ToInt64(_memory[sum]);
        }
    }
}
