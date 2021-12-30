#include "pch.h"
#include "file_access_test.h"

#include <codecvt>
#include <string>

#if defined(__cplusplus)
extern "C" {
#endif
 
static std::wstring convertToUTF16 (const std::string& utf8String)
{
  static std::wstring_convert<std::codecvt_utf8_utf16 <unsigned short>, unsigned short> convert;
  return std::wstring((wchar_t*)convert.from_bytes(utf8String).c_str());
}

int checkFileAccess(const char* file_path)
{
  // Just ask for read access to existing file; this fails for OneDrive files
  // with a number of other arguments, though.
  DWORD desiredAccess = GENERIC_READ;
  DWORD creationDisposition = OPEN_EXISTING;

  auto handle = CreateFile2FromAppW(convertToUTF16(file_path).c_str(),
                                    desiredAccess,
                                    FILE_SHARE_READ | FILE_SHARE_WRITE,
                                    creationDisposition,
                                    nullptr);
  int res = 0;
  bool success = handle != INVALID_HANDLE_VALUE;
  if (success)
    CloseHandle(handle);
  else
    res = GetLastError();
  // Returns "0" on success; otherwise, error
  return success ? 0 : 1;
}

#if defined(__cplusplus)
} // extern "C"
#endif

