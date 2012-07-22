using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using RLM.Core.Framework.Log;

namespace RLM.Core.Framework.Utility
{
    public class ImageHelper
    {
        public static RLM.Core.Entity.Size GetSize(int maxWidth, int maxHeight, int width, int height)
        {
            try
            {
                //maxWidth = maxWidth == null ? 0 : maxWidth;
                if (width == 0 || height == 0) { return new RLM.Core.Entity.Size() { Width = maxWidth, Height = maxHeight }; }
                if (width <= maxWidth && height <= maxHeight) { return new RLM.Core.Entity.Size() { Width = width, Height = height }; }
                double scaleWidth = (double)maxWidth / width;
                double scaleHeight = (double)maxHeight / height;
                double scale = scaleHeight;// > scaleWidth ? scaleWidth : scaleHeight;
                if (scale <= 0) { scale = scaleWidth; }
                else if (scale > scaleWidth && scaleWidth > 0) { scale = scaleWidth; }
                

                return new RLM.Core.Entity.Size() { Width = (int)(width * scale), Height = (int)(height * scale) };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string GetCssSize(int maxWidth, int maxHeight, int width, int height)
        {
            try
            {
                string cssWidth = string.Empty;
                string cssHeight = string.Empty;

                if (width == 0)
                {
                    cssWidth = string.Format("width=\"{0}\"", maxWidth);
                }
                else if (width > maxWidth)
                {
                    double scaleWidth = (double)maxWidth / width;
                    cssWidth = string.Format("width=\"{0}\"", width * scaleWidth);
                }
                if (height == 0)
                {
                    cssHeight = string.Format("height=\"{0}\"", maxHeight);
                }
                else if (width > maxWidth)
                {
                    double scaleHeight = (double)maxHeight / height;
                    cssHeight = string.Format("height=\"{0}\"", height * scaleHeight);
                }

                return string.Format("{0} {1}}", cssWidth, cssHeight);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string GetImageDetail(object imageUrl, string defaultImageUrl, string folderUrl, string width, string height, string altString)
        {
            string url = defaultImageUrl;
            if (imageUrl != null && imageUrl.ToString() != "")
            {
                url = imageUrl.ToString();
            }

            return string.Format("src=\"{0}{1}\" width=\"{2}\" height=\"{3}\" alt=\"{4}\" ",
                    UrlHelper.ResolveUrl(folderUrl),
                    url,
                    width,
                    height,
                    altString
                    );
        }

        public static RLM.Core.Entity.Size GetScaleSize(int maxWidth, int maxHeight, int width, int height)
        {
            try
            {
                //maxWidth = maxWidth == null ? 0 : maxWidth;
                if (width == 0 || height == 0) { return new RLM.Core.Entity.Size() { Width = maxWidth, Height = maxHeight }; }

                double scaleWidth = 1;
                double scaleHeight = 1;
                double scale = 1;

                if (width <= maxWidth && height <= maxHeight)
                {
                    scaleWidth = (double)maxWidth / width;
                    scaleHeight = (double)maxHeight / height;
                    scale = scaleHeight > scaleWidth ? scaleWidth : scaleHeight;
                }
                else
                {
                    scaleWidth = (double)maxWidth / width;
                    scaleHeight = (double)maxHeight / height;
                    scale = scaleHeight > scaleWidth ? scaleWidth : scaleHeight;
                }

                return new RLM.Core.Entity.Size() { Width = (int)(width * scale), Height = (int)(height * scale) };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string GetImageDetail(object imageUrl, string defaultImageUrl, string folderUrl, string serverPath, string maxWidth, string maxHeight, string altString)
        {
            string url = defaultImageUrl;
            if (imageUrl != null && imageUrl.ToString() != "")
            {
                url = imageUrl.ToString();
            }

            string fileDefaultName = string.Format("{0}{1}{2}", serverPath, folderUrl.Replace("~/", "").Replace("/", "\\"), url.Replace("/", "\\"));
            string fileName = string.Format("{0}{1}{2}", serverPath, folderUrl.Replace("~/", "").Replace("/", "\\"), url.Replace("/", "\\"));
            int width = 0, height = 0;
            if (!File.Exists(fileName))
            {
                url = defaultImageUrl;
                fileName = fileDefaultName;
            }

            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(fileName);
                width = img.Width;
                height = img.Height;
            }
            catch { }

            RLM.Core.Entity.Size size = GetScaleSize(Convert.ToInt32(maxWidth), Convert.ToInt32(maxHeight), width, height);
            Logger.Info(string.Format("fileName:{0}", fileName));
            //string file = System.AppDomain.CurrentDomain.D Application.StartupPath
            //System.Drawing.Image img = System.Drawing.Image.FromFile("");
            return string.Format("src=\"{0}{1}\" width=\"{2}\" height=\"{3}\" alt=\"{4}\" ",
                    UrlHelper.ResolveUrl(folderUrl),
                    url,
                    size.Width,
                    size.Height,
                    altString
                    );
        }

        public static byte[] ResizeFromByteArray(int MaxSideSize, Byte[] byteArrayIn)
        {
            byte[] byteArray = null;  // really make this an error gif
            MemoryStream ms = new MemoryStream(byteArrayIn);
            byteArray = ImageHelper.ResizeFromStream(MaxSideSize, ms);

            return byteArray;
        }

        /// <summary>
        /// converts stream to bytearray for resized image
        /// </summary>
        /// <param name="MaxSideSize"></param>
        /// <param name="Buffer"></param>
        /// <returns></returns>
        public static byte[] ResizeFromStream(int MaxSideSize, Stream Buffer)
        {
            byte[] byteArray = null;  // really make this an error gif

            try
            {

                Bitmap bitMap = new Bitmap(Buffer);
                int intOldWidth = bitMap.Width;
                int intOldHeight = bitMap.Height;

                int intNewWidth;
                int intNewHeight;

                int intMaxSide;

                if (intOldWidth >= intOldHeight)
                {
                    intMaxSide = intOldWidth;
                }
                else
                {
                    intMaxSide = intOldHeight;
                }

                if (intMaxSide > MaxSideSize)
                {
                    //set new width and height
                    double dblCoef = MaxSideSize / (double)intMaxSide;
                    intNewWidth = Convert.ToInt32(dblCoef * intOldWidth);
                    intNewHeight = Convert.ToInt32(dblCoef * intOldHeight);
                }
                else
                {
                    intNewWidth = intOldWidth;
                    intNewHeight = intOldHeight;
                }

                System.Drawing.Size ThumbNailSize = new System.Drawing.Size(intNewWidth, intNewHeight);
                System.Drawing.Image oImg = System.Drawing.Image.FromStream(Buffer);
                System.Drawing.Image oThumbNail = new Bitmap(ThumbNailSize.Width, ThumbNailSize.Height);
                
                Graphics oGraphic = Graphics.FromImage(oThumbNail);
                oGraphic.CompositingQuality = CompositingQuality.HighQuality;
                oGraphic.SmoothingMode = SmoothingMode.HighQuality;
                oGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Rectangle oRectangle = new Rectangle
                    (0, 0, ThumbNailSize.Width, ThumbNailSize.Height);

                oGraphic.DrawImage(oImg, oRectangle);

                MemoryStream ms = new MemoryStream();
                oThumbNail.Save(ms, ImageFormat.Jpeg);
                byteArray = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(byteArray, 0, Convert.ToInt32(ms.Length));

                oGraphic.Dispose();
                oImg.Dispose();
                ms.Close();
                ms.Dispose();
                return byteArray;
            }
            catch (Exception ex)
            {
                throw;

            }
            
        }
        public static void SaveResizeFromStream(string fromFileName, string fileName, int maxWidth, int maxHeight)
        {
            try
            {
                System.IO.Stream sr = File.OpenRead(fromFileName);
                SaveResizeFromStream(sr, fileName, maxWidth, maxHeight);
                sr.Close();
                sr.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Saves the resized image to specified file name and path as JPEG
        /// and also returns the bytearray for any other use you may need it for
        /// </summary>
        /// <param name="MaxSideSize"></param>
        /// <param name="Buffer"></param>
        /// <param name="fileName">No Extension</param>
        /// <param name="filePath">Examples: "images/dir1/dir2" or "images" or "images/dir1"</param>
        /// <returns></returns>
        public static void SaveResizeFromStream(Stream Buffer, string fileName, int maxWidth, int maxHeight)
        {

            try
            {

                Bitmap bitMap = new Bitmap(Buffer);
                int intOldWidth = bitMap.Width;
                int intOldHeight = bitMap.Height;



                RLM.Core.Entity.Size size = ImageHelper.GetSize(maxWidth, maxHeight, intOldWidth, intOldHeight);

                System.Drawing.Size ThumbNailSize = new System.Drawing.Size(size.Width, size.Height);
                System.Drawing.Image oImg = System.Drawing.Image.FromStream(Buffer);
                System.Drawing.Image oThumbNail = new Bitmap(ThumbNailSize.Width, ThumbNailSize.Height);

                Graphics oGraphic = Graphics.FromImage(oThumbNail);
                oGraphic.CompositingQuality = CompositingQuality.HighQuality;
                oGraphic.SmoothingMode = SmoothingMode.HighQuality;
                oGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Rectangle oRectangle = new Rectangle
                    (0, 0, ThumbNailSize.Width, ThumbNailSize.Height);

                oGraphic.DrawImage(oImg, oRectangle);

                oThumbNail.Save(fileName, ImageFormat.Png);

                oGraphic.Dispose();
                oThumbNail.Dispose();
                oImg.Dispose();
                GC.Collect();
            }
            catch (Exception)
            {
                throw;

            }
        }

        public static System.Drawing.Image ResizeFromImage(System.Drawing.Image image, int width, int height, Color backgroundColor)
        {
            int destWidth = image.Width;
            int destHeight = image.Height;
            int destX = (width - image.Width) / 2;
            int destY = (height - image.Height) / 2;

            /*if (destX == 0 && destY == 0)
            {
                //grPhoto.Dispose();
                return (System.Drawing.Image)image.Clone();
            }*/

            Bitmap bmPhoto = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(backgroundColor);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(image,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(0, 0, image.Width, image.Height),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }
    }
}
