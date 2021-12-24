using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gomoku
{
    class Board
    {
        int size = 15;
        int[][] board = new int[15][];

        public Board()
        {
            for (int i = 0; i < 15; i++)
            {
                board[i] = new int[15];
                for (int j = 0; j < 15; j++)
                {
                    board[i][j] = 0;
                }
            }
        }

        public Board(int[][] vals)
        {
            board = vals;
        }

        public string printBoard()
        {
            string output = "";
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    output += board[i][j];
                    output += " ";
                }
                output += "\n";
            }
            return output;
        }

        public Boolean setPlayer(int location, int player)
        {
            int x = location / 15;
            int y = location % 15;
            if (isFieldEmpty(x, y))
            {
                board[x][y] = player;
                return true;
            }
            return false;
        }

        public Boolean isFieldEmpty(int x, int y)
        {
            if (board[x][y] == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean checkWin(int index)
        {
            int x = index / 15;
            int y = index % 15;
            int ones = 0;
            int twos = 0;
            for (int i = 0; i < 15; i++)
            {
                if (board[x][i] == 1)
                {
                    ones++;
                    twos = 0;
                    if (ones == 5)
                    {
                        return true;
                    }
                }
                else if (board[x][i] == 2)
                {
                    twos++;
                    ones = 0;
                    if (twos == 5)
                    {
                        return true;
                    }
                }
                else
                {
                    ones = 0;
                    twos = 0;
                }
            }
            ones = 0;
            twos = 0;
            for (int i = 0; i < 15; i++)
            {
                if (board[i][y] == 1)
                {
                    ones++;
                    twos = 0;
                    if (ones == 5)
                    {
                        return true;
                    }
                }
                else if (board[i][y] == 2)
                {
                    twos++;
                    ones = 0;
                    if (twos == 5)
                    {
                        return true;
                    }
                }
                else
                {
                    ones = 0;
                    twos = 0;
                }
            }
            ones = 0;
            twos = 0;
            int xstart = 0;
            int ystart = 0;
            if (x > y)
            {
                ystart = 0;
                xstart = x - y;
            }
            else
            {
                xstart = 0;
                ystart = y - x;
            }
            for (int i = 0; i < 15; i++)
            {
                if (xstart + i > 14 || ystart + i > 14)
                {
                    break;
                }
                if (board[xstart + i][ystart + i] == 1)
                {
                    ones++;
                    twos = 0;
                    if (ones == 5)
                    {
                        return true;
                    }
                }
                else if (board[xstart + i][ystart + i] == 2)
                {
                    twos++;
                    ones = 0;
                    if (twos == 5)
                    {
                        return true;
                    }
                }
                else
                {
                    ones = 0;
                    twos = 0;
                }
            }
            ones = 0;
            twos = 0;
            xstart = 0;
            ystart = 0;
            if (x + y < 14)
            {
                ystart = 0;
                xstart = y + x;
            }
            else if (x + y == 14)
            {
                ystart = 0;
                xstart = 14;
            }
            else
            {
                xstart = 14;
                ystart = x + y - 14;
            }
            for (int i = 0; i < 15; i++)
            {
                if (xstart - i < 0 || ystart + i > 14)
                {
                    break;
                }
                if (board[xstart - i][ystart + i] == 1)
                {

                    ones++;
                    twos = 0;
                    if (ones == 5)
                    {
                        return true;
                    }
                }
                else if (board[xstart - i][ystart + i] == 2)
                {
                    twos++;
                    ones = 0;
                    if (twos == 5)
                    {
                        return true;
                    }
                }
                else
                {
                    ones = 0;
                    twos = 0;
                }
            }
            return false;
        }

        public bool checkDraw()
        {
            int empty = 0;
            for (int i = 0; i < 225; i++)
            {
                int x = i / 15;
                int y = i % 15;
                if (isFieldEmpty(x, y))
                {
                    empty = 1;
                }
            }
            if(empty == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int[] calculateScore(int location)
        {
            int x = location / 15;
            int y = location % 15;
            int score0 = 0;
            int score1 = 0;
            int score2 = 0;
            int score3 = 0;
            int score4 = 0;
            int score5 = 0;
            int score6 = 0;
            int score7 = 0;

            for (int i = 1; i < 15; i++)
            {
                if (y - 1 < 0 || x - i < 0)
                {
                    break;
                }
                else
                {
                    if (board[x - 1][y - 1] == 2)
                    {
                        try
                        {
                            if (board[x - i][y - i] == 2)
                            {
                                score7 += 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else if (board[x - 1][y - 1] == 1)
                    {
                        try
                        {
                            if (board[x - i][y - i] == 1)
                            {
                                score7 -= 1;
                            }
                            else
                            {
                                break;
                            }
                        }

                        catch (Exception)
                        {
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            for (int i = 1; i < 15; i++)
            {
                if (y + i > 14 || x + i > 14)
                {
                    break;
                }
                else
                {
                    if (board[x + 1][y + 1] == 2)
                    {
                        try
                        {
                            if (board[x + i][y + i] == 2)
                            {
                                score3 += 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else if (board[x + 1][y + 1] == 1)
                    {
                        try
                        {
                            if (board[x + i][y + i] == 1)
                            {
                                score3 -= 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            for (int i = 1; i < 15; i++)
            {
                if (y - i < 0 || x + i > 14)
                {
                    break;
                }
                else
                {
                    if (board[x + 1][y - 1] == 2)
                    {
                        try
                        {
                            if (board[x + i][y - i] == 2)
                            {
                                score5 += 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else if (board[x + 1][y - 1] == 1)
                    {
                        try
                        {
                            if (board[x + i][y - i] == 1)
                            {
                                score5 -= 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            for (int i = 1; i < 15; i++)
            {
                if (y + i > 14 || x - i < 0)
                {
                    break;
                }
                else
                {
                    if (board[x - 1][y + 1] == 2)
                    {
                        try
                        {
                            if (board[x - i][y + i] == 2)
                            {
                                score1 += 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else if (board[x - 1][y + 1] == 1)
                    {
                        try
                        {
                            if (board[x - i][y + i] == 1)
                            {
                                score1 -= 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            for (int i = 1; i < 15; i++)
            {
                if (y + i > 14)
                {
                    break;
                }
                else
                {
                    if (board[x][y + 1] == 2)
                    {
                        try
                        {
                            if (board[x][y + i] == 2)
                            {
                                score2 += 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else if (board[x][y + 1] == 1)
                    {
                        try
                        {
                            if (board[x][y + i] == 1)
                            {
                                score2 -= 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            for (int i = 1; i < 15; i++)
            {
                if (y - i < 0)
                {
                    break;
                }
                else
                {
                    if (board[x][y - 1] == 2)
                    {
                        try
                        {
                            if (board[x][y - i] == 2)
                            {
                                score6 += 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else if (board[x][y - 1] == 1)
                    {
                        try
                        {
                            if (board[x][y - i] == 1)
                            {
                                score6 -= 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            for (int i = 1; i < 15; i++)
            {
                if (x + i > 14)
                {
                    break;
                }
                else
                {
                    if (board[x + 1][y] == 2)
                    {
                        try
                        {
                            if (board[x + i][y] == 2)
                            {
                                score4 += 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else if (board[x + 1][y] == 1)
                    {
                        try
                        {
                            if (board[x + i][y] == 1)
                            {
                                score4 -= 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            for (int i = 1; i < 15; i++)
            {
                if (x - i < 0)
                {
                    break;
                }
                else
                {
                    if (board[x - 1][y] == 2)
                    {
                        try
                        {
                            if (board[x - i][y] == 2)
                            {
                                score0 += 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else if (board[x - 1][y] == 1)
                    {
                        try
                        {
                            if (board[x - i][y] == 1)
                            {
                                score0 -= 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if ((score0 > 0 && score4 > 0) || (score0 < 0 && score4 < 0))
            {
                score0 = score0 + score4;
                score4 = score0;
            }
            if ((score1 > 0 && score5 > 0) || (score1 < 0 && score5 < 0))
            {
                score1 = score1 + score5;
                score5 = score1;
            }
            if ((score2 > 0 && score6 > 0) || (score1 < 0 && score5 < 0))
            {
                score2 = score2 + score6;
                score6 = score2;
            }
            if ((score3 > 0 && score7 > 0) || (score1 < 0 && score5 < 0))
            {
                score3 = score3 + score7;
                score7 = score3;
            }
            return new[] { score0, score1, score2, score3, score4, score5, score6, score7 };
        }

        public int returnPlayer(int x, int y)
        {
            return (int)board[x][y];
        }

        public int[] possibleMoves()
        {
            List<int> moves = new List<int>();
            for (int i = 0; i < 225; i++)
            {
                int x = i / 15;
                int y = i % 15;
                if (board[x][y] == 0)
                {
                    moves.Add(i);
                }
            }
            return moves.ToArray();
        }

        public int[][] getBoard()
        {
            int[][] temp = new int[15][];
            for (int i = 0; i < 15; i++)
            {
                temp[i] = new int[15];
                Array.Copy(board[i], temp[i], 15);
            }
            return temp;
        }

        public void setBoard(int[][] board2)
        {
            board = board2;
        }

        public int calculateFullScore()
        {
            int score = 0;
            for (int i = 0; i < 225; i++)
            {
                int temp = 0;
                int[] scoretemp = calculateScore(i);
                for (int j = 0; j < 8; j++)
                {

                    if (Math.Abs(scoretemp[j]) >= Math.Abs(temp))
                    {
                        temp = scoretemp[j];
                    }

                }
                score += (int)Math.Pow(temp, 3);
            }

            return score;
        }
    }
}

