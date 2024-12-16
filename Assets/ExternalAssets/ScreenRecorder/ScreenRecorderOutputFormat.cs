using UnityEngine;

public enum ScreenRecorderOutputFormat
{
    [InspectorName("H.264 Default")] H264Default,
    [InspectorName("H.264 NVIDIA")] H264Nvidia,
    [InspectorName("H.264 Lossless 420")] H264Lossless420,
    [InspectorName("H.264 Lossless 444")] H264Lossless444,
    [InspectorName("H.265 HEVC Default")] HevcDefault,
    [InspectorName("H.265 HEVC NVIDIA")] HevcNvidia,
    [InspectorName("Apple ProRes 4444 XQ (ap4x)")] ProRes4444XQ,
    [InspectorName("Apple ProRes 4444 (ap4h)")] ProRes4444,
    [InspectorName("Apple ProRes 422 HQ (apch)")] ProRes422HQ,
    [InspectorName("Apple ProRes 422 (apcn)")] ProRes422,
    [InspectorName("Apple ProRes 422 LT (apcs)")] ProRes422LT,
    [InspectorName("Apple ProRes 422 Proxy (apco)")] ProRes422Proxy,
    [InspectorName("VP8 (WebM)")] VP8Default,
    [InspectorName("VP9 (WebM)")] VP9Default,
}