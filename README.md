## OneDrive + UWP + Native C++ file access repro

# Description:

The Windows CreateFile2FromAppW function is designed to be used in UWP apps to access files from C++ that are outside of the app's protected directory but within locations given by a "Future Access List".  This works for files outside of an active OneDrive folder, but not from inside (at least not from many Windows systems).

This project is a simple repro of this issue. It is built and tested in VS2019, and the issue noted has been seen on both Windows 10 and Windows 11 systems.

# Repro steps:

Build and run the "OneDriveFileAccessRepro" project in the contained solution. Press the button, and select a .txt file. If selecting a file in a OneDrive folder on affected systems, the dialog will indicate an error, demonstrating the issue.  Otherwise, the dialog will indicate success.

# Additional notes:

- If OneDrive is turned off, and a file is created after OneDrive is turned off, the C++ code can access this file.  It becomes inaccessible after OneDrive is turned back on.
- These files are accessible via the C# StorageFile objects that are used to generate the "Path" string passed to the C++ code.
- Other cloud providers, such as DropBox, do not have an issue with this.
- Several SDK versions, including Win11 SDKs, have been tested and have this same issue.
- The file does not need to be a .txt file; this is just for demonstration
- If the future access list contains a folder, this same behavior occurs for files within the folder.  However, new files _can_ be created.
