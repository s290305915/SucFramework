using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace SucLib.Common
{
    public class SucUpload
    {
        private int _maxFileSize;
        private string _allowFileExtens;
        private string _resultMessage;
        private string _filePath;
        private string _resultFileName;
        /// <summary>
        /// 大小上限
        /// </summary>
        public int MaxFileSize
        {
            set
            {
                this._maxFileSize = value;
            }
        }
        /// <summary>
        /// 自动生成文件后缀名
        /// </summary>
        public string AllowFileExtens
        {
            set
            {
                this._allowFileExtens = value;
            }
        }
        /// <summary>
        /// 结果信息
        /// </summary>
        public string ResultMessage
        {
            get
            {
                return this._resultMessage;
            }
        }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath
        {
            set
            {
                this._filePath = value;
            }
        }
        /// <summary>
        /// 结果文件名
        /// </summary>
        public string ResultFileName
        {
            get
            {
                return this._resultFileName;
            }
        }
        /// <summary>
        /// 上传文件方法（路径存在postfile里）€
        /// </summary>
        /// <param name="postedFile">文件访问</param>
        /// <param name="type">文件类型</param>
        /// <returns>是否上传成功</returns>
        public bool DoUpload(HttpPostedFile postedFile, string type)
        {
            bool result = true;
            int maxFileSize = this._maxFileSize;
            string allowFileExtens = this._allowFileExtens;
            bool flag = false;
            try
            {
                if (postedFile.ContentLength > 0)
                {
                    if (postedFile.ContentLength > maxFileSize * 1000)
                    {
                        this._resultMessage = "上文件大小超过限定值!(最大<b>" + this.CaculatorSize(maxFileSize * 1000) + "</b>)";
                        flag = true;
                        this._resultFileName = "";
                        result = false;
                    }
                    if (!flag && !this.IsAllowFileExtens(allowFileExtens, this.GetFileExtens(postedFile.FileName.ToLower())))
                    {
                        this._resultMessage = "上传的文件类型不正确!(只允许上传<b>" + allowFileExtens + "</b>)";
                        flag = true;
                        this._resultFileName = "";
                        result = false;
                    }
                    if (!flag && !this.IsRightFile(postedFile))
                    {
                        this._resultMessage = "上传文件出错!";
                        flag = true;
                        this._resultFileName = "";
                        result = false;
                    }
                    if (!flag)
                    {
                        this._resultMessage = "上传成功!";
                        string text = CommUtil.GetDataTimeRandomFileName() + "." + this.GetFileExtens(postedFile.FileName);
                        string filename = this._filePath + type + text;
                        postedFile.SaveAs(filename);
                        this._resultFileName = text;
                        result = true;
                    }
                }
                else
                {
                    this._resultMessage = "请选择文件!";
                    this._resultFileName = "";
                    result = false;
                }
            }
            catch
            {
                this._resultMessage = "上传文件出错!";
                this._resultFileName = "";
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 计算大小
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        protected string CaculatorSize(int s)
        {
            if (s < 1024)
            {
                return s + " B";
            }
            if (s / 1024 < 1024)
            {
                return s / 1024 + " KB";
            }
            if (s / 1024 / 1024 < 1024)
            {
                return s / 1024 / 1024 + " M";
            }
            if (s / 1024 / 1024 / 1024 < 1024)
            {
                return s / 1024 / 1024 / 1024 + " G";
            }
            return "";
        }
        /// <summary>
        /// 后缀名
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected string GetFileExtens(string p)
        {
            string text = p.Substring(p.LastIndexOf("\\") + 1);
            return text.Split(new char[]
			{
				'.'
			})[1].ToLower();
        }
        /// <summary>
        /// 是否需要自动生成后缀名
        /// </summary>
        /// <param name="allowExtens"></param>
        /// <param name="nowExtens"></param>
        /// <returns></returns>
        protected bool IsAllowFileExtens(string allowExtens, string nowExtens)
        {
            return allowExtens.IndexOf(nowExtens) > -1;
        }
        /// <summary>
        /// 文件类型
        /// </summary>
        /// <returns></returns>
        protected Hashtable GetStandardExtentsFeater()
        {
            return new Hashtable
			{
				
				{
					"gif",
					"image/gif"
				},
				
				{
					"jpg",
					"image/pjpeg"
				},
				
				{
					"jpeg",
					"image/pjpeg"
				},
				
				{
					"bmp",
					"image/bmp"
				},
				
				{
					"png",
					"image/x-png"
				},
				
				{
					"tif",
					"image/tiff"
				},
				
				{
					"tiff",
					"image/tiff"
				},
				
				{
					"zip",
					"application/x-zip-compressed"
				},
				
				{
					"rar",
					"application/octet-stream"
				},
				
				{
					"wav",
					"all"
				},
				
				{
					"wmv",
					"all"
				},
				
				{
					"mp4",
					"all"
				},
				
				{
					"flv",
					"all"
				},
				
				{
					"xls",
					"all"
				},
				
				{
					"doc",
					"all"
				},
				
				{
					"ppt",
					"all"
				},
				
				{
					"xlsx",
					"all"
				},
				
				{
					"docx",
					"all"
				},
				
				{
					"pptx",
					"all"
				}
			};
        }
        /// <summary>
        /// 获取后缀名
        /// </summary>
        /// <param name="FileExtents"></param>
        /// <returns></returns>
        protected string GetExtentsFeatureString(string FileExtents)
        {
            Hashtable standardExtentsFeater = this.GetStandardExtentsFeater();
            string result = "";
            foreach (DictionaryEntry dictionaryEntry in standardExtentsFeater)
            {
                if (dictionaryEntry.Key.ToString() == FileExtents)
                {
                    result = dictionaryEntry.Value.ToString();
                }
            }
            return result;
        }
        /// <summary>
        /// 检测文件 
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        protected bool IsRightFile(HttpPostedFile postedFile)
        {
            string fileExtens = this.GetFileExtens(postedFile.FileName);
            string extentsFeatureString = this.GetExtentsFeatureString(fileExtens);
            return postedFile.ContentType == extentsFeatureString;
        }
    }
}
