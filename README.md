# ScreenRecorder

## 概要
Unity Recorderでffmpegで録画するスクリプトです。
Windows上でUnityEditorでPlay時に自動録画するために使います。
ノイズの少ない画質を少ない容量で記録したいため
H.265 HEVC NVIDIAコーデックを使うことを想定しています。

## 説明記事
[Qiita記事『Unityのプレイ画面を自動録画したい』](https://qiita.com/koki-shoji/items/ae012203ae4a0e3006d5)

## 録画方法
Assets/ScreenRecorder/Prefabs/ScreenRecorderControllerをシーンのHierarchyに含めた状態でPlayします。
Project/Recordings以下にmp4の動画ファイルが保存されます。

## 設定方法
設定はAssets/ScreenRecorder/ScriptableObjects/ScreenRecorderSettingsから設定できます。

## コードで録画開始・停止する方法
ScreenRecorderController.ScreenRecorderを取得してStartRecording()メソッドを呼び出すと録画開始、
StopRecording()メソッドを呼び出すと録画終了します。
