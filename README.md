# AMSI-ETW-Patch
this repo contains information to patch AMSI and ETW using a single byte patch for both.

The idea was to limit detection of the patch itself since it's a single byte.

# AMSI (patch-amsi-x64.c)

The idea is that AMSI perform a lot of validation check before hitting the critical AMSI "check" code. You can simply toggle one of the `jz` for a `jnz` and vice versa.


![amsi1](https://github.com/Mr-Un1k0d3r/AMSI-ETW-Patch/blob/main/amsi-flow-1.png?raw=true)

The red arrow in tthe figure above is showing where the critical code is located.

![amsi2](https://github.com/Mr-Un1k0d3r/AMSI-ETW-Patch/blob/main/amsi-flow-2.png?raw=true)

Example of checks that can be toggled to avoid calling the critical code.

In this case we patch the `jnz` after the `cmp dword ptr [rbx], 49534d41h`.

the patch is simply Address of `AmsiScanBuffer + 167 = 0x74 (x64)`

# ETW (patch-etw-x64.c)

Instead of patching `EtwEventWrite` simply patch the syscall `NtTraceEvent` which is called by a lot of functions.

![etw1](https://github.com/Mr-Un1k0d3r/AMSI-ETW-Patch/blob/main/etw-flow-1.png?raw=true)

As shown in the figure below `NtTraceEvent` is used by a lot of functions within `ntdll.dll`


![etw1](https://github.com/Mr-Un1k0d3r/AMSI-ETW-Patch/blob/main/etw-func.png?raw=true)

The patch is simply force a return when the `NtTraceEvent` function is called `NtTraceEvent = 0xc3 (x64)`

