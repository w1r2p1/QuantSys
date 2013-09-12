﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuantSys.Indicators.Abstraction;
using QuantSys.Indicators.Averages;
using QuantSys.MarketData;

namespace QuantSys.Indicators.Misc
{
    public class CrossoverIndex : AbstractIndicator
    {
        private QSPolyMA POLYMA;
        private SMA SMA;
        public CrossoverIndex(int n) : base(n)
        {
            POLYMA = new QSPolyMA(n);
            SMA = new SMA(n);
        }


        public override double HandleNextTick(Tick t)
        {
            POLYMA.HandleNextTick(t);
            SMA.HandleNextTick(t);

            double[] a = POLYMA.ToArray();
            double[] b = SMA.ToArray();
            double value = 0;

            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] > b[i] && a[i - 1] < b[i - 1]) value++;
                if (a[i] < b[i] && a[i - 1] > b[i - 1]) value++;
            }

            indicatorData.Enqueue(value);
            return value;
        }

        public override string ToString()
        {
            return "CrossNumber" + Period;
        }
    }
}
