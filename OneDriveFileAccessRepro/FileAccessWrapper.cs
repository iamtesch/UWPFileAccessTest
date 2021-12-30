using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace OneDriveFileAccessRepro
{
    // Helper to wrap native calls to DLL
    unsafe class FileAccessWrapper
    {
        #region DLL externs
        [DllImport("NativeFileAccess.dll", CallingConvention = CallingConvention.Cdecl)]
        private extern static int checkFileAccess(string path);
        #endregion

        public static bool hasNativeAccessTo(StorageFile file)
        {
            return checkFileAccess(file.Path) == 0;
        }
    }
}
