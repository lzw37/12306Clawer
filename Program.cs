using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crawing
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetTrainList gt = new GetTrainList();
            //gt.ReadFile();
            //GetTrainInfo gti = new GetTrainInfo();
            //gti.GetMethod();
            JSONSolver jss = new JSONSolver();
            jss.Solve();
        }
    }
}
