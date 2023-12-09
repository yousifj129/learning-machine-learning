using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learning_machine_learning
{
    internal class MatrixMath
    {
        public static double[,] Transpose(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[,] result = new double[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }

            return result;
        }

        // Function to calculate the matrix multiplication
        public static double[,] Multiply(double[,] matrixA, double[,] matrixB)
        {
            int rowsA = matrixA.GetLength(0);
            int colsA = matrixA.GetLength(1);
            int rowsB = matrixB.GetLength(0);
            int colsB = matrixB.GetLength(1);

            if (colsA != rowsB)
            {
                throw new ArgumentException("Invalid matrix dimensions for multiplication");
            }

            double[,] result = new double[rowsA, colsB];

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < colsA; k++)
                    {
                        sum += matrixA[i, k] * matrixB[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return result;
        }
        public static double[] Multiply(double[,] matrix, double[] vector)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            if (cols != vector.Length)
            {
                throw new ArgumentException("Invalid matrix and vector dimensions for multiplication");
            }

            double[] result = new double[rows];

            for (int i = 0; i < rows; i++)
            {
                double sum = 0;
                for (int j = 0; j < cols; j++)
                {
                    sum += matrix[i, j] * vector[j];
                }
                result[i] = sum;
            }

            return result;
        }
        public static double[,] Inverse(double[,] matrix)
        {
            int n = matrix.GetLength(0);

            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                throw new ArgumentException("Invalid matrix dimensions for inversion");
            }

            double[,] result = new double[n, n];
            double[,] augmentedMatrix = new double[n, 2 * n];

            // Augment the matrix with an identity matrix
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    augmentedMatrix[i, j] = matrix[i, j];
                    augmentedMatrix[i, j + n] = (i == j) ? 1 : 0;
                }
            }

            // Perform Gaussian elimination
            for (int i = 0; i < n; i++)
            {
                double pivot = augmentedMatrix[i, i];

                for (int j = 0; j < 2 * n; j++)
                {
                    augmentedMatrix[i, j] /= pivot;
                }

                for (int k = 0; k < n; k++)
                {
                    if (k != i)
                    {
                        double factor = augmentedMatrix[k, i];

                        for (int j = 0; j < 2 * n; j++)
                        {
                            augmentedMatrix[k, j] -= factor * augmentedMatrix[i, j];
                        }
                    }
                }
            }

            // Extract the inverse matrix from the augmented matrix
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = augmentedMatrix[i, j + n];
                }
            }

            return result;
        }
    }
}
