using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CS312_Project01_REV00
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void solve_Click(object sender, EventArgs e)
        {
            ulong toBeTested = Convert.ToUInt64(input.Text);
            int kval = Convert.ToInt32(kvalue.Text);

            if (toBeTested == 0)
            {
                output.Text = "Not prime with 100% confidence";
                return;
            }

            if (toBeTested == 1)
            {
                output.Text = "Prime with 100% confidence";
                return;
            }

            int numTimesFailed = 0;
            for (int i = 0; i < kval; i++)
            {
                bool isPrime = fermat(toBeTested);
                if (!isPrime)
                    numTimesFailed++;
            }

            if (numTimesFailed >= (double) kval)
            {
                output.Text = "Not prime NumTimesFailed: " + numTimesFailed;
            }
            else
            {
                double chanceOfError = (1 - (Math.Pow(.5, (double) kval)))*100;
                output.Text = "Prime with " + chanceOfError + "% confidence\n" + "numTestsFailed: " + numTimesFailed;
            }

        }

        /*
         * Fermat's theorem is prime numbers meet the requirement that
         * a^p-1=1 mod P, for every a s.t. 1 <= a <= p.
         * 
         * toBeTested is equivalent to p in fermat's theorem.
         */
        private bool fermat(ulong toBeTested)
        {
            ulong rand = randomBtwn(toBeTested);
            ulong remainder = modExp(rand, toBeTested - 1, toBeTested);
            if (remainder == 1)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        private ulong randomBtwn(ulong max)
        {
            Random r = new Random();
            if (max > Int32.MaxValue)
            {
                return (ulong) r.Next(1, Int32.MaxValue);
            }
            else
            {
                return (ulong) r.Next(1,(int) max);
            }
        }

        /*
         * Performs modular exponentiation.
         * Outputs x^y % n
         */
        private ulong modExp(ulong x, ulong y, ulong n)
        {
            try
            {
                if (y == 0)
                    return 1;

                ulong z = modExp(x, y / 2, n);
                if (y % 2 == 0)
                {
                    return ((z % n) * z) % n;
                }
                else
                {
                    return ((((z % n) * z) % n) * x) % n;
                }
            }
            catch (OverflowException e)
            {
                MessageBox.Show("OVERFLOW EXCEPTION");
                return 0;
            }

        }
    }
}
