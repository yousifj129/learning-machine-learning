// See https://aka.ms/new-console-template for more information
using learning_machine_learning.Simple;
using ScottPlot;
using ScottPlot.WinForms;

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

/*NeuralNetworkSimple nn = new NeuralNetworkSimple(2,6,2 , NeuralNetworkSimple.ActivationMode.relu);
double[] inp = new double[] { 0, 1 };
double[] target = new double[] { 3, 2 };


nn.train(inp, target, 100000, 0.01);

double[] a = nn.feedforward(new double[] { 0, 1 });


for(int i = 0;i < a.Length; i++)
{
    Console.WriteLine(a[i]);
}*/
// Create some example data

double xt(double t)
{
    return Math.Sin( t);
}
double yt(double t)
{
    return t*t;
}

int leng = 1000;
double[] x = new double[leng];
double[] y = new double[leng];

for (int i = 0; i < leng; i++)
{
    x[i] = xt(((double)i) /50);
    y[i] = yt(((double)i) *1000);
}
// Create a new ScottPlot figure
ScottPlot.Plot plt = new();

plt.Style.ColorGrids(new Color(100, 100, 100));
plt.Style.ColorAxes(new Color(255,255,255));
plt.Style.Background(new Color(10, 10, 10), new Color(0, 0, 0));
// Plot the data as a line plot
plt.Add.Scatter(x, y);

// Customize the plot
plt.Title("This Time, We Meet on a Perfect Life");
plt.XLabel("X");
plt.YLabel("Y");

// Save the plot to a file
plt.SavePng("line_plot.png",2000,2000);

