using System.ComponentModel.DataAnnotations;

namespace WOCS.Domain.Enums
{
    public enum LynxDeviceGroup
    {
        [Display(Name = "AT Modem")]
        ATModem = 1,

        [Display(Name = "Carrier Sensor")]
        CarrierSensor = 2,

        [Display(Name = "LTM Sensor")]
        LTMSensor = 3,

        [Display(Name = "AT Surface Modem")]
        ATSurfaceModem = 4,

        [Display(Name = "E-Line Modem")]
        ELineModem = 5,

        [Display(Name = "EM Subsea Modem")]
        EMSubseaModem = 6,

        [Display(Name = "EM Downhole Modem")]
        EMDownholeModem = 7,

        [Display(Name = "EM Downhole Sensor")]
        EMDownholeSensor = 8,

        [Display(Name = "Fluid Sampler")]
        FluidSampler = 9,

        [Display(Name = "Downhole Fluid ID")]
        DownholeFluidID = 10,

        [Display(Name = "Dual Valve")]
        DualValve = 11,

        [Display(Name = "LUI EMX")]
        LUIEMX = 12,

        [Display(Name = "LUI SLB")]
        LUISLB = 13,

        [Display(Name = "Downhole Hub")]
        DownholeHub = 14,

        [Display(Name = "In-Tree Controller")]
        InTreeController = 15,

        [Display(Name = "Seabed Controller")]
        SeabedController = 16,

        [Display(Name = "Wireless Valve")]
        WirelessValve = 17
    }
}
