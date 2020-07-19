using System;
using System.Windows.Forms;

namespace NOModUninstaller {
	public partial class Abort : Form {
		public Abort () {
			InitializeComponent();
		}

		private void Abort_Load (object sender, EventArgs e) {
			AbortInformation.Text = string.Format(AbortInformation.Text, Mod.Name);
		}

		private void CloseButton_Click (object sender, EventArgs e) {
			Close();
			
		}
	}
}
