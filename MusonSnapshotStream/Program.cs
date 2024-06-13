using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusonSnapshotStream {
	internal static class Program {
		
		/*
		 * MusonSnapshotStream was developed by group 21
		 * for the course "Lab on Offensive Computer Security" @ TU/e
		 * Please use responsibily.
		 */

		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Setup());
		}
	}
}
