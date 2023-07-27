using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidingPuzzleEngine
{
    public class Game
    {
        private const int MinimalGamePlotSize = 2;

        private readonly int width;
        private readonly int height;

        private readonly SliderOrderedCollection sliderOrderedCollection;

        public Game(int width, int height)
        {
            Utilities.ThrowExceptionIfLessThan(width, MinimalGamePlotSize, "width");
            Utilities.ThrowExceptionIfLessThan(height, MinimalGamePlotSize, "height");

            this.width = width;
            this.height = height;

            this.sliderOrderedCollection = new SliderOrderedCollection(width, height);
            this.InitializeGame();
        }

        public int Width { get { return width; } }

        public int Height { get { return height; } }

        public IEnumerable<Slider> Sliders
        {
            get
            {
                return this.sliderOrderedCollection.AllSliders;
            }
        }

        public bool GameIsSolved()
        {
            return this.sliderOrderedCollection.AreAllSlidersOnDesiredPositions();
        }

        private void InitializeGame()
        {
            Dictionary<Position, Position> initialPositions = GamePositionRandomizer.GetActualPositionToDesiredPosition(this.width, this.height);

            foreach (var positionPair in initialPositions)
            {
                this.sliderOrderedCollection.TryAdd(positionPair.Value, positionPair.Key, out _);
            }
        }
    }
}
