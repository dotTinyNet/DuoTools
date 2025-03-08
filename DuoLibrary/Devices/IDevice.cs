namespace DuoLibrary;

public interface IDevice
{

    public Dictionary<string, GPIOPin> GPIOList { get; }

    uint BaseAddress_Pinmux { get; }
    uint MaxOffset { get; }


}