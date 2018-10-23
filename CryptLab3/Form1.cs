using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Numerics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptLab3
{
    public partial class Form1 : Form
    {
        List<Panel> PanelControlList = new List<Panel>();
        List<Tuple<long, long, long>> equations = new List<Tuple<long, long, long>>();
        int index = 1;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            equations.Add(new Tuple<long, long, long>(18, 2, 29));
            equations.Add(new Tuple<long, long, long>(166, 7, 433));
            equations.Add(new Tuple<long, long, long>(7531, 6, 8101));
            equations.Add(new Tuple<long, long, long>(525, 3, 809));
            equations.Add(new Tuple<long, long, long>(12, 7, 41));
            equations.Add(new Tuple<long, long, long>(70, 2, 131));
            equations.Add(new Tuple<long, long, long>(525, 2, 809));
            equations.Add(new Tuple<long, long, long>(525, -2, 131));
            PanelControlList.Add(panel1);
            PanelControlList.Add(panel2);
            PanelControlList.Add(panel3);
            PanelControlList.Add(panel4);
            foreach (var X in PanelControlList)
            {
                X.Visible = false;
            }
            PanelControlList[index - 1].Visible = true; 
            PanelControlList[index - 1].BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            index = 1;
            foreach (var X in PanelControlList)
            {
                X.Visible = false;
            }
            PanelControlList[index - 1].Visible = true;
            PanelControlList[index - 1].BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            index = 2;
            foreach (var X in PanelControlList)
            {
                X.Visible = false;
            }
            PanelControlList[index - 1].Visible = true;
            PanelControlList[index - 1].BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            index = 3;
            foreach (var X in PanelControlList)
            {
                X.Visible = false;
            }
            PanelControlList[index - 1].Visible = true;
            PanelControlList[index - 1].BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            index = 4;
            foreach (var X in PanelControlList)
            {
                X.Visible = false;
            }
            PanelControlList[index - 1].Visible = true;
            PanelControlList[index - 1].BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox5.Text = String.Empty;
            textBox6.Text = String.Empty;
            textBox8.Text = String.Empty;
            textBox9.Text = String.Empty;
            textBox10.Text = String.Empty;
            textBox11.Text = String.Empty;
            textBox12.Text = String.Empty;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = rnd.Next(1, 1000).ToString();
            textBox2.Text = rnd.Next(5, 50).ToString();
            textBox3.Text += "Generating new values.\r\nNumber: " + textBox1.Text + "\r\nIterations: " + textBox2.Text + "\r\n";
        }

        public static long PowerSearch(long z, long pow, long mod)
        {
            long x = 1;
            for (long j = 0; j < pow; j++)
            {
                x = (x * z) % mod;
            }
            return x;
        }

        void ParseAndRunFermaTest()
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox3.Text += "Missing NUMBER value.\r\n";
                return;
            }

            if (string.IsNullOrEmpty(textBox2.Text))
            {
                textBox3.Text += "Missing ITERATIONS value.\r\n";
                return;
            }


            long number = 0, cycles = 0;
            try
            {
                number = long.Parse(textBox1.Text);
                cycles = long.Parse(textBox2.Text);

                if (number == 0 || cycles == 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                textBox3.Text += "One of the values is invalid.\r\n";
                return;
            }

            Task.Factory.StartNew(() => FermaEntry(number, cycles));
        }

        void FermaEntry(long number, long cycles)
        {
            try
            {
                long a, b;
                bool prime = true;
                for (int i = 0; i < cycles; i++)
                {
                    a = (long)rnd.Next(2, (int)number - 1);
                    b = PowerSearch(a, number - 1, number);

                    if (b != 1)
                    {
                        textBox3.Text += "---------------------\r\nThe number " + textBox1.Text + " is composite.\r\n---------------------\r\n";
                        prime = false;
                        break;
                    }
                }

                if (prime)
                {
                    textBox3.Text += "---------------------\r\nThe number " + textBox1.Text + " is prime.\r\n---------------------\r\n";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ParseAndRunFermaTest();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox6.Text = rnd.Next(1, 1000).ToString();
            textBox4.Text += "Generating new values.\r\nNumber: " + textBox6.Text + "\r\n";
        }

        void ParseAndRunRabinMillerTest()
        {
            if (string.IsNullOrEmpty(textBox6.Text))
            {
                textBox4.Text += "Missing NUMBER value.\r\n";
                return;
            }

            long number = 0;
            try
            {
                number = long.Parse(textBox6.Text);
                if (number == 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                textBox4.Text += "One of the values is invalid.\r\n";
                return;
            }

            Task.Factory.StartNew(() => RabinMillerEntry(number));
        }

        void RabinMillerEntry(long number)
        {
            try
            {
                long baseNum = (long)rnd.Next(2, (int)number - 2);
                long s = 0; // степень двойки
                long t = 0; // нечетное число, n-1 = 2^s*t
                bool searchRequired = true;
                long primeSearchCounter = 0;

                // если число n чётное или НОД(baseNum, n)!=1, то оно составное
                if (number % 2 == 0 || ExtendedGCD(baseNum, number)[0] != 1)
                {
                    textBox4.Text += "---------------------\r\nThe number " + number.ToString() + " is composite.\r\n---------------------\r\n";
                }
                else
                {
                    t = (number - 1);
                    //представляем число n - 1 в таком виде: n-1 = 2^s*t,  находим s и t
                    while (searchRequired)
                    {
                        if (t % 2 == 0)
                        {
                            s++;
                            t = (number - 1) / (long)Math.Pow(2, s);
                        }
                        else
                        {
                            searchRequired = false;
                        }
                    }

                    //если baseNum^t = 1 (mod n), или baseNum^((2^r)*t) = -1 (mod n) при 0<=r<s , то n псевдопростое по основанию baseNum
                    for (int i = 0; i < s; i++)
                    {
                        long atn = PowerSearch(baseNum, t, number); // возводим а в степень t по модулю n

                        if ((Math.Abs(atn) == 1) || (PowerSearch(atn, (long)Math.Pow(2, i), number) == number - 1)) // вместо того, чтобы писать Stepen(atn, (long) Math.Pow(2, i), n) == -1
                        {
                            textBox4.Text += "---------------------\r\nThe number " + number.ToString() + " is prime.\r\n---------------------\r\n";
                            break;
                        }
                        else
                        {
                            primeSearchCounter += 1;
                        }
                    }
                    //если ни при одном r (0<=r<s) не выполняется baseNum^((2^r)*t) = -1 (mod n), то n составное
                    if (primeSearchCounter == s)
                    {
                        textBox4.Text += "---------------------\r\nThe number " + number.ToString() + " is composite.\r\n---------------------\r\n";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        public static long[] ExtendedGCD(long a, long b)
        {
            long[] result = new long[3];
            if (b == 0)
            {
                result[0] = a;
                result[1] = 1;
                result[2] = 0;
                return result;
            }
            var sub_res = ExtendedGCD(b, a % b);
            result[0] = sub_res[0];
            result[1] = sub_res[2];
            result[2] = sub_res[1] - (a / b) * sub_res[2];
            return result;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ParseAndRunRabinMillerTest();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox8.Text = rnd.Next(1, 1000).ToString();
            textBox5.Text += "Generating new values.\r\nNumber: " + textBox8.Text + "\r\n";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ParseAndRunPollardPMinusOne();
        }

        void PollardTest(long number, long b)
        {
            try
            {
                long a = 2, p;
                for (int j = 2; j <= b; j++)
                {
                    a = PowerSearch(a, j, number);
                }

                p = ExtendedGCD(a - 1, number)[0];
                if (p != 1 || p != number)
                {
                    textBox5.Text += p.ToString() + " is a divisor of " + number.ToString() + "\r\n";
                }
                else
                {
                    textBox5.Text += "Something went wrong, try again.\r\n";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        void ParseAndRunPollardPMinusOne()
        {
            if (string.IsNullOrEmpty(textBox8.Text) || string.IsNullOrEmpty(textBox7.Text))
            {
                textBox5.Text += "Missing NUMBER or B value.\r\n";
                return;
            }

            long number = 0;
            long b = 0;
            try
            {
                number = long.Parse(textBox8.Text);
                if (number <= 0)
                {
                    throw new Exception();
                }

                b = long.Parse(textBox7.Text);
                if (b <= 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                textBox5.Text += "Value is invalid.\r\n";
                return;
            }

            Task.Factory.StartNew(() => PollardTest(number, b));
        }

        private Tuple<long, long, long> SelectRandomEquation()
        {
            return equations[rnd.Next() % equations.Count];
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var tuple = SelectRandomEquation();
            textBox10.Text = tuple.Item1.ToString();
            textBox11.Text = tuple.Item2.ToString();
            textBox12.Text = tuple.Item3.ToString();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox9.Text = String.Empty;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ParseAndRunPohligHellman();
        }

        public void PrintFormated(object arg1, object arg2, object arg3, object arg4, object arg5)
        {
            textBox9.Text += "--------------------------" + "\r\n";
            textBox9.Text += String.Format(" {0} | {1} | {2} | {3} | {4}", arg1, arg2, arg3, arg4, arg5) + "\r\n";
            textBox9.Text += "--------------------------" + "\r\n";
        }

        void ParseAndRunPohligHellman()
        {
            if (string.IsNullOrEmpty(textBox11.Text))
            {
                MessageBox.Show("Enter number");
                return;
            }

            if (string.IsNullOrEmpty(textBox10.Text))
            {
                MessageBox.Show("Enter H");
                return;
            }

            if (string.IsNullOrEmpty(textBox11.Text))
            {
                MessageBox.Show("Enter G");
                return;
            }

            if (string.IsNullOrEmpty(textBox12.Text))
            {
                MessageBox.Show("Enter P");
                return;
            }

            long h = 0, g = 0, p = 0;
            try
            {
                h = long.Parse(textBox10.Text);
                g = long.Parse(textBox11.Text);
                p = long.Parse(textBox12.Text);
            }
            catch
            {
                MessageBox.Show("One of the values isn't valid");
                return;
            }

            try
            {
                PohlingHellman(h, g, p);
            }
            catch (Exception ex)
            {
                textBox9.Text += ex.Message + "\r\n";
                textBox9.Text += String.Format("The equation {0} = {1}^x (mod {2}) has no solution", h, g, p) + "\r\n";
                textBox9.Text += "--------------------" + "\r\n";
            }
            textBox9.Text += "\n" + "\r\n";
        }

        public static Dictionary<long, int> CountOccurences(List<long> primeFactors)
        {
            return primeFactors.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        }

        public static long FixModulo(long value, long module)
        {
            return value >= 0 ? value : value + module;
        }

        public static List<long> Factorization(long p)
        {
            var d = 2;
            var primeFactors = new List<long>();
            while (d * d <= p)
            {
                while ((p % d) == 0)
                {
                    primeFactors.Add(d);
                    p = (long)Math.Floor((double)p / (double)d);
                }
                d += 1;
            }
            if (p > 1)
            {
                primeFactors.Add(p);
            }
            return primeFactors;
        }

        public static long ChineseRemainderAlgorithm(List<KeyValuePair<long, long>> pairs)
        {
            var N = pairs[0].Value;
            var X = 0L;
            foreach (var ni in pairs.Skip(1))
            {
                N *= ni.Value;
            }
            foreach (var tuple in pairs)
            {
                var mi = N / tuple.Value;
                var gcd = ExtendedGCD(mi, tuple.Value);
                X += mi * tuple.Key * gcd[1];
            }
            var result = X % N;
            return FixModulo(result, N);
        }

        public static long ShankssSmallStepBigStepAlgorithm(long alpha, long beta, long n)
        {
            var m = Convert.ToInt32(Math.Ceiling(Math.Sqrt(n - 1)));
            var a = BigInteger.ModPow(alpha, m, n);
            var b = ExtendedGCD(alpha, n)[1];

            var L1 = new List<KeyValuePair<long, long>>();
            var L2 = new List<KeyValuePair<long, long>>();

            for (long k = 0; k < m; k++)
            {
                var valueL1 = FixModulo((long)(BigInteger.ModPow(a, k, n)), n);
                var valueL2 = FixModulo((long)(beta * BigInteger.Pow(b, (int)k) % n), n);
                L1.Add(new KeyValuePair<long, long>(k, valueL1));
                L2.Add(new KeyValuePair<long, long>(k, valueL2));
            }

            var sortedL1 = L1.OrderBy(o => o.Value).ToList();
            var sortedL2 = L2.OrderBy(o => o.Value).ToList();

            int i = 0, j = 0;
            while (i < m && j < m)
            {
                if (sortedL1[j].Value == sortedL2[i].Value)
                {
                    return m * sortedL1[j].Key + sortedL2[i].Key % n;
                }
                else if (Math.Abs(sortedL1[j].Value) > Math.Abs(sortedL2[i].Value))
                {
                    i = i + 1;
                }
                else
                {
                    j = j + 1;
                }
            }

            throw new Exception("This can be happening...");
        }

        public static long ModuloInversion(long b, long n)
        {
            var gcd = ExtendedGCD(b, n);
            var g = gcd[0];
            var x = gcd[1];
            if (g == 1)
            {
                return x % n;
            }
            throw new Exception("Ain't gonna work m8");
        }

        public static KeyValuePair<long, long> CongruencePair(long g, long h, long p, long q, long e, long e1, long e2)
        {
            var alphaInverse = ModuloInversion(e1, p);
            var x = 0L;
            foreach (var i in Enumerable.Range(1, (int)(e)))
            {
                var a = FixModulo((long)(BigInteger.ModPow(e1, BigInteger.Pow(q, (int)(e - 1)), p)), p);
                var b = FixModulo((long)(BigInteger.ModPow((e2 * BigInteger.Pow(alphaInverse, (int)x)), (BigInteger.Pow(q, (int)(e - i))), p)), p);
                x += ShankssSmallStepBigStepAlgorithm(a, b, p) * (long)(BigInteger.Pow(q, i - 1));
            }
            return new KeyValuePair<long, long>(x, (long)(BigInteger.Pow(q, (int)e)));
        }

        public void PohlingHellman(long h, long g, long p)
        {
            var CountOccurencesList = CountOccurences(Factorization(p - 1)).ToList();
            var CongruenceList = new List<KeyValuePair<long, long>>();
            textBox9.Text += "-------------------------" + "\r\n";
            textBox9.Text += String.Format("Solving equation {0} = ({1})^x (mod {2})", h, g, p) + "\r\n";
            textBox9.Text += "-------------------------" + "\r\n";
            PrintFormated("q", "e", "g^((p-1)/q^e)", "h^((p-1)/q^e)", "Equation (g^((p-1)/q^e))^x = h^((p-1)/q^e) для x");
            foreach (var i in Enumerable.Range(0, CountOccurencesList.Count))
            {
                var e1 = FixModulo((long)BigInteger.ModPow(g, ((p - 1) / BigInteger.Pow(CountOccurencesList[i].Key, CountOccurencesList[i].Value)), p), p);
                var e2 = FixModulo((long)BigInteger.ModPow(h, ((p - 1) / BigInteger.Pow(CountOccurencesList[i].Key, CountOccurencesList[i].Value)), p), p);

                CongruenceList.Add(CongruencePair(h, g, p, CountOccurencesList[i].Key, (long)CountOccurencesList[i].Value, e1, e2));

                var e3 = CongruenceList[CongruenceList.Count - 1].Key % CongruenceList[CongruenceList.Count - 1].Value;
                var e4 = CongruenceList[CongruenceList.Count - 1].Value;
                PrintFormated(CountOccurencesList[i].Key, CountOccurencesList[i].Value, e1, e2, String.Format("x = {0} (mod {1})", e3, e4));
            }
            textBox9.Text += String.Format("Solving the system with GDC:") + "\r\n";
            textBox9.Text += "------------------------" + "\r\n";
            textBox9.Text += String.Format(" x = {0}", ChineseRemainderAlgorithm(CongruenceList)) + "\r\n";
            textBox9.Text += "------------------------" + "\r\n";
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
