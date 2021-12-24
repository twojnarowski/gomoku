using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gomoku
{
    public partial class Form1 : Form
    {
        Color turn = Color.Black;
        Board board = new Board();
        int score = 0;
        int bestMove = -1;

        public Form1()
        {
            InitializeComponent();
            label1.Text = board.printBoard();
        }



        Boolean changeTurn(int location, Board boardToMove)
        {
            if (turn == Color.Black)
            {
                if (boardToMove.setPlayer(location - 1, 1))
                {
                    if (boardToMove == board)
                    {
                        turn = Color.White;
                        label1.Text = boardToMove.printBoard();
                    }
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                if (boardToMove.setPlayer(location - 1, 2))
                {
                    if (boardToMove == board)
                    {
                        turn = Color.Black;
                    }
                    label1.Text = boardToMove.printBoard();
                    return true;
                }
                return false;
            }
        }

        private void buttonClick(object sender, EventArgs e)
        {
            string buttonText = ((Button)sender).Name;
            string number = new String(buttonText.Where(Char.IsDigit).ToArray());
            int index = int.Parse(number);
            if (changeTurn(index, board))
            {
                int win = 0;
                int draw = 0;
                ((Button)sender).BackColor = turn;
                if (board.checkWin(index - 1))
                {
                    MessageBox.Show("VICTORY of " + turn);
                    win = 1;
                }
                if (board.checkDraw())
                {
                    MessageBox.Show("DRAW");
                    win = 1;
                }
                if (win == 0)
                {
                    //richTextBox1.Clear();

                    int[] next = checkNextMove(-1, board, 1, 1, -1, -1);
                    int res = AIMakeMove(-1, bestMove);

                    foreach (Control l_control in Controls)
                    {
                        if (l_control is Button && l_control.Name == "button" + res)
                        {
                            l_control.BackColor = turn;
                        }
                    }


                    if (board.checkWin(res - 1))
                    {
                        MessageBox.Show("VICTORY of " + turn);
                        win = 1;
                    }
                    if (board.checkDraw())
                    {
                        MessageBox.Show("DRAW");
                        win = 1;
                    }
                }
                if (win == 1)
                {
                    Application.Restart();
                }

                int score = board.calculateFullScore();
                label2.Text = score + " ";

            }
        }

        private int AIMakeMove(int skip, int best)
        {
            if (skip > 0)
            {
                if (changeTurn(skip + 1, board))
                {
                    return skip;
                }
            }
            if (best > 0)
            {
                if (board.isFieldEmpty(best / 15, best % 15))
                {
                    if (changeTurn(best + 1, board))
                    {
                        return best + 1;
                    }
                }
            }
            int minmax = 0;
            int tempminmax = 0;
            List<int> choices = new List<int>();
            List<string> output = new List<string>();
            int location = 0;
            for (int i = 0; i < 225; i++)
            {
                if (i == skip)
                {
                    break;
                }
                if (board.isFieldEmpty(i / 15, i % 15))
                {
                    int[] temp = board.calculateScore(i);
                    for (int j = 0; j < 8; j++)
                    {
                        if (Math.Abs(temp[j]) > tempminmax)
                        {
                            if (temp[j] < 0)
                            {
                                tempminmax = Math.Abs(temp[j]) + 1;
                            }
                            else
                            {
                                tempminmax = Math.Abs(temp[j]);
                            }
                        }
                    }

                    if (tempminmax > minmax)
                    {
                        minmax = tempminmax;
                        choices.Clear();
                        output.Clear();
                        choices.Add(i);
                        output.Add(i + " - " + tempminmax);
                    }
                    else if (tempminmax == minmax)
                    {
                        choices.Add(i);
                        output.Add(i + " - " + tempminmax);
                    }
                    tempminmax = 0;
                }
            }
            location = 0;
            var random = new Random();
            int selector = random.Next(choices.Count);
            location = choices[selector];
            if (changeTurn(location + 1, board))
            {
                return location + 1;
            }
            else
            {
                location = AIMakeMove(location, bestMove);
            }
            return location;

        }


        private int[] checkNextMove(int skip, Board board2, int player, int round, int locked, int locked2)
        {
            int[][] tempboardval = board2.getBoard();
            Board tempboard = new Board(tempboardval);
            int[] res = new int[1];

            if (skip > 0)
            {
                if (changeTurn(skip + 1, tempboard))
                {

                    res[0] = skip;
                    return res;
                }
            }
            int minmax = 0;
            int tempminmax = 0;
            List<int> choices = new List<int>();
            List<string> output = new List<string>();
            int location = 0;
            for (int i = 0; i < 225; i++)
            {
                if (i == skip)
                {
                    break;
                }
                if (tempboard.isFieldEmpty(i / 15, i % 15))
                {
                    int[] temp = tempboard.calculateScore(i);
                    for (int j = 0; j < 8; j++)
                    {
                        if (Math.Abs(temp[j]) > tempminmax)
                        {
                            if (temp[j] < 0)
                            {
                                tempminmax = Math.Abs(temp[j]);
                            }
                            else
                            {
                                tempminmax = Math.Abs(temp[j]);
                            }

                        }
                    }

                    if (tempminmax > minmax)
                    {
                        minmax = tempminmax;
                        choices.Clear();
                        output.Clear();
                        choices.Add(i);
                        output.Add(i + " - " + tempminmax);
                        score = tempminmax;
                    }
                    else if (tempminmax == minmax)
                    {
                        choices.Add(i);
                        output.Add(i + " - " + tempminmax);
                    }
                    tempminmax = 0;
                }
            }
            location = 0;
            var random = new Random();
            //MessageBox.Show("best moves for P" + player + ": " + String.Join(", ", output.ToArray()));
            int selector = random.Next(choices.Count);
            location = choices[selector];
            int tempbestmove = -1;
            int tempbestmove2 = -1;
            if (round == 1)
            {

                foreach (int choice in choices)
                {
                    int choice2 = choice + 1;
                    if (board.isFieldEmpty(choice2 / 15, choice2 % 15))
                    {
                        //richTextBox1.AppendText(choice2 + " ==== \n");
                        checkNextMove(-1, tempboard, 1, 2, choice2, -1);
                        if (Math.Abs(tempbestmove) > Math.Abs(tempbestmove2))
                        {
                            bestMove = choice2;
                        }
                    }
                }
            }
            if (player == 2)
            {
                return choices.ToArray();
            }
            if (player == 1)
            {

                int bestScore = 0;
                foreach (int choice in choices)
                {
                    int choice2 = choice + 1;
                    if (choice2 != locked)
                    {
                        if (board.isFieldEmpty(choice2 / 15, choice2 % 15))
                        {
                            //richTextBox1.AppendText(choice2 + "-----\n");
                            tempboardval = board.getBoard();
                            tempboard = new Board(tempboardval);
                            int[][] tempboardval2 = tempboard.getBoard();
                            Board tempboard2 = new Board(tempboardval2);
                            if (tempboard.setPlayer(choice2, player))
                            {
                                int tempscore = tempboard.calculateFullScore();
                                int[] res2 = checkNextMove(-1, tempboard2, 2, 2, locked, choice2);
                                foreach (int restemp in res2)
                                {
                                    int restemp2 = restemp + 1;
                                    if (restemp2 != choice2 && choice2 != locked && locked != restemp2)
                                    {
                                        //richTextBox1.AppendText(restemp2 + "....\n");
                                        tempscore += tempboard2.calculateFullScore();
                                        if (Math.Abs(tempscore) >= Math.Abs(bestScore))
                                        {
                                            tempbestmove = restemp2;
                                        }
                                        //richTextBox1.AppendText(choice2 + " -> " + restemp2 + " = " + tempscore + "\n");
                                    }
                                    else
                                    {
                                        //richTextBox1.AppendText("same" + restemp2 + " - " + choice2 + " - " + locked+"\n");
                                    }
                                }
                            }
                        }
                    }
                }
            }

            /*foreach (Control l_control in Controls)
            {
                if (l_control is Button)
                {
                    if (l_control.BackColor != Color.White && l_control.BackColor != Color.Black)
                    {
                        l_control.BackColor = default(Color);
                    }
                }
            }
            foreach (Control l_control in Controls)
            {
                foreach (int choice in choices)
                {
                    int temp = choice + 1;
                    if (l_control is Button && l_control.Name == "button" + temp)
                    {
                        if (l_control.BackColor != Color.White && l_control.BackColor != Color.Black)
                        {
                            l_control.BackColor = Color.Red;
                        }
                    }
                    else
                    {
                        if (l_control.BackColor != Color.White && l_control.BackColor != Color.Black && l_control.BackColor != Color.Red)
                        {
                            l_control.BackColor = default(Color);
                        }
                    }

                }
            }*/
            label3.Text = tempboard.printBoard();
            if (tempboard.setPlayer(location + 1, player))
            {
                res[0] = location + 1;
                return res;
            }
            else
            {

                res = checkNextMove(location, tempboard, player, 2, -1, -1);
            }
            return res;
        }
    }
}
