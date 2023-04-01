using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ColorUniversalDesignLibrary.ColorBlindnessSimulator
{
    public class CUD_ColorBlindnessSimulator
    {
        private readonly DispatcherTimer _timer;
        private FrameworkElement _mirrorTarget;
        private readonly CUD_MirroringWindow _mirrorWindow;
        int _width;
        int _height;
        int _stride;
        byte[] _originalPixels;
        byte[] _protanopiaBgraPixels;
        byte[] _tritanopiaBgraPixels;
        byte[] _deuteranopiaBgraPixels;

        public CUD_ColorBlindnessSimulator()
        {
            // 優先順位を指定してタイマのインスタンスを生成
            _timer = new DispatcherTimer(DispatcherPriority.ContextIdle)
            {
                // インターバルを設定
                Interval = new TimeSpan(0, 0, 1)
            };
            // タイマメソッドを設定
            _timer.Tick += new EventHandler(TimerMethod);

            // 描画用ウィンドウを開く
            _mirrorWindow = new CUD_MirroringWindow();
            _mirrorWindow.Closed += MirrorWindow_Closed;
        }

        /// <summary>
        /// ミラー画面の更新頻度を設定する
        /// </summary>
        /// <param name="ts" valuetype="TimeSpan">更新頻度</param>
        public void SetInterval(TimeSpan ts)
        {
            if (_timer == null)
            {
                return;
            }
            _timer.Interval = ts;
        }

        public void StartMirroring(FrameworkElement targetFrameworkElement)
        {
            _mirrorTarget = targetFrameworkElement ?? throw new ArgumentNullException(nameof(targetFrameworkElement));

            // タイマースタート
            _timer.Start();

            // 描画用ウィンドウを開く
            _mirrorWindow.Show();
        }
        public void StopMirroring()
        {
            Dispose();
        }

        public event PropertyChangedEventHandler ProcessStared;
        public event PropertyChangedEventHandler ProcessFinished;

        private void OnProcessStared(string propertyName = null)
        {
            if (propertyName == null || ProcessStared == null) { return; }
            ProcessStared(this, new PropertyChangedEventArgs(propertyName));
        }
        private void OnProcessFinished(string propertyName = null)
        {
            if (propertyName == null) { return; }
            // 変換が終わったら表示に反映する
            if (propertyName.Equals("GetSimulatedPixels"))
            {
                RefreshView();
            }
            if (ProcessFinished == null) { return; }
            ProcessFinished(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 定期的に呼ばれる処理
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        private void TimerMethod(object sender, EventArgs e)
        {
            if (_mirrorTarget == null)
            {
                return;
            }
            if (_mirrorTarget.ActualWidth == 0 || _mirrorTarget.ActualHeight == 0)
            {
                return;
            }

            OnProcessStared("TimerMethod");

            // レンダリング
            Result res = _mirrorWindow.GetBitmapElement(_mirrorTarget);
            if (res.originalPixels == null)
            {
                return;
            }

            // 描画に必要な定数を持っておく
            _originalPixels = res.originalPixels;
            _width = res.width;
            _height = res.height;
            _stride = res.stride;

            // 色相の変換
            _ = Task.Run(() => GetSimulatedPixels(_originalPixels));

            OnProcessFinished("TimerMethod");
        }

        /// <summary>
        /// 描画の更新
        /// </summary>
        /// UIスレッド外から呼ばれてもいいようにDispatcher.Invokeにしておく
        private void RefreshView()
        {
            _mirrorWindow.Dispatcher.Invoke((Action)(() =>
            {
                var dpi = 96.0;
                var original = BitmapSource.Create((int)_width, (int)_height, dpi, dpi, PixelFormats.Pbgra32, null, _originalPixels, _stride);
                var protanopia = BitmapSource.Create((int)_width, (int)_height, dpi, dpi, PixelFormats.Pbgra32, null, _protanopiaBgraPixels, _stride);
                var tritanopia = BitmapSource.Create((int)_width, (int)_height, dpi, dpi, PixelFormats.Pbgra32, null, _tritanopiaBgraPixels, _stride);
                var deuteranopia = BitmapSource.Create((int)_width, (int)_height, dpi, dpi, PixelFormats.Pbgra32, null, _deuteranopiaBgraPixels, _stride);

                // 表示
                _mirrorWindow.SetImages(original, protanopia, tritanopia, deuteranopia);
            }));
        }

        private void GetSimulatedPixels(byte[] originalPixels)
        {
            OnProcessStared("GetSimulatedPixels");

            _protanopiaBgraPixels = ColorBlindnessSimulatorHelper.SimulateProtanopia(originalPixels);
            _tritanopiaBgraPixels = ColorBlindnessSimulatorHelper.SimulateTritanopia(originalPixels);
            _deuteranopiaBgraPixels = ColorBlindnessSimulatorHelper.SimulateDeuteranopia(originalPixels);

            OnProcessFinished("GetSimulatedPixels");
        }

        private void Dispose()
        {
            _timer.Stop();
            if (_timer != null)
            {
                _timer.Stop();
            }
            if (_mirrorWindow != null)
            {
                _mirrorWindow.Close();
            }
        }

        private void MirrorWindow_Closed(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
