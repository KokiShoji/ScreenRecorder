using System.IO;
using UnityEngine;

public class ScreenRecorderController : MonoBehaviour
{
    [SerializeField]
    private ScreenRecorderSettings settings;
    private ScreenRecorderFileCleaner _fileCleaner;
    public IScreenRecorder ScreenRecorder{ get; private set; }

    private void Awake()
    {
        InitializeOutputDir();
        InitializeFileCleaner();
        InitializeScreenRecorder();
    }
    private void Start()
    {
        // 古い録画ファイルの削除
        _fileCleaner.DeleteOldRecordings();
        // 起動時に自動録画
        if(settings.allowAutoRecordOnStart) ScreenRecorder.StartRecording();
    }

    private void InitializeOutputDir()
    {
        var outputPath = settings.OutputDirPath;        
        if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);
    }

    private void InitializeFileCleaner()
    {
        _fileCleaner = new ScreenRecorderFileCleaner(settings);
    }

    private void InitializeScreenRecorder()
    {
#if UNITY_EDITOR        
        ScreenRecorder = new EditorScreenRecorder();
        ScreenRecorder.Initialize(settings);
#endif        
    }
}
