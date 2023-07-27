using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidingPuzzleEngine
{
    public struct Position
    {
        private readonly int x;
        private readonly int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X { get { return x; } }
        public int Y { get { return y; } }

        /// <summary>
        /// This method creates a new position based on the old position.
        /// </summary>
        /// <param name="slideDirection">The direction.</param>
        /// <returns>The new position.</returns>
        public Position Move(SlideDirection slideDirection)
        {
            switch (slideDirection)
            {
                case SlideDirection.Left:
                    return new Position(this.x - 1, this.y);
                case SlideDirection.Right:
                    return new Position(this.x + 1, this.y);
                case SlideDirection.Up:
                    return new Position(this.x, this.y + 1);
                case SlideDirection.Down:
                    return new Position(this.x, this.y - 1);
                default:
                    throw new InvalidOperationException("No such direction.");
            }
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is not Position)
            {
                return false;
            }

            Position other = (Position)obj;

            return this.x == other.X &&
                this.y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.x, this.y);
        }

        public static bool operator ==(Position first, Position second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Position first, Position second)
        {
            return !first.Equals(second);
        }

        public override string ToString()
        {
            return string.Format("X: {0}, Y: {1}", this.X, this.Y);
        }
    }
}
