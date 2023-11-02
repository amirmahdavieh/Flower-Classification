using System;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;

public class Program
{
    public static void Main()
    {
        String[,] inputArray = new string[150, 5];          // load data from a file into an array.
        int rowCounter = 0;                                 // count rows
        double accuracy;
        double[] lambdas = { 0.005, 0.01, 0.025 };
        double[] epochs = { 20, 50, 100 };
        double[,] table = new double[3, 3];                 // array to print the results in testing


        // read from the file
        foreach (string line in System.IO.File.ReadLines(@"iris.data"))
        {

            inputArray[rowCounter, 0] = line.Split(',')[0];
            inputArray[rowCounter, 1] = line.Split(',')[1];
            inputArray[rowCounter, 2] = line.Split(',')[2];
            inputArray[rowCounter, 3] = line.Split(',')[3];
            inputArray[rowCounter, 4] = line.Split(',')[4];     // label

            rowCounter++;
        }

        int row = 0;
        foreach (double lambda in lambdas)          // test the labels one by one
        {
            int column = 0;
            foreach (int epoch in epochs)           // trying epochs one by one
            {
                NeuralNetwork neuralNetwork = new NeuralNetwork();          // neural network created
                neuralNetwork.learn(inputArray, lambda, epoch);             // train 
                accuracy = neuralNetwork.test(inputArray);                  // test
                Console.WriteLine("The accuracy of " + epoch + " epoch and " + lambda + " lambda: " + "%" + String.Format("{0:0.00}", accuracy));
                Console.WriteLine("----------------------------------------------");
                table[row, column] = accuracy;          // add the results to the table
                column++;
            }
            row++;
        }

        Console.WriteLine("\n");
        Console.WriteLine("\t" + "0.005" + "\t" + "0.01" + "\t" + "0.025");
        Console.WriteLine("");

        for (int v = 0; v < table.GetLength(0); v++)                // print the results on the screen in a tabular format
        {
            Console.Write(epochs[v]);
            for (int i = 0; i < table.GetLength(1); i++)
            {
                Console.Write("\t" + "%" + (table[i, v]).ToString("0.##"));
            }
            Console.WriteLine("\n");
        }
    }
}

public class Neuron
{
    public double[] weights;

    public Neuron()
    {
        weights = new double[4];
        Random rnd = new Random();
        for (int i = 0; i < weights.Length; i++)        // Four weights were generated (between 0 and 1)
        {
            weights[i] = rnd.NextDouble();
        }
    }
    public double calculate(double[] inputArray)        // w1 * x1 + w2 * x2 + ... + w4 * x4
    {
        double sum = 0;


        for (int i = 0; i < weights.Length; i++)
        {
            sum += weights[i] * inputArray[i];
        }

        return sum;
    }
}

public class NeuralNetwork
{
    public Neuron n1;
    public Neuron n2;
    public Neuron n3;

    public double resultN1;         // the result of the calculation
    public double resultN2;
    public double resultN3;

    public int maxCode;
    public NeuralNetwork()
    {
        n1 = new Neuron();      // Three Neuron objects were created
        n2 = new Neuron();
        n3 = new Neuron();
    }
    public void learn(String[,] inputArray, double lambda, int epoch)       // learning
    {
        Neuron[] neurons = { n1, n2, n3 };

        double input1;
        double input2;
        double input3;
        double input4;
        String label;           // label for supervised learning

        for (int e = 0; e < epoch; e++)         // Training up to the number of epochs
        {
            double maxResult = 0;
            double[] inputs = new double[4];            //  place the data into an array excluding the labels
            int labelCode;

            for (int row = 0; row < inputArray.GetLength(0); row++)             // It iterates 150 times (for processing the data)
            {

                input1 = Convert.ToDouble(String.Format("{0:0.00}", Convert.ToDouble(inputArray[row, 0]) / 10));        // It divides each input by 10
                input2 = Convert.ToDouble(String.Format("{0:0.00}", Convert.ToDouble(inputArray[row, 1]) / 10));
                input3 = Convert.ToDouble(String.Format("{0:0.00}", Convert.ToDouble(inputArray[row, 2]) / 10));
                input4 = Convert.ToDouble(String.Format("{0:0.00}", Convert.ToDouble(inputArray[row, 3]) / 10));

                inputs[0] = input1;
                inputs[1] = input2;
                inputs[2] = input3;
                inputs[3] = input4;

                label = inputArray[row, 4];

                if (label == "Iris-setosa")                 // We are coding the names of the flowers
                {
                    labelCode = 0;
                }
                else if (label == "Iris-versicolor")
                {
                    labelCode = 1;
                }
                else
                {
                    labelCode = 2;
                }

                resultN1 = n1.calculate(inputs);
                resultN2 = n2.calculate(inputs);
                resultN3 = n3.calculate(inputs);
                double[] results = { resultN1, resultN2, resultN3 };

                for (int i = 0; i < results.Length; i++)            // Finding the maximum value among the results of n1, n2, and n3
                {
                    if (maxResult < results[i])
                    {
                        maxResult = results[i];
                        maxCode = i;                                // Coding the maximum value
                    }
                }

                if (maxCode != labelCode)                           // If maxCode and labelCode are not equal, change the weights
                {
                    for (int i = 0; i < neurons[maxCode].weights.Length; i++)
                    {
                        neurons[maxCode].weights[i] -= (lambda * inputs[i]);
                    }

                    for (int j = 0; j < neurons[labelCode].weights.Length; j++)
                    {
                        neurons[labelCode].weights[j] += (lambda * inputs[j]);
                    }
                }

            }
        }
    }

    public double test(String[,] inputArray)                // test
    {
        Neuron[] neurons = { n1, n2, n3 };

        double correctAnswers = 0;
        double input1;
        double input2;
        double input3;
        double input4;
        String label;


        double[] inputs = new double[4];
        int labelCode;

        for (int row = 0; row < inputArray.GetLength(0); row++)
        {
            double maxResult = 0;
            input1 = Convert.ToDouble(String.Format("{0:0.00}", Convert.ToDouble(inputArray[row, 0]) / 10));
            input2 = Convert.ToDouble(String.Format("{0:0.00}", Convert.ToDouble(inputArray[row, 1]) / 10));
            input3 = Convert.ToDouble(String.Format("{0:0.00}", Convert.ToDouble(inputArray[row, 2]) / 10));
            input4 = Convert.ToDouble(String.Format("{0:0.00}", Convert.ToDouble(inputArray[row, 3]) / 10));

            inputs[0] = input1;
            inputs[1] = input2;
            inputs[2] = input3;
            inputs[3] = input4;

            label = inputArray[row, 4];

            if (label == "Iris-setosa")
            {
                labelCode = 0;
            }
            else if (label == "Iris-versicolor")
            {
                labelCode = 1;
            }
            else
            {
                labelCode = 2;
            }

            resultN1 = n1.calculate(inputs);
            resultN2 = n2.calculate(inputs);
            resultN3 = n3.calculate(inputs);
            double[] results = { resultN1, resultN2, resultN3 };


            for (int i = 0; i < results.Length; i++)
            {
                if (maxResult < results[i])
                {
                    maxResult = results[i];
                    maxCode = i;
                }
            }

            if (maxCode == labelCode)           // If maxCode and labelCode are equal, it indicates a correct evaluation -> we need to add it to correctAnswers
            {
                correctAnswers += 1;

            }

        }

        return (correctAnswers / 150) * 100;            // returns the percentage of correctness
    }

}
