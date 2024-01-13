
namespace Dota2GSI.Nodes.Helpers
{
    /// <summary>
    /// Struct representing 2D vectors.
    /// </summary>
    public struct Vector2D
    {
        /// <summary>
        /// The X component of the vector.
        /// </summary>
        public int X;

        /// <summary>
        /// The Y component of the vector.
        /// </summary>
        public int Y;

        /// <summary>
        /// Default constructor with given X and Y coordinates.
        /// </summary>
        /// <param name="x">The X component of the vector.</param>
        /// <param name="y">The Y component of the vector.</param>
        public Vector2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Equates this Vector2D object to another object.
        /// </summary>
        /// <param name="obj">The other object to compare against.</param>
        /// <returns>True if the two objects are equal, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            return obj is Vector2D other &&
                   X == other.X &&
                   Y == other.Y;
        }

        /// <summary>
        /// Calculates unique hash code for this object.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[X: {X}, Y: {Y}]";
        }
    }
}
