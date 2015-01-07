using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessing
{
    class MainEntryPoint
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class ClusterPoint
    {
        public UInt32 color;
        ///
        /// gray value
        ///
        public double GrayValue { get; set; }
        ///
        /// Gets or sets X-coord of the point
        ///

        public int X { get; set; }

        ///

        /// Gets or sets Y-coord of the point
        ///

        public int Y { get; set; }

        ///

        /// Gets or sets some additional data for point
        ///

        public object Tag { get; set; }

        ///

        /// Gets or sets cluster index
        ///

        public double ClusterIndex { get; set; }

        ///

        /// Basic constructor
        ///

        /// X-coord
        /// Y-coord
        public ClusterPoint(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.ClusterIndex = 0;
            GrayValue = 1.0;
        }

        ///

        /// Basic constructor
        ///

        /// X-coord
        /// Y-coord
        public ClusterPoint(int x, int y, object tag)
        {
            this.X = x;
            this.Y = y;
            this.Tag = tag;
            this.ClusterIndex = 0;
            GrayValue = 1.0;
        }

        public ClusterPoint()
        {
            X = 0;
            Y = 0;
            GrayValue = 1.0;
        }
    }

    public class ClusterCentroid : ClusterPoint
    {
        ///


        /// Basic constructor
        ///

        /// Centroid x-coord
        /// Centroid y-coord
        public ClusterCentroid(int x, int y)
            : base(x, y)
        {
        }

        public ClusterCentroid()
        {
            X = 0;
            Y = 0;
        }
    }

    public sealed class CMeansAlgorithm
    {
        ///


        /// Array containing all points used by the algorithm
        ///

        public ClusterPoint[] Points;

        ///

        /// Array containing all clusters handled by the algorithm
        ///

        private ClusterCentroid[] Clusters;

        ///

        /// Gets or sets membership matrix
        ///

        public double[,] U;

        ///

        /// Gets or sets the current fuzzyness factor
        ///

        private double Fuzzyness;

        ///

        /// Algorithm precision
        ///

        private double Eps = Math.Pow(10, -5);

        ///

        /// Gets or sets objective function
        ///

        private double J { get; set; }

        ///

        /// Gets or sets log message
        ///

        public string Log { get; set; }

        ///

        /// Initialize the algorithm with points and initial clusters
        ///

        ///

        /// Private constructor
        ///

        public CMeansAlgorithm()
        {

        }

        ///

        /// Recalculates cluster indexes
        ///

        private void RecalculateClusterIndexes()
        {
            for (int i = 0; i < this.Points.Length; i++)
            {
                double max = U[i, 0];
                var p = this.Points[i];
                p.ClusterIndex = 0;
                for (int j = 1; j < this.Clusters.Length; j++)
                {
                    if (max > U[i, j])
                    {
                        max = U[i, j];
                        p.ClusterIndex = j;
                    }
                }
            }
        }

        ///

        /// Perform one step of the algorithm
        ///

        public void Step()
        {
            for (int c = 0; c < Clusters.Length; c++)
            {
                for (int h = 0; h < Points.Length; h++)
                {
                    double top = CalculateGrayEulerDistance(Points[h], Clusters[c]);
                    if (top < 1.0) top = Eps;

                    // Bottom is the sum of distances from this data point to all clusters.
                    double sumTerms = 0.0;
                    for (int ck = 0; ck < Clusters.Length; ck++)
                    {
                        double thisDistance = CalculateGrayEulerDistance(Points[h], Clusters[ck]);
                        if (thisDistance < 1.0) thisDistance = Eps;
                        sumTerms += Math.Pow(top / thisDistance, 2.0 / (this.Fuzzyness - 1.0));
                    }

                    // Then the MF can be calculated as...
                    U[h, c] = 1.0 / sumTerms;
                }
            }

            this.RecalculateClusterIndexes();
        }

        ///

        /// Calculates Euler's distance between point and centroid
        ///

        /// Point
        /// Centroid
        /// Calculated distance
        /// 

        private double CalculateGrayEulerDistance(ClusterPoint p, ClusterCentroid c)
        {
            return Math.Abs(p.GrayValue - c.GrayValue);
        }

        ///

        /// Calculate the objective function
        ///

        /// The objective function as double value
        private double CalculateObjectiveFunction()
        {
            double Jk = 0;

            for (int i = 0; i < this.Points.Length; i++)
            {
                for (int j = 0; j < this.Clusters.Length; j++)
                {
                    Jk += Math.Pow(U[i, j], this.Fuzzyness) * Math.Pow(this.CalculateGrayEulerDistance(Points[i], Clusters[j]), 2);
                }
            }
            return Jk;
        }

        ///

        /// Calculates the centroids of the clusters 
        ///

        private void CalculateClusterCenters()
        {
            for (int j = 0; j < this.Clusters.Length; j++)
            {
                ClusterCentroid c = this.Clusters[j];
                double uX = 0.0;
                double l = 0.0;

                for (int i = 0; i < this.Points.Length; i++)
                {
                    ClusterPoint p = this.Points[i];

                    double uu = Math.Pow(U[i, j], this.Fuzzyness);
                    uX += uu * p.GrayValue;
                    l += uu;
                }

                c.GrayValue = (uX / l);
               // c.Y = ((int)(uY / l));

                this.Log += string.Format("Cluster Centroid: ({0};)  ", c.GrayValue);
            }
            this.Log += System.Environment.NewLine;
        }

        public void InitMembershipMatrix(double[,] matrix,int width,int height)
        {
            Random random = new Random((int)DateTime.UtcNow.Ticks);
            int max = width * height;
            long sum = 0;
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < height; x++)
                {
                    matrix[x, y] = random.Next(0,max);
                    sum += (long)matrix[x, y];
                }
            }

            // normalize
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < height; x++)
                {
                    matrix[x, y] /= sum;
                }
            }
        }

        ///

        /// Perform a complete run of the algorithm until the desired accuracy is achieved.
        /// For demonstration issues, the maximum Iteration counter is set to 20.
        ///

        /// Algorithm accuracy
        /// The number of steps the algorithm needed to complete
        public int Run(double accuracy, int maxIterations)
        {
            int i = 0;
            double Jnew = 0.0;
            // record the time
            long beginTime = DateTime.UtcNow.Ticks;
            do
            {
                i++;
                // get original objective value
                this.J = this.CalculateObjectiveFunction();
                this.Step();
                // calculate cluster center
                this.CalculateClusterCenters();
                Jnew = this.CalculateObjectiveFunction();
                if (Math.Abs(this.J - Jnew) < accuracy) break;
            }
            while (maxIterations > i);
            if (ViewController.GetInstance().timeUpdate != null)
            {
                ViewController.GetInstance().timeUpdate(DateTime.UtcNow.Ticks - beginTime);
            }
            return i;
        }

        public void PrepareForRun(int categoryNumber)
        {
            // init membership matrix
            ImageContainer container = ImageContainer.GetInstance();
            int samplerNumber = container.imageData.Length;
            U = new double[samplerNumber,categoryNumber];
            this.Fuzzyness = 2;
            Points = new ClusterPoint[samplerNumber];
            Clusters = new ClusterCentroid[categoryNumber];

            for (int index = 0; index < samplerNumber; index++)
            {
                GrayColorMix data = container.imageData[index];
                Points[index] = new ClusterPoint(data.x,data.y);
                // normalize gray value
                Points[index].GrayValue = (double)data.gray;
            }
            for (int index = 0; index < categoryNumber;index++)
            {
                Clusters[index] = new ClusterCentroid(0,0);
                Clusters[index].GrayValue = 0;
            }
            InitMembershipMatrix(U, categoryNumber, samplerNumber);
            CalculateClusterCenters();
        }
    }
}
