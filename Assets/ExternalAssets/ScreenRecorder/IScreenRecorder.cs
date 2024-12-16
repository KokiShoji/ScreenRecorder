/// <summary>録画IF</summary>
public interface IScreenRecorder
{
    /// <summary>初期化</summary>
    void Initialize(ScreenRecorderSettings settings);
    
    /// <summary>録画開始</summary>
    void StartRecording();
    
    /// <summary>録画停止</summary>
    void StopRecording();
}
