using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace NOModUninstaller {
	public static class Mod {
		public static string Name {
			get; private set;
		}

		public static IO.PathContainer FileListPath {
			get; private set;
		}

		public static IO.PathContainer ConfigPath {
			get; private set;
		}

		public static FileListType FileList {
			get; private set;
		}

		static Mod () {
			Name = Properties.Settings.Default.ModName;
			string fileListFileName = Properties.Settings.Default.FileListFileName;
			FileListPath = new IO.PathContainer(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), fileListFileName));
			ConfigPath = new IO.PathContainer(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), Path.GetFileName(Application.ExecutablePath) + ".config"));

			try {
				if(File.Exists(FileListPath.GetPath())) {
					FileList = (FileListType)XML.Read<FileListType>(File.ReadAllText(FileListPath.GetPath()));
				}
			} catch { }
		}

		public static List<IO.PathContainer> GetValidFilePaths () {
			List<IO.PathContainer> validFilePaths = new List<IO.PathContainer>();

			if(FileList == null) {
				return validFilePaths;
			}

			validFilePaths.AddRange(IO.SearchForFilesNamed(Paths.ModsPath.ToString(), FileList.UniqueFileNames, 2));

			for(int fileIndex = 0; fileIndex < FileList.FilePaths.Length; fileIndex++) {
				IO.PathContainer filePath = new IO.PathContainer(Path.Combine(Paths.ModsPath.GetPath(), FileList.FilePaths[fileIndex]));

				if(File.Exists(filePath.ToString()) && !validFilePaths.Contains(filePath)) {
					validFilePaths.Add(filePath);
				}
			}

			return validFilePaths;
		}

		public class FileListType {
			public string[] FilePaths {
				get; set;
			} = new string[0];

			public string[] UniqueFileNames {
				get; set;
			} = new string[0];
		}
	}
}
