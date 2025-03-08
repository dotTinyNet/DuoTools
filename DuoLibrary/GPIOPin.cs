namespace DuoLibrary;


public class GPIOPin
{


    public uint Offset { get;  init; }
    public string Name  { get; init; }

    public uint Value { get; set; }

    public Dictionary<uint, string> FunctionList;



    public GPIOPin(uint offset, string name, Dictionary<uint, string> functionList)
    {
        Offset = offset;
        Name = name;
        FunctionList = functionList;
    }


    public string Function
    {
        get
        {
            ReadValue();
            if (FunctionList.TryGetValue(Value, out string _function))
            {
                return _function;
            }

            return "UNKNOWN";
        }

    }


    public void ReadValue()
    {
        Value = MemoryDevice.ReadUint32(Device.BaseAddress_Pinmux + Offset);
    }

    public void WriteValue(string function)
    {
        if (uint.TryParse(function, out uint numericFunction))
        {
            WriteValue(numericFunction);
            return;
        }

        var result = FunctionList.FirstOrDefault(kv => kv.Value.Equals(function, StringComparison.OrdinalIgnoreCase));
        if (result.Key != 0 && FunctionList.ContainsKey(result.Key))
        {
            WriteValue(result.Key);
        }
        else
        {
            throw new Exception("Failed to match function");
        }
    }

    public void WriteValue(uint value)
    {
        Value = value;
        MemoryDevice.WriteUint32(Device.BaseAddress_Pinmux + Offset, Value);
    }

    public override string ToString()
    {
        return $"{Name,3} -> {Function,10} ({Value}) [{string.Join(" ", FunctionList.Values)}]";
    }
}