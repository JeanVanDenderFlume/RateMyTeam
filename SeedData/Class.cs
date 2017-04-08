using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// https://pastebin.com/XCmbiPSX takes 
/*
 * 
 * <jcotton> >all night
<jcotton> how slow is your machine?
<tttb> well it will take an hour or so i think
<tttb> and it's late
<jcotton> 1000^4 is 1,000 billion
<jcotton> if we assume one loop iteratoin per clock cycle that's about 16 minutes at 1GHz
* nug700 (~nug700@unaffiliated/nug700) has joined ##csharp
<tttb> i have 2.00GHz i3 and it's been more than 16 mins
<jcotton> hm
<jcotton> well
<jcotton> if it's O(n^4) there's not much you can do besides parallelize it
 * */
namespace RateMyTeam.Euler39IntegerRightTriangles
{
    class EulerAlgorithm
    {
        static void StartCalc(string[] args) {
            int countOfFoundTriangles = 0; int key = 0;
            for (int p = 1000; p > 0; p--) {
                if (countOfFoundTriangles > key)
                    key = p;
                Console.WriteLine(countOfFoundTriangles / 2);
                countOfFoundTriangles = 0;
                for (int i = 1; i < 1000; i++) {
                    for (int j = 1; j < 1000; j++) {
                        for (int k = 0; k < 1000; k++) {
                            if (i + j + k == p)
                                if (k * k == i * i + j * j) {
                                    Console.WriteLine("{0}+{1}+{2}={3}", i, j, k, p);
                                    countOfFoundTriangles++;
                                }

                        }
                    }
                }
            }
            Console.WriteLine("Finished, best-perimeter for max int-values is: {0}", key);
            Console.ReadLine();
        }
    }
}