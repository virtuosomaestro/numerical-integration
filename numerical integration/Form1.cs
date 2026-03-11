using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace numerical_integration
{
    public partial class Form1 : Form
    {
        int ind_of_problem, ind_of_method, num_of_nodes;
        PointPairList nodes = new PointPairList();
        double ans = 0, err = 0, a, b, step;
        const double pi = Math.PI;
        GraphPane pane;

        public Form1()
        {
            InitializeComponent();
            pane = zedGraphControl.GraphPane;
            pane.Title.Text = "График интерполяции";
            pane.Title.FontSpec.Size = 16;
            pane.XAxis.Title.Text = "Ось X";
            pane.YAxis.Title.Text = "Ось Y";
            zedGraphControl.GraphPane.XAxis.MajorGrid.IsVisible = true;
            zedGraphControl.GraphPane.YAxis.MajorGrid.IsVisible = true;
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }

        private double f(double x)
        {
            switch (ind_of_problem)
            {
                case 0:
                    return x * x * x;
                case 1:
                    return x / Math.Sqrt(1 - x * x);
                case 2:
                    return x/Math.Sqrt(4-x*x);
                case 3:
                    double a = 50;
                    return (a / x) * (a / x) * Math.Sin(a / x);
                case 4:
                    return Math.Exp(-x * x);
                default:
                    return 0;
            }
            
        }
        private void init_nodes()
        {
            nodes.Clear();
            switch (ind_of_problem)
            {
                case 0:
                    a = 0;
                    b = 2;
                    break;
                case 1:
                    a = 0;
                    b = 0.99;
                    break;
                case 2:
                    a = 0;
                    b = 1;
                    break;
                case 3:
                    a = 1;
                    b = 3;
                    break;
                case 4:
                    a = 0;
                    b = 5;
                    break;
            }

            step = (b - a) / (num_of_nodes - 1);
            nodes.Add(a, f(a));
            for (int i = 1; i < num_of_nodes; i++)
            {
                double x = nodes.Last().X + step;
                nodes.Add(x, f(x));
            }
        }
        private void draw_function()
        {
            PointPairList list = new PointPairList();
            double N = 10000 * (b - a);
            double stp = (b - a)/ (N - 1), x = a;
            for (int i = 0; i < N; i++)
            {
                list.Add(x, f(x));
                x += stp;
            }
            LineItem myCurve = pane.AddCurve("Функция", list, Color.Red, SymbolType.None);
            myCurve.Line.Width = 3.0F;
        }
        private void draw_nodes()
        {
            LineItem myCurve = pane.AddCurve("Узел", nodes, Color.Red, SymbolType.Circle);
            myCurve.Line.IsVisible = false;
            myCurve.Symbol.Fill = new Fill(Color.Blue);
            myCurve.Line.Width = 8.0F;
        }
        private void draw_rectangles()
        {           
            for (int i = 0; i < num_of_nodes - 1; i++)
            {
                PointPairList list = new PointPairList();
                if (ind_of_method == 0)
                {
                    list.Add(nodes[i].X, 0);
                    list.Add(nodes[i].X, nodes[i].Y);
                    list.Add(nodes[i + 1].X, nodes[i].Y);
                    list.Add(nodes[i + 1].X, 0);
                    list.Add(nodes[i].X, 0);
                }
                else if (ind_of_method == 1)
                {
                    list.Add(nodes[i].X, 0);
                    list.Add(nodes[i].X, nodes[i + 1].Y);
                    list.Add(nodes[i + 1].X, nodes[i + 1].Y);
                    list.Add(nodes[i + 1].X, 0);
                    list.Add(nodes[i].X, 0);
                }
                else
                {
                    list.Add(nodes[i].X, 0);
                    list.Add(nodes[i].X, f((nodes[i].X + nodes[i + 1].X) / 2.0));
                    list.Add(nodes[i + 1].X, f((nodes[i].X + nodes[i + 1].X) / 2.0));
                    list.Add(nodes[i + 1].X, 0);
                    list.Add(nodes[i].X, 0);
                }
                LineItem polygon = pane.AddCurve("", list, Color.Black, SymbolType.None);
                polygon.Line.Fill = new Fill(Color.LightBlue);
            }
        }
        private void draw_trapezoids()
        {
            for (int i = 0; i < num_of_nodes - 1; i++)
            {
                PointPairList list = new PointPairList();
                
                list.Add(nodes[i].X, 0);
                list.Add(nodes[i].X, nodes[i].Y);
                list.Add(nodes[i + 1].X, nodes[i+1].Y);
                list.Add(nodes[i + 1].X, 0);
                list.Add(nodes[i].X, 0);
                
                LineItem polygon = pane.AddCurve("", list, Color.Black, SymbolType.None);
                polygon.Line.Fill = new Fill(Color.LightBlue);
            }
        }
        private double L(double x, double l, double r)
        {
            return 2.0/((r-l)*(r-l))*((x-r)*(x-(l+r)/2.0)*f(l) - 2.0*(x-l)*(x-r)*f((l+r)/2.0) + (x-l)*(x-(l+r)/2.0)*f(r));
        }
        private void draw_interpolant()
        {
            PointPairList list = new PointPairList();
            double N = 10000*(b-a);
            double stp = (b - a) / (N - 1), x = a;
            for (int i = 0; i < num_of_nodes - 1; i++)
            {
                while (x <= nodes[i + 1].X)
                {
                    list.Add(x, L(x, nodes[i].X, nodes[i+1].X));
                    x += stp;
                }
                list.Add(nodes[i+1].X, L(nodes[i+1].X, nodes[i].X, nodes[i+1].X));
            }
            LineItem myCurve = pane.AddCurve("Интерполянт", list, Color.LightBlue, SymbolType.None);
            myCurve.Line.Width = 3.0F;
        }
        private void calc()
        {
            ans = 0;
            err = 0;
            if (ind_of_method == 0){
                for(int i = 0; i < num_of_nodes-1; i++)
                {
                    ans += step * nodes[i].Y;
                }
            }
            else if (ind_of_method == 1)
            {
                for (int i = 0; i < num_of_nodes - 1; i++)
                {
                    ans += step * nodes[i + 1].Y;
                }
            }
            else if(ind_of_method == 2)
            {
                for (int i = 0; i < num_of_nodes - 1; i++)
                {
                    ans += step * f((nodes[i].X + nodes[i + 1].X) / 2.0);
                }
            }
            else if (ind_of_method == 3)
            {
                for (int i = 0; i < num_of_nodes - 1; i++)
                {
                    ans += step*(nodes[i].Y + nodes[i+1].Y)/2.0;
                }
            }
            else if (ind_of_method == 4)
            {
                for (int i = 0; i < num_of_nodes - 1; i++)
                {
                    ans += step * (nodes[i].Y + 4.0 * f((nodes[i].X + nodes[i + 1].X) / 2.0) + nodes[i+1].Y) / 6.0;
                }
            }
        }
        private void draw()
        {
            pane.CurveList.Clear();
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
            draw_nodes();
            if (ind_of_method == 4) draw_interpolant();
            draw_function();
            if (ind_of_method == 3) draw_trapezoids();
            else if(ind_of_method <= 2) draw_rectangles();
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ind_of_problem = problem.SelectedIndex;
            ind_of_method = method.SelectedIndex;
            num_of_nodes = Convert.ToInt32(number_of_nodes.Text);
            init_nodes();
            calc();
            draw();
            integral.Text = "Площадь под графиком: " + ans.ToString("F5");
            error.Text = "Пошрешность: " + err.ToString("F5");
        }

    }
}
