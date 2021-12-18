namespace Common
{
    public class Vertex : IEquatable<Vertex>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vertex(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            if (obj is not Vertex v)
                throw new ArgumentException("Object is not a Point.");

            return this.X == v.X && this.Y == v.Y;
        }

        public override int GetHashCode()
        {
            int hashX = X.GetHashCode();

            int hashY = Y.GetHashCode();

            return hashX ^ hashY;
        }

        public bool Equals(Vertex v)
        {
            if (v == null)
                return false;

            return this.X == v.X && this.Y == v.Y;
        }
    }
}