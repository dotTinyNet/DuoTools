

using DuoLibrary;

public class Program
{

    public static int Main(string[] args)
    {


        // PinMux                       List pins.
        // PinMux <PIN>                 Pin Function.
        // PinMux <PIN> <FUNCTION>      Set Pin Function

        if (args.Length == 0)
        {
            foreach (var keyValuePair in Device.GPIOList)
            {
                Console.WriteLine(keyValuePair.Value.ToString());
            }
        }
        else if (args.Length > 0)
        {
            var pin = Device.GetPin(args[0]);

            if (pin == null)
            {
                Console.WriteLine("Pin not found.");
                return 1;
            }

            if (args.Length == 2)
            {
                pin.WriteValue(args[1]);
            }

            Console.WriteLine(pin.ToString());
        }

        return 0;
    }

}