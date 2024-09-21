using System;

namespace tomograf.metaheurystyki
{
    class ABCAlgorithm
    {
        //argumenty metody
        int SN;
        int d;
        int lim;
        int MCN;

        double[] a;
        double[] b;

        public delegate void Delegata(ABCAlgorithm o);
        public event Delegata PrintResults;

        Func<double[], double> f;

        public static void kom()
        {
            Console.WriteLine("komunikat");
        }

        //zmienne programu
        double[,] source;
        double[] Fs;
        Random r;
        double[] fit;
        double[] P;
        int indBest;
        double[] xBest;
        double fBest;
        bool[] korekta;

        double fGlobalBest;
        double[] xGlobalBest;



        public ABCAlgorithm(int SN, int MCN, Func<double[], double> f, double[] a, double[] b)
        {
            this.SN = SN;
            this.MCN = MCN;
            this.f = f;
            this.a = a;
            this.b = b;
            this.d = a.Length;
            this.lim = SN * d;
            this.source = new double[SN, d];
            this.Fs = new double[SN];
            this.r = new Random();
            this.fit = new double[SN];
            this.P = new double[SN];
            this.xBest = new double[d];
            this.xGlobalBest = new double[d];
            this.korekta = new bool[SN];


        }

        //rozmieszczenie źródeł i wyznaczenie ich wartości


        private void Init()
        {
            //poczatkowe rozmieszczenie źródeł
            double[] xTmp = new double[this.d];
            for (int i = 0; i < this.SN; i++)
            {
                //i-ty punkt źródłowy

                for (int j = 0; j < d; j++)
                {
                    this.source[i, j] = this.a[j] + r.NextDouble() * (this.b[j] - this.a[j]);
                    xTmp[j] = this.source[i, j];
                }
                Fs[i] = f(xTmp);
                this.korekta[i] = false;
            }

            fGlobalBest = Fs[0];
            for (int i = 0; i < d; i++)
            {
                xGlobalBest[i] = source[0, i];
            }
        }

        private void ChosenBest()
        {
            bool zmiana = false;



            for (int i = 0; i < this.SN; i++)
            {
                if (Fs[i] < fGlobalBest)
                {
                    indBest = i;
                    fGlobalBest = Fs[i];
                    zmiana = true;
                }
            }
            if (zmiana)
            {
                for (int i = 0; i < d; i++)
                    xGlobalBest[i] = this.source[indBest, i];
            }
        }

        private void SourceModification()
        {
            int k;
            double fTemp;
            double[] xTemp = new double[this.d];

            for (int i = 0; i < lim; i++)
            {
                for (int j = 0; j < this.SN; j++)
                {
                    //wylosowanie k różnego od 
                    while ((k = r.Next(1, SN)) == j) ;

                    for (int l = 0; l < this.d; l++)
                    {
                        double nowy = source[j, l] + (2.0 * r.NextDouble() - 1.0) * (source[j, l] - source[k, l]);
                        if (nowy > a[l])
                        {
                            if (nowy < b[l])
                                xTemp[l] = nowy;
                            else
                                xTemp[l] = b[l];
                        }
                        else
                            xTemp[l] = a[l];
                    }
                    fTemp = f(xTemp);

                    if (fTemp < Fs[j])
                    {
                        //podmiana j-tego źródła na lepsze
                        for (int l = 0; l < d; l++)
                            this.source[j, l] = xTemp[l];
                        this.Fs[j] = fTemp;
                        this.korekta[j] = true;
                    }


                }
            }
        }

        private void Fit()
        {
            double sFit = 0.0;

            for (int i = 0; i < this.SN; i++)
            {
                if (Fs[i] >= 0)
                {
                    fit[i] = 1.0 / (1.0 + Fs[i]);
                }
                else
                    fit[i] = 1 - Fs[i];

                sFit += fit[i];
            }
            //wyzaczenie prawdopodobieństw
            for (int i = 0; i < this.SN; i++)
            {
                P[i] = fit[i] / sFit;
            }

        }

        private int ChosenSource()
        {
            //wygenerowanie przedzialow
            double[] podzial = new double[this.SN];
            podzial[0] = 0;
            for (int i = 1; i < this.SN; i++)
                podzial[i] = podzial[i - 1] + P[i - 1];
            double a = r.NextDouble();
            int ind = 0;
            while (a > podzial[ind])
                if (ind < this.SN - 1)
                {
                    ind++;
                }
                else a = 0;
            return ind;
        }

        private void SourceModificationNext()
        {
            int k, ind;
            double[] xTemp = new double[this.d];
            double fTemp;

            for (int i = 0; i < this.SN; i++)
            {
                ind = ChosenSource();

                while ((k = r.Next(1, SN)) == ind) ;

                for (int l = 0; l < this.d; l++)
                {
                    double nowy = source[ind, l] + (2.0 * r.NextDouble() - 1.0) * (source[ind, l] - source[k, l]);
                    if (nowy > a[l])
                    {
                        if (nowy < b[l])
                            xTemp[l] = nowy;
                        else
                            xTemp[l] = b[l];
                    }
                    else
                        xTemp[l] = a[l];
                }
                fTemp = f(xTemp);

                if (fTemp < Fs[ind])
                {
                    for (int l = 0; l < this.d; l++)
                        source[ind, l] = xTemp[l];
                    Fs[ind] = fTemp;
                    this.korekta[ind] = true;
                }

            }
        }

        private void Correct()
        {
            //zakres zmiennych
            double[] min = new double[d];
            double[] max = new double[d];
            double[] xTemp = new double[d];


            for (int i = 0; i < d; i++)
            {
                min[i] = source[0, i];
                max[i] = source[0, i];
            }

            for (int i = 0; i < SN; i++)
            {
                for (int j = 0; j < d; j++)
                {
                    if (min[j] > source[i, j])
                        min[j] = source[i, j];

                    if (max[j] < source[i, j])
                        max[j] = source[i, j];

                }

            }


            //dla nie modyfikowanych źródeł nowe ich losowanie
            for (int i = 0; i < this.SN; i++)
            {

                if (!korekta[i])
                {
                    for (int l = 0; l < this.d; l++)
                    {
                        this.source[i, l] = min[l] + r.NextDouble() * (max[l] - min[l]);
                        xTemp[l] = this.source[i, l];
                    }
                    Fs[i] = f(xTemp);
                }
            }

        }

        public void Solve()
        {
            Init();
            ChosenBest();

            for (int i = 0; i < this.MCN; i++)
            {
                //Console.WriteLine("ITERACJA NR " + i);
                SourceModification();
                Fit();
                SourceModificationNext();
                //Correct();
                ChosenBest();


                //PrintResults(this);
                //zapiszDoPliku("WYN.txt");
                Console.WriteLine(FBest);
            }
            zapiszDoPliku("ABCEND.txt");
        }

        public void zapiszDoPliku(string nazwa)
        {
            string[] dane_do_zapisu = new string[d + 2];
            for (int i = 0; i < d; i++)
                dane_do_zapisu[i] = xGlobalBest[i].ToString();
            dane_do_zapisu[d] = fGlobalBest.ToString();
            dane_do_zapisu[d + 1] = " ";
            System.IO.File.AppendAllLines(@nazwa, dane_do_zapisu);
        }

        public double[] XBest
        {
            get { return xGlobalBest; }
        }
        public double FBest
        {
            get { return fGlobalBest; }
        }
        public double[,] Source
        {
            get { return source; }
        }
        public int Sn
        {
            get { return SN; }
        }
        public int D
        {
            get { return d; }
        }
    }
}