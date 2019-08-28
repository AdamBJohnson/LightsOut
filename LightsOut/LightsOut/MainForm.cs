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
        private int CellLength;
        private LightsOutGame game;

        public MainForm()
        {
            InitializeComponent();

            game = new LightsOutGame();
            
            CellLength = GridLength / game.GridSize;
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGameButton_Click(sender, e);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for(int r = 0; r < game.GridSize; r++)
            {
                for (int c = 0; c < game.GridSize; c++)
                {
                    Brush brush;
                    Pen pen;

                    if(game.GetGridValue(r,c))
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
            if(e.X < GridOffSet || e.X > CellLength * game.GridSize + GridOffSet ||
                e.Y < GridOffSet || e.Y > CellLength * game.GridSize + GridOffSet)
            {
                return;
            }

            int r = (e.Y - GridOffSet) / CellLength;
            int c = (e.X - GridOffSet) / CellLength;

            game.Move(r, c);
            this.Invalidate();

            if(game.IsGameOver())
            {
                MessageBox.Show(this, "Congratulations! You've won!", "Lights Out!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            game.NewGame();

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
            game.changeGridSize(4);
            CellLength = GridLength / game.GridSize;
            this.Invalidate();
        }

        private void X3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.changeGridSize(3);
            CellLength = GridLength / game.GridSize;
            this.Invalidate();
        }

        private void X5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.changeGridSize(5);
            CellLength = GridLength / game.GridSize;
            this.Invalidate();
        }
    }
}
