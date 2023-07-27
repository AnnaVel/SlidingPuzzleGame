using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidingPuzzleEngine
{
    internal static class GamePositionRandomizer
    {
        public static Dictionary<Position,Position> GetActualPositionToDesiredPosition(int width, int height)
        {
            List<Position> allPositions = CreateAllPositions(width, height);
            Dictionary<Position, Position> result = new Dictionary<Position, Position>();

            for (int i = 0; i < allPositions.Count; i++)
            {
                Position desiredPosition = allPositions[i];
                AssignNewPositionToActualPosition(width, height, ref result, i, desiredPosition);
            }

            return result;
        }

        private static void AssignNewPositionToActualPosition(int width, int height, ref Dictionary<Position, Position> result, int i, Position desiredPosition)
        {
            Random random = new Random();
            int currentRandom = random.Next(1, width * height - i);

            int counter = 1;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if(result.Keys.Contains(new Position(x, y)))
                    {
                        continue;
                    }
                    
                    if (counter == currentRandom)
                    {
                        result.Add(new Position(x, y), desiredPosition);
                        return;
                    }

                    counter++;
                }
            }
        }

        private static List<Position> CreateAllPositions(int width, int height)
        {
            List<Position> allPositions = new List<Position>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Position position = new Position(x, y);
                    allPositions.Add(position);
                }
            }

            allPositions.RemoveAt(allPositions.Count - 1);

            return allPositions;
        }
    }
}
