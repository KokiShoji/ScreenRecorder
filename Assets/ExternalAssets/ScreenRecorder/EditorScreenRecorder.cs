#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Examples;
using UnityEditor.Recorder.Input;
using UnityEngine;

/// <summary>Editor上での録画の実装</summary>
public class EditorScreenRecorder : IScreenRecorder
{
    private RecorderControllerSettings _controllerSettings;
    private RecorderController _controller;
    private ScreenRecorderSettings _settings;
    private const string MovieRecorderSettingsName = "Editor Screen Recorder";
    private static readonly Dictionary<ScreenRecorderOutputFormat, FFmpegEncoderSettings.OutputFormat> FormatMapping = new(){
            { ScreenRecorderOutputFormat.H264Default, FFmpegEncoderSettings.OutputFormat.H264Default },
            { ScreenRecorderOutputFormat.H264Nvidia, FFmpegEncoderSettings.OutputFormat.H264Nvidia },
            { ScreenRecorderOutputFormat.H264Lossless420, FFmpegEncoderSettings.OutputFormat.H264Lossless420 },
            { ScreenRecorderOutputFormat.H264Lossless444, FFmpegEncoderSettings.OutputFormat.H264Lossless444 },
            { ScreenRecorderOutputFormat.HevcDefault, FFmpegEncoderSettings.OutputFormat.HevcDefault },
            { ScreenRecorderOutputFormat.HevcNvidia, FFmpegEncoderSettings.OutputFormat.HevcNvidia },
            { ScreenRecorderOutputFormat.ProRes4444XQ, FFmpegEncoderSettings.OutputFormat.ProRes4444XQ },
            { ScreenRecorderOutputFormat.ProRes4444, FFmpegEncoderSettings.OutputFormat.ProRes4444 },
            { ScreenRecorderOutputFormat.ProRes422HQ, FFmpegEncoderSettings.OutputFormat.ProRes422HQ },
            { ScreenRecorderOutputFormat.ProRes422, FFmpegEncoderSettings.OutputFormat.ProRes422 },
            { ScreenRecorderOutputFormat.ProRes422LT, FFmpegEncoderSettings.OutputFormat.ProRes422LT },
            { ScreenRecorderOutputFormat.ProRes422Proxy, FFmpegEncoderSettings.OutputFormat.ProRes422Proxy },
            { ScreenRecorderOutputFormat.VP8Default, FFmpegEncoderSettings.OutputFormat.VP8Default },
            { ScreenRecorderOutputFormat.VP9Default, FFmpegEncoderSettings.OutputFormat.VP9Default },
        };
    public void Initialize(ScreenRecorderSettings settings)
    {
        _settings = settings;
        
        // FFmpegの設定
        var ffmpegSettings = new FFmpegEncoderSettings
        {
            Format = GetOutputFormat(),
        };
        ffmpegSettings.FFMpegPath = settings.FFmpegPath;
        
        // レコーダーの設定
        var movieSettings = ScriptableObject.CreateInstance<MovieRecorderSettings>();
        movieSettings.name = MovieRecorderSettingsName;
        movieSettings.Enabled = true;
        movieSettings.ImageInputSettings = new GameViewInputSettings
        {
            OutputWidth = _settings.resolution.x,
            OutputHeight = _settings.resolution.y
        };
        movieSettings.EncoderSettings = ffmpegSettings;
        _settings.GenerateFileName();
        movieSettings.OutputFile = _settings.OutputFilePath;
        
        // コントローラーの設定
        _controllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
        _controllerSettings.AddRecorderSettings(movieSettings);
        _controllerSettings.ExitPlayMode = true;
        _controllerSettings.SetRecordModeToManual();
        _controllerSettings.FrameRatePlayback = FrameRatePlayback.Constant;
        _controllerSettings.FrameRate = settings.frameRate;
    }

    public void StartRecording()
    {
        if (_controllerSettings == null)
        {
            Debug.LogError("RecorderControllerSettingsは初期化されていない");
            return;
        }
        _controller = new RecorderController(_controllerSettings);
        _controller.PrepareRecording();
        _controller.StartRecording();
        Debug.Log($"録画開始 出力先: {_settings.OutputDirPath}");
    }

    public void StopRecording()
    {
        if (_controller == null || !_controller.IsRecording()) return;
        _controller.StopRecording();
        Debug.Log("録画終了 出力先: " + _settings.OutputDirPath);
    }

    public FFmpegEncoderSettings.OutputFormat GetOutputFormat()
    {
        return FormatMapping.TryGetValue(_settings.outputFormat, out var ffmpegFormat) ? ffmpegFormat : FFmpegEncoderSettings.OutputFormat.HevcNvidia;
    }
}
#endif
