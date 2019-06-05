using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KAutoHelper;
using System.Threading;
using System.Drawing;

namespace DemoAuto_v1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region data
        Bitmap LOGO_NRO;
        Bitmap CAPSU_BT_BMP;
        Bitmap SUDUNG_CAPSU_BMP;
        #endregion
        bool isStop = false;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            LOGO_NRO = (Bitmap)Bitmap.FromFile("Data//logoGame.png");
            CAPSU_BT_BMP = (Bitmap)Bitmap.FromFile("Data//capsuBinhThuong.png");
            SUDUNG_CAPSU_BMP = (Bitmap)Bitmap.FromFile("Data//suDungCapSu.png");
        }

        void sendText()
        {
            string deviceId = null;
            var listDevice = KAutoHelper.ADBHelper.GetDevices();
            if (listDevice != null && listDevice.Count > 0)
            {
                deviceId = listDevice.First();
            }

          //  KAutoHelper.ADBHelper.InputText(deviceId, "01679491315");
            var x = KAutoHelper.ADBHelper.GetScreenResolution(deviceId).X;
            var y = KAutoHelper.ADBHelper.GetScreenResolution(deviceId).Y;
            Console.WriteLine(x);
            Console.WriteLine(y);

            KAutoHelper.ADBHelper.Tap(deviceId, 140, 145);
        }

       

        void auto()
        {
            string deviceId = null;
            var listDevice = KAutoHelper.ADBHelper.GetDevices();
            if (listDevice != null && listDevice.Count > 0)
            {
                deviceId = listDevice.First();
            }

            Task t = new Task(() => {
             
                    var screen = KAutoHelper.ADBHelper.ScreenShoot(deviceId);
                    var topUpPoint = KAutoHelper.ImageScanOpenCV.FindOutPoint(screen, LOGO_NRO);
                    if (topUpPoint != null)
                    {
                        KAutoHelper.ADBHelper.Tap(deviceId, topUpPoint.Value.X, topUpPoint.Value.Y);
                    Console.WriteLine(topUpPoint.Value.X);
                    Console.WriteLine(topUpPoint.Value.Y);
                    }

                Thread.Sleep(5000);

                // click dialog OK
                KAutoHelper.ADBHelper.Tap(deviceId, 648, 641);
                Thread.Sleep(1000);
                // Doi tai khoan
                KAutoHelper.ADBHelper.Tap(deviceId, 641, 403);
                Thread.Sleep(1000);
                // Nhap Email 
                KAutoHelper.ADBHelper.Tap(deviceId, 611, 387);
                KAutoHelper.ADBHelper.InputText(deviceId, "01679491315");
                Thread.Sleep(1000);
                // Nhap Passwork 
                KAutoHelper.ADBHelper.Tap(deviceId, 596, 498);
                KAutoHelper.ADBHelper.InputText(deviceId, "vandon98257");
                Thread.Sleep(1000);
                // Xác nhận Tài khoản 
                KAutoHelper.ADBHelper.Tap(deviceId, 516, 655);
                // Click Login
                KAutoHelper.ADBHelper.Tap(deviceId, 642, 271);
                // Đợi cho đến khi login thành công.
            });
            t.Start();
         }

        private void onStop(object sender, RoutedEventArgs e)
        {
            isStop = true;
        }

        private void onRestart(object sender, RoutedEventArgs e)
        {
            isStop = false;
            isClickCapsu = false;
        }

        // Hàm Chung

        void moMenuGame(string deviceId)
        {
            KAutoHelper.ADBHelper.Tap(deviceId, 16, 191);
        }

        void chonTabHanhTrang(string deviceId)
        {
            KAutoHelper.ADBHelper.Tap(deviceId, 156, 190);
        }

        bool isClickCapsu = false;

        void clickVaoCapsu(string deviceId)
        {
            while(isClickCapsu == false)
            {
                KAutoHelper.ADBHelper.Swipe(deviceId, 109, 704, 109, 516, 200);
                Thread.Sleep(1);
                timCapsu(deviceId);
            }
        }

        void timCapsu(string deviceId)
        {
            var screen = KAutoHelper.ADBHelper.ScreenShoot(deviceId);
            var capsuBtPoint = KAutoHelper.ImageScanOpenCV.FindOutPoint(screen, CAPSU_BT_BMP);
            if (capsuBtPoint != null)
            {
                KAutoHelper.ADBHelper.LongPress(deviceId, capsuBtPoint.Value.X, capsuBtPoint.Value.Y);
                Console.WriteLine(capsuBtPoint.Value.X);
                Console.WriteLine(capsuBtPoint.Value.Y);
                isClickCapsu = true;
            }
        }

        void suDungCapsu(string deviceId)
        {
            var screen = KAutoHelper.ADBHelper.ScreenShoot(deviceId);
            var suDungCapsuPoint = KAutoHelper.ImageScanOpenCV.FindOutPoint(screen, SUDUNG_CAPSU_BMP);
            if (suDungCapsuPoint != null)
            {
                KAutoHelper.ADBHelper.LongPress(deviceId, suDungCapsuPoint.Value.X, suDungCapsuPoint.Value.Y);
            }
        }

        void chonLangKakarot(string deviceId)
        {
            KAutoHelper.ADBHelper.LongPress(deviceId, 100, 491);
            KAutoHelper.ADBHelper.LongPress(deviceId, 100, 491);
        }

        // Auto vao dai hoi vo thuat
        private void onVaoDaiHoiVoThuat(object sender, RoutedEventArgs e)
        {
            isClickCapsu = false;
            string deviceId = null;
            var listDevice = KAutoHelper.ADBHelper.GetDevices();
            if (listDevice != null && listDevice.Count > 0)
            {
                deviceId = listDevice.First();
            }

            Task t = new Task(() =>
            {
                moMenuGame(deviceId);
                chonTabHanhTrang(deviceId);
                clickVaoCapsu(deviceId);
                Thread.Sleep(1000);
                suDungCapsu(deviceId);
                chonLangKakarot(deviceId);
            });

            t.Start();
        }

        void Delay(int delay)
        {
            while (delay > 0)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                delay--;
                if (isStop)
                    break;
            }
        }
    }
}
