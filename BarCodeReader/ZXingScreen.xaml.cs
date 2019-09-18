using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing;

namespace BarCodeReader
{
    public partial class ZXingScreen : ContentPage
    {
        public ZXingScreen()
        {
            InitializeComponent();

            //bar code standarts
            BarcodeScanner.Options = new ZXing.Mobile.MobileBarcodeScanningOptions
            {
                PossibleFormats = new List<BarcodeFormat>() {
                    //BarcodeFormat.RSS_EXPANDED,
                    //BarcodeFormat.RSS_14,
                    //BarcodeFormat.EAN_13,
                    //BarcodeFormat.EAN_8,
                    BarcodeFormat.All_1D

                    //BarcodeFormat.UPC_A,
                    //BarcodeFormat.CODE_128,
                    //BarcodeFormat.DATA_MATRIX
                },
                TryHarder = true,
                AssumeGS1 = true,
                AutoRotate = true,

            };
        }

        public void OnCameraClickedAsync(object sender, EventArgs args)
        {
            resultLabel.Text = "Scanning . . .";
            barcodeType.Text = "";
            CameraSwitcher(true);



        }

        public void Torch(object sender, EventArgs args)
        {
            BarcodeScanner.IsTorchOn = !BarcodeScanner.IsTorchOn;
        }

        private void CameraSwitcher(bool state)
        {
            BarcodeScanner.IsVisible = state;
            BarcodeScanner.IsScanning = state;
            BarcodeScanner.IsAnalyzing = state;

        }

        public void Handle_OnScanResult(Result result)
        {
            if (string.IsNullOrWhiteSpace(result.Text))
            {
                Vibrate(300);
                resultLabel.Text = "Scan fail";
                return;
            }
            Vibrate(100);
            CameraSwitcher(false);

            Device.BeginInvokeOnMainThread(() =>
            {
                resultLabel.Text = result.Text;
                barcodeType.Text = result.BarcodeFormat.ToString();
            });
        }

        private void Vibrate(int millis)
        {
            try
            {
                // Or use specified time
                var duration = TimeSpan.FromMilliseconds(millis);
                Vibration.Vibrate(duration);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Feature not supported on device
            }
            catch (Exception)
            {
                // Other error has occurred.
            }
        }
    }
}