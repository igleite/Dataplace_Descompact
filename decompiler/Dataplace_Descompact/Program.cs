using System;
using System.Windows.Forms;
using Dataplace_Descompact;

namespace Dataplace_Descompact_Custom
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
