using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class ScreenRecorderFileCleaner
{
    private readonly ScreenRecorderSettings _settings;

    public ScreenRecorderFileCleaner(ScreenRecorderSettings settings)
    {
        _settings = settings;
    }

    public void DeleteOldRecordings()
    {
        if (string.IsNullOrEmpty(_settings.OutputDirPath)) return;

        try
        {
            var regex = new Regex($@"^{Regex.Escape(_settings.fileNamePrefix)}.*\.mp4$");

            var files = new DirectoryInfo(_settings.OutputDirPath)
                .GetFiles("*.mp4")
                .Where(file => regex.IsMatch(file.Name))
                .OrderByDescending(file => file.Name)
                .ToList();

            // 指定数を超えた古いファイルを削除
            var limitedFileNum = Math.Max(_settings.maxRecordings - 1, 0);
            foreach (var file in files.Skip(limitedFileNum))
            {
                file.Delete();
                Debug.Log($"古い録画ファイル削除: {file.Name}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"録画ファイル削除に失敗: {e.Message}");
        }
    }
}