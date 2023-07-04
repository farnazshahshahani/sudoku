using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuWFA
{

    public class SudokuForm : Form
    {
        private const int BoardSize = 9;
        private const int CellSize = 50;
        private const int BoardPadding = 50;
        private const int FormPadding = 50;
        int[,] solvedBoard = new int[9, 9];

        private TextBox[,] textBoxes;

        public SudokuForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            textBoxes = new TextBox[BoardSize, BoardSize];

            Text = "Sudoku";
            ClientSize = new Size(BoardSize * CellSize + 2 * BoardPadding + FormPadding, BoardSize * CellSize + 2 * BoardPadding + FormPadding + CellSize + 20);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

           
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    TextBox textBox = new TextBox();
                    textBox.Location = new Point(BoardPadding + col * CellSize, BoardPadding + row * CellSize);
                    textBox.Size = new Size(CellSize, CellSize);
                    textBox.TextAlign = HorizontalAlignment.Center;
                    textBox.Font = new Font("Arial", 12, FontStyle.Bold);
                   
                    textBox.ReadOnly = true;

                    textBoxes[row, col] = textBox;
                    Controls.Add(textBox);
                }
            }

            InitializeRandomBoard();

           
            Button checkButton = new Button();
            checkButton.Text = "Check";
            checkButton.Location = new Point(BoardPadding, BoardSize * CellSize + 2 * BoardPadding);
            checkButton.Size = new Size(BoardSize * CellSize, CellSize);
            checkButton.Font = new Font("Arial", 12, FontStyle.Bold);
            checkButton.Click += CheckButton_Click;
            Controls.Add(checkButton);


            Button solveButton = new Button();
            solveButton.Text = "Solve";
            solveButton.Location = new Point(BoardPadding, BoardSize * CellSize + 2 * BoardPadding + CellSize);
            solveButton.Size = new Size(BoardSize * CellSize, CellSize);
            solveButton.Font = new Font("Arial", 12, FontStyle.Bold);
            solveButton.BackColor = Color.Green;
            solveButton.Click += SolveClick;

            Controls.Add(solveButton);


        }

        private void InitializeRandomBoard()
        {
            solvedBoard = new int[,]
            {
                {5, 3, 4, 6, 7, 8, 9, 1, 2},
                {6, 7, 2, 1, 9, 5, 3, 4, 8},
                {1, 9, 8, 3, 4, 2, 5, 6, 7},
                {8, 5, 9, 7, 6, 1, 4, 2, 3},
                {4, 2, 6, 8, 5, 3, 7, 9, 1},
                {7, 1, 3, 9, 2, 4, 8, 5, 6},
                {9, 6, 1, 5, 3, 7, 2, 8, 4},
                {2, 8, 7, 4, 1, 9, 6, 3, 5},
                {3, 4, 5, 2, 8, 6, 1, 7, 9}
            };

            Random random = new Random();

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    TextBox textBox = textBoxes[row, col];
                    int value = solvedBoard[row, col];

                    textBox.Text = value.ToString();

                    if (random.Next(2) == 0)
                    {
                        textBox.ReadOnly = false;
                        textBox.ForeColor = Color.Blue;
                        textBox.Text = string.Empty;
                    }
                    else
                    {
                        textBox.ReadOnly = true;
                        textBox.ForeColor = Color.Black;
                        textBox.BackColor = Color.Gray;
                    }
                }
            }
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            bool hasEmpty = false;
            bool hasIncorrect = false;
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (textBoxes[i, j].BackColor != Color.Gray)
                    {
                        textBoxes[i, j].BackColor = Color.White;
                    }
                }
            }
           
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    if (int.TryParse(textBoxes[row, col].Text, out int value))
                    {
                        if (textBoxes[row, col].Enabled)
                        {
                            if (solvedBoard[row, col] == value)
                            {
                                textBoxes[row, col].ForeColor = Color.Green;
                            }
                            else
                            {
                                textBoxes[row, col].ForeColor = Color.Red;
                                hasIncorrect = true;
                            }
                        }
                    }
                    else 
                    {
                        textBoxes[row, col].BackColor = Color.Red;
                        hasEmpty = true;
                    }
                }
            }

            if (hasEmpty)
            {
                MessageBox.Show("Invalid input! Please enter numbers from 1 to 9.");
                return;
            }
            if (hasIncorrect)
            {
                MessageBox.Show("Sudoku is not solved!");
                return;
            }
            else
            {
                MessageBox.Show("Congratulations! Sudoku has been solved!");
                return;
            }

        }

        public void SolveClick(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (textBoxes[i, j].Text == "")
                    {
                        textBoxes[i, j].Text = solvedBoard[i, j].ToString();
                    }
                    if (int.TryParse(textBoxes[i, j].Text, out int value))
                    {
                        if (value != solvedBoard[i, j])
                        {
                            textBoxes[i, j].Text = solvedBoard[i, j].ToString();
                        }
                    }
                    else
                    {
                        textBoxes[i, j].Text = solvedBoard[i, j].ToString();
                    }
                }
            }
        }

    }


}
