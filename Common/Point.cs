namespace Common
{
    public class Point : IEquatable<Point>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            if (obj is not Point point)
                throw new ArgumentException("Object is not a Point.");

            return this.X == point.X && this.Y == point.Y;
        }

        public override int GetHashCode()
        {
            int hashX = X.GetHashCode();

            int hashY = Y.GetHashCode();

            return hashX ^ hashY;
        }

        public bool Equals(Point obj)
        {
            if (obj == null)
                return false;

            return this.X == obj.X && this.Y == obj.Y;
        }
    }
}