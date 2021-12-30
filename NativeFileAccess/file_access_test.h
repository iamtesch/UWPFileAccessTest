#pragma once

#if defined(__cplusplus)
extern "C" {
#endif

// Checks ability to open the file using CreateFile2FromApp; returns "0" on success,
// or any other value on error.
__declspec(dllexport) int checkFileAccess(const char* file_path);

#if defined(__cplusplus)
} // extern "C"
#endif
