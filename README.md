# AMSI-ETW-Patch
this repo contains information to patch AMSI and ETW using a single byte patch for both.

The idea was to reduce detection of the patch itself.

# AMSI

the idea is that AMSI perform a lot of validation check before hitting the critical AMSI "check" code. You can simply toggle one of the `jz` for a `jnz` and vice versa.

# ETW

Instead of patching EtwWriteEvent simply patch the syscall NtTraceEvent which is called by a lot of functions.

