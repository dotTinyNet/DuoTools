using System;
using System.Runtime.InteropServices;

public class MemoryDevice
{
    // P/Invoke declarations for libc functions
    [DllImport("libc", SetLastError = true)]
    private static extern int open(string path, int flags);

    [DllImport("libc", SetLastError = true)]
    private static extern IntPtr mmap(IntPtr addr, UIntPtr length, int prot, int flags, int fd, IntPtr offset);

    [DllImport("libc", SetLastError = true)]
    private static extern int munmap(IntPtr addr, UIntPtr length);

    [DllImport("libc", SetLastError = true)]
    private static extern int close(int fd);

    [DllImport("libc", SetLastError = true)]
    private static extern long sysconf(int name);

    // Constants mirroring Linux definitions
    private const int O_RDWR = 2;         // Read/write mode
    private const int O_SYNC = 0x100000;  // Synchronous I/O (value for Linux)
    private const int PROT_READ = 1;      // Read permission
    private const int PROT_WRITE = 2;     // Write permission
    private const int MAP_SHARED = 1;     // Shared mapping
    private const int _SC_PAGESIZE = 30;  // sysconf parameter for page size

    /// <summary>
    /// Maps a physical memory address to a virtual address.
    /// </summary>
    /// <param name="addr">Physical address to map (ulong to support 64-bit addresses).</param>
    /// <param name="len">Length of the memory region to map (in bytes).</param>
    /// <returns>A tuple containing the virtual address (IntPtr) and file descriptor (int).</returns>
    public static (IntPtr virt_addr, int fd) devm_map(ulong addr, int len)
    {
        // Open /dev/mem with read/write and synchronous I/O
        int fd = open("/dev/mem", O_RDWR | O_SYNC);
        if (fd == -1)
        {
            Console.WriteLine("cannot open '/dev/mem'");
            return (IntPtr.Zero, -1);
        }

        // Get system page size
        long pageSize = sysconf(_SC_PAGESIZE);
        if (pageSize == -1)
        {
            Console.WriteLine("sysconf failed");
            close(fd);
            return (IntPtr.Zero, -1);
        }

        // Calculate page-aligned offset
        ulong offset = addr & ~((ulong)pageSize - 1);

        // Map the memory region
        IntPtr map_base = mmap(IntPtr.Zero, (UIntPtr)(len + (int)(addr - offset)),
                              PROT_READ | PROT_WRITE, MAP_SHARED, fd, (IntPtr)offset);
        if (map_base == (IntPtr)(-1)) // MAP_FAILED in C is -1 when cast to IntPtr
        {
            Console.WriteLine("mmap failed");
            close(fd);
            return (IntPtr.Zero, -1);
        }

        // Calculate the virtual address corresponding to the physical address
        IntPtr virt_addr = IntPtr.Add(map_base, (int)(addr - offset));
        return (virt_addr, fd);
    }

    /// <summary>
    /// Unmaps a previously mapped memory region and closes the file descriptor.
    /// </summary>
    /// <param name="virt_addr">Virtual address returned by devm_map.</param>
    /// <param name="len">Length of the memory region originally mapped.</param>
    /// <param name="fd">File descriptor returned by devm_map.</param>
    public static void devm_unmap(IntPtr virt_addr, int len, int fd)
    {
        if (fd == -1)
        {
            Console.WriteLine("'/dev/mem' is closed");
            return;
        }

        long pageSize = sysconf(_SC_PAGESIZE);
        if (pageSize == -1)
        {
            Console.WriteLine("sysconf failed");
            close(fd);
            return;
        }

        // Convert virt_addr to ulong for arithmetic
        ulong virt_addr_ulong = (ulong)virt_addr.ToInt64();
        // Calculate page-aligned address
        ulong page_aligned_addr = virt_addr_ulong & ~((ulong)pageSize - 1);
        IntPtr map_base = (IntPtr)page_aligned_addr;
        int offset_in_page = (int)(virt_addr_ulong - page_aligned_addr);

        // Unmap the memory region
        munmap(map_base, (UIntPtr)(len + offset_in_page));
        close(fd);
    }

    /// <summary>
    /// Reads a 32-bit value from a physical memory address.
    /// </summary>
    /// <param name="addr">Physical address to read from.</param>
    /// <returns>The 32-bit value read, or 0 if mapping fails.</returns>
    public static uint ReadUint32(ulong addr)
    {
        var (virt_addr, fd) = devm_map(addr, 4); // 4 bytes for uint32_t
        if (virt_addr == IntPtr.Zero)
        {
            Console.WriteLine("readl addr map failed");
            return 0;
        }

        uint val;
        unsafe
        {
            val = *(uint*)virt_addr;
        }
        devm_unmap(virt_addr, 4, fd);
        return val;
    }

    /// <summary>
    /// Writes a 32-bit value to a physical memory address.
    /// </summary>
    /// <param name="addr">Physical address to write to.</param>
    /// <param name="val">The 32-bit value to write.</param>
    public static void WriteUint32(ulong addr, uint val)
    {
        var (virt_addr, fd) = devm_map(addr, 4); // 4 bytes for uint32_t
        if (virt_addr == IntPtr.Zero)
        {
            Console.WriteLine("writel addr map failed");
            return;
        }

        unsafe
        {
            *(uint*)virt_addr = val;
        }
        devm_unmap(virt_addr, 4, fd);
    }
}