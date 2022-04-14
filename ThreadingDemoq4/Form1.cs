using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadingDemoq4
{
    public partial class Form1 : Form
    {
        private int threadFunc()
        {
            Random rndNum = new Random(Guid.NewGuid().GetHashCode());
            int num = rndNum.Next();
            return num;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = Int32.Parse(textBox1.Text);
            IAsyncResult[] result = new IAsyncResult[n];
            int[] randNums = new int[n];
            Func<int> getRandonNumber = threadFunc;
            for (int i = 0; i < n; i++)
            {
                result[i] = getRandonNumber.BeginInvoke(null, null);
            }

            textBox2.AppendText("Following random numbers are generated: ");
            textBox2.AppendText(Environment.NewLine);

            for (int i = 0; i < n; i++)
            {
                randNums[i] = getRandonNumber.EndInvoke(result[i]);
                textBox2.AppendText(randNums[i].ToString());
                textBox2.AppendText(Environment.NewLine);
            }
        }
    }
}
