// See https://aka.ms/new-console-template for more information
using learning_machine_learning.Simple;

double[] X = { 1, 2, 3, 4, 5 };
double[] Y = { 3, 5, 7, 9, 11 };

LinearRegression lr = new LinearRegression(X,Y);

for(int i = 1;i < X.Length; i++)
{
    Console.WriteLine(lr.regression(i));

}
