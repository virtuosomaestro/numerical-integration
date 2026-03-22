using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
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
        List<double> exact_values = new List<double> {4.0, Math.Asin(0.99), 2.0 - Math.Sqrt(3.0)};


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
                    double a = 67;
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
        private void draw_simpson()
        {
            PointPairList list = new PointPairList();
            double N = 10000*(b-a);
            double stp = (b - a) / (N - 1), x = a;
            list.Add(nodes[0].X, 0);
            for (int i = 0; i < num_of_nodes - 1; i++)
            {
                while (x <= nodes[i + 1].X)
                {
                    list.Add(x, L(x, nodes[i].X, nodes[i+1].X));
                    x += stp;
                }
                list.Add(nodes[i+1].X, L(nodes[i+1].X, nodes[i].X, nodes[i+1].X));
            }
            list.Add(nodes[num_of_nodes-1].X, 0);
            list.Add(nodes[0].X, 0);
            LineItem polygon = pane.AddCurve("", list, Color.Black, SymbolType.None);
            polygon.Line.Fill = new Fill(Color.LightBlue);
            polygon.Line.Width = 3.0F;
        }
        private void calc()
        {
            ans = 0;
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
            if (ind_of_problem <= 2)
            {
                err = Math.Abs(ans - exact_values[ind_of_problem]);
            }
        }
        private void draw()
        {
            pane.CurveList.Clear();
            draw_nodes();
            draw_function();
            if (ind_of_method == 3) draw_trapezoids();
            else if(ind_of_method <= 2) draw_rectangles();
            else draw_simpson();
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }
        private void display_values()
        {
            integral.Text = "Приближённое значение: " + ans.ToString("F5");
            if(ind_of_problem <= 2)
            {
                exact.Text = "Точное значение: " + exact_values[ind_of_problem].ToString("F5");
                error.Text = "Пошрешность: " + err.ToString("F5");
            }
            else
            {
                exact.Text = "";
                error.Text = "";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ind_of_problem = problem.SelectedIndex;
            ind_of_method = method.SelectedIndex;
            num_of_nodes = Convert.ToInt32(number_of_nodes.Text);
            init_nodes();
            calc();
            draw();
            display_values();
            calc_table();
        }
        private void calc_table()
        {
            grid.Rows.Clear();
            grid.Columns.Clear();

            grid.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 15);

            if (ind_of_problem > 2)
                grid.Columns.Add("-1", "Таблица приближенных значений для F" + problem.Text);
            else
                grid.Columns.Add("-1", "Таблица погрешностей для F" + problem.Text);

            grid.Columns.Add("0", "");
            grid.Columns.Add("1", "");
            grid.Columns.Add("2", "");
            grid.Columns.Add("3", "Количество узлов");
            grid.Columns.Add("4", "");
            grid.Columns.Add("5", "");
            grid.Columns.Add("6", "");
            grid.Rows.Add("", "", "5", "9", "17", "33", "65", "129");
            grid.Rows.Add("", "Прямоугольники (слева)", "", "", "", "", "", "");
            grid.Rows.Add("", "Прямоугольники (справа)", "", "", "", "", "", "");
            grid.Rows.Add("Метод", "Прямоугольники (посередине)", "", "", "", "", "", "");
            grid.Rows[3].Cells[0].Style.Font = new Font("Microsoft Sans Serif", 15, FontStyle.Bold);
            grid.Rows.Add("", "Трапеции", "", "", "", "", "", "");
            grid.Rows.Add("", "Симпсон", "", "", "", "", "", "");
            grid.Columns["-1"].Width = 230;
            grid.Columns["0"].Width = 300;
            grid.Columns["1"].Width = 215;
            grid.Columns["2"].Width = 215;
            grid.Columns["3"].Width = 215;
            grid.Columns["4"].Width = 215;
            grid.Columns["5"].Width = 215;
            grid.Columns["6"].Width = 215;

            grid.Rows[0].Height = 30;
            grid.Rows[1].Height = 30;
            grid.Rows[2].Height = 30;
            grid.Rows[3].Height = 30;
            grid.Rows[4].Height = 30;
            grid.Rows[5].Height = 30;

            for (int column = 2; column <= 7; column++)
            {
                for (int raw = 1; raw <= 5; raw++)
                {
                    num_of_nodes = Convert.ToInt32(Math.Pow(2, column)) + 1;
                    ind_of_method = raw - 1;
                    init_nodes();
                    calc();
                    if (ind_of_problem > 2)
                        grid[column, raw].Value = ans.ToString("F5");
                    else
                        grid[column, raw].Value = err.ToString("F5");
                }
            }

            num_of_nodes = Convert.ToInt32(number_of_nodes.Text);
            ind_of_method = method.SelectedIndex;
            init_nodes();
        }
    }
}
