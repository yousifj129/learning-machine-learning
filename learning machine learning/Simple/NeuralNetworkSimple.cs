using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learning_machine_learning.Simple
{
    internal class NeuralNetworkSimple
    {
        #region variables
        public int inputSize = 0;
        public int hiddenSize = 0;
        public int outputSize = 0;

        public double[,] weightIH = new double[0,0];
        public double[,] weightHO = new double[0, 0];

        public double[,] biases = new double[0,0];

        public ActivationMode activationMode = ActivationMode.relu;
        #endregion
        
        
        #region basics
        public NeuralNetworkSimple(int input = 2, int hidden = 6, int output = 2 , ActivationMode act = ActivationMode.relu) {
            inputSize= input;
            hiddenSize = hidden;
            outputSize = output;
            activationMode= act;
            weightIH = new double[input, hidden];
            weightHO = new double[hidden, output];
            biases = new double[hidden,2];
            biases = randomizeMatrix(biases);
            weightIH = randomizeMatrix(weightIH);
            weightHO= randomizeMatrix(weightHO);
        }

        public enum ActivationMode
        {
            NONE,
            sigmoid,
            relu
        }
        #endregion

        
        #region feedforward
        public double[] feedforward(double[] input)
        {
            double[] output1 = new double[hiddenSize];
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < hiddenSize; j++)
                {
                    output1[j] = ActivationFunction(weightIH[i, j] * input[i] + biases[j,0]);
                }
            }
            double[] output2 = new double[outputSize];

            for (int i = 0; i < weightIH.GetLength(1); i++)
            {
                for (int j = 0; j < outputSize; j++)
                {
                    output2[j] = ActivationFunction(weightHO[i, j] * output1[i] + biases[j,1]);
                }
            }
            return output2;

        }
        #endregion


        #region train
        public void train(double[] input , double[] targetOutput, int epochs, double learningRate) {
            Random rnd = new Random();
            for (int epoch = 0; epoch < epochs; epoch++)
            {
                double[] output = feedforward(input);
                double error = 0;
                double[] outputError = new double[outputSize];
                for (int j = 0; j < outputSize; j++)
                {
                    if (targetOutput != null && output != null && j < targetOutput.Length && j < output.Length)
                    {
                        outputError[j] = targetOutput[j] - output[j];
                        error += outputError[j] * outputError[j];
                    }

                }
                // Backpropagate error and update weights
                double[] hidden = new double[hiddenSize];
                double[] hiddenSum = new double[hiddenSize]; // Define and initialize hiddenSum
                for (int j = 0; j < hiddenSize; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < outputSize; k++)
                    {
                        sum += outputError[k] * weightHO[j, k];
                    }
                    hiddenSum[j] = sum; // Initialize hiddenSum with the sum of weighted inputs
                    hidden[j] = sum;
                }
                hidden = ApplyActivationVector(hidden);
                double[] hiddenError = new double[hiddenSize];
                for (int j = 0; j < hiddenSize; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < outputSize; k++)
                    {
                        sum += outputError[k] * weightHO[j, k];
                    }
                    hiddenError[j] = ApplyActivationVector(hidden)[j] * sum;
                }
                //weightHO = MultiplyWithScalar(weightHO, errorSum * learningRate);
                for (int i = 0; i < weightHO.GetLength(0); i++)
                {
                    for (int j = 0; j < weightHO.GetLength(1); j++)
                    {
                        weightHO[i,j] += learningRate * outputError[j] * hidden[i] ;
                    }
                }
                for (int i = 0; i < weightIH.GetLength(0); i++)
                {
                    for (int j = 0; j < weightIH.GetLength(1); j++)
                    {
                        weightIH[i, j] += learningRate * hiddenError[j] * input[i];
                    }
                }

            }
        }
        #endregion




        #region helper functions
        public double[] ApplyActivationVector(double[] values)
        {
            if (activationMode == ActivationMode.sigmoid)
            {
                return values.Select(x => x * (1 - x)).ToArray();
            }
            else if (activationMode == ActivationMode.NONE)
            {
                return values.Select(x => 1 - Math.Pow(Math.Tanh(x), 2)).ToArray();
            }
            else if (activationMode == ActivationMode.relu)
            {
                return values.Select(x => x > 0 ? 1.0 : 0.0).ToArray();
            }
            else
            {
                 throw new NotImplementedException();
            }
            
        }
            public double ActivationFunction(double x)
        {
            if(activationMode== ActivationMode.NONE)
            {
                return x;
            }
            else if(activationMode== ActivationMode.sigmoid)
            {
                return 1 / (1 + Math.Pow(Math.E, -x));
            }
            else if(activationMode == ActivationMode.relu)
            {
                return Math.Max(0, x);
            }
            else { throw new NotImplementedException(); }
        }
        public static double[] VectorDifference(double[] vector1, double[] vector2)
        {
            if (vector1.Length != vector2.Length)
            {
                throw new ArgumentException("Vectors must have the same length.");
            }

            double[] difference = new double[vector1.Length];

            for (int i = 0; i < vector1.Length; i++)
            {
                difference[i] = vector1[i] - vector2[i];
            }

            return difference;
        }
        public static double[,] MultiplyWithScalar(double[,] matrix, double scalar)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            double[,] result = new double[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[i, j] = matrix[i, j] * scalar;
                }
            }

            return result;
        }
        public static double[,] randomizeMatrix(double[,] matrix)
        {
            double[,] m = matrix;
            Random rnd = new Random();
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    m[i,j] = rnd.NextDouble();
                }
            }
            return m;
        }
        #endregion
    }
}
