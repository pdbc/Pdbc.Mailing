using System;
using System.IO;
using OpenQA.Selenium;

namespace Pdbc.Mailing.Tests.Screenshots
{
    public static class WebDriverScreenshotExtensions
    {
        public static void TakeScreenshot(this IWebDriver driver, string path, string filename = null)
        {
            //var elements = driver.FindElementsByCssClassNameDirect("blurrable").ToList();
            //elements.MakeBlurry();

            filename = filename ?? "BUG_" + DateTime.Now.Ticks + ".jpg";
            driver.SaveScreenshot(path, filename);

            //elements.MakeReadable();

            //Thread.Sleep(200);
            //Waiting.ForMilliseconds(50);
        }

        public static void SaveScreenshot(this IWebDriver driver, string path, string filename)
        {
            // Replace language in filename
            //filename = filename.FormatWith(GetCurrentLanguage());

            var filePath = Path.Combine(path, filename);
            var ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile(filePath, ScreenshotImageFormat.Png);                //use any of the built in image formatting

            //CropScreenshot(ss, filename);

        }

        //private void CropScreenshot(Screenshot screenshot, string filename)
        //{
        //    if (!ShouldCropScreenshot)
        //        return;

        //    var croppedPath = Path.Combine(_screenshotPath, $"{filename}_cropped");

        //    try
        //    {
        //        var element = FindElementByCssClass(CropScreenshotCssClass);
        //        if (element == null) return;

        //        var croppedImage = new Rectangle(element.Location, element.Size);

        //        using var screenshotMemoryStream = new MemoryStream(screenshot.AsByteArray);
        //        using var screenshotBitmap = new Bitmap(screenshotMemoryStream);

        //        var croppedScreenshot = screenshotBitmap.Clone(croppedImage, screenshotBitmap.PixelFormat);
        //        croppedScreenshot.Save(croppedPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        //    }
        //    catch (Exception ex)
        //    {
        //        // do nothing
        //    }
        //}
    }
}