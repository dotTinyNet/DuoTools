namespace DuoLibrary;

static public class Device
{

    static IDevice? _instance = null;

    static object _lockObject = new object();

    static Device()
    {
    }

    static IDevice Instance
    {
        get
        {
            lock (_lockObject)
            {
                if (_instance == null)
                {
                    try
                    {
                        _instance = new DuoS();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Unable to initalise");
                        throw;
                    }
                }

                return _instance;
            }
        }
    }

    static public GPIOPin? GetPin(string name)
    {
        GPIOPin? pin = null;

        Instance.GPIOList.TryGetValue(name.ToUpper(), out pin);
        return pin;
    }


    static public Dictionary<string, GPIOPin> GPIOList
    {
        get
        {
            return Instance.GPIOList;
        }
    }

    static public uint BaseAddress_Pinmux
    {
        get
        {
            return Instance.BaseAddress_Pinmux;
        }
    }

    static public uint MaxOffset
    {
        get
        {
            return Instance.MaxOffset;
        }
    }



}