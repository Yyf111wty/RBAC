using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Flurl;
using Flurl.Http;
using Entity.Models;
using Dal.Permissions;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        /// <summary>
        /// 下载图片
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = Permissions.File.Export)]
        [HttpGet]
        public async Task<IActionResult> DownloadImage3()
        {
            var filePath = @"D:\Image\5.png";
            byte[] fileBytes = await Task.Run(() => System.IO.File.ReadAllBytes(filePath));
            return File(fileBytes, "image/jpeg");
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> DownloadFile3()
        {
            //var filePath = @"C:\Users\sszjj\Desktop\《你必须知道的.NET(第2版)》(王涛).pdf";
            var filePath = @"D:\Image\101.pdf";
            byte[] fileBytes = await Task.Run(() => System.IO.File.ReadAllBytes(filePath));
            //return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "101.pdf");
            return File(fileBytes, "application/pdf", "101111.pdf");
        }
        /// <summary>
        /// 下载图片
        /// </summary>
        /// <returns></returns>
        //[HttpGet, AllowAnonymous]
        //public IActionResult DownloadImage3()
        //{
        //    var filePath = @"D:\Image\5.png";
        //    using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate,FileAccess.Read))
        //    {
        //        var fileBytes = new byte[fileStream.Length];
        //        fileStream.Read(fileBytes, 0, fileBytes.Length);
        //        return File(fileBytes, "image/jpeg");
        //    }
        //}
        /// <summary>
        /// 异步下载图片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DownloadImageAsync()
        {
            byte[] bytes = await "http://localhost:8088/api/File/DownloadImage3".GetBytesAsync();
            return File(bytes, "image/jpeg");
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <returns></returns>
        //[HttpGet, AllowAnonymous]
        //public IActionResult DownloadFile3()
        //{
        //    var filePath = @"D:\Image\101.pdf";
        //    using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read))
        //    {
        //        var fileBytes = new byte[fileStream.Length];
        //        fileStream.Read(fileBytes, 0, fileBytes.Length);
        //        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "101.pdf");
        //    }
        //}
        /// <summary>
        /// 图片流
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public HttpResponseMessage DownloadImage0()
        {

            //FileStream file = new FileStream(@"D:\Image\5.png", FileMode.OpenOrCreate, FileAccess.Read);
            //HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            //using (Image img = Image.FromStream(file))
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    //使用指定的编码器和图像编码器参数，将该 Image 保存到指定的文件
            //    img.Save(ms, ImageFormat.Jpeg);
            //    result.Content = new ByteArrayContent(ms.ToArray());
            //    result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
            //    return result;
            //}
            using (FileStream file = new FileStream(@"D:\Image\5.png", FileMode.OpenOrCreate, FileAccess.Read))
            {
                //var fileBytes = new byte[fileStream.Length];
                //fileStream.Read(fileBytes, 0, fileBytes.Length);
                //return File(fileBytes, "image/jpeg");
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                using (Image img = Image.FromStream(file))
                using (MemoryStream ms = new MemoryStream())
                {
                    //使用指定的编码器和图像编码器参数，将该 Image 保存到指定的文件
                    img.Save(ms, ImageFormat.Jpeg);
                    result.Content = new ByteArrayContent(ms.ToArray());
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                    return result;
                }
            }
        }
        /// <summary>
        /// 文件流
        /// </summary>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public HttpResponseMessage DownloadFile()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new MemoryStream());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "101.pdf"
            };
            return response;
        }
        /// <summary>
        /// 下载图片
        /// </summary>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public IActionResult DownloadImage2()
        {
            var filePath = @"D:\Image\5.png";
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "image/jpeg");
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public IActionResult DownloadFile2()
        {
            var filePath = @"D:\Image\101.pdf";
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "101.pdf");
        }


        /// <summary>
        /// 下载图片
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public HttpResponseMessage DownloadImage4()
        {
            var filePath = @"D:\Image\5.png";
            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                var fileBytes = new byte[fileStream.Length];
                fileStream.Read(fileBytes, 0, fileBytes.Length);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new ByteArrayContent(fileBytes);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                return response;
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public HttpResponseMessage DownloadFile4()
        {
            var filePath = @"D:\Image\101.pdf";
            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                var fileBytes = new byte[fileStream.Length];
                fileStream.Read(fileBytes, 0, fileBytes.Length);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new ByteArrayContent(fileBytes);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "101.pdf"
                };
                return response;
            }
        }
        [HttpPost, AllowAnonymous]
        public byte[] ConvertImageToByteArray()
        {
            var filePath = @"D:\Image\5.png";
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var fileBytes = new byte[fileStream.Length];
                fileStream.Read(fileBytes, 0, fileBytes.Length);
                return fileBytes;
            }
        }

        ////文件地址
        //var fileUrl = Request.Form["fileUrl"].ToString();
        ////校验
        //if (string.IsNullOrWhiteSpace(fileUrl)) throw new ErpFriendlyException("要下载的文件地址不能为空！");
        ////文件路径
        ////string filePath = FileUtil.GetFullPath(fileUrl);
        //string filePath = Path.Combine(FileUtil.GetFullPath(fileUrl));
        ////校验
        //if (!System.IO.File.Exists(filePath)) throw new ErpFriendlyException("要下载的文件不存在！");
        ////新文件
        //var copyFile = new SaveFileOutput(Path.GetFileName(filePath), SaveFileOutputFileNameTypeEnum.TimeStamp, "UploadFiles", "DownloadFile", "Copy");
        ////复制文件
        //System.IO.File.Copy(filePath, copyFile.FileFullPath, true);
        ////文件名
        //var fileName = Request.Form["fileName"].ToString();
        ////文件名
        //if (string.IsNullOrWhiteSpace(fileName)) fileName = Path.GetFileName(filePath);
        ////下载
        //return new FileStreamResult(new FileStream(copyFile.FileFullPath, FileMode.Open), "application/octet-stream") { FileDownloadName = fileName.TrimStr() };

        //public async Task<IActionResult> ShowImage()
        //{
        //    using (var sw = new FileStream(file_path, FileMode.Open))
        //    {
        //        var bytes = new byte[sw.Length];
        //        sw.Read(bytes, 0, bytes.Length);
        //        sw.Close();
        //        return new FileContentResult(bytes, "image/jpeg");
        //    }
        //}
    }
}
