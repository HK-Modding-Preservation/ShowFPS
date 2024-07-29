using System;
using System.Collections;
using System.Globalization;
using UnityEngine;

namespace ShowFPS;

public class FpsCounter : MonoBehaviour
{
    private const float FpsUpdateInterval = 0.5f;

    private double _currentFps = -1.0;
    private double _averageFps = -1.0;
    private double _maxFps = -1.0;
    private double _minFps = -1.0;
    private UInt32 _fpsCounter = 0;
    private UInt32 _cycleCounter = 0;
    private string _displayText = "";
    private readonly GUIStyle _textStyle = new GUIStyle(GUIStyle.none);
    private Font _monoFont;

    private void Start()
    {
        _monoFont = Font.CreateDynamicFontFromOSFont(new string[]
        {
            // Windows
            "Consolas",
            // Mac
            "Menlo",
            // Linux
            "Courier New",
            "DejaVu Mono"
        }, 15);
        _textStyle.font = _monoFont;
        _textStyle.normal.textColor = Color.white;
        _textStyle.alignment = TextAnchor.LowerRight;
        _textStyle.padding = new RectOffset(5, 5, 5, 5);
    }

    private void OnEnable()
    {
        StartCoroutine(CalcTimedFps());
    }

    private IEnumerator CalcTimedFps()
    {
        while (isActiveAndEnabled)
        {
            yield return new WaitForSecondsRealtime(FpsUpdateInterval);

            ++_cycleCounter;

            _currentFps = _fpsCounter / FpsUpdateInterval;
            _averageFps = _averageFps < 0 ? _currentFps : Average(_averageFps, _currentFps, _cycleCounter);
            _maxFps = _maxFps < 0 ? _currentFps : Max(_maxFps, _currentFps);
            _minFps = _minFps < 0 ? _currentFps : Min(_minFps, _currentFps);

            GenerateText();

            _fpsCounter = 0;
        }
    }

    private void Update()
    {
        ++_fpsCounter;
    }

    private void GenerateText()
    {
        _displayText = FormatRow("FPS:", "");
        _displayText += "\n" + FormatRow("Current:", _currentFps);
        _displayText += "\n" + FormatRow("Average:", _averageFps);
        _displayText += "\n" + FormatRow("Max:", _maxFps);
        _displayText += "\n" + FormatRow("Min:", _minFps);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), _displayText, _textStyle);
    }

    private const string formatName = "{0,-8}";
    private const string formatValueDouble = "{0,7:F1}";
    private const string formatValueString = "{0,7}";

    private string FormatRow(string name, double value)
    {
        return String.Format(CultureInfo.InvariantCulture, formatName, name) + " " + String.Format(CultureInfo.InvariantCulture, formatValueDouble, value);
    }
    private string FormatRow(string name, string value)
    {
        return String.Format(CultureInfo.InvariantCulture, formatName, name) + " " + String.Format(CultureInfo.InvariantCulture, formatValueString, value);
    }

    public static double Average(double average, double fpsFrame, double frameNumber)
    {
        return ((average * (frameNumber - 1)) + fpsFrame) / frameNumber;
    }

    public static double Max(double max, double fpsFrame)
    {
        return max < fpsFrame ? fpsFrame : max;
    }

    public static double Min(double min, double fpsFrame)
    {
        return min < fpsFrame ? min : fpsFrame;
    }
}