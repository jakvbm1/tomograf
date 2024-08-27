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

namespace tomograf.shapes
{
    class Polygon : IShape
    {
        private readonly IList<Point> vertices;
        private readonly double material;

        public Polygon(IList<Point> vertices, double material)
        {
            this.vertices = vertices;
            this.material = material;
        }

        public double CalcEnergyLoss(double laserA, double laserB)
        {
            // Szukanie przecięć z krawędziami
            var intersectionPoints = new List<Point>();
            for (int i = 1; i < this.vertices.Count; i++)
            {
                var p = GetIntersectionPointOnEdge(this.vertices[i - 1], this.vertices[i], laserA, laserB);
                if (p != null)
                {
                    intersectionPoints.Add(p);
                }
            }

            var ip = GetIntersectionPointOnEdge(this.vertices[^1], this.vertices[0], laserA, laserB);
            if (ip != null)
            {
                intersectionPoints.Add(ip);
            }

            // Szukanie przecięć z wierzchołkami
            for (int i = 0; i < this.vertices.Count; i++)
            {
                var currVertex = this.vertices[i];
                var prevVertex = this.vertices[(i - 1 + this.vertices.Count) % this.vertices.Count];
                var nextVertex = this.vertices[(i + 1) % this.vertices.Count];
                if (CheckIntersectionWithVertex(currVertex, prevVertex, nextVertex, laserA, laserB))
                {
                    intersectionPoints.Add(currVertex);
                }
            }

            if (intersectionPoints.Count % 2 == 1)
            {
                throw new Exception("Something is not yes 🤔");
            }

            intersectionPoints.Sort((p1, p2) => p1.x.CompareTo(p2.x));

            double distanceInPolygon = 0;
            for (int i = 1; i < intersectionPoints.Count; i += 2)
            {
                distanceInPolygon += CalcDistance(intersectionPoints[i - 1], intersectionPoints[i]);
            }

            return distanceInPolygon * material;
        }

        private static Point? GetIntersectionPointOnEdge(Point p1, Point p2, double laserA, double laserB)
        {
            if (p1.x == p2.x)
            {
                double Y = p1.x * laserA + laserB;
                if (Y >= Math.Max(p1.y, p2.y) || Y <= Math.Min(p1.y, p2.y))
                {
                    return null;
                }

                return new Point { x = p1.x, y = Y };
            }

            double edgeA = CalcA(p1.x, p1.y, p2.x, p2.y);
            double edgeB = CalcB(edgeA, p1.x, p1.y);

            if (edgeA == laserA)
            {
                return null;
            }

            double x = (laserB - edgeB) / (edgeA - laserA);
            double y = x * edgeA + edgeB;

            if (x >= Math.Max(p1.x, p2.x) || x <= Math.Min(p1.x, p2.x))
            {
                return null;
            }

            return new Point { x = x, y = y };
        }

        private static double CalcA(double x1, double y1, double x2, double y2)
        {
            return (y2 - y1) / (x2 - x1);
        }

        private static double CalcB(double a, double x, double y)
        {
            return y - a * x;
        }

        private static bool CheckIntersectionWithVertex(Point currVertex, Point prevVertex, Point nextVertex, double laserA, double laserB)
        {
            if (laserA * currVertex.x + laserB != currVertex.y)
            {
                return false;
            }

            // Uznajemy przecięcie tylko gdy poprzedni i następny wierzchołek są po różnych stronach lasera
            // Jeśli p1 i p2 mają przeciwne znaki, oznacza to, że punkty leżą po przeciwnych stronach prostej
            double p1 = laserA * prevVertex.x + laserB - prevVertex.y;
            double p2 = laserA * nextVertex.x + laserB - nextVertex.y;
            return p1 * p2 < 0;
        }

        private static double CalcDistance(Point p1, Point p2)
        {
            double xDiff = p1.x - p2.x;
            double yDiff = p1.y - p2.y;
            return Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
        }

        public string ShapeDescription()
        {
            string vert = "";

            foreach (var vertex in vertices)
            {
                vert += $"({vertex.x}, {vertex.y}), ";
            }

            return $"Polygon material: {material}, vertices: [{vert}]";
        }
    }
}

