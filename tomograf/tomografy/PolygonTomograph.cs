using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//na ten moment program nie będzie działał dla jednego lasera (dojdzie do dzielenia przez 0), na dniach dorobię obsługe jednego lasera

// 1. Liczymy wzór prostej dla lasera
// 2. Liczymy wzór prostej dla krawędzi wielokąta
// 3. Szukamy punktów przecięcia krawędzi i lasera
// 4. Sprawdzamy czy punkt przecięcia znajduje się na krawędzi wielokąta
// 5. Szukamy przecięć lasera z wierzchołkami wielokąta
// 6. Sortujemy punkty według pozycji x rosnąco
// 7. Liczymy odległości między punktem 1 i 2, 3 i 4...
// 8. Sumujemy odległości i mnożymy przez materiał
// 9. Dodajemy wartości dla wszystkich wielokątów
// W kroku 2 i 3 trzeba pamiętać o przypadku kiedy krawędź jest pionowa

namespace tomograf.tomografy
{
    class Polygon_Tomograph : ITomograph
    {
        private int n_lasers;
        private Point[][] polygons;
        private double[] materials;

        public Polygon_Tomograph(int n_lasers, Point[][] polygons, double[] materials)
        {
            this.n_lasers = n_lasers;
            this.polygons = polygons;
            this.materials = materials;
        }

        public double[][] Run()
        {
            double distance_between_lasers = 2.0 / (n_lasers - 1);
            double[][] energy_loss = new double[n_lasers][];
            for (int i = 0; i < n_lasers; i++)
            {
                double laser_end_y = 1 - i * distance_between_lasers;
                energy_loss[i] = new double[n_lasers];
                for (int j = 0; j < n_lasers; j++)
                {
                    double laser_start_y = 1 - j * distance_between_lasers;
                    double laser_a = calc_a(-1, laser_start_y, 1, laser_end_y);
                    double laser_b = calc_b(laser_a, -1, laser_start_y);
                    energy_loss[i][j] = all_polygons_energy_loss(polygons, materials, laser_a, laser_b);
                }
            }

            save_to_txt(energy_loss);
            return energy_loss;
        }

        private double calc_a(double x1, double y1, double x2, double y2)
        {
            return (y2 - y1) / (x2 - x1);
        }

        private double calc_b(double a, double x, double y)
        {
            return y - a * x;
        }

        private double all_polygons_energy_loss(Point[][] polygons, double[] materials, double laser_a, double laser_b)
        {
            double energy_loss = 0;
            for (int i = 0; i < polygons.Length; i++)
            {
                energy_loss += polygon_energy_loss(polygons[i], materials[i], laser_a, laser_b);
            }
            return energy_loss;
        }

        private double polygon_energy_loss(Point[] poly, double material, double laser_a, double laser_b)
        {
            // Szukanie przecięć z krawędziami
            var intersection_points = new List<Point>();
            for (int i = 1; i < poly.Length; i++)
            {
                var p = get_intersection_point_on_edge(poly[i - 1], poly[i], laser_a, laser_b);
                if (p != null)
                {
                    intersection_points.Add(p);
                }
            }

            var ip = get_intersection_point_on_edge(poly[^1], poly[0], laser_a, laser_b);
            if (ip != null)
            {
                intersection_points.Add(ip);
            }

            // Szukanie przecięć z wierzchołkami
            for (int i = 0; i < poly.Length; i++)
            {
                var currVertex = poly[i];
                var prevVertex = poly[(i - 1 + poly.Length) % poly.Length];
                var nextVertex = poly[(i + 1) % poly.Length];
                if (check_intersection_with_vertex(currVertex, prevVertex, nextVertex, laser_a, laser_b))
                {
                    intersection_points.Add(currVertex);
                }
            }

            if (intersection_points.Count % 2 == 1)
            {
                throw new Exception("Something is not yes 🤔");
            }

            intersection_points.Sort((p1, p2) => p1.x.CompareTo(p2.x));

            double distance_in_polygon = 0;
            for (int i = 1; i < intersection_points.Count; i += 2)
            {
                distance_in_polygon += calc_distance(intersection_points[i - 1], intersection_points[i]);
            }

            return distance_in_polygon * material;
        }

        private Point? get_intersection_point_on_edge(Point p1, Point p2, double laser_a, double laser_b)
        {
            if (p1.x == p2.x)
            {
                double Y = p1.x * laser_a + laser_b;
                if (Y >= Math.Max(p1.y, p2.y) || Y <= Math.Min(p1.y, p2.y))
                {
                    return null;
                }

                return new Point { x = p1.x, y = Y };
            }

            double edge_a = calc_a(p1.x, p1.y, p2.x, p2.y);
            double edge_b = calc_b(edge_a, p1.x, p1.y);

            if (edge_a == laser_a)
            {
                return null;
            }

            double x = (laser_b - edge_b) / (edge_a - laser_a);
            double y = x * edge_a + edge_b;

            if (x >= Math.Max(p1.x, p2.x) || x <= Math.Min(p1.x, p2.x))
            {
                return null;
            }

            return new Point { x = x, y = y };
        }

        private bool check_intersection_with_vertex(Point currVertex, Point prevVertex, Point nextVertex, double laser_a, double laser_b)
        {
            if (laser_a * currVertex.x + laser_b != currVertex.y)
            {
                return false;
            }

            // Uznajemy przecięcie tylko gdy poprzedni i następny wierzchołek są po różnych stronach lasera
            // Jeśli p1 i p2 mają przeciwne znaki, oznacza to, że punkty leżą po przeciwnych stronach prostej
            double p1 = laser_a * prevVertex.x + laser_b - prevVertex.y;
            double p2 = laser_a * nextVertex.x + laser_b - nextVertex.y;
            return p1 * p2 < 0;
        }

        private double calc_distance(Point p1, Point p2)
        {
            double x_diff = p1.x - p2.x;
            double y_diff = p1.y - p2.y;
            return Math.Sqrt(x_diff * x_diff + y_diff * y_diff);
        }

        public void save_to_txt(double[][] results)
        {
            string filename = "polygon_results.txt";
            using (StreamWriter writer = new(filename))
            {
                writer.WriteLine("Ilość laserów: " + n_lasers);
                writer.WriteLine("Ilość wielokątów: " + polygons.Length);

                for (int i = 0; i < polygons.Length; i++)
                {
                    writer.WriteLine("Wielokąt numer " + (i + 1) + ": ");

                    foreach (var polygon in polygons[i])
                    {
                        writer.Write($"({polygon.x}, {polygon.y}), ");
                    }

                    writer.Write("materiał: " + materials[i] + '\n');
                }

                writer.WriteLine("Straty energii:");
                foreach (double[] line in results)
                {
                    foreach (double y in line)
                    {
                        writer.Write(y + " ");
                    }
                    writer.WriteLine();
                }
                writer.Close();
            }
            Console.WriteLine("Wyniki zapisano do: " + filename);
        }
    }
}

