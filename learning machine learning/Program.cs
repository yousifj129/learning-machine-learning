// See https://aka.ms/new-console-template for more information
using learning_machine_learning.Simple;

/*double[] X = { 1, 2, 3, 4, 5 , 6, 7, 8, 9, 10, 11,12 };

double[] Y = new double[X.Length];
for (int i = 0; i < X.Length; i++)
{
    Y[i] = X[i] * X[i];
    Console.WriteLine(Y[i]);
}

LinearRegression lr = new LinearRegression(X,Y);

for(int i = 1;i < X.Length; i++)
{
    Console.WriteLine(lr.regression(i));

}
*/

NeuralNetworkSimple nn = new NeuralNetworkSimple(2,6,2 , NeuralNetworkSimple.ActivationMode.relu);
double[] inp = new double[] { 0, 1 };
double[] target = new double[] { 3, 2 };


nn.train(inp, target, 100000, 0.01);

double[] a = nn.feedforward(new double[] { 0, 1 });


for(int i = 0;i < a.Length; i++)
{
    Console.WriteLine(a[i]);
}