using System.Runtime.Serialization;

namespace Seatmap.Models.Enums
{
    public enum ImageElementType
    {
        [EnumMember(Value = "left-airplane-wing")]
        LeftAirplaneWing,
        [EnumMember(Value = "right-airplane-wing")]
        RightAirplaneWing,
        [EnumMember(Value = "airplane-body")]
        AirplaneBody,
        [EnumMember(Value = "airplane-tailless-body")]
        AirplaneTaillessBody,
        [EnumMember(Value = "airplane-tail-left")]
        AirplaneTailLeft,
        [EnumMember(Value = "airplane-tail-right")]
        AirplaneTailRight,
        [EnumMember(Value = "airplane-face")]
        AirplaneFace,
        [EnumMember(Value = "airplane-empty-body")]
        AirplaneEmptyBody,
        [EnumMember(Value = "helicopter-body-with-doors-and-face")]
        HelicopterBodyWithDoorsAndFace,
        [EnumMember(Value = "helicopter-body-with-face")]
        HelicopterBodyWithFace,
        [EnumMember(Value = "helicopter-empty-body")]
        HelicopterEmptyBody,
        [EnumMember(Value = "helicopter-full")]
        HelicopterFull,
        [EnumMember(Value = "helicopter-no-propeller")]
        HelicopterNoPropeller,
        [EnumMember(Value = "helicopter-face")]
        HelicopterFace,
        [EnumMember(Value = "helicopter-propeller")]
        HelicopterPropeller,
        [EnumMember(Value = "helicopter-big-left-door")]
        HelicopterBigLeftDoor,
        [EnumMember(Value = "helicopter-big-right-door")]
        HelicopterBigRightDoor,
        [EnumMember(Value = "helicopter-tail")]
        HelicopterTail,
        [EnumMember(Value = "helicopter-small-right-door")]
        HelicopterSmallRightDoor,
        [EnumMember(Value = "helicopter-small-left-door")]
        HelicopterSmallLeftDoor,
        [EnumMember(Value = "left-wing-short")]
        LeftWingShort,
        [EnumMember(Value = "right-wing-short")]
        RightWingShort,
        [EnumMember(Value = "helicopter-tail-short")]
        HelicopterTailShort,
    }
}