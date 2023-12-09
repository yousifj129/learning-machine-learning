using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;


namespace learning_machine_learning.Simple
{
    /* linear regression aims to find the best-fitting line
    that minimizes the difference between the predicted values 
    and the actual values of the target variable.

    Let's consider a simple example to illustrate linear regression.
    Suppose we have a dataset with two variables: the input feature (X) and the target variable (Y).
    We want to find a linear relationship between X and Y.
    The goal is to find a line that best represents the relationship between the two variables.

    The equation of a linear regression model can be written as:

    Y = β₀ + β₁X

    Here, Y represents the predicted value of the target variable, 
    X is the input feature, β₀ is the y-intercept (the value of Y when X is zero),
    and β₁ is the slope of the line(the change in Y for a unit change in X).

    To find the best-fitting line, we need to estimate the values of β₀ and β₁.
    This is typically done using a method called ordinary least squares(OLS).
    OLS minimizes the sum of the squared differences between the predicted values
    and the actual values of the target variable.

    Once the coefficients β₀ and β₁ are estimated,
    we can use the linear regression model to make predictions.
    Given a new value of X, we can plug it into the equation to obtain the predicted value of Y.
    */
    internal class LinearRegression
    {
        public static double[] coefficients;
        public double beta0;
        public double beta1;
        public LinearRegression(double[] X, double[] Y) {
            // Number of instances
            int n = X.Length;

            // Augmented matrix
            double[,] Xaug = new double[n, 2];
            for (int i = 0; i < n; i++)
            {
                Xaug[i, 0] = 1; // Column of ones for intercept term
                Xaug[i, 1] = X[i];
            }

            // Transpose of augmented matrix
            double[,] XaugT = MatrixMath.Transpose(Xaug);

            // Matrix multiplication: X' * X
            double[,] XTX = MatrixMath.Multiply(XaugT, Xaug);

            // Matrix multiplication: (X' * X)^-1
            double[,] XTXinv = MatrixMath.Inverse(XTX);

            // Matrix multiplication: (X' * X)^-1 * X'
            double[,] XTXinvXT =MatrixMath.Multiply(XTXinv, XaugT);

            // Matrix multiplication: (X' * X)^-1 * X' * Y
            double[] coefficients =MatrixMath.Multiply(XTXinvXT, Y);
            beta0= coefficients[0];
            beta1= coefficients[1];
        }
        public double regression(double x)
        {
            return Math.Round( beta0 + beta1 * x);
        }
    }
}
