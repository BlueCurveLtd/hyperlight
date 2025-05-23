using System.Runtime.InteropServices;

namespace Hyperlight;

public enum ErrorCode
{
    NoError = 0,
    UnsupportedParameterType = 2,
    GuestFunctionNameNotProvided = 3,
    GuestFunctionNotFound = 4,
    GuestFunctionIncorrecNoOfParameters = 5,
    DispatchFunctionPointerNotSet = 6,
    OutbError = 7,
    UnknownError = 8,
    StackOverflow = 9,
    GsCheckFailed = 10,
    TooManyGuestFunctions = 11,
    FailureInDlmalloc = 12,
    MallocFailed = 13,
    GuestFunctionParameterTypeMismatch = 14,
    GuestError = 15,
    ArrayLengthParamIsMissing = 16
}

public enum Level
{
    Error = 1,
    Warn,
    Info,
    Debug,
    Trace
}

public enum ParameterType
{
    Int,
    UInt,
    Long,
    ULong,
    Float,
    Double,
    String,
    Bool,
    VecBytes
}

public enum ReturnType
{
    Int,
    UInt,
    Long,
    ULong,
    Float,
    Double,
    String,
    Bool,
    Void,
    VecBytes
}

[StructLayout(LayoutKind.Sequential)]
public struct Vec
{
    public IntPtr data;
    public UIntPtr len;
}

[StructLayout(LayoutKind.Explicit)]
public struct ParameterValue
{
    [FieldOffset(0)]
    public int Int;
    [FieldOffset(0)]
    public uint UInt;
    [FieldOffset(0)]
    public long Long;
    [FieldOffset(0)]
    public ulong ULong;
    [FieldOffset(0)]
    public float Float;
    [FieldOffset(0)]
    public double Double;
    [FieldOffset(0)]
    public bool Bool;
    [FieldOffset(0)]
    public IntPtr String;
    [FieldOffset(0)]
    public Vec VecBytes;
}

[StructLayout(LayoutKind.Sequential)]
public struct Parameter
{
    public ParameterType tag;
    public ParameterValue value;
}

[StructLayout(LayoutKind.Sequential)]
public struct FunctionCall
{
    public IntPtr function_name;
    public IntPtr parameters;
    public UIntPtr parameters_len;
    public ReturnType return_type;
}

public delegate IntPtr CGuestFunc(ref FunctionCall function_call);

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Interop")]
public static partial class HyperlightGuest
{
    [LibraryImport("hyperlight_guest")]    
    public static partial IntPtr c_guest_dispatch_function(ref FunctionCall function_call);

    [LibraryImport("hyperlight_guest")]
    public static partial void hl_register_function_definition(
        [MarshalAs(UnmanagedType.LPStr)] string function_name,
        CGuestFunc func_ptr,
        UIntPtr param_no,
        IntPtr params_type,
        ReturnType return_type);

    [LibraryImport("hyperlight_guest")]
    public static partial void hl_call_host_function(ref FunctionCall function_call);

    [LibraryImport("hyperlight_guest")]
    public static partial void hl_set_error(ErrorCode err, [MarshalAs(UnmanagedType.LPStr)] string message);

    [LibraryImport("hyperlight_guest")]
    public static partial void hl_abort_with_code(int err);

    [LibraryImport("hyperlight_guest")]
    public static partial void hl_abort_with_code_and_message(int err, [MarshalAs(UnmanagedType.LPStr)] string message);

    [LibraryImport("hyperlight_guest")]
    public static partial IntPtr hl_flatbuffer_result_from_Int(int value);

    [LibraryImport("hyperlight_guest")]
    public static partial IntPtr hl_flatbuffer_result_from_UInt(uint value);

    [LibraryImport("hyperlight_guest")]
    public static partial IntPtr hl_flatbuffer_result_from_Long(long value);

    [LibraryImport("hyperlight_guest")]
    public static partial IntPtr hl_flatbuffer_result_from_ULong(ulong value);

    [LibraryImport("hyperlight_guest")]
    public static partial IntPtr hl_flatbuffer_result_from_Float(float value);

    [LibraryImport("hyperlight_guest")]
    public static partial IntPtr hl_flatbuffer_result_from_Double(double value);

    [LibraryImport("hyperlight_guest")]
    public static partial IntPtr hl_flatbuffer_result_from_Void();

    [LibraryImport("hyperlight_guest")]
    public static partial IntPtr hl_flatbuffer_result_from_String([MarshalAs(UnmanagedType.LPStr)] string value);

    [LibraryImport("hyperlight_guest")]
    public static partial IntPtr hl_flatbuffer_result_from_Bytes(IntPtr data, UIntPtr len);

    [LibraryImport("hyperlight_guest")]
    public static partial int hl_get_host_return_value_as_Int();

    [LibraryImport("hyperlight_guest")]
    public static partial uint hl_get_host_return_value_as_UInt();

    [LibraryImport("hyperlight_guest")]
    public static partial long hl_get_host_return_value_as_Long();

    [LibraryImport("hyperlight_guest")]
    public static partial ulong hl_get_host_return_value_as_ULong();

    [LibraryImport("hyperlight_guest")]
    public static partial void hl_log(Level level, [MarshalAs(UnmanagedType.LPStr)] string message, int line, [MarshalAs(UnmanagedType.LPStr)] string file);

    [LibraryImport("hyperlight_guest")]
    public static partial void hyperlight_main();

    [LibraryImport("hyperlight_guest")]
    public static partial void srand(uint seed);

    [LibraryImport("hyperlight_guest")]
    public static partial IntPtr malloc(UIntPtr size);

    [LibraryImport("hyperlight_guest")]
    public static partial IntPtr calloc(UIntPtr nmemb, UIntPtr size);

    [LibraryImport("hyperlight_guest")]
    public static partial void free(IntPtr ptr);

    [LibraryImport("hyperlight_guest")]
    public static partial IntPtr realloc(IntPtr ptr, UIntPtr size);

    [LibraryImport("hyperlight_guest")]
    public static partial void _putchar(char c);

    [LibraryImport("hyperlight_guest")]
    public static partial ulong setjmp(ulong x);

    [LibraryImport("hyperlight_guest")]
    public static partial void longjmp(ulong x, ulong y);
}
