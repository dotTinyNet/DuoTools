namespace DuoLibrary;

class DuoS : IDevice
{

    public Dictionary<string, GPIOPin> GPIOList { get; init; }

    public uint BaseAddress_Pinmux { get; } = 0x03001000;
    public uint MaxOffset { get; } = 0x1b8;

    public DuoS()
    {
        GPIOList = new Dictionary<string, GPIOPin>();

        foreach (var gpioPin in PinList)
        {
            GPIOList.Add(gpioPin.Name, gpioPin);
        }
    }


    private List<GPIOPin> PinList
    {
        get
        {
            return new List<GPIOPin>()
            {

                new GPIOPin(0x40, "A16", new Dictionary<uint, string>()
                {
                    { 0, "UART0_TX" },
                    { 1, "CAM_MCLK1" },
                    { 2, "PWM_4" },
                    { 3, "A16" },
                    { 4, "UART1_TX" },
                    { 5, "AUX1" },
                    { 7, "DBG_6" }
                }), //"UART0_TX"

                new GPIOPin(0x44, "A17", new Dictionary<uint, string>()

                {
                    { 0, "UART0_RX" },
                    { 1, "CAM_MCLK0" },
                    { 2, "PWM_5" },
                    { 3, "A17" },
                    { 4, "UART1_RX" },
                    { 5, "AUX0" },
                    { 7, "DBG_7" }
                }),

                new GPIOPin(0x64, "A19", new Dictionary<uint, string>() //"JTAG_CPU_TMS"
                {
                    { 0, "CV_2WTMS_CR_SDA0" },
                    { 1, "CAM_MCLK0" },
                    { 2, "PWM_13" },
                    { 3, "A19" },
                    { 4, "UART1_RTS" },
                    { 5, "AUX0" },
                    { 6, "UART1_TX" },
                    { 7, "VO_D_28" }
                }),

                new GPIOPin(0x68, "A18", new Dictionary<uint, string>() //"JTAG_CPU_TCK"),
                {
                    { 0, "CV_2WTCK_CR_4WTCK" },
                    { 1, "CAM_MCLK1" },
                    { 2, "PWM_6" },
                    { 3, "A18" },
                    { 4, "UART1_RX" },
                    { 5, "AUX1" },
                    { 6, "UART1_RX" },
                    { 7, "VO_D_29" }
                }),

                new GPIOPin(0x6c, "A20", new Dictionary<uint, string>() //"JTAG_CPU_TRST"),
                {
                    { 0, "JTAG_CPU_TRST" },
                    { 3, "A20" },
                    { 6, "VO_D_30" }
                }),

                new GPIOPin(0x70, "A28", new Dictionary<uint, string>()
            {
                { 0, "CV_SCL0" },
                { 1, "UART1_TX" },
                { 2, "UART2_TX" },
                { 3, "A28" },
                { 5, "WG0_D0" },
                { 7, "DBG_10" }
            }),

            new GPIOPin(0x74, "A29", new Dictionary<uint, string>()
            {
                { 0, "CV_SDA0" },
                { 1, "UART1_RX" },
                { 2, "UART2_RX" },
                { 3, "A29" },
                { 5, "WG0_D1" },
                { 6, "WG1_D0" },
                { 7, "DBG_11" }
            }),

            new GPIOPin(0xa4, "E0", new Dictionary<uint, string>()
            {
                { 0, "E0" },
                { 1, "UART2_TX" },
                { 2, "PWR_UART0_RX" },
                { 4, "PWM_8" }
            }),

            new GPIOPin(0xa8, "E1", new Dictionary<uint, string>()
            {
                { 0, "E1" },
                { 1, "UART2_RX" },
                { 3, "EPHY_LNK_LED" },
                { 4, "PWM_9" },
                { 5, "PWR_IIC_SCL" },
                { 6, "IIC2_SCL" },
                { 7, "CV_4WTMS_CR_SDA0" }
            }),

            new GPIOPin(0xac, "E2", new Dictionary<uint, string>()
            {
                { 0, "E2" },
                { 2, "PWR_SECTICK" },
                { 3, "EPHY_SPD_LED" },
                { 4, "PWM_10" },
                { 5, "PWR_IIC_SDA" },
                { 6, "IIC2_SDA" },
                { 7, "CV_4WTCK_CR_2WTCK" }
            }),

            new GPIOPin(0xf0, "B1", new Dictionary<uint, string>()
            {
                { 1, "CAM_MCLK0" },
                { 2, "IIC4_SCL" },
                { 3, "B1" },
                { 4, "PWM_12" },
                { 5, "EPHY_LNK_LED" },
                { 6, "WG2_D0" },
                { 7, "UART3_TX" }
            }),

            new GPIOPin(0xf0, "B2", new Dictionary<uint, string>()
            {
                { 1, "CAM_MCLK1" },
                { 2, "IIC4_SDA" },
                { 3, "B2" },
                { 4, "PWM_13" },
                { 5, "EPHY_SPD_LED" },
                { 6, "WG2_D1" },
                { 7, "UART3_RX" }
            }),

            new GPIOPin(0xf8, "B3", new Dictionary<uint, string>()
            {
                { 3, "B3" },
                { 4, "KEY_COL2" }
            }),

            new GPIOPin(0x134, "B11", new Dictionary<uint, string>()
            {
                { 0, "PWM_1" },
                { 1, "VI1_D_10" },
                { 2, "VO_D_23" },
                { 3, "B11" },
                { 4, "RMII0_IRQ" },
                { 5, "CAM_MCLK0" },
                { 6, "IIC1_SDA" },
                { 7, "UART2_TX" }
            }),

            new GPIOPin(0x134, "B12", new Dictionary<uint, string>()
            {
                { 0, "PWM_2" },
                { 1, "VI1_D_9" },
                { 2, "VO_D_22" },
                { 3, "B12" },
                { 5, "CAM_MCLK1" },
                { 6, "IIC1_SCL" },
                { 7, "UART2_RX" }
            }),

            new GPIOPin(0x13c, "B13", new Dictionary<uint, string>()
            {
                { 0, "PWM_3" },
                { 1, "VI1_D_8" },
                { 2, "VO_D_21" },
                { 3, "B13" },
                { 4, "RMII0_MDIO" },
                { 5, "SPI3_SDO" },
                { 6, "IIC2_SCL" },
                { 7, "CAM_VS0" }
            }),

            new GPIOPin(0x140, "B14", new Dictionary<uint, string>()
            {
                { 0, "VI2_D_7" },
                { 1, "VI1_D_7" },
                { 2, "VO_D_20" },
                { 3, "B14" },
                { 4, "RMII0_RXD1" },
                { 5, "SPI3_SDI" },
                { 6, "IIC2_SDA" },
                { 7, "CAM_HS0" }
            }),

            new GPIOPin(0x144, "B15", new Dictionary<uint, string>()
            {
                { 0, "VI2_D_6" },
                { 1, "VI1_D_6" },
                { 2, "VO_D_19" },
                { 3, "B15" },
                { 4, "RMII0_REFCLKI" },
                { 5, "SPI3_SCK" },
                { 6, "UART2_TX" },
                { 7, "CAM_VS0" }
            }),

            new GPIOPin(0x148, "B16", new Dictionary<uint, string>()
            {
                { 0, "VI2_D_5" },
                { 1, "VI1_D_5" },
                { 2, "VO_D_18" },
                { 3, "B16" },
                { 4, "RMII0_RXD0" },
                { 5, "SPI3_CS_X" },
                { 6, "UART2_RX" },
                { 7, "CAM_HS0" }
            }),

            new GPIOPin(0x148, "B17", new Dictionary<uint, string>()
            {
                { 0, "VI2_D_4" },
                { 1, "VI1_D_4" },
                { 2, "VO_D_17" },
                { 3, "B17" },
                { 4, "RMII0_MDC" },
                { 5, "IIC1_SDA" },
                { 6, "UART2_CTS" },
                { 7, "CAM_VS0" }
            }),

            new GPIOPin(0x150, "B18", new Dictionary<uint, string>()
            {
                { 0, "VI2_D_3" },
                { 1, "VI1_D_3" },
                { 2, "VO_D_16" },
                { 3, "B18" },
                { 4, "RMII0_TXD0" },
                { 5, "IIC1_SCL" },
                { 6, "UART2_RTS" },
                { 7, "CAM_HS0" }
            }),

            new GPIOPin(0x154, "B19", new Dictionary<uint, string>()
            {
                { 0, "VI2_D_2" },
                { 1, "VI1_D_2" },
                { 2, "VO_D_15" },
                { 3, "B19" },
                { 4, "RMII0_TXD1" },
                { 5, "CAM_MCLK1" },
                { 6, "PWM_2" },
                { 7, "UART2_TX" }
            }),

            new GPIOPin(0x158, "B20", new Dictionary<uint, string>()
            {
                { 0, "VI2_D_1" },
                { 1, "VI1_D_1" },
                { 2, "VO_D_14" },
                { 3, "B20" },
                { 4, "RMII0_RXDV" },
                { 5, "IIC3_SDA" },
                { 6, "PWM_3" },
                { 7, "IIC4_SCL" }
            }),

            new GPIOPin(0x15c, "B21", new Dictionary<uint, string>()
            {
                { 0, "VI2_D_0" },
                { 1, "VI1_D_0" },
                { 2, "VO_D_13" },
                { 3, "B21" },
                { 4, "RMII0_TXCLK" },
                { 5, "IIC3_SCL" },
                { 6, "WG1_D0" },
                { 7, "IIC4_SDA" }
            }),

            new GPIOPin(0x160, "B22", new Dictionary<uint, string>()
            {
                { 0, "VI2_CLK" },
                { 1, "VI1_CLK" },
                { 2, "VO_CLK1" },
                { 3, "B22" },
                { 4, "RMII0_TXEN" },
                { 5, "CAM_MCLK0" },
                { 6, "WG1_D1" },
                { 7, "UART2_RX" }
            }),

            new GPIOPin(0x194, "C18", new Dictionary<uint, string>()
            {
                { 0, "VI0_D_15" },
                { 1, "SD1_CLK" },
                { 2, "VO_D_24" },
                { 3, "C18" },
                { 4, "CAM_MCLK1" },
                { 5, "PWM_12" },
                { 6, "IIC1_SDA" },
                { 7, "DBG_18" }
            }),

            new GPIOPin(0x198, "C19", new Dictionary<uint, string>()
            {
                { 0, "VI0_D_16" },
                { 1, "SD1_CMD" },
                { 2, "VO_D_25" },
                { 3, "C19" },
                { 4, "CAM_MCLK0" },
                { 5, "PWM_13" },
                { 6, "IIC1_SCL" },
                { 7, "DBG_19" }
            }),

            new GPIOPin(0x198, "C20", new Dictionary<uint, string>()
            {
                { 0, "VI0_D_17" },
                { 1, "SD1_D0" },
                { 2, "VO_D_26" },
                { 3, "C20" },
                { 4, "IIC2_SDA" },
                { 5, "PWM_14" },
                { 6, "IIC1_SDA" },
                { 7, "CAM_VS0" }
            }),

            new GPIOPin(0x198, "C21", new Dictionary<uint, string>()
            {
                { 0, "VI0_D_18" },
                { 1, "SD1_D1" },
                { 2, "VO_D_27" },
                { 3, "C21" },
                { 4, "IIC2_SCL" },
                { 5, "PWM_15" },
                { 6, "IIC1_SCL" },
                { 7, "CAM_HS0" }
            }),

            new GPIOPin(0x1a4, "C16", new Dictionary<uint, string>()
            {
                { 0, "CV_4WTMS_CR_SDA0" },
                { 1, "VI0_D_13" },
                { 2, "VO_D_0" },
                { 3, "C16" },
                { 4, "IIC1_SDA" },
                { 5, "PWM_8" },
                { 6, "SPI0_SCK" },
                { 7, "SD1_D2" }
            }),

            new GPIOPin(0x1a8, "C17", new Dictionary<uint, string>()
            {
                { 0, "CV_4WTDI_CR_SCL0" },
                { 1, "VI0_D_14" },
                { 2, "VO_CLK0" },
                { 3, "C17" },
                { 4, "IIC1_SCL" },
                { 5, "PWM_9" },
                { 6, "SPI0_CS_X" },
                { 7, "SD1_D3" }
            }),

            new GPIOPin(0x1ac, "C14", new Dictionary<uint, string>()
            {
                { 0, "CV_4WTDO_CR_2WTMS" },
                { 1, "VI0_D_11" },
                { 2, "VO_D_2" },
                { 3, "C14" },
                { 4, "IIC2_SDA" },
                { 5, "PWM_10" },
                { 6, "SPI0_SDO" },
                { 7, "DBG_14" }
            }),

            new GPIOPin(0x1b0, "C15", new Dictionary<uint, string>()
            {
                { 0, "CV_4WTCK_CR_2WTCK" },
                { 1, "VI0_D_12" },
                { 2, "VO_D_1" },
                { 3, "C15" },
                { 4, "IIC2_SCL" },
                { 5, "PWM_11" },
                { 6, "SPI0_SDI" },
                { 7, "DBG_15" }
            }),

            new GPIOPin(0x1b0, "C12", new Dictionary<uint, string>()
            {
                { 1, "VI0_D_9" },
                { 2, "VO_D_4" },
                { 3, "C12" },
                { 4, "CAM_MCLK1" },
                { 5, "PWM_14" },
                { 6, "CAM_VS0" },
                { 7, "DBG_12" }
            }),

            new GPIOPin(0x1b8, "C13", new Dictionary<uint, string>()
            {
                { 1, "VI0_D_10" },
                { 2, "VO_D_3" },
                { 3, "C13" },
                { 4, "CAM_MCLK0" },
                { 5, "PWM_15" },
                { 6, "CAM_HS0" },
                { 7, "DBG_13" }
            })
        };

        }
    }



}