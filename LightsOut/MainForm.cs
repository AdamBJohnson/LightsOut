using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut
{
    public partial class MainForm : Form
    {
        private const int GridOffSet = 25;
        private const int GridLength = 200;
        private int NumCells;
        private int CellLength;

        private bool[,] grid;
        private Random rand;
        public MainForm()
        {
            InitializeComponent();

            rand = new Random();
            NumCells = 3;
            CellLength = GridLength / NumCells;
            grid = new bool[NumCells, NumCells];

            for(int r = 0; r < NumCells; r++)
            {
                for (int c = 0; c < NumCells; c++)
                {
                    grid[r, c] = true;
                }
            }
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGameButton_Click(sender, e);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for(int r = 0; r < NumCells; r++)
            {
                for (int c = 0; c < NumCells; c++)
                {
                    Brush brush;
                    Pen pen;

                    if(grid[r, c])
                    {
                        pen = Pens.Black;
                        brush = Brushes.White;
                    }
                    else
                    {
                        pen = Pens.White;
                        brush = Brushes.Black;
                    }

                    int x = c * CellLength + GridOffSet;
                    int y = r * CellLength + GridOffSet;

                    g.DrawRectangle(pen, x, y, CellLength, CellLength);
                    g.FillRectangle(brush, x + 1, y + 1, CellLength - 1, CellLength - 1);
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.X < GridOffSet || e.X > CellLength * NumCells + GridOffSet ||
                e.Y < GridOffSet || e.Y > CellLength * NumCells + GridOffSet)
            {
                return;
            }

            int r = (e.Y - GridOffSet) / CellLength;
            int c = (e.X - GridOffSet) / CellLength;

            for(int i = r-1; i <= r+1; i++)
            {
                for(int j = c-1; j <= c+1; j++)
                {
                    if(i >= 0 && i < NumCells && j >= 0 && j < NumCells)
                    {
                        grid[i, j] = !grid[i, j];
                    }
                }
            }
            this.Invalidate();

            if(PlayerWon())
            {
                MessageBox.Show(this, "Congratulations! You've won!", "Lights Out!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool PlayerWon()
        {
            for(int i = 0; i < NumCells; ++i)
            {
                for(int j = 0; j < NumCells; ++j)
                {
                    if(grid[i,j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            for(int r = 0; r < NumCells; r++)
            {
                for (int c = 0; c < NumCells; c++)
                {
                    grid[r, c] = rand.Next(2) == 1;
                }
            }

            this.Invalidate();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutBox = new AboutForm();
            aboutBox.ShowDialog(this);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void X4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(sender == x3ToolStripMenuItem)
            {
                NumCells = 3;
                CellLength = GridLength / NumCells;
                x3ToolStripMenuItem.Checked = true;
                x4ToolStripMenuItem.Checked = false;
                x5ToolStripMenuItem.Checked = false;
            }
            else if(sender == x4ToolStripMenuItem)
            {
                NumCells = 4;
                CellLength = GridLength / NumCells;
                x3ToolStripMenuItem.Checked = false;
                x4ToolStripMenuItem.Checked = true;
                x5ToolStripMenuItem.Checked = false;
            }
            else if(sender == x5ToolStripMenuItem)
            {
                NumCells = 5;
                CellLength = GridLength / NumCells;
                x3ToolStripMenuItem.Checked = false;
                x4ToolStripMenuItem.Checked = false;
                x5ToolStripMenuItem.Checked = true;
            }
            grid = new bool[NumCells, NumCells];
            for (int r = 0; r < NumCells; r++)
            {
                for (int c = 0; c < NumCells; c++)
                {
                    grid[r, c] = true;
                }
            }
            this.Invalidate();
        }

        private void X3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            X4ToolStripMenuItem_Click(sender, e);
        }

        private void X5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            X4ToolStripMenuItem_Click(sender, e);
        }
    }
}
