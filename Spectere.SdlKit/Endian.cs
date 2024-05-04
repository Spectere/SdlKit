namespace Spectere.SdlKit;

/// <summary>
/// Represents the endianness of a structure.
/// </summary>
public enum Endian {
    /// <summary>
    /// Little-endian. When this is used, a word will be stored in memory starting with the least significant byte and
    /// ending with the most significant byte. On these systems, the value 0x12345678 will be stored in memory as
    /// 78 56 34 12. This is natively used for most consumer processor architectures (x86, most ARM implementations,
    /// etc.).
    /// </summary>
    LittleEndian,
    
    /// <summary>
    /// Big-endian. When this is used, a word will be stored in memory starting with the most significant byte and
    /// ending with the least significant byte. On these systems, the value 0x12345678 will be stored in memory as
    /// 12 34 56 78. This is natively used for some processor architectures (PowerPC, MIPS, etc.) and in networking
    /// protocols.
    /// </summary>
    BigEndian
}
