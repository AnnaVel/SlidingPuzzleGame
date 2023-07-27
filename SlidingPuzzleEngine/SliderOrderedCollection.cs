using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidingPuzzleEngine
{
    internal class SliderOrderedCollection
    {
        private readonly int width;
        private readonly int height;

        private readonly Dictionary<Position, Slider> actualPositionToSliderCollection;
        private readonly Dictionary<Slider, Position> sliderToActualPositionCollection;

        public SliderOrderedCollection(int width, int height)
        {
            this.width = width;
            this.height = height;

            this.actualPositionToSliderCollection= new Dictionary<Position, Slider>();
            this.sliderToActualPositionCollection= new Dictionary<Slider, Position>();
        }

        public IEnumerable<Slider> AllSliders
        {
            get
            {
                return this.sliderToActualPositionCollection.Keys;
            }
        }

        internal bool TryAdd(Position desiredPosition, Position actualPosition, out Slider? resultSlider) 
        {
            resultSlider = null;

            if(this.actualPositionToSliderCollection.ContainsKey(actualPosition))
            {
                return false;
            }

            resultSlider = new Slider(desiredPosition, this);

            if(this.sliderToActualPositionCollection.ContainsKey(resultSlider))
            {
                return false;
            }

            this.actualPositionToSliderCollection.Add(actualPosition, resultSlider);
            this.sliderToActualPositionCollection.Add(resultSlider, actualPosition);

            return true;
        }

        internal bool TryGetSliderFromActualPosition(Position actualPosition, out Slider? slider)
        {
            if (!actualPositionToSliderCollection.ContainsKey(actualPosition))
            {
                slider = null;
                return false;
            }

            slider = actualPositionToSliderCollection[actualPosition];
            return true;
        }

        internal bool TryGetActualPositionForSlider(Slider slider, out Position position)
        {
            if(!sliderToActualPositionCollection.ContainsKey(slider))
            {
                position = new Position(0, 0);
                return false;
            }

            position = sliderToActualPositionCollection[slider];
            return true;
        }

        internal bool TryMoveSlider(Slider slider, SlideDirection slideDirection)
        {
            Position currentPosition = this.sliderToActualPositionCollection[slider];
            Position newPosition = currentPosition.Move(slideDirection);
            
            if(!this.ValidatePosition(newPosition))
            {
                return false;
            }

            if(this.actualPositionToSliderCollection.ContainsKey(newPosition))
            {
                return false;
            }

            this.actualPositionToSliderCollection.Add(newPosition, slider);
            this.actualPositionToSliderCollection.Remove(currentPosition);
            this.sliderToActualPositionCollection[slider] = newPosition;
            return true;
        }

        internal bool AreAllSlidersOnDesiredPositions()
        {
            bool result = true;

            foreach (KeyValuePair<Slider, Position> sliderToPosition in this.sliderToActualPositionCollection)
            {
                Slider slider = sliderToPosition.Key;
                Position currentPosition = sliderToPosition.Value;

                if(slider.DesiredPosition != currentPosition)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private bool ValidatePosition(Position position)
        {
            if(position.X < 0 || position.X >= this.width ||
                position.Y < 0 || position.Y >= this.height )
            {
                return false;
            }

            return true;
        }
    }
}
