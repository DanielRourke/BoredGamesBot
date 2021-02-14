using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoredGamesBot.Games.Common
{
    public abstract class Board<T> where T : Move
    {
        protected int width;
        protected int height;
        protected int[,] boardState;

        protected Board(int h = 3, int w = 3)
        {
            height = h;
            width = w;
            boardState = new int[height, width];
			SetBoardState(65);
		}
        public virtual void SetBoardState(int value = 0)
        {

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    boardState[i, j] = value;
                }
            }
        }

        public virtual void SetBoardState(int[,] newBoardState)
        {
            {

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        boardState[i, j] = newBoardState[i, j];
                    }
                }
            }
		}

	//	

		//TOOD Impore Layout
        public override string ToString()
		{
			
			string retString = "'''\n";
			//top numbers
			retString += "\u0009";
			retString += "\u2004";
			for (int i = 0; i < width; i++)
			{
				retString += "\u0009";
				retString += "\u2004";
				if (i < 10)
				{
					retString += "\u0009" ;
				}
				retString += (char)(i + 'A') + AddSpacesToChar(i + 'A');
			}
			//
			retString += '\n';

			//start next line 
			//draw top line
			retString += "\u0009";
			retString += "\u3000" + "\u2554";
			


			for (int i = 0; i < width; i++)
			{
				retString += "\u2550" + "\u2550" + "\u2550";
				if (i < width - 1)
				{
					retString += "\u2566";
				}
			}

			retString += "\u2557";
			//retString += '\n';

			//draw line with board values

			for (int i = 0; i < height; i++)
			{

				retString += '\n';
				retString += AddChar(i);


				
				retString += "\u0009";

				for (int j = 0; j < width; j++)
				{
					retString += "\u2551" + "\u2004" + "\u2004";
					retString += ConvertToSymbol(boardState[i, j]);
					retString += "\u2004" + "\u2005";
				}

				retString += "\u2551";
				retString += '\n';
				//draw line in the middle

				retString += "\u0009";
				if (i < height - 1)
				{

					retString += "\u3000"  + "\u2560";

					for (int j = 0; j < width; j++)
					{
						retString += "\u2550" + "\u2550" + "\u2550";
						if (j < width - 1)
						{
							retString += "\u256C";
						}
						else
						{
							retString += "\u2563";
						}
					}

				}


		
			}

			//draw last line
			retString +=  "\u3000" + "\u255A";

			for (int i = 0; i < width; i++)
			{
				retString += "\u2550" + "\u2550" + "\u2550";
				if (i < width - 1)
				{
					retString += "\u2569";
				}
			}
			retString += "\u255D" + '\n' + "'''";

			return retString;
		}


		public virtual string ConvertToSymbol(int s)
        {
			if (s == -1)
				return "\u0009";
			else if (s <10)
				return s.ToString() + AddSpacesToChar(s);
			else
				return (char)s + AddSpacesToChar(s);
        }

		public virtual string AddChar(int s)
		{
				return s.ToString() + AddSpacesToChar(s);
		}

		public virtual string AddSpacesToChar(int s)
		{
			//One fourth of an em wide
			if (s == 0)
				return "\u200A" + "\u200A" + "\u2009" + "\u200A";
			//One half of an em wide
			else if (s == 1)
				return "\u2002" + "\u2009";
			//One third of an em wide
			else if (s == 2)
				return "\u2004" + "\u2009";
			//Hair Space + thin Space
			if (s == 65)
				return "\u200A" + "\u2009";
			//One third of an em wide
			else if (s == 66)
				return "\u2004";
			//two thin space
			else if (s == 67)
				return "\u2009" + "\u2009";
			else if (s == 88)
				return "\u2009" + "\u2009";
			else
				return s.ToString();

			/*
					1/5, 1/6
			"\u2009"


			 */
		}

		public abstract void UpdateBoard(T move);
        public abstract bool ValidMove(T move);
        public abstract List<T> GetPossibleMoves();

		public int[,] GetBoardState()
		{
			return boardState;
		}
    }
}
