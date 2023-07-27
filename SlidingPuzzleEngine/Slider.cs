using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidingPuzzleEngine
{
    public class Slider
    {
        private readonly SliderOrderedCollection owner;
        private readonly Position desiredPosition;

        internal Slider(Position desiredPosition, SliderOrderedCollection owner)
        {
            this.owner = owner;
            this.desiredPosition = desiredPosition;
        }

        public Position DesiredPosition { get { return desiredPosition; } }

        public Position GetActualPosition()
        {
            Position result;

            bool positionIsFound = this.owner.TryGetActualPositionForSlider(this, out result);

            if (positionIsFound)
            {
                return result;
            }
            else
            {
                throw new InvalidOperationException("This slider is not part of a game.");
            }
        }

        public bool TryMove(SlideDirection slideDirection)
        {
            return this.owner.TryMoveSlider(this, slideDirection);
        }

        public bool TryMove(out SlideDirection slideDirection)
        {
            SlideDirection[] directions = Enum.GetValues<SlideDirection>();

            foreach(SlideDirection direction in directions)
            {
                if(this.TryMove(direction))
                {
                    slideDirection = direction;
                    return true;
                }
            }

            slideDirection = default;
            return false;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Slider other)
            {
                return false;
            }

            return this.desiredPosition.Equals(other.desiredPosition) &&
                this.owner.Equals(other.owner);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.owner.GetHashCode(), this.desiredPosition.GetHashCode());
        }
    }
}
