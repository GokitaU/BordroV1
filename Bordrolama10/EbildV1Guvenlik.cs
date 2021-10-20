using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace Bordrolama10

{
    public partial class EbildV1Guvenlik : Form
    {
        public EbildV1Guvenlik()
        {
            InitializeComponent();
        }

        private void btnTmm_Click(object sender, EventArgs e)
        {
            //var v1guvenliksozcugu = ebildirgev1.V1driver.FindElement(By.XPath("//*[@id=\"formA\"]/table/tbody/tr[5]/td/table/tbody/tr[2]/td[2]/img")).GetAttribute();
            //pictureBox1.ImageLocation = ebildirgev1.V1driver.FindElement(By.XPath("//*[@id=\"formA\"]/table/tbody/tr[5]/td/table/tbody/tr[2]/td[2]/img")).Displayed;

            v1guvenliksozcugu.v1guvenlik = txtV1Guvenlik.Text.ToString();
            this.Close();
        }

        private void EbildV1Guvenlik_Load(object sender, EventArgs e)
        {

            var driverPath = Application.StartupPath;
            var chromeOptions = new ChromeOptions();
            //11111111 -- CHROME BROWSER İN GİZLENMESİ İÇİN 
            chromeOptions.AddArgument("headless");
            chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
            //11111111
            chromeOptions.AddUserProfilePreference("intl.accept_languages", "tr");
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
            var v1driver1 = new ChromeDriver(driverPath, chromeOptions);
            v1driver.v1driver1 = v1driver1;

            v1driver1.Navigate().GoToUrl("https://ebildirge.sgk.gov.tr/WPEB/amp/loginldap");


            var captchaImage = v1driver1.FindElement(By.XPath("//*[@id=\"formA\"]/table/tbody/tr[5]/td/table/tbody/tr[2]/td[2]/img"));

            ITakesScreenshot ssdriver = v1driver1 as ITakesScreenshot;
            Screenshot screenshot = ssdriver.GetScreenshot();

            Point point = captchaImage.Location;
            int width = captchaImage.Size.Width;
            int height = captchaImage.Size.Height;

            Rectangle section = new Rectangle(point, new Size(width, height));
            Bitmap source = new Bitmap(new MemoryStream(screenshot.AsByteArray));

            Bitmap finalCaptchImage = ImageAl(source, section);
            pictureBox1.Image = finalCaptchImage;
        }




        private Bitmap ImageAl(Bitmap source, Rectangle section)
        {
            Bitmap bmp = new Bitmap(section.Width, section.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
            return bmp;
        }
    }
}