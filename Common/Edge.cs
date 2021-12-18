namespace Common
{
    public class Edge
    {
        public Vertex A { get; set; }
        public Vertex B { get; set; }

        public Edge()
        { }

        public Edge(Vertex a, Vertex b)
        {
            A = a;
            B = b;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            if (obj is not Edge e)
                throw new ArgumentException("Object is not an Edge.");

            return this.A.Equals(e.A) && this.B.Equals(e.B);
        }

        public override int GetHashCode()
        {
            int hashX = A.GetHashCode();

            int hashY = B.GetHashCode();

            return hashX ^ hashY;
        }

        public bool Equals(Edge e)
        {
            if (e == null)
                return false;

            return this.A.Equals(e.A) && this.B.Equals(e.B);
        }
    }
}