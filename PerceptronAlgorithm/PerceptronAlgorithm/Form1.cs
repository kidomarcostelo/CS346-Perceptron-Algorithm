using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Documents;
using System.Windows.Forms;

namespace PerceptronAlgorithm
{
    public partial class Perceptron : Form
    {
        private List<int> x1 = new List<int>() { 1, 1, -1, -1 };
        private List<int> x2 = new List<int>() { 1, -1, 1, -1 };
        private List<int> t = new List<int>() { 1, -1, -1, -1 };

        private List<float> arrBias = new List<float>();
        private List<float> arrW1 = new List<float>();
        private List<float> arrW2 = new List<float>();
        private List<int> arrY = new List<int>();

        private float w1, w2, bias, lrate;
        
        public Perceptron()
        {
            InitializeComponent();

            // Initialize data in the table
            tbl.Controls.Add(new Label() { Text = "x1"}, 0, 0);
            tbl.Controls.Add(new Label() { Text = "x2"}, 1, 0);
            tbl.Controls.Add(new Label() { Text = "t"}, 2, 0);

            
            FillInputData();

            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "1";
        }

        private void FillInputData()
        {
            for (int i = 1; i < 5; i++)
            {

                tbl.Controls.Add(new Label() { Text = "" + x1[i-1] }, 0, i);
                tbl.Controls.Add(new Label() { Text = "" + x2[i-1] }, 1, i);
                tbl.Controls.Add(new Label() { Text = "" + t[i-1] }, 2, i);
            }
        }

        private void Algorithm(float weight1, float weight2, float b, float lr)
        {
            ClearArray();
            tbl2.Controls.Clear();


            tbl2.Controls.Add(new Label() { Text = "x1" }, 0, 0);
            tbl2.Controls.Add(new Label() { Text = "x2" }, 1, 0);
            tbl2.Controls.Add(new Label() { Text = "t" }, 2, 0);
            tbl2.Controls.Add(new Label() { Text = "y" }, 3, 0);
            tbl2.Controls.Add(new Label() { Text = "newW1" }, 4, 0);
            tbl2.Controls.Add(new Label() { Text = "newW2" }, 5, 0);
            tbl2.Controls.Add(new Label() { Text = "newBias" }, 6, 0);


            // Step 1
            // Initialize all weights, bias and learning rate
            w1 = weight1; w2 = weight2; bias = b; lrate = lr;
            float yin;
            int y;

            

            // Step 2 
            // Calculate new input
            for (int i = 0; i < 4; i++)
            {
                // Sigmoid
                //float num = (x1[i] * w1) + (x2[i] * w2) + bias;
                yin = (x1[i] * w1) + (x2[i] * w2) + bias;
                //arrYin.Add(yin);

                y = SigmoidFunc(yin);
                arrY.Add(y);

                // Step 4 update weights
                
                    // newwegiht = oldweight + learningrate * t[i] * x[i]
                    w1 = w1 + (lrate * t[i] * x1[i]);
                    w2 = w2 + (lrate * t[i] * x2[i]);

                    arrW1.Add(w1);
                    arrW2.Add(w2);

                    // new bias
                    bias = bias + (lrate * t[i]);
                    arrBias.Add(bias);
                

                tbl2.Controls.Add(new Label() { Text = "" + x1[i] }, 0, i + 1);
                tbl2.Controls.Add(new Label() { Text = "" + x2[i] }, 1, i + 1);
                tbl2.Controls.Add(new Label() { Text = "" + t[i] }, 2, i + 1);
                tbl2.Controls.Add(new Label() { Text = "" + arrY[i] }, 3, i + 1);
                tbl2.Controls.Add(new Label() { Text = "" + arrW1[i] }, 4, i + 1);
                tbl2.Controls.Add(new Label() { Text = "" + arrW2[i] }, 5, i + 1);
                tbl2.Controls.Add(new Label() { Text = "" + arrBias[i] }, 6, i + 1);

                tbl2.Update();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            tbl2.Controls.Clear();
            ClearArray();
            label10.Text = "" ;
            label11.Text = "" ;
            label12.Text = "" ;
            label9.Text = "Iteration:";

            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "1";
        }

        private int SigmoidFunc(float num)
        {
            if (num > 0)
            {
                return 1;
            }
            if (num < 0)
            {
                return -1;
            }
            return 0;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            w1 = float.Parse(textBox1.Text);
            w2 = float.Parse(textBox2.Text);
            bias = float.Parse(textBox3.Text);
            lrate = float.Parse(textBox4.Text);
            int count = 0;

            label9.Text = "Iteration:";

            while (true)
            {
                count++;
                label9.Text = $"Iteration: {count}";

                Algorithm(w1, w2, bias, lrate);

                label10.Text = "" + w1;
                label11.Text = "" + w2;
                label12.Text = "" + bias;

                if (t.SequenceEqual(arrY))
                {
                    break;
                }

            }

            textBox1.Text = "" + w1;
            textBox2.Text = "" + w2;
            textBox3.Text = "" + bias;
        }

        private void ClearArray()
        {
            arrBias.Clear();
            arrW1.Clear();
            arrW2.Clear();
            arrY.Clear();
        }
    }
}
