using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "ScreenRecorderSettings", menuName = "ScreenRecorder/Screen Recorder Settings")]
public class ScreenRecorderSettings : ScriptableObject
{
    private string _outputDirPath;
    private string _outputFileName;
    
    [Header("ファイル設定")]
    public string outputDirectoryName = "Recordings";
    public int maxRecordings = 2;
    public string fileNamePrefix = "PJName";
    public string fileNameFormat = "{0}_{1:yyyyMMdd_HHmmss}"; // {0} = Prefix, {1} = DateTime
    public bool allowAutoRecordOnStart = true;
    
    [Header("ビデオ設定")]
    public Vector2Int resolution = new (1920, 1080);
    public ScreenRecorderOutputFormat outputFormat = ScreenRecorderOutputFormat.HevcNvidia;
    public float frameRate = 60.0f;
    public string ffmpegRelativePath = "/ExternalAssets/FFmpeg/Win/ffmpeg.exe";
    
    public string OutputDirPath
    {
        get
        {
            if (string.IsNullOrEmpty(_outputDirPath))
            {
                var assetsPath = Application.dataPath;
                var projectRootPath = assetsPath.Length == 0 ? "" : assetsPath.Replace("/Assets", "");
                _outputDirPath = Path.Combine(projectRootPath, outputDirectoryName);
            }
            return _outputDirPath;
        }
    }
    public string OutputFilePath => Path.Combine(OutputDirPath, OutputFileName);

    public string OutputFileName
    {
        get
        {
            if (string.IsNullOrEmpty(_outputFileName)) GenerateFileName();
            return _outputFileName;
        }
    }
    
    public string GenerateFileName()
    {
        _outputFileName = string.Format(fileNameFormat, fileNamePrefix, System.DateTime.Now);
        return _outputFileName;
    }

    public string FFmpegPath => Application.dataPath + ffmpegRelativePath;
}