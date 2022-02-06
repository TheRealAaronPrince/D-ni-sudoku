using System;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
namespace d_ni_sudoku
{
	class Program
	{
		static int difficulty = 0;
		static Random rand = new Random();
		static int[,] grid = new int[25, 25];
		static string s;
		static string[] dniNumbers = new string[]{"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y"};
		public static void Init(ref int[,] grid)
		{
			for (int i = 0; i < 25; i++)
			{
				for (int j = 0; j < 25; j++)
				{
					grid[i, j] = (i * 5 + i / 5 + j) % 25 + 1;

				}
			}
		}
		public static void Draw(ref int[,] grid, out string _s)
		{
			for (int x = 0; x < 25; x++)
			{
				for (int y = 0; y < 25; y++)
				{
					s += dniNumbers[grid[x, y]-1];
				}
			}
			_s = s;
			s = "";
		}

		public static void ChangeTwoCell(ref int[,] grid, int findValue1, int findValue2)
		{
			int xParm1, yParm1, xParm2, yParm2;
			xParm1 = yParm1 = xParm2 = yParm2 = 0;
			for (int i = 0; i < 25; i += 5)
			{
				for (int k = 0; k < 25; k += 5)
				{
					for (int j = 0; j < 5; j++)
					{
						for (int z = 0; z < 5; z++)
						{
							if (grid[i + j, k + z] == findValue1)
							{
								xParm1 = i + j;
								yParm1 = k + z;

							}
							if (grid[i + j, k + z] == findValue2)
							{
								xParm2 = i + j;
								yParm2 = k + z;

							}
						}
					}
					grid[xParm1, yParm1] = findValue2;
					grid[xParm2, yParm2] = findValue1;
				}
			}
		}

		public static void Update(ref int[,] grid, int shuffleLevel)
		{
			for (int repeat = 0; repeat < shuffleLevel; repeat++)
			{
				Random rand = new Random(Guid.NewGuid().GetHashCode());
				Random rand2 = new Random(Guid.NewGuid().GetHashCode());
				ChangeTwoCell(ref grid, rand.Next(1, 25), rand2.Next(1, 25));
			}
		}
		public static void Main(string[] args)
		{
			Console.WriteLine("pick difficulty (0-50)");
			try
			{
				difficulty = 625 - (400 - 2*(Convert.ToInt32(Console.ReadLine())+50));
			}
			catch (Exception)
			{	
				difficulty = 625 - (400 - 2*75);
			}
			if(difficulty < 375)
			{
				difficulty = 375;
			}
			if(difficulty > 425)
			{
				difficulty = 425;
			}
			s = "";
			string ç1kt1;
			Init(ref grid);
			Update(ref grid, 26);
			Draw(ref grid, out ç1kt1);
			int i = 0;
			string puzzle = Convert.ToString(ç1kt1);
			while(i < difficulty)
			{
				int x = rand.Next(puzzle.Length);
				if(puzzle[x] != (Convert.ToChar("_")))
				{
					StringBuilder sb = new StringBuilder(puzzle);
					sb[x] = Convert.ToChar("_");
					puzzle = Convert.ToString(sb);
					i++;
				}
			}
			Bitmap background = (Bitmap)Image.FromFile("graphics/sudoku back.png");
			var output = new Bitmap(background.Width,background.Height,PixelFormat.Format32bppArgb);
			var graphics = Graphics.FromImage(output);
			graphics.DrawImage(background,0,0);
			DateTime now = DateTime.Now;
			for(int bmpX = 0; bmpX < 25; bmpX++)
			{
				for(int bmpY = 0; bmpY < 25; bmpY++)
				{
					Bitmap number = (Bitmap)Image.FromFile("graphics/" + puzzle[bmpX*25+bmpY] +".png");
					graphics.DrawImage(number,bmpX*18 + 24,bmpY*18 + 6);
				}
			}
			output.Save("sudoku (" + now.ToFileTimeUtc() + ").png",ImageFormat.Png);
			for(int bmpX = 0; bmpX < 25; bmpX++)
			{
				for(int bmpY = 0; bmpY < 25; bmpY++)
				{
					Bitmap number = (Bitmap)Image.FromFile("graphics/" + ç1kt1[bmpX*25+bmpY] +".png");
					graphics.DrawImage(number,bmpX*18 + 24,bmpY*18 + 6);
				}
			}
			output.Save("sudoku solved (" + now.ToFileTimeUtc() + ").png",ImageFormat.Png);
		}
	}
}